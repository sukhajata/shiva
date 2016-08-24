using Shiva.Shared.ContainerControllers;
using System;
using System.Collections.Generic;
using System.Text;
using GnosisControls;
using Shiva.Shared.Interfaces;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Data;
using Shiva.Shared.BaseControllers;

namespace Shiva.Shared.ToolbarControllers
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
