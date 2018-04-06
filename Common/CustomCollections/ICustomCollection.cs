using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineDesigner.Common.CustomCollections
{
    public interface ICustomCollection : IEnumerable
    {
        bool AllowDuplicates
        {
            get;
        }

    }
}
