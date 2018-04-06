using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;

namespace EngineDesigner.Wizards.NewEngine
{
    internal partial class Form_NewEngineWizard_Start : Form_NewEngineWizardBase
    {
        public Form_NewEngineWizard_Start()
            : this(null)
        {
        }
        public Form_NewEngineWizard_Start(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[0];
            base.Next = new Form_NewEngineWizard_Cycle(this);

            //če hočemo, da je gumb finish viden, mora imet neko ciljno formo!
            base.Finish = new Form_NewEngineWizard_Finish(this);
            //ga pa moramo disablat, dokler ni primeren
            base.FinishEnabled = false;
        }

    }
}
