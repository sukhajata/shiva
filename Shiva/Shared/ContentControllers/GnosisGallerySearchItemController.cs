using GnosisControls;
using Shiva.Shared.Data;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shiva.Shared.ContentControllers
{
    public class GnosisGallerySearchItemController : GnosisGalleryItemController
    {
        private int index;

        //public string AutoSearchAction
        //{
        //    get { return ((GnosisGallerySearchItem)ControlImplementation).AutoSearchAction; }
        //}

        //public string SearchAction
        //{
        //    get { return ((GnosisGallerySearchItem)ControlImplementation).SearchAction; }
        //}

        public GnosisGallerySearchItemController(
            GnosisGallerySearchItem searchItem,
           // IGnosisGalleryItemImplementation searchItemImplementation,
            GnosisInstanceController instanceController,
            GnosisGalleryItemController parent,
            int _index)
            :base(searchItem, instanceController, parent)
        {
            index = _index;
        }


        public override void ItemSelected()
        {
            //get the values of the parameters
            var searchParameters = ((GnosisGallerySearchItem)ControlImplementation).SearchParameters;
            foreach (GnosisSearchParameter searchParameter in searchParameters)
            {
                searchParameter.Content = InstanceController.GetDataString(searchParameter.Dataset, searchParameter.DatasetItem, index);
            }

            //get the target entity
            int searchEntityID = ((GnosisGallerySearchItem)ControlImplementation).SearchEntityID;
            int searchSystemID = ((GnosisGallerySearchItem)ControlImplementation).SearchSystemID;
            GlobalData.Singleton.SystemController.LoadSearch(
                searchEntityID, 
                searchSystemID, 
                searchParameters,
                ((GnosisGallerySearchItem)ControlImplementation).SearchAction,
                ((GnosisGallerySearchItem)ControlImplementation).AutoSearchAction);
            //GnosisEntityController target = GlobalData.Singleton.SystemController.GetEntityController(searchEntityID, searchSystemID);

            //if (SearchAction.Equals("New"))
            //{
            //    target.LoadSearch(((GnosisGallerySearchItem)ControlImplementation).SearchParameters, AutoSearchAction);

            //}
        }

    }
}
