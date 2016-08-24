using Shiva.Shared.Utility;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.Data;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Shiva.Shared.Interfaces;
using Shiva.Shared.WindowControllers;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.OuterLayoutControllers;
using GnosisControls;

namespace ShivaWPF3.UtilityWPF
{
    /// <summary>
    /// Interaction logic for GnosisSystemPropertiesDialog.xaml
    /// </summary>
    public partial class GnosisSystemPropertiesDialog : Window, IGnosisPropertiesDialogImplementation
    {

        private GnosisContainerController currentController;
        private GnosisContainerTreeViewItem currentItem;
        private GnosisValidator validator;
        private bool assigningValues;

        public GnosisSystemPropertiesDialog()
        {
            InitializeComponent();

            validator = new GnosisValidator();


            List<GnosisConnectionFrameController> connectionFrames = GlobalData.Singleton.SystemController.GetConnectionFrames();
            foreach (GnosisConnectionFrameController frame in connectionFrames)
            {
                LoadConnectionFrame(frame);
            }

            this.Loaded += GnosisSystemPropertiesDialog_Loaded;

        }

        private void GnosisSystemPropertiesDialog_Loaded(object sender, RoutedEventArgs e)
        {
            //display VisibleChildOrder
            GlobalData.Singleton.LayoutController.ShowVisibleTileOrder();

            //Get tree view
            GnosisContainerTreeViewItemController rootController = new GnosisContainerTreeViewItemController(GlobalData.Singleton.ParentWindowController);
            TreeViewItem rootItem = (GnosisContainerTreeViewItem)rootController.TreeViewItemImplementation;
            treeSplits.Items.Add(rootItem);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (currentController != null)
            {
                currentController.UnHighlight();
            }
            GlobalData.Singleton.LayoutController.HideVisibleTileOrder();
        }

        public void LoadConnectionFrame(GnosisConnectionFrameController connectionFrameController)
        {
            TabItem item = new TabItem();
            item.Content = (GnosisConnectionFrame)connectionFrameController.ControlImplementation;
            item.Header = ((GnosisConnectionFrame)connectionFrameController.ControlImplementation).Caption;
            tbConnection.Items.Add(item);
        }

        //Since the TreeView_SelectedItemChanged event fires multiple times if we bring another window into focus
        //we need to assign it background priority
        private delegate void NoArgDelegate();

