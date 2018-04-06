using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Machine;

namespace EngineDesigner.FloatingForms.EngineMonitors
{
    internal partial class Form_CycleDiagram : Form_EngineMonitorBase
    {
        public Form_CycleDiagram()
            : this(null, null)
        {
        }
        public Form_CycleDiagram(Form _owner, Form_EngineControl _engineControl)
            : base(_owner, _engineControl)
        {
            InitializeComponent();
        }



        private void SetAndRedrawDiagram()
        {
            if (this.axisOptions1.CustomRange)
            {
                this.cycleDiagram1.StartCrankshaftDiagram_deg = this.axisOptions1.StartDegrees;
                this.cycleDiagram1.EndCrankshaftDiagram_deg = this.axisOptions1.EndDegrees;
            }
            else
            {
                this.cycleDiagram1.StartCrankshaftDiagram_deg = 0d;

                if (base.Engine != null)
                {
                    if (base.Engine.CombinedCycleDuration_deg > 0)
                    {
                        this.cycleDiagram1.EndCrankshaftDiagram_deg = base.Engine.CombinedCycleDuration_deg;
                    }
                    else
                    {
                        this.cycleDiagram1.EndCrankshaftDiagram_deg = EngineDesigner.Common.Defaults.DefaultCycle_deg;
                    }
                }
                else
                {
                    this.cycleDiagram1.EndCrankshaftDiagram_deg = EngineDesigner.Common.Defaults.DefaultCycle_deg;
                }
            }


            this.cycleDiagram1.ShowAxisYInPiValues = this.axisOptions1.ShowValuesInPI;


            this.cycleDiagram1.DrawDiagram(base.Engine);
        }
        private void SetAndRedrawCrankshaftAngle()
        {
            if (base.Engine != null)
            {
                if (this.axisOptions1.ShowElapsedStroke)
                {
                    if (!this.cycleDiagram1.ChartCursorYEnabled)
                    {
                        this.cycleDiagram1.ChartCursorYEnabled = true;
                    }


                    double _startDegrees = 0d;
                    double _endDegrees = base.Engine.CombinedCycleDuration_deg;

                    if (this.axisOptions1.CustomRange)
                    {
                        _startDegrees = (double)this.axisOptions1.StartDegrees;
                        _endDegrees = (double)this.axisOptions1.EndDegrees;
                    }

                    if ((this.axisOptions1.ShowStrokeElapseCyclically)
                        || (!this.axisOptions1.CustomRange))
                    {
                        this.cycleDiagram1.ChartCursorY = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                            base.CrankshaftRotation_deg,
                            _startDegrees, _endDegrees,
                            base.Engine.CombinedCycleDuration_deg);
                    }
                    else
                    {
                        this.cycleDiagram1.ChartCursorY = base.CrankshaftRotation_deg;
                    }
                }
                else
                {
                    if (this.cycleDiagram1.ChartCursorYEnabled)
                    {
                        this.cycleDiagram1.ChartCursorYEnabled = false;
                    }
                }
            }
            else
            {
                if (this.cycleDiagram1.ChartCursorYEnabled)
                {
                    this.cycleDiagram1.ChartCursorYEnabled = false;
                }
            }
        }

        private void axisOptions1_CustomRangeChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawDiagram();
        }
        private void axisOptions1_StartDegreesChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawDiagram();
        }
        private void axisOptions1_EndDegreesChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawDiagram();
        }

        private void axisOptions1_ShowElapsedStrokeChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawCrankshaftAngle();
        }
        private void axisOptions1_ShowStrokeElapseCyclicallyChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawCrankshaftAngle();
        }

        private void axisOptions1_ShowValuesInPIChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawDiagram();
        }



        protected override void OnEngineChanged(Engine _engine)
        {
            this.SetAndRedrawDiagram();
        }
        protected override void OnCrankshaftAngleChanged(double _newAngle_deg)
        {
            this.SetAndRedrawCrankshaftAngle();
        }

    }
}
