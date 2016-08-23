using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisToolbarTrayImplementation : IGnosisVisibleControlImplementation, IContainerPaddingPossessor
    {
        //properties
        string TrayHorizontalAlignment { get; set; }
        string GnosisOrientation { get; set; }
        int MenuSystemID { get; set; }
        int MenuControlID { get; set; }
       // bool Disabled { get; set; }

        //methods
       // void AddToolbar(IGnosisToolbarImplementation toolbarImplementation);
        void AddSplitter(string splitterColour);
        void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType trayHorizontalAlignment);
    }
}
