using System;
using System.Collections.Generic;
using System.Text;
using static ShivaShared3.BaseControllers.GnosisController;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisButtonImplementation : IGnosisPanelFieldImplementation
    {
        //properties
        bool Disabled { get; set; }
        string GnosisIcon { get; set; }
        string Shortcut { get; set; }
        string MenuTag { get; set; }
        int MaxChars { get; set; }
        int ColSpan { get; set; }


        //methods
        //void SetClickHandler(Action clickHandler);
        // void SetCaption(string caption);
        // void SetIcon(string icon, bool enabled);

    }
}
