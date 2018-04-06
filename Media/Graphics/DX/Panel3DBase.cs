using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

using SlimDX;
using SlimDX.Direct3D9;

using EngineDesigner.Machine;
using EngineDesigner.Media;
using EngineDesigner.Media.Graphics.DX.Cameras;

namespace EngineDesigner.Media.Graphics.DX
{
    public abstract partial class Panel3DBase : Panel
    {
        private const int SLEEP_ON_EXCEPTION = 100;
        private const float COORDINATE_SYSTEM_LENGTH = 5000f;
        private const float COORDINATE_SYSTEM_THICKNESS = 0.01f;



        private readonly Direct3D direct3D;
        private readonly PresentParameters presentParameters;
        private readonly Device device;
        protected Device @Device
        {
            get { return this.device; }
        }


        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MouseControlledCamera Camera
        {
            get { return mouseControlledCamera; }
        }

        private float mouseWheelChangeMagnitude = 10f;
        [DefaultValue(10f)]
        public float MouseWheelChangeMagnitude
        {
            get { return mouseWheelChangeMagnitude; }
            set { mouseWheelChangeMagnitude = value; }
        }

        private bool showCoordinateSystem = false;
        [DefaultValue(false)]
        public bool ShowCoordinateSystem
        {
            get { return showCoordinateSystem; }
            set { showCoordinateSystem = value; }
        }

        private Color coordinateSystemColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CoordinateSystemColor
        {
            get { return coordinateSystemColor; }

            set
            {
                coordinateSystemColor = value;
                this.Refresh();
            }
        }



        public Panel3DBase()
        {
            InitializeComponent();


            this.Disposed
                += new EventHandler(Panel3DBase_Disposed);


            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                //preprečimo, da bi se risanje dogajalo izven OnPaint metode
                this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);

                //present parameters
                this.presentParameters = new PresentParameters();
                this.presentParameters.Windowed = true;
                this.presentParameters.SwapEffect = SwapEffect.Discard;
                this.presentParameters.EnableAutoDepthStencil = true;
                this.presentParameters.AutoDepthStencilFormat = Format.D24S8;
                this.presentParameters.DeviceWindowHandle = this.Handle;
                this.presentParameters.PresentFlags = PresentFlags.DiscardDepthStencil;

