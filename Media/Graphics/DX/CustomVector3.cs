using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SlimDX;
using System.Globalization;

using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace EngineDesigner.Media.Graphics.DX
{
    /// <summary>
    /// Provides extended (GUI) functionality, based on SlimDX.Vector3 class.
    /// </summary>
    [TypeConverter(typeof(CustomVector3Converter))]
    public class CustomVector3
    {
        public CustomVector3()
        {
            vector3 = new Vector3();
        }
        public CustomVector3(float _x, float _y, float _z)
        {
            vector3 = new Vector3(_x, _y, _z);
        }
        public static CustomVector3 operator +(CustomVector3 _a, CustomVector3 _b)
        {
            return CustomVector3.From(_a.ToVector3() + _b.ToVector3());
        }
        public static CustomVector3 operator -(CustomVector3 _a, CustomVector3 _b)
        {
            return CustomVector3.From(_a.ToVector3() - _b.ToVector3());
        }
        public static CustomVector3 operator *(CustomVector3 _a, float _b)
        {
            return CustomVector3.From(_a.ToVector3() * _b);
        }
        public static bool operator ==(CustomVector3 _a, CustomVector3 _b)
        {
            return _a.X == _b.X && _a.Y == _b.Y;
        }
        public static bool operator !=(CustomVector3 _a, CustomVector3 _b)
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
                "{0}; {1}; {2}",
                this.X.ToString(),
                this.Y.ToString(),
                this.Z.ToString());
        }



        public static CustomVector3 Zero
        {
            get { return CustomVector3.From(Vector3.Zero); }
        }
        public static CustomVector3 From(Vector3 _vector3)
        {
            return new CustomVector3(_vector3.X, _vector3.Y, _vector3.Z);
        }



        private Vector3 vector3;



        public float X
        {
            get { return vector3.X; }
            set { vector3.X = value; }
        }
        public float Y
        {
            get { return vector3.Y; }
            set { vector3.Y = value; }
        }
        public float Z
        {
            get { return vector3.Z; }
            set { vector3.Z = value; }
        }



        public Vector3 ToVector3()
        {
            return this.vector3;
        }
    }


    /// <summary>
    /// Provides UI integration for CustomVector3 class.
    /// </summary>
    public class CustomVector3Converter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //OBSOLETE:
            //try
            {
                string[] _values = value.ToString().Split(';');

                if (_values.Length == 1)
                {
                    if (_values[0] == "Zero")
                    {
                        return CustomVector3.Zero;
                    }
                }
                else if (_values.Length == 3)
                {
                    return new CustomVector3(
                        float.Parse(_values[0]),
                        float.Parse(_values[1]),
                        float.Parse(_values[2]));
                }

                throw new ArgumentException(
                    string.Format(
                        "'{0}' is not a valid value for CustomVector3.",
                        value.ToString()));
            }
            //OBSOLETE:
            //catch { }

            //OBSOLETE:
            //return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }

            // Always call the base to see if it can perform the conversion.
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                ConstructorInfo _constructorInfo = typeof(CustomVector3).GetConstructor(new Type[] { 
                    typeof(float), typeof(float), typeof(float) });

                CustomVector3 _customVector3 = (CustomVector3)value;

                return new InstanceDescriptor(_constructorInfo, new object[] { _customVector3.X, _customVector3.Y, _customVector3.Z });
            }

            // Always call base, even if you can't convert.
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }

}
