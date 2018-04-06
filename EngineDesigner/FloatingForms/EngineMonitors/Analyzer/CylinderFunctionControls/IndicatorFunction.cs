using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using EngineDesigner.Common;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [DefaultEvent("IndicatorFunctionChanged")]
    internal partial class IndicatorFunction : UserControl
    {
        private event EventHandler<IndicatorFunctionEventArgs> indicatorFunctionChanged;
        public event EventHandler<IndicatorFunctionEventArgs> IndicatorFunctionChanged
        {
            add { indicatorFunctionChanged += value; }
            remove { indicatorFunctionChanged -= value; }
        }



        private string indicatorFunctionName = null;
        [DefaultValue(null)]
        public string IndicatorFunctionName
        {
            get { return this.indicatorFunctionName; }

            set
            {
                this.indicatorFunctionName = value;

                this.groupBox_IndicatorFunction.Text = string.Format(
                    "Indicator function - {0}",
                    this.indicatorFunctionName);
            }
        }

        private Function selectedIndicatorFunction = null;
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        public Function SelectedIndicatorFunction
        {
            get { return this.selectedIndicatorFunction; }
            set { this.selectedIndicatorFunction = value; }
        }

        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        public FileInfo SelectedIndicatorFunctionFile
        {
            get
            {
                if (!string.IsNullOrEmpty(this.textBox_IndicatorFunctionFilename.Text))
                {
                    return new FileInfo(this.textBox_IndicatorFunctionFilename.Text);
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (value != null)
                {
                    this.openFileDialog1.FileName = value.FullName;
                    this.textBox_IndicatorFunctionFilename.Text = value.FullName;
                }
                else
                {
                    this.openFileDialog1.FileName = string.Empty;
                    this.textBox_IndicatorFunctionFilename.Text = string.Empty;
                }
            }
        }

        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        public InterpolationMethodInfo SelectedInterpolationMethod
        {
            get { return (InterpolationMethodInfo)this.comboBox_InterpolateMissingValues.SelectedItem; }

            set
            {
                if (value != null)
                {
                    foreach (InterpolationMethodInfo _interpolationMethodInfo in this.comboBox_InterpolateMissingValues.Items)
                    {
                        if (_interpolationMethodInfo.Equals(value))
                        {
                            this.comboBox_InterpolateMissingValues.SelectedItem = _interpolationMethodInfo;
                        }
                    }
                }
                else
                {
                    this.comboBox_InterpolateMissingValues.SelectedIndex = -1;
                }
            }
        }



        public IndicatorFunction()
        {
            InitializeComponent();


            this.comboBox_InterpolateMissingValues.Items.Clear();
            this.comboBox_InterpolateMissingValues.Items.AddRange(InterpolationMethodInfo.GetAvailableInterpolationMethods());
            this.comboBox_InterpolateMissingValues.SelectedItem = this.comboBox_InterpolateMissingValues.Items[0];
        }



        private void button_SelectIndicatorFunctionFilename_Click(object sender, EventArgs e)
        {
            DialogResult _dialogResult = this.openFileDialog1.ShowDialog();

            if (_dialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    FileInfo _fileInfo = new FileInfo(openFileDialog1.FileName);

                    if (_fileInfo.Exists)
                    {
                        try
                        {
                            Function _function = Function.From(openFileDialog1.FileName);

                            this.textBox_IndicatorFunctionFilename.Text = openFileDialog1.FileName;
                            this.selectedIndicatorFunction = _function;

                            if (this.comboBox_InterpolateMissingValues.SelectedIndex == -1)
                            {
                                this.comboBox_InterpolateMissingValues.SelectedIndex = 0;
                            }
                        }
                        catch (Exception _exception)
                        {
                            Utility.Exception(
                                this,
                                _exception.Message,
                                _exception);
                        }
                    }
                }


                this.OnIndicatorFunctionChanged(
                    this.selectedIndicatorFunction,
                    (InterpolationMethodInfo)this.comboBox_InterpolateMissingValues.SelectedItem);
            }
        }
        private void comboBox_InterpolateMissingValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _comboBox = (ComboBox)sender;

            if (this.selectedIndicatorFunction != null)
            {
                this.OnIndicatorFunctionChanged(
                    this.selectedIndicatorFunction,
                    (InterpolationMethodInfo)_comboBox.SelectedItem);
            }
        }



        protected virtual void OnIndicatorFunctionChanged(Function _function, InterpolationMethodInfo _interpolationMethodInfo)
        {
            if (this.indicatorFunctionChanged != null)
            {
                this.indicatorFunctionChanged(
                    this,
                    new IndicatorFunctionEventArgs(
                        _function,
                        _interpolationMethodInfo));
            }
        }

    }


    internal class IndicatorFunctionEventArgs : EventArgs
    {
        public IndicatorFunctionEventArgs(Function _function, InterpolationMethodInfo _interpolationMethodInfo)
        {
            this.function = _function;
            this.interpolationMethodInfo = _interpolationMethodInfo;
        }


        private Function function;
        public Function @Function
        {
            get { return function; }
        }

        private InterpolationMethodInfo interpolationMethodInfo;
        public InterpolationMethodInfo @InterpolationMethodInfo
        {
            set { interpolationMethodInfo = value; }
            get { return interpolationMethodInfo; }
        }

    }

}
