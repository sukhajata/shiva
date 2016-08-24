using Shiva.Shared.ContainerControllers;
using Shiva.Shared.Interfaces;
using ShivaWPF3.LayoutFeatures;
using ShivaWPF3.UtilityWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GnosisControls
{
    public partial class GnosisTile : GnosisContainer, IGnosisTileImplemenation
    {
        protected bool layoutMode;
        protected Action<double> loadedHandler;
        protected OverlayGrid gdOverlay;

        //protected Action GotFocusHandler;
        //protected Action LostFocusHandler;

        //protected static Thickness NormalThickness = new Thickness(0);
        //protected static Thickness FocusThickness = new Thickness(3);
        //protected static Thickness HighlightThickness = new Thickness(5);
        //protected static Thickness NoBorder = new Thickness(0);

        //public Grid contentGrid;
        //private GnosisContainerController controller;


        public GnosisTile()
            :base()
        {
            //contentGrid = new Grid();
            //this.Child = contentGrid;

            ////this.BorderBrush = (Brush)Application.Current.FindResource("BorderColor");
            //this.BorderThickness = GnosisContainer.NormalThickness;
            //this.HorizontalAlignment = HorizontalAlignment.Stretch;
            //this.VerticalAlignment = VerticalAlignment.Stretch;

            //this.MouseDown += GnosisContainerWPF_MouseDown;
            //this.MouseUp += GnosisContainerWPF_MouseUp;
            //this.MouseEnter += GnosisContainerWPF_MouseEnter;
            //this.MouseLeave += GnosisContainerWPF_MouseLeave;

            //this.PropertyChanged += GnosisContainer_PropertyChanged;
        }

        //protected virtual void GnosisContainer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        public void SetLoadedHandler(Action<double> action)
        {
            loadedHandler = action;
            this.Loaded += GnosisTileWPF_Loaded;
        }

        private void GnosisTileWPF_Loaded(object sender, RoutedEventArgs e)
        {
            double width = this.ActualWidth;
            loadedHandler.Invoke(width);
        }

        public void HideVisibleTileOrder()
        {
            contentGrid.Children.Remove(gdOverlay);
            layoutMode = false;
        }


        public void LoadFrameImplementation(IGnosisFrameImplementation frame)
        {
            contentGrid.Children.Clear();
            contentGrid.Children.Add((UIElement)frame);
            //contentGrid.Children.Add(new TextBox() { Text = ((GnosisFrameWPF)frame).GetController(). });
        }

        public void LoadTabImplementation(IGnosisTileTabImplementation tabImplementation)
        {
            contentGrid.Children.Clear();
            contentGrid.Children.Add((GnosisTileTab)tabImplementation);
        }

        //private void GnosisTileWPF_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    this.BorderBrush = (Brush)Application.Current.FindResource("BorderColor");
        //    this.BorderThickness = GnosisContainer.NormalThickness;
        //}

        //private void GnosisTileWPF_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    this.BorderBrush = (Brush)Application.Current.FindResource("BorderFocusColor");
        //    this.BorderThickness = GnosisContainer.FocusThickness;
        //}

        public void ShowVisibleTileOrder(int visibleOrder)
        {
            if (!layoutMode)
            {
                gdOverlay = new OverlayGrid();
                gdOverlay.SetTileOrder(visibleOrder);
                contentGrid.Children.Add(gdOverlay);
                layoutMode = true;
            }
            else
            {
                gdOverlay.SetTileOrder(visibleOrder);
            }
        }

        public void RemoveFrameImplementation(IGnosisFrameImplementation frameImplementation)
        {
            throw new NotImplementedException();
        }

        public double GetWidth()
        {
            return this.ActualWidth;
        }

        public void DisplayLoadingAnimation(string barColour)
        {
            viewLoading.Visibility = Visibility.Visible;
        }

        public void HideLoadingAnimation()
        {
            viewLoading.Visibility = Visibility.Collapsed;
        }

        //public void Clear()
        //{
        //    contentGrid.Children.Clear();
        //}

        //public virtual void Highlight()
        //{
        //    this.BorderBrush = (Brush)Application.Current.FindResource("BorderHighlightColor");
        //    this.BorderThickness = GnosisContainer.HighlightThickness;
        //}

        //public virtual void UnHighlight()
        //{
        //    this.BorderBrush = (Brush)Application.Current.FindResource("BorderColor");
        //    this.BorderThickness = GnosisContainer.NormalThickness;
        //}



        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.Padding = new Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Right, paddingVertical);
        //}

       

        //private void GnosisContainerWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //MouseDownHandler.Invoke();
        //    HasMouseDown = true;
        //}
        
        //private void GnosisContainerWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //MouseUpHandler.Invoke();
        //    HasMouseDown = false;
        //}
        
        //private void GnosisContainerWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    // GotMouseFocusHandler.Invoke();
        //    HasMouseFocus = true;
        //}
        
        //private void GnosisContainerWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    //LostMouseFocusHandler.Invoke();
        //    HasMouseFocus = false;
        //}
        
        //public void SetGotFocusHandler(Action action)
        //{
        //    GotFocusHandler = action;
        //    this.GotFocus += GnosisContainerWPF_GotFocus;
        //}

        //private void GnosisContainerWPF_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    GotFocusHandler.Invoke();
        //    HasFocus = true;
        //}

        //public void SetLostFocusHandler(Action action)
        //{
        //    LostFocusHandler = action;
        //    this.LostFocus += GnosisContainerWPF_LostFocus;
        //}

        //private void GnosisContainerWPF_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    LostFocusHandler.Invoke();
        //    HasFocus = false;
        //}

        //public void SetTooltipVisible(bool visible)
        //{
        //    ToolTipService.SetIsEnabled(this, visible);
        //}

        //public virtual void GnosisAddChild(IGnosisObject child)
        //{
        //    throw new NotImplementedException();
        //}



    }
}
