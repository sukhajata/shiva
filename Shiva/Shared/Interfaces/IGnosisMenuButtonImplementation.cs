using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisMenuButtonImplementation : IGnosisMouseVisibleControlImplementation
    {
        //properties
        string GnosisIcon { get; set; }
        string Shortcut { get; set; }
        bool Disabled { get; set; }

        void AddMenuItem(IGnosisMenuItemImplementation item);
        void AddToggleButton(GnosisToggleButton toggleButton);
        void AddButton(GnosisButton button);
        void AddMenuButton(GnosisMenuButton menuButton);
    }
}
