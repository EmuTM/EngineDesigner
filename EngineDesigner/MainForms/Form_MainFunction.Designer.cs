namespace EngineDesigner.MainForms
{
    partial class Form_MainFunction
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupPanel1 = new EngineDesigner.Environment.Controls.GroupPanel();
            this.functionEditor1 = new EngineDesigner.Controls.Editors.FunctionEditor();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Function = new System.Windows.Forms.TabPage();
            this.inputFunctionChart1 = new EngineDesigner.Environment.Controls.Charting.InputFunctionChart();
            this.tabPage_Options = new System.Windows.Forms.TabPage();
            this.groupBox_Interpolation = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox_Interpolated = new System.Windows.Forms.CheckBox();
            this.groupBox_RangeY = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_RangeY = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.numericTextBox_MinimumY = new EngineDesigner.Environment.Controls.NumericTextBox();
            this.label_MinimumY = new System.Windows.Forms.Label();
            this.label_MaximumY = new System.Windows.Forms.Panel();
            this.numericTextBox_MaximumY = new EngineDesigner.Environment.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox_RangeX = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_RangeX = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericTextBox_MinimumX = new EngineDesigner.Environment.Controls.NumericTextBox();
            this.label_MinimumX = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numericTextBox_MaximumX = new EngineDesigner.Environment.Controls.NumericTextBox();
            this.label_MaximumX = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Function.SuspendLayout();
            this.tabPage_Options.SuspendLayout();
            this.groupBox_Interpolation.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox_RangeY.SuspendLayout();
            this.tableLayoutPanel_RangeY.SuspendLayout();
            this.panel3.SuspendLayout();
            this.label_MaximumY.SuspendLayout();
            this.groupBox_RangeX.SuspendLayout();
            this.tableLayoutPanel_RangeX.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(784, 491);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 6;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupPanel1.Controls.Add(this.functionEditor1);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(257, 491);
            this.groupPanel1.TabIndex = 2;
            this.groupPanel1.Text = "Points";
            // 
            // functionEditor1
            // 
            this.functionEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionEditor1.Location = new System.Drawing.Point(0, 20);
            this.functionEditor1.Name = "functionEditor1";
            this.functionEditor1.Size = new System.Drawing.Size(255, 489);
            this.functionEditor1.TabIndex = 2;
            this.functionEditor1.EditedFunctionChanged += new System.EventHandler<EngineDesigner.Controls.Editors.EditedFunctionChangedEventArgs>(this.functionEditor1_EditedFunctionChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Function);
            this.tabControl1.Controls.Add(this.tabPage_Options);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(523, 491);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage_Function
            // 
            this.tabPage_Function.Controls.Add(this.inputFunctionChart1);
            this.tabPage_Function.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Function.Name = "tabPage_Function";
            this.tabPage_Function.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Function.Size = new System.Drawing.Size(515, 465);
            this.tabPage_Function.TabIndex = 0;
            this.tabPage_Function.Text = "Function";
            this.tabPage_Function.UseVisualStyleBackColor = true;
            // 
            // inputFunctionChart1
            // 
            this.inputFunctionChart1.ChartMarkersColor = System.Drawing.Color.Empty;
            this.inputFunctionChart1.ChartMarkersSize = 7;
            this.inputFunctionChart1.ChartType = EngineDesigner.Environment.Controls.Charting.FunctionChartType.SPLINE_WITH_MARKERS;
            this.inputFunctionChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputFunctionChart1.Location = new System.Drawing.Point(3, 3);
            this.inputFunctionChart1.Name = "inputFunctionChart1";
            this.inputFunctionChart1.Size = new System.Drawing.Size(509, 459);
            this.inputFunctionChart1.TabIndex = 0;
            this.inputFunctionChart1.NewFunctionGenerated += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.FunctionGeneratedEventArgs>(this.inputFunctionChart1_NewFunctionGenerated);
            // 
            // tabPage_Options
            // 
            this.tabPage_Options.Controls.Add(this.groupBox_Interpolation);
            this.tabPage_Options.Controls.Add(this.groupBox_RangeY);
            this.tabPage_Options.Controls.Add(this.groupBox_RangeX);
            this.tabPage_Options.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Options.Name = "tabPage_Options";
            this.tabPage_Options.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Options.Size = new System.Drawing.Size(515, 465);
            this.tabPage_Options.TabIndex = 1;
            this.tabPage_Options.Text = "Options";
            this.tabPage_Options.UseVisualStyleBackColor = true;
            // 
            // groupBox_Interpolation
            // 
            this.groupBox_Interpolation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Interpolation.Controls.Add(this.tableLayoutPanel1);
            this.groupBox_Interpolation.Location = new System.Drawing.Point(17, 238);
            this.groupBox_Interpolation.Name = "groupBox_Interpolation";
            this.groupBox_Interpolation.Size = new System.Drawing.Size(480, 79);
            this.groupBox_Interpolation.TabIndex = 4;
            this.groupBox_Interpolation.TabStop = false;
            this.groupBox_Interpolation.Text = "Interpolation";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(17, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(447, 32);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox_Interpolated);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(441, 26);
            this.panel4.TabIndex = 4;
            // 
            // checkBox_Interpolated
            // 
            this.checkBox_Interpolated.AutoSize = true;
            this.checkBox_Interpolated.Checked = true;
            this.checkBox_Interpolated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Interpolated.Location = new System.Drawing.Point(3, 5);
            this.checkBox_Interpolated.Name = "checkBox_Interpolated";
            this.checkBox_Interpolated.Size = new System.Drawing.Size(82, 17);
            this.checkBox_Interpolated.TabIndex = 0;
            this.checkBox_Interpolated.Text = "Interpolated";
            this.checkBox_Interpolated.UseVisualStyleBackColor = true;
            this.checkBox_Interpolated.CheckedChanged += new System.EventHandler(this.checkBox_Interpolated_CheckedChanged);
            // 
            // groupBox_RangeY
            // 
            this.groupBox_RangeY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_RangeY.Controls.Add(this.tableLayoutPanel_RangeY);
            this.groupBox_RangeY.Location = new System.Drawing.Point(17, 131);
            this.groupBox_RangeY.Name = "groupBox_RangeY";
            this.groupBox_RangeY.Size = new System.Drawing.Size(480, 79);
            this.groupBox_RangeY.TabIndex = 3;
            this.groupBox_RangeY.TabStop = false;
            this.groupBox_RangeY.Text = "Range Y";
            // 
            // tableLayoutPanel_RangeY
            // 
            this.tableLayoutPanel_RangeY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_RangeY.ColumnCount = 3;
            this.tableLayoutPanel_RangeY.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_RangeY.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel_RangeY.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_RangeY.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel_RangeY.Controls.Add(this.label_MaximumY, 2, 0);
            this.tableLayoutPanel_RangeY.Location = new System.Drawing.Point(17, 30);
            this.tableLayoutPanel_RangeY.Name = "tableLayoutPanel_RangeY";
            this.tableLayoutPanel_RangeY.RowCount = 1;
            this.tableLayoutPanel_RangeY.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_RangeY.Size = new System.Drawing.Size(447, 32);
            this.tableLayoutPanel_RangeY.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.numericTextBox_MinimumY);
            this.panel3.Controls.Add(this.label_MinimumY);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(201, 26);
            this.panel3.TabIndex = 4;
            // 
            // numericTextBox_MinimumY
            // 
            this.numericTextBox_MinimumY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTextBox_MinimumY.Location = new System.Drawing.Point(74, 3);
            this.numericTextBox_MinimumY.Name = "numericTextBox_MinimumY";
            this.numericTextBox_MinimumY.Size = new System.Drawing.Size(124, 20);
            this.numericTextBox_MinimumY.TabIndex = 1;
            this.numericTextBox_MinimumY.Text = "0";
            this.numericTextBox_MinimumY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox_MinimumY.ValueChanged += new System.EventHandler(this.numericTextBox_MinimumY_ValueChanged);
            // 
            // label_MinimumY
            // 
            this.label_MinimumY.AutoSize = true;
            this.label_MinimumY.Location = new System.Drawing.Point(3, 6);
            this.label_MinimumY.Name = "label_MinimumY";
            this.label_MinimumY.Size = new System.Drawing.Size(48, 13);
            this.label_MinimumY.TabIndex = 0;
            this.label_MinimumY.Text = "Minimum";
            // 
            // label_MaximumY
            // 
            this.label_MaximumY.Controls.Add(this.numericTextBox_MaximumY);
            this.label_MaximumY.Controls.Add(this.label4);
            this.label_MaximumY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_MaximumY.Location = new System.Drawing.Point(242, 3);
            this.label_MaximumY.Name = "label_MaximumY";
            this.label_MaximumY.Size = new System.Drawing.Size(202, 26);
            this.label_MaximumY.TabIndex = 5;
            // 
            // numericTextBox_MaximumY
            // 
            this.numericTextBox_MaximumY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTextBox_MaximumY.Location = new System.Drawing.Point(75, 3);
            this.numericTextBox_MaximumY.Name = "numericTextBox_MaximumY";
            this.numericTextBox_MaximumY.Size = new System.Drawing.Size(124, 20);
            this.numericTextBox_MaximumY.TabIndex = 1;
            this.numericTextBox_MaximumY.Text = "0";
            this.numericTextBox_MaximumY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox_MaximumY.ValueChanged += new System.EventHandler(this.numericTextBox_MaximumY_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Maximum";
            // 
            // groupBox_RangeX
            // 
            this.groupBox_RangeX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_RangeX.Controls.Add(this.tableLayoutPanel_RangeX);
            this.groupBox_RangeX.Location = new System.Drawing.Point(17, 24);
            this.groupBox_RangeX.Name = "groupBox_RangeX";
            this.groupBox_RangeX.Size = new System.Drawing.Size(480, 79);
            this.groupBox_RangeX.TabIndex = 3;
            this.groupBox_RangeX.TabStop = false;
            this.groupBox_RangeX.Text = "Range X";
            // 
            // tableLayoutPanel_RangeX
            // 
            this.tableLayoutPanel_RangeX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_RangeX.ColumnCount = 3;
            this.tableLayoutPanel_RangeX.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_RangeX.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel_RangeX.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_RangeX.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel_RangeX.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel_RangeX.Location = new System.Drawing.Point(17, 30);
            this.tableLayoutPanel_RangeX.Name = "tableLayoutPanel_RangeX";
            this.tableLayoutPanel_RangeX.RowCount = 1;
            this.tableLayoutPanel_RangeX.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_RangeX.Size = new System.Drawing.Size(447, 32);
            this.tableLayoutPanel_RangeX.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericTextBox_MinimumX);
            this.panel1.Controls.Add(this.label_MinimumX);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 26);
            this.panel1.TabIndex = 4;
            // 
            // numericTextBox_MinimumX
            // 
            this.numericTextBox_MinimumX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTextBox_MinimumX.Location = new System.Drawing.Point(74, 3);
            this.numericTextBox_MinimumX.Name = "numericTextBox_MinimumX";
            this.numericTextBox_MinimumX.Size = new System.Drawing.Size(124, 20);
            this.numericTextBox_MinimumX.TabIndex = 1;
            this.numericTextBox_MinimumX.Text = "0";
            this.numericTextBox_MinimumX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox_MinimumX.ValueChanged += new System.EventHandler(this.numericTextBox_MinimumX_ValueChanged);
            // 
            // label_MinimumX
            // 
            this.label_MinimumX.AutoSize = true;
            this.label_MinimumX.Location = new System.Drawing.Point(3, 6);
            this.label_MinimumX.Name = "label_MinimumX";
            this.label_MinimumX.Size = new System.Drawing.Size(48, 13);
            this.label_MinimumX.TabIndex = 0;
            this.label_MinimumX.Text = "Minimum";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numericTextBox_MaximumX);
            this.panel2.Controls.Add(this.label_MaximumX);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(242, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(202, 26);
            this.panel2.TabIndex = 5;
            // 
            // numericTextBox_MaximumX
            // 
            this.numericTextBox_MaximumX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTextBox_MaximumX.Location = new System.Drawing.Point(75, 3);
            this.numericTextBox_MaximumX.Name = "numericTextBox_MaximumX";
            this.numericTextBox_MaximumX.Size = new System.Drawing.Size(124, 20);
            this.numericTextBox_MaximumX.TabIndex = 1;
            this.numericTextBox_MaximumX.Text = "0";
            this.numericTextBox_MaximumX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox_MaximumX.ValueChanged += new System.EventHandler(this.numericTextBox_MaximumX_ValueChanged);
            // 
            // label_MaximumX
            // 
            this.label_MaximumX.AutoSize = true;
            this.label_MaximumX.Location = new System.Drawing.Point(3, 6);
            this.label_MaximumX.Name = "label_MaximumX";
            this.label_MaximumX.Size = new System.Drawing.Size(51, 13);
            this.label_MaximumX.TabIndex = 0;
            this.label_MaximumX.Text = "Maximum";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form_MainFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_MainFunction";
            this.Load += new System.EventHandler(this.Form_MainFunction_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Function.ResumeLayout(false);
            this.tabPage_Options.ResumeLayout(false);
            this.groupBox_Interpolation.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox_RangeY.ResumeLayout(false);
            this.tableLayoutPanel_RangeY.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.label_MaximumY.ResumeLayout(false);
            this.label_MaximumY.PerformLayout();
            this.groupBox_RangeX.ResumeLayout(false);
            this.tableLayoutPanel_RangeX.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private EngineDesigner.Environment.Controls.GroupPanel groupPanel1;
        private EngineDesigner.Controls.Editors.FunctionEditor functionEditor1;
        private EngineDesigner.Environment.Controls.Charting.InputFunctionChart inputFunctionChart1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Function;
        private System.Windows.Forms.TabPage tabPage_Options;
        private System.Windows.Forms.GroupBox groupBox_RangeX;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_RangeX;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_MinimumX;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_MaximumX;
        private System.Windows.Forms.GroupBox groupBox_RangeY;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_RangeY;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label_MinimumY;
        private System.Windows.Forms.Panel label_MaximumY;
        private System.Windows.Forms.Label label4;
        private EngineDesigner.Environment.Controls.NumericTextBox numericTextBox_MinimumY;
        private EngineDesigner.Environment.Controls.NumericTextBox numericTextBox_MaximumY;
        private EngineDesigner.Environment.Controls.NumericTextBox numericTextBox_MinimumX;
        private EngineDesigner.Environment.Controls.NumericTextBox numericTextBox_MaximumX;
        private System.Windows.Forms.GroupBox groupBox_Interpolation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBox_Interpolated;


    }
}
