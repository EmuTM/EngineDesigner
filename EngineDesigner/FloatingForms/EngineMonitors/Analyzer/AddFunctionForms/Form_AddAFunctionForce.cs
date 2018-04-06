using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Machine;
using System.IO;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class Form_AddAFunctionForce : Form_AddAFunctionMoment
    {
        public Form_AddAFunctionForce()
            : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                throw new Exception("This constructor is only meant for design time purposes.");
            }


            this.Constructor();
        }
        public Form_AddAFunctionForce(ChartAreaInfo[] _availableChartAreas, PositionedCylinder[] _availablePositionedCylinders,
            Function _selectedCylinderPressureVsCrankAngleIndicatorFunction, FileInfo _selectedIndicatorFunctionFile, InterpolationMethodInfo _selectedInterpolationMethod)
            : base(_availableChartAreas, _availablePositionedCylinders)
        {
            this.Constructor();


            this.cylinderFunctionWithGasPressure_Force.AvailablePositionedCylinders = _availablePositionedCylinders;
            this.cylinderFunctionWithGasPressure_Force.AvailableFunctions = FunctionInfoForce.GetAvailableFunctions();
            this.cylinderFunctionWithGasPressure_Force.AvailableHarmonicOrders = new HarmonicOrderInfo[]
            {
                HarmonicOrderInfo.Full,
                HarmonicOrderInfo.FirstApproximation,
                HarmonicOrderInfo.SecondApproximation,
            };

            this.cylinderFunctionWithGasPressure_Force.SelectedCylinderPressureVsCrankAngleIndicatorFunction = _selectedCylinderPressureVsCrankAngleIndicatorFunction;
            this.cylinderFunctionWithGasPressure_Force.SelectedCylinderPressureVsCrankAngleIndicatorFunctionFile = _selectedIndicatorFunctionFile;
            this.cylinderFunctionWithGasPressure_Force.SelectedCylinderPressureVsCrankAngleIndicatorFunctionInterpolationMethod = _selectedInterpolationMethod;


            //disablamo, ker je po defaultu izbrana neustrezna funkcija za to
            this.cylinderFunctionWithGasPressure_Force.DisableHarmonicOrder();
            this.cylinderFunctionWithGasPressure_Force.DeselectHarmonicOrder();


            //pokličemo kar tega, da pohendlamo še enable/disable ok buttona
            this.tabControl1_SelectedIndexChanged(base.tabControl1, new EventArgs());
        }
        private void Constructor()
        {
            InitializeComponent();
        }



        private void cylinderFunctionWithGasPressure_Force_FunctionChanged(object sender, FunctionEventArgs e)
        {
            CylinderFunctionWithGasPressure _cylinderFunctionWithGasPressure = (CylinderFunctionWithGasPressure)sender;

            #region "requires harmonic order"
            bool _requiresHarmonicOrder = false;
            if (e.FunctionInfoBase is FunctionInfoKinematic)
            {
                FunctionInfoKinematic _functionInfoKinematic = (FunctionInfoKinematic)e.FunctionInfoBase;

                if (_functionInfoKinematic.RequiresHarmonicOrder)
                {
                    _requiresHarmonicOrder = true;
                }
            }
            if (_requiresHarmonicOrder)
            {
                _cylinderFunctionWithGasPressure.ReselectHarmonicOrder();
                _cylinderFunctionWithGasPressure.EnableHarmonicOrder();
            }
            else
            {
                _cylinderFunctionWithGasPressure.DisableHarmonicOrder();
                _cylinderFunctionWithGasPressure.DeselectHarmonicOrder();
            }
            #endregion "requires harmonic order"

            #region "requires indicator function"
            bool _requiresIndicatorFunction = false;
            if (e.FunctionInfoBase is FunctionInfoForce)
            {
                FunctionInfoForce _functionInfoForce = (FunctionInfoForce)e.FunctionInfoBase;

                if (_functionInfoForce.RequiresIndicatorFunction)
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
        private void cylinderFunctionWithGasPressure_Force_CylinderPressureVsCrankAngleIndicatorFunctionChanged(object sender, IndicatorFunctionEventArgs e)
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


            if (base.tabControl1.SelectedTab == this.tabPage_Forces)
            {
                this.WithRespectToPanelEnabled = true;

                if (this.cylinderFunctionWithGasPressure_Force.SelectedPositionedCylinder == null)
                {
                    base.Button_OK_Enabled = false;
                }

                if (this.cylinderFunctionWithGasPressure_Force.SelectedFunction is FunctionInfoForce)
                {
                    FunctionInfoForce _functionInfoForce = (FunctionInfoForce)this.cylinderFunctionWithGasPressure_Force.SelectedFunction;

                    if (_functionInfoForce.RequiresIndicatorFunction)
                    {
                        if (this.cylinderFunctionWithGasPressure_Force.SelectedCylinderPressureVsCrankAngleIndicatorFunction == null)
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


            if (base.tabControl1.SelectedTab == this.tabPage_Forces)
            {
                FunctionInfoForce _functionInfoForce = (FunctionInfoForce)this.cylinderFunctionWithGasPressure_Force.SelectedFunction;
                _functionInfoForce.PositionedCylinder = this.cylinderFunctionWithGasPressure_Force.SelectedPositionedCylinder;
                _functionInfoForce.HarmonicOrder = this.cylinderFunctionWithGasPressure_Force.SelectedHarmonicOrder;
                _functionInfoForce.CylinderRelative = base.CylinderRelative;
                _functionInfoForce.CylinderPressureVsCrankAngleIndicatorFunction = this.cylinderFunctionWithGasPressure_Force.SelectedCylinderPressureVsCrankAngleIndicatorFunction;
                _functionInfoForce.InterpolationMethod = this.cylinderFunctionWithGasPressure_Force.SelectedCylinderPressureVsCrankAngleIndicatorFunctionInterpolationMethod;

                _selectedFunction = _functionInfoForce;
            }
        }


    }

}
