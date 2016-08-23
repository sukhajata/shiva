using ShivaShared3.ContainerControllers;
using GnosisControls;
using ShivaShared3.ContentControllers;
using ShivaShared3.Data;
using ShivaShared3.DataControllers;
using ShivaShared3.Interfaces;
using ShivaShared3.ToolbarControllers;
using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.OuterLayoutControllers;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.WindowControllers
{
    public class GnosisParentWindowController : GnosisVisibleController
    {

        public List<GnosisToolbarTrayController> ToolbarTrayControllers; 

        public GnosisPrimarySplit PrimarySplit
        {
            get
            {
                return ((GnosisParentWindow)ControlImplementation).PrimarySplit;
            }
        }

        //public string Device
        //{
        //    get
        //    {
        //        return ((GnosisParentWindow)ControlImplementation).Device;
        //    }
        //    //set
        //    //{
        //    //    ((GnosisParentWindow)ControlImplementation).Device = value;
        //    //}
        //}



        //public string Orientation
        //{
        //    get
        //    {
        //        return ((GnosisParentWindow)ControlImplementation).Orientation;
        //    }
        //    set
        //    {
        //        if (value.Equals(GnosisSplitDetail.ORIENTATION_LANDSCAPE) || value.Equals(GnosisSplitDetail.ORIENTATION_PORTRAIT))
        //        {
        //            ((GnosisParentWindow)ControlImplementation).Orientation = value;
        //            OnPropertyChanged("Orientation");
        //        }
        //        else
        //        {
        //            GlobalData.Instance.ErrorHandler.HandleError("Unrecognised orientation: " + value, "GnosisParentWindowController.Orientation.Set");
        //        }
        //    }
        //}

        //public bool Visible
        //{
        //    get
        //    {
        //        return ((GnosisParentWindow)ControlImplementation).Visible;
        //    }
        //    //set
        //    //{
        //    //    ((GnosisParentWindow)ControlImplementation).Visible = value;
        //    //    OnPropertyChanged("Visible");
        //    //}
        //}


        public GnosisParentWindowController(
            GnosisParentWindow _parentWindow, 
           // IGnosisParentWindowImplementation _parentWindowImplementation,
            GnosisEntityController _entityController)
            : base(_parentWindow, _entityController, null)
        {
            GlobalData.Singleton.ParentWindowController = this;

        }


        public void Setup()
        {
            ((IGnosisParentWindowImplementation)ControlImplementation).SetLoadedHandler(new Action<double>(WindowLoaded));

            //toolbars
            ToolbarTrayControllers = new List<GnosisToolbarTrayController>();

            foreach (GnosisToolbarTray toolbarTray in ((GnosisParentWindow)ControlImplementation).ToolbarTrays)
            {
                if (toolbarTray._GnosisOrientation == GlobalData.Singleton.AppOrientation)
                {
                   // IGnosisToolbarTrayImplementation toolBarTrayImplementation =
                     //   GlobalData.Singleton.ImplementationCreator.GetGnosisToolbarTrayImplementation();
                    GnosisToolbarTrayController toolbarTrayController = new GnosisToolbarTrayController(toolbarTray, EntityController, this);
                    toolbarTrayController.Setup();

                    ToolbarTrayControllers.Add(toolbarTrayController);
                }
            }

            ////tooltips enabled?
            if (GlobalData.Singleton.SystemController.ShowTooltips)
            {
                foreach (GnosisToolbarTrayController toolbarTrayController in ToolbarTrayControllers)
                {
                    toolbarTrayController.ShowTooltips();// GlobalData.Instance.Connection.ShowTooltips);
                }
            }

            //primary split
          //  IGnosisPrimarySplitImplementation primarySplitImplementation =
            //    GlobalData.Singleton.ImplementationCreator.GetGnosisPrimarySplitImplementation();
            GnosisPrimarySplitController primarySplitController = new GnosisPrimarySplitController(((GnosisParentWindow)ControlImplementation).PrimarySplit,
                EntityController, this);
            GlobalData.Singleton.PrimarySplitController = primarySplitController;
            primarySplitController.Setup();

            //styles
            GlobalData.Singleton.StyleHelper.ApplyStyle(((GnosisParentWindow)ControlImplementation).PrimarySplit, EntityController.GetNormalStyle());

        }

        public void WindowLoaded(double width)
        {
            //foreach (GnosisToolbarTrayController toolbarTrayController in ToolbarTrayControllers)
            //{
            //    ((IGnosisParentWindowImplementation)ControlImplementation).LoadToolbarTray((IGnosisToolbarTrayImplementation)toolbarTrayController.ControlImplementation);
            //}

            //((IGnosisParentWindowImplementation)ControlImplementation).LoadPrimarySplit((IGnosisPrimarySplitImplementation)GlobalData.Singleton.PrimarySplitController.ControlImplementation);

        }

        public GnosisController FindControllerByID(int controlID)
        {
            if (this.ControlImplementation.ID == controlID)
            {
                return this;
            }


            //foreach (GnosisToolbarTrayController toolbarTrayController in ToolbarTrayControllers)
            //{
            //    GnosisContentControlController controller = toolbarTrayController.FindControllerByID(controlID);

            //    if (controller != null)
            //    {
            //        return controller;
            //    }
            //}

            return GlobalData.Singleton.PrimarySplitController.FindControllerByID(controlID);

        }


        public override void LoadData()
        {
            GlobalData.Singleton.PrimarySplitController.LoadData();
        }

        internal override void ShowTooltips()
        {
            base.ShowTooltips();

            foreach (GnosisToolbarTrayController trayController in ToolbarTrayControllers)
            {
                trayController.ShowTooltips();
            }
        }

        internal override void HideTooltips()
        {
            base.HideTooltips();

            foreach (GnosisToolbarTrayController trayController in ToolbarTrayControllers)
            {
                trayController.HideTooltips();
            }
        }

    }
}
