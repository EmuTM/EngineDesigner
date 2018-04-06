using System;
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
    internal partial class Form_AddAFunctionBase : Form
    {
        private const string AUTO_CHART_AREA = "Auto";



        private ChartAreaInfo[] availableChartAreas;



        private FunctionInfoBase selectedFunction = null;
        public FunctionInfoBase SelectedFunction
        {
            get { return selectedFunction; }
        }



        public Form_AddAFunctionBase()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                throw new Exception("This constructor is only meant for design time purposes.");
            }


            this.Constructor();
        }
        public Form_AddAFunctionBase(ChartAreaInfo[] _availableChartAreas)
        {
            this.availableChartAreas = _availableChartAreas;


            this.Constructor();
        }
        private void Constructor()
        {
            InitializeComponent();
        }
        private void Form_AddAFunctionBase_Load(object sender, EventArgs e)
        {
            this.colorPicker1.GetRandomColor(true);

            this.comboBox_ChartArea.Items.Clear();
            this.comboBox_ChartArea.Items.Add(AUTO_CHART_AREA);
            this.comboBox_ChartArea.SelectedItem = this.comboBox_ChartArea.Items[0];
            if (this.availableChartAreas != null)
            {
                this.comboBox_ChartArea.Items.AddRange(this.availableChartAreas);
                //daodamo še eno "ForceNew"
                this.comboBox_ChartArea.Items.Add(new ChartAreaInfo());
            }
        }



        private void radioButton_Percents_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _radioButton = (RadioButton)sender;
            this.panel_MinimumIs.Enabled = _radioButton.Checked;
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            this.selectedFunction = null;


            bool _cancelled = false;
            this.OnOKButtonClicked(ref this.selectedFunction, ref _cancelled);
            if (_cancelled)
            {
                return;
            }
            else
            {
                this.selectedFunction.ConvertYToPercents = this.radioButton_Percents.Checked;
                this.selectedFunction.MinIsNegative100 = this.radioButton_MinimumIsNegative100.Checked;

                this.selectedFunction.Color = this.colorPicker1.SelectedColor;

                if (this.comboBox_ChartArea.Items[this.comboBox_ChartArea.SelectedIndex] is ChartAreaInfo)
                {
                    this.selectedFunction.ChartArea = (ChartAreaInfo)this.comboBox_ChartArea.Items[this.comboBox_ChartArea.SelectedIndex];
                }


                this.Close();
            }
        }



        protected virtual void OnOKButtonClicked(ref FunctionInfoBase _selectedFunction, ref bool _cancel) //tukaj lahko preprečimo zapiranje
        {
            _selectedFunction = null;
        }
        protected virtual void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //NOTE: to stavimo tudi v vsak derivan klas!
            if (this.DesignMode)
            {
                return;
            }


            this.button_OK.Enabled = true;
        }
        protected virtual void comboBox_ChartArea_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected virtual void colorPicker1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected bool Button_OK_Enabled
        {
            get { return this.button_OK.Enabled; }
            set { this.button_OK.Enabled = value; }
        }

    }

}
