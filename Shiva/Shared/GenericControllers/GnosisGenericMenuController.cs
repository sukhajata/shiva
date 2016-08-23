using System;
using System.Collections.Generic;
using System.Text;

using ShivaShared3.Events;
using ShivaShared3.BaseControllers;
using GnosisControls;

namespace ShivaShared3.GenericControllers
{
    public class GnosisGenericMenuController : GnosisController
    {
        public List<GnosisGenericMenuItemController> GenericMenuItemControllers;

        //public GnosisSystemAddressField GnosisSystemAddress
        //{
        //    get
        //    {
        //        if (((GnosisGenericMenu)ControlImplementation).SystemAddressField != null)
        //        {
        //            return ((GnosisGenericMenu)ControlImplementation).SystemAddressField;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}


        //public bool Hidden
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenu)ControlImplementation).Hidden;
        //    }
        //    set
        //    {
        //        ((GnosisGenericMenu)ControlImplementation).Hidden = value;
        //        OnPropertyChanged("Hidden");
        //    }
        //}

        //public bool AllHidden
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenu)ControlImplementation).AllHidden;
        //    }
        //    set
        //    {
        //        ((GnosisGenericMenu)ControlImplementation).AllHidden = value;
        //        OnPropertyChanged("AllHidden");
        //    }
        //}

        //public bool AllDisabled
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenu)ControlImplementation).AllDisabled;
        //    }
        //    set
        //    {
        //        ((GnosisGenericMenu)ControlImplementation).AllDisabled = value;
        //        OnPropertyChanged("AllDisabled");
        //    }
        //}

        //public string Caption
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenu)ControlImplementation).Caption;
        //    }
        //}

        //public string Tooltip
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenu)ControlImplementation).Tooltip;
        //    }
        //}


        public GnosisController FindControllerByID(int controlID)
        {
            if (this.ControlImplementation.ID == controlID)
            {
                return this;
            }
            
            foreach (GnosisGenericMenuItemController menuItemController in GenericMenuItemControllers)
            {
                GnosisController controller = menuItemController.FindControllerByID(controlID);
                if (controller != null)
                {
                    return controller;
                }
            }


            return null;
        }

        public GnosisGenericMenuController(GnosisGenericMenu _genericMenu)
            :base(_genericMenu)
        {
            ((GnosisGenericMenu)ControlImplementation).PropertyChanged += GnosisGenericMenu_PropertyChanged;
            GenericMenuItemControllers = new List<GnosisGenericMenuItemController>();

        }

        private void GnosisGenericMenu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("AllDisabled"))
            {
                if (((GnosisGenericMenu)ControlImplementation).AllDisabled)
                {
                    foreach (GnosisGenericMenuItemController itemController in GenericMenuItemControllers)
                    {
                        SetEnabled(itemController, false);
                    }
                }
            }
            else if(e.PropertyName.Equals("AllHidden"))
            {
                if (((GnosisGenericMenu)ControlImplementation).AllHidden)
                {
                    foreach (GnosisGenericMenuItemController itemController in GenericMenuItemControllers)
                    {
                        SetVisible(itemController, false);
                    }
                }
            }
        }

        private void SetEnabled(GnosisGenericMenuItemController itemController, bool enabled)
        {
            ((GnosisGenericMenuItem)itemController.ControlImplementation).Disabled = !enabled;
            foreach (GnosisGenericMenuItemController childController in itemController.ChildControllers)
            {
                SetEnabled(childController, enabled);
            }
        }

        private void SetVisible(GnosisGenericMenuItemController itemController, bool visible)
        {
            ((GnosisGenericMenuItem)itemController.ControlImplementation).Hidden = !visible;
            foreach (GnosisGenericMenuItemController childController in itemController.ChildControllers)
            {
                SetVisible(childController, visible);
            }
        }

        public void Setup()
        {
            if (((GnosisGenericMenu)ControlImplementation).GenericMenuItems != null)
            {
                foreach (GnosisGenericMenuItem menuItem in ((GnosisGenericMenu)ControlImplementation).GenericMenuItems)
                {
                    GnosisGenericMenuItemController menuItemController = new GnosisGenericMenuItemController(menuItem, this);

                    //if (menuItem.MenuTag.Equals("Search"))
                    //{
                    //    menuItemController = new GnosisGenericMenuItemController_Search(menuItem, this);
                    //}
                    //else
                    //{
                    //    menuItemController = new GnosisGenericMenuItemController(menuItem, this);

                    //}
                    GenericMenuItemControllers.Add(menuItemController);
                }
            }

            if (((GnosisGenericMenu)ControlImplementation).ToggleMenuItems != null)
            {
                foreach (GnosisGenericToggleMenuItem toggleMenuItem in ((GnosisGenericMenu)ControlImplementation).ToggleMenuItems)
                {
                    GnosisGenericToggleMenuItemController toggleMenuItemController = new GnosisGenericToggleMenuItemController(toggleMenuItem, this);
                    GenericMenuItemControllers.Add(toggleMenuItemController);
                }
            }

            if (((GnosisGenericMenu)ControlImplementation).MenuGroups != null)
            {
                foreach (GnosisGenericMenuGroup menuGroup in ((GnosisGenericMenu)ControlImplementation).MenuGroups)
                {
                    GnosisGenericMenuGroupController menuGroupController = new GnosisGenericMenuGroupController(menuGroup, this);
                    GenericMenuItemControllers.Add(menuGroupController);
                }
            }
        }

        public override void ExecuteAction(GnosisEventHandler.TargetActionType actionType)
        {
            switch (actionType)
            {
               
                default:
                    base.ExecuteAction(actionType);
                    break;
            }
        }

        //private void Redraw()
        //{
        //    foreach(GnosisGenericMenuItemController itemController in GenericMenuItemControllers)
        //    {
        //        itemController.Redraw = true;
        //    }
        //}

        //private void GnosisGenericMenuController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
          

        //    GlobalData.Instance.ErrorHandler.HandleError(e.PropertyName + " changed", "GnosisGenericMenuController");
        //}
    }
}
