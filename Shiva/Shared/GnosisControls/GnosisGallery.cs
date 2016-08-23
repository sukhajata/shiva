using GnosisControls;
using ShivaShared3.Data;
using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GnosisControls
{
    public partial class GnosisGallery : INotifyPropertyChanged
    {

        private List<GnosisGalleryItem> galleryItems;
        private List<GnosisGallerySearchAttribute> galleryAttributes;
        private List<GnosisGalleryDetail> galleryDetails;
        private List<GnosisGalleryDocumentItem> galleryDocumentItems;
        private List<GnosisGallerySearchItem> gallerySearchItems;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private string controlType;
        private string direction;
        private int expandToLevel;
        private bool hasBorder;
        private bool isWideFormat;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int maxSectionSpan;
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
                // string xaml = XamlWriter.Save(this);
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
        public bool HasBorder
        {
            get { return hasBorder; }
            set { hasBorder = value; }
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

        [GnosisProperty]
        public bool IsWideFormat
        {
            get { return isWideFormat; }
            set { isWideFormat = value; }
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
        public int ExpandToLevel
        {
            get
            {
                return expandToLevel;
            }

            set
            {
                expandToLevel = value;
            }
        }

        [GnosisPropertyAttribute]
        public string Direction
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxSectionSpan
        {
            get
            {
                return maxSectionSpan;
            }

            set
            {
                maxSectionSpan = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



       
        
        [GnosisCollection]
        public List<GnosisGalleryItem> GalleryItems
        {
            get { return galleryItems; }
            set { galleryItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGallerySearchAttribute> GalleryAttributes
        {
            get { return galleryAttributes; }
            set { galleryAttributes = value; }
        }

        [GnosisCollection]
        public List<GnosisGalleryDocumentItem> GalleryDocumentItems
        {
            get { return galleryDocumentItems; }
            set { galleryDocumentItems = value; }
        }

        
        [GnosisCollection]
        public List<GnosisGallerySearchItem> GallerySearchItems
        {
            get { return gallerySearchItems; }
            set { gallerySearchItems = value; }
        }


      

    }
}
