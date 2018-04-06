using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Controls.Editors;
using EngineDesigner.Controls.Viewers;
using System.IO;
using EngineDesigner.Machine;
using EngineDesigner.FloatingForms;
using EngineDesigner.FloatingForms.EngineMonitors;
using EngineDesigner.FloatingForms.EngineMonitors.Analyzer;


namespace EngineDesigner.MainForms
{
    internal partial class Form_MainEngine : Form_MainIPart
    {
        public Form_MainEngine()
            : this(null, null)
        {
        }
        public Form_MainEngine(Engine _engine)
            : this(null, _engine)
        {
        }
        public Form_MainEngine(FileInfo _fileInfo)
            : this(_fileInfo, null)
        {
        }
        public Form_MainEngine(FileInfo _fileInfo, Engine _engine)
            : base(_fileInfo, _engine)
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                this.engineEditor = (EngineEditor)base.iPartEditor1;
                this.engineViewer = (EngineViewer)base.iPartViewer1;
                this.engineEditor.EditedPart = this.engine;
                this.engineViewer.IPart = this.engine;


                #region "Menus & Toolbars"
                this.menuStrip2.Visible = false;
                //this.toolStrip2.Visible = false;
                //this.statusStrip2.Visible = false;


                if (!ToolStripManager.Merge(this.menuStrip2, base.MenuStrip1))
                {
                    throw new Exception();
                }

                //if (!ToolStripManager.Merge(this.toolStrip2, base.ToolStrip1))
                //{
                //    throw new Exception();
                //}

                if (!this.engineEditor.MergeMenuStripWith(base.MenuStrip1))
                {
                    throw new Exception();
                }
                if (!this.engineEditor.MergeToolStripWith(base.ToolStrip1))
                {
                    throw new Exception();
                }
                #endregion "Menus & Toolbars"


                #region "Floating forms"
                this.form_EngineControl = new Form_EngineControl(this);
                this.form_EngineControl.VisibleChanged
                    += new EventHandler(form_EngineControl_VisibleChanged);
                this.form_EngineControl.CrankshaftAngleChanged
                    += new EventHandler<CrankshaftAngleEventArgs>(form_EngineControl_CrankshaftAngleChanged);

                this.form_CycleDiagram = new Form_CycleDiagram(this, this.form_EngineControl);
                this.form_CycleDiagram.VisibleChanged
                    += new EventHandler(form_CycleDiagram_VisibleChanged);

                this.form_CrankshaftDiagram = new Form_CrankshaftDiagram(this, this.form_EngineControl);
                this.form_CrankshaftDiagram.VisibleChanged
                    += new EventHandler(form_CrankshaftDiagram_VisibleChanged);

                this.form_ExhaustNote = new Form_ExhaustNote(this, this.form_EngineControl);
                this.form_ExhaustNote.VisibleChanged
                    += new EventHandler(form_ExhaustNote_VisibleChanged);

                this.form_Analyzer = new Form_Analyzer(this, this.form_EngineControl);
                this.form_Analyzer.VisibleChanged
                    += new EventHandler(form_Analyzer_VisibleChanged);

