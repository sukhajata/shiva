using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using System;
using Shiva.Shared.ContainerControllers;
using GnosisControls;
using Shiva.Shared.BaseControllers;


namespace Shiva.Shared.OuterLayoutControllers
{
    public class GnosisConnectionFrameController : GnosisFrameController
    {
        public GnosisConnectionFrameController(
            GnosisConnectionFrame connectionFrame,
           // IGnosisConnectionFrameImplementation connectionFrameImplementation,
            GnosisInstanceController instanceController,
            GnosisContainerController parent)
            :base (connectionFrame, instanceController, parent)
        {

        }


    }
}
