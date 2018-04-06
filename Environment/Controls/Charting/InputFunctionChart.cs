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
    [DefaultEvent("NewFunctionGenerated")]
    public partial class InputFunctionChart : FunctionChart
    {
        //to se zgodi ob vsakem premiku bilo katere točke
        private event EventHandler<PointMovedEventArgs> pointMoved;
        public event EventHandler<PointMovedEventArgs> PointMoved
        {
            add { pointMoved += value; }
            remove { pointMoved -= value; }
        }

        //to se zgodi na koncu, ko spustimo miš in se generira nova funkcija
        private event EventHandler<FunctionGeneratedEventArgs> newFunctionGenerated;
        public event EventHandler<FunctionGeneratedEventArgs> NewFunctionGenerated
        {
            add { newFunctionGenerated += value; }
            remove { newFunctionGenerated -= value; }
        }

        private event EventHandler<PointMoveAllowedEventArgs> pointMoveAllowed;
        public event EventHandler<PointMoveAllowedEventArgs> PointMoveAllowed
        {
            add { pointMoveAllowed += value; }
            remove { pointMoveAllowed -= value; }
        }

        private event EventHandler<PointSelectionChangedEventArgs> pointSelectionChanged;
        public event EventHandler<PointSelectionChangedEventArgs> PointSelectionChanged
        {
            add { pointSelectionChanged += value; }
            remove { pointSelectionChanged -= value; }
        }

        private event EventHandler<PointDeleteAllowedEventArgs> pointDeleteAllowed;
        public event EventHandler<PointDeleteAllowedEventArgs> PointDeleteAllowed
        {
            add { pointDeleteAllowed += value; }
            remove { pointDeleteAllowed -= value; }
        }

        private event EventHandler<PointAddAllowedEventArgs> pointAddAllowed;
        public event EventHandler<PointAddAllowedEventArgs> PointAddAllowed
        {
            add { pointAddAllowed += value; }
            remove { pointAddAllowed -= value; }
        }



        private DataPoint selectedDataPoint = null; //drži trenutno izbran data point
        private Point mouseDownLocation = Point.Empty; //tukaj si zapomnimo pozicijo MouseDown



        //ali se lahko ob vleku točke čez mejo grafa, ta meja premakne v smer vleka
        private bool chartBordersMovableX = true;
        [DefaultValue(true)]
        public bool ChartBordersMovableX
        {
            get { return chartBordersMovableX; }
            set { chartBordersMovableX = value; }
        }

        //premik meje grafa v procentih POVPREČNE vrednosti grafa (max-min)/2
        private double chartBorderChangePercentageX = 1d;
        [DefaultValue(1d)]
        public double ChartBorderChangePercentageX
        {
            get { return chartBorderChangePercentageX; }
            set { chartBorderChangePercentageX = value; }
        }

        //ali se lahko ob vleku točke čez mejo grafa, ta meja premakne v smer vleka
        private bool chartBordersMovableY = true;
        [DefaultValue(true)]
        public bool ChartBordersMovableY
        {
            get { return chartBordersMovableY; }
            set { chartBordersMovableY = value; }
        }

        //premik meje grava v procentih POVPREČNE vrednosti grafa (max-min)/2
        private double chartBorderChangePercentageY = 1d;
        [DefaultValue(1d)]
        public double ChartBorderChangePercentageY
        {
            get { return chartBorderChangePercentageY; }
            set { chartBorderChangePercentageY = value; }
        }


        public XY SelectedPoint
        {
            get
            {
                if (this.selectedDataPoint != null)
                {
                    return (XY)this.selectedDataPoint.Tag;
                }
                else
                {
                    return XY.Empty;
                }
            }
        }
        public Function SelectedFunction
        {
            get
            {
                if (this.selectedDataPoint != null)
                {
                    Series _series = base.GetSeriesByDataPoint(this.selectedDataPoint);
                    return (Function)_series.Tag;
                }
                else
                {
                    return null;
                }
            }
        }



        public InputFunctionChart()
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                #region "Menus & Toolbars"
                if (!ToolStripManager.Merge(this.contextMenuStrip2, base.ContextMenuStrip1))
                {
                    throw new Exception();
                }
                #endregion "Menus & Toolbars"


                this.menuStrip2.Visible = false;
                this.toolStrip1.Visible = false;
            }
        }



        public override bool MergeMenuStripWith(MenuStrip _menuStrip)
        {
            bool _bool;

            _bool = ToolStripManager.Merge(base.MenuStrip1, _menuStrip);
            _bool &= ToolStripManager.Merge(this.menuStrip2, _menuStrip);

            return _bool;
        }
        public bool MergeToolStripWith(ToolStrip _toolStrip)
        {
            return ToolStripManager.Merge(this.toolStrip1, _toolStrip);
        }



        public void AddPoint(Function _function, XY _newXY)
        {
            Series _series = base.GetSeriesByFunction(_function);

            if (_series != null)
            {
                this.AddPoint(_series, _newXY);
            }
            else
            {
                throw new Exception();
            }
        }
        public void DeletePoint(Function _function, XY _xy)
        {
            DataPoint _dataPoint = base.GetDataPointByXY(_function, _xy);

            if (_dataPoint != null)
            {
                this.DeletePoint(_dataPoint);
            }
            else
            {
                throw new Exception();
            }
        }



        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            this.ClearSelection();


            DataPoint _dataPoint = this.GetDataPointAt(e.X, e.Y);

            if (_dataPoint != null)
            {
                this.selectedDataPoint = _dataPoint;
                this.mouseDownLocation = e.Location;


                this.OnPointSelectionChanged(this.selectedDataPoint);


                #region "preverimo, če je dovoljen premik po koordinatah"
                bool _moveAllowedX;
                bool _moveAllowedY;
                this.OnPointMoveAllowed(_dataPoint, out _moveAllowedX, out _moveAllowedY);
                if ((_moveAllowedX)
                    || (_moveAllowedY))
                {
                    base.chart1.Cursor = Cursors.Hand;
                }
                else
                {
                    base.chart1.Cursor = Cursors.Default;
                }
                #endregion "preverimo, če je dovoljen premik po koordinatah"

                #region "preverimo, če je dovoljen delete"
                bool _deleteAllowed;
                this.OnPointDeleteAllowed(_dataPoint, out _deleteAllowed);
                this.toolStripMenuItem_DeletePoint.Enabled = _deleteAllowed;
                this.toolStripMenuItem_Edit_DeletePoint.Enabled = _deleteAllowed;
                this.toolStripButton_DeletePoint.Enabled = _deleteAllowed;
                #endregion "preverimo, če je dovoljen delete"

                #region "preverimo, če je dovoljen add"
                bool _addAllowed;
                double _chartX = base.BaseAxisX.PixelPositionToValue(e.X);
                double _chartY = base.BaseAxisY.PixelPositionToValue(e.Y);
                //ne moremo dodat na obstoječ X
                if (_chartX == _dataPoint.XValue)
                {
                    _addAllowed = false;
                }
                else
                {
                    this.OnPointAddAllowed(
                        base.GetSeriesByDataPoint(_dataPoint),
                        new XY(_chartX, _chartY),
                        out _addAllowed);
                }
                this.toolStripMenuItem_AddPointHere.Enabled = _addAllowed;
                this.toolStripMenuItem_Edit_AddPointHere.Enabled = _addAllowed;
                this.toolStripButton_AddPointHere.Enabled = _addAllowed;
                #endregion "preverimo, če je dovoljen add"
            }
        }
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectedDataPoint != null)
                {
                    #region "preverimo, če je dovoljen premik po koordinatah"
                    bool _moveAllowedX;
                    bool _moveAllowedY;
                    this.OnPointMoveAllowed(this.selectedDataPoint, out _moveAllowedX, out _moveAllowedY);
                    #endregion "preverimo, če je dovoljen premik po koordinatah"


                    if ((_moveAllowedX) || (_moveAllowedY))
                    {
                        double _oldX = selectedDataPoint.XValue;
                        double _oldY = selectedDataPoint.YValues[0];


                        #region "poskrbimo, da koordinate od miši niso izven charta"
                        int _mouseX = e.X;
                        if (_mouseX < 0)
                        {
                            _mouseX = 0;
                        }
                        else if (_mouseX > base.chart1.Size.Width - 10) //upoštevamo malo manjše območje
                        {
                            _mouseX = base.chart1.Size.Width - 10;
                        }

                        int _mouseY = e.Y;
                        if (_mouseY < 0)
                        {
                            _mouseY = 0;
                        }
                        else if (_mouseY > base.chart1.Size.Height - 10) //upoštevamo malo manjše območje
                        {
                            _mouseY = base.chart1.Size.Height - 10;
                        }
                        #endregion "poskrbimo, da koordinate od miši niso izven charta"

                        #region "dobimo koordinate na grafu"
                        double _chartX = base.BaseAxisX.PixelPositionToValue(_mouseX);
                        _chartX = Math.Min(_chartX, base.BaseAxisX.Maximum);
                        _chartX = Math.Max(_chartX, base.BaseAxisX.Minimum);

                        double _chartY = base.BaseAxisY.PixelPositionToValue(_mouseY);
                        _chartY = Math.Min(_chartY, base.BaseAxisY.Maximum);
                        _chartY = Math.Max(_chartY, base.BaseAxisY.Minimum);
                        #endregion "dobimo koordinate na grafu"

                        //Console.Debug.WriteLine("X: " + _chartX);
                        //Console.Debug.WriteLine("Y: " + _chartY);


                        double _newX = _chartX;
                        double _newY = _chartY;


                        #region "če se je zgodil premik..."
                        bool _selectedDataPointHasMoved = false;

                        if (_moveAllowedX)
                        {
                            if (_newX != _oldX)
                            {
                                this.selectedDataPoint.XValue = _newX;
                                _selectedDataPointHasMoved = true;
                            }
                        }
                        if (_moveAllowedY)
                        {
                            if (_newY != _oldY)
                            {
                                this.selectedDataPoint.YValues[0] = _newY;
                                _selectedDataPointHasMoved = true;
                            }
                        }

                        if (_selectedDataPointHasMoved)
                        {
                            #region "Po potrebi spremenimo meje grafa"
                            Series _series = base.GetSeriesByDataPoint(this.selectedDataPoint);

                            if (this.chartBordersMovableX)
                            {
                                #region
                                double _maxX = base.BaseAxisX.Maximum;
                                double _minX = base.BaseAxisX.Minimum;
                                double _averageX = Math.Abs(_maxX - _minX) / 2d;
                                double _changePercentage = this.chartBorderChangePercentageX / 100d;

                                //bool _xShouldChange = false;

                                if (_newX == _maxX)
                                {
                                    base.SetAxis(base.BaseAxisX,
                                        _minX, _maxX + (_averageX * _changePercentage), base.ChartGridIntervalPercentage,
                                        base.AxisLabelsFormat,
                                        base.ShowAxisXInPiValues,
                                        base.ChartLabelsColor);

                                    this.selectedDataPoint.XValue = base.BaseAxisX.Maximum;

                                    //_xShouldChange = true;
                                }
                                if (_newX == _minX)
                                {
                                    base.SetAxis(base.BaseAxisX,
                                        _minX - (_averageX * _changePercentage), _maxX, base.ChartGridIntervalPercentage,
                                        base.AxisLabelsFormat,
                                        base.ShowAxisXInPiValues,
                                        base.ChartLabelsColor);

                                    this.selectedDataPoint.XValue = base.BaseAxisX.Minimum;

                                    //_xShouldChange = true;
                                }

                                //NOTE: zakaj bi to sploh imel?!
                                //if (_xShouldChange)
                                //{
                                //    base.SetAxis(base.BaseAxisX,
                                //        base.GetSeriesMinX(_series), base.GetSeriesMaxX(_series), base.ChartGridIntervalPercentage,
                                //        base.AxisLabelsFormat,
                                //        base.ShowAxisXInPiValues);
                                //}
                                #endregion
                            }
                            if (this.chartBordersMovableY)
                            {
                                #region
                                double _maxY = base.BaseAxisY.Maximum;
                                double _minY = base.BaseAxisY.Minimum;
                                double _averageY = Math.Abs(_maxY - _minY) / 2d;
                                double _changePercentage = this.chartBorderChangePercentageY / 100d;


                                //bool _yShouldChange = false;

                                if (_newY == _maxY)
                                {
                                    base.SetAxis(base.BaseAxisY,
                                        _minY, _maxY + (_averageY * _changePercentage), base.ChartGridIntervalPercentage,
                                        base.AxisLabelsFormat,
                                        base.ShowAxisXInPiValues,
                                        base.ChartLabelsColor);

                                    this.selectedDataPoint.YValues[0] = base.BaseAxisY.Maximum;

                                    //_yShouldChange = true;
                                }
                                if (_newY == _minY)
                                {
                                    base.SetAxis(base.BaseAxisY,
                                        _minY - (_averageY * _changePercentage), _maxY, base.ChartGridIntervalPercentage,
                                        base.AxisLabelsFormat,
                                        base.ShowAxisXInPiValues,
                                        base.ChartLabelsColor);

                                    this.selectedDataPoint.YValues[0] = base.BaseAxisY.Minimum;

                                    //_yShouldChange = true;
                                }

                                //NOTE: zakaj bi to sploh imel?!
                                //if (_yShouldChange)
                                //{
                                //    base.SetAxis(base.BaseAxisY,
                                //        base.GetSeriesMinY(_series), base.GetSeriesMaxY(_series), base.ChartGridIntervalPercentage,
                                //        base.AxisLabelsFormat,
                                //        base.ShowAxisXInPiValues);
                                //}
                                #endregion
                            }
                            #endregion "Po potrebi spremenimo meje grafa"


                            XY _xy = (XY)this.selectedDataPoint.Tag;


                            if (_moveAllowedX)
                            {
                                _xy.X = _newX;
                            }
                            if (_moveAllowedY)
                            {
                                _xy.Y = _newY;
                            }


                            base.chart1.Invalidate();
                            base.chart1.Update();


                            //in dvignemo event
                            this.OnPointMoved(this.selectedDataPoint, _xy);


                            //Console.WriteLine(_xy.ToString());
                        }
                        #endregion "če se je zgodil premik..."
                    }
                }
            }
            else if (e.Button == MouseButtons.None)
            {
                DataPoint _dataPoint = this.GetDataPointAt(e.X, e.Y);

                if (_dataPoint != null)
                {
                    #region "preverimo, če je dovoljen premik po koordinatah"
                    bool _moveAllowedX;
                    bool _moveAllowedY;
                    this.OnPointMoveAllowed(_dataPoint, out _moveAllowedX, out _moveAllowedY);
                    if ((_moveAllowedX)
                        || (_moveAllowedY))
                    {
                        base.chart1.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        base.chart1.Cursor = Cursors.Default;
                    }
                    #endregion "preverimo, če je dovoljen premik po koordinatah"
                }
                else
                {
                    base.chart1.Cursor = Cursors.Default;
                }
            }
        }
        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Location != this.mouseDownLocation)
                {
                    if (this.selectedDataPoint != null)
                    {
                        Series _series = base.GetSeriesByDataPoint(this.selectedDataPoint);

                        List<XY> _list = new List<XY>();
                        foreach (DataPoint _dataPoint in _series.Points)
                        {
                            XY _xy = new XY(
                                _dataPoint.XValue,
                                _dataPoint.YValues[0]);
                            _list.Add(_xy);
                        }

                        Function _function = Function.FromPoints(_list);
                        _series.Tag = _function;

                        this.OnNewFunctionGenerated(_series, _function);
                    }
                }
            }
        }

        private void toolStripMenuItem_AddPointHere_Click(object sender, EventArgs e)
        {
            this.AddPointHere();
        }
        private void toolStripMenuItem_Edit_AddPointHere_Click(object sender, EventArgs e)
        {
            this.AddPointHere();
        }
        private void toolStripMenuItem_DeletePoint_Click(object sender, EventArgs e)
        {
            this.DeletePoint();
        }
        private void toolStripMenuItem_Edit_DeletePoint_Click(object sender, EventArgs e)
        {
            this.DeletePoint();
        }
        private void toolStripButton_AddPointHere_Click(object sender, EventArgs e)
        {
            this.AddPointHere();
        }
        private void toolStripButton_DeletePoint_Click(object sender, EventArgs e)
        {
            this.DeletePoint();
        }


        private void AddPointHere()
        {
            Series _series = base.GetSeriesByDataPoint(this.selectedDataPoint);
            XY _newXY = new XY(
                base.BaseAxisX.PixelPositionToValue(mouseDownLocation.X),
                base.BaseAxisY.PixelPositionToValue(mouseDownLocation.Y));

            this.AddPoint(_series, _newXY);
        }
        private void DeletePoint()
        {
            DialogResult _dialogResult = MessageBox.Show(
                this,
                "Are you sure, you want to remove the selected point?",
                "Confirm point remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            if (_dialogResult == DialogResult.Yes)
            {
                this.DeletePoint(this.selectedDataPoint);
            }
            else
            {
                this.ClearSelection();
            }
        }

        private DataPoint GetDataPointAt(int _x, int _y)
        {
            //System.Diagnostics.Debug.WriteLine("X: " + _double + "  Y: " + _y);

            DataPoint _hitDataPoint = null;


            HitTestResult[] _hitTestResults = base.chart1.HitTest(_x, _y, false, new ChartElementType[] { ChartElementType.DataPoint });
            foreach (HitTestResult _hitTestResult in _hitTestResults)
            {
                if (_hitTestResult.ChartElementType == ChartElementType.DataPoint)
                {
                    DataPoint _dataPoint = (DataPoint)_hitTestResult.Object;

                    //System.Diagnostics.Debug.WriteLine("dataX: " + _dataPoint.XValue + "  dataY: " + _dataPoint.YValues[0]);

                    _hitDataPoint = _dataPoint;
                    break;
                }
            }


            return _hitDataPoint;
        }

        private void ClearSelection()
        {
            this.selectedDataPoint = null;
            this.mouseDownLocation = Point.Empty;

            this.toolStripMenuItem_DeletePoint.Enabled = false;
            this.toolStripMenuItem_Edit_DeletePoint.Enabled = false;
            this.toolStripButton_DeletePoint.Enabled = false;
            this.toolStripMenuItem_AddPointHere.Enabled = false;
            this.toolStripMenuItem_Edit_AddPointHere.Enabled = false;
            this.toolStripButton_AddPointHere.Enabled = false;

            base.chart1.Cursor = Cursors.Default;


            this.OnPointSelectionChanged(null);
        }

        private void AddPoint(Series _series, XY _newXY)
        {
            ChartArea _chartArea = base.GetChartAreaBySeries(_series);

            List<XY> _points = new List<XY>();
            foreach (XY _xy in (Function)_series.Tag)
            {
                _points.Add(_xy);
            }
            _points.Add(_newXY);


            Function _function = Function.FromPoints(_points);
            _series.Points.Clear();
            foreach (XY _xy in _function)
            {
                base.AddDataPoint(_chartArea, _series,
                    _xy.X,
                    null, _series.Color, null,
                    _xy,
                    _xy.Y);
            }
            _series.Tag = _function;


            this.OnNewFunctionGenerated(_series, _function);

            this.ClearSelection();
        }
        private void DeletePoint(DataPoint _dataPoint)
        {
            Series _series = base.GetSeriesByDataPoint(_dataPoint);
            _series.Points.Remove(_dataPoint);


            List<XY> _list = new List<XY>();
            foreach (DataPoint _dataPointTmp in _series.Points)
            {
                XY _xy = new XY(
                    _dataPointTmp.XValue,
                    _dataPointTmp.YValues[0]);
                _list.Add(_xy);
            }

            Function _function = Function.FromPoints(_list);
            _series.Tag = _function;


            this.OnNewFunctionGenerated(_series, _function);

            this.ClearSelection();
        }



        protected virtual void OnPointMoved(DataPoint _dataPoint, XY _newXY)
        {
            if (this.pointMoved != null)
            {
                Series _series = base.GetSeriesByDataPoint(_dataPoint);
                Function _function = (Function)_series.Tag;

                this.pointMoved(this, new PointMovedEventArgs(/*_dataPoint, */_function, _newXY));
            }
        }
        protected virtual void OnPointSelectionChanged(DataPoint _dataPoint)
        {
            if (this.pointSelectionChanged != null)
            {
                if (_dataPoint != null)
                {
                    Series _series = base.GetSeriesByDataPoint(_dataPoint);
                    Function _function = (Function)_series.Tag;
                    XY _xy = (XY)_dataPoint.Tag;

                    this.pointSelectionChanged(this, new PointSelectionChangedEventArgs(/*_dataPoint, */_function, _xy));
                }
                else
                {
                    this.pointSelectionChanged(this, new PointSelectionChangedEventArgs(/*_dataPoint, */null, XY.Empty));
                }
            }
        }
        protected virtual void OnNewFunctionGenerated(Series _series, Function _function)
        {
            if (this.newFunctionGenerated != null)
            {
                this.newFunctionGenerated(this, new FunctionGeneratedEventArgs(_function));
            }
        }
        protected virtual void OnPointMoveAllowed(DataPoint _dataPoint, out bool _moveAllowedX, out bool _moveAllowedY)
        {
            if (this.pointMoveAllowed != null)
            {
                Series _series = base.GetSeriesByDataPoint(_dataPoint);
                Function _function = (Function)_series.Tag;
                XY _xy = (XY)_dataPoint.Tag;

                PointMoveAllowedEventArgs _pointMoveAllowedEventArgs = new PointMoveAllowedEventArgs(_function, _xy);

                this.pointMoveAllowed(this, _pointMoveAllowedEventArgs);

                _moveAllowedX = _pointMoveAllowedEventArgs.MoveAllowedX;
                _moveAllowedY = _pointMoveAllowedEventArgs.MoveAllowedY;
            }
            else
            {
                _moveAllowedX = true;
                _moveAllowedY = true;
            }
        }
        protected virtual void OnPointAddAllowed(Series _series, XY _newXY, out bool _addAllowed)
        {
            if (this.pointAddAllowed != null)
            {
                Function _function = (Function)_series.Tag;

                PointAddAllowedEventArgs _pointAddAllowedEventArgs = new PointAddAllowedEventArgs(_function, _newXY);

                this.pointAddAllowed(this, _pointAddAllowedEventArgs);

                _addAllowed = _pointAddAllowedEventArgs.AddAllowed;
            }
            else
            {
                _addAllowed = true;
            }
        }
        protected virtual void OnPointDeleteAllowed(DataPoint _dataPoint, out bool _deleteAllowed)
        {
            if (this.pointDeleteAllowed != null)
            {
                Series _series = base.GetSeriesByDataPoint(_dataPoint);
                Function _function = (Function)_series.Tag;
                XY _xy = (XY)_dataPoint.Tag;

                PointDeleteAllowedEventArgs _pointDeleteAllowedEventArgs = new PointDeleteAllowedEventArgs(_function, _xy);

                this.pointDeleteAllowed(this, _pointDeleteAllowedEventArgs);

                _deleteAllowed = _pointDeleteAllowedEventArgs.DeleteAllowed;
            }
            else
            {
                _deleteAllowed = true;
            }
        }

    }


    public class PointMoveAllowedEventArgs : EventArgs
    {
        private Function function;
        public Function @Function
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return function; }
        }

        private XY xy;
        public XY @XY
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return xy; }
        }

        private bool moveAllowedX = true;
        public bool MoveAllowedX
        {
            get { return moveAllowedX; }
            set { moveAllowedX = value; }
        }

        private bool moveAllowedY = true;
        public bool MoveAllowedY
        {
            get { return moveAllowedY; }
            set { moveAllowedY = value; }
        }


        internal PointMoveAllowedEventArgs(Function _function, XY _xy)
        {
            this.function = _function;
            this.xy = _xy;
        }
    }


    public class PointAddAllowedEventArgs : EventArgs
    {
        private Function function;
        public Function @Function
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return function; }
        }

        private XY newXY;
        public XY NewXY
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return newXY; }
        }

        private bool addAllowed = true;
        public bool AddAllowed
        {
            get { return addAllowed; }
            set { addAllowed = value; }
        }


        internal PointAddAllowedEventArgs(Function _function, XY _newXY)
        {
            this.function = _function;
            this.newXY = _newXY;
        }
    }


    public class PointDeleteAllowedEventArgs : EventArgs
    {
        private Function function;
        public Function @Function
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return function; }
        }

        private XY xy;
        public XY @XY
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return xy; }
        }

        private bool deleteAllowed = true;
        public bool DeleteAllowed
        {
            get { return deleteAllowed; }
            set { deleteAllowed = value; }
        }


        internal PointDeleteAllowedEventArgs(Function _function, XY _xy)
        {
            this.function = _function;
            this.xy = _xy;
        }
    }


    public class PointMovedEventArgs : EventArgs
    {
        //private DataPoint dataPoint;
        //public DataPoint DataPoint
        //{
        //    [System.Diagnostics.DebuggerStepThrough()]
        //    get { return dataPoint; }
        //}

        private Function function;
        public Function @Function
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return function; }
        }

        private XY newXY;
        public XY NewXY
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return newXY; }
        }


        internal PointMovedEventArgs(/*DataPoint _dataPoint, */Function _function, XY _newXY)
        {
            //this.dataPoint = _dataPoint;
            this.function = _function;
            this.newXY = _newXY;
        }
    }


    public class PointSelectionChangedEventArgs : EventArgs
    {
        //private DataPoint dataPoint;
        //public DataPoint DataPoint
        //{
        //    [System.Diagnostics.DebuggerStepThrough()]
        //    get { return dataPoint; }
        //}

        private Function function;
        public Function @Function
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return function; }
        }

        private XY xy;
        public XY @XY
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return xy; }
        }


        internal PointSelectionChangedEventArgs(/*DataPoint _dataPoint, */Function _function, XY _xy)
        {
            //this.dataPoint = _dataPoint;
            this.function = _function;
            this.xy = _xy;
        }
    }


    public class FunctionGeneratedEventArgs : EventArgs
    {
        private Function newFunction;
        public Function NewFunction
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return newFunction; }
        }


        internal FunctionGeneratedEventArgs(Function _newFunction)
        {
            this.newFunction = _newFunction;
        }
    }

}
