using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;
using System.Reflection;
using System.Threading;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Common.Serialization;


namespace EngineDesigner.Common
{
    public static class Utility
    {
        public static object CopyObject(object _serializableSource)
        {
            byte[] _bytes = BinarySerializer.Serialize(_serializableSource);
            object _target = BinarySerializer.Deserialize(_bytes);
            return _target;
        }
        public static T CopyObject<T>(T _serializableSource)
        {
            byte[] _bytes = BinarySerializer.Serialize(_serializableSource);
            T _target = BinarySerializer.Deserialize<T>(_bytes);
            return _target;
        }

        public static Color GetInterpolatedColor(Color _color1, Color _color2, double _interpolationPercentage)
        {
            if ((_interpolationPercentage < 0)
                || (_interpolationPercentage > 1))
            {
                throw new Exception("prevelik ali premajhen procent");
            }


            int _a = _color2.A - _color1.A;
            int _r = _color2.R - _color1.R;
            int _g = _color2.G - _color1.G;
            int _b = _color2.B - _color1.B;


            int _interpolatedA = _color1.A + (int)(_a * _interpolationPercentage);
            int _interpolatedR = _color1.R + (int)(_r * _interpolationPercentage);
            int _interpolatedG = _color1.G + (int)(_g * _interpolationPercentage);
            int _interpolatedB = _color1.B + (int)(_b * _interpolationPercentage);


            return Color.FromArgb(_interpolatedA, _interpolatedR, _interpolatedG, _interpolatedB);
        }
        public static Color GetInterpolatedColor(Color _color1, bool _logaritmic, Color _color2, double _interpolationPercentage)
        {
            if (_logaritmic)
            {
                double _log = Math.Abs(Math.Log(_interpolationPercentage));
                if (double.IsInfinity(_log))
                {
                    _log = 0;
                }

                double _logaritmicPercentage = _interpolationPercentage - _log;

                if (_logaritmicPercentage < 0)
                {
                    _logaritmicPercentage = 0;
                }

                return Utility.GetInterpolatedColor(_color1, _color2, _logaritmicPercentage);
            }
            else
            {
                return Utility.GetInterpolatedColor(_color1, _color2, _interpolationPercentage);
            }
        }
        public static Color GetInterpolatedColor(Color _color1, Color _color2, bool _logaritmic, double _interpolationPercentage)
        {
            if (_logaritmic)
            {
                double _logaritmicPercentage = _interpolationPercentage + (_interpolationPercentage * 0.5d);

                if (_logaritmicPercentage > 1)
                {
                    _logaritmicPercentage = 1;
                }

                return Utility.GetInterpolatedColor(_color1, _color2, _logaritmicPercentage);
            }
            else
            {
                return Utility.GetInterpolatedColor(_color1, _color2, _interpolationPercentage);
            }
        }

        public static void Log(params string[] _strings)
        {
            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.Append(DateTime.Now.ToString("o"));
            _stringBuilder.Append(" >  ");

            foreach (string _string in _strings)
            {
                _stringBuilder.Append(_string);
                _stringBuilder.Append(" ");
            }


            System.Diagnostics.Debug.WriteLine(_stringBuilder.ToString());
        }


