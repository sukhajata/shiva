using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ShivaShared3.Events;
using ShivaShared3.Data;
using GnosisControls;
using ShivaShared3.ContainerControllers;
using ShivaShared3.BaseControllers;
using ShivaShared3.Interfaces;

namespace ShivaShared3.Utility
{
    public class LayoutManager
    {
        private int lastVisibleTileOrder;

      

        public void AddTile(GnosisTileController currentTileController, GnosisController.TilePosition newTilePosition)
        {
            //Create new sibling Tile and new parent Split
            //currentTile becomes a child of the new parent

            //new parent split
            GnosisSplit newParentSplit = GnosisControlCreator.CreateGnosisSplit(currentTileController.ControlImplementation.Order);
            GnosisSplitController newParentSplitController = new GnosisSplitController(newParentSplit, currentTileController.EntityController, currentTileController.Parent);

            //new split detail
            GnosisSplitDetail splitDetail = new GnosisSplitDetail();
            splitDetail.ID = GlobalData.Singleton.GetNewControlID();
            splitDetail.Order = 0;
            splitDetail._GnosisOrientation = GnosisController.OrientationType.LANDSCAPE;
            splitDetail.SplitPercentage = 50;
            if (newTilePosition == GnosisController.TilePosition.TOP || newTilePosition == GnosisController.TilePosition.BOTTOM)
            {
                splitDetail._SplitDirection = GnosisController.DirectionType.HORIZONTAL;
            }
            else
            {
                splitDetail._SplitDirection = GnosisController.DirectionType.VERTICAL;
            }
            newParentSplit.SplitDetails = new List<GnosisSplitDetail>();
            newParentSplit.SplitDetails.Add(splitDetail);
            //newParentSplit.GnosisSplitDetails = new GnosisSplitDetail[2];
            //newParentSplit.GnosisSplitDetails[0] = splitDetail;

            //new sibling tile
            GnosisTile newSiblingTile = new GnosisTile();
            newSiblingTile.ID = GlobalData.Singleton.GetNewControlID();

            AssignNamesAndOrders(newParentSplit, splitDetail, currentTileController, newSiblingTile, newTilePosition);

            //store old parent
            GnosisSplitController oldParent = (GnosisSplitController)currentTileController.Parent;

            //remove current tile from old parent
            oldParent.RemoveChild(currentTileController);

            //add current tile and new sibling to new parent
            newParentSplitController.AddChildModel(newSiblingTile);
            newParentSplitController.AddChildController(currentTileController);
            newParentSplitController.BuildContent();

            //add new parent to old parent
            oldParent.AddChildController(newParentSplitController);

            oldParent.BuildContent();

            //GnosisParentWindow parentWindow = (GnosisParentWindow)GlobalData.Singleton.ParentWindowController.ControlImplementation;
            //GnosisXMLHelper.SaveParentWindow(parentWindow);

            //newParentSplit.GnosisTiles = new GnosisTile[2];
            //if (currentTileController.ControlImplementation.Order == 1)
            //{
            //    newParentSplit.GnosisTiles[0] = (GnosisTile)currentTileController.ControlImplementation;
            //    newParentSplit.GnosisTiles[1] = newSiblingTile;
            //}
            //else
            //{
            //    newParentSplit.GnosisTiles[0] = newSiblingTile;
            //    newParentSplit.GnosisTiles[1] = (GnosisTile)currentTileController.ControlImplementation; 
            //}


            ////remove current tile from old parent and replace it with new split
            //GnosisSplit oldParent = (GnosisSplit)currentTileController.ParentSplitController.ControlImplementation;
            //oldParent.RemoveTile((GnosisTile)currentTileController.ControlImplementation);
            //oldParent.AddSplit(newParentSplit);


            //currentTileController.ParentSplitController.ReplaceChild(currentTileController, newParentSplitController);


            //notify listeners
            //currentParent.Altered = true;

        }

