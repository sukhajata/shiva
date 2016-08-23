using GnosisControls;
using System;
using System.ComponentModel;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public partial class GnosisSystemAddressField : INotifyPropertyChanged
    {
        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool hidden;
        private int id;
        private bool locked;
        private string menuTag;
        private int order;
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
        public bool HasMouseFocus
        {
            get { return hasMouseFocus; }
            set
            {
                hasMouseFocus = value;
                OnPropertyChanged("HasMouseFocus");
            }
        }
        public bool HasMouseDown
        {
            get { return hasMouseDown; }
            set
            {
                hasMouseDown = value;
                OnPropertyChanged("HasMouseDown");
            }
        }

        [GnosisProperty]
        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                OnPropertyChanged("Caption");
            }
        }

        [GnosisPropertyAttribute]
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
        public string GnosisName
        {
            get { return gnosisName; }
            set { gnosisName = value; }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisProperty]
        public bool Hidden
        {
            get { return hidden; }
            set
            {
                hidden = value;
                OnPropertyChanged("Hidden");
            }

        }

        [GnosisProperty]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [GnosisProperty]
        public bool Locked
        {
            get { return locked; }
            set
            {
                locked = value;
                OnPropertyChanged("Locked");
            }
        }
        

        [GnosisProperty]
        public string MenuTag
        {
            get { return menuTag; }
            set { menuTag = value; }
        }


        [GnosisProperty]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get { return tooltip; }
            set
            {
                tooltip = value;
                OnPropertyChanged("Tooltip");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
       

    }
}
