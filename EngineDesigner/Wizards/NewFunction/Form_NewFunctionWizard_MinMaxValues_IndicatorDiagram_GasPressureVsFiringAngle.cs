using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;

namespace EngineDesigner.Wizards.NewFunction
{
    internal partial class Form_NewFunctionWizard_MinMaxValues_IndicatorDiagram_GasPressureVsFiringAngle : Form_NewFunctionWizard_MinMaxValuesBase
    {
        public Form_NewFunctionWizard_MinMaxValues_IndicatorDiagram_GasPressureVsFiringAngle()
            : this(null)
        {
        }
        public Form_NewFunctionWizard_MinMaxValues_IndicatorDiagram_GasPressureVsFiringAngle(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            if (!double.IsNaN(((NewFunctionWizardState)base.State).LowestPressureOnIntakeStroke))
            {
                this.numericUpDown_LowestPressureOnIntakeStroke.Value = Math.Round((decimal)((NewFunctionWizardState)base.State).LowestPressureOnIntakeStroke, 2);
            }
            if (!double.IsNaN(((NewFunctionWizardState)base.State).HighestPressureOnPowerStroke))
            {
                this.numericUpDown_HighestPressureOnPowerStroke.Value = Math.Round((decimal)((NewFunctionWizardState)base.State).HighestPressureOnPowerStroke, 2);
            }
            if (!double.IsNaN(((NewFunctionWizardState)base.State).AverageAtmosphericPressure))
            {
                this.numericUpDown_AverageAtmosphericPressure.Value = Math.Round((decimal)((NewFunctionWizardState)base.State).AverageAtmosphericPressure, 2);
            }
        }

        protected override bool OnNext()
        {
            this.SetMinMaxValuesToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            this.SetMinMaxValuesToState();

            return base.OnFinish();
        }




        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if ((this.numericUpDown_LowestPressureOnIntakeStroke.Value < this.numericUpDown_HighestPressureOnPowerStroke.Value)
                && (this.numericUpDown_LowestPressureOnIntakeStroke.Value != this.numericUpDown_HighestPressureOnPowerStroke.Value)
                && (this.numericUpDown_HighestPressureOnPowerStroke.Value > this.numericUpDown_AverageAtmosphericPressure.Value)
                && (this.numericUpDown_LowestPressureOnIntakeStroke.Value < this.numericUpDown_AverageAtmosphericPressure.Value))
            {
                base.NextEnabled = true;
                base.FinishEnabled = true;
            }
            else
            {
                base.NextEnabled = false;
                base.FinishEnabled = false;
            }
        }


        private void SetMinMaxValuesToState()
        {
            ((NewFunctionWizardState)base.State).LowestPressureOnIntakeStroke = (double)this.numericUpDown_LowestPressureOnIntakeStroke.Value;
            ((NewFunctionWizardState)base.State).HighestPressureOnPowerStroke = (double)this.numericUpDown_HighestPressureOnPowerStroke.Value;
            ((NewFunctionWizardState)base.State).AverageAtmosphericPressure = (double)this.numericUpDown_AverageAtmosphericPressure.Value;
        }

    }
}
