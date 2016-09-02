using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using Shiva.Shared.ContainerControllers;
using GnosisControls;
using Shiva.Shared.OuterLayoutControllers;
using Shiva.Shared.Data;
using Shiva.Shared.PanelFieldControllers;
using Shiva.Shared.Utility;

namespace Shiva.Shared.ContainerControllers
{
    public class GnosisTileTabItemController : GnosisContainerController, IGnosisFrameContainer
    {
        private List<GnosisFrameController> frameControllers;
        private GnosisFrameController currentFrameController;
        private bool loaded;
        private bool showLoadingAnimation;
        private string animationColour;
        private bool isDummyTabItem;

        public bool IsDummyTabItem
        {
            get { return isDummyTabItem; }
            set { isDummyTabItem = value; }
        }
       
  

        public GnosisTileTabItemController (
            GnosisTileTabItem tabItem, 
          //  IGnosisTileTabItemImplementation tabItemImplementation,
            GnosisEntityController entityController,
            GnosisTileTabController parent)
            :base(tabItem, entityController, parent)
        {
            frameControllers = new List<GnosisFrameController>();
            tabItem.SetLoadedHandler(new Action<double>(TileTabItemLoaded));
            tabItem.SetCloseHandler(new Action(OnTileTabItemClose));
            
        }

        internal void Clear()
        {
            ((IGnosisTileTabItemImplementation)ControlImplementation).Clear();
            frameControllers = new List<GnosisFrameController>();
            currentFrameController = null;
        }

        public void OnTileTabItemClose()
        {
            ((GnosisTileTabController)Parent).CloseTileTab(this);
        }

        public void TileTabItemLoaded(double width)
        {
            loaded = true;

            if (currentFrameController != null)
            {
                ((IGnosisTileTabItemImplementation)ControlImplementation).LoadFrame((IGnosisFrameImplementation)currentFrameController.ControlImplementation, currentFrameController.TabHeaderButton);
                showLoadingAnimation = false;
            }
            else if (showLoadingAnimation)
            {
                ((IGnosisTileTabItemImplementation)ControlImplementation).DisplayLoadingAnimation(animationColour);
            }

        }

        public void LoadFrame(GnosisFrameController frameController)
        {
            showLoadingAnimation = false;

            frameControllers.Add(frameController);
            currentFrameController = frameController;

            ((GnosisTileTabItem)ControlImplementation).Clear();

            //((IGnosisTileTabItemImplementation)ControlImplementation).SetHeader(headerButton);

            string caption = ((IGnosisFrameImplementation)frameController.ControlImplementation).Caption;
            if (frameController.InstanceController != null)
            {
                if (frameController.InstanceController.Instance.InstanceName != null && frameController.InstanceController.Instance.InstanceName.Length > 0)
                {
                    caption += " - " + frameController.InstanceController.Instance.InstanceName;
                }
            }
            ((IGnosisFrameImplementation)frameController.ControlImplementation).Caption = caption;

            //Create a toggle button for the tab item header. Apply the style of the frame to it
            //Use the tile tab item as the binding source for the toggle button so that it changes with mouse over of tab item etc
            GnosisToggleButton headerButton = GnosisControlCreator.CreateGnosisToggleButton(
                this.ControlImplementation.Order,
                caption);
            GlobalData.Singleton.StyleHelper.ApplyStyle(headerButton, frameController.EntityController.GetNormalStyle());
            frameController.TabHeaderButton = headerButton;
         
           

            if (loaded)
            {
                ((IGnosisTileTabItemImplementation)ControlImplementation)
                    .LoadFrame((IGnosisFrameImplementation)currentFrameController.ControlImplementation, headerButton);
            }
        }

        public override void LoadData()
        {
            foreach (GnosisFrameController frameController in frameControllers)
            {
                frameController.LoadData();
            }
        }

        internal override void RemoveFrame(GnosisFrameController frameController)
        {
            if (currentFrameController == frameController)
            {
                currentFrameController = null;
            }

            frameControllers.Remove(frameController);

            ((IGnosisTileTabItemImplementation)ControlImplementation).RemoveFrame((IGnosisFrameImplementation)frameController.ControlImplementation);
        }

        public void NavigateBack()
        {
            int currentIndex = frameControllers.FindIndex(x => x == currentFrameController);

            if (currentIndex > 0 && frameControllers[currentIndex - 1] != null)
            {
                currentFrameController = frameControllers[currentIndex - 1];
                ((IGnosisTileTabItemImplementation)ControlImplementation).LoadFrame((IGnosisFrameImplementation)currentFrameController.ControlImplementation, currentFrameController.TabHeaderButton);
            }

        }

        public void NavigateForward()
        {
            int currentIndex = frameControllers.FindIndex(x => x == currentFrameController);

            if (frameControllers.Count > currentIndex + 1 && frameControllers[currentIndex + 1] != null)
            {
                currentFrameController = frameControllers[currentIndex + 1];
                ((IGnosisTileTabItemImplementation)ControlImplementation).LoadFrame((IGnosisFrameImplementation)currentFrameController.ControlImplementation, currentFrameController.TabHeaderButton);
            }

        }

        public List<GnosisFrameController> GetFrames()
        {
            return frameControllers;
        }

        public GnosisFrameController GetCurrentFrameController()
        {
            return currentFrameController;
        }

        public void SetCaption(string caption)
        {
            ((IGnosisTileTabItemImplementation)ControlImplementation).SetCaption(caption);
        }

        internal override void SizeChanged()
        {
            foreach (var child in frameControllers)
            {
                child.SizeChanged();
            }
        }

        internal void DisplayLoadingAnimation(string colour)
        {
            if (loaded)
            {
                ((IGnosisTileTabItemImplementation)ControlImplementation).DisplayLoadingAnimation(colour);
            }
            else
            {
                showLoadingAnimation = true;
                animationColour = colour;
            }
        }
    }
}
