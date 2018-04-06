namespace EngineDesigner.Environment.Controls.Charting
{
    partial class MultiFunctionChart
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
            this.multiFunctionChartLegend1 = new EngineDesigner.Environment.Controls.Charting.MultiFunctionChartLegend();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Raised;
            this.chart1.Location = new System.Drawing.Point(0, 24);
            this.chart1.Size = new System.Drawing.Size(484, 198);
            // WARNING: This generates an exception in design time, but is ok (select 'Ignore and continue').
            this.multiFunctionChartLegend1.OwnerChart = this;
            // 
            // multiFunctionChartLegend1
            // 
            this.multiFunctionChartLegend1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.multiFunctionChartLegend1.AutoSize = true;
            this.multiFunctionChartLegend1.Location = new System.Drawing.Point(314, 12);
            this.multiFunctionChartLegend1.Name = "multiFunctionChartLegend1";
            this.multiFunctionChartLegend1.Size = new System.Drawing.Size(150, 16);
            this.multiFunctionChartLegend1.TabIndex = 1;
            this.multiFunctionChartLegend1.Tag = "";
            this.multiFunctionChartLegend1.LegendContextMenuOpening += new System.EventHandler(this.multiFunctionChartLegend1_LegendContextMenuOpening);
            this.multiFunctionChartLegend1.LegendContextMenuClosing += new System.EventHandler(this.multiFunctionChartLegend1_LegendContextMenuClosing);
            this.multiFunctionChartLegend1.LegendItemToolTipShowing += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.CustomLegendItemEventArgs>(this.multiFunctionChartLegend1_LegendItemToolTipShowing);
            this.multiFunctionChartLegend1.CustomLegendsSelectedChanged += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.CustomLegendEventArgs>(this.multiFunctionChartLegend1_CustomLegendsSelectedChanged);
            // 
            // MultiFunctionChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.multiFunctionChartLegend1);
            this.Name = "MultiFunctionChart";
            this.Size = new System.Drawing.Size(484, 222);
            this.Controls.SetChildIndex(this.chart1, 0);
            this.Controls.SetChildIndex(this.multiFunctionChartLegend1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MultiFunctionChartLegend multiFunctionChartLegend1;


    }
}
