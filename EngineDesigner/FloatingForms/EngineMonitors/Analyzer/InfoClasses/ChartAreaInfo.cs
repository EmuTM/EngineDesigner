using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using System.Drawing;
using EngineDesigner.Machine;
using EngineDesigner.Common;
using System.Runtime.Serialization;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    internal class ChartAreaInfo
    {
        [DataMember]
        private ChartArea chartAreaX = null;
        public ChartArea ChartAreaX
        {
            get { return chartAreaX; }
        }

        [DataMember]
        private ChartArea chartAreaY = null;
        public ChartArea ChartAreaY
        {
            get { return chartAreaY; }
        }

        [DataMember]
        private ChartArea chartAreaChart = null;
        public ChartArea ChartAreaChart
        {
            get { return chartAreaChart; }
        }

        public bool ForceNewChartArea
        {
            get
            {
                if ((this.chartAreaX == null)
                    || (this.chartAreaX == null)
                    || (this.chartAreaX == null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public ChartAreaInfo()
            : this(null, null, null)
        {
        }
        public ChartAreaInfo(ChartArea _chartAreaX, ChartArea _chartAreaY, ChartArea _chartAreaChart)
        {
            this.chartAreaX = _chartAreaX;
            this.chartAreaY = _chartAreaY;
            this.chartAreaChart = _chartAreaChart;
        }
        public override string ToString()
        {
            if (!this.ForceNewChartArea)
            {
                string _string = string.Format(
                    "X [{0}, {1}];   Y [{2}, {3}]",
                    this.chartAreaChart.AxisX.Minimum.ToString(Defaults.ROUNDING),
                    this.chartAreaChart.AxisX.Maximum.ToString(Defaults.ROUNDING),
                    this.chartAreaChart.AxisY.Minimum.ToString(Defaults.ROUNDING),
                    this.chartAreaChart.AxisY.Maximum.ToString(Defaults.ROUNDING));

                return _string;
            }
            else
            {
                return "Force new";
            }
        }

    }

}



