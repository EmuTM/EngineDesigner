using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Media.Graphics.DX.IPartSketches;
using EngineDesigner.Machine;

namespace EngineDesigner.Controls.Viewers
{
    internal partial class EngineViewer : IPartViewer
    {
        [DefaultValue(null)]
        public new Engine IPart
        {
            get { return (Engine)base.IPart; }
            set { ((EngineSketch)base.iPartSketch1).IPart = value; }
        }

        [DefaultValue(0d)]
        public double CrankshaftRotation_deg
        {
            get { return ((EngineSketch)this.iPartSketch1).CrankshaftRotation_deg; }
            set { ((EngineSketch)this.iPartSketch1).CrankshaftRotation_deg = value; }
        }

        private bool animated = true;
        [DefaultValue(true)]
        public bool Animated
        {
            get { return this.animated; }

            set
            {
                this.animated = false;

                this.toolStripMenuItem_Animated.Checked = this.animated;
                this.toolStripMenuItem_View_Animated.Checked = this.animated;

                ((EngineSketch)base.iPartSketch1).Animated = this.animated;
            }
        }

        private bool animatedEnabled = true;
        [DefaultValue(true)]
        public bool AnimatedEnabled
        {
            get { return this.animatedEnabled; }

            set
            {
                this.animatedEnabled = value;

                this.toolStripMenuItem_Animated.Enabled = this.animatedEnabled;
                this.toolStripMenuItem_View_Animated.Enabled = this.animatedEnabled;

                if (!this.animatedEnabled)
                {
                    ((EngineSketch)base.iPartSketch1).Animated = false;
                }
                else
                {
                    ((EngineSketch)base.iPartSketch1).Animated = this.animated;
                }
            }
        }

        [DefaultValue(null)]
        public IPart[] SelectedIParts
        {
            get { return ((EngineSketch)this.iPartSketch1).SelectedParts; }
            set { ((EngineSketch)this.iPartSketch1).SelectedParts = value; }
        }



        public EngineViewer()
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                #region "Menus & Toolbars"
                this.menuStrip2.Visible = false;


                if (!ToolStripManager.Merge(this.contextMenuStrip2, base.ContextMenuStrip1))
                {
                    throw new Exception();
                }
                if (!ToolStripManager.Merge(this.menuStrip2, base.MenuStrip1))
                {
                    throw new Exception();
                }
                #endregion "Menus & Toolbars"
            }
        }
        protected override IPartSketch GetIPartSketchInstance()
        {
            return new EngineSketch();
        }



        private void toolStripMenuItem_Animated_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem = (ToolStripMenuItem)sender;
            this.Animated = !_toolStripMenuItem.Checked;
        }
        private void toolStripMenuItem_View_Animated_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem = (ToolStripMenuItem)sender;
            this.Animated = !_toolStripMenuItem.Checked;
        }

    }
}
