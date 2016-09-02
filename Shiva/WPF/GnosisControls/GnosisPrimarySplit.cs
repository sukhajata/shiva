using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Shiva.Shared.Interfaces;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.Data;
using ShivaWPF3.LayoutFeatures;

namespace GnosisControls
{
    public partial class GnosisPrimarySplit : GnosisSplit, IGnosisPrimarySplitImplementation
    {
        private GnosisNavigatorTile gnosisNavTileField;


        [GnosisChild]
        public GnosisNavigatorTile GnosisNavTile
        {
            get
            {
                return this.gnosisNavTileField;
            }
            set
            {
                this.gnosisNavTileField = value;
            }
        }

        private Action<double, int> splitterMovedHandler;

        private bool tilesOverlayParent;

        [GnosisPropertyAttribute]
        public bool TilesOverlayParent
        {
            get
            {
                return tilesOverlayParent;
            }

            set
            {
                tilesOverlayParent = value;
            }
        }

        public GnosisPrimarySplit() :base()
        {
        }


        public int SetSplitterPercent(double newPercent)
        {
            base.SetSplitterPercent(newPercent, GnosisSplitController.DirectionType.VERTICAL);

            //Get new split units
            int navWidth = (int)contentGrid.ColumnDefinitions[0].ActualWidth;
            return navWidth;
        }



        public double SetSplitterUnits(int newUnits)
        {
            double newNavWidth = newUnits;
            double newOtherContainerWidth = Application.Current.MainWindow.ActualWidth - 25 - newNavWidth;
            if (newOtherContainerWidth < 0)
                newOtherContainerWidth = 0;

            //work out percentage
            double total = newNavWidth + newOtherContainerWidth;
            double newPercent = newNavWidth / total * 100;
            
            if (!((GnosisNavigatorTile)GlobalData.Singleton.PrimarySplitController.NavTileController.ControlImplementation).Hidden)
            {
                contentGrid.ColumnDefinitions[0].Width = new GridLength(newNavWidth);
                contentGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);
                contentGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);//fill remaining space
            }
            else
            {
                contentGrid.ColumnDefinitions[0].Width = new GridLength(0);
                contentGrid.ColumnDefinitions[1].Width = new GridLength(0);
                contentGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
            }

