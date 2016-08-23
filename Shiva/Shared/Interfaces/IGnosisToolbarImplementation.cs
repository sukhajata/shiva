using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisToolbarImplementation : IGnosisVisibleControlImplementation
    {
        //properties

        //methods
        void AddMenuButton(GnosisMenuButton menuButton);
        void AddToolbarButton(GnosisButton btnImplementation);
        void AddToggleButton(GnosisToggleButton toggleButton);
        void AddSystemAddressField(GnosisSystemAddressField _systemAddressField);
    }
}
