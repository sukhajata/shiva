using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Data;
using Shiva.Shared.InnerLayoutControllers;
using GnosisControls;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.BaseControllers;

namespace Shiva.Shared.OuterLayoutControllers
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
