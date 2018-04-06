using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Machine;
using System.Windows.Forms.DataVisualization.Charting;
using EngineDesigner.Environment.Controls.Charting;
using EngineDesigner.Common;

namespace EngineDesigner.FloatingForms
{
    internal partial class Form_EngineControl : Form_FloatingBase
    {
        private const int CHART_MARKER_SIZE_DIVISOR = 15;



        public event EventHandler<CrankshaftAngleEventArgs> CrankshaftAngleChanged;
        public event EventHandler<RPMEventArgs> RPMChanged;



        public Form_EngineControl()
            : this(null)
        {
        }
        public Form_EngineControl(Form _owner)
            : base(_owner)
        {
            InitializeComponent();

            //kličemo zato, da se inicializirajo series in marker
            this.DrawMarker(0d);
        }
        private void Form_EngineControl_Load(object sender, EventArgs e)
        {
            //moramo klicat ponovno na load
            this.ReDrawMarker();
        }
        private void Form_EngineControl_Resize(object sender, EventArgs e)
        {
            this.ReDrawMarker();
        }



        public Engine @Engine
        {
            get { return this.rpmTimer1.Engine; }
            set { this.rpmTimer1.Engine = value; }
        }

        public int RPM
        {
            get { return (int)this.numericUpDown_RPM.Value; }

            //naj bo onemogočeno zaenkrat
            //set 
            //{
            //    this.SetRPMControls(
            //        value, 
            //        RPMControl.NUMERIC_UP_DOWN,
            //        RPMControl.TRACK_BAR);

            //    if (this.engine != null)
            //    {
            //        if (this.engineInstances.ContainsKey(this.engine))
            //        {
            //            this.engineInstances[this.engine].RPM = value;
            //        }
            //    }
            //}
        }

        private double currentRPMCrankshaftRotation_deg = 0d;
        public double CrankshaftRotation_deg
        {
            get
            {
                if (this.tabControl1.SelectedTab == this.tabPage_CrankshaftAngle)
                {
                    return (double)this.numericUpDown_CrankshaftAngle.Value;
                }
                else
                {
                    return this.currentRPMCrankshaftRotation_deg;
                }
            }


            //OBSOLETE:
            //get { return (double)this.numericUpDown_CrankshaftAngle.Value; }

            //naj bo onemogočeno zaenkrat
            //set
            //{
            //    this.SetCrankshaftRotationControls(
            //        value,
            //        CrankshaftRotationControl.NUMERIC_UP_DOWN,
            //        CrankshaftRotationControl.MARKER);

            //    if (this.engine != null)
            //    {
            //        if (this.engineInstances.ContainsKey(this.engine))
            //        {
            //            this.engineInstances[this.engine].CrankshaftRotation_deg = value;
            //        }
            //    }
            //}
        }

