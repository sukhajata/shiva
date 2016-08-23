using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShivaShared3.ContainerControllers;
using GnosisControls;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.Data;
using ShivaShared3.ContentControllers;
using ShivaShared3.OuterLayoutControllers;
using ShivaShared3.WindowControllers;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.ContainerControllers
{
    public class GnosisPrimarySplitController : GnosisSplitController
    {
        public GnosisNavTileController NavTileController;

        public int SplitUnits
        {
            get { return ((GnosisSplit)ControlImplementation).SplitDetails[0].SplitUnits; }
            //set
            //{
            //    ((GnosisSplit)ControlImplementation).GnosisSplitDetails[0].SplitUnits = value;
            //}
        }

        public bool UsingUnits
        {
            get
            {
               return ((GnosisSplit)ControlImplementation).SplitDetails[0].UsingUnits;
            }
        }
        

        public GnosisPrimarySplitController(
            GnosisPrimarySplit _primarySplit, 
          //  IGnosisPrimarySplitImplementation _primarySplitImplementation,
            GnosisEntityController entityController,
            GnosisParentWindowController parentWindow)
            :base(_primarySplit, entityController, parentWindow)
        {
            ((IGnosisPrimarySplitImplementation)ControlImplementation).SetSplitterMovedHandler(new Action<double, int>(SplitterMoved));
        }


        public void SplitterMoved(double newPercent, int newSplitUnits)
        {
            base.SplitterMoved(newPercent);

            ((GnosisSplit)ControlImplementation).SplitDetails[0].SplitUnits = newSplitUnits;
        }

        protected override void CreateControllers()
        {
            if (((GnosisPrimarySplit)ControlImplementation).GnosisNavTile != null)
            {
                GnosisNavigatorTile navTile = ((GnosisPrimarySplit)ControlImplementation).GnosisNavTile;
               // IGnosisNavTileImplementation navImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisNavTileImplementation();
                GnosisNavTileController navController = new GnosisNavTileController(navTile, EntityController, this);
                
                navController.PropertyChanged += ChildContainer_PropertyChanged;

                ChildControllers.Add(navController);
                NavTileController = navController;
            }

            base.CreateControllers();
        }

        //public void AddNavFrame(GnosisNavFrameController gnosisNavFrameController)
        //{
        //    NavTileController.AddFrame(gnosisNavFrameController);
        //}

        public void LoadSearchFrame(GnosisSearchFrameController searchFrameController)
        {
            GetFirstTile().LoadFrame(searchFrameController);
        }

        public override void BuildContent()
        {
            //Call the implementation to build the split content
            List<GnosisContainer> containerImplementations = new List<GnosisContainer>();
            foreach (GnosisContainerController controller in ChildControllers)
            {
                if (!((IGnosisVisibleControlImplementation)ControlImplementation).Hidden)
                {
                    containerImplementations.Add((GnosisContainer)controller.ControlImplementation);
                }
            }

            if (UsingUnits)
            {
                ((IGnosisPrimarySplitImplementation)ControlImplementation).BuildContentFixedNavWidth(containerImplementations,
                    this.SplitUnits, this.SplitDirection);
            }
            else
            {
                ((IGnosisSplitImplementation)ControlImplementation).BuildContent(containerImplementations,
                    this.SplitPercentage, this.SplitDirection);
            }
        }

        public void ToggleNavigatorVisible()
        {
            ((IGnosisVisibleControlImplementation)NavTileController.ControlImplementation).Hidden = !((IGnosisVisibleControlImplementation)NavTileController.ControlImplementation).Hidden;
            BuildContent();
        }

        //Called by implementation when split percentage is changed in properties dialog.
        //Update the model and instruct the view to move the splitter.
        //Get the new split units and update the model
        public override void ChangeSplitPercentage(double newPercent)
        {
            base.ChangeSplitPercentage(newPercent);

            int newUnits = ((IGnosisPrimarySplitImplementation)ControlImplementation).SetSplitterPercent(newPercent);
            ((GnosisSplit)ControlImplementation).SplitDetails[0].SplitUnits = newUnits;
        }

        //Called by implementation when split units are changed in properties dialog.
        //Update the model and instruct the view to move the splitter.
        //Get the new split percentage and update the model
        public void ChangeSplitUnits(int newUnits)
        {
            ((GnosisSplit)ControlImplementation).SplitDetails[0].SplitUnits = newUnits;
            double newPercentage = ((IGnosisPrimarySplitImplementation)ControlImplementation).SetSplitterUnits(newUnits);
            ((GnosisSplit)ControlImplementation).SplitDetails[0].SplitPercentage = newPercentage;
        }


        public void ChangeUsingUnits(bool useUnits)
        {
            ((GnosisSplit)ControlImplementation).SplitDetails[0].UsingUnits = useUnits;

            BuildContent();
        }

        public override GnosisController FindControllerByID(int controlID)
        {
            if (this.ControlImplementation.ID == controlID)
            {
                return this;
            }

            if (NavTileController.ControlImplementation.ID == controlID)
            {
                return NavTileController;
            }
            else
            {
                return base.FindControllerByID(controlID);
            }
        }


        public GnosisTileController GetFirstTile()
        {
            GnosisContainerController firstChild = ChildControllers.OrderBy(x => x.ControlImplementation.Order).Where(x => !(x is GnosisNavTileController)).First();
            while (firstChild is GnosisSplitController)
            {
                firstChild = ((GnosisSplitController)firstChild).ChildControllers.OrderBy(x => x.ControlImplementation.Order).First();
            }

            return (GnosisTileController)firstChild;
        }

        internal void DisplayLoadingProgress()
        {
            GetFirstTile().DisplayLoadingAnimation();
        }


        //private void NavController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("Visible"))
        //    {
        //        // ((IGnosisPrimarySplitImplementation)ControlImplementation).ResizeNavWidth();
        //        BuildContent();
        //    }
        //}

        //protected override void SplitDetailController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals(GnosisSplitDetail.SPLIT_UNITS_ATTRIBUTE_NAME))
        //    {
        //        ((IGnosisPrimarySplitImplementation)ControlImplementation).ResizeNavWidth();
        //    }
        //    else if (e.PropertyName.Equals(GnosisSplitDetail.SPLIT_PERCENTAGE_ATTRIBUTE_NAME))
        //    {
        //        ((IGnosisSplitImplementation)ControlImplementation).OnSplitPercentageChanged(this.SplitDetailController.SplitPercentage, this.SplitDetailController.SplitDirection);
        //    }
        //    else
        //    {
        //        base.SplitDetailController_PropertyChanged(sender, e);
        //    }
        //}


    }
}
