using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using EngineDesigner.Common.Serialization;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.Common.Definitions
{
    /// <summary>
    /// Represents a mathematical function f(x).
    /// </summary>
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class Function : Serializable<Function>, System.Collections.IEnumerable
    {
        protected Function(bool _sortedByX)
        {
            this.sortedByX = _sortedByX;
        }



        [DataMember]
        protected XY[] points = null;

        //načeloma so vse funkcije sortirane po X naraščajoče; v izjemnih primerih potrebujem nesortirano fukncijo in takrat je to false - zaenkrat ne znam drugače rešit
        //zaenkrat mora bit zapisano tudi v fajlu - ni nevem kako fanj, ma drugače ne vem kako bi to ugotovil
        [DataMember]
        private bool sortedByX;
        public bool Unsorted
        {
            get { return !this.sortedByX; }
        }

        private object _tag;
        public object _Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }



        public double GetFx(double _x)
        {
            foreach (XY _xy in this.points)
            {
                if (_xy.X == _x)
                {
                    return _xy.Y;
                }
            }

            return double.NaN;
        }
        public double GetFx(double _x, string _comparator)
        {
            foreach (XY _xy in this.points)
            {
                if (_xy.X.ToString(_comparator) == _x.ToString(_comparator))
                {
                    return _xy.Y;
                }
            }

            return double.NaN;
        }



        public int Length
        {
            get
            {
                if (this.points != null)
                {
                    return points.Length;
                }
                else
                {
                    return 0;
                }
            }
        }
        public XY this[int _index]
        {
            get
            {
                if (this.points != null)
                {
                    return this.points[_index];
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        /// <summary>
        /// Gets the index of the given point (compared by hash code).
        /// </summary>
        /// <param name="_xy">The point to check for in the function.</param>
        /// <returns></returns>
        public int GetPointIndex(XY _xy)
        {
            for (int a = 0; a < this.points.Length; a++)
            {
                if (this.points[a] == _xy)
                {
                    return a;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets a function from given points.
        /// </summary>
        /// <param name="_points">The array of points that represent the function.</param>
        public static Function FromPoints(IEnumerable<XY> _points)
        {
            return Function.FromPoints(_points, true);
        }
        /// <summary>
        /// Gets a function from given points.
        /// </summary>
        /// <param name="_points">The array of points that represent the function.</param>
        /// <param name="_unsorted">Defines whether _double values will be sorted incrementally.</param>
        public static Function FromPoints(IEnumerable<XY> _points, bool _sorted)
        {
            Function _function = new Function(_sorted);


            //prepričamo se, da se X ne ponavlja
            List<double> _list = new List<double>();
            foreach (XY _xy in _points)
            {
                if (_sorted)
                {
                    if (_list.Contains(_xy.X))
                    {
                        throw new Exception("There are more X components of equal value.");
                    }
                }

                _list.Add(_xy.X);
            }


            XY[] _xys;

            if (_sorted)
            {
                _xys = XY.SortByX(_points.ToArray());
            }
            else
            {
                _xys = _points.ToArray();
            }

            _function.points = new XY[_xys.Length];
            _xys.CopyTo(_function.points, 0);


            return _function;
        }
        /// <summary>
        /// Gets an empty function.
        /// </summary>
        public static Function Empty
        {
            get
            {
                Function _function = new Function(true);
                _function.points = new XY[0];

                return _function;
            }
        }
        //OBSOLETE:       
        ///// <summary>
        ///// Gets an sine function.
        ///// </summary>
        ///// <param name="_from">The starting angle, in degrees.</param>
        ///// <param name="_to">The ending angle, in degrees.</param>
        ///// <param name="_resolution">The step-up fraction when computing the function.</param>
        //public static Function Sine(double _from, double _to, double _resolution)
        //{
        //    Function _function = new Function(true);

        //    List<XY> _points = new List<XY>();
        //    for (double _double = _from; _double < _to; _double += _resolution)
        //    {
        //        _points.Add(new XY(
        //            _double,
        //            Math.Sin(Conversions.DegToRad(_double))));
        //    }

        //    _function.points = _points.ToArray();

        //    return _function;
        //}
        ///// <summary>
        ///// Gets an inverted sine function.
        ///// </summary>
        ///// <param name="_from">The starting angle, in degrees.</param>
        ///// <param name="_to">The ending angle, in degrees.</param>
        ///// <param name="_resolution">The step-up fraction when computing the function.</param>
        //public static Function InvertedSine(double _from, double _to, double _resolution)
        //{
        //    Function _function = new Function(true);

        //    List<XY> _points = new List<XY>();
        //    for (double _double = _from; _double < _to; _double += _resolution)
        //    {
        //        _points.Add(new XY(
        //            _double,
        //            -Math.Sin(Conversions.DegToRad(_double))));
        //    }

        //    _function.points = _points.ToArray();

        //    return _function;
        //}
        ///// <summary>
        ///// Gets an cosine function.
        ///// </summary>
        ///// <param name="_from">The starting angle, in degrees.</param>
        ///// <param name="_to">The ending angle, in degrees.</param>
        ///// <param name="_resolution">The step-up fraction when computing the function.</param>
        //public static Function Cosine(double _from, double _to, double _resolution)
        //{
        //    Function _function = new Function(true);

        //    List<XY> _points = new List<XY>();
        //    for (double _double = _from; _double < _to; _double += _resolution)
        //    {
        //        _points.Add(new XY(
        //            _double,
        //            Math.Cos(Conversions.DegToRad(_double))));
        //    }

        //    _function.points = _points.ToArray();

        //    return _function;
        //}
        ///// <summary>
        ///// Gets an inverted cosine function.
        ///// </summary>
        ///// <param name="_from">The starting angle, in degrees.</param>
        ///// <param name="_to">The ending angle, in degrees.</param>
        ///// <param name="_resolution">The step-up fraction when computing the function.</param>
        //public static Function InvertedCosine(double _from, double _to, double _resolution)
        //{
        //    Function _function = new Function(true);

        //    List<XY> _points = new List<XY>();
        //    for (double _double = _from; _double < _to; _double += _resolution)
        //    {
        //        _points.Add(new XY(
        //            _double,
        //            -Math.Cos(Conversions.DegToRad(_double))));
        //    }

        //    _function.points = _points.ToArray();

        //    return _function;
        //}
        /// <summary>
        /// Gets a constant value function.
        /// </summary>
        /// <param name="_from">The starting angle, in degrees.</param>
        /// <param name="_to">The ending angle, in degrees.</param>
        /// <param name="_resolution">The step-up fraction when computing the function.</param>
        /// <param name="_value">The value of the function.</param>
        public static Function Constant(double _from, double _to, double _resolution, double _value)
        {
            Function _function = new Function(true);

            List<XY> _points = new List<XY>();
            for (double _x = _from; _x < _to; _x += _resolution)
            {
                _points.Add(new XY(
                    _x,
                    _value));
            }

            _function.points = _points.ToArray();

            return _function;
        }

        public static Function SuperpositonX(params Function[] _functions)
        {
            if (_functions.Length < 2)
            {
                throw new ArgumentException();
            }


            Dictionary<double, double> _dictionary = new Dictionary<double, double>(); //_double, y

            foreach (Function _function in _functions)
            {
                if (!_function.sortedByX)
                {
                    throw new Exception("Cannot operate with unsorted functions.");
                }


                foreach (XY _xy in _function)
                {
                    if (_dictionary.ContainsKey(_xy.X))
                    {
                        _dictionary[_xy.X] += _xy.Y;
                    }
                    else
                    {
                        _dictionary.Add(_xy.X, _xy.Y);
                    }
                }
            }


            List<XY> _list = new List<XY>();

            foreach (double _double in _dictionary.Keys)
            {
                XY _xy = new XY(
                    _double,
                    _dictionary[_double]);
                _list.Add(_xy);
            }


            return Function.FromPoints(_list);


            //OBSOLETE: tu je blo po starem
            //Function _main = _functions[0];
            //List<Function> _other = new List<Function>();
            //for (int a = 1; a < _functions.Length; a++)
            //{
            //    _other.Add(_functions[a]);
            //}


            //List<XY> _list = new List<XY>();

            //for (int a = 0; a < _main.points.Length; a++)
            //{
            //    XY _xy = new XY();

            //    foreach (Function _function in _other)
            //    {
            //        if (_main.points[a].Y != _function.points[a].Y)
            //        {
            //            throw new Exception();
            //        }
            //        else
            //        {
            //            _xy.X = _main.points[a].X + _function.points[a].X;
            //        }
            //    }

            //    _xy.Y = _main.points[a].Y;

            //    _list.Add(_xy);
            //}

            //return Function.FromPoints(_list);
        }
        public static Function SuperpositonY(params Function[] _functions)
        {
            if (_functions.Length < 2)
            {
                throw new ArgumentException();
            }


            Dictionary<double, double> _dictionary = new Dictionary<double, double>(); //_double, y

            foreach (Function _function in _functions)
            {
                if (!_function.sortedByX)
                {
                    throw new Exception("Cannot operate with unsorted functions.");
                }


                foreach (XY _xy in _function)
                {
                    if (_dictionary.ContainsKey(_xy.X))
                    {
                        _dictionary[_xy.X] += _xy.Y;
                    }
                    else
                    {
                        _dictionary.Add(_xy.X, _xy.Y);
                    }
                }
            }


            List<XY> _list = new List<XY>();

            foreach (double _double in _dictionary.Keys)
            {
                XY _xy = new XY(
                    _double,
                    _dictionary[_double]);
                _list.Add(_xy);
            }


            return Function.FromPoints(_list);


            //OBSOLETE: tu je blo po starem
            //Function _main = _functions[0];
            //List<Function> _other = new List<Function>();
            //for (int a = 1; a < _functions.Length; a++)
            //{
            //    _other.Add(_functions[a]);
            //}


            //List<XY> _list = new List<XY>();

            //for (int a = 0; a < _main.points.Length; a++)
            //{
            //    XY _xy = new XY();

            //    _xy.X = _main.points[a].X;

            //    foreach (Function _function in _other)
            //    {
            //        if (_main.points[a].X != _function.points[a].X)
            //        {
            //            throw new Exception();
            //        }
            //        else
            //        {
            //            _xy.Y = _main.points[a].Y + _function.points[a].Y;
            //        }
            //    }

            //    _list.Add(_xy);
            //}

            //return Function.FromPoints(_list);
        }
        public static Function AverageX(params Function[] _functions)
        {
            Function _function;


            if (_functions.Length < 1)
            {
                throw new ArgumentException();
            }
            else if (_functions.Length == 1)
            {
                _function = _functions[0];
            }
            else //if (_functions.Length > 1)
            {
                //najprej superpozicija!
                _function = Function.SuperpositonX(_functions);
            }


            double _averageX = _function.GetAverageX();


            List<XY> _list = new List<XY>();

            foreach (XY _xyTmp in _function)
            {
                XY _xy = new XY(
                    _averageX,
                    _xyTmp.Y);
                _list.Add(_xy);
            }


            return Function.FromPoints(_list);
        }
        public static Function AverageY(params Function[] _functions)
        {
            Function _function;


            if (_functions.Length < 1)
            {
                throw new ArgumentException();
            }
            else if (_functions.Length == 1)
            {
                _function = _functions[0];
            }
            else //if (_functions.Length > 1)
            {
                //najprej superpozicija!
                _function = Function.SuperpositonY(_functions);
            }


            double _averageY = _function.GetAverageY();


            List<XY> _list = new List<XY>();

            foreach (XY _xyTmp in _function)
            {
                XY _xy = new XY(
                    _xyTmp.X,
                    _averageY);
                _list.Add(_xy);
            }


            return Function.FromPoints(_list);
        }

        /// <summary>
        /// Creates a function based on the given return data from the supplied delegate.
        /// </summary>
        /// <param name="_from">The starting angle, in degrees.</param>
        /// <param name="_to">The ending angle, in degrees.</param>
        /// <param name="_resolution">The step-up fraction when computing the function.</param>
        /// <param name="_function">The delegate for supplying return data.</param>
        public static Function Compute(double _from, double _to, double _resolution, Func<double, double> _function)
        {
            List<XY> _points = new List<XY>();

            for (double _x = _from; _x < _to; _x += _resolution)
            {
                double _y = _function(_x);
                _points.Add(new XY(_x, _y));
            }

            return Function.FromPoints(_points);
        }

        /// <summary>
        /// Gets the minimum _double of the function.
        /// </summary>
        public double GetMinX()
        {
            double _double = 0;
            return this.GetMinX(out _double);
        }
        /// <summary>
        /// Gets the minimum _double of the function.
        /// </summary>
        /// <param name="_yAtMinX">The value of Y at minimum _double.</param>
        public double GetMinX(out double _yAtMinX)
        {
            _yAtMinX = double.NaN;

            double _minX = double.MaxValue;
            foreach (XY _xy in this.points)
            {
                if (_xy.X < _minX)
                {
                    _minX = _xy.X;
                    _yAtMinX = _xy.Y;
                }
            }

            return _minX;
        }
        /// <summary>
        /// Gets the maximum _double of the function.
        /// </summary>
        public double GetMaxX()
        {
            double _double = 0;
            return this.GetMaxX(out _double);
        }
        /// <summary>
        /// Gets the maximum _double of the function.
        /// </summary>
        /// <param name="_yAtMaxX">The value of Y at maximum _double.</param>
        public double GetMaxX(out double _yAtMaxX)
        {
            _yAtMaxX = double.NaN;

            double _maxX = double.MinValue;
            foreach (XY _xy in this.points)
            {
                if (_xy.X > _maxX)
                {
                    _maxX = _xy.X;
                    _yAtMaxX = _xy.Y;
                }
            }

            return _maxX;
        }
        /// <summary>
        /// Gets the average _double of the function.
        /// </summary>
        public double GetAverageX()
        {
            List<double> _points = new List<double>();
            foreach (XY _xy in points)
            {
                _points.Add(_xy.X);
            }

            return Mathematics.GetAverage(_points.ToArray());
        }

        /// <summary>
        /// Gets the minimum y of the function.
        /// </summary>
        public double GetMinY()
        {
            double _double = 0;
            return this.GetMinY(out _double);
        }
        /// <summary>
        /// Gets the minimum y of the function.
        /// </summary>
        /// <param name="_yAtMinY">The value of Y at minimum y.</param>
        public double GetMinY(out double _xAtMinY)
        {
            _xAtMinY = double.NaN;

            double _minY = double.MaxValue;
            foreach (XY _xy in this.points)
            {
                if (_xy.Y < _minY)
                {
                    _minY = _xy.Y;
                    _xAtMinY = _xy.X;
                }
            }

            return _minY;
        }
        /// <summary>
        /// Gets the maximum y of the function.
        /// </summary>
        public double GetMaxY()
        {
            double _double = 0;
            return this.GetMaxY(out _double);
        }
        /// <summary>
        /// Gets the maximum y of the function.
        /// </summary>
        /// <param name="_yAtMayY">The value of Y at maximum y.</param>
        public double GetMaxY(out double _xAtMinY)
        {
            _xAtMinY = double.NaN;

            double _maxY = double.MinValue;
            foreach (XY _xy in this.points)
            {
                if (_xy.Y > _maxY)
                {
                    _maxY = _xy.Y;
                    _xAtMinY = _xy.X;
                }
            }

            return _maxY;
        }
        /// <summary>
        /// Gets the average y of the function.
        /// </summary>
        /// <returns></returns>
        public double GetAverageY()
        {
            List<double> _points = new List<double>();
            foreach (XY _xy in points)
            {
                _points.Add(_xy.Y);
            }

            return Mathematics.GetAverage(_points.ToArray());
        }

        /// <summary>
        /// Converts the y component to a precentage representation. Values will be represented as percentages of the given value.
        /// </summary>
        /// <param name="_valueFor100">Defines the value that represents 100 percents.</param>
        public void ConvertYToPercents(double _valueFor100)
        {
            for (int a = 0; a < this.points.Length; a++)
            {
                this.points[a].Y = (this.points[a].Y / _valueFor100) * 100d;
            }
        }
        /// <summary>
        /// Converts the y component to a precentage representation. Values will be represented as percentages between the given values.
        /// </summary>
        /// <param name="_valueFor0">Defines the value that represents 0 percents.</param>
        /// <param name="_valueFor100">Defines the value that represents 100 percents.</param>
        public void ConvertYToPercents(double _valueFor0, double _valueFor100)
        {
            this.ConvertYToPercents(_valueFor0, _valueFor100, false);
        }
        /// <summary>
        /// Converts the y component to a precentage representation. Values will be represented as percentages between the given values.
        /// </summary>
        /// <param name="_valueFor0orNegative100">Defines the value that represents 0 (or -100) percents.</param>
        /// <param name="_valueFor100">Defines the value that represents 100 percents.</param>
        /// <param name="_minIsNegative100">Defines whether the lower value should represent -100 percents instead of 0 percents.</param>
        public void ConvertYToPercents(double _valueFor0orNegative100, double _valueFor100, bool _minIsNegative100)
        {
            double _onePercent = Math.Abs(_valueFor100 - _valueFor0orNegative100) / 100d;

            for (int a = 0; a < this.points.Length; a++)
            {
                this.points[a].Y = (this.points[a].Y - _valueFor0orNegative100) / _onePercent;

                if (_minIsNegative100)
                {
                    this.points[a].Y = this.points[a].Y - (100d - this.points[a].Y);
                }
            }
        }

        /// <summarx>
        /// Converts the _double component to a precentage representation. Values will be represented as percentages of the given value.
        /// </summarx>
        /// <param name="_valueFor100">Defines the value that represents 100 percents.</param>
        public void ConvertXToPercents(double _valueFor100)
        {
            for (int a = 0; a < this.points.Length; a++)
            {
                this.points[a].X = (this.points[a].X / _valueFor100) * 100d;
            }
        }
        /// <summarx>
        /// Converts the _double component to a precentage representation. Values will be represented as percentages between the given values.
        /// </summarx>
        /// <param name="_valueFor0">Defines the value that represents 0 percents.</param>
        /// <param name="_valueFor100">Defines the value that represents 100 percents.</param>
        public void ConvertXToPercents(double _valueFor0, double _valueFor100)
        {
            this.ConvertXToPercents(_valueFor0, _valueFor100, false);
        }
        /// <summarx>
        /// Converts the _double component to a precentage representation. Values will be represented as percentages between the given values.
        /// </summarx>
        /// <param name="_valueFor0orNegative100">Defines the value that represents 0 (or -100) percents.</param>
        /// <param name="_valueFor100">Defines the value that represents 100 percents.</param>
        /// <param name="_minIsNegative100">Defines whether the lower value should represent -100 percents instead of 0 percents.</param>
        public void ConvertXToPercents(double _valueFor0orNegative100, double _valueFor100, bool _minIsNegative100)
        {
            double _onePercent = Math.Abs(_valueFor100 - _valueFor0orNegative100) / 100d;

            for (int a = 0; a < this.points.Length; a++)
            {
                this.points[a].X = (this.points[a].X - _valueFor0orNegative100) / _onePercent;

                if (_minIsNegative100)
                {
                    this.points[a].X = this.points[a].X - (100d - this.points[a].X);
                }
            }
        }

        public bool ContainsX(double _x)
        {
            foreach (XY _xy in this.points)
            {
                if (_xy.X == _x)
                {
                    return true;
                }
            }

            return false;
        }
        public bool ContainsY(double _y)
        {
            foreach (XY _xy in this.points)
            {
                if (_xy.Y == _y)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasSmallerMagnitudeX(Function _function)
        {
            bool _bool = true;

            for (int a = 0; a < this.points.Length; a++)
            {
                //če je samo enkrat večje, je dovolj!
                if (this.points[a].X > _function.points[a].X)
                {
                    _bool = false;
                }
            }

            return _bool;
        }
        public bool HasBiggerMagnitudeX(Function _function)
        {
            bool _bool = false;

            for (int a = 0; a < this.points.Length; a++)
            {
                //če je samo enkrat večje, je dovolj!
                if (this.points[a].X > _function.points[a].X)
                {
                    _bool = true;
                }
            }

            return _bool;
        }
        public bool HasSmallerMagnitudeY(Function _function)
        {
            bool _bool = true;

            for (int a = 0; a < this.points.Length; a++)
            {
                //če je samo enkrat večje, je dovolj!
                if (this.points[a].Y > _function.points[a].Y)
                {
                    _bool = false;
                }
            }

            return _bool;
        }
        public bool HasBiggerMagnitudeY(Function _function)
        {
            bool _bool = false;

            for (int a = 0; a < this.points.Length; a++)
            {
                //če je samo enkrat večje, je dovolj!
                if (this.points[a].Y > _function.points[a].Y)
                {
                    _bool = true;
                }
            }

            return _bool;
        }

        public string ToString(bool _listPoints)
        {
            StringBuilder _stringBuilder = new StringBuilder();

            foreach (XY _xy in this.points)
            {
                _stringBuilder.AppendLine(_xy.ToString());
            }

            return _stringBuilder.ToString();
        }

        public Polygon ToPolygon()
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }


            return Polygon.From(this.points);
        }


        //predela funkcijo, da je povprečje 0
        public Function Normalize()
        {
            List<XY> _points = new List<XY>();

            double _average = this.GetAverageY();
            foreach (XY _xyTmp in this)
            {
                XY _xy = new XY();
                _xy.X = _xyTmp.X;

                _xy.Y = this.GetFx(_xyTmp.X) - _average;
                _points.Add(_xy);
            }

            return Function.FromPoints(_points);
        }

        #region "Interpolation"
        private Function ToLinearInterpolation(int _totalNumberOfPoints)
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }


            double _xStepSize = (this.points[this.points.Length - 1].X - this.points[0].X) / (_totalNumberOfPoints - 1);


            double[] _interpolatedXValues = new double[_totalNumberOfPoints];
            for (int a = 0; a < _totalNumberOfPoints; a++)
            {
                _interpolatedXValues[a] = this.points[0].X + a * _xStepSize;
            }


            return this.ToLinearInterpolation(_interpolatedXValues);
        }
        private Function ToLinearInterpolation(double _resolution)
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }

            if ((_resolution <= 0)
                || (_resolution >= 1))
            {
                throw new ArgumentException();
            }


            List<double> _interpolatedXValues = new List<double>();

            for (int a = 0; a < this.points.Length - 1; a++)
            {
                double _startX = this.points[a].X;
                double _endX = this.points[a + 1].X;
                double _increment = (_endX - _startX) * _resolution;

                for (double _double = _startX; _double <= _endX + (_increment / 2)/*s tem poštukamo pomankljivosti od floating pointa*/; _double += _increment)
                {
                    if (!_interpolatedXValues.Contains(_double))
                    {
                        _interpolatedXValues.Add(_double);
                    }
                }
            }

            return this.ToLinearInterpolation(_interpolatedXValues.ToArray());
        }
        private Function ToLinearInterpolation(double[] _desiredXValues)
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }


            List<XY> _interpolatedValues = new List<XY>();


            for (int a = 0; a < _desiredXValues.Length; a++)
            {
                double _desiredX = _desiredXValues[a];


                XY _foundXY = XY.Empty;
                foreach (XY _xy in this.points)
                {
                    if (_xy.X == _desiredX)
                    {
                        _foundXY = _xy;
                        break;
                    }
                }


                if (!_foundXY.IsEmpty())
                {
                    _interpolatedValues.Add(_foundXY);
                }
                else
                {
                    double _previousX = this.GetPreviousXY(_desiredX).X;
                    double _nextX = this.GetNextXY(_desiredX).X;

                    if (double.IsNaN(_previousX)) //smo pred prvo točko
                    {
                        _previousX = this.points[0].X;
                        _nextX = this.GetNextXY(_previousX).X;
                    }
                    else if (double.IsNaN(_nextX)) //smo za zadnjo točko
                    {
                        _nextX = this.points[this.points.Length - 1].X;
                        _previousX = this.GetPreviousXY(_nextX).X;
                    }

                    double _previousY = this.GetFx(_previousX);
                    double _nextY = this.GetFx(_nextX);


                    double _interpolatedY = _previousY + (_nextY - _previousY) * ((_desiredXValues[a] - _previousX) / (_nextX - _previousX));

                    _interpolatedValues.Add(new XY(
                        _desiredX,
                        _interpolatedY));
                }
            }


            return Function.FromPoints(_interpolatedValues);
        }


        /// <summary><para>
        /// Calculate the Fritsch-Carlson monotone cubic spline interpolation for the 
        /// given abscissa vector _double and ordinate vector y. 
        /// All vectors must have conformant dimenions.
        /// The abscissa vector must be strictly increasing.
        /// </para>
        /// <para>
        /// The Fritsch-Carlson interpolation produces a neat monotone
        /// piecewise cubic curve, which is especially suited for the
        /// presentation of scientific data. 
        /// This is the state of the art to create curves that preserve
        /// monotonicity, although it is not so well known as Akima's
        /// interpolation. The commonly used Akima interpolation doesn't 
        /// produce so pleasant results.
        /// </para>
        /// <code>
        /// Reference:
        ///    F.N.Fritsch,R.E.Carlson: Monotone Piecewise Cubic
        ///    Interpolation, SIAM J. Numer. Anal. Vol 17, No. 2, 
        ///    April 1980
        ///
        /// Copyright (C) 1991-1998 by Berndt M. Gammel
        /// Translated to C# by Dirk Lellinger.
        /// </code>
        /// </summary>
        private class FritschCarlsonCubicSpline
        {
            /// <summary>
            /// Reference to the vector of the independent variable.
            /// </summary>
            private Vector x;
            /// <summary>
            /// Reference to the vector of the dependent variable.
            /// </summary>
            private Vector y;

            private Vector y1 = new Vector();
            private Vector y2 = new Vector();
            private Vector y3 = new Vector();



            public FritschCarlsonCubicSpline(Vector _x, Vector _y)
            {
                this.Interpolate(_x, _y);
            }



            public double GetY(double _x)
            {
                return CubicSplineHorner(_x, x, y, y1, y2, y3);
            }



            /// <summary>
            /// Calculate the spline coefficients y2(a) and y3(a) for a natural cubic
            /// spline, given the abscissa _double(a), the ordinate y(a), and the 1st 
            /// derivative y1(a).
            /// </summary>
            /// <param name="_double">The vector (lo,hi) of data abscissa (must be strictly increasing).</param>
            /// <param name="y">The vector (lo,hi) of ordinate.</param>
            /// <param name="y1">The vector containing the 1st derivative y'(_double(a)).</param>
            /// <param name="y2">Output: the spline coefficients y2(a).</param>
            /// <param name="y3">Output: the spline coefficients y3(a).</param>
            /// <remarks><code>
            /// The spline interpolation can then be evaluated using Horner's rule
            ///
            ///      P(u) = y(a) + dx * (y1(a) + dx * (y2(a) + dx * y3(a)))
            ///
            /// where  _double(a) &lt;= u &lt; _double(a+1) and dx = u - _double(a).
            /// </code></remarks>
            private void CubicSplineCoefficients(Vector _x, Vector _y, Vector _y1, Vector _y2, Vector _y3)
            {
                int lo = _x.LowerBound,
                  hi = _x.UpperBound;

                for (int i = lo; i < hi; i++)
                {
                    double h = _x[i + 1] - _x[i],
                      mi = (_y[i + 1] - _y[i]) / h;
                    _y2[i] = (3 * mi - 2 * _y1[i] - _y1[i + 1]) / h;
                    _y3[i] = (_y1[i] + _y1[i + 1] - 2 * mi) / (h * h);
                }

                _y2[hi] = _y3[hi] = 0.0;
            }
            /// <summary>
            /// Return True if vectors have the same index range, False otherwise.
            /// </summary>
            /// <param name="a">First vector.</param>
            /// <param name="b">Second vector.</param>
            /// <returns>True if both vectors have the same LowerBounds and the same UpperBounds.</returns>
            private static bool MatchingIndexRange(Vector _a, Vector _b)
            {
                return (_a.LowerBound == _b.LowerBound && _a.UpperBound == _b.UpperBound);
            }
            /// <summary>
            /// int MpFritschCarlsonCubicSpline::Interpolate (const Vector &_double, const Vector &y)
            ///
            /// Calculate the Fritsch-Carlson monotone cubic spline interpolation for the 
            /// given abscissa vector _double and ordinate vector y. 
            /// All vectors must have conformant dimenions.
            /// The abscissa vector must be strictly increasing.
            ///
            /// The Fritsch-Carlson interpolation produces a neat monotone
            /// piecewise cubic curve, which is especially suited for the
            /// presentation of scientific data. 
            /// This is the state of the art to create curves that preserve
            /// monotonicity, although it is not so well known as Akima's
            /// interpolation. The commonly used Akima interpolation doesn't 
            /// produce so pleasant results.
            ///
            /// Reference:
            ///    F.N.Fritsch,R.E.Carlson: Monotone Piecewise Cubic
            ///    Interpolation, SIAM J. Numer. Anal. Vol 17, No. 2, 
            ///    April 1980
            ///
            /// Copyright (C) 1991-1998 by Berndt M. Gammel
            /// </summary>
            private int Interpolate(Vector _x, Vector _y)
            {
                // check input parameters

                if (!MatchingIndexRange(_x, _y))
                    throw new ArgumentException("index range mismatch of vectors");

                // link original data vectors into base class
                this.x = _x;
                this.y = _y;

                // Empty data vectors - free auxilliary storage
                if (_x.Length == 0)
                {
                    y1.Clear();
                    y2.Clear();
                    y3.Clear();
                    return 0; // ok
                }

                int lo = _x.LowerBound,
                  hi = _x.UpperBound;

                // Resize the auxilliary vectors. Note, that there is no reallocation if the
                // vector already has the appropriate dimension.
                y1.Resize(lo, hi);
                y2.Resize(lo, hi);
                y3.Resize(lo, hi);

                if (_x.Length == 1)
                {

                    // default derivative is 0.0
                    y1[lo] = y2[lo] = y3[lo] = 0.0;

                }
                else if (_x.Length == 2)
                {

                    // set derivatives for a line
                    y1[lo] = y1[hi] = (_y[hi] - _y[lo]) / (_x[hi] - _x[lo]);
                    y2[lo] = y2[hi] =
                      y3[lo] = y3[hi] = 0.0;

                }
                else
                { // three or more points

                    // initial guess derivative vector 
                    y1[lo] = deriv1(_x, _y, lo + 1, -1);
                    y1[hi] = deriv1(_x, _y, hi - 1, 1);
                    for (int i = lo + 1; i < hi; i++)
                        y1[i] = deriv2(_x, _y, i);

                    if (_x.Length > 3)
                    {

                        // adjust derivatives at boundaries
                        if (y1[lo] * y1[lo + 1] < 0) y1[lo] = 0;
                        if (y1[hi] * y1[hi - 1] < 0) y1[hi] = 0;

                        // adjustment of cubic interpolant 
                        fritsch(_x, _y, y1);
                    }

                    // calculate remaining spline coefficients y2(a) and y3(a)
                    CubicSplineCoefficients(_x, _y, y1, y2, y3);

                }

                return 0; // ok
            }
            /// <summary>
            /// Return the interpolation value P(u) for a piecewise cubic curve determined
            /// by the abscissa vector _double, the ordinate vector y, the 1st derivative
            /// vector y1, the 2nd derivative vector y2, and the 3rd derivative vector y3,
            /// using the Horner scheme. 
            /// </summary>
            /// <param name="u">The abscissa value at which the interpolation is to be evaluated.</param>
            /// <param name="_double">The vector (lo,hi) of data abscissa (must be strictly increasing).</param>
            /// <param name="y">The vectors (lo,hi) of ordinate</param>
            /// <param name="y1">contains the 1st derivative y'(_double(a))</param>
            /// <param name="y2">contains the 2nd derivative y''(_double(a))</param>
            /// <param name="y3">contains the 3rd derivative y'''(_double(a))</param>
            /// <returns>P(u) = y(a) + dx * (y1(a) + dx * (y2(a) + dx * y3(a))).
            /// In the special case of empty data vectors (_double,y) a value of 0.0 is returned.</returns>
            /// <remarks><code>
            /// All vectors must have conformant dimenions.
            /// The abscissa _double(a) values must be strictly increasing.
            /// 
            ///
            /// This subroutine evaluates the function
            ///
            ///    P(u) = y(a) + dx * (y1(a) + dx * (y2(a) + dx * y3(a)))
            ///
            /// where  _double(a) &lt;= u &lt; _double(a+1) and dx = u - _double(a), using Horner's rule
            ///
            ///    lo &lt;= a &lt;= hi is the index range of the vectors.
            ///    if  u &lt;  _double(lo) then  a = lo  is used.
            ///    if  u &lt;= _double(hi) then  a = hi  is used.
            ///
            ///    A fast binary search is performed to determine the proper interval.
            /// </code></remarks>
            private double CubicSplineHorner(double _u, Vector _x, Vector _y, Vector _y1, Vector _y2, Vector _y3)
            {
                // special case that there are no data. Return 0.0.
                if (_x.Length == 0) return 0.0;

                int i = FindInterval(_u, _x);
                if (i < _x.LowerBound) i = _x.LowerBound;  // extrapolate to the left
                if (i == _x.UpperBound) i--;   // extrapolate to the right
                double dx = _u - _x[i];
                return (_y[i] + dx * (_y1[i] + dx * (_y2[i] + dx * _y3[i])));
            }



            /// <summary>
            /// Initial derivatives at boundaries of data set using quadratic Newton interpolation.
            /// </summary>
            private static double deriv1(Vector _x, Vector _y, int _i, int _sgn)
            {
                double di, dis, di2, his;
                int i1, i2;

                i1 = _i + 1;
                i2 = _i - 1;
                his = _x[i1] - _x[i2];
                dis = (_y[i1] - _y[i2]) / his;
                di = (_y[i1] - _y[_i]) / (_x[i1] - _x[_i]);
                di2 = (di - dis) / (_x[_i] - _x[i2]);
                return dis + _sgn * di2 * his;
            }
            /// <summary>
            /// Initial derivatives within data set using quadratic Newton interpolation.
            /// </summary>
            private static double deriv2(Vector _x, Vector _y, int _i)
            {
                double di0, di1, di2, hi0;
                int i1, i2;

                i1 = _i + 1;
                i2 = _i - 1;
                hi0 = _x[_i] - _x[i2];
                di0 = (_y[_i] - _y[i2]) / hi0;
                di1 = (_y[i1] - _y[_i]) / (_x[i1] - _x[_i]);
                di2 = (di1 - di0) / (_x[i1] - _x[i2]);
                return di0 + di2 * hi0;
            }
            /// <summary>
            /// Fritsch-Carlson iteration to adjust the monotone cubic interpolant. The iteration converges with cubic order.
            /// </summary>
            private static void fritsch(Vector _x, Vector _y, Vector _d)
            {
                int i, i1;
                bool stop;
                double d1, r2, t;

                const int max_loop = 20; // should never happen! Note, that currently it
                // can happen when the curve is not strictly 
                // monotone. In future this case should be handled
                // more gracefully without wasting CPU time.
                int loop = 0;

                do
                {
                    stop = true;
                    for (i = _x.LowerBound; i < _x.UpperBound; i++)
                    {
                        i1 = i + 1;
                        d1 = (_y[i1] - _y[i]) / (_x[i1] - _x[i]);
                        if (d1 == 0.0)
                            _d[i] = _d[i1] = 0.0;
                        else
                        {
                            t = _d[i] / d1;
                            r2 = t * t;
                            t = _d[i1] / d1;
                            r2 += t * t;
                            if (r2 > 9.0)
                            {
                                t = 3.0 / Math.Sqrt(r2);
                                _d[i] *= t;
                                _d[i1] *= t;
                                stop = false;
                            }
                        }
                    }
                } while (!stop && ++loop < max_loop);
            }
            /// <summary>
            /// Find index of largest element in the increasingly ordered vector _double, 
            /// which is smaller than u. If u is smaller than the smallest value in 
            /// the vector then the lowest index minus one is returned. 
            /// </summary>
            /// <param name="u">The value to search for.</param>
            /// <param name="_double">Vector of (strictly increasing) _double values.</param>
            /// <returns>The index a so that _double[a]&lt;u&lt;=_double[a+1]. If u is smaller than _double[0] then -1 is returned.</returns>
            /// <remarks>
            /// A fast binary search is performed.
            /// Note, that the vector must be strictly increasing.
            /// </remarks>
            private static int FindInterval(double _u, Vector _x)
            {
                int i, j;
                int lo = _x.LowerBound;
                int hi = _x.UpperBound;
                if (_u < _x[lo])
                {
                    i = lo - 1; // attention: return index below smallest index
                }
                else if (_u >= _x[hi])
                {
                    i = hi; // attention: return highest index
                }
                else
                {
                    i = lo;
                    j = hi;
                    do
                    {
                        int k = (i + j) / 2;
                        if (_u < _x[k]) j = k;
                        if (_u >= _x[k]) i = k;
                    } while (j > i + 1);
                }
                return i;
            }

        }

        private Function ToPolynomialInterpolation(int _totalNumberOfPoints)
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }

            if (_totalNumberOfPoints < 1)
            {
                throw new ArgumentException();
            }


            double _xStepSize = (this.points[this.points.Length - 1].X - this.points[0].X) / (_totalNumberOfPoints - 1);


            double[] _interpolatedXValues = new double[_totalNumberOfPoints];
            for (int a = 0; a < _totalNumberOfPoints; a++)
            {
                _interpolatedXValues[a] = this.points[0].X + a * _xStepSize;
            }


            return this.ToPolynomialInterpolation(_interpolatedXValues);
        }
        private Function ToPolynomialInterpolation(double _resolution)
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }

            if ((_resolution <= 0)
                || (_resolution >= 1))
            {
                throw new ArgumentException();
            }


            List<double> _interpolatedXValues = new List<double>();

            for (int a = 0; a < this.points.Length - 1; a++)
            {
                double _startX = this.points[a].X;
                double _endX = this.points[a + 1].X;
                double _increment = (_endX - _startX) * _resolution;

                for (double _double = _startX; _double <= _endX + (_increment / 2)/*s tem poštukamo pomankljivosti od floating pointa*/; _double += _increment)
                {
                    if (!_interpolatedXValues.Contains(_double))
                    {
                        _interpolatedXValues.Add(_double);
                    }
                }
            }

            return this.ToPolynomialInterpolation(_interpolatedXValues.ToArray());
        }
        private Function ToPolynomialInterpolation(double[] _desiredXValues)
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }


            Vector _x = new Vector(this.points.Length);
            Vector _y = new Vector(this.points.Length);
            for (int a = 0; a < this.points.Length; a++)
            {
                _x[a] = this.points[a].X;
                _y[a] = this.points[a].Y;
            }

            FritschCarlsonCubicSpline _fritschCarlsonCubicSpline = new FritschCarlsonCubicSpline(_x, _y);

            List<XY> _list = new List<XY>();
            for (int a = 0; a < _desiredXValues.Length; a++)
            {
                _list.Add(new XY(
                    _desiredXValues[a],
                    _fritschCarlsonCubicSpline.GetY(_desiredXValues[a])));
            }



            return Function.FromPoints(_list);
        }

        public Function Interpolate(InterpolationMethod _interpolationMethod, int _totalNumberOfPoints)
        {
            switch (_interpolationMethod)
            {
                case InterpolationMethod.Linear:
                    return this.ToLinearInterpolation(_totalNumberOfPoints);

                case InterpolationMethod.Polynomial:
                    return this.ToPolynomialInterpolation(_totalNumberOfPoints);


                default:
                    throw new NotSupportedException();
            }
        }
        public Function Interpolate(InterpolationMethod _interpolationMethod, double _resolution)
        {
            switch (_interpolationMethod)
            {
                case InterpolationMethod.Linear:
                    return this.ToLinearInterpolation(_resolution);

                case InterpolationMethod.Polynomial:
                    return this.ToPolynomialInterpolation(_resolution);


                default:
                    throw new NotSupportedException();
            }
        }
        public Function Interpolate(InterpolationMethod _interpolationMethod, double[] _desiredXValues)
        {
            switch (_interpolationMethod)
            {
                case InterpolationMethod.Linear:
                    return this.ToLinearInterpolation(_desiredXValues);

                case InterpolationMethod.Polynomial:
                    return this.ToPolynomialInterpolation(_desiredXValues);


                default:
                    throw new NotSupportedException();
            }
        }
        #endregion "Interpolation"

        #region "Numerical integration"
        /// <param name="_fX">integrand</param>
        /// <param name="_min">left limit of integration</param>
        /// <param name="_max">right limit of integration</param>
        /// <param name="_maximumFunctionEvaluations">If this value is N, the number of function evaluations will be less than 2^n + 1</param>
        private double IntegrateY(int _maximumFunctionEvaluations)
        {
            double _min = this.GetMinX();
            double _max = this.GetMaxX();

            double _integral = 0d;
            double _mostRecentContribution = 0d;
            double _previousContribution = 0d;
            double _sum = 0d;


            for (int _stage = 0; _stage <= _maximumFunctionEvaluations; _stage++)
            {
                if (_stage == 0)
                {
                    _sum = this.GetFx(_min) + this.GetFx(_max);
                    _integral = _sum * 0.5 * (_max - _min);
                }
                else
                {
                    // Pattern of Simpson's rule coefficients:
                    //
                    // 1               1
                    // 1       4       1
                    // 1   4   2   4   1
                    // 1 4 2 4 2 4 2 4 1
                    // ...
                    //
                    // Each row multiplies new function evaluations by 4, and the evalutations from the previous step by 2.


                    int _numNewPts = 1 << (_stage - 1);
                    _mostRecentContribution = 0.0;
                    double _h = (_max - _min) / _numNewPts;
                    double _x = _min + 0.5 * _h;


                    for (int a = 0; a < _numNewPts; a++)
                    {
                        //Console.WriteLine(_double + a * _h);
                        _mostRecentContribution += this.GetFx(_x + a * _h);
                    }
                    _mostRecentContribution *= 4.0;
                    _sum += _mostRecentContribution - 0.5 * _previousContribution;


                    _integral = _sum * (_max - _min) / ((1 << _stage) * 3.0);


                    _previousContribution = _mostRecentContribution;
                }
            }


            return _integral;
        }

        private double[] GetRequiredXValues(int _maximumFunctionEvaluations)
        {
            double _min = this.GetMinX();
            double _max = this.GetMaxX();

            List<double> _requiredXValues = new List<double>();
            _requiredXValues.Add(_min);
            _requiredXValues.Add(_max);

            for (int _stage = 0; _stage <= _maximumFunctionEvaluations; _stage++)
            {
                {
                    int _numNewPts = 1 << (_stage - 1);
                    double _h = (_max - _min) / _numNewPts;
                    double _x = _min + 0.5 * _h;

                    //dobimo točke
                    for (int a = 0; a < _numNewPts; a++)
                    {
                        double _double = _x + a * _h;

                        //Console.WriteLine(_double);
                        if (!_requiredXValues.Contains(_double))
                        {
                            _requiredXValues.Add(_double);
                        }
                    }
                }
            }

            _requiredXValues.Sort();

            return _requiredXValues.ToArray();
        }



        public double IntegrateY(InterpolationMethod _interpolationMethod, int _functionEvaluations)
        {
            double[] _requiredXValues = this.GetRequiredXValues(_functionEvaluations);
            Function _interpolatedFunction = this.Interpolate(_interpolationMethod, _requiredXValues);

            double _integral = _interpolatedFunction.IntegrateY(_functionEvaluations);

            return _integral;
        }
        #endregion "Numerical integration"

        public Function SubFunction(double _xFrom, double _xTo)
        {
            List<XY> _list = new List<XY>();
            List<double> _addedXValues = new List<double>();

            foreach (XY _xy in this.points)
            {
                if ((_xy.X >= _xFrom)
                    && (_xy.X <= _xTo))
                {
                    if (!_addedXValues.Contains(_xy.X))
                    {
                        _addedXValues.Add(_xy.X);
                        _list.Add(_xy);
                    }
                }
            }

            return Function.FromPoints(_list);
        }


        private XY GetPreviousXY(double _x)
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }


            XY _previousXY = XY.Empty;

            foreach (XY _xy in points)
            {
                if (_xy.X > _x)
                {
                    break;
                }
                else
                {
                    if (_xy.X != _x)
                    {
                        _previousXY = _xy;
                    }
                }
            }

            return _previousXY;
        }
        private XY GetNextXY(double _x)
        {
            if (!this.sortedByX)
            {
                throw new Exception("Cannot operate with unsorted functions.");
            }


            XY _nextXY = XY.Empty;

            foreach (XY _xy in points)
            {
                if (_xy.X > _x)
                {
                    _nextXY = _xy;
                    break;
                }
            }

            return _nextXY;
        }


        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.points.GetEnumerator();
        }
        #endregion IEnumerable Members

    }


    public enum InterpolationMethod
    {
        Linear,
        Polynomial
    }

}