        public static void NoticeMessage(IWin32Window _owner, string _caption, string _message)
        {
            MessageBox.Show(
                _owner,
                _message,
                _caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk);
        }
        public static void WarningMessage(IWin32Window _owner, string _caption, string _captionReason, string _message)
        {
            MessageBox.Show(
                _owner,
                _message,
                string.Format(
                    "{0} - {1}",
                    _caption,
                    _captionReason),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }
        public static void Exception(IWin32Window _owner, string _caption, Exception _exception)
        {
            MessageBox.Show(
                _owner,
                _exception.Message,
                _caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }


        #region "ComputeFunctionInThreadPool"
        private class ComputeFunctionInThreadPoolStateBase
        {
            public ComputeFunctionInThreadPoolStateBase(double _x)
            {
                this.x = _x;
            }



            protected static long numerOfThreadsNotYetCompleted;

            protected static ManualResetEvent allThreadsDone;
            public static ManualResetEvent AllThreadsDone
            {
                get { return allThreadsDone; }
            }

            protected static List<XY> points;
            public static List<XY> Points
            {
                get
                {
                    lock (points)
                    {
                        return points;
                    }
                }
            }


            protected double x;
            public double X
            {
                get { return x; }
            }


            public static void Reset(long _numerOfThreadsToComplete)
            {
                //to je potrebno, ker imamo preveliko število threadov, da bi delali direktno z WaitCallback
                numerOfThreadsNotYetCompleted = _numerOfThreadsToComplete;

                allThreadsDone = new ManualResetEvent(false);
                points = new List<XY>();
            }
            public void SetResult(double _double)
            {
                lock (points)
                {
                    points.Add(
                        new XY(
                            this.x,
                            _double));
                }

                //če imamo result, pomeni, da je ta thread končan
                if (Interlocked.Decrement(ref numerOfThreadsNotYetCompleted) == 0)
                {
                    //če so vsi threadi končani, signaliziramo konec
                    allThreadsDone.Set();
                }
            }
        }
        //ta je base in z njo dobimo točen state za več različnih (trenutno 2) variant
        private static Function ComputeInThreadPoolBase(double _from, double _to, double _resolution, WaitCallback _waitCallback, Func<double, object> _getState)
        {
            //si zapomnimo originalno nastavitev
            int _workerThreads = 0;
            int _completionPortThreads = 0;
            ThreadPool.GetMinThreads(out _workerThreads, out _completionPortThreads);

            //nastavimo glede na settinge
            ThreadPool.SetMinThreads(
                EngineDesigner.Common.Properties.Settings.Default.MinWorkerThreadsForThreadPoolOperations,
                EngineDesigner.Common.Properties.Settings.Default.MinCompletionPortThreadsForThreadPoolOperations);

            //poskrbimo da je vse na nuli
            long _numerOfThreadsToComplete = (long)((_to - _from) / _resolution);
            ComputeFunctionInThreadPoolStateBase.Reset(_numerOfThreadsToComplete);

            //rešimo funkcijo
            for (double _x = _from; _x < _to; _x += _resolution)
            {
                //zabijemo v threadpool
                ThreadPool.QueueUserWorkItem(_waitCallback, _getState(_x));
            }

            //čakamo, dokler niso vsi končani
            ComputeFunctionInThreadPoolStateBase.AllThreadsDone.WaitOne();

            //povrnemo originalno nastavitev
            ThreadPool.SetMinThreads(_workerThreads, _completionPortThreads);

            //vrnemo
            Function _functionToReturn = Function.FromPoints(ComputeFunctionInThreadPoolStateBase.Points);
            return _functionToReturn;
        }

        public static Function ComputeInThreadPool(double _from, double _to, double _resolution, Func<double, double> _function)
        {
            Function _functionToReturn = ComputeInThreadPoolBase(_from, _to, _resolution,
                new WaitCallback(delegate(object _state)
                {
                    ComputeFunctionInThreadPoolState _computeFunctionInThreadPoolState = (ComputeFunctionInThreadPoolState)_state;

                    //Console.WriteLine(string.Format(
                    //    "Computing in threadpool: {0}",
                    //    _computeFunctionInThreadPoolState.X));

                    double _output = _computeFunctionInThreadPoolState.Function(_computeFunctionInThreadPoolState.X);
                    _computeFunctionInThreadPoolState.SetResult(_output);

                    //Console.WriteLine(string.Format(
                    //    "Computed in threadpool: {0} / {1}",
                    //    _computeFunctionInThreadPoolState.X,
                    //    _output));
                }),
                delegate(double _x)
                {
                    ComputeFunctionInThreadPoolState _computeFunctionInThreadPoolState = new ComputeFunctionInThreadPoolState(_function, _x);
                    return _computeFunctionInThreadPoolState;
                });
            return _functionToReturn;
        }
        private class ComputeFunctionInThreadPoolState : ComputeFunctionInThreadPoolStateBase
        {
            public ComputeFunctionInThreadPoolState(Func<double, double> _function, double _x)
                : base(_x)
            {
                this.function = _function;
            }


            private Func<double, double> function;
            public Func<double, double> Function
            {
                get { return function; }
            }
        }

        public static Function ComputeInThreadPool<T>(double _from, double _to, double _resolution, Func<double, T, double> _function, T _argument)
        {
            Function _functionToReturn = ComputeInThreadPoolBase(_from, _to, _resolution,
                //new WaitCallback(ComputeFunctionInThreadPoolInvoke<T>),
                new WaitCallback(delegate(object _state)
                {
                    ComputeFunctionInThreadPoolState<T> _computeFunctionInThreadPoolState = (ComputeFunctionInThreadPoolState<T>)_state;

                    //Console.WriteLine(string.Format(
                    //    "Computing in threadpool: {0}",
                    //    _computeFunctionInThreadPoolState.X));

                    double _output = _computeFunctionInThreadPoolState.Function(_computeFunctionInThreadPoolState.X, _computeFunctionInThreadPoolState.Argument);
                    _computeFunctionInThreadPoolState.SetResult(_output);

                    //Console.WriteLine(string.Format(
                    //    "Computed in threadpool: {0} / {1}",
                    //    _computeFunctionInThreadPoolState.X,
                    //    _output));
                }),
                delegate(double _x)
                {
                    ComputeFunctionInThreadPoolState<T> _computeFunctionInThreadPoolState = new ComputeFunctionInThreadPoolState<T>(_function, _x, _argument);
                    return _computeFunctionInThreadPoolState;
                });
            return _functionToReturn;
        }
        private class ComputeFunctionInThreadPoolState<T> : ComputeFunctionInThreadPoolStateBase
        {
            public ComputeFunctionInThreadPoolState(Func<double, T, double> _function, double _x, T _argument)
                : base(_x)
            {
                this.function = _function;
                this.argument = _argument;
            }


            private Func<double, T, double> function;
            public Func<double, T, double> Function
            {
                get { return function; }
            }

            private T argument;
            public T Argument
            {
                get { return argument; }
            }
        }
        #endregion "ComputeFunctionInThreadPool"

    }

}
