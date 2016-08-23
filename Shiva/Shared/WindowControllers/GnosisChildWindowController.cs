using System;
using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.BaseControllers;


namespace ShivaShared3.WindowControllers
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
