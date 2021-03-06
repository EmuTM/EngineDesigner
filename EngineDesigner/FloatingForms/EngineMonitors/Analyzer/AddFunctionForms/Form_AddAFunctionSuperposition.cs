﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;
using System.Windows.Forms.DataVisualization.Charting;
using EngineDesigner.FloatingForms.EngineMonitors.Analyzer;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class Form_AddAFunctionSuperposition : Form_AddAFunctionBase
    {
        private FunctionInfoBase[] functions = null;



        public Form_AddAFunctionSuperposition()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                throw new Exception("This constructor is only meant for design time purposes.");
            }


            this.Constructor();
        }
        public Form_AddAFunctionSuperposition(ChartAreaInfo[] _availableChartAreas, params FunctionInfoBase[] _functions)
            : base(_availableChartAreas)
        {
            if (_functions.Length < 2)
            {
                throw new Exception();
            }

            this.functions = _functions;


            this.Constructor();
        }
        private void Constructor()
        {
            InitializeComponent();
        }
        private void Form_AddAFunctionSuperposition_Load(object sender, EventArgs e)
        {
            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.Append(EngineDesigner.Properties.Settings.Default.SuperpositionFunctionNameBaseText);

            foreach (FunctionInfoBase _functionInfoBase in this.functions)
            {
                _stringBuilder.Append(" ");
                _stringBuilder.Append(_functionInfoBase.Name);
                _stringBuilder.Append(" /");
            }

            _stringBuilder = _stringBuilder.Remove(_stringBuilder.Length - 2, 2);


            this.textBox_Function.Text = _stringBuilder.ToString();
        }



        protected override void OnOKButtonClicked(ref FunctionInfoBase _selectedFunction, ref bool _cancel)
        {
            base.OnOKButtonClicked(ref _selectedFunction, ref _cancel);


            if (base.tabControl1.SelectedTab == this.tabPage_Superposition)
            {
                FunctionInfoSuperposition _functionInfoSuperposition = new FunctionInfoSuperposition(
                    this.textBox_Function.Text, this.functions);
                _selectedFunction = _functionInfoSuperposition;
            }
        }
    }

}