                //kreiramo device
                this.direct3D = new Direct3D();
                this.device = new Device(this.direct3D, 0, DeviceType.Hardware, this.Handle, CreateFlags.HardwareVertexProcessing, this.presentParameters);
                this.ResetDevice();
            }
        }
        private void Panel3DBase_Disposed(object sender, EventArgs e)
        {
            this.Disposed
                += new EventHandler(Panel3DBase_Disposed);


            if (this.device != null)
            {
                this.device.Dispose();
                this.direct3D.Dispose();
            }
        }



        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);


            if (this.device != null)
            {
                this.ResetDevice();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            if ((!this.DesignMode)
                && (this.device != null))
            {
                #region "Render"
                this.device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, this.BackColor, 1.0f, 0);

                this.device.SetTransform(TransformState.View, this.mouseControlledCamera.ViewMatrix);

                this.device.BeginScene();
                this.Render(device);
                this.device.EndScene();

                try
                {
                    /*Result _result = */
                    this.device.Present();
                }
                catch (Direct3D9Exception _direct3D9Exception)
                {
                    if (_direct3D9Exception.ResultCode == ResultCode.DeviceLost)
                    {
                        if (this.device.TestCooperativeLevel() == ResultCode.DeviceLost)
                        {
                            System.Threading.Thread.Sleep(SLEEP_ON_EXCEPTION);
                        }
                        else
                        {
                            this.ResetDevice();
                        }
                    }
                    else
                    {
                        this.ShowException(e.Graphics, _direct3D9Exception);
                    }
                }
                catch (Exception _exception)
                {
                    this.ShowException(e.Graphics, _exception);
                }
                #endregion "Render"
            }
        }
        private void ShowException(System.Drawing.Graphics _graphics, Exception _exception)
        {
            _graphics.DrawString(_exception.ToString(),
                base.Font,
                Brushes.Black,
                new RectangleF(0, 0, base.Width, base.Height));

            System.Threading.Thread.Sleep(SLEEP_ON_EXCEPTION);
        }




        protected void ResetDevice()
        {
            //NOTE: ne vem zakaj to ne dela samo od sebe
            this.presentParameters.BackBufferWidth = base.Width;
            this.presentParameters.BackBufferHeight = base.Height;

            this.device.Reset(this.presentParameters);
            this.SetupLighting(this.device);
            this.device.SetTransform(TransformState.Projection, this.mouseControlledCamera.ProjectionMatrix);

        }
        protected virtual void SetupLighting(Device _device)
        {
            _device.SetRenderState(RenderState.Lighting, true);
            _device.SetRenderState(RenderState.Ambient, Color.White.ToArgb());
        }
        protected virtual void Render(Device _device)
        {
            if (this.showCoordinateSystem)
            {
                this.SetMaterial(_device, this.coordinateSystemColor, true);


                Mesh _mesh;

                _device.SetTransform(TransformState.World, Matrix.Translation(0f, 0f, 0f));

                _mesh = Mesh.CreateBox(_device, COORDINATE_SYSTEM_THICKNESS, COORDINATE_SYSTEM_THICKNESS, COORDINATE_SYSTEM_LENGTH);
                _mesh.DrawSubset(0);
                _mesh.Dispose();

                _mesh = Mesh.CreateBox(_device, COORDINATE_SYSTEM_THICKNESS, COORDINATE_SYSTEM_LENGTH, COORDINATE_SYSTEM_THICKNESS);
                _mesh.DrawSubset(0);
                _mesh.Dispose();

                _mesh = Mesh.CreateBox(_device, COORDINATE_SYSTEM_LENGTH, COORDINATE_SYSTEM_THICKNESS, COORDINATE_SYSTEM_THICKNESS);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
            }
        }
        protected virtual void SetMaterial(Device _device, Color _color, bool _solid)
        {
            Material _material = new Material();
            _material.Ambient = _color;
            _device.Material = _material;

            if (_solid)
            {
                _device.SetRenderState<FillMode>(RenderState.FillMode, FillMode.Solid);
            }
            else
            {
                _device.SetRenderState<FillMode>(RenderState.FillMode, FillMode.Wireframe);
            }
        }



        public void View_Left()
        {
            mouseControlledCamera.Left();
            this.Refresh();
        }
        public void View_Right()
        {
            mouseControlledCamera.Right();
            this.Refresh();
        }
        public void View_Top()
        {
            mouseControlledCamera.Top();
            this.Refresh();
        }
        public void View_Bottom()
        {
            mouseControlledCamera.Bottom();
            this.Refresh();
        }
        public void View_Front()
        {
            mouseControlledCamera.Front();
            this.Refresh();
        }
        public void View_Back()
        {
            mouseControlledCamera.Back();
            this.Refresh();
        }
        public void View_Isometric()
        {
            mouseControlledCamera.Isometric();
            this.Refresh();
        }
        public void View_Reset()
        {
            mouseControlledCamera.Reset();
            this.Refresh();
        }



        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();

            if (e.Button == MouseButtons.Left)
            {
                mouseControlledCamera.MouseAngleDown(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouseControlledCamera.MouseAnchorDown(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Middle)
            {
                mouseControlledCamera.MouseOrbitalRadiusDown(e.Y);
            }


            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseControlledCamera.MouseAngleMove(e.X, e.Y);
                this.Refresh();
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouseControlledCamera.MouseAnchorMove(e.X, e.Y);
                this.Refresh();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                mouseControlledCamera.MouseOrbitalRadiusMove(e.Y);
                this.Refresh();
            }


            base.OnMouseMove(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                mouseControlledCamera.OrbitalRadius += this.mouseWheelChangeMagnitude;
                this.Refresh();
            }
            else if (e.Delta > 0)
            {
                float _float = mouseControlledCamera.OrbitalRadius - this.mouseWheelChangeMagnitude;
                //manj kot 1 je exception!
                if (_float < 1)
                {
                    _float = 1;
                }

                mouseControlledCamera.OrbitalRadius = _float;
                this.Refresh();
            }


            base.OnMouseWheel(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseControlledCamera.MouseAngleUp();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                mouseControlledCamera.MouseOrbitalRadiusUp();
            }


            base.OnMouseUp(e);
        }

    }
}
