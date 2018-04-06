using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Common.Definitions;
using System.Threading;

namespace EngineDesigner.TestForms
{
    internal partial class TestForm_AsyncFunctionComputing : Form
    {
        public TestForm_AsyncFunctionComputing()
        {
            InitializeComponent();
        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            DateTime _start = DateTime.Now;
            Function _function1 = Common.Utility.ComputeInThreadPool(0, 200, 1, new Func<double, double>(this.Logika1));
            Function _function2 = Common.Utility.ComputeInThreadPool<string>(0, 200, 1, new Func<double, string, double>(this.Logika2), "Cigo");
            double _duration = (DateTime.Now - _start).TotalMilliseconds;
        }
        private double Logika1(double _x)
        {
            System.Diagnostics.Debug.WriteLine("STARTED: " + _x);

            Thread.Sleep(500);

            System.Diagnostics.Debug.WriteLine("FINISHED: " + _x);

            return -_x;
        }
        private double Logika2(double _x, string _string)
        {
            System.Diagnostics.Debug.WriteLine("STARTED: " + _x + " " + _string);

            Thread.Sleep(500);

            System.Diagnostics.Debug.WriteLine("FINISHED: " + _x + " " + _string);

            return -_x;
        }
    }



}
