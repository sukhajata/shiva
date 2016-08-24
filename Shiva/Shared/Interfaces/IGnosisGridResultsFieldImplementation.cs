using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisResultsFieldImplementation : IGnosisGridFieldImplementation
    {
        //properties
       // bool HasRowFocus { get; set; }
        bool HasRowMouseFocus { get; set; }
        bool HasRowMouseDown { get; set; }

        //void SetMouseDownHandler(Action action);
        //void SetMouseUpHandler(Action action);
        //void SetGotMouseFocusHandler(Action action);
        //void SetLostMouseFocusHandler(Action action);
    }
}
