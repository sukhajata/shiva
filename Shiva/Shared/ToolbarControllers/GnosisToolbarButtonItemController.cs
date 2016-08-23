using ShivaShared3.ContentControllers;
using ShivaShared3.DataControllers;
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
            if (((GnosisGenericMenu)genericMenuItemController.ControlImplementation).GnosisIcon != null 
                && ((GnosisGenericMenu)genericMenuItemController.ControlImplementation).GnosisIcon.Length > 0)
            {
                string iconFileName = IconNameMapper.GetIconFileName(((GnosisGenericMenu)genericMenuItemController.ControlImplementation).GnosisIcon);
                if (iconFileName != null)
                {
                    toolbarButtonItemImplementation.GnosisIcon = iconFileName;//, !genericMenuItemController.Disabled);
                }
                else
                {
                    toolbarButtonItemImplementation.Caption = ((GnosisGenericMenu)genericMenuItemController.ControlImplementation).Caption;
                }
            }
            else
            {
                toolbarButtonItemImplementation.Caption = ((GnosisGenericMenu)genericMenuItemController.ControlImplementation).Caption;
            }

           // toolbarButtonItemImplementation.Disabled = ((GnosisGenericMenu)genericMenuItemController.ControlImplementation).Disabled;
        }

    }
}