        public void RemoveTile(GnosisTileController tileController)
        {

            //Find sibling and grandparent
            GnosisSplitController parentSplitController = (GnosisSplitController)tileController.Parent;
            var siblingController =
                parentSplitController.ChildControllers.Find(x => x.ControlImplementation.ID != tileController.ControlImplementation.ID);
            GnosisSplitController grandparent = (GnosisSplitController)parentSplitController.Parent;

            //remove tile and sibling from current parent
            parentSplitController.RemoveChild(tileController);
            parentSplitController.RemoveChild(siblingController);

            //assign parent's name and order to sibling
            siblingController.ControlImplementation.Order = parentSplitController.ControlImplementation.Order;
            siblingController.ControlImplementation.GnosisName = parentSplitController.ControlImplementation.GnosisName;

            //replace current parent with the sibling
            grandparent.RemoveChild(parentSplitController);
            grandparent.AddChildController(siblingController);

            //redraw
            grandparent.BuildContent();

            GnosisParentWindow parentWindow = (GnosisParentWindow)GlobalData.Singleton.ParentWindowController.ControlImplementation;
            GnosisXMLHelper.SaveParentWindow(parentWindow);
        }

        
        public GnosisSplit FindParent(IGnosisContainerImplementation child, GnosisSplit currentNode)
        {
            GnosisSplit parent = null;
            if (currentNode.Splits != null)
            {
                while (parent == null)
                { 
                    foreach (GnosisSplit split in currentNode.Splits)
                    {
                        if (split == child)
                        {
                            parent = currentNode;
                        }
                        else
                        {
                            parent = FindParent(child, split);
                        }
                    }
                    break;
                }
            }
            else
            {
                foreach(GnosisTile tile in currentNode.Tiles)
                {
                    if (tile == child)
                    {
                        parent = currentNode;
                        break;
                    }
                }
            }

            return parent;

        }


        public GnosisController.TilePosition GetPosition(int currentChildOrder, GnosisSplitController.DirectionType splitDirection)
        {
            if (splitDirection == GnosisSplitController.DirectionType.VERTICAL)
            {
                if (currentChildOrder == 1)
                {
                    return GnosisController.TilePosition.LEFT;
                }
                else
                {
                    return GnosisController.TilePosition.RIGHT;
                }
            }
            else
            {
                if (currentChildOrder == 1)
                {
                    return GnosisController.TilePosition.TOP;
                }
                else
                {
                    return GnosisController.TilePosition.BOTTOM;
                }
            }
        }

        public void ShowVisibleTileOrder()
        {
            lastVisibleTileOrder = 0;
            ShowVisibleTileOrder(GlobalData.Singleton.PrimarySplitController);
        }



        private void ShowVisibleTileOrder(GnosisContainerController controller)
        {
            if (controller is GnosisSplitController || controller is GnosisPrimarySplitController)
            {
                var orderedChildren = ((GnosisSplitController)controller).ChildControllers.OrderBy(x => x.ControlImplementation.Order);
                foreach (GnosisContainerController childController in orderedChildren)
                {
                    ShowVisibleTileOrder(childController);
                }
            }
            else if (controller is GnosisTileController || controller is GnosisNavTileController)
            {
                ((GnosisTileController)controller).ShowVisibleTileOrder(++lastVisibleTileOrder);
            }
        }

        public void HideVisibleTileOrder()
        {
            HideVisibleTileOrder(GlobalData.Singleton.PrimarySplitController);
        }

        private void HideVisibleTileOrder(GnosisContainerController controller)
        {
            if (controller is GnosisSplitController || controller is GnosisPrimarySplitController)
            {
                foreach (GnosisContainerController childController in ((GnosisSplitController)controller).ChildControllers)
                {
                    HideVisibleTileOrder(childController);
                }
            }
            else if (controller is GnosisTileController || controller is GnosisNavTileController)
            {
                ((GnosisTileController)controller).HideVisibleTileOrder();
            }
        }


