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
    internal partial class Form_NewFunctionWizard_Finish : Form_NewFunctionWizardBase
    {
        public Form_NewFunctionWizard_Finish()
            : this(null)
        {
        }
        public Form_NewFunctionWizard_Finish(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[5];


            if (((NewFunctionWizardState)base.State).Function == null)
            {
                ((NewFunctionWizardState)base.State).Function = base.GenerateFunctionFromState((NewFunctionWizardState)base.State);
            }


            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.AppendLine("Your function is ready to use:");
            _stringBuilder.AppendLine();

            _stringBuilder.AppendLine(((NewFunctionWizardState)base.State).FunctionTypeName);

            _stringBuilder.AppendLine();
            _stringBuilder.AppendLine();
            _stringBuilder.AppendLine("Click Close to start using your function.");


            this.label_Result.Text = _stringBuilder.ToString();


            base.Result = ((NewFunctionWizardState)base.State).Function;
        }

    }
}
