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
using EngineDesigner.Common;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class CylinderFunctionWithGasPressure : CylinderFunction
    {
        private event EventHandler<IndicatorFunctionEventArgs> cylinderPressureVsCrankAngleIndicatorFunctionChanged;
        public event EventHandler<IndicatorFunctionEventArgs> CylinderPressureVsCrankAngleIndicatorFunctionChanged
        {
            add { cylinderPressureVsCrankAngleIndicatorFunctionChanged += value; }
            remove { cylinderPressureVsCrankAngleIndicatorFunctionChanged -= value; }
        }



        [DefaultValue(null)]
        [Browsable(false)]
        public Function SelectedCylinderPressureVsCrankAngleIndicatorFunction
        {
            get { return this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedIndicatorFunction; }
            set { this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedIndicatorFunction=value; }
        }
        [DefaultValue(null)]
        [Browsable(false)]
        public InterpolationMethodInfo SelectedCylinderPressureVsCrankAngleIndicatorFunctionInterpolationMethod
        {
            get { return this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedInterpolationMethod; }
            set { this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedInterpolationMethod = value; }
        }
        [DefaultValue(null)]
        [Browsable(false)]
        public FileInfo SelectedCylinderPressureVsCrankAngleIndicatorFunctionFile
        {
            get { return this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedIndicatorFunctionFile; }
            set { this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedIndicatorFunctionFile = value; }
        }



        public CylinderFunctionWithGasPressure()
            : this(null, null, null, null, null, null)
        {
        }
        public CylinderFunctionWithGasPressure(FunctionInfoBase[] _availableFunctions, PositionedCylinder[] _availablePositionedCylinders, HarmonicOrderInfo[] _availableHarmonicOrders,
            Function _selectedCylinderPressureVsCrankAngleIndicatorFunction, FileInfo _selectedIndicatorFunctionFile, InterpolationMethodInfo _selectedInterpolationMethod)
            : base(_availableFunctions, _availablePositionedCylinders, _availableHarmonicOrders)
        {
            InitializeComponent();


            this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedIndicatorFunction = _selectedCylinderPressureVsCrankAngleIndicatorFunction;
            this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedIndicatorFunctionFile = _selectedIndicatorFunctionFile;
            this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedInterpolationMethod = _selectedInterpolationMethod;
        }



        public void EnableCylinderPressureVsCrankAngleIndicatorFunction()
        {
            this.indicatorFunction_CylinderPressureVsCrankAngle.Enabled = true;
        }
        public void DisableCylinderPressureVsCrankAngleIndicatorFunction()
        {
            this.indicatorFunction_CylinderPressureVsCrankAngle.Enabled = false;
        }



        private void indicatorFunction_CylinderPressureVsCrankAngle_IndicatorFunctionChanged(object sender, IndicatorFunctionEventArgs e)
        {
            this.OnCylinderPressureVsCrankAngleIndicatorFunctionChanged(e.Function, e.InterpolationMethodInfo);
        }




        protected virtual void OnCylinderPressureVsCrankAngleIndicatorFunctionChanged(Function _function, InterpolationMethodInfo _interpolationMethodInfo)
        {
            if (this.cylinderPressureVsCrankAngleIndicatorFunctionChanged != null)
            {
                this.cylinderPressureVsCrankAngleIndicatorFunctionChanged(
                    this,
                    new IndicatorFunctionEventArgs(
                        _function,
                        _interpolationMethodInfo));
            }
        }

    }

}
