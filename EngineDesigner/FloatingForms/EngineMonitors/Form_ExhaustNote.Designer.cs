namespace EngineDesigner.FloatingForms.EngineMonitors
{
    partial class Form_ExhaustNote
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
            this.trackBar_Volume = new System.Windows.Forms.TrackBar();
            this.dxPlayer1 = new EngineDesigner.Media.Sound.DXPlayer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox_Mute = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radioButton_RealTime = new System.Windows.Forms.RadioButton();
            this.radioButton_Simulation = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Volume)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar_Volume
            // 
            this.trackBar_Volume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_Volume.Location = new System.Drawing.Point(53, 3);
            this.trackBar_Volume.Maximum = 100;
            this.trackBar_Volume.Name = "trackBar_Volume";
            this.trackBar_Volume.Size = new System.Drawing.Size(120, 24);
            this.trackBar_Volume.TabIndex = 0;
            this.trackBar_Volume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Volume.Value = 75;
            this.trackBar_Volume.ValueChanged += new System.EventHandler(this.trackBar_Volume_ValueChanged);
            this.dxPlayer1.Owner = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(184, 56);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.trackBar_Volume);
            this.tabPage1.Controls.Add(this.checkBox_Mute);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(176, 30);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Volume";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox_Mute
            // 
            this.checkBox_Mute.AutoSize = true;
            this.checkBox_Mute.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_Mute.Location = new System.Drawing.Point(3, 3);
            this.checkBox_Mute.Name = "checkBox_Mute";
            this.checkBox_Mute.Size = new System.Drawing.Size(50, 24);
            this.checkBox_Mute.TabIndex = 1;
            this.checkBox_Mute.Text = "Mute";
            this.checkBox_Mute.UseVisualStyleBackColor = true;
            this.checkBox_Mute.CheckedChanged += new System.EventHandler(this.checkBox_Mute_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(176, 30);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Options";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.radioButton_RealTime, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioButton_Simulation, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(170, 24);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // radioButton_RealTime
            // 
            this.radioButton_RealTime.AutoSize = true;
            this.radioButton_RealTime.Checked = true;
            this.radioButton_RealTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton_RealTime.Location = new System.Drawing.Point(3, 3);
            this.radioButton_RealTime.Name = "radioButton_RealTime";
            this.radioButton_RealTime.Size = new System.Drawing.Size(79, 18);
            this.radioButton_RealTime.TabIndex = 0;
            this.radioButton_RealTime.TabStop = true;
            this.radioButton_RealTime.Text = "Real time";
            this.radioButton_RealTime.UseVisualStyleBackColor = true;
            this.radioButton_RealTime.CheckedChanged += new System.EventHandler(this.radioButton_RealTime_CheckedChanged);
            // 
            // radioButton_Simulation
            // 
            this.radioButton_Simulation.AutoSize = true;
            this.radioButton_Simulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton_Simulation.Location = new System.Drawing.Point(88, 3);
            this.radioButton_Simulation.Name = "radioButton_Simulation";
            this.radioButton_Simulation.Size = new System.Drawing.Size(79, 18);
            this.radioButton_Simulation.TabIndex = 0;
            this.radioButton_Simulation.Text = "Simulation";
            this.radioButton_Simulation.UseVisualStyleBackColor = true;
            this.radioButton_Simulation.CheckedChanged += new System.EventHandler(this.radioButton_Simulation_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form_ExhaustNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 56);
            this.Controls.Add(this.tabControl1);
            this.MaximumSize = new System.Drawing.Size(384, 90);
            this.MinimumSize = new System.Drawing.Size(200, 90);
            this.Name = "Form_ExhaustNote";
            this.Text = "Exhaust note";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Volume)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar_Volume;
        private EngineDesigner.Media.Sound.DXPlayer dxPlayer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButton_RealTime;
        private System.Windows.Forms.RadioButton radioButton_Simulation;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox checkBox_Mute;
    }
}