        private void AssignNamesAndOrders(GnosisSplit newParentSplit, GnosisSplitDetail splitDetail, 
            GnosisTileController currentTileController, GnosisTile newSibling, GnosisController.TilePosition newTilePosition)
        {
      
            GnosisController.TilePosition currentTilePosition = GetPosition(currentTileController.ControlImplementation.Order, ((GnosisSplitController)currentTileController.Parent).SplitDirection);
            newParentSplit.GnosisName = "Split" + newParentSplit.ID.ToString() + Enum.GetName(typeof(GnosisController.TilePosition),currentTilePosition);
            splitDetail.GnosisName = newParentSplit.GnosisName + "Detail";

            if (newTilePosition == GnosisController.TilePosition.TOP)
            {
                newSibling.GnosisName = newParentSplit.GnosisName + "TopTile";
                newSibling.Order = 1;
                currentTileController.ControlImplementation.GnosisName = newParentSplit.GnosisName + "BottomTile";
                currentTileController.ControlImplementation.Order = 2;
            }
            else if (newTilePosition == GnosisController.TilePosition.BOTTOM)
            {
                newSibling.GnosisName = newParentSplit.GnosisName + "BottomTile";
                newSibling.Order = 2;
                currentTileController.ControlImplementation.GnosisName = newParentSplit.GnosisName + "TopTile";
                currentTileController.ControlImplementation.Order = 1;
            }
            else if (newTilePosition == GnosisController.TilePosition.LEFT)
            {
                newSibling.GnosisName = newParentSplit.GnosisName + "LeftTile";
                newSibling.Order = 1;
                currentTileController.ControlImplementation.GnosisName = newParentSplit.GnosisName + "RightTile";
                currentTileController.ControlImplementation.Order = 2;
            }
            else
            {
                newSibling.GnosisName = newParentSplit.GnosisName + "RightTile";
                newSibling.Order = 2;
                currentTileController.ControlImplementation.GnosisName = newParentSplit.GnosisName + "LeftTile";
                currentTileController.ControlImplementation.Order = 1;
            }
            
        }

        //public void AddTile(GnosisTile currentTile, GnosisSplit currentParent, string newTilePosition)
        //{
        //    //Create new sibling Tile and new parent Split
        //    //currentTile becomes a child of the new parent
        //    GnosisTile newSibling = new GnosisTile();
        //    newSibling.ID = GlobalData.Instance.GetNewControlID();
        //    newSibling.Visible = true;

        //    GnosisSplit newParent = new GnosisSplit();
        //    newParent.ID = GlobalData.Instance.GetNewControlID();
        //    newParent.Visible = true;
        //    newParent.Order = currentTile.Order;

        //    GnosisSplitDetail splitDetail = new GnosisSplitDetail();
        //    splitDetail.ID = GlobalData.Instance.GetNewControlID();
        //    splitDetail.Order = 0;
        //    splitDetail.Orientation = GnosisSplitDetail.ORIENTATION_LANDSCAPE;
        //    if (newTilePosition.Equals(GnosisContainer.POSITION_TOP) || newTilePosition.Equals(GnosisContainer.POSITION_BOTTOM))
        //    {
        //        splitDetail.SplitDirection = GnosisSplitDetail.DIRECTION_HORIZONTAL;
        //    }
        //    else
        //    {
        //        splitDetail.SplitDirection = GnosisSplitDetail.DIRECTION_VERTICAL;
        //    }
        //    splitDetail.SplitPercentage = 50;
        //    newParent.GnosisSplitDetails = new GnosisSplitDetail[2];
        //    newParent.GnosisSplitDetails[0] = splitDetail;

        //    AssignNamesAndOrders(currentParent, newParent, splitDetail, currentTile, newSibling, newTilePosition);

        //    //remove currentTile from currentParent and add newParent
        //    List<GnosisTile> tiles = new List<GnosisTile>(currentParent.GnosisTiles);
        //    tiles.Remove(currentTile);
        //    currentParent.GnosisTiles = tiles.ToArray();

        //    if (currentParent.GnosisSplits == null)
        //    {
        //        currentParent.GnosisSplits = new GnosisSplit[1];
        //        currentParent.GnosisSplits[0] = newParent;
        //    }
        //    else
        //    {
        //        List<GnosisSplit> splits = new List<GnosisSplit>(currentParent.GnosisSplits);
        //        splits.Add(newParent);
        //        currentParent.GnosisSplits = splits.ToArray();
        //    }

        //    //Add tiles to newParent
        //    newParent.GnosisTiles = new GnosisTile[2];
        //    if (currentTile.Order == 1)
        //    {
        //        newParent.GnosisTiles[0] = currentTile;
        //        newParent.GnosisTiles[1] = newSibling;
        //    }
        //    else
        //    {
        //        newParent.GnosisTiles[0] = newSibling;
        //        newParent.GnosisTiles[1] = currentTile;
        //    }

        //    XMLService.GnosisXMLHelper.SaveParentWindow();

        //    //notify listeners
        //    currentParent.Altered = true;

