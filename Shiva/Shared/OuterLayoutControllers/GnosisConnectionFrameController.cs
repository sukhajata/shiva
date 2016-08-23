using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;
using System;
using ShivaShared3.ContainerControllers;
using GnosisControls;
using ShivaShared3.BaseControllers;


namespace ShivaShared3.OuterLayoutControllers
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
