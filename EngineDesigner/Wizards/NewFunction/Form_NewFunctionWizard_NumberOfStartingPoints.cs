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
    internal partial class Form_NewFunctionWizard_NumberOfStartingPoints : Form_NewFunctionWizardBase
    {
        public Form_NewFunctionWizard_NumberOfStartingPoints()
            : this(null)
        {
        }
        public Form_NewFunctionWizard_NumberOfStartingPoints(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[3];
            base.Next = new Form_NewFunctionWizard_AlterFunction(this);
            base.Finish = new Form_NewFunctionWizard_Finish(this);


            if (((NewFunctionWizardState)base.State).NumberOfStartingPoints > 0)
            {
                this.numericUpDown_NumberOfStartingPoints.Value = ((NewFunctionWizardState)base.State).NumberOfStartingPoints;
            }
        }


        protected override bool OnNext()
        {
            this.SetNumberOfStartingPointsToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            this.SetNumberOfStartingPointsToState();

            return base.OnFinish();
        }


        private void SetNumberOfStartingPointsToState()
        {
            ((NewFunctionWizardState)base.State).NumberOfStartingPoints = (int)this.numericUpDown_NumberOfStartingPoints.Value;
        }

    }
}
