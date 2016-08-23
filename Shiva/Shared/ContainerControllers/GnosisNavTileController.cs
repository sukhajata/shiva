using System;
using System.Collections.Generic;
using System.Text;

using GnosisControls;
using ShivaShared3.Data;
using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;

namespace ShivaShared3.ContainerControllers
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
