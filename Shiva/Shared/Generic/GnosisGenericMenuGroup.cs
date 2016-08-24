using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisGenericMenuGroup : GnosisGenericMenuItem
    {
        private List<GnosisGenericMenuItem> menuItems;
        private List<GnosisGenericToggleMenuItem> toggleMenuItems;

        private int variableSystemID;

        private int variableControlID;


        [GnosisProperty]
        public int VariableSystemID
        {
            get { return variableSystemID; }
            set { variableSystemID = value; }
        }

        [GnosisProperty]
        public int VariableControlID
        {
            get { return variableControlID; }
            set { variableControlID = value; }
        }

        [GnosisCollection]
        public List<GnosisGenericToggleMenuItem> ToggleMenuItems
        {
            get { return toggleMenuItems; }
            set { toggleMenuItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGenericMenuItem> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisGenericToggleMenuItem)
            {
                if (toggleMenuItems == null)
                {
                    toggleMenuItems = new List<GnosisGenericToggleMenuItem>();
                }
                toggleMenuItems.Add((GnosisGenericToggleMenuItem)child);
            }
            else if (child is GnosisGenericMenuItem)
            {
                if (menuItems == null)
                {
                    menuItems = new List<GnosisGenericMenuItem>();
                }
                menuItems.Add((GnosisGenericMenuItem)child);
            }
            else
            {
                base.GnosisAddChild(child);

            }
        }

    }
}
