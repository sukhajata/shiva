
using System;


namespace GnosisControls
{
    public class GnosisGallerySearchAttribute : GnosisControl
    {
        private string datasetItem;

        private string dataset;

        private bool hasFocus;

        private bool hasMouseFocus;

        private bool hasMouseDown;

        private int galleryID;

        private string galleryRole;

        [GnosisProperty]
        public string DatasetItem
        {
            get { return datasetItem; }
            set { datasetItem = value; }
        }

        [GnosisProperty]
        public string Dataset
        {
            get { return dataset; }
            set { dataset = value; }
        }

        
        public bool HasFocus
        {
            get { return hasFocus; }
            set { hasFocus = value; }
        }

        
        public bool HasMouseDown
        {
            get { return hasMouseDown; }
            set { hasMouseDown = value; }
        }

        
        public bool HasMouseFocus
        {
            get { return hasMouseFocus; }
            set { hasMouseFocus = value; }
        }


        [GnosisProperty]
        public int GalleryID
        {
            get { return galleryID; }
            set { galleryID = value; }
        }

        [GnosisProperty]
        public string GalleryRole
        {
            get { return galleryRole; }
            set { galleryRole = value; }
        }

    }
}
