using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Media.Sound;

namespace EngineDesigner.TestForms
{
    internal partial class TestForm_Sound : Form
    {
        public TestForm_Sound()
        {
            InitializeComponent();
        }

        private void TestForm_Sound_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DXPlayer _dxPlayer = new DXPlayer();
            _dxPlayer.Owner = this;

            Wave _wave = new Wave(EngineDesigner.Properties.Resources.Kick);

            Track _track = new Track(_wave.WaveFormat.Channels, _wave.WaveFormat.BitsPerSample);
            _track = _track.GenerateSilence(_wave.WaveTrack.SampleNumber);

            Track _left = new Track(_wave.WaveFormat.Channels, _wave.WaveFormat.BitsPerSample);
            for (int a = 0; a < _wave.WaveTrack.SampleNumber; a++)
            {
                _left.Add(0, _wave.WaveTrack[0, a]);
                _left.Add(1, new Sample(16));
            }

            Track _right = new Track(_wave.WaveFormat.Channels, _wave.WaveFormat.BitsPerSample);
            for (int a = 0; a < _wave.WaveTrack.SampleNumber; a++)
            {
                _right.Add(0, new Sample(16));
                _right.Add(1, _wave.WaveTrack[1, a]);
            }

            _track.MixWith(_left, 0, false);
            _track.MixWith(_right, 6000, false);

            _wave.WaveTrack = _track;



            SoundPatch _soundPatch = _dxPlayer.AddSoundPatch(_wave);
            _soundPatch.Play(true);


        }
    }
}
