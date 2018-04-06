using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EngineDesigner.Controls.Charting
{
    internal partial class AxisOptions : UserControl
    {
        private event EventHandler customRangeChanged;
        public event EventHandler CustomRangeChanged
        {
            add { customRangeChanged += value; }
            remove { customRangeChanged -= value; }
        }
        private event EventHandler startDegreesChanged;
        public event EventHandler StartDegreesChanged
        {
            add { startDegreesChanged += value; }
            remove { startDegreesChanged -= value; }
        }
        private event EventHandler endDegreesChanged;
        public event EventHandler EndDegreesChanged
        {
            add { endDegreesChanged += value; }
            remove { endDegreesChanged -= value; }
        }

        private event EventHandler showElapsedStrokeChanged;
        public event EventHandler ShowElapsedStrokeChanged
        {
            add { showElapsedStrokeChanged += value; }
            remove { showElapsedStrokeChanged -= value; }
        }
        private event EventHandler showStrokeElapseCyclicallyChanged;
        public event EventHandler ShowStrokeElapseCyclicallyChanged
        {
            add { showStrokeElapseCyclicallyChanged += value; }
            remove { showStrokeElapseCyclicallyChanged -= value; }
        }

        private event EventHandler showValuesInPIChanged;
        public event EventHandler ShowValuesInPIChanged
        {
            add { showValuesInPIChanged += value; }
            remove { showValuesInPIChanged -= value; }
        }



        [DefaultValue(true)]
        public bool RangeVisible
        {
            get { return this.groupBox_Range.Visible; }
            set { this.groupBox_Range.Visible = value; }
        }
        [DefaultValue(true)]
        public bool RangeEnabled
        {
            get { return this.groupBox_Range.Enabled; }
            set { this.groupBox_Range.Enabled = value; }
        }
        [DefaultValue(false)]
        public bool CustomRange
        {
            get { return this.checkBox_CustomRange.Checked; }
            set { this.checkBox_CustomRange.Checked = value; }
        }
        [DefaultValue(0d)]
        public double StartDegrees
        {
            get { return (double)this.numericUpDown_StartDegrees.Value; }
            set { this.numericUpDown_StartDegrees.Value = (decimal)value; }
        }
        [DefaultValue(-7200d)]
        public double StartDegreesMinimum
        {
            get { return (double)this.numericUpDown_StartDegrees.Minimum; }
            set { this.numericUpDown_StartDegrees.Minimum = (decimal)value; }
        }
        [DefaultValue(7200d)]
        public double StartDegreesMaximum
        {
            get { return (double)this.numericUpDown_StartDegrees.Maximum; }
            set { this.numericUpDown_StartDegrees.Maximum = (decimal)value; }
        }
        [DefaultValue(0d)]
        public double EndDegrees
        {
            get { return (double)this.numericUpDown_EndDegrees.Value; }
            set { this.numericUpDown_EndDegrees.Value = (decimal)value; }
        }
        [DefaultValue(7200d)]
        public double EndDegreesMinimum
        {
            get { return (double)this.numericUpDown_EndDegrees.Minimum; }
            set { this.numericUpDown_EndDegrees.Minimum = (decimal)value; }
        }
        [DefaultValue(7200d)]
        public double EndDegreesMaximum
        {
            get { return (double)this.numericUpDown_EndDegrees.Maximum; }
            set { this.numericUpDown_EndDegrees.Maximum = (decimal)value; }
        }

        [DefaultValue(true)]
        public bool CycleElapseVisible
        {
            get { return this.groupBox_CycleElapse.Visible; }
            set { this.groupBox_CycleElapse.Visible = value; }
        }
        [DefaultValue(true)]
        public bool CycleElapseEnabled
        {
            get { return this.groupBox_CycleElapse.Enabled; }
            set { this.groupBox_CycleElapse.Enabled = value; }
        }
        [DefaultValue(false)]
        public bool ShowElapsedStroke
        {
            get { return this.checkBox_ShowElapsedStroke.Checked; }
            set { this.checkBox_ShowElapsedStroke.Checked = value; }
        }
        [DefaultValue(false)]
        public bool ShowStrokeElapseCyclically
        {
            get { return this.checkBox_ShowStrokeElapseCyclically.Checked; }
            set { this.checkBox_ShowStrokeElapseCyclically.Checked = value; }
        }

        [DefaultValue(true)]
        public bool RepresentationVisible
        {
            get { return this.groupBox_Representation.Visible; }
            set { this.groupBox_Representation.Visible = value; }
        }
        [DefaultValue(true)]
        public bool RepresentationEnabled
        {
            get { return this.groupBox_Representation.Enabled; }
            set { this.groupBox_Representation.Enabled = value; }
        }
        [DefaultValue(false)]
        public bool ShowValuesInPI
        {
            get { return this.checkBox_ShowValuesInPi.Checked; }
            set { this.checkBox_ShowValuesInPi.Checked = value; }
        }



        public AxisOptions()
        {
            InitializeComponent();
        }



        private void checkBox_CustomRange_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox = (CheckBox)sender;

            this.tableLayoutPanel_Range.Enabled = _checkBox.Checked;

            this.OnCustomRangeChanged();
        }
        private void numericUpDown_StartDegrees_ValueChanged(object sender, EventArgs e)
        {
            this.OnStartDegreesChanged();
        }
        private void numericUpDown_EndDegrees_ValueChanged(object sender, EventArgs e)
        {
            this.OnEndDegreesChanged();
        }

        private void checkBox_ShowElapsedStroke_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox = (CheckBox)sender;
            this.checkBox_ShowStrokeElapseCyclically.Enabled = _checkBox.Checked;

            this.OnShowElapsedStrokeChanged();
        }
        private void checkBox_ShowStrokeElapseCyclically_CheckedChanged(object sender, EventArgs e)
        {
            this.OnShowStrokeElapseCyclicallyChanged();
        }

        private void checkBox_ShowValuesInPi_CheckedChanged(object sender, EventArgs e)
        {
            this.OnShowValuesInPIChanged();
        }



        protected virtual void OnCustomRangeChanged()
        {
            if (this.customRangeChanged != null)
            {
                this.customRangeChanged(this, new EventArgs());
            }
        }
        protected virtual void OnStartDegreesChanged()
        {
            if (this.startDegreesChanged != null)
            {
                this.startDegreesChanged(this, new EventArgs());
            }
        }
        protected virtual void OnEndDegreesChanged()
        {
            if (this.endDegreesChanged != null)
            {
                this.endDegreesChanged(this, new EventArgs());
            }
        }

        protected virtual void OnShowElapsedStrokeChanged()
        {
            if (this.showElapsedStrokeChanged != null)
            {
                this.showElapsedStrokeChanged(this, new EventArgs());
            }
        }
        protected virtual void OnShowStrokeElapseCyclicallyChanged()
        {
            if (this.showStrokeElapseCyclicallyChanged != null)
            {
                this.showStrokeElapseCyclicallyChanged(this, new EventArgs());
            }
        }

        protected virtual void OnShowValuesInPIChanged()
        {
            if (this.showValuesInPIChanged != null)
            {
                this.showValuesInPIChanged(this, new EventArgs());
            }
        }
    }
}
