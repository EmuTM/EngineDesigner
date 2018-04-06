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


namespace EngineDesigner.Environment.Controls.Charting
{
    public partial class AngleChart : ChartBase
    {
        private const double Y_AXIS_MAX_VALUE = 1d;



        private int circle = 1;
        [DefaultValue(1)]
        public int Circle
        {
            get { return circle; }

            set
            {
                if (value == 0)
                {
                    throw new ArgumentException(">0 za pozitivne vrednosti; <0 za negativne vrednosti. 0 ni dovoljeno");
                }

                circle = value;
                this.SetChartArea(base.BaseChartArea);
            }
        }

        private double intervalDegrees = 45d;
        [DefaultValue(45d)]
        public double IntervalDegrees
        {
            get { return intervalDegrees; }
            set { intervalDegrees = value; }
        }



        protected override void FormatChartArea(ChartArea _chartArea)
        {
            base.FormatChartArea(_chartArea);


            _chartArea.AxisX.MajorTickMark.Enabled = false;
            _chartArea.AxisX.LineColor = Color.Transparent;

            _chartArea.AxisY.MajorTickMark.Enabled = false;
            _chartArea.AxisY.LineColor = Color.Transparent;
        }



        public void DrawLine(double _angle_deg)
        {
            DrawLine(_angle_deg, string.Empty);
        }
        public void DrawLine(double _angle_deg, string _label)
        {
            DrawLine(_angle_deg, _label, Color.Empty);
        }
        public void DrawLine(double _angle_deg, string _label, Color _color)
        {
            DrawLine(_angle_deg, _label, _color, false);
        }
        public void DrawLine(double _angle_deg, string _label, Color _color, bool _showAngleValue)
        {
            DrawLine(_angle_deg, _label, _color, _showAngleValue, 1);
        }
        public void DrawLine(double _angle_deg, string _label, Color _color, bool _showAngleValue, int _lineThickness)
        {
            DrawLine(new Series(), _angle_deg, _label, _color, _showAngleValue, _lineThickness);
        }
        public void DrawLine(Series _series, double _angle_deg, string _label, Color _color, bool _showAngleValue, int _lineThickness)
        {
            _series.BorderWidth = _lineThickness;

            //najprej ničlo za izhodišče
            base.AddDataPoint(_series, 0, 0, string.Empty, _color);


            this.Draw(_series, _angle_deg, _label, _color, _showAngleValue);
        }

        public void DrawMarker(double _angle_deg)
        {
            DrawMarker(_angle_deg, string.Empty);
        }
        public void DrawMarker(double _angle_deg, string _label)
        {
            DrawMarker(_angle_deg, _label, Color.Empty);
        }
        public void DrawMarker(double _angle_deg, string _label, Color _color)
        {
            DrawMarker(_angle_deg, _label, _color, false);
        }
        public void DrawMarker(double _angle_deg, string _label, Color _color, bool _showAngleValue)
        {
            DrawMarker(_angle_deg, _label, _color, _showAngleValue, MarkerStyle.Circle, 10);
        }
        public void DrawMarker(double _angle_deg, string _label, Color _color, bool _showAngleValue, MarkerStyle _markerStyle)
        {
            DrawMarker(_angle_deg, _label, _color, _showAngleValue, _markerStyle, 10);
        }
        public void DrawMarker(double _angle_deg, string _label, Color _color, bool _showAngleValue, MarkerStyle _markerStyle, int _markerSize)
        {
            DrawMarker(new Series(), _angle_deg, _label, _color, _showAngleValue, _markerStyle, _markerSize);
        }
        public void DrawMarker(Series _series, double _angle_deg, string _label, Color _color, bool _showAngleValue, MarkerStyle _markerStyle, int _markerSize)
        {
            _series.CustomProperties = "PolarDrawingStyle=Marker";
            _series.MarkerStyle = _markerStyle;
            _series.MarkerSize = _markerSize;


            this.Draw(_series, _angle_deg, _label, _color, _showAngleValue);
        }

        private void Draw(Series _series, double _angle_deg, string _label, Color _color, bool _showAngleValue)
        {
            if ((_angle_deg < this.StartDegrees)
                || (_angle_deg > this.EndDegrees))
            {
                throw new ArgumentException("The provided angle is out of chart's range.");
            }


            _series.ChartType = SeriesChartType.Polar;


            //potem še dejansko vrednost kota
            if (_showAngleValue)
            {
                base.AddDataPoint(
                    _series,
                    _angle_deg,
                    Y_AXIS_MAX_VALUE,
                    string.Format(
                        "{0} ({1}°)",
                        _label,
                        _angle_deg.ToString()),
                    _color);
            }
            else
            {
                base.AddDataPoint(_series, _angle_deg, Y_AXIS_MAX_VALUE, _label, _color);
            }


            this.SetChartArea(base.BaseChartArea);
        }

        private double StartDegrees
        {
            get
            {
                double _startDegrees;

                if (circle > 0)
                {
                    _startDegrees = (circle * 360) - 360;
                }
                else if (circle < 0)
                {
                    _startDegrees = circle * 360;
                }
                else
                {
                    throw new NotSupportedException();
                }

                return _startDegrees;
            }
        }
        private double EndDegrees
        {
            get
            {
                double _endDegrees = this.StartDegrees + 360d;
                return _endDegrees;
            }
        }



        //to naredimo novo, za setiranje axisov tukaj
        protected virtual void SetChartArea(ChartArea _chartArea)
        {
            double _intervalPercentage = 100d / (360d / this.intervalDegrees);

            base.SetAxis(_chartArea.AxisX,
                this.StartDegrees, this.EndDegrees, _intervalPercentage,
                string.Format("{0}°", base.AxisLabelsFormat),
                base.ShowAxisXInPiValues,
                base.ChartLabelsColor);

            //tukaj malo heknemo, da je pravilno tudi prikazovanje negativnih stopinj (čene bi pisalo -360)
            if (this.circle < 0)
            {
                _chartArea.AxisX.CustomLabels[0].Text = string.Format(
                    "{0}°",
                    this.EndDegrees.ToString(base.AxisLabelsFormat));
            }


            base.SetAxis(_chartArea.AxisY,
                0d, Y_AXIS_MAX_VALUE, 100d,
                null,
                false,
                base.ChartLabelsColor);
        }
        //to overridamo, da ni logike v njej (ker čene je v basu logika za setiranje axisov)
        protected override void SetChartArea(ChartArea _chartArea, Series _series, DataPoint _dataPoint)
        {
        }

    }
}
