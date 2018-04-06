namespace EngineDesigner.FloatingForms.EngineMonitors
{
    partial class Form_CycleDiagram
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
            this.cycleDiagram1 = new EngineDesigner.Controls.Charting.CycleDiagram();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Diagram = new System.Windows.Forms.TabPage();
            this.tabPage_Options = new System.Windows.Forms.TabPage();
            this.axisOptions1 = new EngineDesigner.Controls.Charting.AxisOptions();
            this.tabControl1.SuspendLayout();
            this.tabPage_Diagram.SuspendLayout();
            this.tabPage_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // cycleDiagram1
            // 
            this.cycleDiagram1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cycleDiagram1.Location = new System.Drawing.Point(3, 3);
            this.cycleDiagram1.Name = "cycleDiagram1";
            this.cycleDiagram1.Size = new System.Drawing.Size(509, 340);
            this.cycleDiagram1.TabIndex = 0;
            this.cycleDiagram1.ZoomAxisX = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Diagram);
            this.tabControl1.Controls.Add(this.tabPage_Options);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(523, 372);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_Diagram
            // 
            this.tabPage_Diagram.Controls.Add(this.cycleDiagram1);
            this.tabPage_Diagram.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Diagram.Name = "tabPage_Diagram";
            this.tabPage_Diagram.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Diagram.Size = new System.Drawing.Size(515, 346);
            this.tabPage_Diagram.TabIndex = 0;
            this.tabPage_Diagram.Text = "Diagram";
            this.tabPage_Diagram.UseVisualStyleBackColor = true;
            // 
            // tabPage_Options
            // 
            this.tabPage_Options.Controls.Add(this.axisOptions1);
            this.tabPage_Options.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Options.Name = "tabPage_Options";
            this.tabPage_Options.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Options.Size = new System.Drawing.Size(515, 346);
            this.tabPage_Options.TabIndex = 1;
            this.tabPage_Options.Text = "Options";
            this.tabPage_Options.UseVisualStyleBackColor = true;
            // 
            // axisOptions1
            // 
            this.axisOptions1.CustomRange = false;
            this.axisOptions1.CycleElapseEnabled = true;
            this.axisOptions1.CycleElapseVisible = true;
            this.axisOptions1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisOptions1.EndDegrees = 720;
            this.axisOptions1.EndDegreesMaximum = 7200;
            this.axisOptions1.EndDegreesMinimum = -7200;
            this.axisOptions1.Location = new System.Drawing.Point(3, 3);
            this.axisOptions1.Name = "axisOptions1";
            this.axisOptions1.Padding = new System.Windows.Forms.Padding(4);
            this.axisOptions1.RangeEnabled = true;
            this.axisOptions1.RangeVisible = true;
            this.axisOptions1.RepresentationEnabled = true;
            this.axisOptions1.RepresentationVisible = true;
            this.axisOptions1.ShowElapsedStroke = false;
            this.axisOptions1.ShowStrokeElapseCyclically = false;
            this.axisOptions1.ShowValuesInPI = false;
            this.axisOptions1.Size = new System.Drawing.Size(509, 340);
            this.axisOptions1.StartDegrees = 0;
            this.axisOptions1.StartDegreesMaximum = 7200;
            this.axisOptions1.StartDegreesMinimum = -7200;
            this.axisOptions1.TabIndex = 0;
            this.axisOptions1.ShowStrokeElapseCyclicallyChanged += new System.EventHandler(this.axisOptions1_ShowStrokeElapseCyclicallyChanged);
            this.axisOptions1.ShowElapsedStrokeChanged += new System.EventHandler(this.axisOptions1_ShowElapsedStrokeChanged);
            this.axisOptions1.ShowValuesInPIChanged += new System.EventHandler(this.axisOptions1_ShowValuesInPIChanged);
            this.axisOptions1.CustomRangeChanged += new System.EventHandler(this.axisOptions1_CustomRangeChanged);
            this.axisOptions1.EndDegreesChanged += new System.EventHandler(this.axisOptions1_EndDegreesChanged);
            this.axisOptions1.StartDegreesChanged += new System.EventHandler(this.axisOptions1_StartDegreesChanged);
            // 
            // Form_CycleDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 372);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(350, 246);
            this.Name = "Form_CycleDiagram";
            this.Text = "Cycle diagram";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Diagram.ResumeLayout(false);
            this.tabPage_Options.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EngineDesigner.Controls.Charting.CycleDiagram cycleDiagram1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Diagram;
        private System.Windows.Forms.TabPage tabPage_Options;
        private EngineDesigner.Controls.Charting.AxisOptions axisOptions1;
    }
}