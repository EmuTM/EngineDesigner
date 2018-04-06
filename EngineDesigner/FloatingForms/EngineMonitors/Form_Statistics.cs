using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Environment.Controls;
using EngineDesigner.Common;

namespace EngineDesigner.FloatingForms.EngineMonitors
{
    internal partial class Form_Statistics : Form_EngineMonitorBase
    {
        private double minX = 0d;
        private double maxX = 359d;
        private double resolutionX = 1d;




        public Form_Statistics()
            : this(null, null)
        {
        }
        public Form_Statistics(Form _owner, Form_EngineControl _engineControl)
            : base(_owner, _engineControl)
        {
            InitializeComponent();
        }



        protected override void OnEngineChanged(Engine _engine)
        {
            this.SetForEngine();
        }
        protected override void OnRPMChanged(int _newRPM)
        {
            this.SetForEngine();
        }



        private Engine engine = null;
        public new Engine @Engine
        {
            get { return this.engine; }
            set
            {
                this.engine = value;
                this.SetForEngine();
                //this.engineControl.Engine = this.engine;

                //this.OnEngineChanged(this.engine);
            }
        }


        //TODO: to se bo renamalo
        private void SetForEngine()
        {
            List<ReportItem> _reportItems = new List<ReportItem>();

            #region "Keys"
            //keye, ki imajo vključeno dinamično variablo moramo doličiti zunaj, ker čene niso enaki v primerjanju (c# fore)!
            string _maxPistonVelocityAtSelectedRPM = string.Format(
                    "Max piston velocity @ selected RPM ({0}) (mps)",
                    base.RPM);

            string _meanPistonVelocityAtSelectedRPM = string.Format(
                    "Mean piston velocity @ selected RPM ({0}) (mps)",
                    base.RPM);

            string _maxPistonAccelerationAtSelectedRPM = string.Format(
                    "Max piston acceleration @ selected RPM ({0}) (mps2)",
                    base.RPM);
            #endregion "Keys"

            for (int a = 0; a < this.engine.NumberOfCylinders; a++)
            {
                ReportItem _reportItem;

                #region "Piston velocity"
                double _maxPistonVelocityAngle_degs = this.GetMaxPistonVelocityAngle_degs(this.engine.PositionedCylinders[a]);
                double _maxPistonVelocityAtSelectedRPM_mps = this.engine.PositionedCylinders[a].GetPistonVelocity_mps(_maxPistonVelocityAngle_degs, base.RPM);
                double _meanPistonVelocityAtSelectedRPM_mps = this.engine.PositionedCylinders[a].GetMeanPistonVelocity_mps(base.RPM);


                _reportItem = new ReportItem(
                    "Angle @ max piston velocity (mps)",
                    _maxPistonVelocityAngle_degs.ToString(Defaults.ROUNDING),
                    this.engine.PositionedCylinders[a]);
                _reportItems.Add(_reportItem);

                _reportItem = new ReportItem(
                    _maxPistonVelocityAtSelectedRPM,
                    _maxPistonVelocityAtSelectedRPM_mps.ToString(Defaults.ROUNDING),
                    this.engine.PositionedCylinders[a]);
                _reportItems.Add(_reportItem);

                _reportItem = new ReportItem(
                    _meanPistonVelocityAtSelectedRPM,
                    _meanPistonVelocityAtSelectedRPM_mps.ToString(Defaults.ROUNDING),
                    this.engine.PositionedCylinders[a]);
                _reportItems.Add(_reportItem);

                _reportItems.Add(new ReportItem(_meanPistonVelocityAtSelectedRPM, null, this.engine.PositionedCylinders[a]));
                #endregion "Piston velocity"

                #region "Piston acceleration"
                double _maxPistonAccelerationAngle_degs = this.GetMaxPistonAccelerationAngle_degs(this.engine.PositionedCylinders[a]);
                double _maxPistonAccelerationAtSelectedRPM_mps2 = this.engine.PositionedCylinders[a].GetPistonAcceleration_mps2(_maxPistonAccelerationAngle_degs, base.RPM);


                _reportItem = new ReportItem(
                    "Angle @ max piston acceleration (degs)",
                    _maxPistonAccelerationAngle_degs.ToString(Defaults.ROUNDING),
                    this.engine.PositionedCylinders[a]);
                _reportItems.Add(_reportItem);

                _reportItem = new ReportItem(
                    _maxPistonAccelerationAtSelectedRPM,
                    _maxPistonVelocityAtSelectedRPM_mps.ToString(Defaults.ROUNDING),
                    this.engine.PositionedCylinders[a]);
                _reportItems.Add(_reportItem);

                _reportItems.Add(new ReportItem(_maxPistonAccelerationAtSelectedRPM, null, this.engine.PositionedCylinders[a]));
                #endregion "Piston acceleration"
            }

            this.report1.ReportItems = _reportItems.ToArray();
        }


        private double GetMaxPistonVelocityAngle_degs(Cylinder _cylinder)
        {
            Function _function = Function.Compute(this.minX, this.maxX, this.resolutionX, (delegate(double _x)
            {
                return _cylinder.GetPistonVelocity_mpdeg(_x);
            }));

            double _xAtMaxY = 0;
            _function.GetMaxY(out _xAtMaxY);

            return _xAtMaxY;
        }
        private double GetMaxPistonAccelerationAngle_degs(Cylinder _cylinder)
        {
            Function _function = Function.Compute(this.minX, this.maxX, this.resolutionX, (delegate(double _x)
            {
                return _cylinder.GetPistonAcceleration_mpdeg2(_x);
            }));

            double _xAtMaxY = 0;
            _function.GetMaxY(out _xAtMaxY);

            return _xAtMaxY;
        }
        private double GetMeanPistonVelocityAtSelectedRPM_mps(Cylinder _cylinder)
        {
            Function _function = Function.Compute(this.minX, this.maxX, this.resolutionX, (delegate(double _x)
            {
                return
                    _cylinder.GetMeanPistonVelocity_mps(_x);
            }));

            double _xAtMaxY = 0;
            _function.GetMaxY(out _xAtMaxY);

            return _xAtMaxY;
        }


    }
}
