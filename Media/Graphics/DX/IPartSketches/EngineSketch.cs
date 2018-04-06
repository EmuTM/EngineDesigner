using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

using SlimDX;
using SlimDX.Direct3D9;

using EngineDesigner.Machine;
using EngineDesigner.Common;
using EngineDesigner.Media;
using EngineDesigner.Media.Graphics.DX.Cameras;

namespace EngineDesigner.Media.Graphics.DX.IPartSketches
{
    public partial class EngineSketch : IPartSketch
    {
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

        private Color pistonColor = Common.Defaults.BlackColor;
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

        private Color crankshaftColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CrankshaftColor
        {
            get { return crankshaftColor; }

            set
            {
                crankshaftColor = value;
                this.Refresh();
            }
        }

        private Color flywheelColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color FlywheelColor
        {
            get { return flywheelColor; }

            set
            {
                flywheelColor = value;
                this.Refresh();
            }
        }

        private RenderMode cylinderRenderMode = RenderMode.WIRE_FRAME;
        [DefaultValue(RenderMode.WIRE_FRAME)]
        public RenderMode CylinderRenderMode
        {
            get { return cylinderRenderMode; }

            set
            {
                cylinderRenderMode = value;
                this.Refresh();
            }
        }

        private RenderMode pistonRenderMode = RenderMode.WIRE_FRAME;
        [DefaultValue(RenderMode.WIRE_FRAME)]
        public RenderMode PistonRenderMode
        {
            get { return pistonRenderMode; }

            set
            {
                pistonRenderMode = value;
                this.Refresh();
            }
        }

        private RenderMode connectingRodRenderMode = RenderMode.WIRE_FRAME;
        [DefaultValue(RenderMode.WIRE_FRAME)]
        public RenderMode ConnectingRodRenderMode
        {
            get { return connectingRodRenderMode; }

            set
            {
                connectingRodRenderMode = value;
                this.Refresh();
            }
        }

        private RenderMode crankshaftRenderMode = RenderMode.WIRE_FRAME;
        [DefaultValue(RenderMode.WIRE_FRAME)]
        public RenderMode CrankshaftRenderMode
        {
            get { return crankshaftRenderMode; }

            set
            {
                crankshaftRenderMode = value;
                this.Refresh();
            }
        }

        private RenderMode combustionChamberRenderMode = RenderMode.SOLID;
        [DefaultValue(RenderMode.WIRE_FRAME)]
        public RenderMode CombustionChamberRenderMode
        {
            get { return combustionChamberRenderMode; }

            set
            {
                combustionChamberRenderMode = value;
                this.Refresh();
            }
        }

        private Color intakeColor = Common.Defaults.IntakeColor;
        [DefaultValue(typeof(Color), Common.Defaults.IntakeColorString)]
        public Color IntakeColor
        {
            get { return intakeColor; }
            set { intakeColor = value; }
        }

        private Color compressionColor = Common.Defaults.CompressionColor;
        [DefaultValue(typeof(Color), Common.Defaults.CompressionColorString)]
        public Color CompressionColor
        {
            get { return compressionColor; }
            set { compressionColor = value; }
        }

        private Color combustionColor = Common.Defaults.CombustionColor;
        [DefaultValue(typeof(Color), Common.Defaults.CombustionColorString)]
        public Color CombustionColor
        {
            get { return combustionColor; }
            set { combustionColor = value; }
        }

        private Color exhaustColor = Common.Defaults.ExhaustColor;
        [DefaultValue(typeof(Color), Common.Defaults.ExhaustColorString)]
        public Color ExhaustColor
        {
            get { return exhaustColor; }
            set { exhaustColor = value; }
        }

        private bool showSpark = true;
        [DefaultValue(true)]
        public bool ShowSpark
        {
            get { return showSpark; }
            set { showSpark = value; }
        }

        private Color sparkColor = Common.Defaults.RedColor;
        [DefaultValue(typeof(Color), Common.Defaults.RedColorString)]
        public Color SparkColor
        {
            get { return sparkColor; }
            set { sparkColor = value; }
        }

        private double sparkAdvance_degs = 20d;
        [DefaultValue(20d)]
        public double SparkAdvance_degs
        {
            get { return sparkAdvance_degs; }
            set { sparkAdvance_degs = value; }
        }

