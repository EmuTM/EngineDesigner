using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using EngineDesigner.Controls.Editors;
using EngineDesigner.Controls.Viewers;
using EngineDesigner.Machine;


namespace EngineDesigner.MainForms
{
    internal partial class Form_MainIPart : Form_Main
    {
        public Form_MainIPart()
            : this(null, null)
        {
        }
        public Form_MainIPart(IPart _iPart)
            : this(null, _iPart)
        {
        }
        public Form_MainIPart(FileInfo _fileInfo)
            : this(_fileInfo, null)
        {
        }
        public Form_MainIPart(FileInfo _fileInfo, IPart _iPart)
            : base(_fileInfo, _iPart)
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                if (!this.iPartViewer1.MergeMenuStripWith(base.MenuStrip1))
                {
                    throw new Exception();
                }
            }
        }
        private void Form_MainIPart_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                //v tem vrstnem redu!!!
                this.Show();
                Application.DoEvents();
                this.iPartViewer1.ResetView();
            }
        }
        //HACK: obesimo se na SuspendLayout, ker se kliče v InitializeComponent PO inštanciranju vseh fieldov (kontrol)
        bool suspendLayout = false;
        new public void SuspendLayout()
        {
            if (!this.suspendLayout)
            {
                this.iPartEditor1 = this.GetIPartEditorInstance();
                this.iPartViewer1 = this.GetIPartViewerInstance();

                this.suspendLayout = true;
            }

            base.SuspendLayout();
        }
        protected virtual IPartEditor GetIPartEditorInstance()
        {
            return new IPartEditor();
        }
        protected virtual IPartViewer GetIPartViewerInstance()
        {
            return new IPartViewer();
        }

        private void iPartEditor1_EditedPartChanged(object sender, EditedPartChangedEventArgs e)
        {
            this.iPartViewer1.IPart = e.EditedPart;
            base.changesSaved = false;
        }

    }

}
