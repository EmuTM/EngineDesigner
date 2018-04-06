using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.FloatingForms;
using EngineDesigner.FloatingForms.EngineMonitors;
using EngineDesigner.FloatingForms.EngineMonitors.Analyzer;
using EngineDesigner.Machine;
using System.Windows.Forms.DataVisualization.Charting;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;

using SlimDX;
using SlimDX.Direct3D9;

using EngineDesigner.Media;
using EngineDesigner.Media.Graphics.DX.Cameras;
using EngineDesigner.Media.Graphics.DX.IPartSketches;
using EngineDesigner.Media.Graphics.DX;

namespace EngineDesigner.TestForms
{
    internal partial class TestForm_IPart : Form
    {
        private Engine engine;



        public TestForm_IPart()
        {
            InitializeComponent();
        }
        private void TestForm_IPart_Load(object sender, EventArgs e)
        {
            this.Show();

            this.engine = Engine.From(@"..\..\..\Samples\FourStrokeEngine.xml");
            this.engine.PositionedCylinders[1].CrankThrow.CrankRotationRadius_mm = 5;


            //Form_EngineControl _form_EngineControl = new Form_EngineControl(this);
            //_form_EngineControl.Engine = this.engine;
            //Form_Analyzer _form_Analyzer = new Form_Analyzer(this, _form_EngineControl);
            //_form_Analyzer.Engine = this.engine;
            //_form_Analyzer.ShowDialog();


            //this.pistonSketch1.BringToFront();
            //this.pistonSketch1.Dock = DockStyle.Fill;
            //this.pistonSketch1.BoundingBoxColor = Color.Red;
            //this.pistonSketch1.ShowCoordinateSystem = true;
            //this.pistonSketch1.IPart = this.engine[1].Piston;
            //this.pistonSketch1.ShowBoundingBox = true;
            //this.pistonSketch1.View_ZoomToFit();

            //this.connectingRodSketch1.BringToFront();
            //this.connectingRodSketch1.Dock = DockStyle.Fill;
            //this.connectingRodSketch1.BoundingBoxColor = Color.Red;
            //this.connectingRodSketch1.ShowCoordinateSystem = true;
            //this.connectingRodSketch1.IPart = this.engine[1].ConnectingRod;
            //this.connectingRodSketch1.ShowBoundingBox = true;
            //this.connectingRodSketch1.View_ZoomToFit();

            //this.crankThrowSketch1.BringToFront();
            //this.crankThrowSketch1.Dock = DockStyle.Fill;
            //this.crankThrowSketch1.BoundingBoxColor = Color.Red;
            //this.crankThrowSketch1.ShowCoordinateSystem = true;
            //this.crankThrowSketch1.IPart = this.engine[1].CrankThrow;
            //this.crankThrowSketch1.ShowBoundingBox = true;
            //this.crankThrowSketch1.View_ZoomToFit();

            //this.cylinderSketch1.BringToFront();
            //this.cylinderSketch1.Dock = DockStyle.Fill;
            //this.cylinderSketch1.BoundingBoxColor = Color.Red;
            //this.cylinderSketch1.ShowCoordinateSystem = true;
            //this.cylinderSketch1.IPart = (Cylinder)this.engine[1];
            //this.cylinderSketch1.ShowBoundingBox = true;
            //this.cylinderSketch1.View_ZoomToFit();

            //this.positionedCylinderSketch1.BringToFront();
            //this.positionedCylinderSketch1.Dock = DockStyle.Fill;
            //this.positionedCylinderSketch1.BoundingBoxColor = Color.Red;
            //this.positionedCylinderSketch1.ShowCoordinateSystem = true;
            //this.positionedCylinderSketch1.IPart = this.engine[1];
            //this.positionedCylinderSketch1.ShowBoundingBox = true;
            //this.positionedCylinderSketch1.View_ZoomToFit();

            this.flywheelSketch1.BringToFront();
            this.flywheelSketch1.Dock = DockStyle.Fill;
            this.flywheelSketch1.BoundingBoxColor = Color.Red;
            this.flywheelSketch1.ShowCoordinateSystem = true;
            this.flywheelSketch1.IPart = Flywheel.DefaultFlywheel;
            this.flywheelSketch1.ShowBoundingBox = true;
            this.flywheelSketch1.View_ZoomToFit();
        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown _numericUpDown = (NumericUpDown)sender;
            this.cylinderSketch1.CrankThrowRotation_deg = (double)_numericUpDown.Value;
            this.positionedCylinderSketch1.CrankThrowRotation_deg = (double)_numericUpDown.Value;
        }

    }


