using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisGalleryDocumentItem : GnosisGalleryItem
    {
        private string documentAction;

        private int documentSystemID;

        private int documentEntityID;

        private List<GnosisGalleryAttribute> galleryAttributes;


        [GnosisProperty]
        public string DocumentAction
        {
            get { return documentAction; }
            set { documentAction = value; }
        }

        [GnosisProperty]
        public int DocumentSystemID
        {
            get { return documentSystemID; }
            set { documentSystemID = value; }
        }

        [GnosisProperty]
        public int DocumentEntityID
        {
            get { return documentEntityID; }
            set { documentEntityID = value; }
        }

        [GnosisCollection]
        public List<GnosisGalleryAttribute> GalleryAttributes
        {
            get { return galleryAttributes; }
            set { galleryAttributes = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisGalleryAttribute)
            {
                if (galleryAttributes == null)
                {
                    galleryAttributes = new List<GnosisGalleryAttribute>();
                }
                galleryAttributes.Add((GnosisGalleryAttribute)child);

            }
            else
            {
                base.GnosisAddChild(child);
            }

        }

    }
}
