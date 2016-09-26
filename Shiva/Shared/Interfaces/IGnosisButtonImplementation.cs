using System;
using System.Collections.Generic;
using System.Text;
using static Shiva.Shared.BaseControllers.GnosisController;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisButtonImplementation : IGnosisPanelFieldImplementation, IGnosisIconPossessor
    {
        //properties
        bool Disabled { get; set; }
        //string GnosisIcon { get; set; }
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
