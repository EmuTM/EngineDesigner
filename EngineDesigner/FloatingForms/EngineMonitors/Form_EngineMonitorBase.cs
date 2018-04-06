using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Machine;
using EngineDesigner.FloatingForms;

namespace EngineDesigner.FloatingForms.EngineMonitors
{
    internal partial class Form_EngineMonitorBase : Form_FloatingBase
    {
        public Form_EngineMonitorBase()
            : this(null, null)
        {

        }
        public Form_EngineMonitorBase(Form _owner, Form_EngineControl _engineControl)
            : base(_owner)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                if (_engineControl == null)
                {
                    throw new Exception();
                }

                this.engineControl = _engineControl;
                this.engineControl.VisibleChanged
                    += new EventHandler(engineControl_VisibleChanged);
                this.engineControl.CrankshaftAngleChanged
                    += new EventHandler<CrankshaftAngleEventArgs>(engineControl_CrankshaftAngleChanged);
                this.engineControl.RPMChanged
                    += new EventHandler<RPMEventArgs>(engineControl_RPMChanged);
            }
        }



        private Form_EngineControl engineControl;



        private Engine engine = null;
        public Engine @Engine
        {
            get { return this.engine; }
            set
            {
                this.engine = value;
                this.engineControl.Engine = this.engine;

                this.OnEngineChanged(this.engine);
            }
        }



        protected double CrankshaftRotation_deg
        {
            get { return this.engineControl.CrankshaftRotation_deg; }
        }
        protected int RPM
        {
            get { return this.engineControl.RPM; }
        }
        protected bool EngineControlVisible
        {
            get { return this.engineControl.Visible; }
        }



        private void engineControl_CrankshaftAngleChanged(object sender, CrankshaftAngleEventArgs e)
        {
            if (this.Visible)
            {
                this.OnCrankshaftAngleChanged(e.NewAngle_deg);
            }
        }
        private void engineControl_RPMChanged(object sender, RPMEventArgs e)
        {
            if (this.Visible)
            {
                this.OnRPMChanged(e.NewRPM);
            }
        }
        private void engineControl_VisibleChanged(object sender, EventArgs e)
        {
            Form_EngineControl _engineControl = (Form_EngineControl)sender;

            this.OnEngineControlVisibleChanged(_engineControl.Visible);
        }



        protected virtual void OnEngineChanged(Engine _engine)
        {
            throw new NotImplementedException();
        }
        protected virtual void OnCrankshaftAngleChanged(double _newAngle_deg)
        {
            //ni obvezno
        }
        protected virtual void OnRPMChanged(int _newRPM)
        {
            //ni obvezno
        }
        protected virtual void OnEngineControlVisibleChanged(bool _visible)
        {
            //ni obvezno
        }

    }
}
