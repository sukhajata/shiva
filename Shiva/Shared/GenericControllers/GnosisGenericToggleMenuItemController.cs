using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.BaseControllers;
using GnosisControls;

namespace Shiva.Shared.GenericControllers
{
    public class GnosisGenericToggleMenuItemController : GnosisGenericMenuItemController
    {
        //public bool Active
        //{
        //    get { return ((GnosisGenericToggleMenuItem)ControlImplementation).Active; }
        //    set
        //    {
        //        ((GnosisGenericToggleMenuItem)ControlImplementation).Active = value;
        //        OnPropertyChanged("Active");
        //    }
        //}

        //public int Code
        //{
        //    get { return ((GnosisGenericToggleMenuItem)ControlImplementation).Code; }
        //}

        public GnosisGenericToggleMenuItemController(
            GnosisGenericToggleMenuItem toggleMenuItem,
            GnosisController parentController)
        : base(toggleMenuItem, parentController)
        {
           // toggleMenuItem.PropertyChanged += ToggleMenuItem_PropertyChanged;
        }

      
        public override void SelectMenuItem()
        {
            if (parentController is GnosisGenericMenuGroupController)
            {
                ((GnosisGenericToggleMenuItem)ControlImplementation).Active = true; //clicking a toggle group button can never unselect  it. Another button must be selected
            }            
            else 
            {
                ((GnosisGenericToggleMenuItem)ControlImplementation).Active = !((GnosisGenericToggleMenuItem)ControlImplementation).Active;
                base.SelectMenuItem();
            }
        }

    }
}