        //public EngineControlOption CurrentEngineControlOption
        //{
        //    get
        //    {
        //        if (this.tabControl1.SelectedTab == this.tabPage_CrankshaftAngle)
        //        {
        //            return EngineControlOption.CRANKSHAFT_ANGLE;
        //        }
        //        else if (this.tabControl1.SelectedTab == this.tabPage_RPM)
        //        {
        //            return EngineControlOption.RPM;
        //        }
        //        else
        //        {
        //            throw new NotImplementedException();
        //        }
        //    }
        //}



        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == this.tabPage_CrankshaftAngle)
            {
                rpmTimer1.Enabled = false;
                this.OnCrankshaftAngleChanged((double)this.numericUpDown_CrankshaftAngle.Value);
            }
            else if (e.TabPage == this.tabPage_RPM)
            {
                rpmTimer1.Enabled = true;
                this.OnRPMChanged((int)this.numericUpDown_RPM.Value);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private void trackBar_RPM_ValueChanged(object sender, EventArgs e)
        {
            if (!trackBar_RPM.Enabled)
            {
                return;
            }

            this.SetRPMControls(this.trackBar_RPM.Value, RPMControl.NUMERIC_UP_DOWN);

            this.OnRPMChanged(this.rpmTimer1.RPM);
        }
        private void numericUpDown_RPM_ValueChanged(object sender, EventArgs e)
        {
            if (!numericUpDown_RPM.Enabled)
            {
                return;
            }


            int _rpmInt = (int)this.numericUpDown_RPM.Value;

            this.SetRPMControls(_rpmInt, RPMControl.TRACK_BAR);

            this.OnRPMChanged(_rpmInt);
        }
        private void numericUpDown_MaximumRPM_ValueChanged(object sender, EventArgs e)
        {
            trackBar_RPM.Maximum = (int)numericUpDown_MaximumRPM.Value;
            numericUpDown_RPM.Maximum = (int)numericUpDown_MaximumRPM.Value;
        }
        private void numericUpDown_MinimumRPM_ValueChanged(object sender, EventArgs e)
        {
            trackBar_RPM.Minimum = (int)numericUpDown_MinimumRPM.Value;
            numericUpDown_RPM.Minimum = (int)numericUpDown_MinimumRPM.Value;
        }
        private void SetRPMControls(int _value, params RPMControl[] _rpmControls)
        {
            foreach (RPMControl _rpmControl in _rpmControls)
            {
                switch (_rpmControl)
                {
                    case RPMControl.NUMERIC_UP_DOWN:
                        numericUpDown_RPM.Enabled = false;
                        numericUpDown_RPM.Value = _value;
                        numericUpDown_RPM.Enabled = true;
                        break;

                    case RPMControl.TRACK_BAR:
                        trackBar_RPM.Enabled = false;
                        trackBar_RPM.Value = _value;
                        trackBar_RPM.Enabled = true;
                        break;
                }
            }

            this.rpmTimer1.RPM = _value;
        }
        private enum RPMControl
        {
            NUMERIC_UP_DOWN,
            TRACK_BAR
        }

        private void inputAngleChart_CrankshaftAngle_AngleChanged(object sender, AngleChangedEventArgs e)
        {
            this.SetCrankshaftRotationControls(e.NewAngle, CrankshaftRotationControl.NUMERIC_UP_DOWN);

            this.OnCrankshaftAngleChanged(e.NewAngle);
        }
        private void numericUpDown_CrankshaftAngle_ValueChanged(object sender, EventArgs e)
        {
            if (!numericUpDown_CrankshaftAngle.Enabled)
            {
                return;
            }


            double _angleDouble = (double)this.numericUpDown_CrankshaftAngle.Value;

            this.SetCrankshaftRotationControls(_angleDouble, CrankshaftRotationControl.INPUT_ANGLE_CHART);

            this.OnCrankshaftAngleChanged(_angleDouble); //tukaj je bil newAngle včasih castan na int; zakaj?
        }
        private void SetCrankshaftRotationControls(double _value, params CrankshaftRotationControl[] _crankshaftRotationControls)
        {
            foreach (CrankshaftRotationControl _crankshaftRotationControl in _crankshaftRotationControls)
            {
                switch (_crankshaftRotationControl)
                {
                    case CrankshaftRotationControl.NUMERIC_UP_DOWN:
                        this.numericUpDown_CrankshaftAngle.Enabled = false;
                        this.numericUpDown_CrankshaftAngle.Value = (decimal)_value;
                        this.numericUpDown_CrankshaftAngle.Enabled = true;
                        break;

                    case CrankshaftRotationControl.INPUT_ANGLE_CHART:
                        this.SetMarkerAngle(_value);
                        break;
                }
            }
        }
        private enum CrankshaftRotationControl
        {
            NUMERIC_UP_DOWN,
            INPUT_ANGLE_CHART
        }



        private void DrawMarker(double _angle)
        {
            int _markerSize = 0;
            if (this.inputAngleChart_CrankshaftAngle.Width >= this.inputAngleChart_CrankshaftAngle.Height)
            {
                _markerSize = this.inputAngleChart_CrankshaftAngle.Height / CHART_MARKER_SIZE_DIVISOR;
            }
            else
            {
                _markerSize = this.inputAngleChart_CrankshaftAngle.Width / CHART_MARKER_SIZE_DIVISOR;
            }


            this.inputAngleChart_CrankshaftAngle.DrawMarker(
                _angle,
                null,
                Color.Red,
                false,
                MarkerStyle.Circle,
                _markerSize);
        }
        private void ReDrawMarker()
        {
            double _angle = this.inputAngleChart_CrankshaftAngle.@SeriesCollection[0].Points[0].XValue;
            this.inputAngleChart_CrankshaftAngle.@SeriesCollection[0].Points.RemoveAt(0);
            this.inputAngleChart_CrankshaftAngle.@SeriesCollection.RemoveAt(0);

            this.DrawMarker(_angle);
        }
        private void SetMarkerAngle(double _angle)
        {
            long _anyRotations = EngineDesigner.Common.Mathematics.GetAnyRotations(_angle);
            this.inputAngleChart_CrankshaftAngle.Circle = (int)_anyRotations;
            this.inputAngleChart_CrankshaftAngle.@SeriesCollection[0].Points[0].XValue = _angle;
            this.inputAngleChart_CrankshaftAngle.Refresh();
        }





        private void rpmTimer1_CrankshaftAngleChanged(object sender, EngineDesigner.Media.RPMTimerEventArgs e)
        {
            this.currentRPMCrankshaftRotation_deg = e.NewAngle_deg;
            this.OnCrankshaftAngleChanged(e.NewAngle_deg);
        }

        protected void OnCrankshaftAngleChanged(double _newAngle)
        {
            if (CrankshaftAngleChanged != null)
            {
                CrankshaftAngleChanged(
                    this,
                    new CrankshaftAngleEventArgs(_newAngle));
            }
        }
        protected void OnRPMChanged(int _newRPM)
        {
            this.label_CurrentAccuracy.Text = string.Format(
                "1 : {0}",
                this.rpmTimer1.Accuracy.ToString(EngineDesigner.Common.Defaults.ROUNDING));


            if (RPMChanged != null)
            {
                RPMChanged(
                    this,
                    new RPMEventArgs(_newRPM));
            }
        }

    }


    public enum EngineControlOption
    {
        CRANKSHAFT_ANGLE,
        RPM
    }


    //internal class EngineEventArgs : EventArgs
    //{
    //    public EngineEventArgs(Engine _engine)
    //    {
    //        this.engine = _engine;
    //    }


    //    private Engine engine;
    //    public Engine @Engine
    //    {
    //        get { return engine; }
    //    }
    //}


    internal class CrankshaftAngleEventArgs : EventArgs
    {
        public CrankshaftAngleEventArgs(double _newAngle)
        {
            this.newAngle_deg = _newAngle;
        }


        private double newAngle_deg;
        public double NewAngle_deg
        {
            get { return newAngle_deg; }
        }
    }


    internal class RPMEventArgs : EventArgs
    {
        public RPMEventArgs(int _newRPM)
        {
            this.newRPM = _newRPM;
        }


        private int newRPM;
        public int NewRPM
        {
            get { return newRPM; }
        }
    }

}
