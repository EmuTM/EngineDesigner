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
    public partial class FunctionChart : ZoomableChart
    {
        private FunctionChartType chartType = FunctionChartType.LINE;
        [DefaultValue(FunctionChartType.LINE)]
        public FunctionChartType ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        private int chartThickness = 2;
        [DefaultValue(2)]
        public int ChartThickness
        {
            get { return this.chartThickness; }
            set { this.chartThickness = value; }
        }

        private Color chartMarkersColor = Color.Empty; //EMPTY = isto kot chart!
        [DefaultValue(typeof(Color), "Empty")]
        public Color ChartMarkersColor
        {
            get { return this.chartMarkersColor; }
            set { this.chartMarkersColor = value; }
        }

        private int chartMarkersSize = 5;
        [DefaultValue(5)]
        public int ChartMarkersSize
        {
            get { return this.chartMarkersSize; }
            set { this.chartMarkersSize = value; }
        }

        public virtual Function[] FunctionsOnChart
        {
            get
            {
                List<Function> _functions = new List<Function>();

                foreach (Series _series in base.SeriesCollection)
                {
                    if (_series.Tag is Function)
                    {
                        _functions.Add((Function)_series.Tag);
                    }
                }

                return _functions.ToArray();
            }
        }



        //SERIES.TAG JE VEDNO FUNCTION!!!
        //DATAPOINT.TAG JE VEDNO XY!!!
        public Series DrawFunction(Function _function)
        {
            return this.DrawFunction(_function, null);
        }
        public Series DrawFunction(Function _function, string _legendTitle)
        {
            return this.DrawFunction(_function, null, Color.Empty);
        }
        public Series DrawFunction(Function _function, string _legendTitle, Color _color)
        {
            return this.DrawFunction(base.BaseChartArea, _function, _legendTitle, _color);
        }
        public Series DrawFunction(ChartArea _chartArea, Function _function, string _legendTitle, Color _color)
        {
            Series _series = new Series();
            _series.Tag = _function;


            switch (this.chartType)
            {
                case FunctionChartType.LINE:
                    _series.ChartType = SeriesChartType.FastLine; //zmanjša preračunavanje, pohitri
                    _series.BorderWidth = this.chartThickness;
                    break;

                case FunctionChartType.LINE_WITH_MARKERS:
                    _series.ChartType = SeriesChartType.Line;
                    _series.BorderWidth = this.chartThickness;

                    _series.MarkerStyle = MarkerStyle.Circle;
                    _series.MarkerSize = chartMarkersSize;
                    if (!this.chartMarkersColor.IsEmpty)
                    {
                        _series.MarkerColor = this.chartMarkersColor;
                    }
                    break;

                case FunctionChartType.POINT:
                    _series.ChartType = SeriesChartType.FastPoint;
                    _series.MarkerStyle = MarkerStyle.Circle;
                    _series.MarkerSize = this.chartThickness;
                    break;

                case FunctionChartType.SPLINE:
                    _series.ChartType = SeriesChartType.Spline;
                    _series.BorderWidth = this.chartThickness;
                    break;

                case FunctionChartType.SPLINE_WITH_MARKERS:
                    _series.ChartType = SeriesChartType.Spline;
                    _series.BorderWidth = this.chartThickness;
                    _series.MarkerStyle = MarkerStyle.Circle;
                    _series.MarkerSize = chartMarkersSize;
                    if (!this.chartMarkersColor.IsEmpty)
                    {
                        _series.MarkerColor = this.chartMarkersColor;
                    }
                    break;


                default:
                    throw new NotSupportedException();
            }


            if (_color != Color.Empty)
            {
                _series.Color = _color;
            }

            if (!string.IsNullOrEmpty(_legendTitle))
            {
                base.BaseLegend.Enabled = true;
                _series.LegendText = _legendTitle;
            }

            foreach (XY _xy in _function)
            {
                if ((double.IsInfinity(_xy.X))
                    || (double.IsInfinity(_xy.Y)))
                {
                    throw new Exception("Cannot insert infinity values.");
                }


                base.AddDataPoint(
                    _chartArea, _series,
                    _xy.X,
                    null,
                    _color, null,
                    _xy,
                    _xy.Y);
            }


            //ker smo overridali, pokličemo tukaj base (zdaj so točke že dodane in se bo logika izvedla samo 1x)
            base.SetChartArea(_chartArea, _series, null);


            return _series;
        }

        public Series GetSeriesByFunction(Function _function)
        {
            foreach (Series _series in base.SeriesCollection)
            {
                if (_series.Tag == _function)
                {
                    return _series;
                }
            }

            return null;
        }
        public Function[] GetFunctionsOnChartArea(ChartArea _chartArea)
        {
            List<Function> _functions = new List<Function>();

            foreach (Series _series in base.@SeriesCollection)
            {
                if (_series.Tag is Function)
                {
                    if (_series.ChartArea == _chartArea.Name)
                    {
                        _functions.Add((Function)_series.Tag);
                    }
                }
            }

            return _functions.ToArray();
        }
        public string GetFunctionLegendTitle(Function _function)
        {
            List<ChartArea> _chartAreas = new List<ChartArea>();

            foreach (Series _series in base.@SeriesCollection)
            {
                if (_series.Tag == _function)
                {
                    if (!string.IsNullOrEmpty(_series.Label))
                    {
                        return _series.Label;
                    }
                }
            }


            return null;
        }
        public Color GetFunctionColor(Function _function)
        {
            List<ChartArea> _chartAreas = new List<ChartArea>();

            foreach (Series _series in base.@SeriesCollection)
            {
                if (_series.Tag == _function)
                {
                    if (_series.Color != Color.Empty)
                    {
                        return _series.Color;
                    }
                }
            }


            return Color.Empty;
        }



        //to se splača overridat zaradi performans, da se ne izvaja logika po vsaki dodani točki
        protected override void SetChartArea(ChartArea _chartArea, Series _series, DataPoint _dataPoint)
        {
            //kličemo potem base, po vseh dodanih točkah
        }


        //NOTE: zaenkrat naj bo to protected
        protected DataPoint GetDataPointByXY(Function _function, XY _xy)
        {
            return this.GetDataPointByXY(this.GetSeriesByFunction(_function), _xy);
        }
        protected DataPoint GetDataPointByXY(Series _series, XY _xy)
        {
            foreach (DataPoint _dataPoint in _series.Points)
            {
                if ((XY)_dataPoint.Tag == _xy)
                {
                    return _dataPoint;
                }
            }

            return null;
        }

    }


    public enum FunctionChartType
    {
        LINE,
        LINE_WITH_MARKERS,
        POINT,
        SPLINE,
        SPLINE_WITH_MARKERS
    }

}
