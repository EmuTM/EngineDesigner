namespace EngineDesigner.Wizards.NewFunction
{
    partial class Form_NewFunctionWizard_AlterFunction
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
            this.inputFunctionChart1 = new EngineDesigner.Environment.Controls.Charting.InputFunctionChart();
            this.SuspendLayout();
            // 
            // inputFunctionChart1
            // 
            this.inputFunctionChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.inputFunctionChart1.ChartBordersMovableX = false;
            this.inputFunctionChart1.ChartBordersMovableY = false;
            this.inputFunctionChart1.ChartMarkersColor = System.Drawing.Color.Empty;
            this.inputFunctionChart1.ChartType = EngineDesigner.Environment.Controls.Charting.FunctionChartType.SPLINE_WITH_MARKERS;
            this.inputFunctionChart1.Location = new System.Drawing.Point(280, 68);
            this.inputFunctionChart1.Name = "inputFunctionChart1";
            this.inputFunctionChart1.Size = new System.Drawing.Size(576, 388);
            this.inputFunctionChart1.TabIndex = 6;
            this.inputFunctionChart1.PointMoveAllowed += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.PointMoveAllowedEventArgs>(this.inputFunctionChart1_PointMoveAllowed);
            this.inputFunctionChart1.NewFunctionGenerated += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.FunctionGeneratedEventArgs>(this.inputFunctionChart1_NewFunctionGenerated);
            this.inputFunctionChart1.PointAddAllowed += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.PointAddAllowedEventArgs>(this.inputFunctionChart1_PointAddAllowed);
            this.inputFunctionChart1.PointDeleteAllowed += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.PointDeleteAllowedEventArgs>(this.inputFunctionChart1_PointDeleteAllowed);
            // 
            // Form_NewFunctionWizard_AlterFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.inputFunctionChart1);
            this.CurrentStep = 5;
            this.Name = "Form_NewFunctionWizard_AlterFunction";
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            this.Controls.SetChildIndex(this.inputFunctionChart1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private EngineDesigner.Environment.Controls.Charting.InputFunctionChart inputFunctionChart1;


    }
}
