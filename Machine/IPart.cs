using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace EngineDesigner.Machine
{
    public interface IPart
    {
        event IPartDelegate Validated;


        [Browsable(false)]
        Guid @Guid
        {
            get;
        }


        [Browsable(false)]
        double Width
        {
            get;
        }

        [Browsable(false)]
        double Height
        {
            get;
        }

        [Browsable(false)]
        double Length
        {
            get;
        }

        [Browsable(false)]
        double Bound_X_Min
        {
            get;
        }

        [Browsable(false)]
        double Bound_Y_Min
        {
            get;
        }

        [Browsable(false)]
        double Bound_Z_Min
        {
            get;
        }

        [Browsable(false)]
        double Bound_X_Max
        {
            get;
        }

        [Browsable(false)]
        double Bound_Y_Max
        {
            get;
        }

        [Browsable(false)]
        double Bound_Z_Max
        {
            get;
        }


        void Validate();

    }

    public delegate void IPartDelegate(IPart _iPart);


}
