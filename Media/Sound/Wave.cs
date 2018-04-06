using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SlimDX.DirectSound;
using SlimDX.Multimedia;

using System.IO;
using System.Runtime.InteropServices;
using EngineDesigner.Machine;

namespace EngineDesigner.Media.Sound
{
    public class Wave
    {
        private const string INVALID_FILE_FORMAT = "Invalid file format.";



        private WaveFormat waveFormat;
        public WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }

        private List<byte> header;
        public byte[] Header
        {
            get { return header.ToArray(); }
        }

        private Track waveTrack;
        public Track WaveTrack
        {
            get { return waveTrack; }

            set
            {
                if ((waveTrack.ChannelNumer != value.ChannelNumer)
                    || (waveTrack.BitsPerSample != value.BitsPerSample))
                {
                    throw new ArgumentException("ni združljivo!");
                }

                waveTrack = value;
            }
        }



        public Wave(UnmanagedMemoryStream _stream)
        {
            BinaryReader _binaryReader = new BinaryReader(_stream);


            #region "header"
            header = new List<byte>();

            #region "RIFF"
            if (ReadChunk(_binaryReader, ref header) != "RIFF") //chunk id
            {
                throw new FormatException(INVALID_FILE_FORMAT);
            }

            header.AddRange(BitConverter.GetBytes(_binaryReader.ReadInt32())); //chunk size

            if (ReadChunk(_binaryReader, ref header) != "WAVE") //format
            {
                throw new FormatException(INVALID_FILE_FORMAT);
            }
            #endregion "RIFF"

            #region "fmt"
            if (ReadChunk(_binaryReader, ref header) != "fmt ") //chunk id
            {
                throw new FormatException(INVALID_FILE_FORMAT);
            }

            header.AddRange(BitConverter.GetBytes(_binaryReader.ReadInt32())); //chunk size

            waveFormat = new WaveFormat(); //format bytes
            waveFormat.FormatTag = (WaveFormatTag)_binaryReader.ReadInt16();
            header.AddRange(BitConverter.GetBytes((short)waveFormat.FormatTag));
            waveFormat.Channels = _binaryReader.ReadInt16();
            header.AddRange(BitConverter.GetBytes((short)waveFormat.Channels));
            waveFormat.SamplesPerSecond = _binaryReader.ReadInt32();
            header.AddRange(BitConverter.GetBytes((int)waveFormat.SamplesPerSecond));
            waveFormat.AverageBytesPerSecond = _binaryReader.ReadInt32();
            header.AddRange(BitConverter.GetBytes((int)waveFormat.AverageBytesPerSecond));
            waveFormat.BlockAlignment = _binaryReader.ReadInt16();
            header.AddRange(BitConverter.GetBytes((short)waveFormat.BlockAlignment));
            waveFormat.BitsPerSample = _binaryReader.ReadInt16();
            header.AddRange(BitConverter.GetBytes((short)waveFormat.BitsPerSample));
            #endregion "fmt"
            #endregion "header"

            if (ReadChunk(_binaryReader, ref header) != "data") //chunk id; damo kar u header
            {
                throw new FormatException(INVALID_FILE_FORMAT);
            }

            #region "data"
            this.waveTrack = new Track(this.waveFormat.Channels, this.waveFormat.BitsPerSample);

            int _dataSize = _binaryReader.ReadInt32();
            int _bytesPerSample = waveFormat.BitsPerSample / 8;
            for (int _byteInData = 0; _byteInData < _dataSize / _bytesPerSample / this.waveFormat.Channels; _byteInData++)
            {
                for (int _channel = 0; _channel < waveFormat.Channels; _channel++)
                {
                    Sample _sample = new Sample((byte)waveFormat.BitsPerSample);
                    byte[] _sampleData = new byte[_bytesPerSample];
                    for (int _byteInSample = 0; _byteInSample < _bytesPerSample; _byteInSample++)
                    {
                        _sampleData[_byteInSample] = _binaryReader.ReadByte();


                        //tako lahko mutiramo en kanal!
                        //if (_channel == 1)
                        //{
                        //    _sampleData[_byteInSample] = 0;
                        //}
                    }

                    switch (waveFormat.BitsPerSample)
                    {
                        case 8:
                            _sample.SetData(_sampleData[0]);
                            break;

                        case 16:
                            _sample.SetData(BitConverter.ToInt16(_sampleData, 0));
                            break;


                        default:
                            throw new Exception();
                    }

                    this.waveTrack.Add(_channel, _sample);
                }
            }
            #endregion "data"
        }
        private static string ReadChunk(BinaryReader _binaryReader, ref List<byte> _bytes)
        {
            byte[] _bytesTmp = new byte[4];
            _binaryReader.Read(_bytesTmp, 0, _bytesTmp.Length);

            _bytes.AddRange(_bytesTmp);


            return System.Text.Encoding.ASCII.GetString(_bytesTmp, 0, _bytesTmp.Length);
        }



