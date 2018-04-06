using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using EngineDesigner.Common.Definitions;
using System.Globalization;
using EngineDesigner.Environment.Controls;
using EngineDesigner.Environment.Controls.Charting;
using EngineDesigner.Controls.Editors;

namespace EngineDesigner.MainForms
{
    internal partial class Form_MainFunction : Form_Main
    {
        private Function function;



        public Form_MainFunction()
            : this(null, null)
        {
        }
        public Form_MainFunction(Function _function)
            : this(null, _function)
        {
        }
        public Form_MainFunction(FileInfo _fileInfo)
            : this(_fileInfo, null)
        {
        }
        public Form_MainFunction(FileInfo _fileInfo, Function _function)
            : base(_fileInfo, _function)
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                #region "Menus & Toolbars"
                //this.menuStrip2.Visible = false;
                //this.toolStrip2.Visible = false;
                //this.statusStrip2.Visible = false;


                //if (!ToolStripManager.Merge(this.menuStrip2, base.MenuStrip1))
                //{
                //    //throw new Exception();
                //}

                //if (!ToolStripManager.Merge(this.toolStrip2, base.ToolStrip1))
                //{
                //    throw new Exception();
                //}
                if (!this.inputFunctionChart1.MergeMenuStripWith(base.MenuStrip1))
                {
                    throw new Exception();
                }
                if (!this.inputFunctionChart1.MergeToolStripWith(base.ToolStrip1))
                {
                    throw new Exception();
                }
                #endregion "Menus & Toolbars"
            }
        }
        private void Form_MainFunction_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.numericTextBox_MinimumX.Value = this.inputFunctionChart1.BaseAxisX.Minimum;
                this.numericTextBox_MaximumX.Value = this.inputFunctionChart1.BaseAxisX.Maximum;
                this.numericTextBox_MinimumY.Value = this.inputFunctionChart1.BaseAxisY.Minimum;
                this.numericTextBox_MaximumY.Value = this.inputFunctionChart1.BaseAxisY.Maximum;

                this.PutFunctionToChart(this.function);
                this.PutFunctionToEditor(this.function);
            }
        }



        protected override bool SaveFile(FileInfo _fileInfo)
        {
            try
            {
                this.function.Save(_fileInfo.FullName);
                return true;
            }
            catch (Exception _exception)
            {
                EngineDesigner.Common.Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                return false;
            }
        }
        protected override void LoadFileOrObject(FileInfo _fileInfo, object _object)
        {
            if (_fileInfo.Exists)
            {
                this.function = Function.From(_fileInfo.FullName);
            }
            else if (_object != null)
            {
                this.function = (Function)_object;
                base.changesSaved = false;
            }
            else
            {
                this.function = Function.Empty;
            }
        }
        protected override string FileTypeFilter
        {
            get
            {
                return "Engine designer Function files (*.xml)|*.xml";
            }
        }




        private void numericTextBox_MinimumX_ValueChanged(object sender, EventArgs e)
        {
            NumericTextBox _numericTextBox = (NumericTextBox)sender;
            this.inputFunctionChart1.BaseAxisX.Minimum = _numericTextBox.Value;
        }
        private void numericTextBox_MaximumX_ValueChanged(object sender, EventArgs e)
        {
            NumericTextBox _numericTextBox = (NumericTextBox)sender;
            this.inputFunctionChart1.BaseAxisX.Maximum = _numericTextBox.Value;
        }
        private void numericTextBox_MinimumY_ValueChanged(object sender, EventArgs e)
        {
            NumericTextBox _numericTextBox = (NumericTextBox)sender;
            this.inputFunctionChart1.BaseAxisY.Minimum = _numericTextBox.Value;
        }
        private void numericTextBox_MaximumY_ValueChanged(object sender, EventArgs e)
        {
            NumericTextBox _numericTextBox = (NumericTextBox)sender;
            this.inputFunctionChart1.BaseAxisY.Maximum = _numericTextBox.Value;
        }



        private void checkBox_Interpolated_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox = (CheckBox)sender;

            if (_checkBox.Checked)
            {
                this.inputFunctionChart1.ChartType = FunctionChartType.SPLINE_WITH_MARKERS;
            }
            else
            {
                this.inputFunctionChart1.ChartType = FunctionChartType.LINE_WITH_MARKERS;
            }


            this.PutFunctionToChart(this.function);
        }



        private void inputFunctionChart1_NewFunctionGenerated(object sender, FunctionGeneratedEventArgs e)
        {
            this.function = e.NewFunction;
            this.PutFunctionToEditor(this.function);

            base.changesSaved = false;
        }

        private void functionEditor1_EditedFunctionChanged(object sender, EditedFunctionChangedEventArgs e)
        {
            this.function = e.Function;
            this.PutFunctionToChart(this.function);

            base.changesSaved = false;
        }


        private void PutFunctionToChart(Function _function)
        {
            this.inputFunctionChart1.SeriesCollection.Clear();
            this.inputFunctionChart1.DrawFunction(this.function);


            if (!double.IsNaN(this.numericTextBox_MinimumX.Value))
            {
                this.inputFunctionChart1.BaseAxisX.Minimum = this.numericTextBox_MinimumX.Value;
            }
            if (!double.IsNaN(this.numericTextBox_MaximumX.Value))
            {
                this.inputFunctionChart1.BaseAxisX.Maximum = this.numericTextBox_MaximumX.Value;
            }
            if (!double.IsNaN(this.numericTextBox_MinimumY.Value))
            {
                this.inputFunctionChart1.BaseAxisY.Minimum = this.numericTextBox_MinimumY.Value;
            }
            if (!double.IsNaN(this.numericTextBox_MaximumY.Value))
            {
                this.inputFunctionChart1.BaseAxisY.Maximum = this.numericTextBox_MaximumY.Value;
            }
        }
        private void PutFunctionToEditor(Function _function)
        {
            this.functionEditor1.Function = this.function;
        }

    }

}
