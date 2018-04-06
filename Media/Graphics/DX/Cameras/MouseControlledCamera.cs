using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using SlimDX;
using SlimDX.Direct3D9;

using EngineDesigner.Common;
using EngineDesigner.Media;

namespace EngineDesigner.Media.Graphics.DX.Cameras
{
    public class MouseControlledCamera : ArcBallCamera
    {
        public event EventHandler<MouseControlledCameraEventArgs> AnchorChanged;
        public event EventHandler<MouseControlledCameraEventArgs> AngleChanged;
        public event EventHandler<MouseControlledCameraEventArgs> OrbitalRadiusChanged;

        //OBSOLETE: morda sem samo kaj preizkušal in pozabil noter, ker ne vem kaj je
        //public System.Windows.Forms.ControlBindingsCollection cbc = new System.Windows.Forms.ControlBindingsCollection(null);

        private int mouseAngleStartX = 0;
        private int mouseAngleStartY = 0;
        private int mouseAngleCurrentX = 0;
        private int mouseAngleCurrentY = 0;
        private int mouseAngleEndX = 0;
        private int mouseAngleEndY = 0;

        private int mouseAnchorStartX = 0;
        private int mouseAnchorStartY = 0;
        private int mouseAnchorCurrentX = 0;
        private int mouseAnchorCurrentY = 0;
        private int mouseAnchorEndX = 0;
        private int mouseAnchorEndY = 0;

        private int mouseOrbitalRadiusStartY = 0;
        private int mouseOrbitalRadiusCurrentY = 0;
        private int mouseOrbitalRadiusEndY = 0;



        private float angleChangeMagnitude = 0.5f;
        [DefaultValue(0.5f)]
        public float AngleChangeMagnitude
        {
            get { return angleChangeMagnitude; }
            set { angleChangeMagnitude = value; }
        }

        private float anchorChangeMagnitude = 0.5f;
        [DefaultValue(0.5f)]
        public float AnchorChangeMagnitude
        {
            get { return anchorChangeMagnitude; }
            set { anchorChangeMagnitude = value; }
        }

        private float orbitalRadiusChangeMagnitude = 0.5f;
        [DefaultValue(0.5f)]
        public float OrbitalRadiusChangeMagnitude
        {
            get { return orbitalRadiusChangeMagnitude; }
            set { orbitalRadiusChangeMagnitude = value; }
        }



        private void SetAngle(int _x, int _y)
        {
            base.horizontalAngle_deg = Mathematics.GetAbsoluteAngle_deg(_x * angleChangeMagnitude);
            base.verticalAngle_deg = Mathematics.GetAbsoluteAngle_deg(_y * angleChangeMagnitude);
            BuildViewMatrix();

            OnAngleChanged();
        }
        private void SetAnchor(int _x, int _y)
        {
            CustomVector3 _lookAt = CustomVector3.From(EngineDesigner.Media.Utility.ExtractLookAt(base.ViewMatrix));
            CustomVector3 _right = CustomVector3.From(EngineDesigner.Media.Utility.ExtractRight(base.ViewMatrix));
            CustomVector3 _up = CustomVector3.From(EngineDesigner.Media.Utility.ExtractUp(base.ViewMatrix));

            base.anchor += _right * (_y * anchorChangeMagnitude);
            base.anchor -= _up * (_x * anchorChangeMagnitude);

            base.BuildViewMatrix();


            OnAnchorChanged();
        }
        private void SetOrbitalRadius(int _orbitalRadius)
        {
            float _properOrbitalRadius = _orbitalRadius * orbitalRadiusChangeMagnitude;

            if (_properOrbitalRadius >= 1)
            {
                base.orbitalRadius = _properOrbitalRadius;
                base.BuildViewMatrix();

                OnOrbitalRadiusChanged();
            }
        }



        //events raising
        private void OnAnchorChanged()
        {
            if (AnchorChanged != null)
            {
                AnchorChanged(
                    this,
                    new MouseControlledCameraEventArgs(
                        base.HorizontalAngle_deg,
                        base.VerticalAngle_deg,
                        base.Anchor,
                        base.OrbitalRadius));
            }
        }
        private void OnAngleChanged()
        {
            if (AngleChanged != null)
            {
                AngleChanged(
                    this,
                    new MouseControlledCameraEventArgs(
                        base.HorizontalAngle_deg,
                        base.VerticalAngle_deg,
                        base.Anchor,
                        base.OrbitalRadius));
            }
        }
        private void OnOrbitalRadiusChanged()
        {
            if (OrbitalRadiusChanged != null)
            {
                OrbitalRadiusChanged(
                    this,
                    new MouseControlledCameraEventArgs(
                        base.HorizontalAngle_deg,
                        base.VerticalAngle_deg,
                        base.Anchor,
                        base.OrbitalRadius));
            }
        }