        public byte[] ToByteArray()
        {
            //NOTE: to sploh ni res!!!
            //List<byte> _bytes = new List<byte>();
            //_bytes.AddRange(this.header);

            //byte[] _track = this.waveTrack.ToByteArray();

            //_bytes.AddRange(BitConverter.GetBytes(_track.Length));
            //_bytes.AddRange(_track);


            //return _bytes.ToArray();


            return this.waveTrack.ToByteArray();
        }

    }


    public class Sample
    {
        public Sample(int _bitsPerSample)
        {
            switch (_bitsPerSample)
            {
                case 8:
                    data = new byte[1]
                        {
                            0
                        };
                    break;

                case 16:
                    data = new byte[2]
                        {
                            0,
                            0
                        };
                    break;


                default:
                    throw new ArgumentException("8 or 16 allowed.");
            }
        }


        private byte[] data;
        public byte[] Data
        {
            get { return data; }
        }


        public int BitsPerSample
        {
            get { return this.data.Length * 8; }
        }


        public void SetData(byte _byte)
        {
            if (this.data.Length != 1)
            {
                throw new ArgumentException();
            }

            this.data[0] = _byte;
        }
        public void SetData(short _short)
        {
            if (this.data.Length != 2)
            {
                throw new ArgumentException();
            }

            this.data = BitConverter.GetBytes(_short);
        }
        public void MixWith(Sample _sample)
        {
            if (_sample.BitsPerSample != this.BitsPerSample)
            {
                throw new ArgumentException("ni združljivo");
            }

            //miksamo s povprečno vrednostjo
            switch (this.BitsPerSample)
            {
                case 8:
                    {
                        int _mixed = (this.data[0] + _sample.data[0]);// / 2;
                        if (_mixed > byte.MaxValue)
                        {
                            _mixed = byte.MaxValue;
                        }

                        this.SetData((byte)_mixed);
                    }
                    break;

                case 16:
                    {
                        short _thisData = BitConverter.ToInt16(this.data, 0);
                        short _sampleData = BitConverter.ToInt16(_sample.data, 0);

                        int _mixed = (_thisData + _sampleData);// / 2;
                        if (_mixed > short.MaxValue)
                        {
                            _mixed = short.MaxValue;
                        }
                        else if (_mixed < short.MinValue)
                        {
                            _mixed = short.MinValue;
                        }

                        this.SetData((short)_mixed);
                    }
                    break;


                default:
                    throw new ArgumentException("8 or 16 allowed.");
            }
        }



        public override string ToString()
        {
            switch (this.BitsPerSample)
            {
                case 8:
                    return string.Format(
                        "{0}",
                        this.data[0].ToString());

                case 16:
                    return string.Format(
                        "{0}",
                        (this.data[0] * this.data[1]).ToString());


                default:
                    throw new NotSupportedException();
            }
        }
    }


    public class Track
    {
        public Track(int _channels, int _bitsPerSample)
        {
            this.bitsPerSample = _bitsPerSample;

            this.samples = new List<List<Sample>>(); //channels _double samples
            for (int a = 0; a < _channels; a++)
            {
                this.samples.Add(new List<Sample>());
            }
        }


        private List<List<Sample>> samples; //channels _double samples


        public int ChannelNumer
        {
            get { return this.samples.Count; }
        }
        public int SampleNumber
        {
            get
            {
                if (this.samples.Count > 0)
                {
                    return this.samples[0].Count; //vseeno s katerega kanala vzamemo, ker mora biti v obeh enako (wav filozofija)
                }

                return 0;
            }
        }
        private int bitsPerSample;
        public int BitsPerSample
        {
            get { return this.bitsPerSample; }
        }


        public Sample this[int _channel, int _index]
        {
            get
            {
                return this.samples[_channel][_index];
            }
        }
        public void Add(int _channel, Sample _sample)
        {
            if (_sample.BitsPerSample != this.bitsPerSample)
            {
                throw new ArgumentException("ni združljivo");
            }

            this.samples[_channel].Add(_sample);
        }
        public byte[] ToByteArray()
        {
            List<byte> _bytes = new List<byte>();

            for (int _sampleIndex = 0; _sampleIndex < this.SampleNumber; _sampleIndex++)
            {
                for (int _channelIndex = 0; _channelIndex < this.ChannelNumer; _channelIndex++)
                {
                    Sample _sample = this[_channelIndex, _sampleIndex];
                    _bytes.AddRange(_sample.Data);
                }
            }

            return _bytes.ToArray();
        }


        public void MixWith(Track _track, int _startAt, bool _addIfNeccessary)
        {
            if (_track.ChannelNumer != this.ChannelNumer)
            {
                throw new ArgumentException("ni združljivo");
            }


            int _sampleNdx = 0;
            for (int _sampleIndex = _startAt; _sampleIndex < this.SampleNumber; _sampleIndex++)
            {
                for (int _channelIndex = 0; _channelIndex < this.ChannelNumer; _channelIndex++)
                {
                    if (_sampleNdx < _track.SampleNumber)
                    {
                        Sample _sample = this[_channelIndex, _sampleIndex];
                        _sample.MixWith(_track[_channelIndex, _sampleNdx]);
                    }
                }

                _sampleNdx++;
                if (_sampleNdx >= _track.SampleNumber)
                {
                    break;
                }
            }


            //če je še kaj ostalo
            if (_sampleNdx < _track.SampleNumber)
            {
                if (_addIfNeccessary)
                {
                    for (int _sampleIndex = _sampleNdx; _sampleIndex < _track.SampleNumber; _sampleIndex++)
                    {
                        for (int _channelIndex = 0; _channelIndex < _track.ChannelNumer; _channelIndex++)
                        {
                            Sample _sample = _track[_channelIndex, _sampleIndex];
                            this.Add(_channelIndex, _sample);
                        }
                    }
                }
            }
        }
        public Track MixWithReturn(Track _track, int _startAt)
        {
            if (_track.ChannelNumer != this.ChannelNumer)
            {
                throw new ArgumentException("ni združljivo");
            }


            int _sampleNdx = 0;
            for (int _sampleIndex = _startAt; _sampleIndex < this.SampleNumber; _sampleIndex++)
            {
                for (int _channelIndex = 0; _channelIndex < this.ChannelNumer; _channelIndex++)
                {
                    if (_sampleNdx < _track.SampleNumber)
                    {
                        Sample _sample = this[_channelIndex, _sampleIndex];
                        _sample.MixWith(_track[_channelIndex, _sampleNdx]);
                    }
                }

                _sampleNdx++;
                if (_sampleNdx >= _track.SampleNumber)
                {
                    break;
                }
            }


            //če je še kaj ostalo
            if (_sampleNdx < _track.SampleNumber)
            {
                Track _trackToReturn = new Track(this.ChannelNumer, this.bitsPerSample);

                for (int _sampleIndex = _sampleNdx; _sampleIndex < _track.SampleNumber; _sampleIndex++)
                {
                    for (int _channelIndex = 0; _channelIndex < _track.ChannelNumer; _channelIndex++)
                    {
                        Sample _sample = _track[_channelIndex, _sampleIndex];
                        _trackToReturn.Add(_channelIndex, _sample);
                    }
                }

                return _trackToReturn;
            }


            return null;
        }
        public void AddTo(Track _track)
        {
            if (_track.ChannelNumer != this.ChannelNumer)
            {
                throw new ArgumentException("ni združljivo");
            }


            for (int _sampleIndex = 0; _sampleIndex < _track.SampleNumber; _sampleIndex++)
            {
                for (int _channelIndex = 0; _channelIndex < _track.ChannelNumer; _channelIndex++)
                {
                    Sample _sample = _track[_channelIndex, _sampleIndex];
                    this.Add(_channelIndex, _sample);
                }
            }
        }
        public void Cut(int _from, int _length)
        {
            Track _track = new Track(this.ChannelNumer, this.bitsPerSample);

            for (int _sampleIndex = _from; _sampleIndex < _from + _length; _sampleIndex++)
            {
                for (int _channel = 0; _channel < this.ChannelNumer; _channel++)
                {
                    _track.Add(_channel, this[_channel, _sampleIndex]);
                }
            }

            this.samples = _track.samples;
        }
        public Track SubTrack(int _from, int _length)
        {
            Track _track = new Track(this.ChannelNumer, this.bitsPerSample);

            for (int _sampleIndex = _from; _sampleIndex < _from + _length; _sampleIndex++)
            {
                for (int _channel = 0; _channel < this.ChannelNumer; _channel++)
                {
                    _track.Add(_channel, this[_channel, _sampleIndex]);
                }
            }

            return _track;
        }
        public void Trim(int _length)
        {
            if (this.SampleNumber > _length)
            {
                Track _track = new Track(this.ChannelNumer, this.bitsPerSample);

                for (int _sampleIndex = 0; _sampleIndex < _length; _sampleIndex++)
                {
                    for (int _channel = 0; _channel < this.ChannelNumer; _channel++)
                    {
                        _track.Add(_channel, this[_channel, _sampleIndex]);
                    }
                }

                this.samples = _track.samples;
            }
        }
        public Track GenerateSilence(int _length)
        {
            Track _track = new Track(this.ChannelNumer, this.bitsPerSample);

            for (int b = 0; b < _length; b++)
            {
                for (int a = 0; a < this.ChannelNumer; a++)
                {
                    _track.Add(a, new Sample(this.bitsPerSample));
                }
            }

            return _track;
        }
    }


}
