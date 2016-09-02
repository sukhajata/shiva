using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

using Shiva.Shared.Interfaces;
using Shiva.Shared.ContainerControllers;
using ShivaWPF3.LayoutFeatures;
using Shiva.Shared.Data;

namespace GnosisControls
{
    public partial class GnosisSplit : GnosisContainer, IGnosisSplitImplementation
    {
        protected List<GnosisSplitDetail> gnosisSplitDetails;

        protected List<GnosisSplit> gnosisSplits;

        protected List<GnosisTile> gnosisTiles;


        [GnosisCollection]
        public List<GnosisSplit> Splits
        {
            get { return gnosisSplits; }
            set { gnosisSplits = value; }
        }

        [GnosisCollection]
        public List<GnosisTile> Tiles
        {
            get
            {
                return gnosisTiles;
            }
            set
            {
                gnosisTiles = value;
            }
        }


        [GnosisCollection]
        public List<GnosisSplitDetail> SplitDetails
        {
            get
            {
                return this.gnosisSplitDetails;
            }
            set { this.gnosisSplitDetails = value; }
        }



        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisSplit)
            {
                if (gnosisSplits == null)
                {
                    gnosisSplits = new List<GnosisSplit>();
                }

                gnosisSplits.Add((GnosisSplit)child);

            }
            else if (child is GnosisTile)
            {
                if (gnosisTiles == null)
                {
                    gnosisTiles = new List<GnosisTile>();
                }

                gnosisTiles.Add((GnosisTile)child);
            }
            else if (child is GnosisSplitDetail)
            {
                if (gnosisSplitDetails == null)
                {
                    gnosisSplitDetails = new List<GnosisSplitDetail>();
                }

                gnosisSplitDetails.Add((GnosisSplitDetail)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown type added to GnosisSplit: " + child.GetType().Name,
                    "GnosisSplit.GnosisAddChild");
            }
        }

        public GridSplitter splitter;

        private Action<double> splitterMovedHandler;
        private Action<double> loadedHandler;

        public GnosisSplit() : base()
        {
            //BuildContent();
           
        }

        public void SetLoadedHandler(Action<double> _loadedHandler)
        {
            loadedHandler = _loadedHandler;
            this.Loaded += GnosisSplitWPF_Loaded;
        }

        private void GnosisSplitWPF_Loaded(object sender, RoutedEventArgs e)
        {
            double width = this.ActualWidth;
            loadedHandler.Invoke(width);
        }

        public void SetSplitterMovedHandler(Action<double> _splitterMovedHandler)
        {
            splitterMovedHandler = _splitterMovedHandler;
        }

        
        public virtual void SplitterMoved(GnosisSplitController.DirectionType splitDirection)
        {
            //Calculate new split percentage and pass to the splitterMoved handler
            double percent;

            //The gridsplitter is in the second column
            if (splitDirection == GnosisSplitController.DirectionType.VERTICAL)
            {
                //vertical split
                GridLength gl1 = contentGrid.ColumnDefinitions[0].Width;
                double width1 = gl1.Value;

                GridLength gl2 = contentGrid.ColumnDefinitions[2].Width;
                double width2 = gl2.Value;

                //convert to percentage
                double total = width1 + width2;
                percent = width1 / total * 100;

            }
            else
            {
                //horizontal split
                GridLength gl1 = contentGrid.RowDefinitions[0].Height;
                 double height1 = gl1.Value;

                GridLength gl2 = contentGrid.RowDefinitions[2].Height;
                double height2 = gl2.Value;

                //convert to percent
                double total = height1 + height2;
                percent = height1 / total * 100;
            }

            percent = Math.Round(percent, 2);

            splitterMovedHandler.Invoke(percent);
        }

        public override void Highlight()
        {
            base.Highlight();
            splitter.Background = (Brush)Application.Current.FindResource("SplitterHighlightColor");
        }

        public override void UnHighlight()
        {
            base.UnHighlight();
            splitter.Background = (Brush)Application.Current.FindResource("SplitterColor");
        }

      

        public void BuildContent(List<GnosisContainer> containers, double splitPercentage,
            GnosisSplitController.DirectionType splitDirection)
        {
            contentGrid.Children.Clear();
            contentGrid.ColumnDefinitions.Clear();
            contentGrid.RowDefinitions.Clear();

            if (containers.Count == 1)
            {
                contentGrid.Children.Add((GnosisContainer)containers[0]);
            }
            else if (containers.Count == 2)
            {
                //Convert percentage width to * values 
                double length1 = splitPercentage / 10;
                double length2 = 10 - length1;

                if (splitDirection == GnosisSplitController.DirectionType.VERTICAL)
                {
                    //build columns
                    //first column
                    ColumnDefinition colDef0 = new ColumnDefinition();
                    colDef0.Width = new GridLength(length1, GridUnitType.Star);
                    contentGrid.ColumnDefinitions.Add(colDef0);

                    //second column containing gridSplitter
                    ColumnDefinition colDef1 = new ColumnDefinition();
                    colDef1.Width = GridLength.Auto;
                    contentGrid.ColumnDefinitions.Add(colDef1);

                    //third column
                    ColumnDefinition colDef2 = new ColumnDefinition();
                    colDef2.Width = new GridLength(length2, GridUnitType.Star);
                    contentGrid.ColumnDefinitions.Add(colDef2);

                    //add containers
                    splitter = new VerticalGridSplitter(this);
                    Grid.SetColumn(splitter, 1);
                    contentGrid.Children.Add(splitter);

                    
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
                }
                else
                {
                    //build rows
                    //first row
                    RowDefinition rowDef0 = new RowDefinition();
                    rowDef0.Height = new GridLength(length1, GridUnitType.Star);
                    contentGrid.RowDefinitions.Add(rowDef0);

                    //second row containing gridSplitter
                    RowDefinition rowDef1 = new RowDefinition();
                    rowDef1.Height = GridLength.Auto;
                    contentGrid.RowDefinitions.Add(rowDef1);

                    //third row
                    RowDefinition rowDef2 = new RowDefinition();
                    rowDef2.Height = new GridLength(length2, GridUnitType.Star);
                    contentGrid.RowDefinitions.Add(rowDef2);

                    splitter = new HorizontalGridSplitter(this);
                    Grid.SetRow(splitter, 1);
                    contentGrid.Children.Add(splitter);

                    //add containers
                    if (containers[0].Order == 1)
                    {
                        Grid.SetRow((GnosisContainer)containers[0], 0);
                        Grid.SetRow((GnosisContainer)containers[1], 2);
                    }
                    else
                    {
                        Grid.SetRow((GnosisContainer)containers[1], 0);
                        Grid.SetRow((GnosisContainer)containers[0], 2);
                    }

                }

                //add children to parent
                contentGrid.Children.Add((GnosisContainer)containers[0]);
                contentGrid.Children.Add((GnosisContainer)containers[1]);
            }
        }

        public void RemoveChild(GnosisContainer child)
        {
            contentGrid.Children.Remove((GnosisContainer)child);
        }

        public void SetSplitterPercent(double newPercent, GnosisSplitController.DirectionType splitDirection)
        {
            double bound1 = newPercent / 10;
            double bound2 = 10 - bound1;

            if (splitDirection == GnosisSplitController.DirectionType.VERTICAL)
            {
                contentGrid.ColumnDefinitions[0].Width = new GridLength(bound1, GridUnitType.Star);
                contentGrid.ColumnDefinitions[2].Width = new GridLength(bound2, GridUnitType.Star);
            }
            else
            {
                contentGrid.RowDefinitions[0].Height = new GridLength(bound1, GridUnitType.Star);
                contentGrid.RowDefinitions[2].Height = new GridLength(bound2, GridUnitType.Star);
            }
        }

        //public void RebuildContent()
        //{
        //    contentGrid.Children.Clear();
        //    contentGrid.ColumnDefinitions.Clear();
        //    contentGrid.RowDefinitions.Clear();

        //    //BuildContent();
        //}

        //public void OnSplitPercentageChanged(double newPercent, string splitDirection)
        //{
        //    double bound1 = newPercent / 10;
        //    double bound2 = 10 - bound1;

        //    if (splitDirection.Equals(GnosisSplitDetail.DIRECTION_VERTICAL))
        //    {
        //        contentGrid.ColumnDefinitions[0].Width = new GridLength(bound1, GridUnitType.Star);
        //        contentGrid.ColumnDefinitions[2].Width = new GridLength(bound2, GridUnitType.Star);
        //    }
        //    else
        //    {
        //        contentGrid.RowDefinitions[0].Height = new GridLength(bound1, GridUnitType.Star);
        //        contentGrid.RowDefinitions[2].Height = new GridLength(bound2, GridUnitType.Star);
        //    }
        //}

        //public void OnSplitDirectionChanged(string newDirection)
        //{
        //    contentGrid.Children.Clear();
        //    contentGrid.ColumnDefinitions.Clear();
        //    contentGrid.RowDefinitions.Clear();

        //    //BuildContent();
        //}

        //protected virtual void AddContainers()
        //{
        //    ChildContainers = new List<GnosisContainer>();

        //    if (gnosisSplit.GnosisSplits != null)
        //    {
        //        foreach (GnosisSplit split in gnosisSplit.GnosisSplits)
        //        {
        //            GnosisSplitWPF splitWPF = new GnosisSplitWPF(split);
        //            ChildContainers.Add(splitWPF);
        //        }
        //    }
        //    if (gnosisSplit.GnosisTiles != null)
        //    {
        //        foreach (GnosisTile tile in gnosisSplit.GnosisTiles)
        //        {
        //            GnosisTileWPF tileWPF = new GnosisTileWPF(tile);
        //            ChildContainers.Add(tileWPF);
        //        }
        //    }
        //}

        //protected virtual void GnosisSplitDetail_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("SplitPercentage"))
        //    {
        //        OnSplitPercentageChanged();
        //    }
        //    else if (e.PropertyName.Equals("SplitDirection"))
        //    {
        //        OnSplitDirectionChanged();
        //    }
        //}

        //protected virtual void ChildContainer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("Visible"))
        //    {
        //        contentGrid.Children.Clear();
        //        contentGrid.ColumnDefinitions.Clear();
        //        contentGrid.RowDefinitions.Clear();

        //        BuildContent();
        //    }
        //    if (e.PropertyName.Equals("HasTabs"))
        //    {

        //    }
        //}

        //private void GnosisSplit_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("Altered"))
        //    {
        //        contentGrid.Children.Clear();
        //        contentGrid.ColumnDefinitions.Clear();
        //        contentGrid.RowDefinitions.Clear();

        //        AddContainers();
        //        BuildContent();
        //    }
        //}

        //protected void BuildContent()
        //{
        //    //Get visibilities
        //    bool child0Visible = ((GnosisContainer)ChildContainers[0].GetBaseContainer()).Visible;
        //    bool child1Visible = ((GnosisContainer)ChildContainers[1].GetBaseContainer()).Visible;

        //    if (!child0Visible && !child1Visible)
        //    {
        //        return;
        //    }

        //    if (child0Visible && child1Visible)
        //    {

        //        //Convert percentage width to * values 
        //        double length1 = gnosisSplit.GnosisSplitDetails[0].SplitPercentage / 10;
        //        double length2 = 10 - length1;

        //        //column/row definitions
        //        if (gnosisSplit.GnosisSplitDetails[0].SplitDirection.Equals("Horizontal"))
        //        {
        //            //first row
        //            RowDefinition rowDef0 = new RowDefinition();
        //            rowDef0.Height = new GridLength(length1, GridUnitType.Star);
        //            contentGrid.RowDefinitions.Add(rowDef0);

        //            //second row containing gridSplitter
        //            RowDefinition rowDef1 = new RowDefinition();
        //            rowDef1.Height = GridLength.Auto;
        //            contentGrid.RowDefinitions.Add(rowDef1);

        //            //third row
        //            RowDefinition rowDef2 = new RowDefinition();
        //            rowDef2.Height = new GridLength(length2, GridUnitType.Star);
        //            contentGrid.RowDefinitions.Add(rowDef2);

        //            splitter = new HorizontalGridSplitter(this);
        //            Grid.SetRow(splitter, 1);
        //            contentGrid.Children.Add(splitter);

        //        }
        //        else  //vertical split
        //        {
        //            //first column
        //            ColumnDefinition colDef0 = new ColumnDefinition();
        //            colDef0.Width = new GridLength(length1, GridUnitType.Star);
        //            contentGrid.ColumnDefinitions.Add(colDef0);

        //            //second column containing gridSplitter
        //            ColumnDefinition colDef1 = new ColumnDefinition();
        //            colDef1.Width = GridLength.Auto;
        //            contentGrid.ColumnDefinitions.Add(colDef1);

        //            //third column
        //            ColumnDefinition colDef2 = new ColumnDefinition();
        //            colDef2.Width = new GridLength(length2, GridUnitType.Star);
        //            contentGrid.ColumnDefinitions.Add(colDef2);

        //            splitter = new VerticalGridSplitter(this);
        //            Grid.SetColumn(splitter, 1);
        //            contentGrid.Children.Add(splitter);
        //        }

        //        //Assign Row/Column to child Splits
        //        if (gnosisSplit.GnosisSplitDetails[0].SplitDirection.Equals("Horizontal"))
        //        {
        //            if (ChildContainers[0].GetBaseContainer().Order == 1)
        //            {
        //                Grid.SetRow(ChildContainers[0], 0);
        //                Grid.SetRow(ChildContainers[1], 2);
        //            }
        //            else
        //            {
        //                Grid.SetRow(ChildContainers[1], 0);
        //                Grid.SetRow(ChildContainers[0], 2);
        //            }
        //        }
        //        else
        //        {
        //            if (ChildContainers[0].GetBaseContainer().Order == 1)
        //            {
        //                Grid.SetColumn(ChildContainers[0], 0);
        //                Grid.SetColumn(ChildContainers[1], 2);
        //            }
        //            else
        //            {
        //                Grid.SetColumn(ChildContainers[1], 0);
        //                Grid.SetColumn(ChildContainers[0], 2);
        //            }
        //        }

        //        //add children to parent
        //        contentGrid.Children.Add(ChildContainers[0]);
        //        contentGrid.Children.Add(ChildContainers[1]);
        //    }
        //    else
        //    { //only 1 visible
        //        if (child0Visible)
        //        {
        //            contentGrid.Children.Add(ChildContainers[0]);
        //        }
        //        else
        //        {
        //            contentGrid.Children.Add(ChildContainers[1]);
        //        }
        //    }
        //}


        //When a gridsplitter has been dragged we must calculate the new splitPercentage value
        //for later saving to xml
        //public void UpdateSplitPercentage()
        //{
        //    double percent;
        //    //Widths/Heights are in * star values which are proportions. The gridsplitter is in the second column
        //    if (gnosisSplit.GnosisSplitDetails[0].SplitDirection.Equals("Vertical"))
        //    {
        //        //vertical split
        //        GridLength gl1 = contentGrid.ColumnDefinitions[0].Width;
        //        double width1 = gl1.Value;

        //        GridLength gl2 = contentGrid.ColumnDefinitions[2].Width;
        //        double width2 = gl2.Value;

        //        //convert to percentage
        //        double total = width1 + width2;
        //        percent = width1 / total * 100;

        //    }
        //    else
        //    {
        //        //horizontal split
        //        GridLength gl1 = contentGrid.RowDefinitions[0].Height;
        //        double height1 = gl1.Value;

        //        GridLength gl2 = contentGrid.RowDefinitions[2].Height;
        //        double height2 = gl2.Value;

        //        //convert to percent
        //        double total = height1 + height2;
        //        percent = height1 / total * 100;
        //    }

        //    percent = Math.Round(percent, 2);
        //    gnosisSplit.GnosisSplitDetails[0].SplitPercentage = percent;

        //}



    }
}
