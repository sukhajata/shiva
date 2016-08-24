using Shiva.Shared.BaseControllers;
using System;
using System.Collections.Generic;
using System.Text;


namespace Shiva.Shared.Interfaces
{
    public interface IGnosisMouseVisibleControlImplementation : IGnosisVisibleControlImplementation
    {
        bool HasMouseFocus { get; set; }
        bool HasMouseDown { get; set; }

      
    }
}
