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
    internal partial class Form_NewEngineWizard_BalancerMassAndRotationRadius : Form_NewEngineWizardBase
    {
        public Form_NewEngineWizard_BalancerMassAndRotationRadius()
            : this(null)
        {
        }
        public Form_NewEngineWizard_BalancerMassAndRotationRadius(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[6];
            base.Next = new Form_NewEngineWizard_Flywheel(this);


            if (!double.IsNaN(((NewEngineWizardState)base.State).BalancerMass))
            {
                this.numericUpDown_BalancerMass.Value = (decimal)((NewEngineWizardState)base.State).BalancerMass;
            }
            if (!double.IsNaN(((NewEngineWizardState)base.State).BalancerRotationRadius))
            {
                this.numericUpDown_BalancerRotationRadius.Value = (decimal)((NewEngineWizardState)base.State).BalancerRotationRadius;
            }
        }

        protected override bool OnNext()
        {
            this.SetBalancerMassAndRotationRadiusToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            this.SetBalancerMassAndRotationRadiusToState();

            return base.OnFinish();
        }



        private void numericUpDown_BalancerMass_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown _numericUpDown = (NumericUpDown)sender;

            if (_numericUpDown.Value > 0)
            {
                this.numericUpDown_BalancerRotationRadius.Enabled = true;
            }
            else
            {
                this.numericUpDown_BalancerRotationRadius.Enabled = false;
            }
        }


        private void SetBalancerMassAndRotationRadiusToState()
        {
            ((NewEngineWizardState)base.State).BalancerMass = (double)this.numericUpDown_BalancerMass.Value;
            ((NewEngineWizardState)base.State).BalancerRotationRadius = (double)this.numericUpDown_BalancerRotationRadius.Value;
        }

    }
}
