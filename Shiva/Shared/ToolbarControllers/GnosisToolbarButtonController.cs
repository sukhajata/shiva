using ShivaShared3.BaseControllers;

using ShivaShared3.ContentControllers;
using ShivaShared3.Data;
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
    public class GnosisToolbarButtonController : GnosisVisibleController
    {
        private GnosisGenericMenuItemController genericMenuItemController;
        private GnosisGenericMenuItem menuItem;
        private GnosisButton button;
       // private List<GnosisToolbarButtonItemController> childControllers;


        public GnosisToolbarButtonController(
            GnosisGenericMenuItemController _genericMenuItemController,
            GnosisButton _button,
            GnosisEntityController entityController,
            GnosisVisibleController parent)
            :base (_button, entityController, parent)
        {
            genericMenuItemController = _genericMenuItemController;
            menuItem = (GnosisGenericMenuItem)genericMenuItemController.ControlImplementation;
            button = _button;

           

            menuItem.PropertyChanged += GenericMenuItem_PropertyChanged;

        }

      

        private void GenericMenuItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Disabled"))
            {
                button.Disabled = menuItem.Disabled;

                //string iconFileName = IconNameMapper.GetIconFileName(((GnosisGenericMenuItem)genericMenuItemController.ControlImplementation).GnosisIcon);
                //if (iconFileName != null)
                //{
                //    button.GnosisIcon = iconFileName;//, !((GnosisGenericMenuItem)genericMenuItemController.ControlImplementation).Disabled);
                //}
            }
            else if (e.PropertyName.Equals("Hidden"))
            {
                button.Hidden = menuItem.Hidden;
            }

        }

        public void Setup()
        {
            //childControllers = new List<GnosisToolbarButtonItemController>();

            //if (genericMenuItemController.ChildControllers.Count == 0)
            //{
            //   // buttonImplementation.SetClickHandler(new Action(genericMenuItemController.SelectMenuItem));
            //    if (menuItem.GnosisIcon != null && menuItem.GnosisIcon.Length > 0)
            //    {
            //        string iconFileName = IconNameMapper.GetIconFileName(menuItem.GnosisIcon);
            //        if (iconFileName != null)
            //        {
            //            button.GnosisIcon = iconFileName;//, !genericMenuItemController.Disabled);
            //        }
            //        else
            //        {
            //            button.Caption = menuItem.Caption;
            //        }
            //    }
            //    else
            //    {
            //        button.Caption = menuItem.Caption;
            //    }

               
            //}
            //else
            //{
            //    foreach (GnosisGenericMenuItemController secondTierItemController in genericMenuItemController.ChildControllers)
            //    {
            //       // IGnosisToolbarMenuButtonItemImplementation secondTierItemImplementation =
            //         //   GlobalData.Singleton.ImplementationCreator.GetGnosisToolbarMenuButtonItemImplementation();
            //        GnosisToolbarButtonItemController secondTierButtonController =
            //            new GnosisToolbarButtonItemController(secondTierItemController, secondTierItemImplementation);
            //        secondTierButtonController.Setup();

            //        childControllers.Add(secondTierButtonController);
            //        ((IGnosisToolbarMenuButtonImplementation)button).AddItem(secondTierItemImplementation);

            //    }
            //}


        }

        public override void OnMouseUp()
        {
            base.OnMouseUp();

            genericMenuItemController.SelectMenuItem();

        }

        internal override void ShowTooltips()
        {
            button.SetTooltipVisible(true);

            //foreach (GnosisToolbarButtonItemController childController in childControllers)
            //{
            //    ShowTooltips();
            //}

        }

        internal override void HideTooltips()
        {
            button.SetTooltipVisible(false);

            //foreach (GnosisToolbarButtonItemController childController in childControllers)
            //{
            //    HideTooltips();
            //}
        }
    }
}
