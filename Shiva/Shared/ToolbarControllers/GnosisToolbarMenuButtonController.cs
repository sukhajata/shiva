using GnosisControls;
using ShivaShared3.BaseControllers;
using ShivaShared3.DataControllers;
using ShivaShared3.GenericControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.ToolbarControllers
{
    public class GnosisToolbarMenuButtonController : GnosisVisibleController
    {
        private GnosisGenericMenuItemController genericController;
        private GnosisGenericMenuItem menuItem;
        private GnosisMenuButton menuButton;
        private List<GnosisVisibleController> childControllers;

        public GnosisToolbarMenuButtonController(
            GnosisGenericMenuItemController genericMenuItemController,
            GnosisMenuButton _menuButton,
            GnosisEntityController entityController,
            GnosisToolbarController parent)
            :base(_menuButton, entityController, parent)
        {
            menuItem = (GnosisGenericMenuItem)genericMenuItemController.ControlImplementation;
            menuButton = _menuButton;
            genericController = genericMenuItemController;

            menuButton.Order = menuItem.Order;
            menuButton.GnosisIcon = menuItem.GnosisIcon;
            menuButton.Disabled = menuItem.Disabled;
            menuButton.Caption = menuItem.Caption;
            menuButton.Tooltip = menuItem.Tooltip;
            menuButton.Hidden = menuItem.Hidden;

            menuItem.PropertyChanged += MenuItem_PropertyChanged;
        }

        public GnosisToolbarMenuButtonController(
           GnosisGenericMenuItemController genericMenuItemController,
           GnosisMenuButton _menuButton,
           GnosisEntityController entityController,
           GnosisToolbarMenuButtonController parent)
           : base(_menuButton, entityController, parent)
        {
            menuItem = (GnosisGenericMenuItem)genericMenuItemController.ControlImplementation;
            menuButton = _menuButton;
            genericController = genericMenuItemController;

            menuButton.Order = menuItem.Order;
            menuButton.GnosisIcon = menuItem.GnosisIcon;
            menuButton.Disabled = menuItem.Disabled;
            menuButton.Caption = menuItem.Caption;
            menuButton.Tooltip = menuItem.Tooltip;
            menuButton.Hidden = menuItem.Hidden;

            menuItem.PropertyChanged += MenuItem_PropertyChanged;
        }

        private void MenuItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Disabled":
                    ((GnosisMenuButton)ControlImplementation).Disabled = menuItem.Disabled;
                    break;
                case "Hidden":
                    ((GnosisMenuButton)ControlImplementation).Hidden = menuItem.Hidden;
                    break;
            }
        }

        public void Setup()
        {
            foreach (GnosisGenericMenuItemController menuItemController in genericController.ChildControllers)
            {

                if (menuItemController.ChildControllers.Count > 0)
                {
                    GnosisMenuButton menuButton = new GnosisMenuButton();
                    //btn = CreateDropDownButton(menuItemController);
                    //  IGnosisToolbarMenuButtonImplementation menuBtnImplementation =
                    //    GlobalData.Singleton.ImplementationCreator.GetGnosisToolbarMenuButtonImplementation();
                    GnosisToolbarMenuButtonController btnController = new GnosisToolbarMenuButtonController(menuItemController, menuButton, EntityController, this);
                    btnController.Setup();

                    menuButton.AddMenuButton(menuButton);
                    childControllers.Add(btnController);

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
                    GnosisToolbarToggleButtonController btnController = new GnosisToolbarToggleButtonController((GnosisGenericToggleMenuItemController)menuItemController, toggleButton, EntityController, this);
                    //((GnosisToolbarToggleButtonController)btnController).Setup();

                    menuButton.AddToggleButton(toggleButton);
                    childControllers.Add(btnController);

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
                        GnosisToolbarToggleButtonController btnController = new GnosisToolbarToggleButtonController(toggleMenuItemController, toggleButton, EntityController, this);

                        menuButton.AddToggleButton(toggleButton);
                        childControllers.Add(btnController);

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
                    GnosisToolbarButtonController btnController = new GnosisToolbarButtonController(menuItemController, toolbarButton, EntityController, this);
                    ((GnosisToolbarButtonController)btnController).Setup();

                    menuButton.AddButton(toolbarButton);
                    childControllers.Add(btnController);

                }

            }
        }

        internal override void ShowTooltips()
        {
            menuButton.SetTooltipVisible(true);

            foreach (GnosisVisibleController childController in childControllers)
            {
                ShowTooltips();
            }

        }

        internal override void HideTooltips()
        {
            menuButton.SetTooltipVisible(false);

            foreach (GnosisVisibleController childController in childControllers)
            {
                HideTooltips();
            }
        }
    }
}
