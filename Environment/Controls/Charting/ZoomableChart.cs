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
    public partial class ZoomableChart : ChartBase
    {
        public event EventHandler ChartContextMenuOpening;



        private int currentZoom = 0;



        protected ContextMenuStrip @ContextMenuStrip1
        {
            get { return this.contextMenuStrip1; }
        }
        protected MenuStrip @MenuStrip1
        {
            get { return this.menuStrip1; }
        }



        private bool userZoomEnabled = true;
        [DefaultValue(true)]
        public bool UserZoomEnabled
        {
            get { return userZoomEnabled; }
            set { userZoomEnabled = value; }
        }

        private double zoomIntervalPercentage = 10d;
        [DefaultValue(10d)]
        public double ZoomIntervalPercentage
        {
            get { return zoomIntervalPercentage; }
            set { zoomIntervalPercentage = value; }
        }

        private bool zoomAxisX = true;
        [DefaultValue(true)]
        public bool ZoomAxisX
        {
            get { return zoomAxisX; }
            set { zoomAxisX = value; }
        }

        private bool zoomAxisY = true;
        [DefaultValue(true)]
        public bool ZoomAxisY
        {
            get { return zoomAxisY; }
            set { zoomAxisY = value; }
        }



        public ZoomableChart()
        {
            InitializeComponent();


            base.chart1.MouseWheel
                += new MouseEventHandler(chart1_MouseWheel);
        }
        protected override void FormatChartArea(ChartArea _chartArea)
        {
            base.FormatChartArea(_chartArea);


            _chartArea.AxisX.ScrollBar.BackColor = Color.Gainsboro;
            _chartArea.AxisX.ScrollBar.ButtonColor = Color.Silver;
            _chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            _chartArea.AxisX.ScrollBar.LineColor = Color.SlateGray;
            _chartArea.AxisX.ScrollBar.Size = 15;
            _chartArea.AxisX.ScrollBar.IsPositionedInside = true;

            _chartArea.AxisY.ScrollBar.BackColor = Color.Gainsboro;
            _chartArea.AxisY.ScrollBar.ButtonColor = Color.Silver;
            _chartArea.AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            _chartArea.AxisY.ScrollBar.LineColor = Color.SlateGray;
            _chartArea.AxisY.ScrollBar.Size = 15;
            _chartArea.AxisY.ScrollBar.IsPositionedInside = true;
        }



        public virtual bool MergeMenuStripWith(MenuStrip _menuStrip)
        {
            return ToolStripManager.Merge(this.menuStrip1, _menuStrip);
        }



        protected override void SetAxis(Axis _axis, double _min, double _max, double _interval_percentage, string _labelFormat, bool _showAxisInPiValues, Color _labelsColor)
        {
            base.SetAxis(_axis, _min, _max, _interval_percentage, _labelFormat, _showAxisInPiValues, _labelsColor);


            _axis.ScaleView.Zoomable = true;
            _axis.ScaleView.SmallScrollSize = _axis.Interval / this.zoomIntervalPercentage;
            _axis.ScaleView.SmallScrollMinSize = _axis.Interval / this.zoomIntervalPercentage;
        }



        //zoom
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.userZoomEnabled)
            {
                if (e.Delta < 0)
                {
                    this.ZoomOut();
                }
                else if (e.Delta > 0)
                {
                    this.ZoomIn();
                }
            }
        }
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            base.chart1.Focus();

            if (this.userZoomEnabled)
            {
                if (e.Button == MouseButtons.Middle)
                {
                    this.ZoomReset();
                }
            }
        }
        //tukaj zamikamo grid in lable ob zoomu
        protected virtual void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            e.Axis.MajorGrid.IntervalOffset = -e.NewPosition;
            e.Axis.MajorTickMark.IntervalOffset = -e.NewPosition;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!this.userZoomEnabled)
            {
                e.Cancel = true;
            }


            if (!e.Cancel)
            {
                this.OnChartContextMenuOpening();
            }
        }
        private void toolStripMenuItem_ZoomIn_Click(object sender, EventArgs e)
        {
            this.ZoomIn();
        }
        private void toolStripMenuItem_View_ZoomIn_Click(object sender, EventArgs e)
        {
            this.ZoomIn();
        }
        private void toolStripMenuItem_ZoomOut_Click(object sender, EventArgs e)
        {
            this.ZoomOut();
        }
        private void toolStripMenuItem_View_ZoomOut_Click(object sender, EventArgs e)
        {
            this.ZoomOut();
        }
        private void toolStripMenuItem_ZoomReset_Click(object sender, EventArgs e)
        {
            this.ZoomReset();
        }
        private void toolStripMenuItem_View_ZoomReset_Click(object sender, EventArgs e)
        {
            this.ZoomReset();
        }

        private void SetZoom()
        {
            foreach (ChartArea _chartArea in base.ChartAreaCollection)
            {
                if (this.zoomAxisX)
                {
                    #region
                    double _zoomX = (double)(this.currentZoom * _chartArea.AxisX.Interval);
                    _zoomX /= this.zoomIntervalPercentage;

                    if (_chartArea.AxisX.Minimum + _zoomX >= _chartArea.AxisX.Maximum - _zoomX)
                    {
                        this.currentZoom--;
                        this.toolStripMenuItem_ZoomIn.Enabled = false;

                        return;
                    }
                    else
                    {
                        _chartArea.AxisX.ScaleView.Zoom(_chartArea.AxisX.Minimum + _zoomX, _chartArea.AxisX.Maximum - _zoomX);
                        _chartArea.AxisX.MajorGrid.IntervalOffset = -_zoomX;
                        _chartArea.AxisX.MajorTickMark.IntervalOffset = -_zoomX;
                    }
                    #endregion
                }

                if (this.zoomAxisY)
                {
                    #region
                    double _zoomY = (double)(this.currentZoom * _chartArea.AxisY.Interval);
                    _zoomY /= this.zoomIntervalPercentage;

                    if (_chartArea.AxisY.Minimum + _zoomY >= _chartArea.AxisY.Maximum - _zoomY)
                    {
                        this.currentZoom--;
                        this.toolStripMenuItem_ZoomIn.Enabled = false;

                        return;
                    }
                    else
                    {
                        _chartArea.AxisY.ScaleView.Zoom(_chartArea.AxisY.Minimum + _zoomY, _chartArea.AxisY.Maximum - _zoomY);
                        _chartArea.AxisY.MajorGrid.IntervalOffset = -_zoomY;
                        _chartArea.AxisY.MajorTickMark.IntervalOffset = -_zoomY;
                    }
                    #endregion
                }
            }
        }

        private void ZoomIn()
        {
            this.currentZoom += 1;

            this.toolStripMenuItem_ZoomOut.Enabled = true;
            this.toolStripMenuItem_View_ZoomOut.Enabled = true;
            this.toolStripMenuItem_ZoomReset.Enabled = true;
            this.toolStripMenuItem_View_ZoomReset.Enabled = true;

            this.SetZoom();
        }
        private void ZoomOut()
        {
            this.toolStripMenuItem_ZoomIn.Enabled = true;

            this.currentZoom -= 1;
            if (this.currentZoom <= 0)
            {
                this.currentZoom = 0;

                this.toolStripMenuItem_ZoomOut.Enabled = false;
                this.toolStripMenuItem_View_ZoomOut.Enabled = false;
                this.toolStripMenuItem_ZoomReset.Enabled = false;
                this.toolStripMenuItem_View_ZoomReset.Enabled = false;

                foreach (ChartArea _chartArea in base.ChartAreaCollection)
                {
                    _chartArea.AxisX.ScaleView.ZoomReset();
                    _chartArea.AxisY.ScaleView.ZoomReset();
                }
            }
            else
            {
                this.SetZoom();
            }
        }
        private void ZoomReset()
        {
            this.currentZoom = 0;

            this.toolStripMenuItem_ZoomOut.Enabled = false;
            this.toolStripMenuItem_View_ZoomOut.Enabled = false;
            this.toolStripMenuItem_ZoomReset.Enabled = false;
            this.toolStripMenuItem_View_ZoomReset.Enabled = false;

            this.SetZoom();
        }

        private void OnChartContextMenuOpening()
        {
            if (this.ChartContextMenuOpening != null)
            {
                this.ChartContextMenuOpening(
                    this,
                    EventArgs.Empty);
            }
        }

    }
}
