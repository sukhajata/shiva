using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.ContentControllers;
using ShivaShared3.Data;
using GnosisControls;
using ShivaShared3.OuterLayoutControllers;
using ShivaShared3.BaseControllers;


namespace ShivaShared3.InnerLayoutControllers
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
