using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shiva.Shared.Interfaces;
using System.Windows.Controls;
using ShivaWPF3.UtilityWPF;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

namespace GnosisControls
{
    public partial class GnosisGridCheckField : Border, IGnosisGridCheckFieldImplementation, INotifyPropertyChanged
    {
        private CheckBox checkBox;

        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        protected int horizontalPadding;
        protected int verticalPadding;

        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set
            {
                horizontalPadding = value;
                this.SetHorizontalPaddingExt(horizontalPadding);
            }
        }

        public int VerticalPadding
        {
            get { return verticalPadding; }
            set
            {
                verticalPadding = value;
                this.SetVerticalPaddingExt(verticalPadding);
            }
        }

        public static readonly DependencyProperty ControlThicknessProperty =
            DependencyProperty.RegisterAttached("ControlThickness",
            typeof(int), typeof(GnosisGridCheckField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
        //new FrameworkPropertyMetadata(0,
        //    FrameworkPropertyMetadataOptions.Inherits));

        public static void SetControlThickness(UIElement element, int value)
        {
            element.SetValue(ControlThicknessProperty, value);
        }

        public static int GetControlThickness(UIElement element)
        {
            return (int)element.GetValue(ControlThicknessProperty);
        }

        public static void ControlThicknessPropertyChanged(object source, DependencyPropertyChangedEventArgs e)
        {
            GnosisGridCheckField panelField = source as GnosisGridCheckField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin
                marginHorizontal = panelField.Margin.Left - newThickness;
                marginVertical = panelField.Margin.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase margin
                marginHorizontal = panelField.Margin.Left + oldThickness;
                marginVertical = panelField.Margin.Top + oldThickness;
            }

            panelField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
            panelField.BorderThickness = new Thickness(newThickness);

        }

        public GnosisGridCheckField()
        {
            checkBox = new CheckBox();
            checkBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            checkBox.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.MouseEnter += GnosisTextFieldWPF_MouseEnter;
            this.MouseLeave += GnosisTextFieldWPF_MouseLeave;
            this.PreviewMouseDown += GnosisTextFieldWPF_MouseDown;
            this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;

            Binding binding = new Binding("GnosisChecked");
            binding.Source = this;
            binding.Mode = BindingMode.OneWay;
            checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);

            this.Child = checkBox;

            this.PropertyChanged += GnosisGridCheckField_PropertyChanged;
        }

        private void GnosisGridCheckField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Caption":
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Locked":
                    if (!readOnly)
                    {
                        checkBox.IsEnabled = !locked;
                    }
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
                case "ReadOnly":
                    checkBox.IsEnabled = !readOnly;
                    break;
            }
        }

        //public void SetController(GnosisVisibleController _controller)
        //{
        //    controller = (GnosisGridFieldController)_controller;
        //}


        public double GetWidth()
        {
            return this.ActualWidth;
        }


        //public void SetLocked(bool locked)
        //{
        //    this.IsEnabled = !locked;
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

        public void SetCaption(string caption)
        {

        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public void SetFont(string font)
        {
            //  this.FontFamily = new System.Windows.Media.FontFamily(font);
        }

        public void SetFontSize(int fontSize)
        {
            //  this.FontSize = FontSize;
        }

        public void SetForegroundColour(string contentColour)
        {
          //  this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisTextFieldWPF_MouseEnter;
        //}

        private void GnosisTextFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
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
            //breaks textbox filling available space
            this.SetHorizontalAlignmentExt(horizontalAlignment);

        }
        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
           // this.SetHorizontalContentAlignmentExt(horizontalAlignment);

        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    this.IsEnabled = IsEnabled;
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisTextFieldWPF_MouseLeave;
        //}

        private void GnosisTextFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
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
        //    this.MaxWidth = MaxWidth;
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.PreviewMouseDown += GnosisTextFieldWPF_MouseDown;
        //}

        private void GnosisTextFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;
        //}

        private void GnosisTextFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        public void SetOutlineColour(string outlineColour)
        {
            this.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
            this.BorderThickness = new System.Windows.Thickness(2);
        }

        //public void RemoveOutlineColour()
        //{
        //    this.BorderThickness = new System.Windows.Thickness(0);
        //}

        //public void SetStrikethrough()
        //{

        //}

        public void SetTooltip(string tooltip)
        {
            this.ToolTip = tooltip;
        }

        public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
          //  this.SetVerticalContentAlignmentExt(verticalAlignment);

        }

        public void SetVisible(bool visible)
        {
            this.SetVisibleExt(visible);

        }

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        public void SetWidth(double width)
        {
            this.Width = width;
        }


        public void SetPaddingHorizontal(double paddingHorizontal)
        {
           // this.SetPaddingHorizontalExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
          //  this.SetPaddingVerticalExt(paddingVertical);
        }

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public void SetMinWidth(double minWidth)
        {
            this.MinWidth = minWidth;
        }

        public void SetMaxWidth(double maxWidth)
        {
            this.MaxWidth = maxWidth;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisGridCheckFieldWPF_GotFocus;
        }

        private void GnosisGridCheckFieldWPF_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }


        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisGridCheckFieldWPF_LostFocus;
        }

        private void GnosisGridCheckFieldWPF_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }
        

        public void SetReadOnly(bool isReadOnly)
        {
            this.IsEnabled = !isReadOnly;
        }

        public void SetChecked(bool selected)
        {
            checkBox.IsChecked = selected;
        }

        public bool GetIsChecked()
        {
            return (bool)checkBox.IsChecked;
        }

        public void SetStrikethrough(bool strikethrough)
        {
            
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }

    }
}
