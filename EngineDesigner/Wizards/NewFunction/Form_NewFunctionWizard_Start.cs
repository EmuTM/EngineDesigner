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
    internal partial class Form_NewFunctionWizard_Start : Form_NewFunctionWizardBase
    {
        public Form_NewFunctionWizard_Start()
            : this(null)
        {
        }
        public Form_NewFunctionWizard_Start(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[0];
            base.Next = new Form_NewFunctionWizard_TypeOfFunction(this);

            //če hočemo, da je gumb finish viden, mora imet neko ciljno formo!
            base.Finish = new Form_NewFunctionWizard_Finish(this);
            //ga pa moramo disablat, dokler ni primeren
            base.FinishEnabled = false;
        }

    }
}
