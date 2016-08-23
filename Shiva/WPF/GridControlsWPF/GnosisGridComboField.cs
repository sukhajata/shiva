using System;
using System.Windows.Controls;
using ShivaShared3.Interfaces;
using ShivaShared3.GridFieldControllers;
using ShivaWPF3.UtilityWPF;
using System.Windows;
using ShivaShared3.BaseControllers;
using System.ComponentModel;

namespace GnosisControls
{
    public partial class GnosisGridComboField : GnosisComboField, IGnosisGridComboFieldImplementation, INotifyPropertyChanged
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        //protected Action GotFocusHandler;
        //protected Action LostFocusHandler;
        //private bool locked;
        //private bool hasFocus;
        //private bool hasMouseFocus;
        //private bool hasMouseDown;

       
       
        public GnosisGridComboField() : base()
        {
            this.MouseEnter += GnosisGridComboFieldWPF_MouseEnter;
            this.MouseLeave += GnosisGridComboFieldWPF_MouseLeave;
            this.MouseDown += GnosisGridComboFieldWPF_MouseDown;
            this.MouseUp += GnosisGridComboFieldWPF_MouseUp;

        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }


        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        //public void RemoveOutlineColour()
        //{
        //    this.BorderThickness = new System.Windows.Thickness(0);
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new System.Windows.Thickness(1);
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisGridComboFieldController)gnosisLayoutController;
        //}

        public void SetFont(string font)
        {
            this.FontFamily = new System.Windows.Media.FontFamily(font);
        }

        public void SetFontSize(int fontSize)
        {
            this.FontSize = fontSize;
        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public void SetForegroundColour(string contentColour)
        {
            this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisGridComboFieldWPF_MouseEnter;
        //}

        private void GnosisGridComboFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }


        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            throw new NotImplementedException();
        }

        public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalContentAlignmentExt(horizontalAlignment);
        }

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetLocked(bool locked)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisGridComboFieldWPF_MouseLeave;
        //}

        private void GnosisGridComboFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisGridComboFieldWPF_MouseDown;
        //}

        private void GnosisGridComboFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisGridComboFieldWPF_MouseUp;
        //}

        private void GnosisGridComboFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    throw new NotImplementedException();
        //}

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.Padding = new Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Right, paddingVertical);
        }

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }


        public void SetTooltip(string tooltip)
        {
            this.ToolTip = tooltip;
        }

        public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalContentAlignmentExt(verticalAlignment);
        }

        public void SetVisible(bool visible)
        {
            this.SetVisibleExt(visible);
        }

        public void SetMinWidth(double minWidth)
        {
            this.MinWidth = minWidth;
        }

        public void SetMaxWidth(double maxWidth)
        {
            this.MaxWidth = maxWidth;
        }
        public void SetWidth(double width)
        {
            this.Width = width;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisGridComboFieldWPF_GotFocus;
        }

        private void GnosisGridComboFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisGridComboFieldWPF_LostFocus;
        }

        private void GnosisGridComboFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetReadOnly(bool isReadOnly)
        {
            this.IsReadOnly = IsReadOnly;
        }

        public void SetStrikethrough(bool strikethrough)
        {
            
        }

        public double GetWidth()
        {
            return this.ActualWidth;
        }

    }
}
