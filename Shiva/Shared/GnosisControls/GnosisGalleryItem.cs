
using GnosisControls;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Markup;

namespace GnosisControls
{
    public partial class GnosisGalleryItem : INotifyPropertyChanged
    {

        private List<GnosisGalleryDatasetItem> galleryDatasetItems;
        private List<GnosisGalleryItem> galleryItems;
        private List<GnosisGalleryDocumentItem> galleryDocumentItems;
        private List<GnosisGallerySearchItem> gallerySearchItems;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private bool active;
        private string caption;
        private bool disabled;
        private bool gnosisExpanded;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private string controlType;
        private bool hidden;
        private int id;
        private int order;
        private int selectedFactor;
        private string tooltip;

      
        [GnosisPropertyAttribute]
        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                OnPropertyChanged("Active");
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

        [GnosisPropertyAttribute]
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

        [GnosisPropertyAttribute]
        public bool GnosisExpanded
        {
            get { return gnosisExpanded; }
            set
            {
                gnosisExpanded = value;
                OnPropertyChanged("GnosisExpanded");
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


        [GnosisPropertyAttribute]
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

        [GnosisPropertyAttribute]
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                // OnPropertyChanged("ID");
            }
        }

        [GnosisPropertyAttribute]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
                //OnPropertyChanged("Order");
            }
        }

        [GnosisProperty]
        public int SelectedFactor
        {
            get { return selectedFactor; }
            set { selectedFactor = value; }
        }

        [GnosisPropertyAttribute]
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

        [GnosisPropertyAttribute]
        public bool Disabled
        {
            get { return disabled; }
            set
            {
                disabled = value;
                OnPropertyChanged("Disabled");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        [GnosisCollection]
        public List<GnosisGalleryDatasetItem> GalleryDatasetItems
        {
            get { return galleryDatasetItems; }
            set { galleryDatasetItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGalleryDocumentItem> GalleryDocumentItems
        {
            get { return galleryDocumentItems; }
            set { galleryDocumentItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGalleryItem> GalleryItems
        {
            get { return galleryItems; }
            set { galleryItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGallerySearchItem> GallerySearchItems
        {
            get { return gallerySearchItems; }
            set { gallerySearchItems = value; }
        }



       
    }
}
