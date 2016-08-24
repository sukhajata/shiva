using System;
using System.Collections.Generic;
using System.Text;

using GnosisControls;
using Shiva.Shared.Data;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;

namespace Shiva.Shared.ContainerControllers
{
    public class GnosisNavTileController : GnosisTileController
    {
        public int Width
        {
            get { return GlobalData.Singleton.PrimarySplitController.SplitUnits; }
        }
        public GnosisNavTileController(
            GnosisNavigatorTile _navTile, 
          //  IGnosisNavTileImplementation _navTileImplementation,
            GnosisEntityController entityController,
            GnosisPrimarySplitController _primarySplitController)
            :base (_navTile, entityController, _primarySplitController)
        {

        }
    }
}
