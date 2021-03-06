﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using EngineDesigner.Machine;
using System.Runtime.Serialization;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    internal class FunctionInfoSuperposition : FunctionInfoBase
    {
        public FunctionInfoSuperposition(string _name, params FunctionInfoBase[] _functions)
            : base(_name)
        {
            if (_functions.Length < 2)
            {
                throw new Exception();
            }

            this.functions = _functions;
        }


        [DataMember]
        private FunctionInfoBase[] functions;
        public FunctionInfoBase[] Functions
        {
            get { return functions; }
        }


        public static FunctionInfoSuperposition[] GetAvailableFunctions()
        {
            return null;
        }

    }

}