        private void treeSplits_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background,
                  (NoArgDelegate)delegate { SelectTreeViewItem(); });

        }


        private void SelectTreeViewItem()
        {
            if (treeSplits.SelectedItem != null)
            {
                GnosisContainerTreeViewItem item = (GnosisContainerTreeViewItem)treeSplits.SelectedItem;
                if (item.Tag is GnosisParentWindowController)
                {
                    //GnosisParentWindowWPF is the only control in the tree which is not a GnosisContainerWPF
                    //it has no editable features
                    return;
                }

                if (item != null)
                {
                    if (currentController != null)
                    {
                        currentController.UnHighlight();
                    }
                    if (currentItem != null)
                    {
                        currentItem.UnHighlight();
                    }

                    //GnosisContainerWPF container = (GnosisContainerWPF)item.Tag;
                    currentController = (GnosisContainerController)item.Tag;
                    currentController.Highlight();

                    //bring the split's window to the fore
                    //parentWindow.ActivateWindow(container);

                    item.Highlight();
                    currentItem = item;

                    assigningValues = true; //a flag to allow us to stop events from firing while we assign values to controls
                    if (currentController is GnosisNavTileController)
                    {
                        DisplayPropertiesNavTile();
                    }
                    else if (currentController is GnosisPrimarySplitController)
                    {
                        DisplayPropertiesPrimarySplit();
                    }
                    else if (currentController is GnosisSplitController)
                    {
                        DisplayPropertiesSplit();
                    }
                    else if (currentController is GnosisTileController)
                    {
                        DisplayPropertiesTile();
                    }
                    else
                    {
                        gdNav.Visibility = Visibility.Collapsed;
                        gdParent.Visibility = Visibility.Collapsed;
                    }
                    assigningValues = false;
                }
            }
        }

        private TreeViewItem GetParentNode()
        {
            DependencyObject parent = VisualTreeHelper.GetParent(currentItem);
            while (!(parent is TreeViewItem))
                parent = VisualTreeHelper.GetParent(parent);
            TreeViewItem parentItem = (TreeViewItem)parent;
            return parentItem;
        }

        private void DisplayPropertiesNavTile()
        {
            gdLeaf.Visibility = Visibility.Collapsed;
            gdParent.Visibility = Visibility.Collapsed;
            gdNav.Visibility = Visibility.Visible;
            gdMain.Visibility = Visibility.Collapsed;

            //GnosisNavTile navTile = (GnosisNavTile)((GnosisNavTileWPF)currentContainer).GetBaseContainer();

            ////Get the parent to find split properties
            //TreeViewItem parentItem = GetParentNode();
            //GnosisPrimarySplitWPF primarySplitWPF = (GnosisPrimarySplitWPF)parentItem.Tag;
            //GnosisPrimarySplit primarySplit = (GnosisPrimarySplit)primarySplitWPF.GetBaseContainer();
            //GnosisSplitDetail splitDetail = primarySplit.GnosisSplitDetails[0];

            //chkNavVisible.IsChecked = navTile.Visible;
            //chkEnforceFixedNavWidth.IsChecked = primarySplitWPF.UsingFixedNavWidth;
            //chkNavTileHasTabs.IsChecked = navTile.HasTabs;
            //txtWidth.Value = splitDetail.SplitUnits;

            chkNavVisible.IsChecked = !((GnosisNavigatorTile)currentController.ControlImplementation).Hidden;
            chkEnforceFixedNavWidth.IsChecked = GlobalData.Singleton.PrimarySplitController.UsingUnits;
            chkNavTileHasTabs.IsChecked = ((GnosisNavigatorTile)currentController.ControlImplementation).HasTabs;
            txtWidth.Value = ((GnosisNavTileController)currentController).Width;
            txtWidth.IsEnabled = GlobalData.Singleton.PrimarySplitController.UsingUnits;

        }

        private void DisplayPropertiesPrimarySplit()
        {
            gdLeaf.Visibility = Visibility.Collapsed;
            gdParent.Visibility = Visibility.Collapsed;
            gdNav.Visibility = Visibility.Collapsed;
            gdMain.Visibility = Visibility.Visible;

            lblRootHeader.Content = currentController.ControlImplementation.GnosisName;
            txtRootPercentage.Value = (int)((GnosisPrimarySplitController)currentController).SplitPercentage;

            if (((GnosisPrimarySplitController)currentController).UsingUnits)
            {
                txtRootPercentage.IsEnabled = false;
                lblRootPercentage.IsEnabled = false;
            }
            else
            {
                txtRootPercentage.IsEnabled = true;
                lblRootPercentage.IsEnabled = true;
            }
        }

        private void DisplayPropertiesSplit()
        {
            gdLeaf.Visibility = Visibility.Collapsed;
            gdParent.Visibility = Visibility.Visible;
            gdNav.Visibility = Visibility.Collapsed;
            gdMain.Visibility = Visibility.Collapsed;

            lblParentHeader.Content = currentController.ControlImplementation.GnosisName;
            lblParentOrientation.Content = ((GnosisSplitController)currentController).Orientation;
            txtPercentage.IsEnabled = true;
            lblPercentage.IsEnabled = true;
            txtPercentage.Value = (int)((GnosisSplitController)currentController).SplitPercentage;

            if (((GnosisSplitController)currentController).SplitDirection == GnosisSplitController.DirectionType.HORIZONTAL)
            {
                cboSplitDirection.SelectedItem = cboiHorizontal;
            }
            else
            {
                cboSplitDirection.SelectedItem = cboiVertical;
            }

            //Show position of this split in relation to sibling
            TreeViewItem parentItem = GetParentNode();
            if (parentItem != null && !(currentController is GnosisPrimarySplitController))
            {
                GnosisSplitController parentController = (GnosisSplitController)parentItem.Tag;
                if (parentController.SplitDirection == GnosisSplitController.DirectionType.VERTICAL)
                {
                    pnlParentSplitDirectionVertical1.Visibility = Visibility.Visible;
                    pnlParentSplitDirectionHorizontal1.Visibility = Visibility.Collapsed;
                    rdLeft1.IsEnabled = true;
                    rdRight1.IsEnabled = true;
                    if (currentController.ControlImplementation.Order == 1)
                    {
                        rdLeft1.IsChecked = true;
                        rdRight1.IsChecked = false;
                    }
                    else
                    {
                        rdRight1.IsChecked = true;
                        rdLeft1.IsChecked = false;
                    }
                    rdLeft1.IsEnabled = false;
                    rdRight1.IsEnabled = false;
                }
                else
                {
                    pnlParentSplitDirectionHorizontal1.Visibility = Visibility.Visible;
                    pnlParentSplitDirectionVertical1.Visibility = Visibility.Collapsed;
                    rdTop1.IsEnabled = true;
                    rdBottom1.IsEnabled = true;
                    if (currentController.ControlImplementation.Order == 1)
                    {
                        rdTop1.IsChecked = true;
                        rdBottom1.IsChecked = false;
                    }
                    else
                    {
                        rdBottom1.IsChecked = true;
                        rdTop1.IsChecked = false;
                    }
                    rdTop1.IsEnabled = false;
                    rdBottom1.IsEnabled = false;
                }
            }
        }

        private void DisplayPropertiesTile()
        {
            gdParent.Visibility = Visibility.Collapsed;
            gdNav.Visibility = Visibility.Collapsed;
            gdLeaf.Visibility = Visibility.Visible;
            gdMain.Visibility = Visibility.Collapsed;

            lblLeafSplit.Content = currentController.ControlImplementation.GnosisName;

            TreeViewItem parentItem = GetParentNode();
            if (parentItem != null)
            {
                GnosisSplitController parentController = (GnosisSplitController)parentItem.Tag;
                if (parentController.SplitDirection == GnosisSplitController.DirectionType.VERTICAL)
                {
                    pnlParentSplitDirectionVertical.Visibility = Visibility.Visible;
                    pnlParentSplitDirectionHorizontal.Visibility = Visibility.Collapsed;
                    rdLeft.IsEnabled = true;
                    rdRight.IsEnabled = true;
                    if (currentController.ControlImplementation.Order == 1)
                    {
                        rdLeft.IsChecked = true;
                        rdRight.IsChecked = false;
                    }
                    else
                    {
                        rdRight.IsChecked = true;
                        rdLeft.IsChecked = false;
                    }
                    rdLeft.IsEnabled = false;
                    rdRight.IsEnabled = false;
                }
                else
                {
                    pnlParentSplitDirectionHorizontal.Visibility = Visibility.Visible;
                    pnlParentSplitDirectionVertical.Visibility = Visibility.Collapsed;
                    rdTop.IsEnabled = true;
                    rdBottom.IsEnabled = true;
                    if (currentController.ControlImplementation.Order == 1)
                    {
                        rdTop.IsChecked = true;
                        rdBottom.IsChecked = false;
                    }
                    else
                    {
                        rdBottom.IsChecked = true;
                        rdTop.IsChecked = false;
                    }
                    rdTop.IsEnabled = false;
                    rdBottom.IsEnabled = false;
                }
            }
        }

        private void chkNavVisible_Checked(object sender, RoutedEventArgs e)
        {
            if (!assigningValues)
            {
                ((GnosisNavigatorTile)currentController.ControlImplementation).Hidden = false;
            }
        }

        private void chkNavVisible_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!assigningValues)
            {
                ((GnosisNavigatorTile)currentController.ControlImplementation).Hidden = true;
            }
        }

        private void txtWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!assigningValues)
            {
                UpdateSplitUnits();
            }
        }

        private void txtWidth_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateSplitUnits();
        }

        private void UpdateSplitUnits()
        {
            if (currentController != null)
            {
                if (txtWidth.Value != null)
                {
                    if (validator.ValidateSplitUnits((int)txtWidth.Value))
                    {
                        int newWidth = (int)txtWidth.Value;
                        TreeViewItem parentItem = GetParentNode();
                        GnosisPrimarySplitController primarySplitController = (GnosisPrimarySplitController)parentItem.Tag;
                        primarySplitController.ChangeSplitUnits(newWidth);
                        txtWidth.Style = gdRoot.FindResource("textBoxNormalStyle") as System.Windows.Style;

                    }
                    else
                    {
                        txtWidth.Style = gdRoot.FindResource("textBoxErrorStyle") as System.Windows.Style;
                    }
                }
            }
        }

        private void txtPercentage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!assigningValues)
            {
                UpdateSplitPercentage();
            }
        }

        private void txtPercentage_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateSplitPercentage();
        }

        private void UpdateSplitPercentage()
        {
            if (currentController != null)
            {
                if (txtPercentage.Value != null)
                {
                    if (validator.ValidateSplitPercentage((int)txtPercentage.Value))
                    {
                        int newPercent = (int)txtPercentage.Value;
                        GnosisSplitController splitController = (GnosisSplitController)currentController;
                        splitController.ChangeSplitPercentage(newPercent);
                        txtPercentage.Style = gdRoot.FindResource("textBoxNormalStyle") as System.Windows.Style;
                    }
                    else
                    {
                        txtPercentage.Style = gdRoot.FindResource("textBoxErrorStyle") as System.Windows.Style;
                    }
                }
                else
                {
                    txtPercentage.Style = gdRoot.FindResource("textBoxErrorStyle") as System.Windows.Style;
                }
            }
        }


        private void chkEnforceFixedNavWidth_Checked(object sender, RoutedEventArgs e)
        {
            if (!assigningValues)
            {
                GlobalData.Singleton.PrimarySplitController.ChangeUsingUnits(true);
                txtWidth.IsEnabled = true;
                lblWidth.IsEnabled = true;
            }
        }

        private void chkEnforceFixedNavWidth_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!assigningValues)
            {
                GlobalData.Singleton.PrimarySplitController.ChangeUsingUnits(false);
                txtWidth.IsEnabled = false;
                lblWidth.IsEnabled = false;
            }
        }


        private void RefreshTree()
        {
            currentItem = null;
            currentController = null;
            treeSplits.Items.Remove(treeSplits.Items[0]);

            //display VisibleChildOrder
            GlobalData.Singleton.LayoutController.ShowVisibleTileOrder();

            //Get tree view
            GnosisContainerTreeViewItemController rootController = new GnosisContainerTreeViewItemController(GlobalData.Singleton.ParentWindowController);
            TreeViewItem rootItem = (GnosisContainerTreeViewItem)rootController.TreeViewItemImplementation;
            treeSplits.Items.Add(rootItem);

            gdParent.Visibility = Visibility.Collapsed;
            gdNav.Visibility = Visibility.Collapsed;
            gdLeaf.Visibility = Visibility.Collapsed;
            gdMain.Visibility = Visibility.Collapsed;
        }

        private void tabItem_Layout_Loaded(object sender, RoutedEventArgs e)
        {
            //GnosisTile tile = parentWindow.GetCurrentTile();
            //if (tile == null)
            //{
            //    ((TreeViewItem)treeSplits.Items[0]).IsSelected = true;
            //}
            //else
            //{
            //    SelectSplit((TreeViewItem)treeSplits.Items[0], tile);
            //}
        }

        private void SelectContainer(TreeViewItem node, GnosisContainerController containerToSelect)
        {
            if (node.Tag is GnosisParentWindowController)
            {
                if (node.Items.Count > 0)
                {
                    foreach (TreeViewItem item in node.Items)
                    {
                        SelectContainer(item, containerToSelect);
                    }
                }
            }
            else
            {
                GnosisContainerController controller = node.Tag as GnosisContainerController;
                if (controller.ControlImplementation.ID == containerToSelect.ControlImplementation.ID)
                {
                    node.IsSelected = true;
                    return;
                }
                else
                {
                    if (node.Items.Count > 0)
                    {
                        foreach (TreeViewItem item in node.Items)
                        {
                            SelectContainer(item, containerToSelect);
                        }
                    }
                }
            }

        }

        private void cboSplitDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //check if change was caused by user rather than instantiation
            if (cboSplitDirection.IsDropDownOpen)
            {
                ComboBoxItem item = (ComboBoxItem)cboSplitDirection.SelectedItem;
                string newDirection = item.Content.ToString();
                GnosisSplitController.DirectionType direction = (GnosisSplitController.DirectionType)Enum.Parse(typeof(GnosisSplitController.DirectionType), newDirection);
                ((GnosisSplitController)currentController).ChangeSplitDirection(direction);
                currentController.Highlight();
            }
        }

        private void AddSplit(GnosisController.TilePosition position)
        {
            TreeViewItem parentItem = GetParentNode();

            GlobalData.Singleton.LayoutController.AddTile((GnosisTileController)currentController, position);
            GnosisContainerController controller = currentController;

            RefreshTree();

            GlobalData.Singleton.LayoutController.ShowVisibleTileOrder();
            SelectContainer((TreeViewItem)treeSplits.Items[0], controller);
        }

        private void chkNavTileHasTabs_Checked(object sender, RoutedEventArgs e)
        {
            if (!assigningValues)
            {
                ((GnosisNavigatorTile)currentController.ControlImplementation).HasTabs = true;
            }
        }

        private void chkNavTileHasTabs_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!assigningValues)
            {
                ((GnosisNavigatorTile)currentController.ControlImplementation).HasTabs = false;
            }
        }

        private void txtRootPercentage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!assigningValues)
            {
                UpdatePrimarySplitPercentage();
            }
        }

        private void txtRootPercentage_KeyUp(object sender, KeyEventArgs e)
        {
            UpdatePrimarySplitPercentage();
        }

        private void UpdatePrimarySplitPercentage()
        {
            if (currentController != null)
            {
                if (txtRootPercentage.Value != null)
                {
                    if (validator.ValidateSplitPercentage((int)txtRootPercentage.Value))
                    {
                        int newPercent = (int)txtRootPercentage.Value;
                        ((GnosisSplitController)currentController).ChangeSplitPercentage(newPercent);
                        txtRootPercentage.Style = gdRoot.FindResource("textBoxNormalStyle") as System.Windows.Style;
                    }
                    else
                    {
                        txtRootPercentage.Style = gdRoot.FindResource("textBoxErrorStyle") as System.Windows.Style;
                    }
                }
                else
                {
                    txtRootPercentage.Style = gdRoot.FindResource("textBoxErrorStyle") as System.Windows.Style;
                }
            }
        }

        private void chkTileHasTabs_Checked(object sender, RoutedEventArgs e)
        {
            if (!assigningValues)
            {

            }
        }

        private void chkTileHasTabs_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!assigningValues)
            {

            }
        }

        private void cboManageTiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboManageTiles.IsDropDownOpen && cboManageTiles.SelectedItem != null)
            {
                ComboBoxItem item = (ComboBoxItem)cboManageTiles.SelectedItem;
                if (item.Name.Equals("cboiNewTileLeft"))
                {
                    AddSplit(GnosisController.TilePosition.LEFT);
                }
                else if (item.Name.Equals("cboiNewTileRight"))
                {
                    AddSplit(GnosisController.TilePosition.RIGHT);
                }
                else if (item.Name.Equals("cboiNewTileAbove"))
                {
                    AddSplit(GnosisController.TilePosition.TOP);
                }
                else if (item.Name.Equals("cboiNewTileBelow"))
                {
                    AddSplit(GnosisController.TilePosition.BOTTOM);
                }
                else if (item.Name.Equals("cboiDeleteTile"))
                {
                    TreeViewItem parentItem = GetParentNode();
                    GlobalData.Singleton.LayoutController.RemoveTile((GnosisTileController)currentController);

                    GnosisContainerController controller = currentController;
                    RefreshTree();
                    SelectContainer((TreeViewItem)treeSplits.Items[0], controller);
                }
                else if (item.Name.Equals("cboiMoveTileToNewWindow"))
                {

                }
            }
            cboManageTiles.SelectedItem = null;
        }
    }


}