        private double sparkDuration_degs = 40d;
        [DefaultValue(40d)]
        public double SparkDuration_degs
        {
            get { return sparkDuration_degs; }
            set { sparkDuration_degs = value; }
        }

        private bool showCylinderPositions = true;
        [DefaultValue(true)]
        public bool ShowCylinderPositions
        {
            get { return showCylinderPositions; }
            set { showCylinderPositions = value; }
        }

        private Color cylinderPositionColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CylinderPositionColor
        {
            get { return cylinderPositionColor; }
            set { cylinderPositionColor = value; }
        }

        private System.Drawing.Font cylinderPositionFont = Common.Defaults.DefaultFont;
        [DefaultValue(typeof(System.Drawing.Font), Common.Defaults.DefaultFontString)]
        public System.Drawing.Font CylinderPositionFont
        {
            get { return cylinderPositionFont; }
            set { cylinderPositionFont = value; }
        }

        private Color selectedIPartColor = Common.Defaults.SelectedIPartColor;
        [DefaultValue(typeof(Color), Common.Defaults.SelectedIPartColorString)]
        public Color SelectedIPartColor
        {
            get { return selectedIPartColor; }
            set { selectedIPartColor = value; }
        }

        [DefaultValue(true)]
        public bool Animated
        {
            get { return this.rpmTimer1.Enabled; }
            set { this.rpmTimer1.Enabled = value; }
        }

        private Engine engine = null;
        [DefaultValue(null)]
        public new Engine IPart
        {
            get
            {
                return engine;
            }

            set
            {
                engine = value;
                base.IPart = engine;
                this.rpmTimer1.Engine = engine;
            }
        }

        private IPart[] selectedParts = null;
        [DefaultValue(null)]
        public IPart[] SelectedParts
        {
            get { return selectedParts; }

            set
            {
                selectedParts = value;
                this.Refresh();
            }
        }



        private double crankRotationInAnimation_deg = 0;



        public EngineSketch()
        {
            InitializeComponent();

            this.Disposed
                += new EventHandler(EngineSketch_Disposed);
        }
        private void EngineSketch_Disposed(object sender, EventArgs e)
        {
            rpmTimer1.Dispose();
        }



