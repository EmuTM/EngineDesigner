using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using System.Drawing;
using EngineDesigner.Machine;
using System.Runtime.Serialization;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    internal class HarmonicOrderInfo
    {
        private HarmonicOrderInfo(string _name, uint[] _harmonicOrders)
        {
            this.name = _name;
            this.harmonicOrders = _harmonicOrders;
        }
        public override bool Equals(object obj)
        {
            if (obj is HarmonicOrderInfo)
            {
                HarmonicOrderInfo _harmonicOrderInfo = (HarmonicOrderInfo)obj;

                if (this.name == _harmonicOrderInfo.name)
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



        public static HarmonicOrderInfo Full
        {
            get
            {
                return new HarmonicOrderInfo(
                    "Full",
                    null);
            }
        }
        public static HarmonicOrderInfo FirstApproximation
        {
            get
            {
                return new HarmonicOrderInfo(
                    "First approximation",
                    new uint[] { 1 });
            }
        }
        public static HarmonicOrderInfo SecondApproximation
        {
            get
            {
                return new HarmonicOrderInfo(
                    "Second approximation",
                    new uint[] { 2 });
            }
        }
        public static HarmonicOrderInfo ThirdApproximation
        {
            get
            {
                return new HarmonicOrderInfo(
                    "Third approximation",
                    new uint[] { 3 });
            }
        }



        [DataMember]
        private string name;
        public string Name
        {
            get { return name; }
        }

        [DataMember]
        private uint[] harmonicOrders;
        public uint[] @HarmonicOrders
        {
            get { return harmonicOrders; }
        }



        public static HarmonicOrderInfo[] GetAvailableHarmonicOrders()
        {
            PropertyInfo[] _propertyInfos = typeof(HarmonicOrderInfo).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);

            List<HarmonicOrderInfo> _harmonicOrderInfos = new List<HarmonicOrderInfo>();
            foreach (PropertyInfo _propertyInfo in _propertyInfos)
            {
                _harmonicOrderInfos.Add((HarmonicOrderInfo)_propertyInfo.GetValue(null, null));
            }

            return _harmonicOrderInfos.ToArray();
        }

    }
}
