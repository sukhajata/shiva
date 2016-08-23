using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShivaShared3.Interfaces;
using ShivaShared3.ContentControllers;
using ShivaShared3.Data;
using ShivaShared3.WindowControllers;
using ShivaShared3.BaseControllers;
using ShivaShared3.Utility;

namespace ShivaShared3.ContainerControllers
{
    public class GnosisContainerTreeViewItemController
    {
        public GnosisVisibleController SourceController;
        public IGnosisContainerTreeViewItemImplementation TreeViewItemImplementation;

        private string iconName;
        private string caption;

        public GnosisContainerTreeViewItemController(GnosisVisibleController controller)
        {
            SourceController = controller;
            TreeViewItemImplementation = GnosisControlCreator.CreateGnosisContainerTreeViewItem();
            TreeViewItemImplementation.GnosisTag = SourceController;

            SetIconAndCaption();


            //Add child nodes
            if (controller is GnosisParentWindowController)
            {
                GnosisContainerTreeViewItemController primary =
                    new GnosisContainerTreeViewItemController(GlobalData.Singleton.PrimarySplitController);
                TreeViewItemImplementation.AddItem(primary.TreeViewItemImplementation);
            }
            else if (controller is GnosisPrimarySplitController || controller is GnosisSplitController)
            {
                foreach (GnosisContainerController child in ((GnosisSplitController)controller).ChildControllers.OrderBy(x => x.ControlImplementation.Order))
                {
                    GnosisContainerTreeViewItemController childItem = new GnosisContainerTreeViewItemController(child);
                    TreeViewItemImplementation.AddItem(childItem.TreeViewItemImplementation);
                }
            }

        }

        private void SetIconAndCaption()
        {
            if (SourceController is GnosisParentWindowController)
            {
                iconName = "th_icon-nav-window";
                caption = "Parent Window";
            }
            else if (SourceController is GnosisNavTileController)
            {
                iconName = "th_icon-navigator";
                caption = "Navigator";
            }
            else if (SourceController is GnosisTileController)
            {
                caption = "Tile " + ((GnosisTileController)SourceController).VisibleTileOrder.ToString();
                GnosisController.TilePosition position = GlobalData.Singleton.LayoutController.GetPosition(SourceController.ControlImplementation.Order, ((GnosisSplitController)SourceController.Parent).SplitDirection);
                if (position == GnosisController.TilePosition.LEFT)
                {
                    iconName = "th_icon-split-left";
                }
                else if (position == GnosisController.TilePosition.RIGHT)
                {
                    iconName = "th_icon-split-right";
                }
                else if (position == GnosisController.TilePosition.TOP)
                {
                    iconName = "th_icon-split-top";
                }
                else
                {
                    iconName = "th_icon-split-bottom";
                }

            }
            else if (SourceController is GnosisPrimarySplitController)
            {
                caption = "Primary Split";
                iconName = "th_icon-nav-window";
            }
            else if (SourceController is GnosisSplitController)
            {
                caption = "Split";
                GnosisSplitController.DirectionType splitDirection = ((GnosisSplitController)SourceController).SplitDirection;
                if (splitDirection == GnosisSplitController.DirectionType.VERTICAL)
                {
                    iconName = "th_icon-parent-vertical";
                }
                else
                {
                    iconName = "th_icon-parent-horizontal";
                }
            }
            TreeViewItemImplementation.Caption = caption;
            TreeViewItemImplementation.GnosisIcon = iconName;
        }
            


    }
}
