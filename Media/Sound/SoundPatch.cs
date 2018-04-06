using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX.DirectSound;
using SlimDX.Multimedia;

namespace EngineDesigner.Media.Sound
{
    public class SoundPatch
    {
        private DirectSound directSound;
        private SecondarySoundBuffer secondarySoundBuffer;



        private Wave wave;
        public Wave Wave
        {
            get { return wave; }
            set
            {
                wave = value;
                this.SetWave(wave);
            }
        }

        private double volume = 75d;
        /// <summary>
        /// od 0 - 100
        /// </summary>
        public double Volume
        {
            get { return this.volume; }

            set
            {
                if ((value < 0d)
                    || (value > 100d))
                {
                    throw new ArgumentException("Volume must be within 0 and 100.");
                }


                this.volume = value;
                this.SetVolume();
            }
        }

        private double balance = 0d;
        /// <summary>
        /// -100 - 100
        /// </summary>
        public double Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                this.SetBalance();
            }
        }

        public int Frequency
        {
            get
            {
                if (this.secondarySoundBuffer != null)
                {
                    return this.secondarySoundBuffer.Frequency;
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if (this.secondarySoundBuffer != null)
                {
                    this.secondarySoundBuffer.Frequency = value;
                }

            }
        }

        private bool mute = false;
        public bool Mute
        {
            get { return mute; }

            set
            {
                mute = value;
                this.SetVolume();
            }
        }

        private object tag = null;
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public bool Playing
        {
            get 
            {
                if (this.secondarySoundBuffer.Status == BufferStatus.Playing)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        internal SoundPatch(DirectSound _directSound, Wave _wave)
        {
            this.directSound = _directSound;
            this.wave = _wave;

            this.SetWave(_wave);
        }



        public void Play(bool _looping)
        {
            if (_looping)
            {
                secondarySoundBuffer.Play(0, PlayFlags.Looping);
            }
            else
            {
                secondarySoundBuffer.Play(0, PlayFlags.None);
            }
        }
        public void Stop()
        {
            secondarySoundBuffer.Stop();
        }



        //dobi vrednost glasnosti iz procenta glasnosti
        private int GetVolume(double _percentage)
        {
            double _range = EngineDesigner.Media.Properties.Settings.Default.Volume_max - EngineDesigner.Media.Properties.Settings.Default.Volume_min;
            double _current = _range * (this.volume / 100d);
            double _volume = EngineDesigner.Media.Properties.Settings.Default.Volume_min + _current;

            return (int)_volume;
        }
        //ustvari secondary buffer in mu da ta wave
        private void SetWave(Wave _wave)
        {
            byte[] _bytes = _wave.ToByteArray();

            SoundBufferDescription _soundBufferDescription = new SoundBufferDescription();
            _soundBufferDescription.Format = _wave.WaveFormat;
            _soundBufferDescription.SizeInBytes = _bytes.Length;
            _soundBufferDescription.Flags = BufferFlags.ControlVolume | BufferFlags.ControlFrequency | BufferFlags.ControlPan;

            this.secondarySoundBuffer = new SecondarySoundBuffer(this.directSound, _soundBufferDescription);
            this.secondarySoundBuffer.Write<byte>(_bytes, 0, LockFlags.EntireBuffer);

            this.secondarySoundBuffer.Frequency = _wave.WaveFormat.SamplesPerSecond;

            this.SetVolume();
            this.SetBalance();
        }

        private void SetVolume()
        {
            if (this.secondarySoundBuffer != null)
            {
                if (this.mute)
                {
                    this.secondarySoundBuffer.Volume = -10000; //to je po DXu nula
                }
                else
                {
                    this.secondarySoundBuffer.Volume = this.GetVolume(this.volume);
                }
            }
        }
        private void SetBalance()
        {
            int _pan = (int)(this.balance * 100d);
            if (this.secondarySoundBuffer != null)
            {
                this.secondarySoundBuffer.Pan = _pan;
            }
        }

    }
}
