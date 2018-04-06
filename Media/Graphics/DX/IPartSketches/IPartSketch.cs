using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.Design;

using SlimDX;
using SlimDX.Direct3D9;

using EngineDesigner.Machine;
using EngineDesigner.Media;
using EngineDesigner.Media.Graphics.DX.Cameras;

namespace EngineDesigner.Media.Graphics.DX.IPartSketches
{
    public class IPartSketch : Panel3DBase
    {
        private Padding zoomToFitClearance = Padding.Empty;
        [DefaultValue(typeof(Padding), "0, 0, 0, 0")]
        public Padding ZoomToFitClearance
        {
            get { return zoomToFitClearance; }

            set
            {
                if (value.Left < 0
                    || value.Right < 0
                    || value.Top < 0
                    || value.Bottom < 0
                    || value.Left > this.Width
                    || value.Right > this.Width
                    || value.Top > this.Width
                    || value.Bottom > this.Height)
                {
                    throw new ArgumentOutOfRangeException("ZoomToFitClearance must not define an area out of the bounds of the control itself.");
                }

                zoomToFitClearance = value;
            }
        }

        private bool showBoundingBox = false;
        [DefaultValue(false)]
        public bool ShowBoundingBox
        {
            get { return showBoundingBox; }

            set
            {
                showBoundingBox = value;

                this.Refresh();
            }
        }

        private Color boundingBoxColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color BoundingBoxColor
        {
            get { return boundingBoxColor; }

            set
            {
                boundingBoxColor = value;
                this.Refresh();
            }
        }

        private IPart iPart = null;
        [DefaultValue(null)]
        public virtual IPart IPart
        {
            get { return iPart; }

            set
            {
                iPart = value;
                this.Refresh();
            }
        }



        protected override void Render(Device _device)
        {
            base.Render(_device);

            if (showBoundingBox)
            {
                if (iPart != null)
                {
                    _device.SetRenderState<FillMode>(RenderState.FillMode, FillMode.Wireframe);

                    Material _material = new Material();
                    _material.Ambient = boundingBoxColor;
                    _material.Diffuse = boundingBoxColor;
                    _device.Material = _material;

                    _device.SetTransform(TransformState.World, Matrix.Translation(
                        (float)((iPart.Width / 2) + iPart.Bound_X_Min),
                        (float)((iPart.Height / 2) + iPart.Bound_Y_Min),
                        (float)((iPart.Length / 2) + iPart.Bound_Z_Min)));

                    Mesh _mesh = Mesh.CreateBox(
                        _device,
                        (float)iPart.Width,
                        (float)iPart.Height,
                        (float)iPart.Length);
                    _mesh.DrawSubset(0);
                    _mesh.Dispose();
                }
            }
        }



        public void View_ZoomToFit()
        {
            base.ResetDevice(); //moramo resetirat, čene ne dela

            base.Camera.OrbitalRadius = 1;

            Vector3[] _iPartBoundingBox = GetIPartBoundingBox(iPart);
            Rectangle _zoomToFitArea = new Rectangle(
                zoomToFitClearance.Left,
                zoomToFitClearance.Top,
                this.Width - zoomToFitClearance.Right,
                this.Height - zoomToFitClearance.Bottom);


            float _currentRadiusIncrement = -1000f;
            bool _radiusFound = false;
            while (!_radiusFound)
            {
                _currentRadiusIncrement /= -10f;

                if (_currentRadiusIncrement > 0)
                {
                    #region "_currentRadiusIncrement > 0"
                    int a = 0;
                    while (IsBoundingBoxOutOfArea(_iPartBoundingBox, _zoomToFitArea, base.Device, base.Camera))
                    {
                        a++;

                        base.Camera.OrbitalRadius += _currentRadiusIncrement; //se oddaljujemo

                        if (_currentRadiusIncrement < 1) //če nismo na decimalkah, ne preverjamo ločljivosti floata
                        {
                            if (a > 10) //ločljivost floata je dosegla mejo, zato prekinemo
                            {
                                _radiusFound = true;
                                break;
                            }
                        }
                    }
                    #endregion "_currentRadiusIncrement > 0"
                }
                else if (_currentRadiusIncrement < 0)
                {
                    #region "_currentRadiusIncrement < 0"
                    int a = 0;
                    while (!IsBoundingBoxOutOfArea(_iPartBoundingBox, _zoomToFitArea, base.Device, base.Camera))
                    {
                        a++;

                        if (base.Camera.OrbitalRadius + _currentRadiusIncrement >= 1) //ne sme bit manjši od 1, čene kamera vrže exception
                        {
                            base.Camera.OrbitalRadius += _currentRadiusIncrement; //se približujemo

                            if (_currentRadiusIncrement < 1) //če nismo na decimalkah, ne preverjamo ločljivosti floata
                            {
                                if (a > 10) //ločljivost floata je dosegla mejo, zato prekinemo
                                {
                                    _radiusFound = true;
                                    break;
                                }
                            }
                        }
                        else //nismo uspeli ma prekinemo ker ni druge možnosti
                        {
                            _radiusFound = true;
                            break;
                        }
                    }
                    #endregion "_currentRadiusIncrement < 0"
                }
            }


            this.Refresh();
        }
        private Vector3[] GetIPartBoundingBox(IPart _iPart)
        {
            //kocka okoli dela
            Vector3[] _boundingBox = new Vector3[]
            {
                new Vector3((float)_iPart.Bound_X_Min, (float)_iPart.Bound_Y_Min, (float)_iPart.Bound_Z_Min),

                new Vector3((float)_iPart.Bound_X_Min, (float)_iPart.Bound_Y_Min, (float)_iPart.Bound_Z_Max),
                new Vector3((float)_iPart.Bound_X_Min, (float)_iPart.Bound_Y_Max, (float)_iPart.Bound_Z_Max),
                new Vector3((float)_iPart.Bound_X_Min, (float)_iPart.Bound_Y_Max, (float)_iPart.Bound_Z_Min),

                new Vector3((float)_iPart.Bound_X_Max, (float)_iPart.Bound_Y_Max, (float)_iPart.Bound_Z_Min),
                new Vector3((float)_iPart.Bound_X_Max, (float)_iPart.Bound_Y_Min, (float)_iPart.Bound_Z_Min),
                new Vector3((float)_iPart.Bound_X_Max, (float)_iPart.Bound_Y_Min, (float)_iPart.Bound_Z_Max),
                
                new Vector3((float)_iPart.Bound_X_Max, (float)_iPart.Bound_Y_Max, (float)_iPart.Bound_Z_Max)
            };

            return _boundingBox;
        }
        private bool IsBoundingBoxOutOfArea(Vector3[] _boundingBox, Rectangle _area, Device _device, ArcBallCamera _arcBallCamera)
        {
            foreach (Vector3 _vector3 in _boundingBox)
            {
                Vector3 _projectedVector = Vector3.Project(
                    _vector3,
                    _device.Viewport.X,
                    _device.Viewport.Y,
                    _device.Viewport.Width,
                    _device.Viewport.Height,
                    _device.Viewport.MinZ,
                    _device.Viewport.MaxZ,
                    _arcBallCamera.ViewMatrix * _arcBallCamera.ProjectionMatrix);

                if ((_projectedVector.X < _area.X)
                    || (_projectedVector.X > _area.Width)
                    || (_projectedVector.Y < _area.Y)
                    || (_projectedVector.Y > _area.Height))
                {
                    return true;
                }
            }

            return false;
        }

    }

}
