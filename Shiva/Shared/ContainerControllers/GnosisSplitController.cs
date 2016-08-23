using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GnosisControls;
using ShivaShared3.Data;
using ShivaShared3.ContentControllers;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.ContainerControllers
{
    public class GnosisSplitController : GnosisContainerController
    {
        public List<GnosisContainerController> ChildControllers;

        //Properties
        private GnosisSplitDetail SplitDetail
        {
            get
            {
                if (GlobalData.Singleton.AppOrientation == ((GnosisSplit)ControlImplementation).SplitDetails[0]._GnosisOrientation)
                {
                    return ((GnosisSplit)ControlImplementation).SplitDetails[0];
                }
                else
                {
                    return ((GnosisSplit)ControlImplementation).SplitDetails[1];
                }
            }
        }
        public double SplitPercentage
        {
            get { return SplitDetail.SplitPercentage; }
        }
        public DirectionType SplitDirection
        {
            get { return SplitDetail._SplitDirection; }
        }
        public GnosisController.OrientationType Orientation
        {
            get { return SplitDetail._GnosisOrientation; }
        }


        public GnosisSplitController(
            GnosisSplit split, 
          //  IGnosisSplitImplementation _splitImplementation,
            GnosisEntityController entityController,
            GnosisVisibleController _parent)
            : base(split, entityController, _parent)
        {
            //create split detail controller
            // SplitDetailController = new GnosisSplitDetailController(((GnosisSplit)ControlImplementation).GnosisSplitDetails[0]);
            //SplitDetailController.PropertyChanged += SplitDetailController_PropertyChanged;

            //register to listen for property changes
            //this.PropertyChanged += GnosisSplitController_PropertyChanged;

            ChildControllers = new List<GnosisContainerController>();

            //Handlers
            split.SetSplitterMovedHandler(new Action<double>(SplitterMoved));
            split.SetLoadedHandler(new Action<double>(SplitLoaded));
        }

        public void SplitLoaded(double width)
        {
            BuildContent();
        }

        public void Setup()
        {
            
            CreateControllers();

        }

        public override void LoadData()
        {
            foreach (GnosisContainerController child in ChildControllers)
            {
                child.LoadData();
            }
        }


        public virtual void BuildContent()
        {
            //Call the implementation to build the split content
            List<GnosisContainer> containerImplementations = new List<GnosisContainer>();
            foreach (GnosisContainerController controller in ChildControllers)
            {
                if (!((GnosisContainer)controller.ControlImplementation).Hidden)
                {
                    containerImplementations.Add((GnosisContainer)controller.ControlImplementation);
                }
            }

            ((IGnosisSplitImplementation)ControlImplementation).BuildContent(containerImplementations,
                this.SplitPercentage, this.SplitDirection);
        }

    
        //Called when the split percentage is changed in a properties dialog.
        //Update the model then instruct the view to resize.
        public virtual void ChangeSplitPercentage(double newPercent)
        {
            if (newPercent > 0 && newPercent < 100)
            {
                SplitDetail.SplitPercentage = newPercent;
                ((IGnosisSplitImplementation)ControlImplementation).SetSplitterPercent(newPercent, this.SplitDirection);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Invalid split percentage: " + newPercent, "GnosisSplitController.SplitPercentage.Set");
            }
            
        }

        public void ChangeSplitDirection(GnosisSplitController.DirectionType newDirection)
        {
            SplitDetail._SplitDirection = newDirection;
            BuildContent();
        }
    

        //invoked from implementation
        public virtual void SplitterMoved(double newPercent)
        {
            SplitDetail.SplitPercentage = newPercent;
            SizeChanged();
        }

        internal override void SizeChanged()
        {
            foreach (var child in ChildControllers)
            {
                child.SizeChanged();
            }
        }

        protected virtual void ChildContainer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Visible"))
            {
                //start over
                BuildContent();
            }
        }


        protected virtual void CreateControllers()
        {

            if (((GnosisSplit)ControlImplementation).Splits != null)
            {
                foreach (GnosisSplit split in ((GnosisSplit)ControlImplementation).Splits)
                {
                   // IGnosisSplitImplementation splitImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisSplitImplementation();
                    GnosisSplitController splitController = new GnosisSplitController(split, EntityController, this);
                    splitController.Setup();
                    split.PropertyChanged += ChildContainer_PropertyChanged;

                    ChildControllers.Add(splitController);
                }
            }
            if (((GnosisSplit)ControlImplementation).Tiles != null)
            {
                foreach (GnosisTile tile in ((GnosisSplit)ControlImplementation).Tiles)
                {
                   // IGnosisTileImplemenation tileImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisTileImplementation();
                    GnosisTileController tileController = new GnosisTileController(tile, EntityController, this);
                    tile.PropertyChanged += ChildContainer_PropertyChanged;

                    ChildControllers.Add(tileController);
                }
            }
        }


        public void RemoveChild(GnosisContainerController child)
        {
            if (child is GnosisSplitController)
            {
                //List<GnosisSplit> splits = new List<GnosisSplit>(((GnosisSplit)ControlImplementation).GnosisSplits);
                //splits.Remove((GnosisSplit)child.ControlImplementation);
                //((GnosisSplit)ControlImplementation).GnosisSplits = splits.ToArray();
                ((GnosisSplit)ControlImplementation).Splits.Remove((GnosisSplit)child.ControlImplementation);
            }
            else if (child is GnosisTileController)
            {
                //List<GnosisTile> tiles = new List<GnosisTile>(((GnosisSplit)ControlImplementation).GnosisTiles);
                //tiles.Remove((GnosisTile)child.ControlImplementation);
                //((GnosisSplit)ControlImplementation).GnosisTiles = tiles.ToArray();
                ((GnosisSplit)ControlImplementation).Tiles.Remove((GnosisTile)child.ControlImplementation);
            }
            ((IGnosisSplitImplementation)ControlImplementation).RemoveChild((GnosisContainer)child.ControlImplementation);

            ((GnosisContainer)child.ControlImplementation).PropertyChanged -= ChildContainer_PropertyChanged;
            ChildControllers.Remove(child);
        }


        public void AddChildController(GnosisContainerController childController)
        {
            if (childController is GnosisSplitController)
            {
                if (((GnosisSplit)ControlImplementation).Splits == null)
                {
                    ((GnosisSplit)ControlImplementation).Splits = new List<GnosisSplit>();
                }

                ((GnosisSplit)ControlImplementation).Splits.Add((GnosisSplit)childController.ControlImplementation);
            }
            else if (childController is GnosisTileController)
            {
                if (((GnosisSplit)ControlImplementation).Tiles == null)
                {
                    ((GnosisSplit)ControlImplementation).Tiles = new List<GnosisTile>();
                }
             
                ((GnosisSplit)ControlImplementation).Tiles.Add((GnosisTile)childController.ControlImplementation);
            }

            ((GnosisContainer)childController.ControlImplementation).PropertyChanged += ChildContainer_PropertyChanged;
            childController.Parent = this;
            ChildControllers.Add(childController);
        }

        public void AddChildModel(IGnosisContainerImplementation childModel)
        {
            if (childModel is GnosisSplit)
            {
               // IGnosisSplitImplementation splitImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisSplitImplementation();
                GnosisSplitController splitController = new GnosisSplitController((GnosisSplit)childModel, EntityController, this);
                splitController.Setup();

                AddChildController(splitController);
            }
            else if (childModel is GnosisTile)
            {
               // IGnosisTileImplemenation tileImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisTileImplementation();
                GnosisTileController tileController = new GnosisTileController((GnosisTile)childModel, EntityController, this);

                AddChildController(tileController);
            }
            ((GnosisContainer)childModel).PropertyChanged += ChildContainer_PropertyChanged;
        }

        public override GnosisController FindControllerByID(int controlID)
        {
            if (this.ControlImplementation.ID == controlID)
            {
                return this;
            }

            //if (SplitDetailController.ID == controlID)
            //{
            //    return SplitDetailController;
            //}

            foreach(GnosisContainerController containerController in ChildControllers)
            {
                GnosisContainerController controller = (GnosisContainerController)containerController.FindControllerByID(controlID);
                if (controller != null)
                {
                    return controller;
                }
            }

            return null;
        }

        //protected virtual void GnosisSplitDetails_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("SplitPercentage"))
        //    {
        //        ((IGnosisSplitImplementation)ControlImplementation).OnSplitPercentageChanged(this.SplitPercentage, this.SplitDirection);
        //    }
        //    else if (e.PropertyName.Equals("SplitDirection"))
        //    {
        //        //start over
        //        Setup();
        //    }
        //}

        //protected virtual void SplitDetailController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals(GnosisSplitDetail.SPLIT_DIRECTION_ATTRIBUTE_NAME))
        //    {
        //        BuildContent();
        //    }
        //    else if (e.PropertyName.Equals(GnosisSplitDetail.SPLIT_PERCENTAGE_ATTRIBUTE_NAME))
        //    {
        //        ((IGnosisSplitImplementation)ControlImplementation).OnSplitPercentageChanged(this.SplitDetailController.SplitPercentage, this.SplitDetailController.SplitDirection);
        //    }
        //}

        //protected virtual void GnosisSplitController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("SplitPercentage"))
        //    {
        //        ((IGnosisSplitImplementation)ControlImplementation).OnSplitPercentageChanged(this.SplitDetailController.SplitPercentage, this.SplitDetailController.SplitDirection);
        //    }
        //    else if (e.PropertyName.Equals("SplitDirection"))
        //    {
        //        //start over
        //        Setup();
        //    }
        //    if (e.PropertyName.Equals("Altered"))
        //    {
        //        //start over
        //        Setup();
        //    }
        //}

        //protected virtual void AddContainers()
        //{

        //    if (((GnosisSplit)ControlImplementation).GnosisSplits != null)
        //    {
        //        foreach (GnosisSplit split in ((GnosisSplit)ControlImplementation).GnosisSplits)
        //        {
        //            if (split.Visible)
        //            {
        //                IGnosisSplitImplementation splitImplementation = GlobalData.Instance.ImplementationCreator.GetGnosisSplitImplementation();
        //                GnosisSplitController splitController = new GnosisSplitController(split, splitImplementation);
        //                ChildContainers.Add(split, splitImplementation);
        //            }
        //        }
        //    }
        //    if (((GnosisSplit)ControlImplementation).GnosisTiles != null)
        //    {
        //        foreach (GnosisTile tile in ((GnosisSplit)ControlImplementation).GnosisTiles)
        //        {
        //            if (tile.Visible)
        //            {
        //                IGnosisTileImplemenation tileImplementation = GlobalData.Instance.ImplementationCreator.GetGnosisTileImplementation();
        //                GnosisTileController tileController = new GnosisTileController(tile, tileImplementation);
        //                ChildContainers.Add(tile, tileImplementation);
        //            }
        //        }
        //    }
        //}


    }
}
