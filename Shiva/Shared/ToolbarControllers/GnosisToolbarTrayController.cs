using ShivaShared3.BaseControllers;
using ShivaShared3.ContainerControllers;
using ShivaShared3.ContentControllers;
using ShivaShared3.Data;
using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.WindowControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.ToolbarControllers
{
    public class GnosisToolbarTrayController : GnosisVisibleController
    {
        //private GnosisInstanceController instanceController;
        private List<GnosisToolbarController> toolbarControllers;
             
        //public GnosisController.HorizontalAlignmentType TrayHorizontalAlignment
        //{
        //    get { return ((GnosisToolbarTray)ControlImplementation).TrayHorizontalAlignment; }
        //}

        //public GnosisController.OrientationType Orientation
        //{
        //    get { return ((GnosisToolbarTray)ControlImplementation).Orientation; }
        //}

        public GnosisToolbarTrayController (
            GnosisToolbarTray toolbarTray,
           // IGnosisToolbarTrayImplementation _toolbarTrayImplementation,
            GnosisEntityController entityController,
            GnosisParentWindowController parent)
            :base(toolbarTray, entityController, parent)
        {
            toolbarTray.SetHorizontalAlignment(toolbarTray._TrayHorizontalAlignment);
        }

        public void Setup()
        {
            toolbarControllers = new List<GnosisToolbarController>();

            int count = ((GnosisToolbarTray)ControlImplementation).Toolbars.Count;
            int i = 1;
            string splitterColour = EntityController.GetNormalStyle().ContentColour;
            foreach (GnosisToolbar toolbar in ((GnosisToolbarTray)ControlImplementation).Toolbars)
            {
               // IGnosisToolbarImplementation toolbarImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisToolbarImplementation();
                GnosisToolbarController toolbarController = new GnosisToolbarController(toolbar, EntityController, this);
                toolbarController.Setup();

               

               // ((IGnosisToolbarTrayImplementation)ControlImplementation).AddToolbar(toolbar);
                toolbarControllers.Add(toolbarController);
                i++;
            }
        }

        internal override void ShowTooltips()
        {
            base.ShowTooltips();

            foreach (GnosisToolbarController toolbarController in toolbarControllers)
            {
                toolbarController.ShowTooltips();
            }
        }

        internal override void HideTooltips()
        {
            base.HideTooltips();

            foreach (GnosisToolbarController toolbarController in toolbarControllers)
            {
                toolbarController.HideTooltips();
            }
        }
    }
}
