using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SlimDX;
using SlimDX.Direct3D9;

using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;
using EngineDesigner.Machine;
using EngineDesigner.Machine.Definitions;


namespace EngineDesigner.TestForms
{
    internal partial class CoordinateSystemFormD3D : Form
    {
        protected PresentParameters presentParams;
        protected Device device = null;



        public CoordinateSystemFormD3D()
        {
            InitializeComponent();


            //prevent painting from happening outside of OnPaint method
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            InitializeGraphics();
        }
        private void CoordinateSystemFormD3D_Paint(object sender, PaintEventArgs e)
        {
            device_DeviceReset(null, null);


            #region "Render"
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Silver, 1.0f, 0);

            SetupCamera();

            //start editing the scene
            device.BeginScene();

            DrawCoordinateSystem();

            Render();

            //end editing the scene
            device.EndScene();

            //present the scene on the device
            device.Present();
            #endregion "Render"
        }



        protected float FieldOfView_deg
        {
            get { return (float)numericUpDown_ProjectionFieldOfView.Value; }
        }
        protected float AspectRatio
        {
            get { return (float)(numericUpDown_ProjectionAspectRatio.Value / 100); }
        }
        protected float NearPlane
        {
            get { return (float)numericUpDown_ProjectionNearPlane.Value; }
        }
        protected float FarPlane
        {
            get { return (float)numericUpDown_ProjectionFarPlane.Value; }
        }
        protected float PositionX
        {
            get { return (float)numericUpDown_PositionX.Value; }
        }
        protected float PositionY
        {
            get { return (float)numericUpDown_PositionY.Value; }
        }
        protected float PositionZ
        {
            get { return (float)numericUpDown_PositionZ.Value; }
        }
        protected Vector3 UpVector
        {
            get
            {
                Vector3 _upVector = Vector3.Zero;

                if (radioButton_PositionXUpPos.Checked)
                {
                    _upVector = new Vector3(1f, 0f, 0f); //x je gor
                }
                else if (radioButton_PositionXUpNeg.Checked)
                {
                    _upVector = new Vector3(-1f, 0f, 0f); //x je dol
                }
                else if (radioButton_PositionYUpPos.Checked)
                {
                    _upVector = new Vector3(0f, 1f, 0f); //y je gor
                }
                else if (radioButton_PositionYUpNeg.Checked)
                {
                    _upVector = new Vector3(0f, -1f, 0f); //y je dol
                }
                else if (radioButton_PositionZUpPos.Checked)
                {
                    _upVector = new Vector3(0f, 0f, 1f); //z je gor
                }
                else if (radioButton_PositionZUpNeg.Checked)
                {
                    _upVector = new Vector3(0f, 0f, -1f); //z je dol
                }

                return _upVector;
            }
        }



        private void InitializeGraphics()
        {
            this.presentParams = new PresentParameters();
            this.presentParams.Windowed = true;
            this.presentParams.SwapEffect = SwapEffect.Discard;
            this.presentParams.EnableAutoDepthStencil = true;
            this.presentParams.AutoDepthStencilFormat = Format.D16;
            this.presentParams.DeviceWindowHandle = this.Handle;    


            // Create our device
            Direct3D _direct3D = new Direct3D();
            this.device = new Device(_direct3D, 0, DeviceType.Hardware, this.Handle, CreateFlags.HardwareVertexProcessing, this.presentParams);
            //device.DeviceReset += new EventHandler(device_DeviceReset);
            device_DeviceReset(null, null);


        }
        private void device_DeviceReset(object sender, EventArgs e)
        {
            if (checkBox_Solid.Checked)
            {
                this.device.SetRenderState<FillMode>(RenderState.FillMode, FillMode.Solid);
            }
            else
            {
                this.device.SetRenderState<FillMode>(RenderState.FillMode, FillMode.Wireframe);
            }

            device.SetRenderState(RenderState.Lighting, true);
            device.SetRenderState(RenderState.Ambient, Color.White.ToArgb());

            //define somelighting
            Light _light = new Light();
            _light.Type = LightType.Directional;
            _light.Diffuse = colorDialog1.Color;
            _light.Direction = new Vector3(
                (float)numericUpDown_LightX.Value,
                (float)numericUpDown_LightY.Value,
                (float)numericUpDown_LightZ.Value);
            device.SetLight(0, _light);
        }
        protected virtual void SetupCamera()
        {
            Vector3 _position = new Vector3(
                (float)numericUpDown_PositionX.Value,
                (float)numericUpDown_PositionY.Value,
                (float)numericUpDown_PositionZ.Value);

            Vector3 _lookingAt = Vector3.Zero;


            this.device.SetTransform(TransformState.View, Matrix.LookAtLH(
                _position,
                _lookingAt,
                this.UpVector));

            this.device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(
                Conversions.DegToRad((float)numericUpDown_ProjectionFieldOfView.Value),
                 (float)(numericUpDown_ProjectionAspectRatio.Value / 100),
                 (float)numericUpDown_ProjectionNearPlane.Value,
                 (float)numericUpDown_ProjectionFarPlane.Value));
        }
        private void DrawCoordinateSystem()
        {
            float _coordinateSystemLength = 5000f;
            float _coordinateSystemThickness = 0.01f;

            Material boxMaterial = new Material();
            boxMaterial.Ambient = Color.Blue;
            boxMaterial.Diffuse = Color.Blue;
            device.Material = boxMaterial;

            Mesh _mesh;

            device.SetTransform(TransformState.World, Matrix.Translation(0f, 0f, 0f));
            _mesh = Mesh.CreateBox(device, _coordinateSystemThickness, _coordinateSystemThickness, _coordinateSystemLength);
            _mesh.DrawSubset(0);
            _mesh.Dispose();

            _mesh = Mesh.CreateBox(device, _coordinateSystemThickness, _coordinateSystemLength, _coordinateSystemThickness);
            _mesh.DrawSubset(0);
            _mesh.Dispose();

            _mesh = Mesh.CreateBox(device, _coordinateSystemLength, _coordinateSystemThickness, _coordinateSystemThickness);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }


        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void checkBox_Solid_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void radioButton_PositionUp_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void button_LightColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            this.Invalidate();
        }



        protected virtual void Render()
        {
        }

    }
}
