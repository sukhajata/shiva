using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShivaShared3.Interfaces;
using ShivaShared3.Data;
using System.ComponentModel;

namespace GnosisControls
{
    public class GnosisGenericMenu : GnosisControl, INotifyPropertyChanged
    {
        private GnosisSystemAddressField gnosisSystemAddressField;

        private List<GnosisGenericMenuItem> genericMenuItems;

        private List<GnosisGenericMenuGroup> genericMenuGroups;

        private List<GnosisGenericToggleMenuItem> toggleMenuItems;

        private bool hidden;

        private bool allHiddenField;

        private bool allDisabled;

        private string caption;

        private string tooltipField;

        private string gnosisIcon;
        

        [GnosisChild]
        public GnosisSystemAddressField SystemAddressField
        {
            get
            {
                return this.gnosisSystemAddressField;
            }
            set
            {
                this.gnosisSystemAddressField = value;
            }
        }


        [GnosisCollection]
        public List<GnosisGenericMenuItem> GenericMenuItems
        {
            get
            {
                return this.genericMenuItems;
            }
            set
            {
                this.genericMenuItems = value;
            }
        }

        [GnosisCollection]
        public List<GnosisGenericToggleMenuItem> ToggleMenuItems
        {
            get { return toggleMenuItems; }
            set { toggleMenuItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGenericMenuGroup> MenuGroups
        {
            get { return genericMenuGroups; }
            set { genericMenuGroups = value; }
        }

        [GnosisProperty]
        public bool Hidden
        {
            get
            {
                return this.hidden;
            }
            set
            {
                this.hidden = value;
                OnPropertyChanged("Hidden");
            }
        }

        [GnosisProperty]
        public bool AllHidden
        {
            get
            {
                return allHiddenField;
            }
            set
            {
                this.allHiddenField = value;
                OnPropertyChanged("AllHidden");
            }
        }

        [GnosisProperty]
        public bool AllDisabled
        {
            get
            {
                return allDisabled ;
            }
            set
            {
                this.allDisabled = value;
                OnPropertyChanged("AllDisabled");
            }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get
            {
                return this.tooltipField;
            }
            set
            {
                this.tooltipField = value;
                OnPropertyChanged("Tooltip");
            }
        }

        [GnosisProperty]
        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
            }
        }

        [GnosisProperty]
        public string GnosisIcon
        {
            get { return gnosisIcon; }
            set
            {
                gnosisIcon = value;
            }
        }


        public override void GnosisAddChild(IGnosisObject child)
        {

            if (child is GnosisSystemAddressField)
            {
                gnosisSystemAddressField = (GnosisSystemAddressField)child;
            }
            else if (child is GnosisGenericMenuGroup)
            {
                if (genericMenuGroups == null)
                {
                    genericMenuGroups = new List<GnosisGenericMenuGroup>();
                }
                genericMenuGroups.Add((GnosisGenericMenuGroup)child);
            }
            else if (child is GnosisGenericToggleMenuItem)
            {
                if (toggleMenuItems == null)
                {
                    toggleMenuItems = new List<GnosisGenericToggleMenuItem>();
                }
                toggleMenuItems.Add((GnosisGenericToggleMenuItem)child);
            }
            else if (child is GnosisGenericMenuItem)
            {
                if (genericMenuItems == null)
                {
                    genericMenuItems = new List<GnosisGenericMenuItem>();
                }
                genericMenuItems.Add((GnosisGenericMenuItem)child);

            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown type added to GnosisGenericMenuItem: " + child.GetType().Name,
                    "GnosisGenericMenutItem.AddChild()");
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
