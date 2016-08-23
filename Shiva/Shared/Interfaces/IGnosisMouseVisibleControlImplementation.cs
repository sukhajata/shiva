using ShivaShared3.BaseControllers;
using System;
using System.Collections.Generic;
using System.Text;


namespace ShivaShared3.Interfaces
{
    public interface IGnosisMouseVisibleControlImplementation : IGnosisVisibleControlImplementation
    {
        bool HasMouseFocus { get; set; }
        bool HasMouseDown { get; set; }

      
    }
}
