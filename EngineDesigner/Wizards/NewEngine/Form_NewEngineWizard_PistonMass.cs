using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;
using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.Wizards.NewEngine
{
    internal partial class Form_NewEngineWizard_PistonMass : Form_NewEngineWizardBase
    {
        public Form_NewEngineWizard_PistonMass()
            : this(null)
        {
        }
        public Form_NewEngineWizard_PistonMass(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[4];
            base.Next = new Form_NewEngineWizard_ConnectingRod_Length(this);
            base.Finish = new Form_NewEngineWizard_Finish(this);


            if (!double.IsNaN(((NewEngineWizardState)base.State).PistonMass))
            {
                this.numericUpDown_PistonMass.Value = (decimal)((NewEngineWizardState)base.State).PistonMass;
            }
        }

        protected override bool OnNext()
        {
            this.SetPistonMassToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            this.SetPistonMassToState();

            return base.OnFinish();
        }


        private void SetPistonMassToState()
        {
            ((NewEngineWizardState)base.State).PistonMass = (double)this.numericUpDown_PistonMass.Value;
        }

    }
}
