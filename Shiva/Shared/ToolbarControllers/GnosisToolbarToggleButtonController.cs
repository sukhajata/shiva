using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.GenericControllers;
using ShivaShared3.Utility;
using ShivaShared3.BaseControllers;
using GnosisControls;

namespace ShivaShared3.ToolbarControllers
{
    public  class GnosisToolbarToggleButtonController : GnosisVisibleController
    {
        private GnosisToggleButton toggleButton;
        private GnosisGenericToggleMenuItemController genericToggleMenuItemController;
        private GnosisGenericToggleMenuItem menuItem;

        public GnosisToolbarToggleButtonController(
            GnosisGenericToggleMenuItemController toggleMenuItemController,
            GnosisToggleButton _toggleButton,
            GnosisEntityController entityController,
            GnosisVisibleController parent)
            :base(_toggleButton, entityController, parent)
        {
            toggleButton = _toggleButton;
            genericToggleMenuItemController = toggleMenuItemController;
            menuItem = (GnosisGenericToggleMenuItem)genericToggleMenuItemController.ControlImplementation;

            //The toggle button reflects the properties of the generic menu item.
            //A click on a toggle button is passed to the generic menu item controller and changes are made to the generic menu item.
            //toggleButton.Order = menuItem.Order;
            //toggleButton.GnosisIcon = menuItem.GnosisIcon;
            //toggleButton.Tooltip = menuItem.Tooltip;
            //toggleButton.Caption = menuItem.Caption;
            //toggleButton.Active = menuItem.Active;
            //toggleButton.Disabled = menuItem.Disabled;

            menuItem.PropertyChanged += MenuItem_PropertyChanged;
           // toggleButton.PropertyChanged += ToggleButton_PropertyChanged;

            toggleButton.SetClickHandler(new Action(OnClick));

         
        }

     
        //private void ToggleButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("Active"))
        //    {
        //        if (menuItem.Active != toggleButton.Active) //avoid infinite loop
        //        {
        //            menuItem.Active = toggleButton.Active;
        //        }
        //    }
        //}

        private void MenuItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Active":
                    if (toggleButton.Active != menuItem.Active)//avoid infinite loop
                    {
                        toggleButton.Active = menuItem.Active;
                    }
                    break;
                case "Disabled":
                    toggleButton.Disabled = menuItem.Disabled;
                    break;
                case "Hidden":
                    toggleButton.Hidden = menuItem.Hidden;
                    break;


            }
        }

        //called by implementation
        public void OnClick()
        {
            genericToggleMenuItemController.SelectMenuItem();

            //if (selected != ((GnosisGenericToggleMenuItem)genericToggleMenuItemController.ControlImplementation).Active) //avoid infinite loops
            //{
            //    genericToggleMenuItemController.SelectMenuItem();
            //}
        }

        //private void GenericToggleMenuItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    GnosisGenericToggleMenuItem genericMenuItem = (GnosisGenericToggleMenuItem)genericToggleMenuItemController.ControlImplementation;
        //    if (e.PropertyName.Equals("Disabled"))
        //    {
        //        toggleButton.Disabled = genericMenuItem.Disabled;
        //        string iconFileName = IconNameMapper.GetIconFileName(genericMenuItem.GnosisIcon);
        //        if (iconFileName != null)
        //        {
        //            toggleButton.GnosisIcon = iconFileName;//, !genericToggleMenuItemController.Disabled);
        //        }
        //    }
        //    else if (e.PropertyName.Equals("Active"))
        //    {
        //        //beware of infinite loop - setting selected in the implementation triggers a property change in the GenericToggleMenuItemController
        //        if (genericMenuItem.Active != toggleButton.Active)
        //        {
        //            toggleButton.Active = genericMenuItem.Active;
        //        }
        //    }
        //    else if (e.PropertyName.Equals("Hidden"))
        //    {
        //        toggleButton.Hidden = genericMenuItem.Hidden;
        //    }
        //}

        //internal void SetSelected(bool selected)
        //{
        //    toggleButtonImplementation.SetSelected(selected);
        //}

        internal override void HideTooltips()
        {
            toggleButton.SetTooltipVisible(false);
        }

        internal override void ShowTooltips()
        {
            toggleButton.SetTooltipVisible(true);
        }
    }
}
