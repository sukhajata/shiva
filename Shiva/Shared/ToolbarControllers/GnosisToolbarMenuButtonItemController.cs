using ShivaShared3.ContentControllers;
using ShivaShared3.DataControllers;
using ShivaShared3.Generic;
using ShivaShared3.GenericControllers;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.ToolbarControllers
{
    public class GnosisToolbarButtonItemController 
    {
        private GnosisGenericMenuItemController genericMenuItemController;
        private IGnosisToolbarMenuButtonItemImplementation toolbarButtonItemImplementation;

        public GnosisToolbarButtonItemController(
            GnosisGenericMenuItemController _genericMenuItemController,
            IGnosisToolbarMenuButtonItemImplementation _buttonItemImplementation)
        {
            genericMenuItemController = _genericMenuItemController;
            toolbarButtonItemImplementation = _buttonItemImplementation;
        }

        public void Setup()
        {
            if (genericMenuItemController.Icon != null && genericMenuItemController.Icon.Length > 0)
            {
                string iconFileName = IconNameMapper.GetIconFileName(genericMenuItemController.Icon);
                if (iconFileName != null)
                {
                    toolbarButtonItemImplementation.SetIcon(iconFileName, !genericMenuItemController.Disabled);
                }
                else
                {
                    toolbarButtonItemImplementation.SetCaption(genericMenuItemController.Caption);
                }
            }
            else
            {
                toolbarButtonItemImplementation.SetCaption(genericMenuItemController.Caption);
            }

            toolbarButtonItemImplementation.Disabled = genericMenuItemController.Disabled;
        }

    }
}
