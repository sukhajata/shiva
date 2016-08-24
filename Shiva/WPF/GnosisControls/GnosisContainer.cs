using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.ContentControllers;
using ShivaWPF3.UtilityWPF;
using Shiva.Shared.BaseControllers;

namespace GnosisControls
{
    public partial class GnosisContainer : Border, IGnosisContainerImplementation
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        public static Thickness NormalThickness = new Thickness(0);
        public static Thickness FocusThickness = new Thickness(3);
        public static Thickness HighlightThickness = new Thickness(5);
        public static Thickness NoBorder = new Thickness(0);

        public Grid contentGrid;
        protected GnosisContainerController controller;


        public GnosisContainer()
        {
            contentGrid = new Grid();
            this.Child = contentGrid;

            //this.BorderBrush = (Brush)Application.Current.FindResource("BorderColor");
            this.BorderThickness = GnosisContainer.NormalThickness;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Stretch;

            this.MouseDown += GnosisContainerWPF_MouseDown;
            this.MouseUp += GnosisContainerWPF_MouseUp;
            this.MouseEnter += GnosisContainerWPF_MouseEnter;
            this.MouseLeave += GnosisContainerWPF_MouseLeave;

            this.PropertyChanged += GnosisContainer_PropertyChanged;
        }

        protected virtual void GnosisContainer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        public void SetOrder(int _order)
        {
            order = _order;
        }

        public int GetOrder()
        {
            return order;
        }

        public void Clear()
        {
            contentGrid.Children.Clear();
        }

        public virtual void Highlight()
        {
            this.BorderBrush = (Brush)Application.Current.FindResource("BorderHighlightColor");
            this.BorderThickness = GnosisContainer.HighlightThickness;
        }

        public virtual void UnHighlight()
        {
            this.BorderBrush = (Brush)Application.Current.FindResource("BorderColor");
            this.BorderThickness = GnosisContainer.NormalThickness;
        }


        //public void SetVisible(bool visible)
        //{
        //    if (visible)
        //    {
        //        this.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        this.Visibility = Visibility.Collapsed;
        //    }
        //}

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.Padding = new Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Right, paddingVertical);
        }

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetOutlineColour(string outlineColour)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveOutlineColour()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisContainerWPF_MouseDown;
        //}

        private void GnosisContainerWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    //MouseUpHandler = action;
        //    this.MouseUp += GnosisContainerWPF_MouseUp;
        //}

        private void GnosisContainerWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisContainerWPF_MouseEnter;
        //}

        private void GnosisContainerWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisContainerWPF_MouseLeave;

        //}

        private void GnosisContainerWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetTooltip(string toolTip)
        //{
        //    this.ToolTip = toolTip;
       // }

        //public void SetController(GnosisVisibleController gnosisVisibleController)
        //{
        //    controller = (GnosisContainerController)gnosisVisibleController;
        //}

        //public void SetFont(string font)
        //{

        //}

        //public void SetFontSize(int fontSize)
        //{

        //}

        //public void SetForegroundColour(string contentColour)
        //{

        //}

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisContainerWPF_GotFocus;
        }

        private void GnosisContainerWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisContainerWPF_LostFocus;
        }

        private void GnosisContainerWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public virtual void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }


        //public void SetController(GnosisController _controller)
        //{
        //    controller = (GnosisContainerController)_controller;
        //}
    }
}