    public class PistonSketch : IPartSketch
    {
        private Piston piston = null;
        [DefaultValue(null)]
        public new Piston IPart
        {
            get
            {
                return piston;
            }

            set
            {
                piston = value;
                base.IPart = piston;
            }
        }

        private Color color = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color @Color
        {
            get { return color; }

            set
            {
                color = value;
                this.Refresh();
            }
        }



        protected override void Render(Device _device)
        {
            base.Render(_device);


            if (!this.DesignMode)
            {
                if (this.piston != null)
                {
                    this.DrawPiston(_device, this.piston, this.color);
                }

            }
        }
        private void DrawPiston(Device _device, Piston _piston, Color _color)
        {
            float _pistonRadius = (float)_piston.Diameter_mm / 2f;


            base.SetMaterial(_device, _color, false);


            _device.SetTransform(TransformState.World, Matrix.RotationYawPitchRoll(
                0f,
                Conversions.DegToRad(90f),
                0f));

            Mesh _mesh = Mesh.CreateCylinder(
                _device,
                _pistonRadius,
                _pistonRadius,
                (float)_piston.SkirtLength_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }

    }

    public class ConnectingRodSketch : EngineDesigner.Media.Graphics.DX.IPartSketches.IPartSketch
    {
        private ConnectingRod connectingRod = null;
        [DefaultValue(null)]
        public new ConnectingRod IPart
        {
            get
            {
                return connectingRod;
            }

            set
            {
                connectingRod = value;
                base.IPart = connectingRod;
            }
        }

        private Color color = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color @Color
        {
            get { return color; }

            set
            {
                color = value;
                this.Refresh();
            }
        }



        protected override void Render(Device _device)
        {
            base.Render(_device);


            if (!this.DesignMode)
            {
                if (this.connectingRod != null)
                {
                    this.DrawConnectingRod(_device, this.connectingRod, this.color);
                }

            }
        }
        private void DrawConnectingRod(Device _device, ConnectingRod _connectingRod, Color _color)
        {
            base.SetMaterial(_device, _color, false);


            _device.SetTransform(TransformState.World, Matrix.RotationYawPitchRoll(
                Conversions.DegToRad(0f),
                Conversions.DegToRad(90f),
                Conversions.DegToRad(0f)));

            Mesh _mesh = Mesh.CreateBox(
                _device,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_connectingRod.Length_mm);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }

    }

    public class CrankThrowSketch : EngineDesigner.Media.Graphics.DX.IPartSketches.IPartSketch
    {
        private CrankThrow crankThrow = null;
        [DefaultValue(null)]
        public new CrankThrow IPart
        {
            get
            {
                return crankThrow;
            }

            set
            {
                crankThrow = value;
                base.IPart = crankThrow;
            }
        }

        private Color color = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color @Color
        {
            get { return color; }

            set
            {
                color = value;
                this.Refresh();
            }
        }



        protected override void Render(Device _device)
        {
            base.Render(_device);


            if (!this.DesignMode)
            {
                if (this.crankThrow != null)
                {
                    this.DrawCrankThrow(_device, this.crankThrow, this.color);
                }

            }
        }
        private void DrawCrankThrow(Device _device, CrankThrow _crankThrow, Color _color)
        {
            base.SetMaterial(_device, _color, false);


            Mesh _mesh;


            #region "narišemo big pin"
            float _bigPinRadius_mm = (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2f;


            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    (float)_crankThrow.CrankRotationRadius_mm,
                    0f));

            _mesh = Mesh.CreateCylinder(
                _device,
                _bigPinRadius_mm,
                _bigPinRadius_mm,
                (float)_crankThrow.CrankPinWidth_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "narišemo big pin"

            #region "narišemo ročice in protiuteži"
            float _crankThrowCenterOfRotation_mm = (float)-((_crankThrow.CrankRotationRadius_mm / 2) - _crankThrow.CrankRotationRadius_mm);

            float _conRodBigEndPinWidthHalf_mm = (float)(_crankThrow.CrankPinWidth_mm / 2);

            float _crankThrowNear_mm = (float)(-_conRodBigEndPinWidthHalf_mm);
            float _crankThrowFar_mm = (float)(_conRodBigEndPinWidthHalf_mm);

            float _balancerThrowCenterOfRotation_mm = (float)-((_crankThrow.BalancerRotationRadius_mm / 2) - _crankThrow.BalancerRotationRadius_mm);

            float _balancerRadius_mm = (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2f;
            float _balancerRotation_Rad = (float)Conversions.DegToRad(_crankThrow.BalancerAngle_deg);
            float _balancerWidthHalf_mm = (float)(EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm / 2d);

            #region "ročica blizu"
            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    _crankThrowCenterOfRotation_mm,
                    _crankThrowNear_mm));

