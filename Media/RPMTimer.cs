using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using EngineDesigner.Machine;
using EngineDesigner.Common;

namespace EngineDesigner.Media
{
    public partial class RPMTimer : Component
    {
        public event EventHandler<RPMTimerEventArgs> CrankshaftAngleChanged;



        private SynchronizationContext synchronizationContext;



        [DefaultValue(true)]
        public bool Enabled
        {
            get { return this.timer1.Enabled; }
            set { this.timer1.Enabled = value; }
        }

        private int rpm = 25;
        [DefaultValue(25)]
        public int RPM
        {
            get { return rpm; }
            set { rpm = value; }
        }

        private Engine engine = null;
        [DefaultValue(null)]
        [Browsable(false)]
        public Engine @Engine
        {
            get { return this.engine; }
            set { this.engine = value; }
        }

        [Browsable(false)]
        public double Accuracy
        {
            get
            {
                double _rotationsPerSecond = this.RPM / 60d;
                double _rotationsPerMilisecond = _rotationsPerSecond / 1000d;
                double _rotationsPerInterval = _rotationsPerMilisecond * this.timer1.Interval;
                double _angleOfRotation = _rotationsPerInterval * 360d;

                return Math.Round(_angleOfRotation, EngineDesigner.Common.Defaults.RoundingDecimals);
            }
        }

        private bool cycleThroughCombinedCycle = false;
        [DefaultValue(false)]
        public bool CycleThroughCombinedCycle
        {
            get { return cycleThroughCombinedCycle; }
            set { cycleThroughCombinedCycle = value; }
        }



        public RPMTimer()
        {
            Constructor();
        }
        public RPMTimer(IContainer container)
        {
            container.Add(this);
            Constructor();
        }
        private void Constructor()
        {
            InitializeComponent();


            this.synchronizationContext = SynchronizationContext.Current;
            if (this.synchronizationContext == null)
            {
                this.synchronizationContext = new SynchronizationContext();
            }

            this.timer1.Interval = EngineDesigner.Common.Defaults.RPMTimerInterval;
        }



        private double crankshaftRotation_deg = 0;



        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (sender)
            {
                if ((this.engine != null)
                    && this.engine.NumberOfCylinders > 0)
                {
                    this.crankshaftRotation_deg += this.Accuracy;


                    if (this.cycleThroughCombinedCycle)
                    {
                        if (this.engine.RevolutionsToCompleteCombinedCycle > 0)
                        {
                            //zaokrožimo, da ni čudnih številk
                            this.crankshaftRotation_deg = Math.Round(
                                EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(
                                this.crankshaftRotation_deg,
                                this.engine.RevolutionsToCompleteCombinedCycle),
                                EngineDesigner.Common.Defaults.RoundingDecimals);
                        }
                        else
                        {
                            //zaokrožimo, da ni čudnih številk
                            this.crankshaftRotation_deg = Math.Round(
                                EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(this.crankshaftRotation_deg),
                                EngineDesigner.Common.Defaults.RoundingDecimals);
                        }
                    }
                    else
                    {
                        //zaokrožimo, da ni čudnih številk
                        this.crankshaftRotation_deg = Math.Round(
                            this.crankshaftRotation_deg,
                            EngineDesigner.Common.Defaults.RoundingDecimals);
                    }


                    if (this.synchronizationContext != null)
                    {
                        SendOrPostCallback _sendOrPostCallback = new SendOrPostCallback(delegate(object _state)
                        {
                            this.OnCrankshaftAngleChanged(this.crankshaftRotation_deg);
                        });
                        this.synchronizationContext.Post(_sendOrPostCallback, null);
                    }
                }
            }
        }
        protected void OnCrankshaftAngleChanged(double _newAngle)
        {
            if (CrankshaftAngleChanged != null)
            {
                CrankshaftAngleChanged(
                    this,
                    new RPMTimerEventArgs(_newAngle));
            }
        }

    }


    public class RPMTimerEventArgs : EventArgs
    {
        public RPMTimerEventArgs(double _newAngle)
        {
            this.newAngle_deg = _newAngle;
        }


        private double newAngle_deg;
        public double NewAngle_deg
        {
            get { return newAngle_deg; }
        }
    }

}
