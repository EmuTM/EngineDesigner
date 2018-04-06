using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;
using EngineDesigner.Machine;

namespace EngineDesigner.Wizards.NewEngine
{
    internal partial class Form_NewEngineWizard_Flywheel : Form_NewEngineWizardBase
    {
        public Form_NewEngineWizard_Flywheel()
            : this(null)
        {
        }
        public Form_NewEngineWizard_Flywheel(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[7];
            base.Finish = new Form_NewEngineWizard_Finish(this);


            if (!double.IsNaN(((NewEngineWizardState)base.State).FlywheelMass))
            {
                this.numericUpDown_FlywheelMass.Value = (decimal)((NewEngineWizardState)base.State).FlywheelMass;
            }
            if (!double.IsNaN(((NewEngineWizardState)base.State).FlywheelDiameter))
            {
                this.numericUpDown_FlywheelDiameter.Value = (decimal)((NewEngineWizardState)base.State).FlywheelDiameter;
            }
        }

        protected override bool OnFinish()
        {
            ((NewEngineWizardState)base.State).FlywheelMass = (double)this.numericUpDown_FlywheelMass.Value;
            ((NewEngineWizardState)base.State).FlywheelDiameter = (double)this.numericUpDown_FlywheelDiameter.Value;

            return base.OnFinish();
        }



        private void numericUpDown_FlywheelMass_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown _numericUpDown = (NumericUpDown)sender;

            if (_numericUpDown.Value > 0)
            {
                this.numericUpDown_FlywheelDiameter.Enabled = true;
            }
            else
            {
                this.numericUpDown_FlywheelDiameter.Enabled = false;
            }
        }

    }
}
