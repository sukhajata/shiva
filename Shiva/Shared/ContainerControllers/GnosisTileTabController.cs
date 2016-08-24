using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.Data;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Utility;
using Shiva.Shared.ContainerControllers;
using GnosisControls;
using Shiva.Shared.OuterLayoutControllers;

namespace Shiva.Shared.ContainerControllers
{
    public class GnosisTileTabController : GnosisContainerController
    {
        private List<GnosisTileTabItemController> tabItemControllers;
        private GnosisTileTabItemController currentTabItemController;
        private List<IGnosisToggleButtonImplementation> headerButtons;

        public GnosisTileTabController(
            GnosisTileTab tab, 
           // IGnosisTileTabImplementation tabImplementation,
            GnosisEntityController entityController,
            GnosisTileController parent)
            :base(tab, entityController, parent)
        {
            tabItemControllers = new List<GnosisTileTabItemController>();
            tab.SetCloseTabItemHandler(new Action<IGnosisTileTabItemImplementation>(CloseTileTab));
            headerButtons = new List<IGnosisToggleButtonImplementation>();
        }

        public void CloseTileTab(IGnosisTileTabItemImplementation tabItemImp)
        {
            if (tabItemControllers.Count == 1)
            {
                tabItemImp.Clear();
            }
            else
            {
                GnosisTileTabItemController controller = tabItemControllers.Find(c => c.ControlImplementation == tabItemImp);
                tabItemControllers.Remove(controller);
                ((IGnosisTileTabImplementation)ControlImplementation).RemoveTabItem(tabItemImp);
            }
        }

        internal void CloseTileTab(GnosisTileTabItemController gnosisTileTabItemController)
        {
            if (tabItemControllers.Count == 1)
            {
                gnosisTileTabItemController.Clear();
            }
            else
            {
                if (currentTabItemController == gnosisTileTabItemController)
                {
                    currentTabItemController = tabItemControllers[tabItemControllers.Count-1];
                }
                tabItemControllers.Remove(gnosisTileTabItemController);
                ((IGnosisTileTabImplementation)ControlImplementation).RemoveTabItem((IGnosisTileTabItemImplementation)gnosisTileTabItemController.ControlImplementation);
            }

            if (tabItemControllers.Count == 0)
            {
                Setup();
            }
        }

        public void Setup()
        {
            GnosisTileTabItem tabItem = GnosisControlCreator.CreateGnosisTabItem(1);
          //  IGnosisTileTabItemImplementation tabItemImp = GlobalData.Singleton.ImplementationCreator.GetGnosisTabItemImplementation();
            GnosisTileTabItemController itemController = new GnosisTileTabItemController(tabItem, EntityController, this);
            tabItemControllers.Add(itemController);
            itemController.IsDummyTabItem = true;
            itemController.ControlImplementation.Order = 1;

            //IGnosisToggleButtonImplementation headerButton = GlobalData.Singleton.ImplementationCreator.GetGnosisToggleButtonImplementation();
            //GlobalData.Singleton.StyleHelper.ApplyStyle(headerButton, tabItemImp, itemController, frameController.EntityController.GetNormalStyle());
           // headerButton.SetCaption("+");
           // headerButton.Order = 1;
           // tabItemImp.SetHeader(headerButton);

            ((IGnosisTileTabImplementation)ControlImplementation).LoadTabItem(tabItem);
            //headerButtons.Add(headerButton);

            currentTabItemController = tabItemControllers[0];
        }


        public void AddFrameController(GnosisFrameController frameController)
        {
            if (((IGnosisTileTabImplementation)ControlImplementation).CurrentTileTabItem == null)
            {
                if (tabItemControllers.Count == 0)
                {
                    Setup();
                }
                else
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("No tab item selected", "GnosisTileTabController.AddFrameController");
                }
            }

