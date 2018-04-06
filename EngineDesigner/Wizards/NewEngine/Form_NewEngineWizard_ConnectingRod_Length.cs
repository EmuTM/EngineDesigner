using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Common;
using EngineDesigner.Environment;
using System.Diagnostics;

namespace EngineDesigner.Wizards.NewEngine
{
    internal partial class Form_NewEngineWizard_ConnectingRod_Length : Form_NewEngineWizardBase
    {
        public Form_NewEngineWizard_ConnectingRod_Length()
            : this(null)
        {
        }
        public Form_NewEngineWizard_ConnectingRod_Length(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[5].SubBookmarks[0];
            base.Next = new Form_NewEngineWizard_ConnectingRod_MassAndMassDistribution(this);
            base.Finish = new Form_NewEngineWizard_Finish(this);


            if (!double.IsNaN(((NewEngineWizardState)base.State).ConnectingRodLength))
            {
                this.numericUpDown_ConnectingRodLength.Value = (decimal)((NewEngineWizardState)base.State).ConnectingRodLength;
            }
        }

        protected override bool OnNext()
        {
            SetConnectingRodLengthToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            SetConnectingRodLengthToState();

            return base.OnFinish();
        }


        private void SetConnectingRodLengthToState()
        {
            ((NewEngineWizardState)base.State).ConnectingRodLength = (double)this.numericUpDown_ConnectingRodLength.Value;
        }

    }
}
