using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using EngineDesigner.Common.Definitions;
using EngineDesigner.Environment.Controls.Charting;
using EngineDesigner.Machine;
using EngineDesigner.Machine.Definitions;


namespace EngineDesigner.Controls.Charting
{
    internal class CrankshaftDiagram : AngleChart
    {
        private Engine engine = null;
        [DefaultValue(null)]
        public Engine @Engine
        {
            get { return engine; }

            set
            {
                engine = value;
                DrawCrankshaftDiagram();
            }
        }


        private double crankshaftRotation_deg = 0d;
        [DefaultValue(0d)]
        public double CrankshaftRotation_deg
        {
            get { return crankshaftRotation_deg; }

            set
            {
                crankshaftRotation_deg = value;
                DrawCrankshaftDiagram();
            }
        }


        public int lineThickness = 1;
        [DefaultValue(1)]
        public int LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
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

        private Color washCompressionColor = Common.Defaults.WashCompressionColor;
        [DefaultValue(typeof(Color), Common.Defaults.WashCompressionColorString)]
        public Color WashCompressionColor
        {
            get { return washCompressionColor; }
            set { washCompressionColor = value; }
        }

        private Color combustionExhaustColor = Common.Defaults.CombustionExhaustColor;
        [DefaultValue(typeof(Color), Common.Defaults.CombustionExhaustColorString)]
        public Color CombustionExhaustColor
        {
            get { return combustionExhaustColor; }
            set { combustionExhaustColor = value; }
        }

        private Color blackColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color BlackColor
        {
            get { return blackColor; }
            set { blackColor = value; }
        }



        private void DrawCrankshaftDiagram()
        {
            base.@SeriesCollection.Clear();


            if (engine == null)
            {
                return;
            }


            #region "poskrbimo, da postavimo delovne takte na vrh"
            List<PositionedCylinder> _workingPositionedCylinders = new List<PositionedCylinder>();
            foreach (PositionedCylinder _positionedCylinder in engine.PositionedCylinders)
            {
                if ((_positionedCylinder.GetStroke(this.crankshaftRotation_deg) .Equals(Stroke.Combustion))
                    || (_positionedCylinder.GetStroke(this.crankshaftRotation_deg).Equals(Stroke.CombustionExhaust)))
                {
                    _workingPositionedCylinders.Add(_positionedCylinder);
                }

            }
            #endregion "poskrbimo, da postavimo delovne takte na vrh"

            #region "narišemo cilindre"
            foreach (PositionedCylinder _positionedCylinder in engine.PositionedCylinders)
            {
                if (!_workingPositionedCylinders.Contains(_positionedCylinder))
                {
                    DrawPositionedCylinder(
                        _positionedCylinder,
                        this.crankshaftRotation_deg);
                }
            }

            //za konec še working cilindre
            foreach (PositionedCylinder _positionedCylinder in _workingPositionedCylinders)
            {
                DrawPositionedCylinder(
                    _positionedCylinder,
                    this.crankshaftRotation_deg);
            }
            #endregion "narišemo cilindre"


            base.chart1.Invalidate();
            base.chart1.Update();
        }
        private void DrawPositionedCylinder(PositionedCylinder _positionedCylinder, double crankshaftRotation_deg)
        {
            Stroke _stroke = _positionedCylinder.GetStroke(crankshaftRotation_deg);

            Color _color;
            #region "določimo barvo glede na takt"
            if (_stroke .Equals(Stroke.Intake))
            {
                _color = this.intakeColor;
            }
            else if (_stroke .Equals(Stroke.Compression))
            {
                _color = this.compressionColor;
            }
            else if (_stroke .Equals(Stroke.Combustion))
            {
                _color = this.combustionColor;
            }
            else if (_stroke .Equals(Stroke.Exhaust))
            {
                _color = this.exhaustColor;
            }
            else if (_stroke .Equals(Stroke.WashCompression))
            {
                _color = this.washCompressionColor;
            }
            else if (_stroke.Equals(Stroke.CombustionExhaust))
            {
                _color = this.combustionExhaustColor;
            }
            else
            {
                _color = this.blackColor;
            }
            #endregion "določimo barvo glede na takt"


            double _crankThrowRotation_deg = _positionedCylinder.GetAbsoluteCrankThrowRotation_deg(crankshaftRotation_deg);
            _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg, true);


            base.DrawLine(
                _crankThrowRotation_deg,
                null,
                _color,
                false,
                this.lineThickness);

            base.DrawMarker(
                _crankThrowRotation_deg,
                _positionedCylinder.Position.ToString(),
                _color,
                false,
                MarkerStyle.Circle,
                this.lineThickness * 5);
        }

    }
}
