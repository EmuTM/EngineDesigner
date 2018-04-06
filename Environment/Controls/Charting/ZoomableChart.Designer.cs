namespace EngineDesigner.Environment.Controls.Charting
{
    partial class ZoomableChart
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
            this.toolStripMenuItem_ZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_ZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_View = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_View_ZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_ZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_ZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Raised;
            this.chart1.ContextMenuStrip = this.contextMenuStrip1;
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            this.chart1.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart1_AxisViewChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ZoomIn,
            this.toolStripMenuItem_ZoomOut,
            this.toolStripSeparator1,
            this.toolStripMenuItem_ZoomReset});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 98);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem_ZoomIn
            // 
            this.toolStripMenuItem_ZoomIn.Image = global::EngineDesigner.Environment.Properties.Resources.zoomin16x16;
            this.toolStripMenuItem_ZoomIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_ZoomIn.Name = "toolStripMenuItem_ZoomIn";
            this.toolStripMenuItem_ZoomIn.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_ZoomIn.Text = "Zoom in";
            this.toolStripMenuItem_ZoomIn.Click += new System.EventHandler(this.toolStripMenuItem_ZoomIn_Click);
            // 
            // toolStripMenuItem_ZoomOut
            // 
            this.toolStripMenuItem_ZoomOut.Enabled = false;
            this.toolStripMenuItem_ZoomOut.Image = global::EngineDesigner.Environment.Properties.Resources.zoomout16x16;
            this.toolStripMenuItem_ZoomOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_ZoomOut.Name = "toolStripMenuItem_ZoomOut";
            this.toolStripMenuItem_ZoomOut.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_ZoomOut.Text = "Zoom out";
            this.toolStripMenuItem_ZoomOut.Click += new System.EventHandler(this.toolStripMenuItem_ZoomOut_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem_ZoomReset
            // 
            this.toolStripMenuItem_ZoomReset.Enabled = false;
            this.toolStripMenuItem_ZoomReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_ZoomReset.Name = "toolStripMenuItem_ZoomReset";
            this.toolStripMenuItem_ZoomReset.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_ZoomReset.Text = "Reset zoom";
            this.toolStripMenuItem_ZoomReset.Click += new System.EventHandler(this.toolStripMenuItem_ZoomReset_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_View});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(320, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // toolStripMenuItem_View
            // 
            this.toolStripMenuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_View_ZoomIn,
            this.toolStripMenuItem_View_ZoomOut,
            this.toolStripSeparator2,
            this.toolStripMenuItem_View_ZoomReset});
            this.toolStripMenuItem_View.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripMenuItem_View.Name = "toolStripMenuItem_View";
            this.toolStripMenuItem_View.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem_View.Text = "&View";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem_View_ZoomReset
            // 
            this.toolStripMenuItem_View_ZoomReset.Enabled = false;
            this.toolStripMenuItem_View_ZoomReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_View_ZoomReset.Name = "toolStripMenuItem_View_ZoomReset";
            this.toolStripMenuItem_View_ZoomReset.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_View_ZoomReset.Text = "Reset zoom";
            this.toolStripMenuItem_View_ZoomReset.Click += new System.EventHandler(this.toolStripMenuItem_View_ZoomReset_Click);
            // 
            // toolStripMenuItem_View_ZoomIn
            // 
            this.toolStripMenuItem_View_ZoomIn.Image = global::EngineDesigner.Environment.Properties.Resources.zoomin16x16;
            this.toolStripMenuItem_View_ZoomIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_View_ZoomIn.Name = "toolStripMenuItem_View_ZoomIn";
            this.toolStripMenuItem_View_ZoomIn.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_View_ZoomIn.Text = "Zoom in";
            this.toolStripMenuItem_View_ZoomIn.Click += new System.EventHandler(this.toolStripMenuItem_View_ZoomIn_Click);
            // 
            // toolStripMenuItem_View_ZoomOut
            // 
            this.toolStripMenuItem_View_ZoomOut.Enabled = false;
            this.toolStripMenuItem_View_ZoomOut.Image = global::EngineDesigner.Environment.Properties.Resources.zoomout16x16;
            this.toolStripMenuItem_View_ZoomOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_View_ZoomOut.Name = "toolStripMenuItem_View_ZoomOut";
            this.toolStripMenuItem_View_ZoomOut.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_View_ZoomOut.Text = "Zoom out";
            this.toolStripMenuItem_View_ZoomOut.Click += new System.EventHandler(this.toolStripMenuItem_View_ZoomOut_Click);
            // 
            // ZoomableChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Name = "ZoomableChart";
            this.Controls.SetChildIndex(this.chart1, 0);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ZoomReset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ZoomIn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_ZoomIn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_ZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_ZoomReset;
    }
}
