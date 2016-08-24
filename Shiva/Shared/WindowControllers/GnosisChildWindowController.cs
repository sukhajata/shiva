using System;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.BaseControllers;


namespace Shiva.Shared.WindowControllers
{
    public class GnosisChildWindowController : GnosisVisibleController
    {
        public GnosisChildWindowController(
            GnosisChildWindow childWindow, 
           // IGnosisChildWindowImplementation childWindowImplementation,
            GnosisEntityController entityController,
            GnosisParentWindowController parent)
            :base(childWindow,  entityController, parent)
        {

        }
    }
}
