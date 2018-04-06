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
    public class ArcBallCamera : Component
    {
        private const float ZERO_AGLE_CORRECTION = 0.001f;
        private const float HORIZONTAL_AGLE_CORRECTION = -180;



        public ArcBallCamera()
            : this(45f, 1f, 1f, 100000f)
        {
        }
        public ArcBallCamera(float _fieldOfView_deg, float _aspectRatio, float _nearPlane, float _farPlane)
        {
            fieldOfView_deg = _fieldOfView_deg;
            aspectRatio = _aspectRatio;
            nearPlane = _nearPlane;
            farPlane = _farPlane;


            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                BuildProjectionMatrix();
            }
        }



        private float aspectRatio = 1f;
        [DefaultValue(1f)]
        public float AspectRatio
        {
            get { return aspectRatio; }

            set
            {
                aspectRatio = value;
                BuildProjectionMatrix();
            }
        }

        private float fieldOfView_deg = 45f;
        [DefaultValue(45f)]
        public float FieldOfView_deg
        {
            get { return fieldOfView_deg; }

            set
            {
                fieldOfView_deg = value;
                BuildProjectionMatrix();
            }
        }

        private float nearPlane = 1f;
        [DefaultValue(1f)]
        public float NearPlane
        {
            get { return nearPlane; }

            set
            {
                nearPlane = value;
                BuildProjectionMatrix();
            }
        }

        private float farPlane = 100000f;
        [DefaultValue(100000f)]
        public float FarPlane
        {
            get { return farPlane; }

            set
            {
                farPlane = value;
                BuildProjectionMatrix();
            }
        }

        protected float horizontalAngle_deg = 0f;
        [DefaultValue(0f)]
        public float HorizontalAngle_deg
        {
            get { return horizontalAngle_deg; }

            set
            {
                if (OnHorizontalAngleSetExternally(horizontalAngle_deg, value))
                {
                    horizontalAngle_deg = Mathematics.GetAbsoluteAngle_deg(value);
                    BuildViewMatrix();
                }
            }
        }

        protected float verticalAngle_deg = 0f;
        [DefaultValue(0f)]
        public float VerticalAngle_deg
        {
            get { return verticalAngle_deg; }

            set
            {
                if (OnVerticalAngleSetExternally(verticalAngle_deg, value))
                {
                    verticalAngle_deg = Mathematics.GetAbsoluteAngle_deg(value);
                    BuildViewMatrix();
                }
            }
        }

        protected CustomVector3 anchor = CustomVector3.Zero;
        [DefaultValue(typeof(CustomVector3), "Zero")]
        public CustomVector3 Anchor
        {
            get { return anchor; }

            set
            {
                if (OnAnchorSetExternally(anchor, value))
                {
                    anchor = value;
                    BuildViewMatrix();
                }
            }
        }

        protected float orbitalRadius = 1f;
        [DefaultValue(1f)]
        public float OrbitalRadius
        {
            get { return orbitalRadius; }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Zoom must be greater than 1.");
                }

                if (OnOrbitalRadiusSetExternally(orbitalRadius, value))
                {
                    orbitalRadius = value;
                    BuildViewMatrix();
                }
            }
        }

        private Matrix projectionMatrix = Matrix.Identity;
        [Browsable(false)]
        public Matrix ProjectionMatrix
        {
            get { return projectionMatrix; }
        }

        private Matrix viewMatrix = Matrix.Identity;
        [Browsable(false)]
        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }



        private void BuildProjectionMatrix()
        {
            if ((fieldOfView_deg < 1) || (fieldOfView_deg > 180))
            {
                throw new ArgumentException("FieldOfView cannot be less than 1 or more than 180 degrees.");
            }

            if (aspectRatio < 0)
            {
                throw new ArgumentException("AspectRatio cannot be negative.");
            }


            this.projectionMatrix = Matrix.PerspectiveFovLH(
                Conversions.DegToRad(fieldOfView_deg),
                this.aspectRatio,
                this.nearPlane,
                this.farPlane);
        }

        protected void BuildViewMatrix()
        {
            #region "popravimo kot"
            float _verticalAngle_deg = verticalAngle_deg;
            if ((verticalAngle_deg == 0)
                || (Mathematics.IsMultiple(verticalAngle_deg, 180))
                || (Mathematics.IsMultiple(verticalAngle_deg, -180)))
            {
                _verticalAngle_deg += ZERO_AGLE_CORRECTION;
            }
            #endregion "popravimo kot"

            #region "dobimo pozicijo kamere v prostoru"
            //določimo orbito
            Vector3 _cameraOrbitPosition = new Vector3(
                0.0f,
                orbitalRadius,
                0.0f);

            //rotiramo čez X
            _cameraOrbitPosition = Vector3.TransformCoordinate(_cameraOrbitPosition, Matrix.RotationX(Conversions.DegToRad(_verticalAngle_deg)));

            //rotiramo čez Y
            float _horizontalAngle_deg = horizontalAngle_deg + HORIZONTAL_AGLE_CORRECTION;
            _cameraOrbitPosition = Vector3.TransformCoordinate(_cameraOrbitPosition, Matrix.RotationY(Conversions.DegToRad(_horizontalAngle_deg)));





            #endregion "dobimo pozicijo kamere v prostoru"

            #region "dobimo up vektor"
            Vector3 _up;
            if (_verticalAngle_deg > 180)
            {
                _up = new Vector3(0, -1, 0);
            }
            else if (_verticalAngle_deg < -180)
            {
                _up = new Vector3(0, 1, 0);
            }
            else if (_verticalAngle_deg < 0)
            {
                _up = new Vector3(0, -1, 0);
            }
            else
            {
                _up = new Vector3(0, 1, 0);
            }
            #endregion "dobimo up vektor"


            this.viewMatrix =
                Matrix.LookAtLH(
                    _cameraOrbitPosition + anchor.ToVector3(),
                    anchor.ToVector3(),
                    _up);
        }



        protected virtual bool OnAnchorSetExternally(CustomVector3 _oldAnchor, CustomVector3 _newAnchor)
        {
            return true;
        }
        protected virtual bool OnHorizontalAngleSetExternally(float _oldAngle_deg, float _newAngle_deg)
        {
            return true;
        }
        protected virtual bool OnVerticalAngleSetExternally(float _oldAngle_deg, float _newAngle_deg)
        {
            return true;
        }
        protected virtual bool OnOrbitalRadiusSetExternally(float _oldZoom, float _newZoom)
        {
            return true;
        }



        public void Left()
        {
            HorizontalAngle_deg = 90f;
            VerticalAngle_deg = 90f;
        }
        public void Right()
        {
            HorizontalAngle_deg = 270f;
            VerticalAngle_deg = 90f;
        }
        public void Top()
        {
            HorizontalAngle_deg = 0f + ZERO_AGLE_CORRECTION;
            VerticalAngle_deg = 0f + ZERO_AGLE_CORRECTION;
        }
        public void Bottom()
        {
            HorizontalAngle_deg = 0f;
            VerticalAngle_deg = 180f;
        }
        public void Front()
        {
            HorizontalAngle_deg = 0f;
            VerticalAngle_deg = 90f;
        }
        public void Back()
        {
            HorizontalAngle_deg = 180f;
            VerticalAngle_deg = 90f;
        }
        public void Isometric()
        {
            HorizontalAngle_deg = 315f;
            VerticalAngle_deg = 45f;
        }
        public void Reset()
        {
            Anchor = CustomVector3.Zero;
            Top();
        }

    }

}
