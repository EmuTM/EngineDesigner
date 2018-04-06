using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using EngineDesigner.Common;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Environment.Controls;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class Form_AddAFunctionReference : Form_AddAFunctionBase
    {
        public Form_AddAFunctionReference()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                throw new Exception("This constructor is only meant for design time purposes.");
            }


            this.Constructor();
        }
        public Form_AddAFunctionReference(ChartAreaInfo[] _availableChartAreas)
            : base(_availableChartAreas)
        {
            this.Constructor();
        }
        private void Constructor()
        {
            InitializeComponent();

            this.customComboBox_Expression_BackColor = this.customComboBox_Expression.BackColor;
        }
        private void Form_AddAFunctionReference_Load(object sender, EventArgs e)
        {
            #region "zložimo vse suportane tokene v report"
            Mathematics.MathParser _mathParser = new Mathematics.MathParser();

            List<ReportItem> _reportItems = new List<ReportItem>();

            KeyValuePair<string, string>[] _operators = _mathParser.GetSupportedOperators();
            foreach (KeyValuePair<string, string> _keyValuePair in _operators)
            {
                ReportItem _reportItem = new ReportItem(
                    _keyValuePair.Key,
                    _keyValuePair.Value,
                    "Operators");

                _reportItems.Add(_reportItem);
            }

            KeyValuePair<string, string>[] _functions = _mathParser.GetSupportedFunctions();
            foreach (KeyValuePair<string, string> _keyValuePair in _functions)
            {
                ReportItem _reportItem = new ReportItem(
                    _keyValuePair.Key,
                    _keyValuePair.Value,
                    "Functions");

                _reportItems.Add(_reportItem);
            }

            KeyValuePair<string, string>[] _constants = _mathParser.GetSupportedConstants();
            foreach (KeyValuePair<string, string> _keyValuePair in _constants)
            {
                ReportItem _reportItem = new ReportItem(
                    _keyValuePair.Key,
                    _keyValuePair.Value,
                    "Constants");

                _reportItems.Add(_reportItem);
            }

            ReportItem _reportItemX = new ReportItem(
                "x",
                "The X in the function",
                "Constants");

            _reportItems.Add(_reportItemX);

            this.report1.ReportItems = _reportItems.ToArray();
            #endregion "zložimo vse suportane tokene v report"

            this.customComboBox_Expression.Items.Clear();
            this.customComboBox_Expression.Items.AddRange(FunctionInfoReference.PreviousExpressions);
        }



        private Color customComboBox_Expression_BackColor;



        protected override void OnOKButtonClicked(ref FunctionInfoBase _selectedFunction, ref bool _cancel)
        {
            base.OnOKButtonClicked(ref _selectedFunction, ref _cancel);


            if (base.tabControl1.SelectedTab == this.tabPage_Reference)
            {
                bool _bool = this.ValidateExpression();

                if (!_bool)
                {
                    _cancel = true;
                }
                else
                {
                    FunctionInfoReference _functionInfoReference = FunctionInfoReference.CreateNew(this.customComboBox_Expression.Text);
                    _selectedFunction = _functionInfoReference;
                }
            }
        }

        private void report1_ReportItemDoubleClicked(object sender, ReportItemEventArgs e)
        {
            this.customComboBox_Expression.Paste(e.ReportItem.Key.ToString());
        }

        private void comboBox_Expression_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == char.Parse(System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator))
            {
                e.Handled = true;
            }
        }
        private void customComboBox_Expression_Enter(object sender, EventArgs e)
        {
            CustomComboBox _customComboBox = (CustomComboBox)sender;
            _customComboBox.BackColor = this.customComboBox_Expression_BackColor;
        }
        private void linkLabel_ValidateExpression_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            this.ValidateExpression();
        }

        private bool ValidateExpression()
        {
            try
            {
                //preverimo z -1 in 1
                Mathematics.MathParser _mathParser;

                _mathParser = new Mathematics.MathParser(new KeyValuePair<string, double>("x", -1));
                _mathParser.Compute(this.customComboBox_Expression.Text);

                _mathParser = new Mathematics.MathParser(new KeyValuePair<string, double>("x", 1));
                _mathParser.Compute(this.customComboBox_Expression.Text);

                this.customComboBox_Expression.BackColor = Defaults.LightGreenColor;

                return true;
            }
            catch (Exception _exception)
            {
                this.customComboBox_Expression.BackColor = Defaults.LightRedColor;

                Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                return false;
            }
        }

    }

}