                this.form_Statistics = new Form_Statistics(this, this.form_EngineControl);
                this.form_Statistics.VisibleChanged
                    += new EventHandler(form_Statistics_VisibleChanged);
                #endregion "Floating forms"
            }
        }



        private Engine engine;
        private EngineEditor engineEditor;
        private EngineViewer engineViewer;

        private Form_EngineControl form_EngineControl;
        private Form_CycleDiagram form_CycleDiagram;
        private Form_CrankshaftDiagram form_CrankshaftDiagram;
        private Form_ExhaustNote form_ExhaustNote;
        private Form_Analyzer form_Analyzer;
        private Form_Statistics form_Statistics;



        protected override IPartEditor GetIPartEditorInstance()
        {
            return new EngineEditor();
        }
        protected override IPartViewer GetIPartViewerInstance()
        {
            return new EngineViewer();
        }



        #region "Tools"
        private void toolStripMenuItem_Tools_EngineControl_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem = (ToolStripMenuItem)sender;

            if (!_toolStripMenuItem.Checked)
            {
                this.form_EngineControl.Show();
            }
            else
            {
                this.form_EngineControl.Hide();
            }
        }
        private void toolStripMenuItem_Tools_CycleDiagram_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem = (ToolStripMenuItem)sender;

            if (!_toolStripMenuItem.Checked)
            {
                this.form_CycleDiagram.Show();
            }
            else
            {
                this.form_CycleDiagram.Hide();
            }
        }
        private void toolStripMenuItem_Tools_CrankshaftDiagram_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem = (ToolStripMenuItem)sender;

            if (!_toolStripMenuItem.Checked)
            {
                this.form_CrankshaftDiagram.Show();
            }
            else
            {
                this.form_CrankshaftDiagram.Hide();
            }
        }
        private void toolStripMenuItem_Tools_ExhaustNote_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem = (ToolStripMenuItem)sender;

            if (!_toolStripMenuItem.Checked)
            {
                this.form_ExhaustNote.Show();
            }
            else
            {
                this.form_ExhaustNote.Hide();
            }
        }
        private void toolStripMenuItem_Tools_Analyzer_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem = (ToolStripMenuItem)sender;

            if (!_toolStripMenuItem.Checked)
            {
                this.form_Analyzer.Show();
            }
            else
            {
                this.form_Analyzer.Hide();
            }
        }
        private void toolStripMenuItem_Statistics_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem = (ToolStripMenuItem)sender;

            if (!_toolStripMenuItem.Checked)
            {
                this.form_Statistics.Show();
            }
            else
            {
                this.form_Statistics.Hide();
            }
        }
        #endregion "Tools"



        #region "Floating forms"
        private void form_EngineControl_VisibleChanged(object sender, EventArgs e)
        {
            Form_EngineControl _form_EngineControl = (Form_EngineControl)sender;

            this.toolStripMenuItem_Tools_EngineControl.Checked = _form_EngineControl.Visible;

            if (_form_EngineControl.Visible)
            {
                _form_EngineControl.Engine = this.engineEditor.EditedPart;
            }

            this.engineViewer.AnimatedEnabled = !_form_EngineControl.Visible;
        }
        private void form_EngineControl_CrankshaftAngleChanged(object sender, CrankshaftAngleEventArgs e)
        {
            Form_EngineControl _form_EngineControl = (Form_EngineControl)sender;

            if (_form_EngineControl.Visible)
            {
                this.engineViewer.CrankshaftRotation_deg = e.NewAngle_deg;
            }
        }

        private void form_CycleDiagram_VisibleChanged(object sender, EventArgs e)
        {
            Form_CycleDiagram _form_CycleDiagram = (Form_CycleDiagram)sender;

            this.toolStripMenuItem_Tools_CycleDiagram.Checked = _form_CycleDiagram.Visible;

            if (_form_CycleDiagram.Visible)
            {
                _form_CycleDiagram.Engine = this.engineEditor.EditedPart;
            }
        }
        private void form_CrankshaftDiagram_VisibleChanged(object sender, EventArgs e)
        {
            Form_CrankshaftDiagram _form_CrankshaftDiagram = (Form_CrankshaftDiagram)sender;

            this.toolStripMenuItem_Tools_CrankshaftDiagram.Checked = _form_CrankshaftDiagram.Visible;

            if (_form_CrankshaftDiagram.Visible)
            {
                _form_CrankshaftDiagram.Engine = this.engineEditor.EditedPart;
            }
        }
        private void form_ExhaustNote_VisibleChanged(object sender, EventArgs e)
        {
            Form_ExhaustNote _form_ExhaustNote = (Form_ExhaustNote)sender;

            this.toolStripMenuItem_Tools_ExhaustNote.Checked = _form_ExhaustNote.Visible;

            if (_form_ExhaustNote.Visible)
            {
                _form_ExhaustNote.Engine = this.engineEditor.EditedPart;
            }
        }
        private void form_Analyzer_VisibleChanged(object sender, EventArgs e)
        {
            Form_Analyzer _form_Analyzer = (Form_Analyzer)sender;

            this.toolStripMenuItem_Tools_Analyzer.Checked = _form_Analyzer.Visible;

            if (_form_Analyzer.Visible)
            {
                _form_Analyzer.Engine = this.engineEditor.EditedPart;
            }
        }
        private void form_Statistics_VisibleChanged(object sender, EventArgs e)
        {
            Form_Statistics _form_Statistics = (Form_Statistics)sender;

            this.toolStripMenuItem_Tools_Statistics.Checked = _form_Statistics.Visible;

            if (_form_Statistics.Visible)
            {
                _form_Statistics.Engine = this.engineEditor.EditedPart;
            }
        }
        #endregion "Floating forms"



        protected override bool SaveFile(FileInfo _fileInfo)
        {
            try
            {
                this.engine.Save(_fileInfo.FullName);
                return true;
            }
            catch (Exception _exception)
            {
                EngineDesigner.Common.Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                return false;
            }
        }
        protected override void LoadFileOrObject(FileInfo _fileInfo, object _object)
        {
            if (_fileInfo.Exists)
            {
                this.engine = EngineDesigner.Machine.Engine.From(_fileInfo.FullName);
            }
            else if (_object != null)
            {
                this.engine = (Engine)_object;
                base.changesSaved = false;
            }
            else
            {
                this.engine = new Engine();
            }
        }
        protected override string FileTypeFilter
        {
            get
            {
                return "Engine designer Engine files (*.xml)|*.xml";
            }
        }



        private void iPartEditor1_EditedItemSelected(object sender, EditedItemSelectedEventArgs e)
        {
            if (e.SelectedItem.Value is IPart)
            {
                this.engineViewer.SelectedIParts = new IPart[] { (IPart)e.SelectedItem.Value };
            }
            else if ((e.SelectedItem.Parent != null)
                && (e.SelectedItem.Parent.Value is IPart))
            {
                this.engineViewer.SelectedIParts = new IPart[] { (IPart)e.SelectedItem.Parent.Value };
            }
            else
            {
                this.engineViewer.SelectedIParts = null;
            }
        }
        private void iPartEditor1_EditedPartChanged(object sender, EditedPartChangedEventArgs e)
        {
            Engine _engine = (Engine)e.EditedPart;


            this.engineViewer.IPart = _engine;


            if (this.form_EngineControl.Visible)
            {
                this.form_EngineControl.Engine = _engine;
            }

            if (this.form_CycleDiagram.Visible)
            {
                this.form_CycleDiagram.Engine = _engine;
            }

            if (this.form_CrankshaftDiagram.Visible)
            {
                this.form_CrankshaftDiagram.Engine = _engine;
            }

            if (this.form_ExhaustNote.Visible)
            {
                this.form_ExhaustNote.Engine = _engine;
            }

            if (this.form_Analyzer.Visible)
            {
                this.form_Analyzer.Engine = _engine;
            }
        }

    }

}
