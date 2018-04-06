using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using System.Drawing;
using EngineDesigner.Machine;
using System.Runtime.Serialization;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    internal class InterpolationMethodInfo
    {
        private InterpolationMethodInfo(string _name, InterpolationMethod _interpolationMethod)
        {
            this.name = _name;
            this.interpolationMethod = _interpolationMethod;
        }
        public override bool Equals(object obj)
        {
            if (obj is InterpolationMethodInfo)
            {
                InterpolationMethodInfo _interpolationMethodInfo = (InterpolationMethodInfo)obj;

                if (this.name == _interpolationMethodInfo.name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return this.name;
        }



        public static InterpolationMethodInfo Polynomially
        {
            get
            {
                return new InterpolationMethodInfo("Polynomially", InterpolationMethod.Polynomial);
            }
        }
        public static InterpolationMethodInfo Linearly
        {
            get
            {
                return new InterpolationMethodInfo("Linearly", InterpolationMethod.Linear);
            }
        }



        [DataMember]
        private string name;
        public string Name
        {
            get { return name; }
        }

        [DataMember]
        private InterpolationMethod interpolationMethod;
        public InterpolationMethod @InterpolationMethod
        {
            get { return interpolationMethod; }
        }



        public static InterpolationMethodInfo[] GetAvailableInterpolationMethods()
        {
            PropertyInfo[] _propertyInfos = typeof(InterpolationMethodInfo).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);

            List<InterpolationMethodInfo> _interpolationMethodsInfos = new List<InterpolationMethodInfo>();
            foreach (PropertyInfo _propertyInfo in _propertyInfos)
            {
                _interpolationMethodsInfos.Add((InterpolationMethodInfo)_propertyInfo.GetValue(null, null));
            }

            return _interpolationMethodsInfos.ToArray();
        }

    }

}
