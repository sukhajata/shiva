using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Utility;
using Shiva.Shared.OuterLayoutControllers;
using Shiva.Shared.BaseControllers;



namespace Shiva.Shared.ContainerControllers
{
    public class GnosisTileController : GnosisContainerController, IGnosisFrameContainer
    {
        public static string CURRENT_FRAME_PROPERTY_NAME = "CurrentFrame";

        private GnosisTileTabController tabController;
        private List<GnosisFrameController> frameControllers;
        private GnosisFrameController currentFrameController;
      //  private bool loaded;

        public GnosisFrameController CurrentFrameController
        {
            get { return currentFrameController; }
            //set
            //{
            //    currentFrame = value;
            //    OnPropertyChanged("CurrentFrame");
            //}
        }


        //public bool HasTabs
        //{
        //    get
        //    {
        //        return ((GnosisTile)ControlImplementation).HasTabs;
        //    }
        //    set
        //    {
        //        ((GnosisTile)ControlImplementation).HasTabs = value;
        //        OnPropertyChanged("HasTabs");
        //    }
        //}

        //public bool AcceptsSearchFrames
        //{
        //    get { return ((GnosisTile)ControlImplementation).AcceptsSearchFrames; }
        //}

        //public bool AcceptsDocumentFrames
        //{
        //    get { return ((GnosisTile)ControlImplementation).AcceptsDocumentFrames; }
        //}

        public int VisibleTileOrder
        {
            get; set;
        }


        public GnosisTileController(
            GnosisTile _gnosisTile, 
           // IGnosisTileImplemenation _tileImplementation,
            GnosisEntityController entityController,
            GnosisSplitController _parentSplitController)
            : base(_gnosisTile, entityController, _parentSplitController)
        {

            this.PropertyChanged += GnosisTileController_PropertyChanged;
            frameControllers = new List<GnosisFrameController>();
            _gnosisTile.SetLoadedHandler(new Action<double>(TileLoaded));

          

        }

        public void TileLoaded(double width)
        {
           // loaded = true;

            if (((GnosisTile)ControlImplementation).HasTabs)
            {
                CreateTabs();
            }
            else if (frameControllers.Count > 0)
            {
                if (currentFrameController == null)
                {
                    currentFrameController = frameControllers[frameControllers.Count - 1];
                }
                ((GnosisTile)ControlImplementation).LoadFrameImplementation((IGnosisFrameImplementation)currentFrameController.ControlImplementation);
            }

        }

        public override void LoadData()
        {
            if (tabController != null)
            {
                tabController.LoadData();
            }
            else
            {
                foreach (GnosisFrameController frameController in frameControllers)
                {
                    frameController.LoadData();
                }
            }
        }

        //private void LoadFrame()
        //{
        //    if (frameControllers.Count > 0)
        //    {
        //        GnosisFrameController lastFrame = frameControllers[frameControllers.Count - 1];
        //        ((IGnosisTileImplemenation)ControlImplementation).LoadFrameImplementation((IGnosisFrameImplementation)lastFrame.ControlImplementation);
        //    }
        //}

        private void CreateTabs()
        {
            //create a tab controller
           // IGnosisTileTabImplementation tabImp = GlobalData.Singleton.ImplementationCreator.GetGnosisTabImplementation();

            GnosisTileTab tab = GnosisControlCreator.CreateGnosisTab();
            tabController = new GnosisTileTabController(tab, EntityController, this);
            tabController.Setup();

            //pass the frames to the tab controller
            foreach (GnosisFrameController frameController in frameControllers)
            {
                tabController.AddFrameController(frameController);
            }

            //update the UI
            ((GnosisTile)ControlImplementation).LoadTabImplementation((IGnosisTileTabImplementation)tabController.ControlImplementation);

            frameControllers = new List<GnosisFrameController>();
            currentFrameController = null;
        }


        private void RemoveTabs()
        {
            frameControllers = tabController.GetFrameControllers();
            currentFrameController = tabController.GetCurrentFrameController();
            tabController = null;

            ((GnosisTile)ControlImplementation).LoadFrameImplementation((IGnosisFrameImplementation)currentFrameController.ControlImplementation);
            
            
        }

        //public double GetWidth()
        //{
        //    return ((IGnosisTileImplemenation)ControlImplementation).GetWidth();
        //}
        //public void AddFrame(GnosisFrameController frameController)
        //{
        //    frameControllers.Add(frameController);

        //    if (loaded)
        //    {
        //        LoadFrame();
        //    }
        //}


