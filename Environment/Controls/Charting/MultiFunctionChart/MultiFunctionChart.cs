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
using System.Runtime.Serialization;
using EngineDesigner.Common.Serialization;

namespace EngineDesigner.Environment.Controls.Charting
{
    public partial class MultiFunctionChart : FunctionChart
    {
        //NOTE: to gre v tag od ChartArea; da ni problemov s serializacijo, delamo z byte[]
        [Serializable]
        private class ChartAreaTagger
        {
            private ChartAreaTagger()
            {
            }
            public ChartAreaTagger(Guid _guid, ChartAreaType _chartAreaType, Color _chartColor)
            {
                this.guid = _guid;
                this.chartAreaType = _chartAreaType;
                this.chartColor = _chartColor;
            }


            private Guid guid;
            public Guid @Guid
            {
                get { return guid; }
            }

            private ChartAreaType chartAreaType;
            public ChartAreaType @ChartAreaType
            {
                get { return chartAreaType; }
            }

            private Color chartColor;
            public Color ChartColor
            {
                get { return chartColor; }
            }
        }
        private enum ChartAreaType
        {
            AXIS_X = 2,
            AXIS_Y = 4,
            CHART = 8
        }



        public event EventHandler<MultiFunctionChartEventArgs> FunctionAdded;
        public event EventHandler<MultiFunctionChartEventArgs> FunctionRemoved;
        public event EventHandler SelectedFunctionsChanged;
        public event EventHandler LegendContextMenuOpening;
        public event EventHandler LegendContextMenuClosing;
        public event EventHandler<CustomLegendItemEventArgs> LegendItemToolTipShowing;



        private const int LEGEND_TO_CHART_HEIGHT_CORRECTION = 32;
        private const float LEGEND_TO_CHART_CLEARANCE_PERCENTAGE = 2f;
        private const int TICK_MARK_SIZE_PIXELS = 2;



        private int legendRightSideClearance; //odmik od skrajnega desnega roba charta
        private bool loaded = false; //pove, če je kontrola zloadana
        private Graphics graphics; //tukaj shranimo graphics tega objekta, za preračunavanje fontov



        private bool legendOverChart = false;
        [DefaultValue(false)]
        public bool LegendOverChart
        {
            get { return legendOverChart; }

            set
            {
                legendOverChart = value;
                this.PerformLegendLayout();
            }
        }

        private float legendWidthPercentage = 25f;
        [DefaultValue(25f)]
        public float LegendWidthPercentage
        {
            get { return legendWidthPercentage; }

            set
            {
                legendWidthPercentage = value;
                this.PerformLegendLayout();
            }
        }

        private double maxAllowedDifferenceBetweenChartExtremesPercentage = 10d;
        [DefaultValue(10d)]
        public double MaxAllowedDifferenceBetweenChartExtremesPercentage
        {
            get { return maxAllowedDifferenceBetweenChartExtremesPercentage; }
            set { maxAllowedDifferenceBetweenChartExtremesPercentage = value; }
        }

        //s temi propertiji lahko določimo razpon glavne osi (na kateri teče kurzor)
        private double cursorWindowMinX = 0d;
        [DefaultValue(0d)]
        public double CursorWindowMinX
        {
            get { return cursorWindowMinX; }
            set { cursorWindowMinX = value; }
        }

        private double cursorWindowMaxX = 100d;
        [DefaultValue(100d)]
        public double CursorWindowMaxX
        {
            get { return cursorWindowMaxX; }
            set { cursorWindowMaxX = value; }
        }

        private double cursorWindowMinY = 0d;
        [DefaultValue(0d)]
        public double CursorWindowMinY
        {
            get { return cursorWindowMinY; }
            set { cursorWindowMinY = value; }
        }

        private double cursorWindowMaxY = 100d;
        [DefaultValue(100d)]
        public double CursorWindowMaxY
        {
            get { return cursorWindowMaxY; }
            set { cursorWindowMaxY = value; }
        }

        private ContextMenuStrip legendContextMenuToMerge = null;
        public ContextMenuStrip LegendContextMenuToMerge
        {
            get { return legendContextMenuToMerge; }
            set { legendContextMenuToMerge = value; }
        }

        public Function[] SelectedFunctions
        {
            get
            {
                List<Function> _selectedFunctions = new List<Function>();

                foreach (Series _series in this.multiFunctionChartLegend1.SelectedSeries)
                {
                    _selectedFunctions.Add((Function)_series.Tag);
                }

                return _selectedFunctions.ToArray();
            }
        }

        public override Function[] FunctionsOnChart
        {
            get
            {
                List<Function> _functions = new List<Function>();

                foreach (Series _series in base.SeriesCollection)
                {
                    //NOTE: samo tiste, kjer color ni transparent!!!
                    if (_series.Color != Color.Transparent)
                    {
                        if (_series.Tag is Function)
                        {
                            _functions.Add((Function)_series.Tag);
                        }
                    }
                }

                return _functions.ToArray();
            }
        }


