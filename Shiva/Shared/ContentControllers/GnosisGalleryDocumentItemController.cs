using GnosisControls;
using ShivaShared3.Data;
using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.ContentControllers
{
    public class GnosisGalleryDocumentItemController : GnosisGalleryItemController
    {
        private int index;

        public GnosisGalleryDocumentItemController(
          GnosisGalleryDocumentItem searchItem,
       //   IGnosisGalleryItemImplementation searchItemImplementation,
          GnosisInstanceController instanceController,
          GnosisGalleryItemController parent,
          int _index)
            :base(searchItem, instanceController, parent)
        {
            index = _index;
        }


        public override void ItemSelected()
        {
            int docEntityID = ((GnosisGalleryDocumentItem)ControlImplementation).DocumentEntityID;
            int docSystemID = ((GnosisGalleryDocumentItem)ControlImplementation).DocumentSystemID;
            string docAction = ((GnosisGalleryDocumentItem)ControlImplementation).DocumentAction;

            Dictionary<int, string> keys = new Dictionary<int, string>();
            foreach (GnosisGalleryAttribute attribute in ((GnosisGalleryDocumentItem)ControlImplementation).GalleryAttributes)
            {
                switch (attribute._LinkRole)
                {
                    case GnosisGalleryAttribute.GalleryAttributeRole.KEY:
                        keys.Add(attribute.LinkKeyOrder, InstanceController.GetDataString(attribute.Dataset, attribute.DatasetItem, index));
                        break;
                }
            }

            //GnosisEntityController target = GlobalData.Singleton.SystemController.GetEntityController(docEntityID, docSystemID);
            //GnosisInstance instance = GlobalData.Singleton.SystemController.GetInstance(docEntityID, docSystemID, docAction, keys);
            //target.Instance = instance;

            GlobalData.Singleton.SystemController.LoadDocument(docEntityID, docSystemID, docAction, keys);
        }

    }
}
