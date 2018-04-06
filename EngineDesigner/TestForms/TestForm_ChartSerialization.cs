using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EngineDesigner.FloatingForms.EngineMonitors.Analyzer;
using System.Runtime.Serialization;
using EngineDesigner.Common.Serialization;

namespace EngineDesigner.TestForms
{
    internal partial class TestForm_ChartSerialization : Form
    {
        public TestForm_ChartSerialization()
        {
            InitializeComponent();
        }

        private void TestForm_ChartSerialization_Load(object sender, EventArgs e)
        {
            TestClass _testClas = new TestClass();
            _testClas.Save(@"D:\Temp\test.xml");
            _testClas = TestClass.From(@"D:\Temp\test.xml");

        }


        [DataContract]
        private class TestClass : Serializable<TestClass>
        {
            [DataMember]
            public ChartAreaInfo chartAreaInfo = new ChartAreaInfo(
                new System.Windows.Forms.DataVisualization.Charting.ChartArea(),
                new System.Windows.Forms.DataVisualization.Charting.ChartArea(),
                new System.Windows.Forms.DataVisualization.Charting.ChartArea());

            [DataMember]
            public InterpolationMethodInfo interpolationMethodInfo = InterpolationMethodInfo.Polynomially;

            [DataMember]
            public HarmonicOrderInfo harmonicOrderInfo = HarmonicOrderInfo.Full;

            [DataMember]
            public FunctionInfoTorque functionInfoTorque = FunctionInfoTorque.CylinderTotalTorque_Nm;

            //[DataMember]
            //public FunctionInfoReference functionInfoReference = FunctionInfoReference.Sine;

            [DataMember]
            public FunctionInfoKinematic functionInfoKinematics = FunctionInfoKinematic.PistonVelocity_mps;

            [DataMember]
            public FunctionInfoForce functionInfoForce = FunctionInfoForce.CylinderGasPressureForceAxial_N;


            //[DataMember]
            //public FunctionInfoSuperposition functionInfoSuperposition = new FunctionInfoSuperposition("car", FunctionInfoReference.Sine, FunctionInfoReference.Cosine);


            protected override Type[] GetKnownTypes()
            {
                return new Type[]
                {
                    typeof(System.Drawing.FontStyle),
                    typeof(System.Drawing.GraphicsUnit),

                    typeof(FunctionInfoBase),
                    typeof(FunctionInfoReference),
                    typeof(FunctionInfoKinematic),
                    typeof(FunctionInfoForce),
                    typeof(FunctionInfoTorque),
                    typeof(FunctionInfoSuperposition),

                };
            }

        }


    }
}
