using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

using System.ComponentModel;
using System.Globalization;
using EngineDesigner.Common.CustomCollections;

namespace EngineDesigner.Common.Definitions
{
    /// <summary>
    /// Represents a real value point in the coordinate system.
    /// </summary>
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    public struct XY
    {
        /// <param name="_double">The _double dot of the point.</param>
        /// <param name="_y">The y dot of the point.</param>
        public XY(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public static implicit operator Point(XY _xy)
        {
            return new Point((int)_xy.X, (int)_xy.Y);
        }
        public static XY operator +(XY _a)
        {
            return new XY(+_a.X, +_a.Y);
        }
        public static XY operator +(XY _a, XY _b)
        {
            return new XY(_a.X + _b.X, _a.Y + _b.Y);
        }
        public static XY operator -(XY _a)
        {
            return new XY(-_a.X, -_a.Y);
        }
        public static XY operator -(XY _a, XY _b)
        {
            return new XY(_a.X - _b.X, _a.Y - _b.Y);
        }
        public static XY operator *(XY _a, double _b)
        {
            return new XY((_a.X * _b), (_a.Y * _b));
        }
        public static bool operator ==(XY _a, XY _b)
        {
            return _a.X == _b.X && _a.Y == _b.Y;
        }
        public static bool operator !=(XY _a, XY _b)
        {
            return !(_a == _b);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format(
                "{0}; {1}",
                x.ToString(),
                y.ToString());
        }



        [DataMember]
        private double x;
        /// <summary>
        /// Defines the _double dot of the point.
        /// </summary>
        public double X
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return x; }
            [System.Diagnostics.DebuggerStepThrough()]
            set { x = value; }
        }

        [DataMember]
        private double y;
        /// <summary>
        /// Defines the y dot of the point.
        /// </summary>
        public double Y
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get { return y; }
            [System.Diagnostics.DebuggerStepThrough()]
            set { y = value; }
        }



        /// <summary>
        /// Gets an empty (0; 0) XY-struct point.
        /// </summary>
        public static XY Empty
        {
            get
            {
                return new XY(double.NaN, double.NaN);
            }
        }
        /// <summary>
        /// Gets a XY-struct point from a Point-struct point.
        /// </summary>
        /// <param name="_point">The point as the Point-struct.</param>
        public static XY FromPoint(Point _point)
        {
            return XY.FromPoint(_point.X, _point.Y);
        }
        /// <summary>
        /// Gets a XY-struct point from dots.
        /// </summary>
        /// <param name="_double">The _double dot of the point.</param>
        /// <param name="_y">The y dot of the point.</param>
        public static XY FromPoint(int _x, int _y)
        {
            return new XY((double)_x, (double)_y);
        }



        /// <summary>
        /// Gets the magnitude of the point.
        /// </summary>
        public double GetMagnitude()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
        /// <summary>
        /// Normalizes the point.
        /// </summary>
        public void Normalize()
        {
            double _magnitude = GetMagnitude();

            x = x / _magnitude;
            y = y / _magnitude;
        }
        /// <summary>
        /// Multiplicates the dots of the point with the given one.
        /// </summary>
        /// <param name="_xy">The point to use for multiplication.</param>
        public double GetDotProduct(XY _xy)
        {
            return (x * _xy.X) + (y * _xy.Y);
        }
        /// <summary>
        /// Gets the distance from the given point.
        /// </summary>
        /// <param name="_xy">The point to use for distance computing.</param>
        public double GetDistanceTo(XY _xy)
        {
            return Math.Sqrt(Math.Pow(_xy.X - x, 2) + Math.Pow(_xy.Y - y, 2));
        }


        public bool IsEmpty()
        {
            if (double.IsNaN(this.x)
                || double.IsNaN(this.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static XY[] SortByX(XY[] _xyArray)
        {
            List<XY> _sorted = new List<XY>();


            int _index = -1;
            do
            {
                _index = -1;
                double _double = double.MaxValue;
                for (int a = 0; a < _xyArray.Length; a++)
                {
                    if (!_xyArray[a].IsEmpty())
                    {
                        if (_xyArray[a].X < _double)
                        {
                            _double = _xyArray[a].X;
                            _index = a;
                        }
                    }
                }

                if (_index > -1)
                {
                    _sorted.Add(_xyArray[_index]);
                    _xyArray[_index] = XY.Empty;
                }
            }
            while (_index > -1);


            return _sorted.ToArray();
        }
        public static XY[] SortByY(XY[] _xyArray)
        {
            List<XY> _sorted = new List<XY>();


            int _index = -1;
            do
            {
                _index = -1;
                double _double = double.MaxValue;
                for (int a = 0; a < _xyArray.Length; a++)
                {
                    if (!_xyArray[a].IsEmpty())
                    {
                        if (_xyArray[a].Y < _double)
                        {
                            _double = _xyArray[a].Y;
                            _index = a;
                        }
                    }
                }

                if (_index > -1)
                {
                    _sorted.Add(_xyArray[_index]);
                    _xyArray[_index] = XY.Empty;
                }
            }
            while (_index > -1);


            return _sorted.ToArray();
        }

        public static bool ContainsX(IEnumerable<XY> _xys, double _x)
        {
            foreach (XY _xy in _xys)
            {
                if (_xy.x == _x)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool ContainsY(IEnumerable<XY> _xys, double _y)
        {
            foreach (XY _xy in _xys)
            {
                if (_xy.y == _y)
                {
                    return true;
                }
            }

            return false;
        }

        public static double[] ToXValues(IEnumerable<XY> _xys)
        {
            List<double> _list = new List<double>();

            foreach (XY _xy in _xys)
            {
                _list.Add(_xy.x);
            }

            return _list.ToArray();
        }
        public static double[] ToYValues(IEnumerable<XY> _xys)
        {
            List<double> _list = new List<double>();

            foreach (XY _xy in _xys)
            {
                _list.Add(_xy.y);
            }

            return _list.ToArray();
        }

    }

}
