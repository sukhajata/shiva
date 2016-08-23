using ShivaShared3.Events;
using System.Collections.Generic;
using System.ComponentModel;

namespace GnosisControls
{
    public partial class GnosisTile : INotifyPropertyChanged//GnosisContainer
    {
        private List<GnosisTileDetail> tileDetails;
        private List<GnosisEventHandler> eventHandlers;


        private bool hasTabs;
        private bool acceptsSearchFrames;
        private bool acceptsDocumentFrames;

        //private bool hasFocus;
        //private bool hasMouseFocus;
        //private bool hasMouseDown;

        //private string caption;
        //private string controlType;
        //private string gnosisName;
        //private bool hidden;
        //private int id;
        //private int order;
        //private string tooltip;

        //public bool HasFocus
        //{
        //    get { return hasFocus; }
        //    set
        //    {
        //        hasFocus = value;
        //        OnPropertyChanged("HasFocus");
        //    }
        //}
        //public bool HasMouseFocus
        //{
        //    get { return hasMouseFocus; }
        //    set
        //    {
        //        hasMouseFocus = value;
        //        OnPropertyChanged("HasMouseFocus");
        //    }
        //}
        //public bool HasMouseDown
        //{
        //    get { return hasMouseDown; }
        //    set
        //    {
        //        hasMouseDown = value;
        //        OnPropertyChanged("HasMouseDown");
        //    }
        //}


        //[GnosisPropertyAttribute]
        //public string ControlType
        //{
        //    get
        //    {
        //        return controlType;
        //    }

        //    set
        //    {
        //        controlType = value;
        //    }
        //}

        //[GnosisPropertyAttribute]
        //public string Caption
        //{
        //    get
        //    {
        //        return caption;
        //    }

        //    set
        //    {
        //        caption = value;
        //        OnPropertyChanged("Caption");
        //    }
        //}

        //[GnosisPropertyAttribute]
        //public string GnosisName
        //{
        //    get { return gnosisName; }
        //    set { gnosisName = value; }
        //}

        //[GnosisPropertyAttribute]
        //public bool Hidden
        //{
        //    get
        //    {
        //        return hidden;
        //    }

        //    set
        //    {
        //        hidden = value;
        //        OnPropertyChanged("Hidden");
        //    }
        //}

        //[GnosisPropertyAttribute]
        //public int ID
        //{
        //    get
        //    {
        //        return id;
        //    }

        //    set
        //    {
        //        id = value;
        //        // OnPropertyChanged("ID");
        //    }
        //}

        //[GnosisPropertyAttribute]
        //public int Order
        //{
        //    get
        //    {
        //        return order;
        //    }

        //    set
        //    {
        //        order = value;
        //        //OnPropertyChanged("Order");
        //    }
        //}

        //[GnosisPropertyAttribute]
        //public string Tooltip
        //{
        //    get
        //    {
        //        return tooltip;
        //    }

        //    set
        //    {
        //        tooltip = value;
        //        OnPropertyChanged("Tooltip");
        //    }
        //}

      


        [GnosisPropertyAttribute]
        public bool HasTabs
        {
            get
            {
                return hasTabs;
            }

            set
            {
                hasTabs = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool AcceptsSearchFrames
        {
            get
            {
                return acceptsSearchFrames;
            }

            set
            {
                acceptsSearchFrames = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool AcceptsDocumentFrames
        {
            get
            {
                return acceptsDocumentFrames;
            }

            set
            {
                acceptsDocumentFrames = value;
            }
        }

        
        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged(string name)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}

        [GnosisCollection]
        public List<GnosisTileDetail> TileDetails
        {
            get { return tileDetails; }
            set { tileDetails = value; }
        }

        [GnosisCollection]
        public List<GnosisEventHandler> EventHandlers
        {
            get { return eventHandlers; }
            set { eventHandlers = value; }
        }

    }
}