        protected override void Render(Device _device)
        {
            base.Render(_device);


            if (!this.DesignMode)
            {
                if (this.engine != null)
                {
                    if (this.rpmTimer1.Enabled)
                    {
                        DrawEngine(_device, this.engine, this.crankRotationInAnimation_deg);
                    }
                    else
                    {
                        DrawEngine(_device, this.engine, this.crankshaftRotation_deg);
                    }
                }
            }
        }
        private void DrawEngine(Device _device, Engine _engine, double _crankshaftRotation_deg)
        {
            foreach (PositionedCylinder _positionedCylinder in _engine.PositionedCylinders)
            {
                #region "Cylinder"
                Color _cylinderColor = this.CylinderColor;
                if (this.selectedParts != null)
                {
                    if (this.selectedParts.Contains(_positionedCylinder))
                    {
                        _cylinderColor = this.selectedIPartColor;
                    }
                }

                switch (this.CylinderRenderMode)
                {
                    case RenderMode.WIRE_FRAME:
                        DrawCylinder(_device, _positionedCylinder, _cylinderColor, false);
                        break;

                    case RenderMode.SOLID:
                        DrawCylinder(_device, _positionedCylinder, _cylinderColor, true);
                        break;

                    case RenderMode.COMBINED:
                        DrawCylinder(_device, _positionedCylinder, _cylinderColor, true);
                        DrawCylinder(_device, _positionedCylinder, _cylinderColor, false);
                        break;
                }
                #endregion "Cylinder"

                #region "Piston"
                Color _pistonColor = this.pistonColor;
                if (this.selectedParts != null)
                {
                    if ((this.selectedParts.Contains(_positionedCylinder))
                        || (this.selectedParts.Contains(_positionedCylinder.Piston)))
                    {
                        _pistonColor = this.selectedIPartColor;
                    }
                }

                switch (this.pistonRenderMode)
                {
                    case RenderMode.WIRE_FRAME:
                        DrawPiston(_device, _positionedCylinder, _pistonColor, false, _crankshaftRotation_deg);
                        break;

                    case RenderMode.SOLID:
                        DrawPiston(_device, _positionedCylinder, _pistonColor, true, _crankshaftRotation_deg);
                        break;

                    case RenderMode.COMBINED:
                        DrawPiston(_device, _positionedCylinder, _pistonColor, true, _crankshaftRotation_deg);
                        DrawPiston(_device, _positionedCylinder, _pistonColor, false, _crankshaftRotation_deg);
                        break;
                }
                #endregion "Piston"

                #region "ConnectingRod"
                Color _connectingRodColor = this.connectingRodColor;
                if (this.selectedParts != null)
                {
                    if ((this.selectedParts.Contains(_positionedCylinder))
                       || (this.selectedParts.Contains(_positionedCylinder.ConnectingRod)))
                    {
                        _connectingRodColor = this.selectedIPartColor;
                    }
                }

                switch (this.connectingRodRenderMode)
                {
                    case RenderMode.WIRE_FRAME:
                        DrawConnectingRod(_device, _positionedCylinder, _connectingRodColor, false, _crankshaftRotation_deg);
                        break;

                    case RenderMode.SOLID:
                        DrawConnectingRod(_device, _positionedCylinder, _connectingRodColor, true, _crankshaftRotation_deg);
                        break;

                    case RenderMode.COMBINED:
                        DrawConnectingRod(_device, _positionedCylinder, _connectingRodColor, true, _crankshaftRotation_deg);
                        DrawConnectingRod(_device, _positionedCylinder, _connectingRodColor, false, _crankshaftRotation_deg);
                        break;
                }
                #endregion "ConnectingRod"

                Stroke _stroke = _positionedCylinder.GetStroke(_crankshaftRotation_deg);

                #region "Spark"
                if (this.showSpark)
                {
                    bool _showSpark = false;

                    if ((_stroke.Equals(Stroke.Combustion))
                        || (_stroke.Equals(Stroke.CombustionExhaust)))
                    {
                        //izračunamo koliko takta je že preteklo
                        double _elapsedStroke_deg = _positionedCylinder.GetElapsedStroke(_stroke, _crankshaftRotation_deg);
                        if (_elapsedStroke_deg < (this.sparkDuration_degs - this.sparkAdvance_degs))
                        {
                            _showSpark = true;
                        }
                    }
                    else if ((_stroke.Equals(Stroke.Compression))
                        || (_stroke.Equals(Stroke.WashCompression)))
                    {
                        //izračunamo koliko takta je že preteklo
                        double _elapsedStroke_deg = _positionedCylinder.GetElapsedStroke(_stroke, _crankshaftRotation_deg);
                        if (_elapsedStroke_deg > (Stroke.StrokeDuration_deg - this.sparkAdvance_degs))
                        {
                            _showSpark = true;
                        }
                    }

                    if (_showSpark)
                    {
                        this.DrawSpark(_device, _positionedCylinder, this.sparkColor);
                    }
                }
                #endregion "Spark"

                #region "CombustionChamber"
                if (!_stroke.Equals(Stroke.NaN))
                {
                    Color _strokeColor = Color.Empty;

                    if ((this.selectedParts != null)
                        && (this.selectedParts.Contains(_positionedCylinder)))
                    {
                        _strokeColor = this.selectedIPartColor;
                    }
                    else
                    {
                        #region "izračunamo pravo barvo"
                        //izračunamo koliko takta je že preteklo
                        double _elapsedStroke_deg = _positionedCylinder.GetElapsedStroke(_stroke, _crankshaftRotation_deg);
                        double _elapsedStroke_percentage = Math.Round(_elapsedStroke_deg / Stroke.StrokeDuration_deg, 2);

                        //dobimo barve
                        if (_stroke.Equals(Stroke.Intake))
                        {
                            _strokeColor = Common.Utility.GetInterpolatedColor(this.exhaustColor, this.intakeColor, _elapsedStroke_percentage);
                        }
                        else if (_stroke.Equals(Stroke.Compression))
                        {
                            _strokeColor = Common.Utility.GetInterpolatedColor(this.intakeColor, this.compressionColor, true, _elapsedStroke_percentage);
                        }
                        else if (_stroke.Equals(Stroke.Combustion))
                        {
                            _strokeColor = Common.Utility.GetInterpolatedColor(this.combustionColor, true, this.exhaustColor, _elapsedStroke_percentage);
                        }
                        else if (_stroke.Equals(Stroke.Exhaust))
                        {
                            _strokeColor = this.exhaustColor;
                        }
                        else if (_stroke.Equals(Stroke.WashCompression))
                        {
                            _strokeColor = Common.Utility.GetInterpolatedColor(this.intakeColor, this.compressionColor, _elapsedStroke_percentage);
                        }
                        else if (_stroke.Equals(Stroke.CombustionExhaust))
                        {
                            _strokeColor = Common.Utility.GetInterpolatedColor(this.combustionColor, this.intakeColor, _elapsedStroke_percentage);
                        }
                        else if (_stroke.Equals(Stroke.NaN))
                        {
                            //takti niso določeni
                        }
                        else
                        {
                            throw new Exception();
                        }
                        #endregion "izračunamo pravo barvo"
                    }

                    switch (this.combustionChamberRenderMode)
                    {
                        case RenderMode.WIRE_FRAME:
                            DrawCombustionChamber(_device, _positionedCylinder, _strokeColor, false, _crankshaftRotation_deg);
                            break;

                        case RenderMode.SOLID:
                            DrawCombustionChamber(_device, _positionedCylinder, _strokeColor, true, _crankshaftRotation_deg);
                            break;

                        case RenderMode.COMBINED:
                            DrawCombustionChamber(_device, _positionedCylinder, _strokeColor, true, _crankshaftRotation_deg);
                            DrawCombustionChamber(_device, _positionedCylinder, _strokeColor, false, _crankshaftRotation_deg);
                            break;
                    }
                }
                #endregion "CombustionChamber"

                #region "CylinderPosition"
                if (this.showCylinderPositions)
                {
                    this.DrawCylinderPosition(_device, _positionedCylinder, this.CylinderPositionColor, this.CylinderPositionFont);
                }
                #endregion "CylinderPosition"
            }

            #region "Crankshaft"
            switch (crankshaftRenderMode)
            {
                case RenderMode.WIRE_FRAME:
                    this.DrawCrankshaft(_device, _engine, this.crankshaftColor, this.selectedParts, this.selectedIPartColor, false, _crankshaftRotation_deg);
                    break;

                case RenderMode.SOLID:
                    this.DrawCrankshaft(_device, _engine, this.crankshaftColor, this.selectedParts, this.selectedIPartColor, true, _crankshaftRotation_deg);
                    break;

                case RenderMode.COMBINED:
                    this.DrawCrankshaft(_device, _engine, this.crankshaftColor, this.selectedParts, this.selectedIPartColor, true, _crankshaftRotation_deg);
                    this.DrawCrankshaft(_device, _engine, this.crankshaftColor, this.selectedParts, this.selectedIPartColor, false, _crankshaftRotation_deg);
                    break;
            }
            #endregion "Crankshaft"

            if (_engine.Flywheel.Mass_g > 0)
            {
                #region "Flywheel"
                Color _flywheelColor = this.flywheelColor;
                if (this.selectedParts != null)
                {
                    if (this.selectedParts.Contains(_engine.Flywheel))
                    {
                        _flywheelColor = this.selectedIPartColor;
                    }
                }

                switch (connectingRodRenderMode)
                {
                    case RenderMode.WIRE_FRAME:
                        this.DrawFlywheel(_device, _engine, _flywheelColor, false, _crankshaftRotation_deg);
                        break;

                    case RenderMode.SOLID:
                        this.DrawFlywheel(_device, _engine, _flywheelColor, true, _crankshaftRotation_deg);
                        break;

                    case RenderMode.COMBINED:
                        this.DrawFlywheel(_device, _engine, _flywheelColor, true, _crankshaftRotation_deg);
                        this.DrawFlywheel(_device, _engine, _flywheelColor, false, _crankshaftRotation_deg);
                        break;
                }
                #endregion "Flywheel"
            }
        }
        private void DrawSpark(Device _device, PositionedCylinder _positionedCylinder, Color _color)
        {
            float _sparkRadius = (float)(_positionedCylinder.Bore_mm * EngineDesigner.Media.Properties.Settings.Default.SparkVsCylinderFactor);
            float _sparkPosition = (float)(_positionedCylinder.GetPhysicalHeightAbovePiston_mm(0) + _sparkRadius
                + EngineDesigner.Media.Properties.Settings.Default.SparkVsCylinderClearance);

            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    -(float)(_positionedCylinder.Offset_mm),
                    0,
                    -_sparkPosition)
                * Matrix.RotationYawPitchRoll(
                    (float)Conversions.DegToRad(90f),
                    (float)Conversions.DegToRad((90f + _positionedCylinder.Tilt_deg)),
                    0f));

