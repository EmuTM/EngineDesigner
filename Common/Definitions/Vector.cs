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
    /// Represents a set of real value points.
    /// </summary>
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    public class Vector
    {
        private double[] points;



        public Vector()
            : this(0)
        {
        }
        public Vector(int _length)
        {
            if (_length < 0)
            {
                throw new ArgumentException("Length must be zero or positive.");
            }


            this.points = new double[_length];
        }



        public double this[int _index]
        {
            get
            {
                return this.points[_index];
            }
            set
            {
                this.points[_index] = value;
            }
        }



        public int Length
        {
            get
            {
                return this.points.Length;
            }
        }
        public int LowerBound
        {
            get
            {
                return 0;
            }
        }
        public int UpperBound
        {
            get
            {
                return this.points.Length - 1;
            }
        }



        /// <summary>
        /// Resizes the vector. The previous element data are lost.
        /// </summary>
        /// <param name="_lo">New lower boundary of the vector (first valid index).</param>
        /// <param name="_hi">New upper boundary of the vector (last valid index).</param>
        public void Resize(int _lo, int _hi)
        {
            if (_lo != 0)
            {
                throw new NotSupportedException("Lower bound value other than zero is not supported by DoubleVector class");
            }


            if ((this.points == null)
                || ((_hi - _lo) >= this.points.Length))
            {
                this.points = new double[_hi - _lo + 1];
            }
        }
        ///<summary>Set all values in the <c>DoubleVector</c> to zero </summary>
        public void Clear()
        {
            for (int a = 0; a < this.points.Length; a++)
            {
                this.points[a] = 0;
            }
        }

    }
}