        public MultiFunctionChart()
        {
            InitializeComponent();


            base.BaseLegend.Enabled = false;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            #region "naredimo chart grid na base chart arei"
            base.BaseAxisX.Minimum = 0;
            base.BaseAxisX.Maximum = 100;
            base.BaseAxisX.Interval = base.ChartGridIntervalPercentage;
            //base.BaseAxisX.MajorGrid.Enabled = true; //TODO: verjetno za vržt vn
            base.BaseAxisX.MajorGrid.Interval = base.ChartGridIntervalPercentage;

            base.BaseAxisY.Minimum = 0;
            base.BaseAxisY.Maximum = 100;
            base.BaseAxisY.Interval = base.ChartGridIntervalPercentage;
            //base.BaseAxisY.MajorGrid.Enabled = true; //TODO: verjetno za vržt vn
            base.BaseAxisY.MajorGrid.Interval = base.ChartGridIntervalPercentage;
            #endregion "naredimo chart grid na base chart arei"


            this.legendRightSideClearance =
                this.Width
                - (this.multiFunctionChartLegend1.Location.X
                + this.multiFunctionChartLegend1.Width);


            graphics = this.CreateGraphics();
            loaded = true;


            this.PerformLegendLayout();
            this.PerformChartLayout();


            if (this.DesignMode)
            {
                base.BaseChartArea.Visible = false;
                this.multiFunctionChartLegend1.Visible = false;
            }
            else
            {
                if (this.legendContextMenuToMerge != null)
                {
                    ToolStripManager.RevertMerge(this.multiFunctionChartLegend1.ContextMenuStrip);
                    if (!ToolStripManager.Merge(this.legendContextMenuToMerge, this.multiFunctionChartLegend1.ContextMenuStrip))
                    {
                        throw new Exception();
                    }
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //ta graf postavlja chart aree po potrebi, zato to ni možno. namreč, zero je vedno nekje drugje
            if (base.ZeroesCrossing)
            {
                throw new Exception("Setting 'ZeroesCrossing' is not compatible with this chart.");
            }


            base.OnPaint(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);


            this.PerformLegendLayout();
            this.PerformChartLayout();
        }
        protected override void FormatChartArea(ChartArea _chartArea)
        {
            base.FormatChartArea(_chartArea);


            if (_chartArea == base.BaseChartArea)
            {
                _chartArea.AxisX.LabelStyle.Enabled = false;
                _chartArea.AxisX.MajorTickMark.Enabled = false;
                _chartArea.AxisX.LineColor = Color.Transparent;

                _chartArea.AxisY.LabelStyle.Enabled = false;
                _chartArea.AxisY.MajorTickMark.Enabled = false;
                _chartArea.AxisY.LineColor = Color.Transparent;
            }
            else
            {
                _chartArea.AxisX.ScrollBar.Enabled = false;
                _chartArea.CursorX.IsUserEnabled = false;

                _chartArea.AxisY.ScrollBar.Enabled = false;
                _chartArea.CursorY.IsUserEnabled = false;
            }
        }
        protected override void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            base.chart1_AxisViewChanged(sender, e);


            Common.Utility.Log(string.Format(
                "NEW POSITION: {0}",
                e.NewPosition));




            if (e.Axis.AxisName == AxisName.X)
            {
                double _basePosition = e.NewPosition / base.BaseAxisX.ScaleView.Size;


                foreach (ChartArea _chartArea in base.ChartAreaCollection)
                {
                    if (_chartArea != e.ChartArea)
                    {
                        double _axisPosition = _chartArea.AxisX.Minimum + (_basePosition * _chartArea.AxisX.ScaleView.Size);

                        _chartArea.AxisX.ScaleView.Position = _axisPosition;
                        _chartArea.AxisX.MajorGrid.IntervalOffset = -_axisPosition;
                        _chartArea.AxisX.MajorTickMark.IntervalOffset = -_axisPosition;


                        ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);
                        Common.Utility.Log(string.Format(
                            "{0}: {1} / {2}]",
                            _chartAreaTagger.ChartAreaType,
                            _axisPosition,
                            _chartArea.AxisX.ScaleView.Size));
                    }
                }
            }
            else if (e.Axis.AxisName == AxisName.Y)
            {
                double _basePosition = e.NewPosition / base.BaseAxisY.ScaleView.Size;


                foreach (ChartArea _chartArea in base.ChartAreaCollection)
                {
                    if (_chartArea != e.ChartArea)
                    {
                        double _axisPosition = _chartArea.AxisY.Minimum + (_basePosition * _chartArea.AxisY.ScaleView.Size);

                        _chartArea.AxisY.ScaleView.Position = _axisPosition;
                        _chartArea.AxisY.MajorGrid.IntervalOffset = -_axisPosition;
                        _chartArea.AxisY.MajorTickMark.IntervalOffset = -_axisPosition;


                        ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);
                        Common.Utility.Log(string.Format(
                            "{0}: {1} / {2}]",
                            _chartAreaTagger.ChartAreaType,
                            _axisPosition,
                            _chartArea.AxisY.ScaleView.Size));
                    }
                }
            }
        }
        protected override double ComputeCustomCursorX(double _reference)
        {
            //dobimo razpon
            double _double = Math.Abs(this.cursorWindowMaxX - this.cursorWindowMinX);
            //dobimo kot v procentih
            double _angle_percentage = (100d / _double) * _reference;
            //System.Diagnostics.Debug.WriteLine("_kotVProcentih :: " + _angle_percentage);
            //dobimo odmik od začetka, v procentih, vedno pozitiven
            double _offset_percentage = Math.Abs((100d / _double) * this.cursorWindowMinX);
            //System.Diagnostics.Debug.WriteLine("_odmikOdZačetka :: " + _odmikOdZačetka);

            return _angle_percentage + _offset_percentage;
        }
        protected override double ComputeCustomCursorY(double _reference)
        {
            //dobimo razpon
            double _double = Math.Abs(this.cursorWindowMaxY - this.cursorWindowMinY);
            //dobimo kot v procentih
            double _angle_percentage = (100d / _double) * _reference;
            //System.Diagnostics.Debug.WriteLine("_kotVProcentih :: " + _angle_percentage);
            //dobimo odmik od začetka, v procentih, vedno pozitiven
            double _offset_percentage = Math.Abs((100d / _double) * this.cursorWindowMinY);
            //System.Diagnostics.Debug.WriteLine("_odmikOdZačetka :: " + _odmikOdZačetka);

            return _angle_percentage + _offset_percentage;
        }



