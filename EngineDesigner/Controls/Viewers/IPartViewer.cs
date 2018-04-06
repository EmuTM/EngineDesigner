using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;
using SlimDX;
using SlimDX.Direct3D9;
using EngineDesigner.Media.Graphics.DX;
using EngineDesigner.Media.Graphics.DX.Cameras;
using EngineDesigner.Media.Graphics.DX.IPartSketches;
using EngineDesigner.Machine.Definitions;

namespace EngineDesigner.Controls.Viewers
{
    internal partial class IPartViewer : UserControl
    {
        //tle si zapomnemo, al je blo premikano sidrišče od kamere (desna tipka); če je blo, ne odpremo context menuja (desna tipka)
        private CustomVector3 previousCameraAnchor = CustomVector3.Zero;
        private CustomVector3 currentCameraAnchor = CustomVector3.Zero;



        protected MenuStrip @MenuStrip1
        {
            get { return this.menuStrip1; }
        }
        protected ContextMenuStrip @ContextMenuStrip1
        {
            get { return this.contextMenuStrip1; }
        }



        [DefaultValue(null)]
        public IPart IPart
        {
            get { return this.iPartSketch1.IPart; }
            set { this.iPartSketch1.IPart = value; }
        }



        public IPartViewer()
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                #region "Menus & Toolbars"
                this.menuStrip1.Visible = false;
                #endregion "Menus & Toolbars"
            }
        }
        private void IPartViewer_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.ResetView();
            }
        }
        //HACK: obesimo se na SuspendLayout, ker se kliče v InitializeComponent PO inštanciranju vseh fieldov (kontrol)
        bool suspendLayout = false;
        new public void SuspendLayout()
        {
            if (!this.suspendLayout)
            {
                this.iPartSketch1 = this.GetIPartSketchInstance();

                this.suspendLayout = true;
            }

            base.SuspendLayout();
        }
        protected virtual IPartSketch GetIPartSketchInstance()
        {
            return new IPartSketch();
        }
        private void iPartSketch1_Resize(object sender, EventArgs e)
        {
            IPartSketch _iPartSketch = (IPartSketch)sender;
            _iPartSketch.Camera.AspectRatio = (float)_iPartSketch.Width / (float)_iPartSketch.Height;
        }



        public bool MergeMenuStripWith(MenuStrip _menuStrip)
        {
            return ToolStripManager.Merge(this.menuStrip1, _menuStrip);
        }

        //resetira view, recimo ob nalaganju novega objekta
        public void ResetView()
        {
            if (this.iPartSketch1.IPart != null)
            {
                float _length = (float)(this.iPartSketch1.IPart.Length / 2);
                if (float.IsInfinity(_length))
                {
                    _length = 0f;
                }

                this.iPartSketch1.Camera.Anchor = CustomVector3.From(
                    new Vector3(
                        0f,
                        0f,
                        _length));

                this.Projection_Isometric();
                this.iPartSketch1.View_ZoomToFit();
            }
        }



        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (previousCameraAnchor != currentCameraAnchor)
            {
                e.Cancel = true;
            }

            previousCameraAnchor = currentCameraAnchor;
        }

        private void ClearMenus()
        {
            //odčekiramo vse poglede
            foreach (ToolStripMenuItem _toolStripMenuItem in toolStripMenuItem_Projection.DropDownItems.OfType<ToolStripMenuItem>())
            {
                _toolStripMenuItem.Checked = false;
            }
            foreach (ToolStripMenuItem _toolStripMenuItem in toolStripMenuItem_View_Projection.DropDownItems.OfType<ToolStripMenuItem>())
            {
                _toolStripMenuItem.Checked = false;
            }
        }

        private void iPartSketch1_Camera_AnchorChanged(object sender, MouseControlledCameraEventArgs e)
        {
            this.CameraChanged(e.Anchor);
        }
        private void iPartSketch1_Camera_AngleChanged(object sender, MouseControlledCameraEventArgs e)
        {
            this.CameraChanged(e.Anchor);
        }
        private void iPartSketch1_Camera_OrbitalRadiusChanged(object sender, MouseControlledCameraEventArgs e)
        {
            this.CameraChanged(e.Anchor);
        }
        private void CameraChanged(CustomVector3 _newAnchor)
        {
            currentCameraAnchor = _newAnchor;
            ClearMenus();
        }



        #region "Menu"
        private void toolStripMenuItem_View_Projection_ZoomToFit_Click(object sender, EventArgs e)
        {
            this.Projection_ZoomToFit();
        }
        private void toolStripMenuItem_View_Projection_Reset_Click(object sender, EventArgs e)
        {
            this.Projection_Reset();
        }
        private void toolStripMenuItem_View_Projection_Isometric_Click(object sender, EventArgs e)
        {
            this.Projection_Isometric();
        }
        private void toolStripMenuItem_View_Projection_Top_Click(object sender, EventArgs e)
        {
            this.Projection_Top();
        }
        private void toolStripMenuItem_View_Projection_Bottom_Click(object sender, EventArgs e)
        {
            this.Projection_Bottom();
        }
        private void toolStripMenuItem_View_Projection_Left_Click(object sender, EventArgs e)
        {
            this.Projection_Left();
        }
        private void toolStripMenuItem_View_Projection_Right_Click(object sender, EventArgs e)
        {
            this.Projection_Right();
        }
        private void toolStripMenuItem_View_Projection_Front_Click(object sender, EventArgs e)
        {
            this.Projection_Front();
        }
        private void toolStripMenuItem_View_Projection_Back_Click(object sender, EventArgs e)
        {
            this.Projection_Back();
        }

        private void toolStripMenuItem_View_ShowBounding_Click(object sender, EventArgs e)
        {
            this.ShowBounding();
        }
        #endregion "Menu"

        #region "Context menu"
        private void toolStripMenuItem_Projection_ZoomToFit_Click(object sender, EventArgs e)
        {
            this.Projection_ZoomToFit();
        }
        private void toolStripMenuItem_Projection_Reset_Click(object sender, EventArgs e)
        {
            this.Projection_Reset();
        }
        private void toolStripMenuItem_Projection_Isometric_Click(object sender, EventArgs e)
        {
            this.Projection_Isometric();
        }
        private void toolStripMenuItem_Projection_Top_Click(object sender, EventArgs e)
        {
            this.Projection_Top();
        }
        private void toolStripMenuItem_Projection_Bottom_Click(object sender, EventArgs e)
        {
            this.Projection_Bottom();
        }
        private void toolStripMenuItem_Projection_Left_Click(object sender, EventArgs e)
        {
            this.Projection_Left();
        }
        private void toolStripMenuItem_Projection_Right_Click(object sender, EventArgs e)
        {
            this.Projection_Right();
        }
        private void toolStripMenuItem_Projection_Front_Click(object sender, EventArgs e)
        {
            this.Projection_Front();
        }
        private void toolStripMenuItem_Projection_Back_Click(object sender, EventArgs e)
        {
            this.Projection_Back();
        }

        private void toolStripMenuItem_ShowBounding_Click(object sender, EventArgs e)
        {
            this.ShowBounding();
        }
        #endregion "Context menu"



        private void Projection_ZoomToFit()
        {
            this.iPartSketch1.View_ZoomToFit();
        }
        private void Projection_Reset()
        {
            this.ResetView();
        }
        private void Projection_Isometric()
        {
            this.ClearMenus();
            this.toolStripMenuItem_Projection_Isometric.Checked = !this.toolStripMenuItem_Projection_Isometric.Checked;
            this.toolStripMenuItem_View_Projection_Isometric.Checked = !this.toolStripMenuItem_View_Projection_Isometric.Checked;

            this.iPartSketch1.View_Isometric();
        }
        private void Projection_Top()
        {
            this.ClearMenus();
            this.toolStripMenuItem_Projection_Top.Checked = !this.toolStripMenuItem_Projection_Top.Checked;
            this.toolStripMenuItem_View_Projection_Top.Checked = !this.toolStripMenuItem_View_Projection_Top.Checked;

            this.iPartSketch1.View_Top();
        }
        private void Projection_Bottom()
        {
            this.ClearMenus();
            this.toolStripMenuItem_Projection_Bottom.Checked = !this.toolStripMenuItem_Projection_Bottom.Checked;
            this.toolStripMenuItem_View_Projection_Bottom.Checked = !this.toolStripMenuItem_View_Projection_Bottom.Checked;

            this.iPartSketch1.View_Bottom();
        }
        private void Projection_Left()
        {
            this.ClearMenus();
            this.toolStripMenuItem_Projection_Left.Checked = !this.toolStripMenuItem_Projection_Left.Checked;
            this.toolStripMenuItem_View_Projection_Left.Checked = !this.toolStripMenuItem_View_Projection_Left.Checked;

            this.iPartSketch1.View_Left();
        }
        private void Projection_Right()
        {
            this.ClearMenus();
            this.toolStripMenuItem_Projection_Right.Checked = !this.toolStripMenuItem_Projection_Right.Checked;
            this.toolStripMenuItem_View_Projection_Right.Checked = !this.toolStripMenuItem_View_Projection_Right.Checked;

            this.iPartSketch1.View_Right();
        }
        private void Projection_Front()
        {
            this.ClearMenus();
            this.toolStripMenuItem_Projection_Front.Checked = !this.toolStripMenuItem_Projection_Front.Checked;
            this.toolStripMenuItem_View_Projection_Front.Checked = !this.toolStripMenuItem_View_Projection_Front.Checked;

            this.iPartSketch1.View_Front();
        }
        private void Projection_Back()
        {
            this.ClearMenus();
            this.toolStripMenuItem_Projection_Back.Checked = !this.toolStripMenuItem_Projection_Back.Checked;
            this.toolStripMenuItem_View_Projection_Back.Checked = !this.toolStripMenuItem_View_Projection_Back.Checked;

            this.iPartSketch1.View_Back();
        }

        private void ShowBounding()
        {
            this.toolStripMenuItem_ShowBounding.Checked = !this.toolStripMenuItem_ShowBounding.Checked;
            this.toolStripMenuItem_View_ShowBounding.Checked = !this.toolStripMenuItem_View_ShowBounding.Checked;

            this.iPartSketch1.ShowBoundingBox = this.toolStripMenuItem_View_ShowBounding.Checked;
        }

    }
}