        //}

        //public void AssignVisibleTileOrders()
        //{
        //    lastVisibleTileOrder = 0;
        //    AssignVisibleTileOrder(GlobalData.Instance.ParentWindow.GnosisPrimarySplit[0]);
        //}

        //private void AssignVisibleTileOrder(GnosisContainer container)
        //{
        //    if (container is GnosisSplit || container is GnosisPrimarySplit)
        //    {
        //        List<GnosisContainer> containers = new List<GnosisContainer>();

        //        if (((GnosisSplit)container).GnosisSplits != null)
        //        {
        //            foreach (GnosisSplit split in ((GnosisSplit)container).GnosisSplits)
        //            {
        //                if (split.Visible)
        //                {
        //                    containers.Add(split);
        //                }
        //            }
        //        }
        //        if (((GnosisSplit)container).GnosisTiles != null)
        //        {
        //            foreach(GnosisTile tile in ((GnosisSplit)container).GnosisTiles)
        //            {
        //                if (tile.Visible)
        //                {
        //                    containers.Add(tile);
        //                }
        //            }
        //        }

        //        foreach(GnosisContainer con in containers.OrderBy(x => x.Order))
        //        {
        //            AssignVisibleTileOrder(con);
        //        }
        //    }
        //    else if (container is GnosisTile || container is GnosisNavTile)
        //    {
        //        ((GnosisTile)container).VisibleOrder = ++lastVisibleTileOrder;
        //    }
        //}

        //public void RemoveTile(GnosisTile tile, GnosisSplit parent)
        //{
        //    //Replace the parent with the sibling
        //    //Make sibling a child of the grandparent

        //    //Find the sibling
        //    GnosisContainer sibling = null;
        //    if (parent.GnosisSplits != null && parent.GnosisSplits.Count() > 0)
        //    {
        //        //there should be only one split since there is one tile
        //        if (parent.GnosisSplits.Count() > 1)
        //        {
        //            GlobalData.Instance.ErrorHandler.HandleError("GnosisSplit contains more than two children: " + tile.GnosisName + ", " + 
        //                parent.GnosisSplits.Count().ToString() + " splits", "LayoutManager.RemoveTile");
        //        }
        //        sibling = parent.GnosisSplits[0];
        //    }
        //    else
        //    {
        //        foreach(GnosisTile child in parent.GnosisTiles)
        //        {
        //            if (child != tile)
        //            {
        //                sibling = child;
        //            }
        //        }
        //    }
        //    if(sibling == null)
        //    {
        //        GlobalData.Instance.ErrorHandler.HandleError("Can not remove this panel. No sibling found for GnosisTile", "LayoutManager.RemoveTile()");
        //        return;
        //    }

        //    //Find the grandparent
        //    GnosisPrimarySplit primarySplit = GlobalData.Instance.ParentWindow.GnosisPrimarySplit[0];
        //    GnosisSplit grandparent = FindParent(parent, primarySplit);
        //    if (grandparent == null)
        //    {
        //        GlobalData.Instance.ErrorHandler.HandleError("Grandparent not found", "LayoutManager.RemoveTile");
        //    }
        //    else
        //    {
        //        //Replace parent with sibling
        //        sibling.Order = parent.Order;
        //        List<GnosisSplit> splits = new List<GnosisSplit>(grandparent.GnosisSplits);
        //        splits.Remove(parent);
        //        if (sibling is GnosisSplit)
        //        {
        //            splits.Add((GnosisSplit)sibling);
        //            grandparent.GnosisSplits = splits.ToArray();
        //        }
        //        else
        //        {
        //            grandparent.GnosisSplits = splits.ToArray();
        //            if (grandparent.GnosisTiles == null)
        //            {
        //                grandparent.GnosisTiles = new GnosisTile[1];
        //                grandparent.GnosisTiles[0] = (GnosisTile)sibling;
        //            }
        //            else
        //            {
        //                List<GnosisTile> tiles = new List<GnosisTile>(grandparent.GnosisTiles);
        //                tiles.Add((GnosisTile)sibling);
        //                grandparent.GnosisTiles = tiles.ToArray();
        //            }
        //        }

        //    }

        //    XMLService.GnosisXMLHelper.SaveParentWindow();

        //    //grandparent.Altered = true;

        //}



    }
}
