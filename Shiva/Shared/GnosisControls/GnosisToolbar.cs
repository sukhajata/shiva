using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public partial class GnosisToolbar : INotifyPropertyChanged //: GnosisToolbarControl
    {
        private GnosisSystemAddressField systemAddressField;

        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasFocus;
        private bool hidden;
        private int id;
        private int menuSystemID;
        private int menuControlID;
        private int order;
        private int showOnChildWindow;
        private string tooltip;

        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
            }
        }
        

        [GnosisProperty]
        public string Caption
        {
            get
            {
                return caption;
            }

            set
            {
                caption = value;
                OnPropertyChanged("Caption");
            }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisProperty]
        public bool Hidden
        {
            get
            {
                return hidden;
            }

            set
            {
                hidden = value;
                OnPropertyChanged("Hidden");
            }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get
            {
                return tooltip;
            }

            set
            {
                tooltip = value;
                OnPropertyChanged("Tooltip");
            }
        }

        [GnosisProperty]
        public string ControlType
        {
            get
            {
                return controlType;
            }

            set
            {
                controlType = value;

            }
        }

        [GnosisProperty]
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [GnosisProperty]
        public string GnosisName
        {
            get
            {
                return gnosisName;
            }

            set
            {
                gnosisName = value;
            }
        }

        [GnosisProperty]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;

            }
        }

        [GnosisProperty]
        public int ShowOnChildWindow
        {
            get { return showOnChildWindow; }
            set { showOnChildWindow = value; }
        }

        [GnosisProperty]
        public int MenuSystemID
        {
            get { return menuSystemID; }
            set { menuSystemID = value; }
        }

        [GnosisProperty]
        public int MenuControlID
        {
            get { return menuControlID; }
            set { menuControlID = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public GnosisSystemAddressField SystemAddressField
        {
            get { return systemAddressField; }
            set { systemAddressField = value; }
        }

        //private List<GnosisToolbarButton> toolbarButtons;

        

      

        //[System.Xml.Serialization.XmlElement("GnosisToolbarButton")]
        //public List<GnosisToolbarButton> ToolbarButtons
        //{
        //    get { return toolbarButtons; }
        //    set { toolbarButtons = value; }
        //}

    }
}
