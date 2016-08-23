using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisVisibleControlImplementation : IGnosisControl
    {
        //properties
        string Caption { get; set; }
        bool HasFocus { get; set; }
        bool Hidden { get; set; }
        string Tooltip { get; set; }
        IGnosisVisibleControlImplementation GnosisParent { get; set; }

        //methods
        //void GnosisAddChild(IGnosisObject child);
        //double GetPaddingHorizontal();
       // void SetPaddingHorizontal(double paddingHorizontal);
        //void SetPaddingVertical(double paddingVertical);
        // void SetBorderColour(string borderColour);
        //void SetBackgroundColour(string backgroundColour);
        // void SetOutlineColour(string outlineColour);
        // void RemoveOutlineColour();
        //void SetMouseDownHandler(Action action);
        //void SetMouseUpHandler(Action action);
        //void SetGotMouseFocusHandler(Action action);
        //void SetLostMouseFocusHandler(Action action);
        // void SetTooltip(string toolTip);
        void SetTooltipVisible(bool visible);
        //void SetController(GnosisVisibleController gnosisVisibleController);
        void SetGotFocusHandler(Action action);
        void SetLostFocusHandler(Action action);
        //void GnosisAddChild(IGnosisVisibleControlImplementation child);
    }
}
