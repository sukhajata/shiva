using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ShivaShared3.Data;
using ShivaShared3.BaseControllers;
using GnosisControls;

namespace ShivaShared3.GenericControllers
{
    public class GnosisGenericMenuItemController : GnosisController
    {
        protected GnosisController parentController;

        public static GnosisGenericMenuItemController CurrentMenuItemController;

        public List<GnosisEventHandler> EventHandlers;

        public List<GnosisGenericMenuItemController> ChildControllers;

        //public string Caption
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenuItem)ControlImplementation).Caption;
        //    }
        //}

        //public string Tooltip
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenuItem)ControlImplementation).Tooltip;
        //    }
        //}

        //public bool Hidden
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenuItem)ControlImplementation).Hidden;
        //    }
        //    set
        //    {
        //        ((GnosisGenericMenuItem)ControlImplementation).Hidden = value;
        //        OnPropertyChanged("Hidden");
        //    }
        //}

        //public string GnosisIcon
        //{
        //    get { return ((GnosisGenericMenuItem)ControlImplementation).GnosisIcon; }
        //}

        //public bool AllHidden
        //{
        //    get { return ((GnosisGenericMenuItem)ControlImplementation).AllHidden; }
        //    set
        //    {
        //        ((GnosisGenericMenuItem)ControlImplementation).AllHidden = value;
        //        OnPropertyChanged("AllHidden");
        //    }
        //}

        //public GnosisGenericMenuItem.MenuTagEnum MenuTag
        //{
        //    get
        //    {
        //        return ((GnosisGenericMenuItem)ControlImplementation)._MenuTag;
        //    }
        //}

        //public bool Disabled
        //{
        //    get
        //    {

        //        return ((GnosisGenericMenuItem)ControlImplementation).Disabled;
        //    }
        //    set
        //    {
        //        ((GnosisGenericMenuItem)ControlImplementation).Disabled = value;
        //        OnPropertyChanged("Disabled");
        //    }
        //}

        //public bool Redraw
        //{
        //    set
        //    {
        //        OnPropertyChanged("Redraw");
        //        foreach(GnosisGenericMenuItemController childController in ChildControllers)
        //        {
        //            childController.Redraw = true;
        //        }
        //    }
            
        //}

        public GnosisGenericMenuItemController(GnosisGenericMenuItem _menuItem, GnosisController _parentController)
            :base(_menuItem)
        {
            parentController = _parentController;
            ChildControllers = new List<GnosisGenericMenuItemController>();
            EventHandlers = new List<GnosisEventHandler>();
            Setup();
        }

        protected virtual void Setup()
        {
            if (((GnosisGenericMenuItem)ControlImplementation).GnosisGenericMenuItems != null)
            {
                foreach (GnosisGenericMenuItem menuItem in ((GnosisGenericMenuItem)ControlImplementation).GnosisGenericMenuItems)
                {
                    GnosisGenericMenuItemController menuItemController = new GnosisGenericMenuItemController(menuItem, this);
                    ChildControllers.Add(menuItemController);
                }
            }

            if(((GnosisGenericMenuItem)ControlImplementation).EventHandlers != null)
            {
                foreach (GnosisEventHandler handler in ((GnosisGenericMenuItem)ControlImplementation).EventHandlers)
                {
                    EventHandlers.Add(handler);
                }
            }

        }

    

        private GnosisGenericMenuController FindRootController()
        {
            GnosisController controller = parentController;
            while (!(controller is GnosisGenericMenuController))
            {
                controller = ((GnosisGenericMenuItemController)controller).parentController;
            }

            return (GnosisGenericMenuController)controller;
        }

        public virtual void SelectMenuItem()
        {
            CurrentMenuItemController = this;
            
            //either a SystemCommand or has EventHandler(s)
            if (EventHandlers.Count == 0)
            {
                GlobalData.Singleton.SystemCommands.RunSystemCommand(((GnosisGenericMenuItem)this.ControlImplementation)._MenuTag);
            }
            else
            {
                //Actuate all EventHandlers
                foreach (GnosisEventHandler handler in EventHandlers.OrderBy(x => x.Order))
                {
                    handler.HandleEvent(this);
                }
            }
        }

        public GnosisController FindControllerByID(int controlID)
        {
            if (this.ControlImplementation.ID == controlID)
            {
                return this;
            }

            foreach (GnosisGenericMenuItemController menuItemController in ChildControllers)
            {
                GnosisController controller = menuItemController.FindControllerByID(controlID);
                if (controller != null)
                {
                    return controller;
                }
            }

            return null;
        }

        internal GnosisGenericMenuItemController GetMenuItemByTag(GnosisGenericMenuItem.MenuTagEnum menuTag)
        {
            GnosisGenericMenuItemController result = null;

            if (((GnosisGenericMenuItem)this.ControlImplementation)._MenuTag == menuTag)
            {
                return this;
            }
            else if (ChildControllers.Count > 0)
            {
                foreach (GnosisGenericMenuItemController childController in ChildControllers)
                {
                    result = childController.GetMenuItemByTag(menuTag);

                    if (result != null)
                    {
                        break;
                    }
                }
            }

            return result;
        }

    }
}
