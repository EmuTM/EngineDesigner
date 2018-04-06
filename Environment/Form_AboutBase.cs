using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace EngineDesigner.Environment
{
    public partial class Form_AboutBase : Form
    {
        public Form_AboutBase()
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                this.Text = String.Format("About {0}", About.AssemblyProduct);
                this.labelProductName.Text = About.AssemblyProduct;
                this.labelVersion.Text = String.Format("Version {0}", About.AssemblyVersion);
                this.labelCopyright.Text = About.AssemblyCopyright;
                this.labelCompanyName.Text = About.AssemblyCompany;
                this.textBoxDescription.Text = About.AssemblyDescription;
            }
        }
        private void Form_AboutBase_Load(object sender, EventArgs e)
        {
            this.logoPictureBox.Image = this.Image;
        }



        public Image @Image
        {
            get { return this.logoPictureBox.Image; }
            set { this.logoPictureBox.Image = value; }
        }

    }
}