        public void AddFunction(Function _function, string _legendTitle, Color _color)
        {
            this.AddFunction(_function, _legendTitle, _color, false);
        }
        public void AddFunction(Function _function, string _legendTitle, Color _color, bool _forceNewChartArea)
        {
            double _minX = _function.GetMinX();
            double _maxX = _function.GetMaxX();
            double _minY = _function.GetMinY();
            double _maxY = _function.GetMaxY();


            ChartArea _chartAreaAxisX = null;
            ChartArea _chartAreaAxisY = null;
            ChartArea _chartArea = null;

            if (!_forceNewChartArea)
            {
                _chartArea = this.GetBestSuitedChartArea(
                    _minX, _maxX, this.maxAllowedDifferenceBetweenChartExtremesPercentage,
                    _minY, _maxY, this.maxAllowedDifferenceBetweenChartExtremesPercentage,
                    out _chartAreaAxisX, out _chartAreaAxisY);
            }


            if (_chartArea == null)
            {
                //vse tri dobijo isti guid
                Guid _guid = Guid.NewGuid();

                if ((_chartAreaAxisX == null) || (_chartAreaAxisY == null))
                {
                    #region "eno moramo narediti za prikazovanje grafa"
                    _chartArea = base.NewChartArea(Guid.NewGuid().ToString());
                    _chartArea.Tag = BinarySerializer.Serialize(new ChartAreaTagger(_guid, ChartAreaType.CHART, _color));
                    _chartArea.BackColor = Color.Transparent;
                    _chartArea.IsSameFontSizeForAllAxes = base.BaseChartArea.IsSameFontSizeForAllAxes;
                    base.chart1.ChartAreas.Add(_chartArea);

                    //te so za chart in nimajo oznak
                    this.SetAxis(_chartArea.AxisX,
                         _minX, _maxX,
                         base.ChartGridIntervalPercentage,
                         null, true, false, false);

                    this.SetAxis(_chartArea.AxisY,
                        _minY, _maxY,
                        base.ChartGridIntervalPercentage,
                        null, true, false, false);
                    #endregion "eno moramo narediti za prikazovanje grafa"
                }

                if (_chartAreaAxisX == null)
                {
                    #region "eno moramo narediti za X os"
                    _chartAreaAxisX = base.NewChartArea(Guid.NewGuid().ToString());
                    _chartAreaAxisX.BackColor = Color.Transparent;
                    _chartAreaAxisX.IsSameFontSizeForAllAxes = base.BaseChartArea.IsSameFontSizeForAllAxes;
                    _chartAreaAxisX.Tag = BinarySerializer.Serialize(new ChartAreaTagger(_guid, ChartAreaType.AXIS_X, _color));
                    base.chart1.ChartAreas.Add(_chartAreaAxisX);

                    if (this.GetChartAreasCountX() > 1)
                    {
                        this.SetAxis(_chartAreaAxisX.AxisX,
                             _minX, _maxX,
                             base.ChartGridIntervalPercentage,
                             base.AxisLabelsFormat, false, base.ShowAxisXInPiValues, true);
                    }
                    else
                    {
                        this.SetAxis(_chartAreaAxisX.AxisX,
                             _minX, _maxX,
                             base.ChartGridIntervalPercentage,
                             base.AxisLabelsFormat, false, base.ShowAxisXInPiValues, true);
                    }

                    //ta je dummy
                    this.SetAxis(_chartAreaAxisX.AxisY,
                        _minY, _maxY,
                        base.ChartGridIntervalPercentage,
                        null, false, false, false);
                    #endregion "eno moramo narediti za X os"
                }

                if (_chartAreaAxisY == null)
                {
                    #region "eno moramo narediti za Y os"
                    _chartAreaAxisY = base.NewChartArea(Guid.NewGuid().ToString());
                    _chartAreaAxisY.BackColor = Color.Transparent;
                    _chartAreaAxisY.IsSameFontSizeForAllAxes = base.BaseChartArea.IsSameFontSizeForAllAxes;
                    _chartAreaAxisY.Tag = BinarySerializer.Serialize(new ChartAreaTagger(_guid, ChartAreaType.AXIS_Y, _color));
                    base.chart1.ChartAreas.Add(_chartAreaAxisY);

                    //ta je dummy
                    this.SetAxis(_chartAreaAxisY.AxisX,
                         _minX, _maxX,
                         base.ChartGridIntervalPercentage,
                         null, false, false, false);

                    if (this.GetChartAreasCountY() > 1)
                    {
                        this.SetAxis(_chartAreaAxisY.AxisY,
                            _minY, _maxY,
                            base.ChartGridIntervalPercentage,
                            base.AxisLabelsFormat, false, base.ShowAxisYInPiValues, true);
                    }
                    else
                    {
                        this.SetAxis(_chartAreaAxisY.AxisY,
                            _minY, _maxY,
                            base.ChartGridIntervalPercentage,
                            base.AxisLabelsFormat, false, base.ShowAxisYInPiValues, true);
                    }
                    #endregion "eno moramo narediti za Y os"
                }
            }


            this.AddFunction(_chartAreaAxisX, _chartAreaAxisY, _chartArea,
                _function, _legendTitle, _color);
        }
        public void AddFunction(ChartArea _chartAreaAxisX, ChartArea _chartAreaAxisY, ChartArea _chartAreaChart, Function _function, string _legendTitle, Color _color)
        {
            if (_color == Color.Transparent)
            {
                throw new Exception("Transparent color is not allowed.");
            }


            //series za base.ChartArea
            //NOTE: color mora biti transparent, ker zaradi tega vemo, kaj je dejansko na chartu za uporabnika
            Series _series2 = base.DrawFunction(base.BaseChartArea, _function, null, Color.Transparent);
            _series2.IsVisibleInLegend = false;


            ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartAreaChart.Tag);


            #region "dobimo nove skrajne vrednosti"
            double _newMinX = _function.GetMinX();
            double _newMaxX = _function.GetMaxX();
            double _newMinY = _function.GetMinY();
            double _newMaxY = _function.GetMaxY();


            Function[] _functionsOnChartArea = base.GetFunctionsOnChartArea(_chartAreaChart);
            foreach (Function _functionTmp in _functionsOnChartArea)
            {
                double _minX = _functionTmp.GetMinX();
                if (_minX < _newMinX)
                {
                    _newMinX = _minX;
                }

                double _maxX = _functionTmp.GetMaxX();
                if (_maxX > _newMaxX)
                {
                    _newMaxX = _maxX;
                }

                double _minY = _functionTmp.GetMinY();
                if (_minY < _newMinY)
                {
                    _newMinY = _minY;
                }

                double _maxY = _functionTmp.GetMaxY();
                if (_maxY > _newMaxY)
                {
                    _newMaxY = _maxY;
                }
            }
            #endregion "dobimo nove skrajne vrednosti"

            #region "spremenimo min in max, da je ok za obe funkciji"
            if (this.GetChartAreasCountX() > 1)
            {
                this.SetAxis(_chartAreaAxisX.AxisX,
                     _newMinX, _newMaxX,
                     base.ChartGridIntervalPercentage,
                     base.AxisLabelsFormat, false, base.ShowAxisXInPiValues, true);
            }
            else
            {
                this.SetAxis(_chartAreaAxisX.AxisX,
                     _newMinX, _newMaxX,
                     base.ChartGridIntervalPercentage,
                     base.AxisLabelsFormat, false, base.ShowAxisXInPiValues, true);
            }

            //ta je dummy
            this.SetAxis(_chartAreaAxisX.AxisY,
                _newMinY, _newMaxY,
                base.ChartGridIntervalPercentage,
                null, false, false, false);


            //ta je dummy
            this.SetAxis(_chartAreaAxisY.AxisX,
                 _newMinX, _newMaxX,
                 base.ChartGridIntervalPercentage,
                 null, false, false, false);

            if (this.GetChartAreasCountY() > 1)
            {
                this.SetAxis(_chartAreaAxisY.AxisY,
                    _newMinY, _newMaxY,
                    base.ChartGridIntervalPercentage,
                    base.AxisLabelsFormat, false, base.ShowAxisYInPiValues, true);
            }
            else
            {
                this.SetAxis(_chartAreaAxisY.AxisY,
                    _newMinY, _newMaxY,
                    base.ChartGridIntervalPercentage,
                    base.AxisLabelsFormat, false, base.ShowAxisYInPiValues, true);
            }


