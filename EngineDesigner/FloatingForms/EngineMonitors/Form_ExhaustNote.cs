using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Machine;
using EngineDesigner.Media.Sound;
using EngineDesigner.Common;

namespace EngineDesigner.FloatingForms.EngineMonitors
{
    internal partial class Form_ExhaustNote : Form_EngineMonitorBase
    {
        private class BackgroundWorkerArgument
        {
            public BackgroundWorkerArgument(Engine _engine, double _rpm)
            {
                this.engine = _engine;
                this.rpm = _rpm;
            }


            private Engine engine;
            public Engine Engine
            {
                get { return engine; }
            }

            private double rpm;
            public double RPM
            {
                get { return rpm; }
            }
        }



        //prag nad katerim se zvok izračunava vnaprej
        private const int REALTIME_RPM_THRESHOLD = 100;



        public Form_ExhaustNote()
            : this(null, null)
        {
        }
        public Form_ExhaustNote(Form _owner, Form_EngineControl _engineControl)
            : base(_owner, _engineControl)
        {
            InitializeComponent();


            this.simulatedExhaustNote = this.dxPlayer1.AddSoundPatch(new Wave(
                EngineDesigner.Properties.Resources.Kick));
            this.realTimeExhaustNote = this.dxPlayer1.AddSoundPatch(new Wave(
                EngineDesigner.Properties.Resources.Kick));

            this.simulatedExhaustNote.Volume = this.trackBar_Volume.Value;
            this.realTimeExhaustNote.Volume = this.trackBar_Volume.Value;
        }



        private SoundPatch simulatedExhaustNote = null;
        private SoundPatch realTimeExhaustNote = null;
        //tukaj si zapomnemo kateri cilinder je že delal
        private Dictionary<PositionedCylinder, bool> cylinderHasFired = new Dictionary<PositionedCylinder, bool>();
        private BackgroundWorkerArgument pendingBackgroundWorkerArgument = null; //tle damo na čakanje, če je trenutno backgroundWorkerZaseden; ko konča svoje opravilo, pogleda še tle



        private void trackBar_Volume_ValueChanged(object sender, EventArgs e)
        {
            TrackBar _trackBar = (TrackBar)sender;
            this.simulatedExhaustNote.Volume = _trackBar.Value;
            this.realTimeExhaustNote.Volume = _trackBar.Value;
        }

