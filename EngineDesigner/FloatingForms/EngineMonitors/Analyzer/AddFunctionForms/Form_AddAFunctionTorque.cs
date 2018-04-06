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
using System.IO;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class Form_AddAFunctionTorque : Form_AddAFunctionForce
    {
        public Form_AddAFunctionTorque()
            : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                throw new Exception("This constructor is only meant for design time purposes.");
            }


            this.Constructor();
        }
        public Form_AddAFunctionTorque(ChartAreaInfo[] _availableChartAreas, PositionedCylinder[] _availablePositionedCylinders,
            Function _selectedCylinderPressureVsCrankAngleIndicatorFunction, FileInfo _selectedIndicatorFunctionFile, InterpolationMethodInfo _selectedInterpolationMethod)
            : base(_availableChartAreas, _availablePositionedCylinders, _selectedCylinderPressureVsCrankAngleIndicatorFunction, _selectedIndicatorFunctionFile, _selectedInterpolationMethod)
        {
            this.Constructor();


            this.cylinderFunctionWithGasPressure_Torque.AvailablePositionedCylinders = _availablePositionedCylinders;
            this.cylinderFunctionWithGasPressure_Torque.AvailableFunctions = FunctionInfoTorque.GetAvailableFunctions();
            this.cylinderFunctionWithGasPressure_Torque.AvailableHarmonicOrders = new HarmonicOrderInfo[]
            {
                HarmonicOrderInfo.Full,
                HarmonicOrderInfo.FirstApproximation,
                HarmonicOrderInfo.SecondApproximation,
                HarmonicOrderInfo.ThirdApproximation,
            };

            this.cylinderFunctionWithGasPressure_Torque.SelectedCylinderPressureVsCrankAngleIndicatorFunction = _selectedCylinderPressureVsCrankAngleIndicatorFunction;
            this.cylinderFunctionWithGasPressure_Torque.SelectedCylinderPressureVsCrankAngleIndicatorFunctionFile = _selectedIndicatorFunctionFile;
            this.cylinderFunctionWithGasPressure_Torque.SelectedCylinderPressureVsCrankAngleIndicatorFunctionInterpolationMethod = _selectedInterpolationMethod;


            //pokličemo kar tega, da pohendlamo še enable/disable ok buttona
            this.tabControl1_SelectedIndexChanged(base.tabControl1, new EventArgs());
        }
        private void Constructor()
        {
            InitializeComponent();
        }



        private void cylinderFunctionWithGasPressure_Torque_FunctionChanged(object sender, FunctionEventArgs e)
        {
            CylinderFunctionWithGasPressure _cylinderFunctionWithGasPressure = (CylinderFunctionWithGasPressure)sender;

            #region "requires indicator function"
            bool _requiresIndicatorFunction = false;
            if (e.FunctionInfoBase is FunctionInfoTorque)
            {
                FunctionInfoTorque _functionInfoTorque = (FunctionInfoTorque)e.FunctionInfoBase;

                if (_functionInfoTorque.RequiresIndicatorFunction)
                {
                    _requiresIndicatorFunction = true;
                }
            }
            if (_requiresIndicatorFunction)
            {
                _cylinderFunctionWithGasPressure.EnableCylinderPressureVsCrankAngleIndicatorFunction();
            }
            else
            {
                _cylinderFunctionWithGasPressure.DisableCylinderPressureVsCrankAngleIndicatorFunction();
            }
            #endregion "requires indicator function"


            //pokličemo kar tega, da pohendlamo še enable/disable ok buttona
            this.tabControl1_SelectedIndexChanged(base.tabControl1, new EventArgs());
        }
        private void cylinderFunctionWithGasPressure_Torque_CylinderPressureVsCrankAngleIndicatorFunctionChanged(object sender, IndicatorFunctionEventArgs e)
        {
            //pokličemo kar tega, da pohendlamo še enable/disable ok buttona
            this.tabControl1_SelectedIndexChanged(base.tabControl1, new EventArgs());
        }


        protected override void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //NOTE: to stavimo tudi v vsak derivan klas!
            if (this.DesignMode)
            {
                return;
            }


            base.tabControl1_SelectedIndexChanged(sender, e);


            if (base.tabControl1.SelectedTab == this.tabPage_Torques)
            {
                this.WithRespectToPanelEnabled = true;

                if (this.cylinderFunctionWithGasPressure_Torque.SelectedPositionedCylinder == null)
                {
                    base.Button_OK_Enabled = false;
                }

                if (this.cylinderFunctionWithGasPressure_Torque.SelectedFunction is FunctionInfoTorque)
                {
                    FunctionInfoTorque _functionInfoTorque = (FunctionInfoTorque)this.cylinderFunctionWithGasPressure_Torque.SelectedFunction;

                    if (_functionInfoTorque.RequiresIndicatorFunction)
                    {
                        if (this.cylinderFunctionWithGasPressure_Torque.SelectedCylinderPressureVsCrankAngleIndicatorFunction == null)
                        {
                            base.Button_OK_Enabled = false;
                        }
                    }
                }
            }
        }
        protected override void OnOKButtonClicked(ref FunctionInfoBase _selectedFunction, ref bool _cancel)
        {
            base.OnOKButtonClicked(ref _selectedFunction, ref _cancel);


            if (base.tabControl1.SelectedTab == this.tabPage_Torques)
            {
                FunctionInfoTorque _functionInfoTorque = (FunctionInfoTorque)this.cylinderFunctionWithGasPressure_Torque.SelectedFunction;
                _functionInfoTorque.PositionedCylinder = this.cylinderFunctionWithGasPressure_Torque.SelectedPositionedCylinder;
                _functionInfoTorque.HarmonicOrder = this.cylinderFunctionWithGasPressure_Torque.SelectedHarmonicOrder;
                _functionInfoTorque.CylinderRelative = base.CylinderRelative;
                _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction = this.cylinderFunctionWithGasPressure_Torque.SelectedCylinderPressureVsCrankAngleIndicatorFunction;
                _functionInfoTorque.InterpolationMethod = this.cylinderFunctionWithGasPressure_Torque.SelectedCylinderPressureVsCrankAngleIndicatorFunctionInterpolationMethod;

                _selectedFunction = _functionInfoTorque;
            }
        }

    }

}
