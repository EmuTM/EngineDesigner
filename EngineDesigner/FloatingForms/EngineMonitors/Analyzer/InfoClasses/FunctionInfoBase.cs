using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using EngineDesigner.Machine;
using System.Drawing;
using System.Runtime.Serialization;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    internal class FunctionInfoBase
    {
        protected FunctionInfoBase(string _name)
        {
            this.name = _name;
        }
        public override bool Equals(object obj)
        {
            if (obj is FunctionInfoBase)
            {
                FunctionInfoBase _functionInfoBase = (FunctionInfoBase)obj;

                if (this.name == _functionInfoBase.name)
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



        [DataMember]
        private string name;
        public string Name
        {
            get { return name; }
        }

        [DataMember]
        private Color color;
        public Color @Color
        {
            get { return color; }
            set { color = value; }
        }

        [DataMember]
        private ChartAreaInfo chartArea = null;
        public ChartAreaInfo ChartArea
        {
            get { return chartArea; }
            set { chartArea = value; }
        }

        [DataMember]
        private bool convertYToPercents = false;
        public bool ConvertYToPercents
        {
            get { return convertYToPercents; }
            set { convertYToPercents = value; }
        }

        [DataMember]
        private bool minIsNegative100 = false;
        public bool MinIsNegative100
        {
            get { return minIsNegative100; }
            set { minIsNegative100 = value; }
        }



        public virtual string ToLongString()
        {
            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.Append(this.Name);

            if (this.convertYToPercents)
            {
                if (this.minIsNegative100)
                {
                    _stringBuilder.Append("; +/- %");
                }
                else
                {
                    _stringBuilder.Append("; + %");
                }
            }

            return _stringBuilder.ToString();
        }

    }

}