            //Find the selected tab item and pass it the frame
            GnosisTileTabItemController controller = tabItemControllers.Find(i => i.ControlImplementation == ((IGnosisTileTabImplementation)ControlImplementation).CurrentTileTabItem);
            controller.LoadFrame(frameController);
            controller.IsDummyTabItem = false;

            //string caption = frameController.Caption;
            //if (frameController.InstanceController != null)
            //{
            //    if (frameController.InstanceController.Instance.InstanceName != null && frameController.InstanceController.Instance.InstanceName.Length > 0)
            //    {
            //        caption += " - " + frameController.InstanceController.Instance.InstanceName;
            //    }
            //}
            //((IGnosisTileTabItemImplementation)ControlImplementation).SetCaption(caption);
            //((IGnosisTileTabImplementation)ControlImplementation).CurrentTileTabItem.LoadFrame((IGnosisFrameImplementation)frameController.ControlImplementation, caption);

            //check if there is a '+' tab item
            GnosisTileTabItemController dummy = tabItemControllers.Find(i => i.IsDummyTabItem);
            if (dummy == null)
            {
                //create dummy tabitem
                GnosisTileTabItem tabItem = GnosisControlCreator.CreateGnosisTabItem(1);
               // IGnosisTileTabItemImplementation tabItemImp = GlobalData.Singleton.ImplementationCreator.GetGnosisTabItemImplementation();
                GnosisTileTabItemController itemController = new GnosisTileTabItemController(tabItem, EntityController, this);
                itemController.IsDummyTabItem = true;
                itemController.ControlImplementation.Order = tabItemControllers.Count + 1;
                tabItemControllers.Add(itemController);

                GnosisTabHeaderButton headerButton = GnosisControlCreator.CreateGnosisTabHeaderButton(1, "+");
                GlobalData.Singleton.StyleHelper.ApplyStyle(headerButton, frameController.EntityController.GetNormalStyle());
               // headerButton.SetCaption("+");
                headerButton.Order = itemController.ControlImplementation.Order;
                tabItem.SetHeaderButton(headerButton);

                ((IGnosisTileTabImplementation)ControlImplementation).LoadTabItem(tabItem);

                //headerButtons.Add(headerButton);
            }


            

        }


        public override void LoadData()
        {
            foreach (GnosisTileTabItemController tabItemController in tabItemControllers)
            {
                tabItemController.LoadData();
            }
        }

        public void NavigateBack()
        {
            if (currentTabItemController != null)
            {
                currentTabItemController.NavigateBack();
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("No current tab from which to navigate", "GnosisTabController.NavigateBack");
            }
        }

        public List<GnosisFrameController> GetFrameControllers()
        {
            if (currentTabItemController != null)
            {
                return currentTabItemController.GetFrames();
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("no current tab item ", "GnosisTabController.GetCurrentFrames");
                return null;
            }
        }

        public GnosisFrameController GetCurrentFrameController()
        {
            if (currentTabItemController != null)
            {
                return currentTabItemController.GetCurrentFrameController();
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("no current tab item", "GnosisTabController.GetCurrentFrameController");
                return null;
            }
        }

        public void NavigateForward()
        {
            if (currentTabItemController != null)
            {
                currentTabItemController.NavigateForward();
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("No current tab from which to navigate", "GnosisTabController.NavigateForward");
            }
        }

        internal void DisplayLoadingAnimation(string colour)
        {
            if (currentTabItemController == null)
            {
                if (tabItemControllers.Count == 0)
                {
                    Setup();
                }
                else
                {
                    currentTabItemController = tabItemControllers[0];
                }
            }

            currentTabItemController.DisplayLoadingAnimation(colour);

        }

        internal void HideLoadingAnimation()
        {
            if (currentTabItemController != null)
            {
                ((IGnosisTileTabItemImplementation)currentTabItemController.ControlImplementation).HideLoadingAnimation();
            }
        }

        internal override void SizeChanged()
        {
            foreach (var child in tabItemControllers)
            {
                child.SizeChanged();
            }
        }
    }
}
