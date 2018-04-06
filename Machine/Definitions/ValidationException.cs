using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineDesigner.Machine.Definitions
{
    public class ValidationException : Exception
    {
        public ValidationException(string _message)
            : base(_message)
        {
        }

    }
}
