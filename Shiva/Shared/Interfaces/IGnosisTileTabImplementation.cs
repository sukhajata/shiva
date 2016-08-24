using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisTileTabImplementation : IGnosisContainerImplementation
    {
        GnosisTileTabItem CurrentTileTabItem { get; set; }

        void LoadTabItem(IGnosisTileTabItemImplementation tabItemImplementation);
        void RemoveTabItem(IGnosisTileTabItemImplementation tabItemImplementation);
        void LoadNewTabItem(IGnosisTileTabItemImplementation tabItemImplementation);
        void LoadDummyTabItem(IGnosisTileTabItemImplementation tabItemImp);
        void SetCloseTabItemHandler(Action<IGnosisTileTabItemImplementation> action);
    }
}
