using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisTileTabItemImplementation : IGnosisContainerImplementation
    {
        void LoadFrame(IGnosisFrameImplementation frameImplementation, IGnosisToggleButtonImplementation headerButton);
       // void LoadFrame(GnosisFrame frame, GnosisTabHeaderButton tabHeaderButton, GnosisButton closeButton);
        void RemoveFrame(IGnosisFrameImplementation ControlImplementation);
       // void SetCaption(string caption);
        void SetLoadedHandler(Action<double> action);
       // void SetHeader(IGnosisToggleButtonImplementation headerButton);
        void DisplayLoadingAnimation(string barColour);
        void HideLoadingAnimation();
       // void SetHeader(IGnosisToggleButtonImplementation headerButton);
        void SetCloseHandler(Action action);
        void SetHeaderButton(IGnosisToggleButtonImplementation headerButton);
    }
}
