using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    internal class FunctionInfoReference : FunctionInfoBase
    {
        protected FunctionInfoReference(string _name)
            : base(_name)
        {
        }



        [DataMember]
        private string expression = null;
        public string Expression
        {
            get { return expression; }
        }

        [DataMember]
        private static List<string> previousExpressions = new List<string>()
        {
            "sin (x)",
            "cos (x)",
            "sin (x) * (-1)",
            "cos (x) * (-1)"
        };
        public static string[] PreviousExpressions
        {
            get { return FunctionInfoReference.previousExpressions.ToArray(); }
        }


        public static FunctionInfoReference CreateNew(string _expression)
        {
            FunctionInfoReference _functionInfoReference = new FunctionInfoReference(_expression);
            _functionInfoReference.expression = _expression;

            FunctionInfoReference.previousExpressions.Add(_expression);

            return _functionInfoReference;
        }


        public static FunctionInfoReference[] GetAvailableFunctions()
        {
            List<FunctionInfoReference> _functionInfos = new List<FunctionInfoReference>();

            foreach (string _expression in FunctionInfoReference.previousExpressions)
            {
                FunctionInfoReference _functionInfoReference = new FunctionInfoReference(_expression);
                _functionInfoReference.expression = _expression;
                _functionInfos.Add(_functionInfoReference);
            }

            return _functionInfos.ToArray();
        }

    }

}
