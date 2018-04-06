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
    internal partial class Form_NewEngineWizard_Cycle : Form_NewEngineWizardBase
    {
        public Form_NewEngineWizard_Cycle()
            : this(null)
        {
        }
        public Form_NewEngineWizard_Cycle(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[1];
            base.Next = new Form_NewEngineWizard_EngineLayout(this);

            //če hočemo, da je gumb finish viden, mora imet neko ciljno formo!
            base.Finish = new Form_NewEngineWizard_Finish(this);
            //ga pa moramo disablat, dokler ni primeren
            base.FinishEnabled = false;


            if (((NewEngineWizardState)base.State).Cycle.Equals(Cycle.TwoStroke))
            {
                this.radioButton_TwoStroke.Checked = true;
            }
            else if (((NewEngineWizardState)base.State).Cycle .Equals( Cycle.FourStroke))
            {
                this.radioButton_FourStroke.Checked = true;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        protected override bool OnNext()
        {
            if (this.radioButton_TwoStroke.Checked)
            {
                ((NewEngineWizardState)base.State).Cycle = Cycle.TwoStroke;
            }
            else if (this.radioButton_FourStroke.Checked)
            {
                ((NewEngineWizardState)base.State).Cycle = Cycle.FourStroke;
            }


            return base.OnNext();
        }

    }
}
