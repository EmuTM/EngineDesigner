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
    internal partial class Form_AddAFunctionKinematics : Form_AddAFunctionReference
    {
        public Form_AddAFunctionKinematics()
            : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                throw new Exception("This constructor is only meant for design time purposes.");
            }


            this.Constructor();
        }
        public Form_AddAFunctionKinematics(ChartAreaInfo[] _availableChartAreas, PositionedCylinder[] _availablePositionedCylinders)
            : base(_availableChartAreas)
        {
            this.Constructor();


            this.cylinderFunction_Kinematics.AvailablePositionedCylinders = _availablePositionedCylinders;
            this.cylinderFunction_Kinematics.AvailableFunctions = FunctionInfoKinematic.GetAvailableFunctions();

            this.cylinderFunction_Kinematics.AvailableHarmonicOrders = new HarmonicOrderInfo[]
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



        protected bool CylinderRelative
        {
            get
            {
                if (this.WithRespectToPanelEnabled)
                {
                    return this.radioButton_WithRespectTo_CrankThrow.Checked;
                }
                else
                {
                    return false;
                }
            }
        }
        protected bool WithRespectToPanelEnabled
        {
            get { return this.groupBox_WithRespectTo.Enabled; }
            set { this.groupBox_WithRespectTo.Enabled = value; }
        }



        protected override void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //NOTE: to stavimo tudi v vsak derivan klas!
            if (this.DesignMode)
            {
                return;
            }


            base.tabControl1_SelectedIndexChanged(sender, e);


            if (base.tabControl1.SelectedTab == this.tabPage_Kinematics)
            {
                this.WithRespectToPanelEnabled = true;

                if (this.cylinderFunction_Kinematics.SelectedPositionedCylinder == null)
                {
                    base.Button_OK_Enabled = false;
                }
            }
            else
            {
                //NOTE: tega ne sme bit v nobenem derivanem klasu več!
                this.WithRespectToPanelEnabled = false;
            }
        }
        protected override void OnOKButtonClicked(ref FunctionInfoBase _selectedFunction, ref bool _cancel)
        {
            base.OnOKButtonClicked(ref _selectedFunction, ref _cancel);


            if (base.tabControl1.SelectedTab == this.tabPage_Kinematics)
            {
                FunctionInfoKinematic _functionInfoKinematic = (FunctionInfoKinematic)this.cylinderFunction_Kinematics.SelectedFunction;
                _functionInfoKinematic.PositionedCylinder = cylinderFunction_Kinematics.SelectedPositionedCylinder;
                _functionInfoKinematic.HarmonicOrder = cylinderFunction_Kinematics.SelectedHarmonicOrder;
                _functionInfoKinematic.CylinderRelative = this.CylinderRelative;
                _selectedFunction = _functionInfoKinematic;
            }
        }

    }

}
