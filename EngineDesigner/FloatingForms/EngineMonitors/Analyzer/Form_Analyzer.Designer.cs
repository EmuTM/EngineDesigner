namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class Form_Analyzer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton_AddAFunction = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripDropDownButton_RemoveSelectedFunctions = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton_Aggregates = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem_CreateAverage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CreateSuperpositon = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton_Redraw = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton_ClearChart = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_RPM = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Chart = new System.Windows.Forms.TabPage();
            this.multiFunctionChart1 = new EngineDesigner.Environment.Controls.Charting.MultiFunctionChart();
            this.contextMenuStrip_LegendAdditionalFeatures = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_CreateAverage2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CreateSuperpositon2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabPage_Options = new System.Windows.Forms.TabPage();
            this.axisOptions1 = new EngineDesigner.Controls.Charting.AxisOptions();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.colorPicker1 = new EngineDesigner.Environment.Controls.ColorPicker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStripMenuItem_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton_LoadSave = new System.Windows.Forms.ToolStripDropDownButton();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Chart.SuspendLayout();
            this.contextMenuStrip_LegendAdditionalFeatures.SuspendLayout();
            this.tabPage_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Status,
            this.toolStripSplitButton_AddAFunction,
            this.toolStripDropDownButton_RemoveSelectedFunctions,
            this.toolStripStatusLabel2,
            this.toolStripDropDownButton_Aggregates,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel1,
            this.toolStripDropDownButton_Redraw,
            this.toolStripStatusLabel3,
            this.toolStripDropDownButton_ClearChart,
            this.toolStripStatusLabel4,
            this.toolStripDropDownButton_LoadSave,
            this.toolStripStatusLabel_RPM});
            this.statusStrip1.Location = new System.Drawing.Point(0, 412);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(804, 24);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Status
            // 
            this.toolStripStatusLabel_Status.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            this.toolStripStatusLabel_Status.Size = new System.Drawing.Size(43, 19);
            this.toolStripStatusLabel_Status.Text = "Ready";
            // 
            // toolStripSplitButton_AddAFunction
            // 
            this.toolStripSplitButton_AddAFunction.Image = global::EngineDesigner.Properties.Resources.add16x16;
            this.toolStripSplitButton_AddAFunction.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSplitButton_AddAFunction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton_AddAFunction.Name = "toolStripSplitButton_AddAFunction";
            this.toolStripSplitButton_AddAFunction.Size = new System.Drawing.Size(127, 22);
            this.toolStripSplitButton_AddAFunction.Text = "Add a function...";
            this.toolStripSplitButton_AddAFunction.ButtonClick += new System.EventHandler(this.toolStripSplitButton_AddAFunction_ButtonClick);
            this.toolStripSplitButton_AddAFunction.DropDownOpening += new System.EventHandler(this.toolStripSplitButton_AddAFunction_DropDownOpening);
            // 
            // toolStripDropDownButton_RemoveSelectedFunctions
            // 
            this.toolStripDropDownButton_RemoveSelectedFunctions.Enabled = false;
            this.toolStripDropDownButton_RemoveSelectedFunctions.Image = global::EngineDesigner.Properties.Resources.remove16x16;
            this.toolStripDropDownButton_RemoveSelectedFunctions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton_RemoveSelectedFunctions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_RemoveSelectedFunctions.Name = "toolStripDropDownButton_RemoveSelectedFunctions";
            this.toolStripDropDownButton_RemoveSelectedFunctions.ShowDropDownArrow = false;
            this.toolStripDropDownButton_RemoveSelectedFunctions.Size = new System.Drawing.Size(177, 22);
            this.toolStripDropDownButton_RemoveSelectedFunctions.Text = "Remove selected function(s)";
            this.toolStripDropDownButton_RemoveSelectedFunctions.Click += new System.EventHandler(this.toolStripDropDownButton_RemoveSelectedFunctions_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(4, 19);
            // 
            // toolStripDropDownButton_Aggregates
            // 
            this.toolStripDropDownButton_Aggregates.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_CreateAverage,
            this.toolStripMenuItem_CreateSuperpositon});
            this.toolStripDropDownButton_Aggregates.Image = global::EngineDesigner.Properties.Resources.chart16x16;
            this.toolStripDropDownButton_Aggregates.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton_Aggregates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_Aggregates.Name = "toolStripDropDownButton_Aggregates";
            this.toolStripDropDownButton_Aggregates.Size = new System.Drawing.Size(96, 22);
            this.toolStripDropDownButton_Aggregates.Text = "Aggregates";
            // 
            // toolStripMenuItem_CreateAverage
            // 
            this.toolStripMenuItem_CreateAverage.Enabled = false;
            this.toolStripMenuItem_CreateAverage.Name = "toolStripMenuItem_CreateAverage";
            this.toolStripMenuItem_CreateAverage.Size = new System.Drawing.Size(183, 22);
            this.toolStripMenuItem_CreateAverage.Text = "Create average";
            this.toolStripMenuItem_CreateAverage.Click += new System.EventHandler(this.toolStripMenuItem_CreateAverage_Click);
            // 
            // toolStripMenuItem_CreateSuperpositon
            // 
            this.toolStripMenuItem_CreateSuperpositon.Enabled = false;
            this.toolStripMenuItem_CreateSuperpositon.Name = "toolStripMenuItem_CreateSuperpositon";
            this.toolStripMenuItem_CreateSuperpositon.Size = new System.Drawing.Size(183, 22);
            this.toolStripMenuItem_CreateSuperpositon.Text = "Create superposition";
            this.toolStripMenuItem_CreateSuperpositon.Click += new System.EventHandler(this.toolStripMenuItem_CreateSuperpositon_Click);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(51, 19);
            this.toolStripStatusLabel5.Spring = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 19);
            // 
            // toolStripDropDownButton_Redraw
            // 
            this.toolStripDropDownButton_Redraw.Image = global::EngineDesigner.Properties.Resources.refresh16x16;
            this.toolStripDropDownButton_Redraw.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton_Redraw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_Redraw.Name = "toolStripDropDownButton_Redraw";
            this.toolStripDropDownButton_Redraw.ShowDropDownArrow = false;
            this.toolStripDropDownButton_Redraw.Size = new System.Drawing.Size(66, 22);
            this.toolStripDropDownButton_Redraw.Text = "Redraw";
            this.toolStripDropDownButton_Redraw.Click += new System.EventHandler(this.toolStripDropDownButton_Redraw_Click);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(4, 19);
            // 
            // toolStripDropDownButton_ClearChart
            // 
            this.toolStripDropDownButton_ClearChart.Image = global::EngineDesigner.Properties.Resources.delete16x16;
            this.toolStripDropDownButton_ClearChart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton_ClearChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_ClearChart.Name = "toolStripDropDownButton_ClearChart";
            this.toolStripDropDownButton_ClearChart.ShowDropDownArrow = false;
            this.toolStripDropDownButton_ClearChart.Size = new System.Drawing.Size(84, 22);
            this.toolStripDropDownButton_ClearChart.Text = "Clear chart";
            this.toolStripDropDownButton_ClearChart.Click += new System.EventHandler(this.toolStripDropDownButton_ClearChart_Click);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(4, 19);
            this.toolStripStatusLabel4.Visible = false;
            // 
            // toolStripStatusLabel_RPM
            // 
            this.toolStripStatusLabel_RPM.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel_RPM.Name = "toolStripStatusLabel_RPM";
            this.toolStripStatusLabel_RPM.Size = new System.Drawing.Size(36, 19);
            this.toolStripStatusLabel_RPM.Text = "RPM";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Chart);
            this.tabControl1.Controls.Add(this.tabPage_Options);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(804, 412);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage_Chart
            // 
            this.tabPage_Chart.Controls.Add(this.multiFunctionChart1);
            this.tabPage_Chart.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Chart.Name = "tabPage_Chart";
            this.tabPage_Chart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Chart.Size = new System.Drawing.Size(796, 386);
            this.tabPage_Chart.TabIndex = 0;
            this.tabPage_Chart.Text = "Chart";
            this.tabPage_Chart.UseVisualStyleBackColor = true;
            // 
            // multiFunctionChart1
            // 
            this.multiFunctionChart1.ChartMarkersColor = System.Drawing.Color.Empty;
            this.multiFunctionChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiFunctionChart1.LegendContextMenuToMerge = this.contextMenuStrip_LegendAdditionalFeatures;
            this.multiFunctionChart1.Location = new System.Drawing.Point(3, 3);
            this.multiFunctionChart1.Name = "multiFunctionChart1";
            this.multiFunctionChart1.Size = new System.Drawing.Size(790, 380);
            this.multiFunctionChart1.TabIndex = 0;
            this.multiFunctionChart1.FunctionAdded += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.MultiFunctionChartEventArgs>(this.multiFunctionChart1_FunctionAdded);
            this.multiFunctionChart1.SelectedFunctionsChanged += new System.EventHandler(this.multiFunctionChart1_SelectedFunctionsChanged);
            this.multiFunctionChart1.LegendItemToolTipShowing += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.CustomLegendItemEventArgs>(this.multiFunctionChart1_LegendItemToolTipShowing);
            // 
            // contextMenuStrip_LegendAdditionalFeatures
            // 
            this.contextMenuStrip_LegendAdditionalFeatures.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_CreateAverage2,
            this.toolStripMenuItem_CreateSuperpositon2,
            this.toolStripSeparator1});
            this.contextMenuStrip_LegendAdditionalFeatures.Name = "contextMenuStrip1";
            this.contextMenuStrip_LegendAdditionalFeatures.Size = new System.Drawing.Size(181, 54);
            // 
            // toolStripMenuItem_CreateAverage2
            // 
            this.toolStripMenuItem_CreateAverage2.Enabled = false;
            this.toolStripMenuItem_CreateAverage2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripMenuItem_CreateAverage2.MergeIndex = 0;
            this.toolStripMenuItem_CreateAverage2.Name = "toolStripMenuItem_CreateAverage2";
            this.toolStripMenuItem_CreateAverage2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_CreateAverage2.Text = "Create average";
            this.toolStripMenuItem_CreateAverage2.Click += new System.EventHandler(this.toolStripMenuItem_CreateAverage2_Click);
            // 
            // toolStripMenuItem_CreateSuperpositon2
            // 
            this.toolStripMenuItem_CreateSuperpositon2.Enabled = false;
            this.toolStripMenuItem_CreateSuperpositon2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_CreateSuperpositon2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripMenuItem_CreateSuperpositon2.MergeIndex = 1;
            this.toolStripMenuItem_CreateSuperpositon2.Name = "toolStripMenuItem_CreateSuperpositon2";
            this.toolStripMenuItem_CreateSuperpositon2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_CreateSuperpositon2.Text = "Create superpositon";
            this.toolStripMenuItem_CreateSuperpositon2.Click += new System.EventHandler(this.toolStripMenuItem_CreateSuperpositon2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 2;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // tabPage_Options
            // 
            this.tabPage_Options.Controls.Add(this.axisOptions1);
            this.tabPage_Options.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Options.Name = "tabPage_Options";
            this.tabPage_Options.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Options.Size = new System.Drawing.Size(796, 386);
            this.tabPage_Options.TabIndex = 1;
            this.tabPage_Options.Text = "Options";
            this.tabPage_Options.UseVisualStyleBackColor = true;
            // 
            // axisOptions1
            // 
            this.axisOptions1.CycleElapseVisible = false;
            this.axisOptions1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisOptions1.EndDegrees = 720D;
            this.axisOptions1.EndDegreesMinimum = -7200D;
            this.axisOptions1.Location = new System.Drawing.Point(3, 3);
            this.axisOptions1.Name = "axisOptions1";
            this.axisOptions1.Padding = new System.Windows.Forms.Padding(4);
            this.axisOptions1.RangeVisible = false;
            this.axisOptions1.RepresentationVisible = false;
            this.axisOptions1.ShowStrokeElapseCyclically = true;
            this.axisOptions1.Size = new System.Drawing.Size(790, 380);
            this.axisOptions1.TabIndex = 0;
            this.axisOptions1.CustomRangeChanged += new System.EventHandler(this.axisOptions1_CustomRangeChanged);
            this.axisOptions1.StartDegreesChanged += new System.EventHandler(this.axisOptions1_StartDegreesChanged);
            this.axisOptions1.EndDegreesChanged += new System.EventHandler(this.axisOptions1_EndDegreesChanged);
            this.axisOptions1.ShowElapsedStrokeChanged += new System.EventHandler(this.axisOptions1_ShowElapsedStrokeChanged);
            this.axisOptions1.ShowStrokeElapseCyclicallyChanged += new System.EventHandler(this.axisOptions1_ShowStrokeElapseCyclicallyChanged);
            this.axisOptions1.ShowValuesInPIChanged += new System.EventHandler(this.axisOptions1_ShowValuesInPIChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // colorPicker1
            // 
            this.colorPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorPicker1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorPicker1.EmbeddedColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Gray,
        System.Drawing.Color.Silver,
        System.Drawing.Color.Brown,
        System.Drawing.Color.Chocolate,
        System.Drawing.Color.SandyBrown,
        System.Drawing.Color.DarkRed,
        System.Drawing.Color.Crimson,
        System.Drawing.Color.Red,
        System.Drawing.Color.Tomato,
        System.Drawing.Color.DarkOrange,
        System.Drawing.Color.Goldenrod,
        System.Drawing.Color.Gold,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.MediumSeaGreen,
        System.Drawing.Color.LawnGreen,
        System.Drawing.Color.GreenYellow,
        System.Drawing.Color.Teal,
        System.Drawing.Color.CadetBlue,
        System.Drawing.Color.SkyBlue,
        System.Drawing.Color.PaleTurquoise,
        System.Drawing.Color.Navy,
        System.Drawing.Color.MediumBlue,
        System.Drawing.Color.RoyalBlue,
        System.Drawing.Color.CornflowerBlue,
        System.Drawing.Color.DarkSlateBlue,
        System.Drawing.Color.MediumSlateBlue,
        System.Drawing.Color.Purple,
        System.Drawing.Color.Orchid,
        System.Drawing.Color.MediumVioletRed,
        System.Drawing.Color.HotPink,
        System.Drawing.Color.LightPink};
            this.colorPicker1.FormattingEnabled = true;
            this.colorPicker1.Location = new System.Drawing.Point(733, 0);
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.Size = new System.Drawing.Size(71, 21);
            this.colorPicker1.TabIndex = 3;
            this.colorPicker1.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Engine designer Analyzer files (*.xml)|*.xml";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Engine designer Analyzer files (*.xml)|*.xml";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // toolStripMenuItem_Load
            // 
            this.toolStripMenuItem_Load.Image = global::EngineDesigner.Properties.Resources.folder16x16;
            this.toolStripMenuItem_Load.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_Load.Name = "toolStripMenuItem_Load";
            this.toolStripMenuItem_Load.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_Load.Text = "Load...";
            this.toolStripMenuItem_Load.Click += new System.EventHandler(this.toolStripMenuItem_Load_Click);
            // 
            // toolStripMenuItem_Save
            // 
            this.toolStripMenuItem_Save.Image = global::EngineDesigner.Properties.Resources.disk_blue16x16;
            this.toolStripMenuItem_Save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_Save.Name = "toolStripMenuItem_Save";
            this.toolStripMenuItem_Save.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_Save.Text = "Save...";
            this.toolStripMenuItem_Save.Click += new System.EventHandler(this.toolStripMenuItem_Save_Click);
            // 
            // toolStripDropDownButton_LoadSave
            // 
            this.toolStripDropDownButton_LoadSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Load,
            this.toolStripMenuItem_Save});
            this.toolStripDropDownButton_LoadSave.Image = global::EngineDesigner.Properties.Resources.disk_blue16x16;
            this.toolStripDropDownButton_LoadSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton_LoadSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_LoadSave.Name = "toolStripDropDownButton_LoadSave";
            this.toolStripDropDownButton_LoadSave.Size = new System.Drawing.Size(97, 22);
            this.toolStripDropDownButton_LoadSave.Text = "Load / Save";
            // 
            // Form_Analyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 436);
            this.Controls.Add(this.colorPicker1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(380, 268);
            this.Name = "Form_Analyzer";
            this.Text = "Analyzer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Chart.ResumeLayout(false);
            this.contextMenuStrip_LegendAdditionalFeatures.ResumeLayout(false);
            this.tabPage_Options.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Chart;
        private System.Windows.Forms.TabPage tabPage_Options;
        private EngineDesigner.Environment.Controls.Charting.MultiFunctionChart multiFunctionChart1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_LegendAdditionalFeatures;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CreateSuperpositon2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_RemoveSelectedFunctions;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_Redraw;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_Aggregates;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton_AddAFunction;
        private EngineDesigner.Environment.Controls.ColorPicker colorPicker1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_ClearChart;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CreateSuperpositon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CreateAverage2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CreateAverage;
        private EngineDesigner.Controls.Charting.AxisOptions axisOptions1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_RPM;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Status;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_LoadSave;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Load;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Save;
    }
}