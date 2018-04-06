using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineDesigner.Common.CustomCollections
{
    /// <summary>
    /// Provides master-properties UI integration for elements used in custom collections
    /// </summary>
    public interface ICustomCollectionElement
    {
        /// <summary>
        /// Defines the text to be displayed as PropertyName
        /// </summary>
        string DisplayName
        {
            get;
        }

        /// <summary>
        /// Defines the text to be displayed as PropertyDescription
        /// </summary>
        string DisplayDescription
        {
            get;
        }

    }

}
