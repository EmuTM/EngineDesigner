using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using EngineDesigner.Machine;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class Form_AddAFunctionMoment : Form_AddAFunctionKinematics
    {
        public Form_AddAFunctionMoment()
            : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                throw new Exception("This constructor is only meant for design time purposes.");
            }


            this.Constructor();
        }
        public Form_AddAFunctionMoment(ChartAreaInfo[] _availableChartAreas, PositionedCylinder[] _availablePositionedCylinders)
            : base(_availableChartAreas, _availablePositionedCylinders)
        {
            this.Constructor();


            this.cylinderFunction_Moments.AvailablePositionedCylinders = _availablePositionedCylinders;
            this.cylinderFunction_Moments.AvailableFunctions = FunctionInfoMoment.GetAvailableFunctions();

            this.cylinderFunction_Moments.AvailableHarmonicOrders = new HarmonicOrderInfo[]
            {
                HarmonicOrderInfo.Full,
                HarmonicOrderInfo.FirstApproximation,
                HarmonicOrderInfo.SecondApproximation,
            };
        }
        private void Constructor()
        {
            InitializeComponent();
        }



        protected override void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //NOTE: to stavimo tudi v vsak derivan klas!
            if (this.DesignMode)
            {
                return;
            }


            base.tabControl1_SelectedIndexChanged(sender, e);


            if (base.tabControl1.SelectedTab == this.tabPage_Moments)
            {
                this.WithRespectToPanelEnabled = true;

                if (this.cylinderFunction_Moments.SelectedPositionedCylinder == null)
                {
                    base.Button_OK_Enabled = false;
                }
            }

        }
        protected override void OnOKButtonClicked(ref FunctionInfoBase _selectedFunction, ref bool _cancel)
        {
            base.OnOKButtonClicked(ref _selectedFunction, ref _cancel);


            if (base.tabControl1.SelectedTab == this.tabPage_Moments)
            {
                FunctionInfoMoment _functionInfoMoment = (FunctionInfoMoment)this.cylinderFunction_Moments.SelectedFunction;
                _functionInfoMoment.PositionedCylinder = cylinderFunction_Moments.SelectedPositionedCylinder;
                _functionInfoMoment.HarmonicOrder = cylinderFunction_Moments.SelectedHarmonicOrder;
                _functionInfoMoment.CylinderRelative = this.CylinderRelative;
                _selectedFunction = _functionInfoMoment;
            }
        }

    }
}
