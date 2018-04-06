using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using EngineDesigner.Common;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class Form_LoadIndicatorFunction : Form
    {
        [DefaultValue(null)]
        public Function SelectedIndicatorFunction
        {
            get { return this.indicatorFunction1.SelectedIndicatorFunction; }
        }
        [DefaultValue(null)]
        public InterpolationMethodInfo SelectedInterpolationMethod
        {
            get { return this.indicatorFunction1.SelectedInterpolationMethod; }
            set { this.indicatorFunction1.SelectedInterpolationMethod = value; }
        }
        [DefaultValue(null)]
        public FileInfo SelectedIndicatorFunctionFile
        {
            get { return this.indicatorFunction1.SelectedIndicatorFunctionFile; }
            set { this.indicatorFunction1.SelectedIndicatorFunctionFile = value; }
        }
        [DefaultValue(null)]
        public string IndicatorFunctionName
        {
            get { return this.indicatorFunction1.IndicatorFunctionName; }
            set { this.indicatorFunction1.IndicatorFunctionName = value; }
        }



        public Form_LoadIndicatorFunction()
        {
            InitializeComponent();
        }
        private void Form_LoadIndicatorFunction_Load(object sender, EventArgs e)
        {
            this.SetOKButton();
        }



        private void indicatorFunction1_IndicatorFunctionChanged(object sender, IndicatorFunctionEventArgs e)
        {
            this.SetOKButton();
        }



        private void SetOKButton()
        {
            if ((this.indicatorFunction1.SelectedIndicatorFunctionFile != null)
                && ((this.indicatorFunction1.SelectedInterpolationMethod != null)))
            {
                this.button_OK.Enabled = true;
            }
            else
            {
                this.button_OK.Enabled = false;
            }
        }

    }
}
