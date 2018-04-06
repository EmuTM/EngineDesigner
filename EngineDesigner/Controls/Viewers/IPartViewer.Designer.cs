namespace EngineDesigner.Controls.Viewers
{
    partial class IPartViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Projection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Projection_ZoomToFit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Projection_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Projection_Isometric = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Projection_Top = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Projection_Bottom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Projection_Left = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Projection_Right = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Projection_Front = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Projection_Back = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_ShowBounding = new System.Windows.Forms.ToolStripMenuItem();
            this.iPartSketch1 = new EngineDesigner.Media.Graphics.DX.IPartSketches.IPartSketch();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_View = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection_ZoomToFit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_View_Projection_Isometric = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection_Top = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection_Bottom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection_Left = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection_Right = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection_Front = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Projection_Back = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_View_ShowBounding = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.iPartSketch1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Projection,
            this.toolStripSeparator2,
            this.toolStripMenuItem_ShowBounding});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 76);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem_Projection
            // 
            this.toolStripMenuItem_Projection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Projection_ZoomToFit,
            this.toolStripMenuItem_Projection_Reset,
            this.toolStripSeparator1,
            this.toolStripMenuItem_Projection_Isometric,
            this.toolStripMenuItem_Projection_Top,
            this.toolStripMenuItem_Projection_Bottom,
            this.toolStripMenuItem_Projection_Left,
            this.toolStripMenuItem_Projection_Right,
            this.toolStripMenuItem_Projection_Front,
            this.toolStripMenuItem_Projection_Back});
            this.toolStripMenuItem_Projection.Name = "toolStripMenuItem_Projection";
            this.toolStripMenuItem_Projection.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_Projection.Text = "Projection";
            // 
            // toolStripMenuItem_Projection_ZoomToFit
            // 
            this.toolStripMenuItem_Projection_ZoomToFit.Name = "toolStripMenuItem_Projection_ZoomToFit";
            this.toolStripMenuItem_Projection_ZoomToFit.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_ZoomToFit.Text = "Zoom to fit";
            this.toolStripMenuItem_Projection_ZoomToFit.Click += new System.EventHandler(this.toolStripMenuItem_Projection_ZoomToFit_Click);
            // 
            // toolStripMenuItem_Projection_Reset
            // 
            this.toolStripMenuItem_Projection_Reset.Name = "toolStripMenuItem_Projection_Reset";
            this.toolStripMenuItem_Projection_Reset.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_Reset.Text = "Reset";
            this.toolStripMenuItem_Projection_Reset.Click += new System.EventHandler(this.toolStripMenuItem_Projection_Reset_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // toolStripMenuItem_Projection_Isometric
            // 
            this.toolStripMenuItem_Projection_Isometric.Name = "toolStripMenuItem_Projection_Isometric";
            this.toolStripMenuItem_Projection_Isometric.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_Isometric.Text = "Isometric";
            this.toolStripMenuItem_Projection_Isometric.Click += new System.EventHandler(this.toolStripMenuItem_Projection_Isometric_Click);
            // 
            // toolStripMenuItem_Projection_Top
            // 
            this.toolStripMenuItem_Projection_Top.Name = "toolStripMenuItem_Projection_Top";
            this.toolStripMenuItem_Projection_Top.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_Top.Text = "Top";
            this.toolStripMenuItem_Projection_Top.Click += new System.EventHandler(this.toolStripMenuItem_Projection_Top_Click);
            // 
            // toolStripMenuItem_Projection_Bottom
            // 
            this.toolStripMenuItem_Projection_Bottom.Name = "toolStripMenuItem_Projection_Bottom";
            this.toolStripMenuItem_Projection_Bottom.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_Bottom.Text = "Bottom";
            this.toolStripMenuItem_Projection_Bottom.Click += new System.EventHandler(this.toolStripMenuItem_Projection_Bottom_Click);
            // 
            // toolStripMenuItem_Projection_Left
            // 
            this.toolStripMenuItem_Projection_Left.Name = "toolStripMenuItem_Projection_Left";
            this.toolStripMenuItem_Projection_Left.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_Left.Text = "Left";
            this.toolStripMenuItem_Projection_Left.Click += new System.EventHandler(this.toolStripMenuItem_Projection_Left_Click);
            // 
            // toolStripMenuItem_Projection_Right
            // 
            this.toolStripMenuItem_Projection_Right.Name = "toolStripMenuItem_Projection_Right";
            this.toolStripMenuItem_Projection_Right.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_Right.Text = "Right";
            this.toolStripMenuItem_Projection_Right.Click += new System.EventHandler(this.toolStripMenuItem_Projection_Right_Click);
            // 
            // toolStripMenuItem_Projection_Front
            // 
            this.toolStripMenuItem_Projection_Front.Name = "toolStripMenuItem_Projection_Front";
            this.toolStripMenuItem_Projection_Front.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_Front.Text = "Front";
            this.toolStripMenuItem_Projection_Front.Click += new System.EventHandler(this.toolStripMenuItem_Projection_Front_Click);
            // 
            // toolStripMenuItem_Projection_Back
            // 
            this.toolStripMenuItem_Projection_Back.Name = "toolStripMenuItem_Projection_Back";
            this.toolStripMenuItem_Projection_Back.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_Projection_Back.Text = "Back";
            this.toolStripMenuItem_Projection_Back.Click += new System.EventHandler(this.toolStripMenuItem_Projection_Back_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem_ShowBounding
            // 
            this.toolStripMenuItem_ShowBounding.Checked = true;
            this.toolStripMenuItem_ShowBounding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem_ShowBounding.Name = "toolStripMenuItem_ShowBounding";
            this.toolStripMenuItem_ShowBounding.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_ShowBounding.Text = "Show bounding";
            this.toolStripMenuItem_ShowBounding.Click += new System.EventHandler(this.toolStripMenuItem_ShowBounding_Click);
            // 
            // iPartSketch1
            // 
            // 
            // 
            // 
            this.iPartSketch1.Camera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.iPartSketch1.Camera.AnchorChanged += new System.EventHandler<EngineDesigner.Media.Graphics.DX.Cameras.MouseControlledCameraEventArgs>(this.iPartSketch1_Camera_AnchorChanged);
            this.iPartSketch1.Camera.AngleChanged += new System.EventHandler<EngineDesigner.Media.Graphics.DX.Cameras.MouseControlledCameraEventArgs>(this.iPartSketch1_Camera_AngleChanged);
            this.iPartSketch1.Camera.OrbitalRadiusChanged += new System.EventHandler<EngineDesigner.Media.Graphics.DX.Cameras.MouseControlledCameraEventArgs>(this.iPartSketch1_Camera_OrbitalRadiusChanged);
            this.iPartSketch1.ContextMenuStrip = this.contextMenuStrip1;
            this.iPartSketch1.Controls.Add(this.menuStrip1);
            this.iPartSketch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iPartSketch1.Location = new System.Drawing.Point(0, 0);
            this.iPartSketch1.Name = "iPartSketch1";
            this.iPartSketch1.ShowBoundingBox = true;
            this.iPartSketch1.Size = new System.Drawing.Size(150, 150);
            this.iPartSketch1.TabIndex = 1;
            this.iPartSketch1.Resize += new System.EventHandler(this.iPartSketch1_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_View});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(150, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_View
            // 
            this.toolStripMenuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_View_Projection,
            this.toolStripSeparator3,
            this.toolStripMenuItem_View_ShowBounding});
            this.toolStripMenuItem_View.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripMenuItem_View.Name = "toolStripMenuItem_View";
            this.toolStripMenuItem_View.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem_View.Text = "&View";
            // 
            // toolStripMenuItem_View_Projection
            // 
            this.toolStripMenuItem_View_Projection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_View_Projection_ZoomToFit,
            this.toolStripMenuItem_View_Projection_Reset,
            this.toolStripSeparator4,
            this.toolStripMenuItem_View_Projection_Isometric,
            this.toolStripMenuItem_View_Projection_Top,
            this.toolStripMenuItem_View_Projection_Bottom,
            this.toolStripMenuItem_View_Projection_Left,
            this.toolStripMenuItem_View_Projection_Right,
            this.toolStripMenuItem_View_Projection_Front,
            this.toolStripMenuItem_View_Projection_Back});
            this.toolStripMenuItem_View_Projection.Name = "toolStripMenuItem_View_Projection";
            this.toolStripMenuItem_View_Projection.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_View_Projection.Text = "Projection";
            // 
            // toolStripMenuItem_View_Projection_ZoomToFit
            // 
            this.toolStripMenuItem_View_Projection_ZoomToFit.Name = "toolStripMenuItem_View_Projection_ZoomToFit";
            this.toolStripMenuItem_View_Projection_ZoomToFit.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_ZoomToFit.Text = "Zoom to fit";
            this.toolStripMenuItem_View_Projection_ZoomToFit.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_ZoomToFit_Click);
            // 
            // toolStripMenuItem_View_Projection_Reset
            // 
            this.toolStripMenuItem_View_Projection_Reset.Name = "toolStripMenuItem_View_Projection_Reset";
            this.toolStripMenuItem_View_Projection_Reset.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_Reset.Text = "Reset";
            this.toolStripMenuItem_View_Projection_Reset.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_Reset_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(131, 6);
            // 
            // toolStripMenuItem_View_Projection_Isometric
            // 
            this.toolStripMenuItem_View_Projection_Isometric.Name = "toolStripMenuItem_View_Projection_Isometric";
            this.toolStripMenuItem_View_Projection_Isometric.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_Isometric.Text = "Isometric";
            this.toolStripMenuItem_View_Projection_Isometric.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_Isometric_Click);
            // 
            // toolStripMenuItem_View_Projection_Top
            // 
            this.toolStripMenuItem_View_Projection_Top.Name = "toolStripMenuItem_View_Projection_Top";
            this.toolStripMenuItem_View_Projection_Top.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_Top.Text = "Top";
            this.toolStripMenuItem_View_Projection_Top.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_Top_Click);
            // 
            // toolStripMenuItem_View_Projection_Bottom
            // 
            this.toolStripMenuItem_View_Projection_Bottom.Name = "toolStripMenuItem_View_Projection_Bottom";
            this.toolStripMenuItem_View_Projection_Bottom.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_Bottom.Text = "Bottom";
            this.toolStripMenuItem_View_Projection_Bottom.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_Bottom_Click);
            // 
            // toolStripMenuItem_View_Projection_Left
            // 
            this.toolStripMenuItem_View_Projection_Left.Name = "toolStripMenuItem_View_Projection_Left";
            this.toolStripMenuItem_View_Projection_Left.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_Left.Text = "Left";
            this.toolStripMenuItem_View_Projection_Left.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_Left_Click);
            // 
            // toolStripMenuItem_View_Projection_Right
            // 
            this.toolStripMenuItem_View_Projection_Right.Name = "toolStripMenuItem_View_Projection_Right";
            this.toolStripMenuItem_View_Projection_Right.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_Right.Text = "Right";
            this.toolStripMenuItem_View_Projection_Right.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_Right_Click);
            // 
            // toolStripMenuItem_View_Projection_Front
            // 
            this.toolStripMenuItem_View_Projection_Front.Name = "toolStripMenuItem_View_Projection_Front";
            this.toolStripMenuItem_View_Projection_Front.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_Front.Text = "Front";
            this.toolStripMenuItem_View_Projection_Front.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_Front_Click);
            // 
            // toolStripMenuItem_View_Projection_Back
            // 
            this.toolStripMenuItem_View_Projection_Back.Name = "toolStripMenuItem_View_Projection_Back";
            this.toolStripMenuItem_View_Projection_Back.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem_View_Projection_Back.Text = "Back";
            this.toolStripMenuItem_View_Projection_Back.Click += new System.EventHandler(this.toolStripMenuItem_View_Projection_Back_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem_View_ShowBounding
            // 
            this.toolStripMenuItem_View_ShowBounding.Checked = true;
            this.toolStripMenuItem_View_ShowBounding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem_View_ShowBounding.Name = "toolStripMenuItem_View_ShowBounding";
            this.toolStripMenuItem_View_ShowBounding.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_View_ShowBounding.Text = "Show bounding";
            this.toolStripMenuItem_View_ShowBounding.Click += new System.EventHandler(this.toolStripMenuItem_View_ShowBounding_Click);
            // 
            // IPartViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.iPartSketch1);
            this.Name = "IPartViewer";
            this.Load += new System.EventHandler(this.IPartViewer_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.iPartSketch1.ResumeLayout(false);
            this.iPartSketch1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_ZoomToFit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_Reset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_Isometric;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_Top;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_Bottom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_Left;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_Right;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_Front;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Projection_Back;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ShowBounding;
        protected EngineDesigner.Media.Graphics.DX.IPartSketches.IPartSketch iPartSketch1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_ShowBounding;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_ZoomToFit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_Reset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_Isometric;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_Top;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_Bottom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_Left;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_Right;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_Front;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Projection_Back;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}
