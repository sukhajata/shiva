using ShivaShared3.ContainerControllers;
using System;
using System.Collections.Generic;
using System.Text;
using GnosisControls;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.Data;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.ToolbarControllers
{
    public class GnosisSystemAddressController : GnosisVisibleController
    {
        
        public GnosisSystemAddressController(
            GnosisSystemAddressField systemAddressField,
           // IGnosisSystemAddressFieldImplementation _systemAddressFieldImplementation,
            GnosisEntityController entityController,
            GnosisToolbarController parent)
            :base (systemAddressField,  entityController, parent)
        {
            systemAddressField.SetAddress(GlobalData.Singleton.SystemController.SystemURL);
           
        }

    }
}
