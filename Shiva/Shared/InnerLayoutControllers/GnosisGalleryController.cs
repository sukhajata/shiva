using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.Data;
using GnosisControls;
using Shiva.Shared.OuterLayoutControllers;
using Shiva.Shared.BaseControllers;


namespace Shiva.Shared.InnerLayoutControllers
{
    public class GnosisGalleryController : GnosisInnerLayoutController
    {
        private List<GnosisGalleryItemController> items;
             
        public GnosisGalleryController(
            GnosisGallery _gallery,
           // IGnosisGalleryImplementation _galleryImplementation,
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController parent)
            :base(_gallery, instanceController, parent)
        {
          

        }

        public void Setup()
        {
            items = new List<GnosisGalleryItemController>();

            foreach (GnosisGalleryItem galleryItem in ((GnosisGallery)ControlImplementation).GalleryItems)
            {
                //IGnosisGalleryItemImplementation galleryItemImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisGalleryItemImplementation();
                GnosisGalleryItemController galleryItemController = new GnosisGalleryItemController(galleryItem, InstanceController, this);
                galleryItemController.Setup();
                items.Add(galleryItemController);
                
                if (((GnosisGallery)ControlImplementation).HorizontalSpacing > 0)
                {
                    ((GnosisGallery)ControlImplementation).ApplySpacing();
                }
              //  ((IGnosisGalleryImplementation)ControlImplementation).AddGalleryItem(galleryItem);
             //   ((IGnosisGalleryImplementation)ControlImplementation).AddRootItem(galleryItem);
            }

        }

        //internal void OnItemSelected(GnosisGalleryItemController selected)
        //{
        //    foreach (GnosisGalleryItemController child in items)
        //    {
        //        child.OnItemSelected(selected);
        //    }
        //}

        internal override void ShowTooltips()
        {
            base.ShowTooltips();

            foreach (GnosisGalleryItemController child in items)
            {
                child.ShowTooltips();
            }
        }

        internal override void HideTooltips()
        {
            base.HideTooltips();

            foreach (GnosisGalleryItemController child in items)
            {
                child.HideTooltips();
            }
        }

        internal override GnosisController FindControllerByID(int controlID)
        {
            throw new NotImplementedException();
        }

        internal override void SizeChanged()
        {
            
        }
    }
}