        private void radioButton_RealTime_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _radioButton = (RadioButton)sender;
            if (_radioButton.Checked)
            {
                this.simulatedExhaustNote.Stop();
            }
        }
        private void radioButton_Simulation_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _radioButton = (RadioButton)sender;
            if (_radioButton.Checked)
            {
                if (base.Engine != null)
                {
                    BackgroundWorkerArgument _backgroundWorkerArgument = new BackgroundWorkerArgument(
                        base.Engine,
                        base.RPM);

                    if (!this.backgroundWorker1.IsBusy)
                    {
                        this.backgroundWorker1.RunWorkerAsync(_backgroundWorkerArgument);
                    }
                    else
                    {
                        this.pendingBackgroundWorkerArgument = _backgroundWorkerArgument;
                        this.backgroundWorker1.CancelAsync();
                    }
                }
            }
        }

        private void checkBox_Mute_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox = (CheckBox)sender;

            this.simulatedExhaustNote.Mute = _checkBox.Checked;
            this.realTimeExhaustNote.Mute = _checkBox.Checked;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _backgroundWorker = (BackgroundWorker)sender;
            BackgroundWorkerArgument _backgroundWorkerArgument = (BackgroundWorkerArgument)e.Argument;


            // t_rotacije = (60 / RPM) s
            // t_cikla = t_rotacije * [št rotacij za ciker] s
            // samplov/cikel = t_cikla * SamplesPerSecond


            Wave _wave = new Wave(Properties.Resources.Kick);


            double _rotationTime_s = 60d / _backgroundWorkerArgument.RPM; //_rotationTime_s *= 2;
            double _cycleTime_s = _rotationTime_s * _backgroundWorkerArgument.Engine.RevolutionsToCompleteCombinedCycle;
            double _samplesPerCycle = _cycleTime_s * (double)_wave.WaveFormat.SamplesPerSecond;
            int _strokes = _backgroundWorkerArgument.Engine.RevolutionsToCompleteCombinedCycle * 2; //po 2 stroka za eno rotacijo
            //dobimo koliko dolg wave lahko uporabimo
            int _samplesPerStroke = (int)(_samplesPerCycle / (double)_strokes);
            double _samplesPerDeg = _samplesPerStroke / 180d;


            #region "Cancelled?"
            if (_backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            #endregion "Cancelled?"


            //dobimo zvok za 1 combustion stroke (trim je pomemben, ker wave ne sme biti daljši ali krajši od stroka!)
            _wave.WaveTrack.Trim(_samplesPerStroke);
            if (_wave.WaveTrack.SampleNumber < _samplesPerStroke)
            {
                _wave.WaveTrack.AddTo(
                    _wave.WaveTrack.GenerateSilence(_samplesPerStroke - _wave.WaveTrack.SampleNumber));
            }


            #region "Cancelled?"
            if (_backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            #endregion "Cancelled?"


            //zgradimo track za cel engine
            Track _engineTrack = new Track(_wave.WaveTrack.ChannelNumer, _wave.WaveTrack.BitsPerSample);
            //v začetku je tišina; gor bomo miksali cilindre
            _engineTrack.AddTo(_engineTrack.GenerateSilence((int)_samplesPerCycle));


            //dobimo naklone
            List<double> _tilts = new List<double>();
            foreach (PositionedCylinder _positionedCylinder in _backgroundWorkerArgument.Engine.PositionedCylinders)
            {
                double _double = Mathematics.GetAbsoluteAngle_deg(_positionedCylinder.Tilt_deg);
                if (!_tilts.Contains(_double))
                {
                    _tilts.Add(_double);
                }
            }


            //dobimo tracke po naklonih
            Dictionary<double, Track> _tracks = new Dictionary<double, Track>(); //naklon, track za naklon
            foreach (double _tilt in _tilts)
            {
                Track _totalTrackPerTilt = new Track(_wave.WaveTrack.ChannelNumer, _wave.WaveTrack.BitsPerSample);
                //v začetku je tišina; gor bomo miksali cilinder
                _totalTrackPerTilt.AddTo(_totalTrackPerTilt.GenerateSilence((int)_samplesPerCycle));


                #region "Cancelled?"
                if (_backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                #endregion "Cancelled?"


                #region "dobimo tracke od cilindrov"
                foreach (PositionedCylinder _positionedCylinder in _backgroundWorkerArgument.Engine.PositionedCylinders)
                {
                    if (_positionedCylinder.Tilt_deg == _tilt)
                    {
                        Track _track = new Track(_wave.WaveFormat.Channels, _wave.WaveFormat.BitsPerSample);

                        foreach (Stroke _stroke in _positionedCylinder.Cycle.Strokes)
                        {
                            if ((_stroke.Equals(Stroke.Combustion))
                                || (_stroke .Equals(Stroke.CombustionExhaust)))
                            {
                                _track.AddTo(_wave.WaveTrack);
                            }
                            else
                            {
                                _track.AddTo(_wave.WaveTrack.GenerateSilence(_samplesPerStroke));
                            }


                            #region "Cancelled?"
                            if (_backgroundWorker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                            #endregion "Cancelled?"
                        }


                        //izračunamo na katerem samplu se začne miksanje za ta cilinder
                        int _mixStartSample = (int)(_positionedCylinder.FiringAngle_deg * _samplesPerDeg);

                        //zmiksamo z ostankom
                        Track _return = _totalTrackPerTilt.MixWithReturn(_track, _mixStartSample);

                        //če je kaj ostalo, damo na začetek (za tiste takte, ki se mogoče ne bi končali v ciklu)
                        if ((_return != null)
                            && (_return.SampleNumber > 0))
                        {
                            _totalTrackPerTilt.MixWith(_return, 0, false);

                            //sprostimo, ker žre dosti rama
                            _return = null;
                        }

                        //sprostimo, ker žre dosti rama
                        _track = null;
                        GC.Collect();


                        #region "Cancelled?"
                        if (_backgroundWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        #endregion "Cancelled?"
                    }
                }
                #endregion "dobimo tracke od cilindrov"


                _tracks.Add(_tilt, _totalTrackPerTilt);
            }


            //če imamo V motor ali boxer, računamo, da gre vsak bank v svoj auspuh
            if (_tilts.Count == 2)
            {
                Track _left = new Track(_wave.WaveFormat.Channels, _wave.WaveFormat.BitsPerSample);
                for (int a = 0; a < _tracks[_tilts[0]].SampleNumber; a++)
                {
                    _left.Add(0, _tracks[_tilts[0]][0, a]);
                    _left.Add(1, new Sample(16));
                }

                Track _right = new Track(_wave.WaveFormat.Channels, _wave.WaveFormat.BitsPerSample);
                for (int a = 0; a < _tracks[_tilts[1]].SampleNumber; a++)
                {
                    _right.Add(0, new Sample(16));
                    _right.Add(1, _tracks[_tilts[1]][1, a]);
                }

                _engineTrack.MixWith(_left, 0, false);
                _engineTrack.MixWith(_right, 0, false);
            }
            else
            {
                foreach (Track _track in _tracks.Values)
                {
                    _engineTrack.MixWith(_track, 0, false);
                }
            }


            //damo spet v wave
            _wave.WaveTrack = _engineTrack;


            //sprostimo, ker žre dosti rama
            _engineTrack = null;
            GC.Collect();


            e.Result = _wave;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if (this.radioButton_Simulation.Checked)
                {
                    Wave _wave = (Wave)e.Result;

                    this.simulatedExhaustNote.Stop();
                    this.simulatedExhaustNote.Wave = _wave;
                    this.simulatedExhaustNote.Volume = this.trackBar_Volume.Value;
                    this.simulatedExhaustNote.Play(true);
                }
            }
            else //če je bil cancellan, imamo v morda pendingu
            {
                if (this.pendingBackgroundWorkerArgument != null)
                {
                    this.backgroundWorker1.RunWorkerAsync(this.pendingBackgroundWorkerArgument);
                    this.pendingBackgroundWorkerArgument = null;
                }
            }
        }



        protected override void OnEngineChanged(Engine _engine)
        {
            this.cylinderHasFired.Clear();
            if (_engine != null)
            {
                foreach (PositionedCylinder _positionedCylinder in _engine.PositionedCylinders)
                {
                    this.cylinderHasFired.Add(_positionedCylinder, false);
                }

                this.OnRPMChanged(base.RPM);
            }
            else
            {
                this.simulatedExhaustNote.Stop();
                this.realTimeExhaustNote.Stop();
            }
        }
        protected override void OnCrankshaftAngleChanged(double _newAngle_deg)
        {
            if (this.radioButton_RealTime.Checked)
            {
                if (base.Engine != null)
                {
                    //dobimo naklone
                    List<double> _tilts = new List<double>();
                    foreach (PositionedCylinder _positionedCylinder in base.Engine.PositionedCylinders)
                    {
                        double _double = Mathematics.GetAbsoluteAngle_deg(_positionedCylinder.Tilt_deg);
                        if (!_tilts.Contains(_double))
                        {
                            _tilts.Add(_double);
                        }
                    }


                    foreach (PositionedCylinder _positionedCylinder in base.Engine.PositionedCylinders)
                    {
                        Stroke _stroke = _positionedCylinder.GetStroke(_newAngle_deg);

                        if ((_stroke .Equals(Stroke.Combustion))
                            || (_stroke.Equals(Stroke.CombustionExhaust)))
                        {
                            if (!cylinderHasFired[_positionedCylinder])
                            {
                                //samo če smo v prvem delu takta
                                double _elapsedStroke = _positionedCylinder.GetElapsedStroke(_stroke, _newAngle_deg);
                                if ((_elapsedStroke > 0d) && (_elapsedStroke < Stroke.StrokeDuration_deg / 2d))
                                {
                                    cylinderHasFired[_positionedCylinder] = true;

                                    if (_tilts.Count == 2)
                                    {
                                        if (_positionedCylinder.Tilt_deg == _tilts[0])
                                        {
                                            this.realTimeExhaustNote.Balance = -100;
                                            this.realTimeExhaustNote.Play(false);
                                        }
                                        else
                                        {
                                            this.realTimeExhaustNote.Balance = 100;
                                            this.realTimeExhaustNote.Play(false);
                                        }
                                    }
                                    else
                                    {
                                        this.realTimeExhaustNote.Balance = 0;
                                        this.realTimeExhaustNote.Play(false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            cylinderHasFired[_positionedCylinder] = false;
                        }
                    }
                }
            }
        }
        protected override void OnRPMChanged(int _newRPM)
        {
            if (_newRPM < REALTIME_RPM_THRESHOLD)
            {
                this.simulatedExhaustNote.Stop();
                if (this.backgroundWorker1.IsBusy)
                {
                    this.backgroundWorker1.CancelAsync();
                }

                this.radioButton_RealTime.Checked = true;
            }
            else
            {
                this.radioButton_Simulation.Checked = true;
            }


            if (this.radioButton_Simulation.Checked)
            {
                if (base.Engine != null)
                {
                    BackgroundWorkerArgument _backgroundWorkerArgument = new BackgroundWorkerArgument(
                        base.Engine,
                        _newRPM);

                    if (!this.backgroundWorker1.IsBusy)
                    {
                        this.backgroundWorker1.RunWorkerAsync(_backgroundWorkerArgument);
                    }
                    else
                    {
                        this.pendingBackgroundWorkerArgument = _backgroundWorkerArgument;
                        this.backgroundWorker1.CancelAsync();
                    }
                }
                else
                {
                    this.simulatedExhaustNote.Stop();
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            this.SetByVisibility();
        }
        protected override void OnEngineControlVisibleChanged(bool _visible)
        {
            this.SetByVisibility();
        }



        private void SetByVisibility()
        {
            if ((this.Visible)
                && (base.EngineControlVisible))
            {
                this.OnRPMChanged(base.RPM);
            }
            else
            {
                this.simulatedExhaustNote.Stop();
                this.realTimeExhaustNote.Stop();
            }
        }

    }
}