            //te so za chart in nimajo oznak
            this.SetAxis(_chartAreaChart.AxisX,
               _newMinX, _newMaxX,
               base.ChartGridIntervalPercentage,
               null, true, false, false);

            this.SetAxis(_chartAreaChart.AxisY,
                _newMinY, _newMaxY,
                base.ChartGridIntervalPercentage,
                null, true, false, false);
            #endregion "spremenimo min in max, da je ok za obe funkciji"


            //NOTE: color mora biti transparent, ker zaradi tega vemo, kaj je dejansko na chartu za uporabnika
            Series _seriesX = base.DrawFunction(_chartAreaAxisX, _function, null, Color.Transparent);
            _seriesX.IsVisibleInLegend = false;

            //NOTE: color mora biti transparent, ker zaradi tega vemo, kaj je dejansko na chartu za uporabnika
            Series _seriesY = base.DrawFunction(_chartAreaAxisY, _function, null, Color.Transparent);
            _seriesY.IsVisibleInLegend = false;

            //dodamo series (tudi na legendo)
            Series _series = base.DrawFunction(_chartAreaChart, _function, _legendTitle, _color);
            _series.Label = _legendTitle;
            _series.IsVisibleInLegend = false;
            this.multiFunctionChartLegend1.SeriesCollection.Add(_series);



            //in naredimo layout
            this.PerformChartLayout();


