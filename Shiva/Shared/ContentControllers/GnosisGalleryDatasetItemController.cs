using ShivaShared3.ContainerControllers;
using GnosisControls;
using ShivaShared3.Data;
using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;
using ShivaShared3.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ShivaShared3.ContentControllers
{
    public class GnosisGalleryDatasetItemController
    {
        private GnosisGalleryDatasetItem galleryDatasetItem;
        private GnosisInstanceController instanceController;
        private GnosisGalleryItemController parent;

        public GnosisGalleryDatasetItemController(
            GnosisGalleryDatasetItem _galleryDatasetItem,
            GnosisInstanceController _instanceController,
            GnosisGalleryItemController _parent)
        {
            galleryDatasetItem = _galleryDatasetItem;
            instanceController = _instanceController;
            parent = _parent;
        }

        public void LoadData(GnosisInstance instance, bool expanded)
        {
           
            //Get the data rows from the instance
            IEnumerable<XElement> dataRows = instanceController.GetDataRows(galleryDatasetItem.Dataset);

            //Find the attribute to use for the caption
            GnosisGallerySearchAttribute captionGalleryAttribute =  galleryDatasetItem.GalleryAttributes.Where(a => a.GalleryRole.Equals("Caption")).First();
            string captionAttributeName = instanceController.GetTargetAttributeName(captionGalleryAttribute.Dataset, captionGalleryAttribute.DatasetItem);
            int order = 1;
            int index = 0;

            foreach (var row in dataRows)
            {
                GnosisGalleryItem galleryItem = GnosisControlCreator.CreateGnosisGalleryItem(order++, expanded);
               // IGnosisGalleryItemImplementation galleryItemImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisGalleryItemImplementation();
                GnosisGalleryItemController itemController = new GnosisGalleryItemController(galleryItem, instanceController, parent);
                itemController.Setup();

                galleryItem.Caption = row.Attribute(captionAttributeName).Value;

                foreach (GnosisGallerySearchItem searchItem in galleryDatasetItem.GallerySearchItems)
                {
                    GnosisGallerySearchItem searchItemClone = GnosisControlCreator.CreateGnosisGallerySearchItem(searchItem);
                   // IGnosisGalleryItemImplementation childImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisGalleryItemImplementation();
                    GnosisGallerySearchItemController childController = new GnosisGallerySearchItemController(searchItemClone, instanceController, itemController, index);
                    childController.Setup();

                    itemController.AddGalleryItemController(childController);
                   // galleryItem.GnosisAddChild(searchItemClone);
                    //galleryItemImplementation.AddGalleryItem(childImplementation);

                }

                foreach (GnosisGalleryDocumentItem docItem in galleryDatasetItem.GalleryDocumentItems)
                {
                    // IGnosisGalleryItemImplementation childImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisGalleryItemImplementation();
                    GnosisGalleryDocumentItem docItemClone = GnosisControlCreator.CreateGnosisGalleryDocumentItem(docItem);
                    GnosisGalleryDocumentItemController childController = new GnosisGalleryDocumentItemController(docItemClone, instanceController, itemController, index);
                    childController.Setup();

                    itemController.AddGalleryItemController(childController);
                  //  galleryItem.GnosisAddChild(docItemClone);
                }

               // itemController.LoadGalleryItems();

                index++;
                parent.AddGalleryItemController(itemController);
              //  ((GnosisGalleryItem)parent.ControlImplementation).GnosisAddChild(itemController.ControlImplementation);
             //   ((IGnosisGalleryImplementation)parent.ControlImplementation).AddGalleryItem(galleryItemImplementation);
            }

           // parent.LoadGalleryItems();

        }

    }
}