        protected override bool OnAnchorSetExternally(CustomVector3 _oldAnchor, CustomVector3 _newAnchor)
        {
            mouseAnchorCurrentX = Convert.ToInt32(_newAnchor.X / anchorChangeMagnitude);
            mouseAnchorEndX = mouseAnchorCurrentX;
            mouseAnchorCurrentY = Convert.ToInt32(_newAnchor.Y / anchorChangeMagnitude);
            mouseAnchorEndY = mouseAnchorCurrentY;

            return base.OnAnchorSetExternally(_oldAnchor, _newAnchor);
        }
        protected override bool OnHorizontalAngleSetExternally(float _oldAngle_deg, float _newAngle_deg)
        {
            mouseAngleCurrentX = Convert.ToInt32(_newAngle_deg / angleChangeMagnitude);
            mouseAngleEndX = mouseAngleCurrentX;

            return base.OnHorizontalAngleSetExternally(_oldAngle_deg, _newAngle_deg);
        }
        protected override bool OnVerticalAngleSetExternally(float _oldAngle_deg, float _newAngle_deg)
        {
            mouseAngleCurrentY = Convert.ToInt32(_newAngle_deg / angleChangeMagnitude);
            mouseAngleEndY = mouseAngleCurrentY;

            return base.OnVerticalAngleSetExternally(_oldAngle_deg, _newAngle_deg);
        }
        protected override bool OnOrbitalRadiusSetExternally(float _oldZoom, float _newZoom)
        {
            mouseOrbitalRadiusCurrentY = Convert.ToInt32(_newZoom / orbitalRadiusChangeMagnitude);
            mouseOrbitalRadiusEndY = mouseOrbitalRadiusCurrentY;

            return base.OnOrbitalRadiusSetExternally(_oldZoom, _newZoom);
        }



        public void MouseAngleDown(int _x, int _y)
        {
            mouseAngleStartX = _x;
            mouseAngleStartY = _y;
        }
        public void MouseAngleMove(int _x, int _y)
        {
            int _deltaX = _x - mouseAngleStartX;
            int _deltaY = _y - mouseAngleStartY;

            mouseAngleCurrentX = _deltaX + mouseAngleEndX;
            mouseAngleCurrentY = _deltaY + mouseAngleEndY;

            SetAngle(mouseAngleCurrentX, mouseAngleCurrentY);
        }
        public void MouseAngleUp()
        {
            mouseAngleEndX = mouseAngleCurrentX;
            mouseAngleEndY = mouseAngleCurrentY;
        }

        public void MouseAnchorDown(int _x, int _y)
        {
            mouseAnchorStartX = _x;
            mouseAnchorStartY = _y;
        }
        public void MouseAnchorMove(int _x, int _y)
        {
            int _deltaX = _x - mouseAnchorStartX;
            int _deltaY = _y - mouseAnchorStartY;

            SetAnchor(_deltaX, _deltaY);

            mouseAnchorStartX = _x;
            mouseAnchorStartY = _y;
        }

        public void MouseOrbitalRadiusDown(int _y)
        {
            mouseOrbitalRadiusStartY = _y;
        }
        public void MouseOrbitalRadiusMove(int _y)
        {
            int _deltaY = _y - mouseOrbitalRadiusStartY;

            mouseOrbitalRadiusCurrentY = _deltaY + mouseOrbitalRadiusEndY;

            SetOrbitalRadius(mouseOrbitalRadiusCurrentY);
        }
        public void MouseOrbitalRadiusUp()
        {
            mouseOrbitalRadiusEndY = mouseOrbitalRadiusCurrentY;
        }

    }


    public class MouseControlledCameraEventArgs : EventArgs
    {
        private float horizontalAngle_deg;
        public float HorizontalAngle_deg
        {
            get { return horizontalAngle_deg; }
        }

        private float verticalAngle_deg;
        public float VerticalAngle_deg
        {
            get { return verticalAngle_deg; }
        }

        private CustomVector3 anchor;
        public CustomVector3 Anchor
        {
            get { return anchor; }
        }

        private float orbitalRadius;
        public float OrbitalRadius
        {
            get { return orbitalRadius; }
        }



        internal MouseControlledCameraEventArgs(float _horizontalAngle_deg, float _verticalAngle_deg, CustomVector3 _anchor, float _orbitalRadius)
            : base()
        {
            horizontalAngle_deg = _horizontalAngle_deg;
            verticalAngle_deg = _verticalAngle_deg;
            anchor = _anchor;
            orbitalRadius = _orbitalRadius;
        }
    }

}


