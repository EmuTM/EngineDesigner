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
using EngineDesigner.Common;

namespace EngineDesigner.Environment.Controls.Charting
{
    public partial class ChartBase : UserControl
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        protected ChartArea BaseChartArea
        {
            get { return chart1.ChartAreas[0]; }
        }



        public ChartAreaCollection @ChartAreaCollection
        {
            get { return this.chart1.ChartAreas; }
        }
        public SeriesCollection @SeriesCollection
        {
            get { return this.chart1.Series; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Legend BaseLegend
        {
            get { return this.chart1.Legends[0]; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Axis BaseAxisX
        {
            get { return this.chart1.ChartAreas[0].AxisX; }
        }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Axis BaseAxisY
        {
            get { return this.chart1.ChartAreas[0].AxisY; }
        }


        private bool zeroesCrossing = false;
        [DefaultValue(false)]
        public bool ZeroesCrossing
        {
            set
            {
                zeroesCrossing = value;

                if (value)
                {
                    this.BaseAxisX.Crossing = 0;
                    this.BaseAxisX.LineWidth = 2;

                    this.BaseAxisY.Crossing = 0;
                    this.BaseAxisY.LineWidth = 2;
                }
            }

            get { return zeroesCrossing; }
        }

        private Color chartGridColor = Color.Silver;
        [DefaultValue(typeof(Color), "Silver")]
        public Color ChartGridColor
        {
            get { return chartGridColor; }
            set { chartGridColor = value; }
        }

        private Color chartAxisColor = Color.DimGray;
        [DefaultValue(typeof(Color), "DimGray")]
        public Color ChartAxisColor
        {
            get { return chartAxisColor; }
            set { chartAxisColor = value; }
        }

        private Color chartCursorColor = Color.Red;
        [DefaultValue(typeof(Color), "Red")]
        public Color ChartCursorColor
        {
            get { return chartCursorColor; }
            set { chartCursorColor = value; }
        }

        private Color chartLabelsColor = Color.Black;
        [DefaultValue(typeof(Color), "Black")]
        public Color ChartLabelsColor
        {
            get { return chartLabelsColor; }
            set { chartLabelsColor = value; }
        }

        private int chartCursorThickness = 1;
        [DefaultValue(1)]
        public int ChartCursorThickness
        {
            get { return chartCursorThickness; }
            set { chartCursorThickness = value; }
        }

        private bool chartCursorXEnabled = false;
        [DefaultValue(false)]
        public bool ChartCursorXEnabled
        {
            get { return this.chartCursorXEnabled; }

            set
            {
                this.chartCursorXEnabled = value;

                if (!this.chartCursorXEnabled)
                {
                    //obvezno se sklicevat na property in ne na field!
                    this.ChartCursorX = double.NaN;
                }
            }
        }

        private bool chartCursorYEnabled = false;
        [DefaultValue(false)]
        public bool ChartCursorYEnabled
        {
            get { return this.chartCursorYEnabled; }

            set
            {
                this.chartCursorYEnabled = value;

                if (!this.chartCursorYEnabled)
                {
                    //obvezno se sklicevat na property in ne na field!
                    this.ChartCursorY = double.NaN;
                }
            }
        }

        private double chartGridIntervalPercentage = 25d;
        [DefaultValue(25d)]
        public double ChartGridIntervalPercentage
        {
            get { return chartGridIntervalPercentage; }
            set { chartGridIntervalPercentage = value; }
        }

        private bool showAxisXInPiValues = false;
        [DefaultValue(false)]
        public bool ShowAxisXInPiValues
        {
            set { showAxisXInPiValues = value; }
            get { return showAxisXInPiValues; }
        }

        private bool showAxisYInPiValues = false;
        [DefaultValue(false)]
        public bool ShowAxisYInPiValues
        {
            set { showAxisYInPiValues = value; }
            get { return showAxisYInPiValues; }
        }

        private string axisLabelsFormat = "0.###";
        [DefaultValue("0.###")]
        public string AxisLabelsFormat
        {
            get { return axisLabelsFormat; }
            set { axisLabelsFormat = value; }
        }

        private double chartCursorX = 0d;
        [DefaultValue(0d)]
        public virtual double ChartCursorX
        {
            get { return chartCursorX; }

            set
            {
                chartCursorX = value;

                this.BaseChartArea.CursorX.Position = this.ComputeCustomCursorX(chartCursorX);
            }
        }

        private double chartCursorY = 0d;
        [DefaultValue(0d)]
        public virtual double ChartCursorY
        {
            get { return chartCursorY; }

            set
            {
                chartCursorY = value;

                this.BaseChartArea.CursorY.Position = this.ComputeCustomCursorX(chartCursorY);
            }
        }



        public ChartBase()
        {
            InitializeComponent();

            this.BaseChartArea.Visible = false;
        }
        private void ChartBase_Load(object sender, EventArgs e)
        {
            this.FormatChartArea(this.BaseChartArea);
        }
        //TA SE OVERRIDA V VSAKEM DERIVANEM KLASU, KJER SI VSAK DA ŠE SVOJE POTREBNO NOTER (VEDNO NAJPREJ KLIČI BASE)
        protected virtual void FormatChartArea(ChartArea _chartArea)
        {
            _chartArea.IsSameFontSizeForAllAxes = true;
            _chartArea.BackColor = Color.White;


            _chartArea.AxisX.LineColor = this.chartAxisColor;

            _chartArea.AxisX.IsMarginVisible = false;

            _chartArea.AxisX.MinorGrid.Enabled = false;

            _chartArea.AxisX.MajorGrid.Enabled = true;
            _chartArea.AxisX.MajorGrid.LineColor = chartGridColor;
            _chartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            _chartArea.AxisX.MajorGrid.LineWidth = 1;

            _chartArea.AxisX.LabelStyle.Enabled = true;
            _chartArea.AxisX.IsLabelAutoFit = false;
            _chartArea.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
            _chartArea.AxisX.LabelStyle.ForeColor = this.BaseAxisX.LabelStyle.ForeColor;
            _chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;

            _chartArea.AxisX.MajorTickMark.Enabled = true;
            _chartArea.AxisX.MajorTickMark.LineColor = this.chartAxisColor;

            _chartArea.CursorX.IsUserEnabled = this.chartCursorXEnabled;
            _chartArea.CursorX.LineWidth = this.chartCursorThickness;
            _chartArea.CursorX.LineDashStyle = ChartDashStyle.Solid;
            _chartArea.CursorX.LineColor = this.chartCursorColor;
            _chartArea.CursorX.SelectionColor = this.chartCursorColor;


            _chartArea.AxisY.LineColor = this.chartAxisColor;

            _chartArea.AxisY.IsMarginVisible = false;

            _chartArea.AxisY.MinorGrid.Enabled = false;

            _chartArea.AxisY.MajorGrid.Enabled = true;
            _chartArea.AxisY.MajorGrid.LineColor = chartGridColor;
            _chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            _chartArea.AxisY.MajorGrid.LineWidth = 1;

            _chartArea.AxisY.LabelStyle.Enabled = true;
            _chartArea.AxisY.IsLabelAutoFit = false;
            _chartArea.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
            _chartArea.AxisY.LabelStyle.ForeColor = this.BaseAxisX.LabelStyle.ForeColor;
            _chartArea.AxisY.LabelStyle.IsEndLabelVisible = true;

            _chartArea.AxisY.MajorTickMark.Enabled = true;
            _chartArea.AxisY.MajorTickMark.LineColor = this.chartAxisColor;

            _chartArea.CursorY.IsUserEnabled = this.chartCursorYEnabled;
            _chartArea.CursorY.LineWidth = this.chartCursorThickness;
            _chartArea.CursorY.LineDashStyle = ChartDashStyle.Solid;
            _chartArea.CursorY.LineColor = this.chartCursorColor;
            _chartArea.CursorY.SelectionColor = this.chartCursorColor;
        }



        //TUKAJ DOBIMO USTREZNO FORMATIRANO CHART AREO (SAMO TAKŠNO LAHKO UPORABLJAMO)
        public ChartArea NewChartArea()
        {
            return this.NewChartArea(Guid.NewGuid().ToString());
        }
        public ChartArea NewChartArea(string _chartAreaName)
        {
            ChartArea _chartArea = new ChartArea(_chartAreaName);
            this.FormatChartArea(_chartArea);

            return _chartArea;
        }

        //NOTE: BUG V CHARTU (če ima samo en datapoint in je ta na 0, 0, ga da na 1, 0)!!!
        public void AddDataPoint(Series _series, double _x, double _y)
        {
            this.AddDataPoint(_series, _x, _y, null);
        }
        public void AddDataPoint(Series _series, double _x, double _y, string _label)
        {
            this.AddDataPoint(_series, _x, _y, _label, Color.Empty, null, null);
        }
        public void AddDataPoint(Series _series, double _x, double _y, string _label, Color _color)
        {
            this.AddDataPoint(_series, _x, _y, _label, _color, null, null);
        }
        public void AddDataPoint(Series _series, double _x, double _y, string _label, Color _color, string _toolTip)
        {
            this.AddDataPoint(_series, _x, _y, _label, _color, _toolTip, null);
        }
        public void AddDataPoint(Series _series, double _x, double _y, string _label, Color _color, string _toolTip, object _tag)
        {
            this.AddDataPoint(this.BaseChartArea, _series, _x, _label, _color, _toolTip, _tag, new double[] { _y });
        }
        public void AddDataPoint(Series _series, double _x, string _label, Color _color, string _toolTip, object _tag, params double[] _yValues)
        {
            this.AddDataPoint(this.BaseChartArea, _series, _x, _label, _color, _toolTip, _tag, _yValues);
        }
        public void AddDataPoint(ChartArea _chartArea, Series _series, double _x, string _label, Color _color, string _toolTip, object _tag, params double[] _yValues)
        {
            DataPoint _dataPoint = this.CreateDataPoint(_x, _label, _color, _toolTip, _tag, _yValues);
            this.AddDataPoint(_chartArea, _series, _dataPoint);
        }
        public void AddDataPoint(ChartArea _chartArea, Series _series, DataPoint _dataPoint)
        {
            //moramo dat na true, ker jo v konstruktorju skrijemo
            this.BaseChartArea.Visible = true;

            #region "ChartArea.Contains po imenu in referenci, ker MSChart očitno upošteva (tudi/zgolj) ime"
            {
                bool _bool = false;
                foreach (ChartArea _chartAreaTmp in this.@ChartAreaCollection)
                {
                    if (_chartAreaTmp.Name == _chartArea.Name)
                    {
                        _bool = true;
                    }
                }
                if ((!_bool)
                    && (!this.@ChartAreaCollection.Contains(_chartArea)))
                {
                    this.@ChartAreaCollection.Add(_chartArea);
                }
            }
            #endregion "ChartArea.Contains po imenu in referenci, ker MSChart očitno upošteva (tudi/zgolj) ime"

            _series.Points.Add(_dataPoint);
            _series.ChartArea = _chartArea.Name;

            #region "Series.Contains po imenu in referenci, ker MSChart očitno upošteva (tudi/zgolj) ime"
            {
                bool _bool = false;
                foreach (Series _seriesTmp in this.@SeriesCollection)
                {
                    if (_seriesTmp.Name == _series.Name)
                    {
                        _bool = true;
                    }
                }
                if ((!_bool)
                    && (!this.@SeriesCollection.Contains(_series)))
                {
                    this.@SeriesCollection.Add(_series);
                }
            }
            #endregion "Series.Contains po imenu in referenci, ker MSChart očitno upošteva (tudi/zgolj) ime"


            this.SetChartArea(_chartArea, _series, _dataPoint);
        }

        public virtual void ClearChart()
        {
            List<ChartArea> _chartAreasToRemove = new List<ChartArea>();
            foreach (ChartArea _chartArea in this.chart1.ChartAreas)
            {
                if (_chartArea != this.BaseChartArea)
                {
                    _chartAreasToRemove.Add(_chartArea);
                }
            }

            foreach (ChartArea _chartArea in _chartAreasToRemove)
            {
                this.chart1.ChartAreas.Remove(_chartArea);
            }


            this.chart1.Series.Clear();
        }


        protected ChartArea GetChartAreaBySeries(Series _series)
        {
            foreach (ChartArea _chartArea in this.ChartAreaCollection)
            {
                if (_series.ChartArea == _chartArea.Name)
                {
                    return _chartArea;
                }
            }

            return null;
        }
        protected Series GetSeriesByDataPoint(DataPoint _dataPoint)
        {
            foreach (Series _series in this.SeriesCollection)
            {
                foreach (DataPoint _dataPointTmp in _series.Points)
                {
                    if (_dataPointTmp == _dataPoint)
                    {
                        return _series;
                    }
                }
            }

            return null;
        }

        protected double GetSeriesMinX(Series _series)
        {
            double _minX = double.MaxValue;

            foreach (DataPoint _dataPoint in _series.Points)
            {
                if (_dataPoint.XValue < _minX)
                {
                    _minX = _dataPoint.XValue;
                }
            }

            return _minX;
        }
        protected double GetSeriesMaxX(Series _series)
        {
            double _maxX = double.MinValue;

            foreach (DataPoint _dataPoint in _series.Points)
            {
                if (_dataPoint.XValue > _maxX)
                {
                    _maxX = _dataPoint.XValue;
                }
            }

            return _maxX;
        }
        protected double GetSeriesMinY(Series _series)
        {
            double _minY = double.MaxValue;

            foreach (DataPoint _dataPoint in _series.Points)
            {
                if (_dataPoint.YValues[0] < _minY)
                {
                    _minY = _dataPoint.YValues[0];
                }
            }

            return _minY;
        }
        protected double GetSeriesMaxY(Series _series)
        {
            double _maxY = double.MinValue;

            foreach (DataPoint _dataPoint in _series.Points)
            {
                if (_dataPoint.YValues[0] > _maxY)
                {
                    _maxY = _dataPoint.YValues[0];
                }
            }

            return _maxY;
        }

        protected double GetChartAreaMinX(ChartArea _chartArea)
        {
            double _minX = double.MaxValue;
            foreach (Series _series in this.SeriesCollection)
            {
                if (_series.ChartArea == _chartArea.Name)
                {
                    double _seriesMinX = this.GetSeriesMinX(_series);

                    if (_seriesMinX < _minX)
                    {
                        _minX = _seriesMinX;
                    }
                }
            }

            return _minX;
        }
        protected double GetChartAreaMaxX(ChartArea _chartArea)
        {
            double _maxX = double.MinValue;
            foreach (Series _series in this.SeriesCollection)
            {
                if (_series.ChartArea == _chartArea.Name)
                {
                    double _seriesMaxX = this.GetSeriesMaxX(_series);

                    if (_seriesMaxX > _maxX)
                    {
                        _maxX = _seriesMaxX;
                    }
                }
            }

            return _maxX;
        }
        protected double GetChartAreaMinY(ChartArea _chartArea)
        {
            double _minY = double.MaxValue;
            foreach (Series _series in this.SeriesCollection)
            {
                if (_series.ChartArea == _chartArea.Name)
                {
                    double _seriesMinY = this.GetSeriesMinY(_series);

                    if (_seriesMinY < _minY)
                    {
                        _minY = _seriesMinY;
                    }
                }
            }

            return _minY;
        }
        protected double GetChartAreaMaxY(ChartArea _chartArea)
        {
            double _maxY = double.MinValue;
            foreach (Series _series in this.SeriesCollection)
            {
                if (_series.ChartArea == _chartArea.Name)
                {
                    double _seriesMaxY = this.GetSeriesMaxY(_series);

                    if (_seriesMaxY > _maxY)
                    {
                        _maxY = _seriesMaxY;
                    }
                }
            }

            return _maxY;
        }



        //TA SE KLIČE PO VSAKI 'ADDDATAPOINT'; ČE MISLIMO FORMATIRAN NA KONCU (PO DODAJANJU VEČ DATAPOINTOV), POTEM TO OVERRIDAMO IN PUSTIMO PRAZNO!
        protected virtual void SetChartArea(ChartArea _chartArea, Series _series, DataPoint _dataPoint)
        {
            double _minX = this.GetChartAreaMinX(_chartArea);
            double _maxX = this.GetChartAreaMaxX(_chartArea);

            this.SetAxis(_chartArea.AxisX,
                _minX, _maxX, this.chartGridIntervalPercentage,
                this.axisLabelsFormat, this.showAxisXInPiValues,
                this.chartLabelsColor);


            double _minY = this.GetChartAreaMinY(_chartArea);
            double _maxY = this.GetChartAreaMaxY(_chartArea);

            this.SetAxis(_chartArea.AxisY,
                _minY, _maxY, this.chartGridIntervalPercentage,
                this.axisLabelsFormat, this.showAxisYInPiValues,
                this.chartLabelsColor);
        }
        //TO LAHKO OVERRIDAMO, DA SI UREDIMO ŠE AXIS PO SVOJE (VEDNO NAJPREJ KLIČI BASE)
        protected virtual void SetAxis(Axis _axis,
            double _min, double _max, double _interval_percentage,
            string _labelFormat, bool _showAxisInPiValues,
            Color _labelsColor)
        {
            try
            {
                //NOTE: to je nekakšen hek, da ni problemov s fukncijo, kjer je celotna aksialna vrednost enaka
                if ((_min == 0)
                    && (_max == 0))
                {
                    _min = -1d;
                    _max = 1d;
                }
                else if (_min == _max)
                {
                    //zmanjšamo za 50%
                    _min -= (_min * 0.5d);
                    //povečamo za 50%
                    _max += (_max * 0.5d);
                }


                //NOTE: ne vem če je prou, da je math.Abs - morda ne sme bit?
                double _interval = Math.Abs((_max - _min) * (_interval_percentage / 100d));
                if (_interval == 0)
                {
                    throw new Exception("Interval cannot be 0.");
                }

                _axis.Minimum = _min;
                _axis.Maximum = _max;
                _axis.Interval = _interval;

                _axis.MajorGrid.Interval = _interval;
                _axis.MajorTickMark.Interval = _interval;

                _axis.CustomLabels.Clear();
                if (_labelFormat != null)
                {
                    #region
                    if (_showAxisInPiValues)
                    {
                        this.CreatePiValues(_axis);
                    }
                    else
                    {
                        bool _minPresent = false;
                        bool _maxPresent = false;

                        for (double _double = _min; _double <= _max; _double += _interval)
                        {
                            CustomLabel _customLabel = new CustomLabel();
                            _customLabel.FromPosition = _double - (_interval / 2d);
                            _customLabel.ToPosition = _double + (_interval / 2d);
                            _customLabel.Text = _double.ToString(_labelFormat);

                            _axis.CustomLabels.Add(_customLabel);


                            if (_double == _min)
                            {
                                _minPresent = true;
                            }
                            if (_double == _max)
                            {
                                _maxPresent = true;
                            }
                        }


                        #region "če je zaradi zaokroževanja pri računanju intervalov izpadel min ali max, ga dodamo tukaj"
                        if (!_minPresent)
                        {
                            CustomLabel _customLabelMin = new CustomLabel();
                            _customLabelMin.FromPosition = _min - 1d; //vseeno kakšna številka je tukaj; samo, da je interval
                            _customLabelMin.ToPosition = _min + 1d; //vseeno kakšna številka je tukaj; samo, da je interval
                            _customLabelMin.Text = _min.ToString(_labelFormat);

                            _axis.CustomLabels.Add(_customLabelMin);
                        }

                        if (!_maxPresent)
                        {
                            CustomLabel _customLabelMax = new CustomLabel();
                            _customLabelMax.FromPosition = _max - 1d; //vseeno kakšna številka je tukaj; samo, da je interval
                            _customLabelMax.ToPosition = _max + 1d; //vseeno kakšna številka je tukaj; samo, da je interval
                            _customLabelMax.Text = _max.ToString(_labelFormat);

                            _axis.CustomLabels.Add(_customLabelMax);
                        }
                        #endregion "če je zaradi zaokroževanja pri računanju intervalov izpadel min ali max, ga dodamo tukaj"
                    }
                    #endregion
                }
                else
                {
                    //skrijemo te elemente
                    _axis.LabelStyle.Enabled = false;
                    _axis.MajorTickMark.Enabled = false;
                }


                _axis.LabelStyle.ForeColor = _labelsColor;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("*****   [System.Diagnostics.DebuggerStepThrough()]   *****");

                _axis.Minimum = double.NaN;
                _axis.Maximum = double.NaN;
                _axis.Interval = double.NaN;

                _axis.MajorGrid.Interval = double.NaN;
                _axis.MajorTickMark.Interval = double.NaN;
            }
        }
        private DataPoint CreateDataPoint(double _x, string _label, Color _color, string _toolTip, object _tag, params double[] _yValues)
        {
            if ((_yValues == null)
                || (_yValues.Length == 0))
            {
                throw new Exception();
            }


            DataPoint _dataPoint = new DataPoint();

            _dataPoint.XValue = _x;
            _dataPoint.YValues = _yValues;
            _dataPoint.Label = _label;
            _dataPoint.LabelForeColor = _color;
            _dataPoint.Color = _color;
            if (string.IsNullOrEmpty(_toolTip))
            {
                _dataPoint.ToolTip = string.Empty;
            }
            else
            {
                _dataPoint.ToolTip = _toolTip;
            }
            _dataPoint.Tag = _tag;

            return _dataPoint;
        }

        private void CreatePiValues(Axis _axis)
        {
            _axis.Interval = 90;

            _axis.MajorGrid.Interval = _axis.Interval;
            _axis.MajorTickMark.Interval = _axis.Interval;

            double _offset = _axis.Minimum % _axis.Interval;
            _axis.MajorGrid.IntervalOffset = -_offset;
            _axis.MajorTickMark.IntervalOffset = -_offset;


            _axis.CustomLabels.Clear();
            double _halfInterval = _axis.Interval / 2;
            double _doubleInterval = _axis.Interval * 2;
            for (double _double = _axis.Minimum; _double <= _axis.Maximum; _double += 1)
            {
                if (_double == 0)
                {
                    CustomLabel _customLabel = new CustomLabel();
                    _customLabel.FromPosition = _double - _halfInterval;
                    _customLabel.ToPosition = _double + _halfInterval;
                    _customLabel.Text = "0";
                    _axis.CustomLabels.Add(_customLabel);
                }
                else
                {
                    if (Mathematics.IsMultiple(_double, _doubleInterval))
                    {
                        #region "PI"
                        CustomLabel _customLabel = new CustomLabel();
                        _customLabel.FromPosition = _double - _halfInterval;
                        _customLabel.ToPosition = _double + _halfInterval;

                        long _multiple = Mathematics.GetMultiple(_double, _doubleInterval);
                        if (_multiple == 1)
                        {
                            _customLabel.Text = "¶";
                        }
                        else if (_multiple == -1)
                        {
                            _customLabel.Text = "-¶";
                        }
                        else
                        {
                            _customLabel.Text = string.Format(
                                "{0}{1}",
                                _multiple.ToString(),
                                "¶");
                        }
                        _axis.CustomLabels.Add(_customLabel);
                        #endregion "PI"
                    }
                    else if (Mathematics.IsMultiple(_double, _axis.Interval))
                    {
                        #region "PI/2"
                        CustomLabel _customLabel = new CustomLabel();
                        _customLabel.FromPosition = _double - _halfInterval;
                        _customLabel.ToPosition = _double + _halfInterval;

                        long _multiple = Mathematics.GetMultiple(_double, _axis.Interval);
                        string _text = string.Empty;
                        if (_multiple < 0)
                        {
                            _text = "-";
                        }

                        long _multipleAbs = Math.Abs(_multiple);
                        _text += string.Format(
                            "{0}/2",
                            _multipleAbs);

                        _customLabel.Text = _text;
                        _axis.CustomLabels.Add(_customLabel);
                        #endregion "PI/2"
                    }
                }
            }
        }

        //te dve prepišemo za kustomizirat pozicijo kurzorja
        protected virtual double ComputeCustomCursorX(double _reference)
        {
            return _reference;
        }
        protected virtual double ComputeCustomCursorY(double _reference)
        {
            return _reference;
        }

    }

}
