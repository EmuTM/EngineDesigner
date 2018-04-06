using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using EngineDesigner.Machine;
using EngineDesigner.Environment.Controls.Charting;

namespace EngineDesigner.Controls.Charting
{
    //PRI TEM GRAFU JE POTREBNA PREVIDNOST, KER SO OSI X IN Y ZAMENJANJE (Y JE HORIZONTALNA); TO JE ZATO, KER DATAPOINTI OMOGOČAJO SAMO MULTI-Y VREDNOSTI, TUKAJ PA POTREBUJEMO 2 VREDNOSTI, DA DOLOČIMO OD-DO
    internal partial class CycleDiagram : ZoomableChart
    {
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


        private string cylinderLabelTemplate = "Cylinder 0";
        [DefaultValue("Cylinder 0")]
        public string CylinderLabelTemplate
        {
            get { return cylinderLabelTemplate; }
            set { cylinderLabelTemplate = value; }
        }


        private double startCrankshaftDiagram_deg = 0d;

        [DefaultValue(0d)]
        public double StartCrankshaftDiagram_deg
        {
            get { return startCrankshaftDiagram_deg; }
            set { startCrankshaftDiagram_deg = value; }
        }

        private double endCrankshaftDiagram_deg = 720d;
        [DefaultValue(720d)]
        public double EndCrankshaftDiagram_deg
        {
            get { return endCrankshaftDiagram_deg; }
            set { endCrankshaftDiagram_deg = value; }
        }



        public CycleDiagram()
        {
            base.ZoomAxisX = false;



        }
        protected override void FormatChartArea(ChartArea _chartArea)
        {
            base.FormatChartArea(_chartArea);


            base.BaseAxisX.MajorTickMark.Enabled = false;
        }
        //overridamo da je prazno
        protected override void SetChartArea(ChartArea _chartArea, Series _series, DataPoint _dataPoint)
        {
        }



        public void DrawDiagram(Engine _engine)
        {
            base.@SeriesCollection.Clear();

            if (_engine == null)
            {
                return;
            }

            DrawCycleDiagram(_engine, this.startCrankshaftDiagram_deg, this.endCrankshaftDiagram_deg);
        }



        private void DrawCycleDiagram(Engine _engine, double _startAngle_deg, double _endAngle_deg)
        {
            Series _series = new Series();
            _series.ChartType = SeriesChartType.RangeBar;
            _series.YValuesPerPoint = 2; //ta tip grafa zahteva 2 Y vrednosti
            _series["PointWidth"] = "1";
            _series["DrawingStyle"] = "Emboss";


            foreach (PositionedCylinder _positionedCylinder in _engine.PositionedCylinders)
            {
                if (!_positionedCylinder.Cycle.Equals(Cycle.NaN))
                {
                    for (double _currentAngle_deg = _startAngle_deg; _currentAngle_deg <= _endAngle_deg + Stroke.StrokeDuration_deg /*tukaj dodamo še en stroke, da zagotovo prikažemo cel chart*/; _currentAngle_deg += Stroke.StrokeDuration_deg)
                    {
                        Stroke _stroke = _positionedCylinder.GetStroke(_currentAngle_deg);

                        double _elapsedStroke_deg = _positionedCylinder.GetElapsedStroke(_stroke, _currentAngle_deg);
                        double _strokeBegin_deg = _currentAngle_deg - _elapsedStroke_deg;

                        Color _color;
                        #region "določimo barvo glede na takt"
                        if (_stroke.Equals(Stroke.Intake))
                        {
                            _color = this.intakeColor;
                        }
                        else if (_stroke.Equals(Stroke.Compression))
                        {
                            _color = this.compressionColor;
                        }
                        else if (_stroke.Equals(Stroke.Combustion))
                        {
                            _color = this.combustionColor;
                        }
                        else if (_stroke.Equals(Stroke.Exhaust))
                        {
                            _color = this.exhaustColor;
                        }
                        else if (_stroke.Equals(Stroke.WashCompression))
                        {
                            _color = this.washCompressionColor;
                        }
                        else if (_stroke.Equals(Stroke.CombustionExhaust))
                        {
                            _color = this.combustionExhaustColor;
                        }
                        else
                        {
                            throw new Exception("_stroke .Equals(Stroke.NaN");
                        }
                        #endregion "določimo barvo glede na takt"

                        base.AddDataPoint(_series,
                            (double)_positionedCylinder.Position - 0.5d,
                            null,
                            _color,
                            _stroke.StrokeId,
                            null,
                            _strokeBegin_deg,
                            _strokeBegin_deg + Stroke.StrokeDuration_deg);
                    }
                }
                else
                {
                    base.AddDataPoint(_series,
                        (double)_positionedCylinder.Position - 0.5d,
                        null,
                        this.blackColor,
                        null,
                        null,
                        _startAngle_deg,
                        _endAngle_deg);
                }
            }


            double _cylindersCount = (double)_engine.NumberOfCylinders;

            base.SetAxis(
                base.BaseAxisX,
                0d, _cylindersCount, 100d / _cylindersCount,
                this.cylinderLabelTemplate + " ",
                false,
                base.ChartLabelsColor);

            #region "popravimo pozicijo lablov"
            foreach (CustomLabel _customLabel in base.BaseAxisX.CustomLabels)
            {
                _customLabel.FromPosition -= .5d;
                _customLabel.ToPosition -= .5d;
            }
            #endregion "popravimo pozicijo lablov"


            base.SetAxis(
                base.BaseAxisY,
                _startAngle_deg, _endAngle_deg, base.ChartGridIntervalPercentage,
                "",
                base.ShowAxisYInPiValues,
                base.ChartLabelsColor);
        }

    }
}
