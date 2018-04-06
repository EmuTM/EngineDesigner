using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EngineDesigner.FloatingForms;
using EngineDesigner.FloatingForms.EngineMonitors;
using EngineDesigner.FloatingForms.EngineMonitors.Analyzer;
using EngineDesigner.Machine;
using System.Windows.Forms.DataVisualization.Charting;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;

namespace EngineDesigner.TestConsoles
{
    internal static class TestConsole_MathParser
    {
        public static void Start()
        {
            string _expression = "2 log(3)";

            try
            {
                Mathematics.MathParser _mathParser = new Mathematics.MathParser();
                double _output = _mathParser.Compute(_expression);
            }
            catch (Exception _ex)
            {
            }


        }

    }

}
