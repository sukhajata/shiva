using ShivaShared3.Interfaces;
using ShivaShared3.PanelFieldControllers;
using ShivaWPF3.UtilityWPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ShivaShared3.BaseControllers;
using System.ComponentModel;
using ShivaShared3.Data;
using System.Windows.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisCheckFieldWPF.xaml
    /// </summary>
    public partial class GnosisCheckField : Border, IGnosisCheckFieldImplementation
    {
        //private Action GotMouseFocusHandler;
        //private Action LostMouseFocusHandler;
        //private Action MouseDownHandler;
        //private Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;



        public GnosisCheckField()
        {
            InitializeComponent();

            this.MouseEnter += GnosisCheckFieldWPF_MouseEnter;
            this.MouseLeave += GnosisCheckFieldWPF_MouseLeave;
            this.MouseDown += GnosisCheckFieldWPF_MouseDown;
            this.MouseUp += GnosisCheckFieldWPF_MouseUp;

            Binding binding = new Binding("GnosisChecked");
            binding.Source = this;
            binding.Mode = BindingMode.TwoWay;
            chkBox.SetBinding(CheckBox.IsCheckedProperty, binding);

            this.PropertyChanged += GnosisCheckField_PropertyChanged;
        }

        private void GnosisCheckField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    lblCaption.Content = caption;
                    break;
                case "ContentVerticalAlignment":
                    lblCaption.SetVerticalContentAlignmentExt(contentVerticalAlignment);
                    break;
                case "ContentHorizontalAlignment":
                    lblCaption.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Locked":
                    if (!readOnly)
                    {
                        chkBox.IsEnabled = !locked;
                    }
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
                case "ReadOnly":
                    chkBox.IsEnabled = !readOnly;
                    break;
            }
        }

        //public void SetIsChecked(bool isChecked)
        //{
        //    chkBox.IsChecked = isChecked;
        //}

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }


        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new System.Windows.Thickness(1);
        //}

        //public void SetCaption(string caption)
        //{
        //    lblCaption.Content = caption;
        //}

        //public void SetFont(string font)
        //{
        //    lblCaption.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    lblCaption.FontSize = fontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    lblCaption.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisCheckFieldWPF_MouseEnter;
        //}

        private void GnosisCheckFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);

        }

        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    switch (horizontalAlignment)
        //    {
        //        case GnosisController.HorizontalAlignmentType.LEFT:
        //            lblCaption.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
        //            break;
        //        case GnosisController.HorizontalAlignmentType.CENTRE:
        //            lblCaption.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
        //            break;
        //        case GnosisController.HorizontalAlignmentType.RIGHT:
        //            lblCaption.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
        //            break;
        //    }
        //}

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    this.IsEnabled = IsEnabled;
        //}

        //public void SetLocked(bool locked)
        //{
        //    this.IsEnabled = !locked;
        //    Locked = locked;
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisCheckFieldWPF_MouseLeave;
        //}

        private void GnosisCheckFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetMaxWidth(int maxWidth)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisCheckFieldWPF_MouseDown;
        //}

        private void GnosisCheckFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisCheckFieldWPF_MouseUp;
        //}

        private void GnosisCheckFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    this.BorderThickness = new System.Windows.Thickness(2);
        //}

        //public void RemoveOutlineColour()
        //{
        //    this.BorderThickness = new System.Windows.Thickness(0);
        //}

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    switch (verticalAlignment)
        //    {
        //        case GnosisController.VerticalAlignmentType.Top:
        //            lblCaption.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
        //            break;
        //        case GnosisController.VerticalAlignmentType.Centre:
        //            lblCaption.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        //            break;
        //        case GnosisController.VerticalAlignmentType.Bottom:
        //            lblCaption.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
        //            break;
        //    }
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisCheckFieldController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}


        //public void SetTextLength(int numCharacters)
        //{
        //    this.Width = numCharacters * StyleController.GetCharacterWidth(lblCaption.FontFamily, lblCaption.FontSize, lblCaption.FontStyle, lblCaption.FontWeight, lblCaption.FontStretch);
        //}

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.Padding = new Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Right, paddingVertical);
        }


        //public FontFamily GetFontFamily()
        //{
        //    return lblCaption.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return lblCaption.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return lblCaption.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return lblCaption.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return lblCaption.FontStretch;
        //}

        public double GetPaddingHorizontal()
        {
            return lblCaption.Padding.Left;
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
            this.GotFocus += GnosisCheckFieldWPF_GotFocus;
        }

        private void GnosisCheckFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisCheckFieldWPF_LostFocus;
        }

        private void GnosisCheckFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }

        //public bool GetIsChecked()
        //{
        //    return (bool)this.chkBox.IsChecked;
        //}

        public void SetStrikethrough(bool strikethrough)
        {
            
        }


    }
}
