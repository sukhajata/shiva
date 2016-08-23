using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisTileImplemenation : IGnosisContainerImplementation
    {
        //properties
        bool HasTabs { get; set; }
        bool AcceptsSearchFrames { get; set; }
        bool AcceptsDocumentFrames { get; set; }

        //methods
        void LoadFrameImplementation(IGnosisFrameImplementation frameImplementation);
        void ShowVisibleTileOrder(int visibleOrder);
        void HideVisibleTileOrder();
        void LoadTabImplementation(IGnosisTileTabImplementation tabImplementation);
        void RemoveFrameImplementation(IGnosisFrameImplementation frameImplementation);
        void SetLoadedHandler(Action<double> loadedHandler);
        void DisplayLoadingAnimation(string barColour);
        void HideLoadingAnimation();
    }
}