            _mesh = Mesh.CreateBox(
                _device,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_crankThrow.CrankRotationRadius_mm,
                0f);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "ročica blizu"

            if ((_crankThrow.BalancerMass_g > 0)
                && (_crankThrow.BalancerRotationRadius_mm > 0))
            {
                #region "ročica uteži blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        0f,
                        _balancerThrowCenterOfRotation_mm,
                        _crankThrowNear_mm)
                    * Matrix.RotationYawPitchRoll(
                        0f,
                        0f,
                        -_balancerRotation_Rad));

                _mesh = Mesh.CreateBox(
                    _device,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                    (float)_crankThrow.BalancerRotationRadius_mm,
                    0f);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "ročica uteži blizu"

                #region "utež blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        (float)(Math.Sin(_balancerRotation_Rad) * _crankThrow.BalancerRotationRadius_mm),
                        (float)(Math.Cos(_balancerRotation_Rad) * _crankThrow.BalancerRotationRadius_mm),
                        _crankThrowNear_mm - _balancerWidthHalf_mm));

                _mesh = Mesh.CreateCylinder(
                    _device,
                    _balancerRadius_mm,
                    _balancerRadius_mm,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm,
                    EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                    1);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "utež blizu"
            }


            #region "ročica daleč"
            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    _crankThrowCenterOfRotation_mm,
                    _crankThrowFar_mm));

            _mesh = Mesh.CreateBox(
                _device,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_crankThrow.CrankRotationRadius_mm,
                0f);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "ročica daleč"

            if ((_crankThrow.BalancerMass_g > 0)
                && (_crankThrow.BalancerRotationRadius_mm > 0))
            {
                #region "ročica uteži blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        0f,
                        _balancerThrowCenterOfRotation_mm,
                        _crankThrowFar_mm)
                    * Matrix.RotationYawPitchRoll(
                        0f,
                        0f,
                        -_balancerRotation_Rad));

                _mesh = Mesh.CreateBox(
                    _device,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                    (float)_crankThrow.BalancerRotationRadius_mm,
                    0f);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "ročica uteži blizu"

                #region "utež daleč"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        (float)(Math.Sin(_balancerRotation_Rad) * _crankThrow.BalancerRotationRadius_mm),
                        (float)(Math.Cos(_balancerRotation_Rad) * _crankThrow.BalancerRotationRadius_mm),
                        _crankThrowFar_mm + _balancerWidthHalf_mm));

                _mesh = Mesh.CreateCylinder(
                    _device,
                    _balancerRadius_mm,
                    _balancerRadius_mm,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm,
                    EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                    1);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "utež daleč"
            }
            #endregion "narišemo ročice in protiuteži"
        }

    }

    public class CylinderSketch : EngineDesigner.Media.Graphics.DX.IPartSketches.IPartSketch
    {
        private Cylinder cylinder = null;
        [DefaultValue(null)]
        public new Cylinder IPart
        {
            get
            {
                return cylinder;
            }

            set
            {
                cylinder = value;
                base.IPart = cylinder;
            }
        }

        private Color cylinderColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CylinderColor
        {
            get { return cylinderColor; }

            set
            {
                cylinderColor = value;
                this.Refresh();
            }
        }

        private Color pistonColor = Common.Defaults.RedColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color PistonColor
        {
            get { return pistonColor; }

            set
            {
                pistonColor = value;
                this.Refresh();
            }
        }

        private Color connectingRodColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color ConnectingRodColor
        {
            get { return connectingRodColor; }

            set
            {
                connectingRodColor = value;
                this.Refresh();
            }
        }

        private Color crankThrowColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CrankThrowColor
        {
            get { return crankThrowColor; }

            set
            {
                crankThrowColor = value;
                this.Refresh();
            }
        }

        private double crankThrowRotation_deg = 0; //trenutno nastavljen zavrtljaj ročične gredi
        [DefaultValue(0d)]
        public double CrankThrowRotation_deg
        {
            get { return crankThrowRotation_deg; }

            set
            {
                crankThrowRotation_deg = value;
                this.Refresh();
            }
        }



        protected override void Render(Device _device)
        {
            base.Render(_device);


            if (!this.DesignMode)
            {
                if (this.cylinder != null)
                {
                    this.DrawCylinder(_device, this.cylinder, this.cylinderColor);
                    this.DrawCrankThrow(_device, this.cylinder.CrankThrow, this.crankThrowColor, this.crankThrowRotation_deg);
                    this.DrawConnectingRod(_device, this.cylinder, this.connectingRodColor, this.crankThrowRotation_deg);
                    this.DrawPiston(_device, this.cylinder, this.pistonColor, this.crankThrowRotation_deg);
                }

            }
        }
        private void DrawCylinder(Device _device, Cylinder _cylinder, Color _color)
        {
            base.SetMaterial(_device, _color, false);


            float _cylinderHeight = (float)_cylinder.CylinderHeight_mm;
            float _cylinderRadius = (float)_cylinder.Bore_mm / 2f;

            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    0f,
                    -(float)(_cylinder.GetPhysicalHeightAbovePiston_mm(0) - _cylinderHeight / 2f))
                * Matrix.RotationYawPitchRoll(
                    0f,
                    Conversions.DegToRad(90f),
                    0f));


            Mesh _mesh = Mesh.CreateCylinder(
                _device,
                _cylinderRadius,
                _cylinderRadius,
                _cylinderHeight,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();

        }
        private void DrawCrankThrow(Device _device, CrankThrow _crankThrow, Color _color, double _crankThrowRotation_deg)
        {
            base.SetMaterial(_device, _color, false);


            Mesh _mesh;


            #region "narišemo big pin"
            float _bigPinRadius_mm = (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2f;
            float _crankThrowRotation_Rad = (float)Conversions.DegToRad(_crankThrowRotation_deg);


            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    (float)(Math.Sin(_crankThrowRotation_Rad) * _crankThrow.CrankRotationRadius_mm),
                    (float)(Math.Cos(_crankThrowRotation_Rad) * _crankThrow.CrankRotationRadius_mm),
                    0f));

            _mesh = Mesh.CreateCylinder(
                _device,
                _bigPinRadius_mm,
                _bigPinRadius_mm,
                (float)_crankThrow.CrankPinWidth_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "narišemo big pin"

            #region "narišemo ročice in protiuteži"
            float _crankThrowCenterOfRotation_mm = (float)-((_crankThrow.CrankRotationRadius_mm / 2) - _crankThrow.CrankRotationRadius_mm);

            float _conRodBigEndPinWidthHalf_mm = (float)(_crankThrow.CrankPinWidth_mm / 2);

            float _crankThrowNear_mm = (float)(-_conRodBigEndPinWidthHalf_mm);
            float _crankThrowFar_mm = (float)(_conRodBigEndPinWidthHalf_mm);

            float _balancerThrowCenterOfRotation_mm = (float)-((_crankThrow.BalancerRotationRadius_mm / 2) - _crankThrow.BalancerRotationRadius_mm);

            float _balancerRadius_mm = (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2f;
            float _balancerRotation_Rad = (float)Conversions.DegToRad(_crankThrowRotation_deg + _crankThrow.BalancerAngle_deg);
            float _balancerWidthHalf_mm = (float)(EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm / 2);


            #region "ročica blizu"
            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    _crankThrowCenterOfRotation_mm,
                    _crankThrowNear_mm)
                * Matrix.RotationYawPitchRoll(
                    0f,
                    0f,
                    -_crankThrowRotation_Rad));

            _mesh = Mesh.CreateBox(
                _device,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_crankThrow.CrankRotationRadius_mm,
                0f);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "ročica blizu"

            if ((_crankThrow.BalancerMass_g > 0)
                && (_crankThrow.BalancerRotationRadius_mm > 0))
            {
                #region "ročica uteži blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        0f,
                        _balancerThrowCenterOfRotation_mm,
                        _crankThrowNear_mm)
                    * Matrix.RotationYawPitchRoll(
                        0f,
                        0f,
                        -_balancerRotation_Rad));

                _mesh = Mesh.CreateBox(
                    _device,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                    (float)_crankThrow.BalancerRotationRadius_mm,
                    0f);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "ročica uteži blizu"

                #region "utež blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        (float)(Math.Sin(_balancerRotation_Rad) * _crankThrow.BalancerRotationRadius_mm),
                        (float)(Math.Cos(_balancerRotation_Rad) * _crankThrow.BalancerRotationRadius_mm),
                        _crankThrowNear_mm - _balancerWidthHalf_mm));

                _mesh = Mesh.CreateCylinder(
                    _device,
                    _balancerRadius_mm,
                    _balancerRadius_mm,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm,
                    EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                    1);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "utež blizu"
            }

            #region "ročica daleč"
            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    _crankThrowCenterOfRotation_mm,
                    _crankThrowFar_mm)
                * Matrix.RotationYawPitchRoll(
                    0f,
                    0f,
                    -_crankThrowRotation_Rad));

            _mesh = Mesh.CreateBox(
                _device,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_crankThrow.CrankRotationRadius_mm,
                0f);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "ročica daleč"

            if ((_crankThrow.BalancerMass_g > 0)
                && (_crankThrow.BalancerRotationRadius_mm > 0))
            {
                #region "ročica uteži blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        0f,
                        _balancerThrowCenterOfRotation_mm,
                        _crankThrowFar_mm)
                    * Matrix.RotationYawPitchRoll(
                        0f,
                        0f,
                        -_balancerRotation_Rad));

                _mesh = Mesh.CreateBox(
                    _device,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                    (float)_crankThrow.BalancerRotationRadius_mm,
                    0f);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "ročica uteži blizu"

                #region "utež daleč"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        (float)(Math.Sin(_balancerRotation_Rad) * _crankThrow.BalancerRotationRadius_mm),
                        (float)(Math.Cos(_balancerRotation_Rad) * _crankThrow.BalancerRotationRadius_mm),
                        _crankThrowFar_mm + _balancerWidthHalf_mm));

                _mesh = Mesh.CreateCylinder(
                    _device,
                    _balancerRadius_mm,
                    _balancerRadius_mm,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm,
                    EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                    1);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "utež daleč"
            }
            #endregion "narišemo ročice in protiuteži"
        }
        private void DrawConnectingRod(Device _device, Cylinder _cylinder, Color _color, double _crankThrowRotation_deg)
        {
            base.SetMaterial(_device, _color, false);


            //SEE: PositionedCylinder.SLDPRT
            float _conRodAngle_deg = -(float)_cylinder.GetConRodAngle_deg(_crankThrowRotation_deg);
            float _crankThrowRotation_Rad = (float)Conversions.DegToRad(_crankThrowRotation_deg);


            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    -(float)(_cylinder.ConnectingRod.Length_mm / 2),
                    0f,
                    0f)
                * Matrix.RotationYawPitchRoll(
                    0f,
                    0f,
                    -Conversions.DegToRad(90f + _conRodAngle_deg))
                * Matrix.Translation(
                    (float)(Math.Sin(_crankThrowRotation_Rad) * _cylinder.CrankThrow.CrankRotationRadius_mm),
                    (float)(Math.Cos(_crankThrowRotation_Rad) * _cylinder.CrankThrow.CrankRotationRadius_mm),
                    0f));

            Mesh _mesh = Mesh.CreateBox(
                _device,
                (float)_cylinder.ConnectingRod.Length_mm,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_cylinder.CrankThrow.CrankPinWidth_mm);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }
        private void DrawPiston(Device _device, Cylinder _cylinder, Color _color, double _crankThrowRotation_deg)
        {
            float _pistonRadius = (float)_cylinder.Piston.Diameter_mm / 2f;
            float _pistonTravelFromCrankCenter =
                (float)_cylinder.GetPistonTravelFromCrankCenter_mm(_crankThrowRotation_deg)
                - (float)(_cylinder.Piston.SkirtLength_mm / 2d)
                + (float)_cylinder.Piston.GudgeonPinDistanceFromTop_mm;


            base.SetMaterial(_device, _color, false);


            _device.SetTransform(TransformState.World,
                Matrix.RotationYawPitchRoll(
                    0f,
                    Conversions.DegToRad(90f),
                    0f)
                * Matrix.Translation(
                    0f,
                    _pistonTravelFromCrankCenter,
                    0f));

            Mesh _mesh = Mesh.CreateCylinder(
                _device,
                _pistonRadius,
                _pistonRadius,
                (float)_cylinder.Piston.SkirtLength_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }

    }

    public class PositionedCylinderSketch : EngineDesigner.Media.Graphics.DX.IPartSketches.IPartSketch
    {
        private PositionedCylinder positionedCylinder = null;
        [DefaultValue(null)]
        public new PositionedCylinder IPart
        {
            get
            {
                return positionedCylinder;
            }

            set
            {
                positionedCylinder = value;
                base.IPart = positionedCylinder;
            }
        }

        private Color cylinderColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CylinderColor
        {
            get { return cylinderColor; }

            set
            {
                cylinderColor = value;
                this.Refresh();
            }
        }

        private Color pistonColor = Common.Defaults.RedColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color PistonColor
        {
            get { return pistonColor; }

            set
            {
                pistonColor = value;
                this.Refresh();
            }
        }

        private Color connectingRodColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color ConnectingRodColor
        {
            get { return connectingRodColor; }

            set
            {
                connectingRodColor = value;
                this.Refresh();
            }
        }

        private Color crankThrowColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CrankThrowColor
        {
            get { return crankThrowColor; }

            set
            {
                crankThrowColor = value;
                this.Refresh();
            }
        }

        private double crankThrowRotation_deg = 0; //trenutno nastavljen zavrtljaj ročične gredi
        [DefaultValue(0d)]
        public double CrankThrowRotation_deg
        {
            get { return crankThrowRotation_deg; }

            set
            {
                crankThrowRotation_deg = value;
                this.Refresh();
            }
        }



        protected override void Render(Device _device)
        {
            base.Render(_device);


            if (!this.DesignMode)
            {
                if (this.positionedCylinder != null)
                {
                    this.DrawCylinder(_device, this.positionedCylinder, this.cylinderColor);
                    this.DrawCrankThrow(_device, this.positionedCylinder, this.crankThrowColor, this.crankThrowRotation_deg);
                    this.DrawConnectingRod(_device, this.positionedCylinder, this.connectingRodColor, this.crankThrowRotation_deg);
                    this.DrawPiston(_device, this.positionedCylinder, this.pistonColor, this.crankThrowRotation_deg);
                }

            }
        }
        private void DrawCylinder(Device _device, PositionedCylinder _positionedCylinder, Color _color)
        {
            base.SetMaterial(_device, _color, false);


            float _cylinderHeight = (float)_positionedCylinder.CylinderHeight_mm;
            float _cylinderRadius = (float)_positionedCylinder.Bore_mm / 2f;

            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    -(float)(_positionedCylinder.Offset_mm),
                    0,
                    -(float)(_positionedCylinder.GetPhysicalHeightAbovePiston_mm(0) - _cylinderHeight / 2f))
                * Matrix.RotationYawPitchRoll(
                    (float)Conversions.DegToRad(90f),
                    (float)Conversions.DegToRad((90f + _positionedCylinder.Tilt_deg)),
                    0f));

            Mesh _mesh = Mesh.CreateCylinder(
                _device,
                _cylinderRadius,
                _cylinderRadius,
                _cylinderHeight,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }
        private void DrawPiston(Device _device, PositionedCylinder _positionedCylinder, Color _color, double _crankshaftRotation_deg)
        {
            base.SetMaterial(_device, _color, false);


            double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
            float _pistonRadius = (float)_positionedCylinder.Bore_mm / 2f * EngineDesigner.Media.Properties.Settings.Default.PistonVsCylinderClearance;
            float _pistonTravelFromCrankCenter =
                (float)_positionedCylinder.GetPistonTravelFromCrankCenter_mm(_cylinderRelativeCrankThrowRotation_deg)
                - (float)_positionedCylinder.Piston.SkirtLength_mm / 2
                + (float)_positionedCylinder.Piston.GudgeonPinDistanceFromTop_mm;

            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    -(float)(_positionedCylinder.Offset_mm),
                    0,
                    -_pistonTravelFromCrankCenter)
                * Matrix.RotationYawPitchRoll(
                    (float)Conversions.DegToRad(90f),
                    (float)Conversions.DegToRad((90f + _positionedCylinder.Tilt_deg)),
                    0f));

            Mesh _mesh = Mesh.CreateCylinder(
                _device,
                _pistonRadius,
                _pistonRadius,
                (float)_positionedCylinder.Piston.SkirtLength_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }
        private void DrawConnectingRod(Device _device, PositionedCylinder _positionedCylinder, Color _color, double _crankshaftRotation_deg)
        {
            base.SetMaterial(_device, _color, false);


            double _crankThrowRotation_Rad = Conversions.DegToRad(_positionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg));
            //SEE: PositionedCylinder.SLDPRT
            float _conRodAngle_deg = (float)(_positionedCylinder.Tilt_deg - _positionedCylinder.GetConRodAngle_deg(_positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg)));

            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    -(float)(_positionedCylinder.ConnectingRod.Length_mm / 2),
                    0f,
                    0f)
                * Matrix.RotationYawPitchRoll(
                    0f,
                    0f,
                    -Conversions.DegToRad(90f + _conRodAngle_deg))
                * Matrix.Translation(
                    (float)(Math.Sin(_crankThrowRotation_Rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm),
                    (float)(Math.Cos(_crankThrowRotation_Rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm),
                    (float)(_positionedCylinder.Offset_mm)));

            Mesh _mesh = Mesh.CreateBox(
                _device,
                (float)_positionedCylinder.ConnectingRod.Length_mm,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_positionedCylinder.CrankThrow.CrankPinWidth_mm);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }
        private void DrawCrankThrow(Device _device, PositionedCylinder _positionedCylinder, Color _color, double _crankshaftRotation_deg)
        {
            base.SetMaterial(_device, _color, false);


            Mesh _mesh;


            #region "narišemo big pin"
            float _bigPinRadius_mm = (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2f;
            float _crankThrowRotation_Rad = (float)Conversions.DegToRad(_positionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg));


            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    (float)(Math.Sin(_crankThrowRotation_Rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm),
                    (float)(Math.Cos(_crankThrowRotation_Rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm),
                    (float)(_positionedCylinder.Offset_mm)));

            _mesh = Mesh.CreateCylinder(
                _device,
                _bigPinRadius_mm,
                _bigPinRadius_mm,
                (float)_positionedCylinder.CrankThrow.CrankPinWidth_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "narišemo big pin"

            #region "narišemo ročice in protiuteži"
            float _crankThrowCenterOfRotation_mm = (float)-((_positionedCylinder.CrankThrow.CrankRotationRadius_mm / 2) - _positionedCylinder.CrankThrow.CrankRotationRadius_mm);

            float _conRodBigEndPinWidthHalf_mm = (float)(_positionedCylinder.CrankThrow.CrankPinWidth_mm / 2);

            float _crankThrowNear_mm = (float)(_positionedCylinder.Offset_mm - _conRodBigEndPinWidthHalf_mm);
            float _crankThrowFar_mm = (float)(_positionedCylinder.Offset_mm + _conRodBigEndPinWidthHalf_mm);

            float _balancerThrowCenterOfRotation_mm = (float)-((_positionedCylinder.CrankThrow.BalancerRotationRadius_mm / 2) - _positionedCylinder.CrankThrow.BalancerRotationRadius_mm);

            float _balancerRadius_mm = (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2f;
            float _balancerRotation_Rad = (float)Conversions.DegToRad(_positionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg + _positionedCylinder.CrankThrow.BalancerAngle_deg));
            float _balancerWidthHalf_mm = (float)(EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm / 2);


            #region "ročica blizu"
            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    _crankThrowCenterOfRotation_mm,
                    _crankThrowNear_mm)
                * Matrix.RotationYawPitchRoll(
                    0f,
                    0f,
                    -_crankThrowRotation_Rad));

            _mesh = Mesh.CreateBox(
                _device,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_positionedCylinder.CrankThrow.CrankRotationRadius_mm,
                0f);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "ročica blizu"

            if ((_positionedCylinder.CrankThrow.BalancerMass_g > 0)
                && (_positionedCylinder.CrankThrow.BalancerRotationRadius_mm > 0))
            {
                #region "ročica uteži blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        0f,
                        _balancerThrowCenterOfRotation_mm,
                        _crankThrowNear_mm)
                    * Matrix.RotationYawPitchRoll(
                        0f,
                        0f,
                        -_balancerRotation_Rad));

                _mesh = Mesh.CreateBox(
                    _device,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                    (float)_positionedCylinder.CrankThrow.BalancerRotationRadius_mm,
                    0f);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "ročica uteži blizu"

                #region "utež blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        (float)(Math.Sin(_balancerRotation_Rad) * _positionedCylinder.CrankThrow.BalancerRotationRadius_mm),
                        (float)(Math.Cos(_balancerRotation_Rad) * _positionedCylinder.CrankThrow.BalancerRotationRadius_mm),
                        _crankThrowNear_mm - _balancerWidthHalf_mm));

                _mesh = Mesh.CreateCylinder(
                    _device,
                    _balancerRadius_mm,
                    _balancerRadius_mm,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm,
                    EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                    1);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "utež blizu"
            }

            #region "ročica daleč"
            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    _crankThrowCenterOfRotation_mm,
                    _crankThrowFar_mm)
                * Matrix.RotationYawPitchRoll(
                    0f,
                    0f,
                    -_crankThrowRotation_Rad));

            _mesh = Mesh.CreateBox(
                _device,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_positionedCylinder.CrankThrow.CrankRotationRadius_mm,
                0f);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "ročica daleč"

            if ((_positionedCylinder.CrankThrow.BalancerMass_g > 0)
                && (_positionedCylinder.CrankThrow.BalancerRotationRadius_mm > 0))
            {
                #region "ročica uteži blizu"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        0f,
                        _balancerThrowCenterOfRotation_mm,
                        _crankThrowFar_mm)
                    * Matrix.RotationYawPitchRoll(
                        0f,
                        0f,
                        -_balancerRotation_Rad));

                _mesh = Mesh.CreateBox(
                    _device,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                    (float)_positionedCylinder.CrankThrow.BalancerRotationRadius_mm,
                    0f);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "ročica uteži blizu"

                #region "utež daleč"
                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        (float)(Math.Sin(_balancerRotation_Rad) * _positionedCylinder.CrankThrow.BalancerRotationRadius_mm),
                        (float)(Math.Cos(_balancerRotation_Rad) * _positionedCylinder.CrankThrow.BalancerRotationRadius_mm),
                        _crankThrowFar_mm + _balancerWidthHalf_mm));

                _mesh = Mesh.CreateCylinder(
                    _device,
                    _balancerRadius_mm,
                    _balancerRadius_mm,
                    (float)EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm,
                    EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                    1);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
                #endregion "utež daleč"
            }
            #endregion "narišemo ročice in protiuteži"
        }

    }

    public class FlywheelSketch : EngineDesigner.Media.Graphics.DX.IPartSketches.IPartSketch
    {
        private Flywheel flywheel = null;
        [DefaultValue(null)]
        public new Flywheel IPart
        {
            get
            {
                return flywheel;
            }

            set
            {
                flywheel = value;
                base.IPart = flywheel;
            }
        }

        private Color color = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color @Color
        {
            get { return color; }

            set
            {
                color = value;
                this.Refresh();
            }
        }

        private double crankshaftRotation_deg = 0; //trenutno nastavljen zavrtljaj ročične gredi
        [DefaultValue(0d)]
        public double CrankshaftRotation_deg
        {
            get { return crankshaftRotation_deg; }

            set
            {
                crankshaftRotation_deg = value;
                this.Refresh();
            }
        }



        protected override void Render(Device _device)
        {
            base.Render(_device);


            if (!this.DesignMode)
            {
                if (this.flywheel != null)
                {
                    this.DrawFlywheel(_device, this.flywheel, this.color, this.crankshaftRotation_deg);
                }

            }
        }
        private void DrawFlywheel(Device _device, Flywheel _flywheel, Color _color, double _crankshaftRotation_deg)
        {
            base.SetMaterial(_device, _color, false);


            Mesh _mesh;


            #region "narišemo big pin"
            float _flywheelRadius_mm = (float)(_flywheel.Diameter_mm / 2d);
            float _flywheelWidth_mm = (float)(_flywheel.Diameter_mm / EngineDesigner.Machine.Properties.Settings.Default.FlywheelDiameterVsFlywheelWidth);


            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    0f,
                    (float)_crankshaftRotation_deg,
                    0f));

            _mesh = Mesh.CreateCylinder(
                _device,
                _flywheelRadius_mm,
                _flywheelRadius_mm,
                _flywheelWidth_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "narišemo big pin"
        }

    }


}
