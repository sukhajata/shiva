using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.ContentControllers;
using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;
using ShivaShared3.Data;
using ShivaShared3.InnerLayoutControllers;
using GnosisControls;
using ShivaShared3.ContainerControllers;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.OuterLayoutControllers
{
    public class GnosisNavigatorFrameController : GnosisFrameController
    {
        private List<GnosisGalleryController> galleryControllers;

        public GnosisNavigatorFrameController(
            GnosisNavigatorFrame _navFrame, 
           // IGnosisNavFrameImplementation _navFrameImplementation,
            GnosisInstanceController instanceController,
            GnosisNavTileController parent)
            :base(_navFrame, instanceController, parent)
        {

        }

        public override GnosisController FindControllerByID(int controlID)
        {
            GnosisController controller = null;

            if (((GnosisNavigatorFrame)ControlImplementation).Galleries.Count > 0)
            {
                foreach (GnosisGalleryController galleryController in galleryControllers)
                {
                    controller = galleryController.FindControllerByID(controlID);
                    if (controller != null)
                    {
                        break;
                    }
                }
            }

            return controller;
        }

        public override void Setup()
        {
            base.Setup();

            ((IGnosisNavFrameImplementation)ControlImplementation).SetHorizontalAlignment(HorizontalAlignmentType.STRETCH);

            galleryControllers = new List<GnosisGalleryController>();

            foreach (GnosisGallery gallery in ((GnosisNavigatorFrame)ControlImplementation).Galleries)
            {
               // IGnosisGalleryImplementation galleryImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisGalleryImplementation();
                GnosisGalleryController galleryController = new GnosisGalleryController(gallery, InstanceController, this);
                galleryController.Setup();
                childControllers.Add(galleryController);
                galleryControllers.Add(galleryController);

            }

        }

    }
}
