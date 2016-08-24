using Shiva.Shared.BaseControllers;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using Shiva.Shared.Data;

namespace GnosisControls
{
    public partial class GnosisMessageGrid : Border, IGnosisMessageGridImplementation, INotifyPropertyChanged
    {

        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        private Grid gridContent;
        //private FontFamily fontFamily;
        //private double fontSize;
        //private Brush fontColour;

      

        public GnosisMessageGrid() : base()
        {
            gridContent = new Grid();
            this.Child = gridContent;

            this.MouseDown += GnosisMessageGridWPF_MouseDown;

            this.MouseUp += GnosisMessageGridWPF_MouseUp;
            this.MouseEnter += GnosisMessageGridWPF_MouseEnter;
            this.MouseLeave += GnosisMessageGridWPF_MouseLeave;

            this.PropertyChanged += GnosisMessageGrid_PropertyChanged;
        }

        private void GnosisMessageGrid_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "CaptionVerticalAlignment":
                    break;
                case "CaptionHorizontalAlignment":
                    break;
                case "CaptionRelativePostion":
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }


        //public void SetCaption(string caption)
        //{

        //}

        //public void SetMarginBottom(int marginBottom)
        //{
        //    this.Margin = new Thickness { Bottom = marginBottom };
        //}

        public void SetMaxWidth(int maxWidth)
        {
            this.MaxWidth = MaxWidth;
        }

        public void SetMinWidth(int minWidth)
        {
            this.MinWidth = minWidth;
        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        //public void SetMessage(string message)
        //{
        //    //content.Content = message;
        //}

        public double GetHeight()
        {
            return this.ActualHeight;
        }


        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }
        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    gridContent.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(borderColour);
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisMessageGridWPF_MouseDown;
        //}

        private void GnosisMessageGridWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisMessageGridWPF_MouseUp;
        //}
            
        private void GnosisMessageGridWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisMessageGridWPF_MouseEnter;
        //}

        private void GnosisMessageGridWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }


        //public void SetFont(string font)
        //{
        //    fontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int _fontSize)
        //{
        //    fontSize = _fontSize;
        //}


        //public void SetForegroundColour(string contentColour)
        //{
        //    fontColour= StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisMessageGridWPF_MouseLeave;
        //}

        private void GnosisMessageGridWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    this.BorderThickness = new Thickness(2);
        //}

        //public void RemoveOutlineColour()
        //{
        //    this.BorderThickness = new Thickness(0);
        //}

        //public void SetMargin(int left, int top, int right, int bottom)
        //{
        //    this.Margin = new System.Windows.Thickness(left, top, right, bottom);
        //}

        //public void SetMargin(int margin)
        //{
        //    this.Margin = new System.Windows.Thickness(margin);
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisMessageGridController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.Padding = new Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Right);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Top, paddingVertical);
        }

        //public FontFamily GetFontFamily()
        //{
        //    return content.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return content.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return content.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return content.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return content.FontStretch;
        //}

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisMessageGridWPF_GotFocus;
        }

        private void GnosisMessageGridWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisMessageGridWPF_LostFocus;
        }

        private void GnosisMessageGridWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetMarginLeft(int horizontalSpacing)
        {
            this.Margin = new Thickness(horizontalSpacing, 0, 0, 0);
        }


    }
}
