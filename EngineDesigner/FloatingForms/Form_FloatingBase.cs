using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Machine;

namespace EngineDesigner.FloatingForms
{
    internal partial class Form_FloatingBase : Form
    {
        public Form_FloatingBase()
            : this(null)
        {
        }
        public Form_FloatingBase(Form _owner)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                if (_owner == null)
                {
                    throw new Exception("This class must not be instanciated with parameterless constructor; this constructor is only meant for design time purposes.");
                }
            }


            this.Owner = _owner;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.ShowInTaskbar = false;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = true;
            this.Hide();
        }
        //tu mora bit zatu ker, čene se tle pohendla exception kar sam od sebe. ne vem zakaj. na tak način, je vsaj preusmerjen ven
        protected override void OnVisibleChanged(EventArgs e)
        {
            try
            {
                base.OnVisibleChanged(e);
            }
            catch (Exception _exception)
            {
                this.BeginInvoke((Action)(delegate { throw _exception; }));
            }
        }



        private ToolStripMenuItem[] associatedMenuItems;
        [DefaultValue(null)]
        [Browsable(false)]
        public ToolStripMenuItem[] AssociatedMenuItems
        {
            get { return associatedMenuItems; }
            set { associatedMenuItems = value; }
        }

    }
}
