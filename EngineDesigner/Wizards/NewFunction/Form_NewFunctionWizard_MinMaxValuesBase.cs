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
    //NOTE: ta klass bi moral biti abstract ampak ni zaradi designerja
    internal partial class Form_NewFunctionWizard_MinMaxValuesBase : Form_NewFunctionWizardBase
    {
        public Form_NewFunctionWizard_MinMaxValuesBase()
            : this(null)
        {
        }
        public Form_NewFunctionWizard_MinMaxValuesBase(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[2];
            base.Next = new Form_NewFunctionWizard_NumberOfStartingPoints(this);

            base.Finish = new Form_NewFunctionWizard_Finish(this);
        }

    }
}
