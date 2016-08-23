using ShivaShared3.BaseControllers;
using ShivaShared3.ContentControllers;
using ShivaShared3.Data;
using ShivaShared3.DataControllers;
using ShivaShared3.GenericControllers;
using ShivaShared3.Interfaces;
using GnosisControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShivaShared3.Utility;

namespace ShivaShared3.ToolbarControllers
{
    public class GnosisToolbarController : GnosisVisibleController
    {
        private GnosisToolbar toolbar;
        private GnosisGenericMenuController genericMenuController;
        private List<object> toolbarButtonControllers;

        public GnosisToolbarController(
            GnosisToolbar toolbar,
          //  IGnosisToolbarImplementation _toolbarImplementation,
            GnosisEntityController entityController,
            GnosisToolbarTrayController parent)
            :base(toolbar, entityController, parent)
        {
            this.toolbar = toolbar;
        }

        public void Setup()
        {
            toolbarButtonControllers = new List<object>();

            try
            {
                //Find the GnosisGenericMenu which this toolbar points at
                genericMenuController = (GnosisGenericMenuController)GlobalData.Singleton.FindController(((GnosisToolbar)ControlImplementation).MenuSystemID,
                     ((GnosisToolbar)ControlImplementation).MenuControlID);

                //create toolbar buttons for each generic menu item
                if (genericMenuController.GenericMenuItemControllers != null && genericMenuController.GenericMenuItemControllers.Count > 0)
                {
                    foreach (GnosisGenericMenuItemController menuItemController in genericMenuController.GenericMenuItemControllers.OrderBy(x => x.ControlImplementation.Order))
                    {
                        object btnController;

                        if (menuItemController.ChildControllers.Count > 0)
                        {
                            GnosisMenuButton menuButton = GnosisControlCreator.CreateGnosisMenuButton();
                            //btn = CreateDropDownButton(menuItemController);
                          //  IGnosisToolbarMenuButtonImplementation menuBtnImplementation =
                            //    GlobalData.Singleton.ImplementationCreator.GetGnosisToolbarMenuButtonImplementation();
                            btnController = new GnosisToolbarMenuButtonController(menuItemController, menuButton, EntityController, this);
                            ((GnosisToolbarMenuButtonController)btnController).Setup();

                            toolbar.AddMenuButton(menuButton);
                            toolbarButtonControllers.Add(btnController);

                        }
                        else if (menuItemController is GnosisGenericToggleMenuItemController)
                        {
                            // IGnosisToggleButtonImplementation toggleButton = GlobalData.Singleton.ImplementationCreator.GetGnosisToggleButtonImplementation();
                            GnosisToggleButton toggleButton = new GnosisToggleButton();
                            GnosisGenericToggleMenuItem menuItem = (GnosisGenericToggleMenuItem)menuItemController.ControlImplementation;
                            toggleButton.Active = menuItem.Active;
                            toggleButton.Caption = menuItem.Caption;
                            toggleButton.GnosisIcon = menuItem.GnosisIcon;
                            toggleButton.ToolTip = menuItem.Tooltip;
                            toggleButton.Disabled = menuItem.Disabled;
                            toggleButton.Hidden = menuItem.Hidden;
                            btnController = new GnosisToolbarToggleButtonController((GnosisGenericToggleMenuItemController)menuItemController, toggleButton, EntityController, this);
                            //((GnosisToolbarToggleButtonController)btnController).Setup();

                            toolbar.AddToggleButton(toggleButton);
                            toolbarButtonControllers.Add(btnController);

                        }
                        else if (menuItemController is GnosisGenericMenuGroupController)
                        {
                            GnosisGenericMenuGroupController groupController = (GnosisGenericMenuGroupController)menuItemController;

                            foreach (GnosisGenericToggleMenuItemController toggleMenuItemController in groupController.ToggleMenuItemControllers)
                            {
                                // IGnosisToggleButtonImplementation toggleButton = GlobalData.Singleton.ImplementationCreator.GetGnosisToggleButtonImplementation();
                                GnosisToggleButton toggleButton = new GnosisToggleButton();
                                GnosisGenericToggleMenuItem menuItem = (GnosisGenericToggleMenuItem)toggleMenuItemController.ControlImplementation;
                                toggleButton.Active = menuItem.Active;
                                toggleButton.Caption = menuItem.Caption;
                                toggleButton.GnosisIcon = menuItem.GnosisIcon;
                                toggleButton.ToolTip = menuItem.Tooltip;
                                toggleButton.Disabled = menuItem.Disabled;
                                toggleButton.Hidden = menuItem.Hidden;
                                btnController = new GnosisToolbarToggleButtonController(toggleMenuItemController, toggleButton, EntityController, this);
                                toolbar.AddToggleButton(toggleButton);
                                toolbarButtonControllers.Add(btnController);

                            }
                        }
                        else
                        {
                            GnosisButton toolbarButton = new GnosisButton();// ((GnosisGenericMenuItem)menuItemController.ControlImplementation);
                            GnosisGenericMenuItem menuItem = (GnosisGenericMenuItem)menuItemController.ControlImplementation;
                            toolbarButton.Caption = menuItem.Caption;
                            toolbarButton.GnosisIcon = menuItem.GnosisIcon;
                            toolbarButton.ToolTip = menuItem.Tooltip;
                            toolbarButton.Disabled = menuItem.Disabled;
                            toolbarButton.Hidden = menuItem.Hidden;
                            // IGnosisButtonImplementation btnImplementation =
                            //   GlobalData.Singleton.ImplementationCreator.GetGnosisButtonImplementation();
                            btnController = new GnosisToolbarButtonController(menuItemController, toolbarButton, EntityController, this);
                            ((GnosisToolbarButtonController)btnController).Setup();

                            toolbar.AddToolbarButton(toolbarButton);
                            toolbarButtonControllers.Add(btnController);

                        }


                        //genericMenuItemControllers.Add(menuItemController);
                    }
                }

                if (((GnosisGenericMenu)genericMenuController.ControlImplementation).SystemAddressField != null)
                {
                   // IGnosisSystemAddressFieldImplementation systemAddressFieldImp = 
                     //   GlobalData.Singleton.ImplementationCreator.GetGnosisSystemAddressFieldImplementation();
                    GnosisSystemAddressController systemAddressController = new GnosisSystemAddressController(
                        ((GnosisGenericMenu)genericMenuController.ControlImplementation).SystemAddressField, EntityController, this);
                    toolbar.AddSystemAddressField(((GnosisGenericMenu)genericMenuController.ControlImplementation).SystemAddressField);
                }

            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }


        }


        internal override void ShowTooltips()
        {
            foreach (object buttonController in toolbarButtonControllers)
            {
                if (buttonController is GnosisToolbarButtonController)
                {
                    ((GnosisToolbarButtonController)buttonController).ShowTooltips();
                }
                else if (buttonController is GnosisToolbarToggleButtonController)
                {
                    ((GnosisToolbarToggleButtonController)buttonController).ShowTooltips();
                }
            }
        }

        internal override void HideTooltips()
        {
            foreach (object buttonController in toolbarButtonControllers)
            {
                if (buttonController is GnosisToolbarButtonController)
                {
                    ((GnosisToolbarButtonController)buttonController).HideTooltips();
                }
                else if (buttonController is GnosisToolbarToggleButtonController)
                {
                    ((GnosisToolbarToggleButtonController)buttonController).HideTooltips();
                }
            }
        }
    }
}