        public void LoadFrame(GnosisFrameController frame)
        {

            if (tabController != null)
            {
                tabController.AddFrameController(frame);
            }
            else
            {
                frameControllers.Add(frame);
                currentFrameController = frame;

                ((GnosisTile)ControlImplementation).LoadFrameImplementation((IGnosisFrameImplementation)frame.ControlImplementation);
            }
        }

        //public void RemoveFrame(GnosisFrameController frameController)
        //{
        //    ((IGnosisTileImplemenation)ControlImplementation).RemoveFrameImplementation((IGnosisFrameImplementation)frameController.ControlImplementation);
        //    frameControllers.Remove(frameController);

        //}

        internal override void RemoveFrame(GnosisFrameController frameController)
        {
            ((IGnosisTileImplemenation)ControlImplementation).RemoveFrameImplementation((IGnosisFrameImplementation)frameController.ControlImplementation);
            frameControllers.Remove(frameController);
        }

        internal override void SizeChanged()
        {
            if (frameControllers.Count > 0)
            {
                foreach (var child in frameControllers)
                {
                    child.SizeChanged();
                }
            }
            else if (tabController != null)
            {
                tabController.SizeChanged();
            }
        }



        public void ShowVisibleTileOrder(int visibleOrder)
        {
            VisibleTileOrder = visibleOrder;
            ((IGnosisTileImplemenation)ControlImplementation).ShowVisibleTileOrder(visibleOrder);
        }

        internal void DisplayLoadingAnimation()
        {
            string barColour = EntityController.GetNormalStyle().BackgroundColour;

            if (tabController != null)
            {
                tabController.DisplayLoadingAnimation(barColour);
            }
            else
            {
                ((IGnosisTileImplemenation)ControlImplementation).DisplayLoadingAnimation(barColour);
            }
        }

        internal void HideLoadingAnimation()
        {
            if (tabController != null)
            {
                tabController.HideLoadingAnimation();
            }
            else
            {
                ((IGnosisTileImplemenation)ControlImplementation).HideLoadingAnimation();
            }
        }

        public void HideVisibleTileOrder()
        {
            ((IGnosisTileImplemenation)ControlImplementation).HideVisibleTileOrder();
        }

        public void GotTileFocus()
        {
            GlobalData.Singleton.CurrentTileController = this;
            //GnosisDocFrame docFrame = GnosisXMLHelper.LoadGnosisDocFrame(GlobalData.Instance.IOHelper.GetStreamReader("xml_DocFrame.xml"));
            //AddFrame(docFrame);
        }

        public void LostTileFocus()
        {
            GlobalData.Singleton.CurrentTileController = null;
        }

        public void NavigateBack()
        {
            if (((GnosisTile)ControlImplementation).HasTabs)
            {
                tabController.NavigateBack();
            }
            else
            {
                int currentIndex = frameControllers.FindIndex(x => x == currentFrameController);

                if (currentIndex > 0 && frameControllers[currentIndex - 1] != null)
                {
                    currentFrameController = frameControllers[currentIndex - 1];
                    ((IGnosisTileImplemenation)ControlImplementation).LoadFrameImplementation((IGnosisFrameImplementation)currentFrameController.ControlImplementation);
                }
                else
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("No frame to navigate to", "GNosisTileController.NavigateBack");
                }
            }

        }

        public override GnosisController FindControllerByID(int controlID)
        {
            if (this.ControlImplementation.ID == controlID)
            {
                return this;
            }

            return null;
        }

        public void NavigateForward()
        {
            if (((GnosisTile)ControlImplementation).HasTabs)
            {
                tabController.NavigateForward();
            }
            else
            {
                int currentIndex = frameControllers.FindIndex(x => x == currentFrameController);

                if (frameControllers.Count > currentIndex + 1 && frameControllers[currentIndex + 1] != null)
                {
                    currentFrameController = frameControllers[currentIndex + 1];
                    ((IGnosisTileImplemenation)ControlImplementation).LoadFrameImplementation((IGnosisFrameImplementation)currentFrameController.ControlImplementation);
                    
                }
                else
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("No frame to navigate to", "GnosisTileController.NavigateForward");
                }
            }

        }

        private void GnosisTileController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName.Equals("HasTabs"))
            //{
               
            //    if (HasTabs)
            //    {
            //        CreateTabs();
            //    }
            //    else
            //    {
            //        RemoveTabs();
            //    }
                
            //}
            //else
            //{
            //    GlobalData.Instance.ErrorHandler.HandleError("Property changed: " + e.PropertyName, "GnosisTileController.PropertyChanged");
            //}
        }

        //public void SetCaption(string caption)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