            return newPercent;
        }


        public void SetSplitterMovedHandler(Action<double, int> _splitterMovedHandler)
        {
            splitterMovedHandler = _splitterMovedHandler;
        }


        public override void SplitterMoved(GnosisSplitController.DirectionType splitDirection)
        {
            //Calculate new split percentage and split units and pass to the splitterMoved handler
            double percent;

            //The gridsplitter is in the second column
            double splitUnits = contentGrid.ColumnDefinitions[0].ActualWidth;
            double width2 = contentGrid.ColumnDefinitions[2].ActualWidth;

            //convert to percentage
            double total = splitUnits + width2;
            percent = splitUnits / total * 100;

            percent = Math.Round(percent, 2);

            splitterMovedHandler.Invoke(percent, (int)splitUnits);
        }

        //public void BuildContentFixedNavWidth(List<GnosisContainer> containers, 
        //    int splitUnits, GnosisSplitController.DirectionType splitDirection)
        //{
        //    contentGrid.Children.Clear();
        //    contentGrid.ColumnDefinitions.Clear();

        //    if (containers.Count == 1)
        //    {
        //        contentGrid.Children.Add((GnosisContainer)containers[0]);
        //    }
        //    else if (containers.Count == 2)
        //    {
        //        //build columns
        //        //first column
        //        ColumnDefinition colDef0 = new ColumnDefinition();
        //        colDef0.Width = new GridLength(splitUnits, GridUnitType.Pixel);
        //        contentGrid.ColumnDefinitions.Add(colDef0);

        //        //second column containing gridSplitter
        //        ColumnDefinition colDef1 = new ColumnDefinition();
        //        colDef1.Width = GridLength.Auto;
        //        contentGrid.ColumnDefinitions.Add(colDef1);

        //        //third column
        //        ColumnDefinition colDef2 = new ColumnDefinition();
        //        colDef2.Width = new GridLength(1, GridUnitType.Star); //fill remaining space
        //        contentGrid.ColumnDefinitions.Add(colDef2);

        //        splitter = new VerticalGridSplitter(this);
        //        Grid.SetColumn(splitter, 1);
        //        contentGrid.Children.Add(splitter);

        //        //add containers
        //        if (containers[0].Order == 1)
        //        {
        //            Grid.SetColumn((GnosisContainer)containers[0], 0);
        //            Grid.SetColumn((GnosisContainer)containers[1], 2);
        //        }
        //        else
        //        {
        //            Grid.SetColumn((GnosisContainer)containers[1], 0);
        //            Grid.SetColumn((GnosisContainer)containers[0], 2);
        //        }

        //        //add children to parent
        //        contentGrid.Children.Add((GnosisContainer)containers[0]);
        //        contentGrid.Children.Add((GnosisContainer)containers[1]);
        //    }
        //}

        public void BuildContentFixedNavWidth(List<GnosisContainer> containers,
            int splitUnits, GnosisSplitController.DirectionType splitDirection)
        {
            contentGrid.Children.Clear();
            contentGrid.ColumnDefinitions.Clear();

            if (containers.Count == 1)
            {
                
                contentGrid.Children.Add((GnosisContainer)containers[0]);
            }
            else if (containers.Count == 2)
            {
                //build columns
                //first column
                ColumnDefinition colDef0 = new ColumnDefinition();
                colDef0.Width = new GridLength(splitUnits, GridUnitType.Pixel);
                contentGrid.ColumnDefinitions.Add(colDef0);

                //second column containing gridSplitter
                ColumnDefinition colDef1 = new ColumnDefinition();
                colDef1.Width = GridLength.Auto;
                contentGrid.ColumnDefinitions.Add(colDef1);

                //third column
                ColumnDefinition colDef2 = new ColumnDefinition();
                colDef2.Width = new GridLength(1, GridUnitType.Star); //fill remaining space
                contentGrid.ColumnDefinitions.Add(colDef2);

                splitter = new VerticalGridSplitter(this);
                Grid.SetColumn(splitter, 1);
                contentGrid.Children.Add(splitter);

                //add containers
                if (containers[0].Order == 1)
                {
                    Grid.SetColumn((GnosisContainer)containers[0], 0);
                    Grid.SetColumn((GnosisContainer)containers[1], 2);
                }
                else
                {
                    Grid.SetColumn((GnosisContainer)containers[1], 0);
                    Grid.SetColumn((GnosisContainer)containers[0], 2);
                }

                //add children to parent
                contentGrid.Children.Add((GnosisContainer)containers[0]);
                contentGrid.Children.Add((GnosisContainer)containers[1]);
            }
        }


        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisNavigatorTile)
            {
                GnosisNavTile = (GnosisNavigatorTile)child;
            }
            else
            {
                base.GnosisAddChild(child);
            }
        }

        ////If a new width is assigned to the navigator from the properties dialog
        ////or the navigator becomes visible after being hidden
        ////we must resize the columns.
        //public void ResizeNavWidth()
        //{
        //    double newNavWidth = ((GnosisPrimarySplitController)controller).SplitUnits;
        //    double newOtherContainerWidth = Application.Current.MainWindow.ActualWidth - 25 - newNavWidth;
        //    if (newOtherContainerWidth < 0)
        //        newOtherContainerWidth = 0;

        //    //work out percentage
        //    double total = newNavWidth + newOtherContainerWidth;
        //    double newPercent = newNavWidth / total * 100;
        //    ((GnosisPrimarySplitController)controller).SplitDetailController.SplitPercentage = newPercent;

        //    if (((GnosisPrimarySplitController)controller).NavTileController.Visible)
        //    {
        //        contentGrid.ColumnDefinitions[0].Width = new GridLength(newNavWidth);
        //        contentGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);
        //        contentGrid.ColumnDefinitions[2].Width = new GridLength(newOtherContainerWidth);
        //    }
        //    else
        //    {
        //        contentGrid.ColumnDefinitions[0].Width = new GridLength(0);
        //        contentGrid.ColumnDefinitions[1].Width = new GridLength(0);
        //        contentGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
        //    }
        //}

        //new public void OnSplitPercentageChanged(double newPercent, string splitDirection)
        //{
        //    base.OnSplitPercentageChanged(newPercent, splitDirection);

        //    //Update SplitUnits
        //    int navWidth = (int)NavTileWPF.ActualWidth;
        //    ((GnosisPrimarySplitController)controller).SplitUnits = navWidth;
        //}

        //internal void ShowNavigator()
        //{
        //    contentGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);
        //    //if (splitWidth > 0)
        //    //{
        //    //    ResizeChildSplitsWidth(splitWidth);
        //    //}
        //    //else
        //    //{
        //    //    ResizeChildSplitsPercentage();
        //    //}

        //}

        //internal void HideNavigator()
        //{
        //    contentGrid.ColumnDefinitions[0].Width = new GridLength(0);
        //    contentGrid.ColumnDefinitions[1].Width = new GridLength(0);
        //    contentGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
        //}

        //protected override void AddContainers()
        //{
        //    ChildContainers = new List<GnosisContainerWPF>();
        //    NavTileWPF = new GnosisNavTileWPF(((GnosisPrimarySplit)gnosisSplit).GnosisNavTile[0]);

        //    ChildContainers.Add(NavTileWPF);

        //    if (gnosisSplit.GnosisSplits != null)
        //    {
        //        foreach (GnosisSplit split in ((GnosisPrimarySplit)gnosisSplit).GnosisSplits)
        //        {
        //            GnosisSplitWPF splitWPF = new GnosisSplitWPF(split);
        //            ChildContainers.Add(splitWPF);
        //        }
        //    }
        //    if (gnosisSplit.GnosisTiles != null)
        //    {
        //        foreach (GnosisTile tile in (gnosisSplit).GnosisTiles)
        //        {
        //            GnosisTileWPF tileWPF = new GnosisTileWPF(tile);
        //            ChildContainers.Add(tileWPF);
        //        }
        //    }
        //}

    }
}
