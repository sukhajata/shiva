using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ShivaShared3.BaseControllers;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;

namespace GnosisControls
{
    public partial class GnosisGridTextField : TextBox, IGnosisGridTextFieldImplementation, INotifyPropertyChanged
    {
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
            typeof(int), typeof(GnosisGridTextField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisGridTextField textField = source as GnosisGridTextField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin
                marginHorizontal = textField.Margin.Left - newThickness;
                marginVertical = textField.Margin.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase margin
                marginHorizontal = textField.Margin.Left + oldThickness;
                marginVertical = textField.Margin.Top + oldThickness;
            }

            textField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
            textField.BorderThickness = new Thickness(newThickness);

        }



        public int NumLines
        {
            get; set;
        }

     

        public GnosisGridTextField()
        {
           // this.Background = Brushes.Transparent;  //allow alternate row colour to show
            this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.Margin = new System.Windows.Thickness(0, 0, 1, 1);

            this.MouseEnter += GnosisTextFieldWPF_MouseEnter;
            this.MouseLeave += GnosisTextFieldWPF_MouseLeave;
            this.PreviewMouseDown += GnosisTextFieldWPF_MouseDown;
            this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;

            this.PropertyChanged += GnosisGridTextField_PropertyChanged;

        }

        protected virtual void GnosisGridTextField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "ContentVerticalAlignment":
                    this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
                    break;
                case "ContentHorizontalAlignment":
                    this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Locked":
                    if (!readOnly)
                    {
                        this.IsReadOnly = locked;
                    }
                    break;
                case "MaxChars":
                    this.MaxLength = maxChars;
                    break;
                case "ReadOnly":
                    this.IsReadOnly = readOnly;
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        //public void SetController(GnosisVisibleController _controller)
        //{
        //    controller = (GnosisGridFieldController)_controller;
        //}

        public void CentreText()
        {
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        }

        public string GetText()
        {
            return this.Text;
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }


        public void SetText(string text)
        {
            this.Text = text;
        }

        public void SetMaxLines(int maxLines)
        {
            this.MaxLines = maxLines;
        }

        //public void SetLocked(bool locked)
        //{
        //    this.IsReadOnly = locked;
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

        //public void SetCaption(string caption)
        //{

        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetFont(string font)
        //{
        //    this.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    this.FontSize = fontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

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

        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    this.SetHorizontalContentAlignmentExt(horizontalAlignment);
            
        //}

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
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;
        //}

        private void GnosisTextFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
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

        //public void SetStrikethrough()
        //{

        //}

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    this.SetVerticalContentAlignmentExt(verticalAlignment);
            
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

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
            this.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.SetVerticalPaddingExt(paddingVertical);
        }

        //public FontFamily GetFontFamily()
        //{
        //    return this.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return this.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return this.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return this.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return this.FontStretch;
        //}

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
            this.GotFocus += GnosisGridTextFieldWPF_GotFocus;
        }

        private void GnosisGridTextFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisGridTextFieldWPF_LostFocus;
        }

        private void GnosisGridTextFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetReadOnly(bool isReadOnly)
        //{
        //    this.IsReadOnly = isReadOnly;
        //}

        public void SetStrikethrough(bool strikethrough)
        {

            if (strikethrough)
            {
                this.TextDecorations = System.Windows.TextDecorations.Strikethrough;
            }
            else
            {
                this.TextDecorations = null;
            }
        }

        public void SetTextWrapping(bool wrap)
        {
            if (wrap)
            {
                this.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                this.TextWrapping = TextWrapping.NoWrap;
            }
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }

        public double GetWidth()
        {
            return this.ActualWidth;
        }


    }
}
