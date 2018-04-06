namespace EngineDesigner.Wizards.NewEngine
{
    partial class Form_NewEngineWizard_ConnectingRod_MassAndMassDistribution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_NewEngineWizard_ConnectingRod_MassAndMassDistribution));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_ConnectingRodMass = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.shape1 = new EngineDesigner.Environment.Controls.Shape();
            this.trackBar_MassAndDistanceDistributionPercentage = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labe_MassAndDistanceDistributionPercentage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ConnectingRodMass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_MassAndDistanceDistributionPercentage)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(280, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(533, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select the connecting rod\'s mass and adjust the mass and distance distribution pe" +
                "rcentages:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mass (g):";
            // 
            // numericUpDown_ConnectingRodMass
            // 
            this.numericUpDown_ConnectingRodMass.DecimalPlaces = 2;
            this.numericUpDown_ConnectingRodMass.Location = new System.Drawing.Point(367, 108);
            this.numericUpDown_ConnectingRodMass.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_ConnectingRodMass.Name = "numericUpDown_ConnectingRodMass";
            this.numericUpDown_ConnectingRodMass.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_ConnectingRodMass.TabIndex = 6;
            this.numericUpDown_ConnectingRodMass.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_ConnectingRodMass.ValueChanged += new System.EventHandler(this.numericUpDown_ConnectingRodMass_ValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(339, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(314, 27);
            this.label4.TabIndex = 5;
            this.label4.Text = "The mass of the connecting rod will contribute to both rotating and reciprocating" +
                " forces.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(311, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mass and distance distribution (%):";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(339, 309);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(314, 67);
            this.label11.TabIndex = 5;
            this.label11.Text = resources.GetString("label11.Text");
            // 
            // shape1
            // 
            this.shape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shape1.Location = new System.Drawing.Point(283, 192);
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = EngineDesigner.Environment.Controls.ShapeType.LINE;
            this.shape1.Size = new System.Drawing.Size(573, 1);
            this.shape1.TabIndex = 7;
            // 
            // trackBar_MassAndDistanceDistributionPercentage
            // 
            this.trackBar_MassAndDistanceDistributionPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_MassAndDistanceDistributionPercentage.AutoSize = false;
            this.trackBar_MassAndDistanceDistributionPercentage.Location = new System.Drawing.Point(342, 247);
            this.trackBar_MassAndDistanceDistributionPercentage.Maximum = 100;
            this.trackBar_MassAndDistanceDistributionPercentage.Minimum = -100;
            this.trackBar_MassAndDistanceDistributionPercentage.Name = "trackBar_MassAndDistanceDistributionPercentage";
            this.trackBar_MassAndDistanceDistributionPercentage.Size = new System.Drawing.Size(471, 28);
            this.trackBar_MassAndDistanceDistributionPercentage.TabIndex = 8;
            this.trackBar_MassAndDistanceDistributionPercentage.TickFrequency = 100;
            this.trackBar_MassAndDistanceDistributionPercentage.Value = -33;
            this.trackBar_MassAndDistanceDistributionPercentage.Scroll += new System.EventHandler(this.trackBar_MassAndDistanceDistributionPercentage_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Rotating";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(395, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Reciprocating";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labe_MassAndDistanceDistributionPercentage, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(342, 277);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(471, 16);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // labe_MassAndDistanceDistributionPercentage
            // 
            this.labe_MassAndDistanceDistributionPercentage.AutoSize = true;
            this.labe_MassAndDistanceDistributionPercentage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labe_MassAndDistanceDistributionPercentage.Location = new System.Drawing.Point(160, 0);
            this.labe_MassAndDistanceDistributionPercentage.Name = "labe_MassAndDistanceDistributionPercentage";
            this.labe_MassAndDistanceDistributionPercentage.Size = new System.Drawing.Size(151, 16);
            this.labe_MassAndDistanceDistributionPercentage.TabIndex = 5;
            this.labe_MassAndDistanceDistributionPercentage.Text = "33";
            this.labe_MassAndDistanceDistributionPercentage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form_NewEngineWizard_ConnectingRod_MassAndMassDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.shape1);
            this.Controls.Add(this.numericUpDown_ConnectingRodMass);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar_MassAndDistanceDistributionPercentage);
            this.Controls.Add(this.label11);
            this.CurrentStep = 7;
            this.Name = "Form_NewEngineWizard_ConnectingRod_MassAndMassDistribution";
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.trackBar_MassAndDistanceDistributionPercentage, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.numericUpDown_ConnectingRodMass, 0);
            this.Controls.SetChildIndex(this.shape1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ConnectingRodMass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_MassAndDistanceDistributionPercentage)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_ConnectingRodMass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private EngineDesigner.Environment.Controls.Shape shape1;
        private System.Windows.Forms.TrackBar trackBar_MassAndDistanceDistributionPercentage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labe_MassAndDistanceDistributionPercentage;
    }
}
