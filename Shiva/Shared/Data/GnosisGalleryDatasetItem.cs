
using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using ShivaShared3.Data;

namespace GnosisControls
{
    public class GnosisGalleryDatasetItem : GnosisVisibleControl
    {

        private string dataset;

        private List<GnosisGalleryItem> galleryItems;

        private List<GnosisGallerySearchAttribute> galleryAttributes;

        private List<GnosisGallerySearchItem> searchItems;

        private List<GnosisGalleryDocumentItem> documentItems;

        [GnosisProperty]
        public string Dataset
        {
            get { return dataset; }
            set { dataset = value; }
        }


        [GnosisCollection]
        public List<GnosisGallerySearchAttribute>  GalleryAttributes
        {
            get { return galleryAttributes; }
            set { galleryAttributes = value; }
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
            get { return searchItems; }
            set { searchItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGalleryDocumentItem> GalleryDocumentItems
        {
            get { return documentItems; }
            set { documentItems = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisGalleryDocumentItem)
            {
                if (documentItems == null)
                {
                    documentItems = new List<GnosisGalleryDocumentItem>();
                }

                documentItems.Add((GnosisGalleryDocumentItem)child);
            }
            else if (child is GnosisGallerySearchItem)
            {
                if (searchItems == null)
                {
                    searchItems = new List<GnosisGallerySearchItem>();
                }

                searchItems.Add((GnosisGallerySearchItem)child);
            }
            else if (child is GnosisGalleryItem)
            {
                if (galleryItems == null)
                {
                    galleryItems = new List<GnosisGalleryItem>();
                }

                galleryItems.Add((GnosisGalleryItem)child);
            }
            else if (child is GnosisGallerySearchAttribute)
            {
                if (galleryAttributes == null)
                {
                    galleryAttributes = new List<GnosisGallerySearchAttribute>();
                }

                galleryAttributes.Add((GnosisGallerySearchAttribute)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unkwown type added to GnosisGalleryDatasetItem: " + child.GetType().ToString(),
                    "GnosisGalleryDatasetItem.GnosisAddChild");
            }
        }

    }
}