            this.OnFunctionAdded(
                _function,
                _chartAreaAxisX, _chartAreaAxisY, _chartAreaChart);
        }
        //to metodo kliče tudi custom legend
        public void RemoveFunction(Function _function)
        {
            #region "dobimo chart aree, na katere bomo vplivali"
            ChartArea _chartAreaAxisX = this.GetFunctionChartAreaAxisX(_function);
            ChartArea _chartAreaAxisY = this.GetFunctionChartAreaAxisY(_function);
            ChartArea _chartAreaChart = this.GetFunctionChartAreaChart(_function);
            #endregion "dobimo chart aree, na katere bomo vplivali"

            #region "odstranimo tiste series, ki imajo za tag našo funkcijo (tudi iz legende)"
            List<Series> _seriesToRemove = new List<Series>();
            foreach (Series _series in base.@SeriesCollection)
            {
                if (_series.Tag is Function)
                {
                    Function _functionTmp = (Function)_series.Tag;

                    if (_functionTmp == _function)
                    {
                        _seriesToRemove.Add(_series);
                    }
                }
            }

            foreach (Series _series in _seriesToRemove)
            {
                base.@SeriesCollection.Remove(_series);

                //tudi iz legende
                if (this.multiFunctionChartLegend1.SeriesCollection.Contains(_series))
                {
                    this.multiFunctionChartLegend1.SeriesCollection.Remove(_series);
                }
            }
            #endregion "odstranimo tiste series, ki imajo za tag našo funkcijo (tudi iz legende)"

            #region "odstranimo tiste chart aree, ki jim ne pripada (več) noben series"
            List<ChartArea> _chartAreasToLeave = new List<ChartArea>();
            foreach (ChartArea _chartArea in base.chart1.ChartAreas)
            {
                foreach (Series _series in base.@SeriesCollection)
                {
                    if (_series.ChartArea == _chartArea.Name)
                    {
                        _chartAreasToLeave.Add(_chartArea);
                        break;
                    }
                }
            }

            List<ChartArea> _chartAreasToRemove = new List<ChartArea>();
            foreach (ChartArea _chartArea in base.chart1.ChartAreas)
            {
                if (!_chartAreasToLeave.Contains(_chartArea))
                {
                    _chartAreasToRemove.Add(_chartArea);
                }
            }

            foreach (ChartArea _chartArea in _chartAreasToRemove)
            {
                //base chartaree ne odstranimo v nobenem primeru
                if (_chartArea != base.BaseChartArea)
                {
                    base.chart1.ChartAreas.Remove(_chartArea);
                }
            }
            #endregion "odstranimo tiste chart aree, ki jim ne pripada (več) noben series"

            #region "izračunamo še enkrat minimume in maksimume"
            #region "AxisX"
            Function[] _functionsOnChartAreaAxisX = base.GetFunctionsOnChartArea(_chartAreaAxisX);

            if (_functionsOnChartAreaAxisX.Length > 0)
            {
                double _minX = double.MaxValue;
                foreach (Function _functionTmp in _functionsOnChartAreaAxisX)
                {
                    double _functionMinX = _functionTmp.GetMinX();

                    if (_functionMinX < _minX)
                    {
                        _minX = _functionMinX;
                    }
                }

                double _maxX = double.MinValue;
                foreach (Function _functionTmp in _functionsOnChartAreaAxisX)
                {
                    double _functionMaxX = _functionTmp.GetMaxX();

                    if (_functionMaxX > _maxX)
                    {
                        _maxX = _functionMaxX;
                    }
                }

                double _minY = double.MaxValue;
                foreach (Function _functionTmp in _functionsOnChartAreaAxisX)
                {
                    double _functionMinY = _functionTmp.GetMinY();

                    if (_functionMinY < _minY)
                    {
                        _minY = _functionMinY;
                    }
                }

                double _maxY = double.MinValue;
                foreach (Function _functionTmp in _functionsOnChartAreaAxisX)
                {
                    double _functionMaxY = _functionTmp.GetMaxY();

                    if (_functionMaxY > _maxY)
                    {
                        _maxY = _functionMaxY;
                    }
                }


                this.SetAxis(_chartAreaAxisX.AxisX,
                     _minX, _maxX,
                     base.ChartGridIntervalPercentage,
                     base.AxisLabelsFormat, false, base.ShowAxisXInPiValues, true);

                //ta je dummy
                this.SetAxis(_chartAreaAxisX.AxisY,
                    _minY, _maxY,
                    base.ChartGridIntervalPercentage,
                    null, false, false, false);
            }
            #endregion "AxisY"

            #region "AxisY"
            Function[] _functionsOnChartAreaAxisY = base.GetFunctionsOnChartArea(_chartAreaAxisY);

            if (_functionsOnChartAreaAxisY.Length > 0)
            {
                double _minX = double.MaxValue;
                foreach (Function _functionTmp in _functionsOnChartAreaAxisY)
                {
                    double _functionMinX = _functionTmp.GetMinX();

                    if (_functionMinX < _minX)
                    {
                        _minX = _functionMinX;
                    }
                }

                double _maxX = double.MinValue;
                foreach (Function _functionTmp in _functionsOnChartAreaAxisY)
                {
                    double _functionMaxX = _functionTmp.GetMaxX();

                    if (_functionMaxX > _maxX)
                    {
                        _maxX = _functionMaxX;
                    }
                }

                double _minY = double.MaxValue;
                foreach (Function _functionTmp in _functionsOnChartAreaAxisY)
                {
                    double _functionMinY = _functionTmp.GetMinY();

                    if (_functionMinY < _minY)
                    {
                        _minY = _functionMinY;
                    }
                }

                double _maxY = double.MinValue;
                foreach (Function _functionTmp in _functionsOnChartAreaAxisY)
                {
                    double _functionMaxY = _functionTmp.GetMaxY();

                    if (_functionMaxY > _maxY)
                    {
                        _maxY = _functionMaxY;
                    }
                }


                //ta je dummy
                this.SetAxis(_chartAreaAxisY.AxisX,
                     _minX, _maxX,
                     base.ChartGridIntervalPercentage,
                     null, false, false, false);

                this.SetAxis(_chartAreaAxisY.AxisY,
                    _minY, _maxY,
                    base.ChartGridIntervalPercentage,
                    base.AxisLabelsFormat, false, base.ShowAxisYInPiValues, true);
            }
            #endregion "AxisY"

            #region "base.chart1"
            Function[] _functionsOnChartAreaChart = base.GetFunctionsOnChartArea(_chartAreaChart);

            if (_functionsOnChartAreaChart.Length > 0)
            {
                double _minX = double.MaxValue;
                foreach (Function _functionTmp in _functionsOnChartAreaChart)
                {
                    double _functionMinX = _functionTmp.GetMinX();

                    if (_functionMinX < _minX)
                    {
                        _minX = _functionMinX;
                    }
                }

                double _maxX = double.MinValue;
                foreach (Function _functionTmp in _functionsOnChartAreaChart)
                {
                    double _functionMaxX = _functionTmp.GetMaxX();

                    if (_functionMaxX > _maxX)
                    {
                        _maxX = _functionMaxX;
                    }
                }

                double _minY = double.MaxValue;
                foreach (Function _functionTmp in _functionsOnChartAreaChart)
                {
                    double _functionMinY = _functionTmp.GetMinY();

                    if (_functionMinY < _minY)
                    {
                        _minY = _functionMinY;
                    }
                }

                double _maxY = double.MinValue;
                foreach (Function _functionTmp in _functionsOnChartAreaChart)
                {
                    double _functionMaxY = _functionTmp.GetMaxY();

                    if (_functionMaxY > _maxY)
                    {
                        _maxY = _functionMaxY;
                    }
                }


                //te so za chart in nimajo oznak
                this.SetAxis(_chartAreaChart.AxisX,
                   _minX, _maxX,
                   base.ChartGridIntervalPercentage,
                   null, true, false, false);

                this.SetAxis(_chartAreaChart.AxisY,
                    _minY, _maxY,
                    base.ChartGridIntervalPercentage,
                    null, true, false, false);
            }
            #endregion "base.chart1"
            #endregion "izračunamo še enkrat minimume in maksimume"


            //in naredimo layout
            this.PerformChartLayout();


            this.OnFunctionRemoved(_function,
                _chartAreaAxisX, _chartAreaAxisY, _chartAreaChart);
        }
        public override void ClearChart()
        {
            base.ClearChart();

            this.multiFunctionChartLegend1.SeriesCollection.Clear();

            //in naredimo layout
            this.PerformChartLayout();
        }
        public ChartArea GetFunctionChartAreaAxisX(Function _function)
        {
            List<ChartArea> _chartAreas = new List<ChartArea>();

            foreach (Series _series in base.@SeriesCollection)
            {
                if (_series.Tag == _function)
                {
                    _chartAreas.Add(base.chart1.ChartAreas[_series.ChartArea]);
                }
            }

            foreach (ChartArea _chartArea in _chartAreas)
            {
                if (_chartArea.Tag is byte[])
                {
                    ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);

                    if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_X)
                    {
                        return _chartArea;
                    }
                }
            }


            return null;
        }
        public ChartArea GetFunctionChartAreaAxisY(Function _function)
        {
            List<ChartArea> _chartAreas = new List<ChartArea>();

            foreach (Series _series in base.@SeriesCollection)
            {
                if (_series.Tag == _function)
                {
                    _chartAreas.Add(base.chart1.ChartAreas[_series.ChartArea]);
                }
            }

            foreach (ChartArea _chartArea in _chartAreas)
            {
                if (_chartArea.Tag is byte[])
                {
                    ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);

                    if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_Y)
                    {
                        return _chartArea;
                    }
                }
            }


            return null;
        }
        public ChartArea GetFunctionChartAreaChart(Function _function)
        {
            List<ChartArea> _chartAreas = new List<ChartArea>();

            foreach (Series _series in base.@SeriesCollection)
            {
                if (_series.Tag == _function)
                {
                    _chartAreas.Add(base.chart1.ChartAreas[_series.ChartArea]);
                }
            }

            foreach (ChartArea _chartArea in _chartAreas)
            {
                if (_chartArea.Tag is byte[])
                {
                    ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);

                    if (_chartAreaTagger.ChartAreaType == ChartAreaType.CHART)
                    {
                        return _chartArea;
                    }
                }
            }


            return null;
        }
        //vrne array [ChartAreaAxisX, ChartAreaAxisY, ChartAreaChart]
        public ChartArea[][] GetAvailableChartAreas()
        {
            List<ChartArea[]> _totalChartAreas = new List<ChartArea[]>();

            foreach (ChartArea _chartAreaChart in base.chart1.ChartAreas)
            {
                if (_chartAreaChart.Tag is byte[])
                {
                    ChartAreaTagger _chartAreaChartTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartAreaChart.Tag);

                    if (_chartAreaChartTagger.ChartAreaType == ChartAreaType.CHART)
                    {
                        List<ChartArea> _internalChartAreas = new List<ChartArea>();

                        #region "axis X"
                        foreach (ChartArea _chartAreaAxisX in base.chart1.ChartAreas)
                        {
                            if (_chartAreaAxisX.Tag is byte[])
                            {
                                ChartAreaTagger _chartAreaAxisXTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartAreaAxisX.Tag);

                                if (_chartAreaAxisXTagger.ChartAreaType == ChartAreaType.AXIS_X)
                                {
                                    _internalChartAreas.Add(_chartAreaAxisX);
                                    break;
                                }
                            }
                        }
                        #endregion "axis X"

                        #region "axis Y"
                        foreach (ChartArea _chartAreaAxisY in base.chart1.ChartAreas)
                        {
                            if (_chartAreaAxisY.Tag is byte[])
                            {
                                ChartAreaTagger _chartAreaAxisYTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartAreaAxisY.Tag);

                                if (_chartAreaAxisYTagger.ChartAreaType == ChartAreaType.AXIS_Y)
                                {
                                    _internalChartAreas.Add(_chartAreaAxisY);
                                    break;
                                }
                            }
                        }
                        #endregion "axis Y"

                        //chart
                        _internalChartAreas.Add(_chartAreaChart);


                        _totalChartAreas.Add(_internalChartAreas.ToArray());
                    }
                }
            }

            return _totalChartAreas.ToArray();
        }


        private float LegendPositionYPercentage
        {
            get
            {
                if (this.multiFunctionChartLegend1 != null)
                {
                    float _float = ((float)this.multiFunctionChartLegend1.Location.Y / (float)base.chart1.Height) * 100f;
                    return _float;
                }
                else
                {
                    return 0f;
                }
            }
        }
        private float LegendRightSideClearancePercentage
        {
            get
            {
                float _float = 0f;
                if (!this.legendOverChart)
                {
                    //poskrbimo, da se legenda in graf ne prekrivata
                    _float = ((float)this.legendRightSideClearance / (float)base.chart1.Width) * 100f;
                    _float += LEGEND_TO_CHART_CLEARANCE_PERCENTAGE;
                }

                return _float;
            }
        }

        private RectangleF ChartPosition
        {
            get
            {
                return new RectangleF(
                    0f,
                    0f,
                    100f,
                    100f);
            }
        }
        private RectangleF ChartInnerPlotPosition
        {
            get
            {
                return new RectangleF(
                    0f,
                    this.LegendPositionYPercentage,
                    100f - this.legendWidthPercentage - this.LegendRightSideClearancePercentage,
                    100f - this.LegendPositionYPercentage);
            }
        }

        private float TickMarkSizePercentageX
        {
            get
            {
                return (TICK_MARK_SIZE_PIXELS / (float)base.chart1.Width) * 100f;
            }
        }
        private float TickMarkSizePercentageY
        {
            get
            {
                return (TICK_MARK_SIZE_PIXELS / (float)base.chart1.Height) * 100f;
            }
        }



        private float GetAxisYLabelWidthPercentage(Graphics _graphics, ChartArea _chartArea)
        {
            float _width = 0;
            foreach (CustomLabel _customLabel in _chartArea.AxisY.CustomLabels)
            {
                SizeF _stringSize = _graphics.MeasureString(_customLabel.Text, _chartArea.AxisY.LabelStyle.Font);

                if (_stringSize.Width > _width)
                {
                    _width = _stringSize.Width;
                }
            }
            _width += 16; //pustimo malo lufta vmes

            return (_width / (float)base.chart1.Width) * 100f;
        }
        private float GetAxisXLabelHeightPercentage(ChartArea _chartArea)
        {
            return (_chartArea.AxisX.LabelStyle.Font.Height / (float)base.chart1.Height) * 100f;
        }

        private ChartArea GetBestSuitedChartArea(
            double _minX, double _maxX, double _maxDifferenceX_percentage,
            double _minY, double _maxY, double _maxDifferenceY_percentage,
            out ChartArea _suitableAxisX, out ChartArea _suitableAxisY)
        {
            List<ChartArea> _suitableAxisXtmp = new List<ChartArea>();
            List<ChartArea> _suitableAxisYtmp = new List<ChartArea>();


            ChartArea _suitableChartArea = null;
            foreach (ChartArea _chartArea in base.chart1.ChartAreas)
            {
                #region "preverjanje, če je ta area sploh kandidat za izračun"
                if (double.IsNaN(_chartArea.AxisX.Minimum))
                {
                    continue;
                }
                if (double.IsInfinity(_chartArea.AxisX.Maximum))
                {
                    continue;
                }
                if (double.IsNaN(_chartArea.AxisY.Minimum))
                {
                    continue;
                }
                if (double.IsInfinity(_chartArea.AxisY.Maximum))
                {
                    continue;
                }
                #endregion "preverjanje, če je ta area sploh kandidat za izračun"

                
                if (_chartArea.Tag is byte[])
                {
                    ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);


                    //SISTEM PRIMERJANJA:
                    //izračun procentne razlike lahko vrne veliko razliko v primeru,da delamo z majhnimi ciframi, 
                    //čeprav v obsegu grafa ni take velike razlike (primer: minimum 0 vs minimum 1, maksimum pa v obeh primerih 100)
                    //v takem primeru je smiselno preveriti še procentno razliko v obsegu grafa in drugo skrajno točko;
                    //če je razlika v obsegu sprejemljiva in druga skrajna točka tudi, je graf primeren za izbor

                    #region "preverjanje ustreznosti X"
                    if (Mathematics.GetPercentalDifference(_chartArea.AxisX.Minimum, _minX) > _maxDifferenceX_percentage)
                    {
                        if ((Mathematics.GetPercentalDifference(_chartArea.AxisX.Maximum - _chartArea.AxisX.Minimum, _maxX - _minX) > _maxDifferenceX_percentage)
                        || (Mathematics.GetPercentalDifference(_chartArea.AxisX.Maximum, _maxX) > _maxDifferenceX_percentage))
                        {
                            continue;
                        }
                    }
                    if (Mathematics.GetPercentalDifference(_chartArea.AxisX.Maximum, _maxX) > _maxDifferenceX_percentage)
                    {
                        if ((Mathematics.GetPercentalDifference(_chartArea.AxisX.Maximum - _chartArea.AxisX.Minimum, _maxX - _minX) > _maxDifferenceX_percentage)
                        || (Mathematics.GetPercentalDifference(_chartArea.AxisX.Minimum, _minX) > _maxDifferenceX_percentage))
                        {
                            continue;
                        }
                    }
                    #endregion "preverjanje ustreznosti X"

                    if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_X)
                    {
                        _suitableAxisXtmp.Add(_chartArea);
                    }


                    #region "preverjanje ustreznosti Y"
                    if (Mathematics.GetPercentalDifference(_chartArea.AxisY.Minimum, _minY) > _maxDifferenceY_percentage)
                    {
                        if ((Mathematics.GetPercentalDifference(_chartArea.AxisY.Maximum - _chartArea.AxisY.Minimum, _maxY - _minY) > _maxDifferenceY_percentage)
                        || (Mathematics.GetPercentalDifference(_chartArea.AxisY.Maximum, _maxY) > _maxDifferenceY_percentage))
                        {
                            continue;
                        }
                    }
                    if (Mathematics.GetPercentalDifference(_chartArea.AxisY.Maximum, _maxY) > _maxDifferenceY_percentage)
                    {
                        if ((Mathematics.GetPercentalDifference(_chartArea.AxisY.Maximum - _chartArea.AxisY.Minimum, _maxY - _minY) > _maxDifferenceY_percentage)
                        || (Mathematics.GetPercentalDifference(_chartArea.AxisY.Minimum, _minY) > _maxDifferenceY_percentage))
                        {
                            continue;
                        }
                    }
                    #endregion "preverjanje ustreznosti Y"

                    if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_Y)
                    {
                        _suitableAxisYtmp.Add(_chartArea);
                    }


                    if (_chartAreaTagger.ChartAreaType == ChartAreaType.CHART)
                    {
                        _suitableChartArea = _chartArea;
                    }
                }
            }


            if (_suitableAxisXtmp.Count > 0)
            {
                _suitableAxisX = _suitableAxisXtmp[0];
            }
            else
            {
                _suitableAxisX = null;
            }
            if (_suitableAxisYtmp.Count > 0)
            {
                _suitableAxisY = _suitableAxisYtmp[0];
            }
            else
            {
                _suitableAxisY = null;
            }


            return _suitableChartArea;
        }

        private void SetAxis(Axis _axis,
            double _min, double _max, double _interval_percentage,
            string _labelFormat, bool _usedForChart, bool _showAxisInPiValues, bool _showAxisLine)
        {
            base.SetAxis(_axis,
                _min, _max, _interval_percentage,
                _labelFormat, _showAxisInPiValues,
                base.ChartLabelsColor);


            _axis.MajorGrid.Enabled = false;


            if (_usedForChart)
            {
                _axis.LabelStyle.Enabled = false;
                _axis.MajorTickMark.Enabled = false;
                _axis.LineColor = Color.Transparent;
            }
            else
            {
                if (_showAxisLine)
                {
                    _axis.LineColor = base.ChartAxisColor;
                }
                else
                {
                    _axis.LineColor = Color.Transparent;
                }
            }
        }

        private int GetChartAreasCountX()
        {
            int _int = 0;

            foreach (ChartArea _chartArea in base.chart1.ChartAreas)
            {
                if (_chartArea.Tag is byte[])
                {
                    ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);

                    if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_X)
                    {
                        _int++;
                    }
                }
            }

            return _int;
        }
        private int GetChartAreasCountY()
        {
            int _int = 0;

            foreach (ChartArea _chartArea in base.chart1.ChartAreas)
            {
                if (_chartArea.Tag is byte[])
                {
                    ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);

                    if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_Y)
                    {
                        _int++;
                    }
                }
            }

            return _int;
        }


        private void PerformLegendLayout()
        {
            if (this.loaded)
            {
                if (this.multiFunctionChartLegend1 != null)
                {
                    int _legendHeight = base.chart1.Height - LEGEND_TO_CHART_HEIGHT_CORRECTION;
                    if (_legendHeight < 1)
                    {
                        _legendHeight = 1;
                    }
                    multiFunctionChartLegend1.MaxAllowedHeight = _legendHeight;


                    int _legendWidth = (int)((float)base.Width * (this.legendWidthPercentage / 100f));
                    int _legendX = base.Width - _legendWidth - this.legendRightSideClearance;

                    this.multiFunctionChartLegend1.Location = new Point(
                        _legendX,
                        this.multiFunctionChartLegend1.Location.Y);

                    this.multiFunctionChartLegend1.Width = _legendWidth;
                }
            }
        }
        private void PerformChartLayout()
        {
            if (this.loaded)
            {
                float _offsetX = 0;
                float _offsetY = 0;

                #region "zamaknemo osne aree za širino/višino labelov in izračunamo offset"
                foreach (ChartArea _chartArea in base.chart1.ChartAreas.Reverse())
                {
                    _chartArea.Position.FromRectangleF(this.ChartPosition);

                    if (_chartArea.Tag is byte[])
                    {
                        ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);

                        if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_X)
                        {
                            if (_chartArea.AxisX.MajorTickMark.Enabled)
                            {
                                _chartArea.AxisX.MajorTickMark.Size = this.TickMarkSizePercentageY;
                            }

                            float _axisXLabelHeight = this.GetAxisXLabelHeightPercentage(_chartArea) + this.TickMarkSizePercentageY;

                            RectangleF _chartInnerPlotPosition = this.ChartInnerPlotPosition;
                            _chartInnerPlotPosition.Y -= (_axisXLabelHeight + _offsetY);
                            _chartInnerPlotPosition.Height -= (_axisXLabelHeight + _offsetY);

                            //NOTE: ta hek je potreben, ker se čene chartarea ne zna deserializirat (izgleda, da je microsoftov bug), ker tukaj pride vrednost manjša od 0 (-0,89)
                            //to izmika x os, zato je zakometirano - treba je bolj točno ugotoviti, kaj je narobe
                            //if (_chartInnerPlotPosition.Y < 0f)
                            //{
                            //    _chartInnerPlotPosition.Y = 0f;
                            //}

                            _chartArea.InnerPlotPosition.FromRectangleF(_chartInnerPlotPosition);

                            _offsetY += _axisXLabelHeight;
                        }
                        else if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_Y)
                        {
                            if (_chartArea.AxisY.MajorTickMark.Enabled)
                            {
                                _chartArea.AxisY.MajorTickMark.Size = this.TickMarkSizePercentageX;
                            }

                            float _axisYLabelWidth = this.GetAxisYLabelWidthPercentage(graphics, _chartArea) + this.TickMarkSizePercentageX;

                            RectangleF _chartInnerPlotPosition = this.ChartInnerPlotPosition;
                            _chartInnerPlotPosition.X += (_axisYLabelWidth + _offsetX);
                            _chartInnerPlotPosition.Width -= (_axisYLabelWidth + _offsetX);
                            _chartArea.InnerPlotPosition.FromRectangleF(_chartInnerPlotPosition);


                            _offsetX += _axisYLabelWidth;
                        }
                    }
                }
                #endregion "zamaknemo osne aree za širino/višino labelov in izračunamo offset"

                #region "poravnamo aree med seboj, glede na offset"
                foreach (ChartArea _chartArea in base.chart1.ChartAreas)
                {
                    if (_chartArea.Tag is byte[])
                    {
                        ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);

                        if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_X)
                        {
                            RectangleF _chartInnerPlotPosition = _chartArea.InnerPlotPosition.ToRectangleF();
                            _chartInnerPlotPosition.X += _offsetX;
                            _chartInnerPlotPosition.Width -= _offsetX;
                            _chartArea.InnerPlotPosition.FromRectangleF(_chartInnerPlotPosition);
                        }
                        else if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_Y)
                        {
                            RectangleF _chartInnerPlotPosition = _chartArea.InnerPlotPosition.ToRectangleF();
                            _chartInnerPlotPosition.Height -= (_offsetY * 2);
                            _chartArea.InnerPlotPosition.FromRectangleF(_chartInnerPlotPosition);
                        }
                        else if (_chartAreaTagger.ChartAreaType == ChartAreaType.CHART)
                        {
                            _chartArea.Position.FromRectangleF(this.ChartPosition);

                            RectangleF _chartInnerPlotPosition = this.ChartInnerPlotPosition;
                            _chartInnerPlotPosition.X += _offsetX;
                            _chartInnerPlotPosition.Width -= _offsetX;
                            _chartInnerPlotPosition.Height -= (_offsetY * 2);
                            _chartArea.InnerPlotPosition.FromRectangleF(_chartInnerPlotPosition);
                        }
                    }
                    else if (_chartArea == base.BaseChartArea) //pozicioniramo tudi to, ker nam drži belo podlago
                    {
                        base.BaseChartArea.Position.FromRectangleF(this.ChartPosition);

                        RectangleF _chartInnerPlotPosition = this.ChartInnerPlotPosition;
                        _chartInnerPlotPosition.X += _offsetX;
                        _chartInnerPlotPosition.Width -= _offsetX;
                        _chartInnerPlotPosition.Height -= (_offsetY * 2);
                        base.BaseChartArea.InnerPlotPosition.FromRectangleF(_chartInnerPlotPosition);
                    }
                }
                #endregion "poravnamo aree med seboj, glede na offset"


                #region "po potrebi pobarvamo lable na axisih"
                int _chartAreasCountX = this.GetChartAreasCountX();
                int _chartAreasCountY = this.GetChartAreasCountY();


                foreach (ChartArea _chartArea in base.chart1.ChartAreas)
                {
                    if (_chartArea.Tag is byte[])
                    {
                        ChartAreaTagger _chartAreaTagger = BinarySerializer.Deserialize<ChartAreaTagger>((byte[])_chartArea.Tag);

                        if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_X)
                        {
                            if (_chartAreasCountX > 1)
                            {
                                _chartArea.AxisX.LabelStyle.ForeColor = _chartAreaTagger.ChartColor;
                            }
                            else
                            {
                                _chartArea.AxisX.LabelStyle.ForeColor = base.ChartLabelsColor;
                            }
                        }
                        else if (_chartAreaTagger.ChartAreaType == ChartAreaType.AXIS_Y)
                        {
                            if (_chartAreasCountY > 1)
                            {
                                _chartArea.AxisY.LabelStyle.ForeColor = _chartAreaTagger.ChartColor;
                            }
                            else
                            {
                                _chartArea.AxisY.LabelStyle.ForeColor = base.ChartLabelsColor;
                            }
                        }
                    }
                }
                #endregion "po potrebi pobarvamo lable na axisih"
            }


            if (base.@SeriesCollection.Count == 0)
            {
                base.BaseChartArea.Visible = false;
            }
        }

        private void multiFunctionChartLegend1_LegendContextMenuOpening(object sender, EventArgs e)
        {
            this.OnLegendContextMenuOpening(e);
        }
        protected virtual void OnLegendContextMenuOpening(EventArgs _eventArgs)
        {
            if (this.LegendContextMenuOpening != null)
            {
                this.LegendContextMenuOpening(
                    this,
                    _eventArgs);
            }
        }
        private void multiFunctionChartLegend1_LegendContextMenuClosing(object sender, EventArgs e)
        {
            this.OnLegendContextMenuClosing(e);
        }
        protected virtual void OnLegendContextMenuClosing(EventArgs _eventArgs)
        {
            if (this.LegendContextMenuClosing != null)
            {
                this.LegendContextMenuClosing(
                    this,
                    _eventArgs);
            }
        }
        private void multiFunctionChartLegend1_CustomLegendsSelectedChanged(object sender, CustomLegendEventArgs e)
        {
            this.OnLegendSelectedFunctionsChanged(e);
        }
        protected virtual void OnLegendSelectedFunctionsChanged(CustomLegendEventArgs e)
        {
            if (this.SelectedFunctionsChanged != null)
            {
                this.SelectedFunctionsChanged(
                    this,
                    e);
            }
        }
        private void multiFunctionChartLegend1_LegendItemToolTipShowing(object sender, CustomLegendItemEventArgs e)
        {
            this.OnLegendItemToolTipShowing(e);
        }
        protected virtual void OnLegendItemToolTipShowing(CustomLegendItemEventArgs e)
        {
            if (this.LegendItemToolTipShowing != null)
            {
                this.LegendItemToolTipShowing(
                    this,
                    e);
            }
        }

        protected virtual void OnFunctionAdded(Function _function, ChartArea _chartAreaAxisX, ChartArea _chartAreaAxisY, ChartArea _chartAreaChart)
        {
            if (this.FunctionAdded != null)
            {
                this.FunctionAdded(
                    this,
                    new MultiFunctionChartEventArgs(
                        _function,
                        _chartAreaAxisX,
                        _chartAreaAxisY,
                        _chartAreaChart));
            }
        }
        protected virtual void OnFunctionRemoved(Function _function, ChartArea _chartAreaAxisX, ChartArea _chartAreaAxisY, ChartArea _chartAreaChart)
        {
            if (this.FunctionRemoved != null)
            {
                this.FunctionRemoved(
                    this,
                    new MultiFunctionChartEventArgs(
                        _function,
                        _chartAreaAxisX,
                        _chartAreaAxisY,
                        _chartAreaChart));
            }
        }

    }


    public class MultiFunctionChartEventArgs : EventArgs
    {
        public MultiFunctionChartEventArgs(Function _function, ChartArea _chartAreaAxisX, ChartArea _chartAreaAxisY, ChartArea _chartAreaChart)
        {
            this.function = _function;
            this.chartAreaAxisX = _chartAreaAxisX;
            this.chartAreaAxisY = _chartAreaAxisY;
            this.chartAreaChart = _chartAreaChart;
        }


        private Function function;
        public Function Function
        {
            get { return function; }
        }

        private ChartArea chartAreaAxisX;
        public ChartArea ChartAreaAxisX
        {
            get { return chartAreaAxisX; }
        }

        private ChartArea chartAreaAxisY;
        public ChartArea ChartAreaAxisY
        {
            get { return chartAreaAxisY; }
        }

        private ChartArea chartAreaChart;
        public ChartArea ChartAreaChart
        {
            get { return chartAreaChart; }
        }

    }

}
