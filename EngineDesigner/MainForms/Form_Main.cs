using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using EngineDesigner.Environment;
using EngineDesigner.Forms;
using System.ComponentModel;
using System.Windows.Forms;
using EngineDesigner.Wizards.NewEngine;
using EngineDesigner.Wizards.NewFunction;
using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;


namespace EngineDesigner.MainForms
{
    internal partial class Form_Main : Form_MainBase
    {
        public Form_Main()
            : this(null, null)
        {
        }
        public Form_Main(FileInfo _fileInfo, object _object)
            : base(_fileInfo, _object)
        {
            this.InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                #region "Menus & Toolbars"
                this.menuStrip2.Visible = false;
                this.toolStrip2.Visible = false;
                //this.statusStrip2.Visible = false;


                if (!ToolStripManager.Merge(this.menuStrip2, base.MenuStrip1))
                {
                    throw new Exception();
                }

                if (!ToolStripManager.Merge(this.toolStrip2, base.ToolStrip1))
                {
                    throw new Exception();
                }

                //if (!this.engineEditor.MergeMenuStripWith(base.MenuStrip1))
                //{
                //    throw new Exception();
                //}
                //if (!this.engineEditor.MergeToolStripWith(base.ToolStrip1))
                //{
                //    throw new Exception();
                //}
                #endregion "Menus & Toolbars"
            }
        }



        protected override Form_AboutBase ObtainAboutForm()
        {
            return new Form_About();
        }
        protected override Form_MainBase ObtainNewForm(FileInfo _fileInfo)
        {
            try
            {
                return new Form_MainEngine(_fileInfo);
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                try
                {
                    return new Form_MainFunction(_fileInfo);
                }
                catch (System.Runtime.Serialization.SerializationException _serializationException)
                {
                    Utility.WarningMessage(
                        this,
                        this.Text,
                        "Error while loading file",
                        string.Format(
                            "File '{0}' could not be loaded:{1}'{2}'",
                            _fileInfo.FullName,
                            System.Environment.NewLine,
                            _serializationException.Message));

                    return null;
                }
            }
        }



        #region "File"
        private void toolStripMenuItem_New_Engine_Empty_Click(object sender, EventArgs e)
        {
            this.NewEngineEmpty();
        }
        private void toolStripMenuItem_New_Engine_Wizard_Click(object sender, EventArgs e)
        {
            this.NewEngineWizard();
        }
        private void toolStripMenuItem_New_Function_Empty_Click(object sender, EventArgs e)
        {
            this.NewFunctionEmpty();
        }
        private void toolStripMenuItem_New_Function_Wizard_Click(object sender, EventArgs e)
        {
            this.NewFunctionWizard();
        }
        #endregion "File"

        #region "ToolBar"
        private void toolStripMenuItem_New_Engine_Wizard2_Click(object sender, EventArgs e)
        {
            this.NewEngineWizard();
        }
        private void toolStripMenuItem_New_Function_Wizard2_Click(object sender, EventArgs e)
        {
            this.NewFunctionWizard();
        }
        #endregion "ToolBar"



        private void NewEngineEmpty()
        {
            base.StartNewWindow(new Form_MainEngine());
        }
        private void NewEngineWizard()
        {
            Form_NewEngineWizard_Start _form_NewEngineWizard_Start = new Form_NewEngineWizard_Start();
            _form_NewEngineWizard_Start.State = new NewEngineWizardState();

            Engine _engine = WizardManager.StartWizard<Engine>(_form_NewEngineWizard_Start);

            if (_engine != null)
            {
                base.StartNewWindow(new Form_MainEngine(_engine));
            }
        }
        private void NewFunctionEmpty()
        {
            base.StartNewWindow(new Form_MainFunction());
        }
        private void NewFunctionWizard()
        {
            Form_NewFunctionWizard_Start _form_NewFunctionWizard_Start = new Form_NewFunctionWizard_Start();
            _form_NewFunctionWizard_Start.State = new NewFunctionWizardState();

            Function _function = WizardManager.StartWizard<Function>(_form_NewFunctionWizard_Start);

            if (_function != null)
            {
                base.StartNewWindow(new Form_MainFunction(_function));
            }
        }

    }
}