            base.SetMaterial(_device, _color, true);

            Mesh _mesh = Mesh.CreateSphere(
                _device,
                _sparkRadius,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }
        private void DrawCylinderPosition(Device _device, PositionedCylinder _positionedCylinder, Color _color, System.Drawing.Font _font)
        {
            float _position = (float)(_positionedCylinder.GetPhysicalHeightAbovePiston_mm(0)
                + EngineDesigner.Media.Properties.Settings.Default.CylinderPositionTextVsCylinderClearance);

            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    -(float)(_positionedCylinder.Offset_mm),
                    0,
                    -_position)
                * Matrix.RotationYawPitchRoll(
                    (float)Conversions.DegToRad(90f),
                    (float)Conversions.DegToRad((90f + _positionedCylinder.Tilt_deg)),
                    0f));


            Vector3 _vector3 = Vector3.Project(
                Vector3.Zero,
                _device.Viewport.X,
                _device.Viewport.Y,
                _device.Viewport.Width,
                _device.Viewport.Height,
                _device.Viewport.MinZ,
                _device.Viewport.MaxZ,
                _device.GetTransform(TransformState.World) * _device.GetTransform(TransformState.View) * _device.GetTransform(TransformState.Projection));
            SlimDX.Direct3D9.Font _text = new SlimDX.Direct3D9.Font(_device, _font);
            _text.DrawString(null, _positionedCylinder.Position.ToString(), (int)_vector3.X, (int)_vector3.Y, _color.ToArgb());
            _text.Dispose();
        }
        private void DrawCylinder(Device _device, PositionedCylinder _positionedCylinder, Color _color, bool _solid)
        {
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

            base.SetMaterial(_device, _color, _solid);

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
        private void DrawPiston(Device _device, PositionedCylinder _positionedCylinder, Color _color, bool _solid, double _crankshaftRotation_deg)
        {
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

            base.SetMaterial(_device, _color, _solid);

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
        private void DrawCombustionChamber(Device _device, PositionedCylinder _positionedCylinder, Color _color, bool _solid, double _crankshaftRotation_deg)
        {
            double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
            float _combustionChamberRadius = (float)_positionedCylinder.Bore_mm / 2f * EngineDesigner.Media.Properties.Settings.Default.PistonVsCylinderClearance;
            double _combustionChamberHeight_mm = _positionedCylinder.GetCombustionChamberHeight_mm(_cylinderRelativeCrankThrowRotation_deg);
            double _combustionChamberPosition = _positionedCylinder.GetPhysicalHeightAbovePiston_mm(_cylinderRelativeCrankThrowRotation_deg) + (_combustionChamberHeight_mm / 2d);


            _device.SetTransform(TransformState.World,
                Matrix.Translation(
                    -(float)(_positionedCylinder.Offset_mm),
                    0,
                    -(float)_combustionChamberPosition)
                * Matrix.RotationYawPitchRoll(
                    (float)Conversions.DegToRad(90f),
                    (float)Conversions.DegToRad((90f + _positionedCylinder.Tilt_deg)),
                    0f));

            base.SetMaterial(_device, _color, _solid);

            Mesh _mesh = Mesh.CreateCylinder(
                _device,
                _combustionChamberRadius,
                _combustionChamberRadius,
                (float)_combustionChamberHeight_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }
        private void DrawConnectingRod(Device _device, PositionedCylinder _positionedCylinder, Color _color, bool _solid, double _crankshaftRotation_deg)
        {
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

            base.SetMaterial(_device, _color, _solid);

            Mesh _mesh = Mesh.CreateBox(
                _device,
                (float)_positionedCylinder.ConnectingRod.Length_mm,
                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                (float)_positionedCylinder.CrankThrow.CrankPinWidth_mm);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
        }
        private void DrawCrankshaft(Device _device, Engine _engine, Color _color, IPart[] _selectedIParts, Color _selectedColor, bool _solid, double _crankshaftRotation_deg)
        {
            Mesh _mesh;


            #region "narišemo ročice in morebitne povezave med njimi"
            #region "non-mated cylinders"
            PositionedCylinder[] _allNonMatedPositionedCylinders = _engine.GetNonMatedPositionedCylinders();
            foreach (PositionedCylinder _positionedCylinder in _allNonMatedPositionedCylinders)
            {
                Color _crankThrowColor = _color;
                if (_selectedIParts != null)
                {
                    if ((_selectedIParts.Contains(_positionedCylinder))
                       || (_selectedIParts.Contains(_positionedCylinder.CrankThrow)))
                    {
                        _crankThrowColor = _selectedColor;
                    }
                }

                this.DrawCrankThrow(_device, _positionedCylinder, _crankThrowColor, _solid, _crankshaftRotation_deg, true, true);
            }
            #endregion "non-mated cylinders"

            #region "mated cylinders"
            PositionedCylinder[][] _allMatedPositionedCylinders = _engine.GetMatedPositionedCylinders();
            foreach (PositionedCylinder[] _matedPositionedCylinders in _allMatedPositionedCylinders)
            {
                PositionedCylinder _firstMatedPositionedCylinder = _engine.GetFirstPositionedCylinder(_matedPositionedCylinders);
                PositionedCylinder _lastMatedPositionedCylinder = _engine.GetLastPositionedCylinder(_matedPositionedCylinders);


                Color _crankThrowColor = _color;
                if (_selectedIParts != null)
                {
                    foreach (PositionedCylinder _currentMatedPositionedCylinder in _matedPositionedCylinders)
                    {
                        if ((_selectedIParts.Contains(_currentMatedPositionedCylinder))
                           || (_selectedIParts.Contains(_currentMatedPositionedCylinder.CrankThrow)))
                        {
                            _crankThrowColor = _selectedColor;
                        }
                    }
                }


                foreach (PositionedCylinder _currentMatedPositionedCylinder in _matedPositionedCylinders)
                {
                    if (_currentMatedPositionedCylinder == _firstMatedPositionedCylinder)
                    {
                        this.DrawCrankThrow(_device, _currentMatedPositionedCylinder, _crankThrowColor, _solid, _crankshaftRotation_deg, true, false);
                    }
                    else if (_currentMatedPositionedCylinder == _lastMatedPositionedCylinder)
                    {
                        this.DrawCrankThrow(_device, _currentMatedPositionedCylinder, _crankThrowColor, _solid, _crankshaftRotation_deg, false, true);
                    }


                    PositionedCylinder _previousMatedPositionedCylinder = _engine.GetPreviousPositionedCylinder(_matedPositionedCylinders, _currentMatedPositionedCylinder);
                    if (_previousMatedPositionedCylinder != null)
                    {
                        double _currentCrankThrowRotation_deg = _currentMatedPositionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg);
                        double _previousCrankThrowRotation_deg = _previousMatedPositionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg);
                        double _currentCrankThrowRotation_Rad = EngineDesigner.Common.Conversions.DegToRad(_currentCrankThrowRotation_deg);


                        //če sta kota ročic enaka, ne rabimo nič risat
                        if ((_currentCrankThrowRotation_deg != _previousCrankThrowRotation_deg)
                            || (_currentMatedPositionedCylinder.CrankThrow.CrankRotationRadius_mm != _previousMatedPositionedCylinder.CrankThrow.CrankRotationRadius_mm))
                        {
                            #region "povezava med matanimi cilindri"
                            //SEE: BindedCrankThrows.SLDPRT (_a, _b, _c = stranice trikotnika, _A, _B = notranji koti trikotnika)

                            double _c = _previousMatedPositionedCylinder.CrankThrow.CrankRotationRadius_mm;
                            double _b = _currentMatedPositionedCylinder.CrankThrow.CrankRotationRadius_mm;

                            double _A = _previousCrankThrowRotation_deg - _currentCrankThrowRotation_deg;

                            double _a = Math.Sqrt(
                                Math.Pow(_b, 2)
                                + Math.Pow(_c, 2)
                                - 2 * _b * _c * Math.Cos(Conversions.DegToRad(_A)));

                            double _C = Conversions.RadToDeg(Math.Asin(_c * Math.Sin(Conversions.DegToRad(_A)) / _a));


                            _device.SetTransform(TransformState.World,
                                Matrix.Translation(
                                    (float)(_a / 2),
                                    0f,
                                    (float)(_currentMatedPositionedCylinder.Offset_mm - (_currentMatedPositionedCylinder.CrankThrow.CrankPinWidth_mm / 2)))

                                * Matrix.RotationYawPitchRoll(
                                    0f,
                                    0f,
                                    (float)-Conversions.DegToRad(_currentCrankThrowRotation_deg - _C + 90f))

                                * Matrix.Translation(
                                    (float)(Math.Sin(_currentCrankThrowRotation_Rad) * _currentMatedPositionedCylinder.CrankThrow.CrankRotationRadius_mm),
                                    (float)(Math.Cos(_currentCrankThrowRotation_Rad) * _currentMatedPositionedCylinder.CrankThrow.CrankRotationRadius_mm),
                                    0f));

                            _mesh = Mesh.CreateBox(
                                _device,
                                (float)_a,
                                (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm,
                                0f);
                            _mesh.DrawSubset(0);
                            _mesh.Dispose();
                            #endregion "povezava med matanimi cilindri"
                        }
                    }
                }
            }
            #endregion "mated cylinders"
            #endregion "narišemo ročice in morebitne povezave med njimi"

            #region "narišemo gred"
            base.SetMaterial(_device, _color, _solid);


            PositionedCylinder _firstPositionedCylinder = _engine.GetFirstPositionedCylinder();
            PositionedCylinder _lastPositionedCylinder = _engine.GetLastPositionedCylinder();

            float _crankshaftRadius_mm = (float)EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2;



            #region "podaljšek gredi blizu"
            if (_firstPositionedCylinder != null)
            {
                float _shaftExtensionLength = (float)((_firstPositionedCylinder.Bore_mm / 2d) - (_firstPositionedCylinder.CrankThrow.CrankPinWidth_mm / 2d));


                _device.SetTransform(TransformState.World,
                    Matrix.RotationYawPitchRoll(
                        0f,
                        0f,
                        -(float)Conversions.DegToRad(_crankshaftRotation_deg))
                    * Matrix.Translation(
                        0f,
                        0f,
                        (float)(_firstPositionedCylinder.Offset_mm - (_shaftExtensionLength / 2d) - (_firstPositionedCylinder.CrankThrow.CrankPinWidth_mm / 2d))));

                _mesh = Mesh.CreateCylinder(
                    _device,
                    _crankshaftRadius_mm,
                    _crankshaftRadius_mm,
                    _shaftExtensionLength,
                    EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                    1);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
            }
            #endregion "podaljšek gredi blizu"

            #region "podaljšek gredi daleč"
            if (_lastPositionedCylinder != null)
            {
                float _shaftExtensionLength = (float)((_lastPositionedCylinder.Bore_mm / 2d) - (_lastPositionedCylinder.CrankThrow.CrankPinWidth_mm / 2d));


                _device.SetTransform(TransformState.World,
                    Matrix.Translation(
                        0f,
                        0f,
                        (float)(_lastPositionedCylinder.Offset_mm + _lastPositionedCylinder.CrankThrow.CrankPinWidth_mm + (_shaftExtensionLength / 2d) + -(_lastPositionedCylinder.CrankThrow.CrankPinWidth_mm / 2d)))
                    * Matrix.RotationYawPitchRoll(
                        0f,
                        0f,
                        -(float)Conversions.DegToRad(_crankshaftRotation_deg)));

                _mesh = Mesh.CreateCylinder(
                    _device,
                    _crankshaftRadius_mm,
                    _crankshaftRadius_mm,
                    _shaftExtensionLength,
                    EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                    1);
                _mesh.DrawSubset(0);
                _mesh.Dispose();
            }
            #endregion "podaljšek gredi daleč"

            #region "med ročicami"
            PositionedCylinder _currentPositionedCylinder = _firstPositionedCylinder;
            PositionedCylinder _nextPositionedCylinder = _engine.GetNextPositionedCylinder(_currentPositionedCylinder);

            //če je next null, pomeni, da je current zadnji
            while (_nextPositionedCylinder != null)
            {
                float _currentBigEndPinWidthHalf_mm = (float)(_currentPositionedCylinder.CrankThrow.CrankPinWidth_mm / 2);
                float _nextBigEndPinWidthHalf_mm = (float)(_nextPositionedCylinder.CrankThrow.CrankPinWidth_mm / 2);

                float _shaftFragmentStart = (float)(
                    _currentPositionedCylinder.Offset_mm
                    + _currentBigEndPinWidthHalf_mm);
                float _shaftFragmentEnd = (float)(
                    _nextPositionedCylinder.Offset_mm
                    - _nextBigEndPinWidthHalf_mm);
                float _shaftFragmentLength_mm = _shaftFragmentEnd - _shaftFragmentStart;
                float _shaftFragmentPosition = (float)(
                    _currentPositionedCylinder.Offset_mm
                    + _currentBigEndPinWidthHalf_mm
                    + (_shaftFragmentLength_mm / 2));

                //če je dolžina 0 (ali manj), potem je mated in se ne riše
                if (_shaftFragmentLength_mm > 0f)
                {
                    _device.SetTransform(TransformState.World,
                        Matrix.Translation(
                            0f,
                            0f,
                            _shaftFragmentPosition)
                        * Matrix.RotationYawPitchRoll(
                            0f,
                            0f,
                            -(float)Conversions.DegToRad(_crankshaftRotation_deg)));

                    _mesh = Mesh.CreateCylinder(
                        _device,
                        _crankshaftRadius_mm,
                        _crankshaftRadius_mm,
                        _shaftFragmentLength_mm,
                        EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                        1);
                    _mesh.DrawSubset(0);
                    _mesh.Dispose();
                }

                _currentPositionedCylinder = _nextPositionedCylinder;
                _nextPositionedCylinder = _engine.GetNextPositionedCylinder(_currentPositionedCylinder);
            }
            #endregion "med ročicami"
            #endregion "narišemo gred"
        }
        private void DrawCrankThrow(Device _device, PositionedCylinder _positionedCylinder, Color _color, bool _solid, double _crankshaftRotation_deg, bool _nearHalf, bool _farHalf)
        {
            base.SetMaterial(_device, _color, _solid);


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


            if (_nearHalf)
            {
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
            }

            if (_farHalf)
            {
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
                    #region "ročica uteži daleč"
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
                    #endregion "ročica uteži daleč"

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
            }
            #endregion "narišemo ročice in protiuteži"
        }
        private void DrawFlywheel(Device _device, Engine _engine, Color _color, bool _solid, double _crankshaftRotation_deg)
        {
            base.SetMaterial(_device, _color, _solid);


            Mesh _mesh;


            #region "narišemo flywheel"
            float _crankshaftRotation_Rad = (float)Conversions.DegToRad(_crankshaftRotation_deg);

            float _flywheelRadius_mm = (float)(_engine.Flywheel.Diameter_mm / 2d);
            float _flywheelWidth_mm = (float)(_engine.Flywheel.Diameter_mm / EngineDesigner.Machine.Properties.Settings.Default.FlywheelDiameterVsFlywheelWidth);


            PositionedCylinder _firstPositionedCylinder = _engine.GetFirstPositionedCylinder();
            float _flywheelPosition = (float)((_firstPositionedCylinder.Offset_mm - (_firstPositionedCylinder.Bore_mm / 2d)) - (_flywheelWidth_mm / 2f));

            _device.SetTransform(TransformState.World,
                Matrix.RotationYawPitchRoll(
                    0f,
                    0f,
                    _crankshaftRotation_Rad)
                * Matrix.Translation(
                    0f,
                    0f,
                    _flywheelPosition));

            _mesh = Mesh.CreateCylinder(
                _device,
                _flywheelRadius_mm,
                _flywheelRadius_mm,
                _flywheelWidth_mm,
                EngineDesigner.Media.Properties.Settings.Default.SlicesInCylinderMesh,
                1);
            _mesh.DrawSubset(0);
            _mesh.Dispose();
            #endregion "narišemo flywheel"
        }



        private void rpmTimer1_CrankshaftAngleChanged(object sender, RPMTimerEventArgs e)
        {
            this.crankRotationInAnimation_deg = e.NewAngle_deg;
            this.Refresh();
        }

    }
}
