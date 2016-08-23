using ShivaShared3.Data;
using ShivaShared3.Interfaces;
using ShivaShared3.PanelFieldControllers;
using ShivaWPF3.UtilityWPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ShivaShared3.BaseControllers;
using System.ComponentModel;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisDateFieldWPF.xaml
    /// </summary>
    public partial class GnosisDateField : UserControl, IGnosisDateFieldImplementation, INotifyPropertyChanged
    {
        //private GnosisDateFieldController controller;

        //private Action GotMouseFocusHandler;
        //private Action LostMouseFocusHandler;
        //private Action MouseDownHandler;
        //private Action MouseUpHandler;
        private Action GotFocusHandler;
        private Action LostFocusHandler;

        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;

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

        public int HorizontalMargin
        {
            get { return horizontalMargin; }
            set
            {
                horizontalMargin = value;
                this.SetHorizontalMarginExt(horizontalMargin);
            }
        }

        public int VerticalMargin
        {
            get { return verticalMargin; }
            set
            {
                verticalMargin = value;
                this.SetVerticalMarginExt(verticalMargin);
            }
        }

        public static readonly DependencyProperty ControlThicknessProperty =
             DependencyProperty.RegisterAttached("ControlThickness",
             typeof(int), typeof(GnosisDateField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisDateField panelField = source as GnosisDateField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease padding
                marginHorizontal = panelField.Margin.Left - newThickness;
                marginVertical = panelField.Margin.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase padding
                marginHorizontal = panelField.Margin.Left + oldThickness;
                marginVertical = panelField.Margin.Top + oldThickness;
            }

            panelField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
            panelField.BorderThickness = new Thickness(newThickness);

        }

        public GnosisDateField()
        {
            InitializeComponent();

            this.MouseDown += GnosisDateFieldWPF_MouseDown;
            this.MouseUp += GnosisDateFieldWPF_MouseUp;
            this.MouseEnter += GnosisDateFieldWPF_MouseEnter;
            this.MouseLeave += GnosisDateFieldWPF_MouseLeave;

            this.PropertyChanged += GnosisDateField_PropertyChanged;
        }

        private void GnosisDateField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "ContentVerticalAlignment":
                    SetVerticalContentAlignment(contentVerticalAlignment);
                    break;
                case "ContentHorizontalAlignment":
                    SetHorizontalContentAlignment(contentHorizontalAlignment);
                    break;
                case "Hidden":
                    this.SetVisible(!hidden);
                    break;
                case "Locked":
                    if (locked || readOnly)
                    {
                        //replace datepicker with textbox
                        string text = picker.Text;
                        picker.Visibility = System.Windows.Visibility.Collapsed;

                        txtDate.Visibility = System.Windows.Visibility.Visible;
                        txtDate.Text = text;
                    }
                    else
                    {
                        if (txtDate.Visibility == System.Windows.Visibility.Visible)
                        {
                            string text = txtDate.Text;
                            txtDate.Visibility = System.Windows.Visibility.Collapsed;

                            picker.SelectedDate = Convert.ToDateTime(text);
                            picker.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                    break;
                case "LongDateFormat":
                    if (longDateFormat)
                    {
                        picker.SelectedDateFormat = DatePickerFormat.Long;
                    }
                    else
                    {
                        picker.SelectedDateFormat = DatePickerFormat.Short;
                    }
                    break;
                case "Tooltip":
                    this.ToolTip = toolip;
                    break;
            }

        }

        public void SetDate(DateTime date)
        {
            if (Locked)
            {
                picker.SelectedDate = date;
            }
            else
            {
                txtDate.IsReadOnly = false;
                if (picker.SelectedDateFormat == DatePickerFormat.Short)
                {
                    txtDate.Text = date.ToShortDateString();
                }
                else
                {
                    txtDate.Text = date.ToLongDateString();
                }
                txtDate.IsReadOnly = true;
            }
        }

        //public void SetDateFormat(GlobalData.GnosisDateFormat dateFormat)
        //{
        //    if (dateFormat == GlobalData.GnosisDateFormat.SHORT)
        //    {
        //        picker.SelectedDateFormat = DatePickerFormat.Short;
        //    }
        //    else if (dateFormat == GlobalData.GnosisDateFormat.LONG)
        //    {
        //        picker.SelectedDateFormat = DatePickerFormat.Long;
        //    }

        //}

        //public void SetLocked(bool locked)
        //{
        //    if (locked)
        //    {
        //        //replace datepicker with textbox
        //        string text = picker.Text;
        //        picker.Visibility = System.Windows.Visibility.Collapsed;

        //        txtDate.Visibility = System.Windows.Visibility.Visible;
        //        txtDate.Text = text;
        //    }
        //    else
        //    {
        //        if (txtDate.Visibility == System.Windows.Visibility.Visible)
        //        {
        //            string text = txtDate.Text;
        //            txtDate.Visibility = System.Windows.Visibility.Collapsed;

        //            picker.SelectedDate = Convert.ToDateTime(text);
        //            picker.Visibility = System.Windows.Visibility.Visible;
        //        }
        //    }
        //    Locked = locked;
        //}

        public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {

            switch (verticalAlignment)
            {
                case GnosisController.VerticalAlignmentType.TOP:
                    picker.VerticalContentAlignment = VerticalAlignment.Top;
                    txtDate.VerticalContentAlignment = VerticalAlignment.Top;
                    break;
                case GnosisController.VerticalAlignmentType.CENTRE:
                    picker.VerticalContentAlignment = VerticalAlignment.Center;
                    txtDate.VerticalContentAlignment = VerticalAlignment.Center;
                    break;
                case GnosisController.VerticalAlignmentType.BOTTOM:
                    picker.VerticalContentAlignment = VerticalAlignment.Bottom;
                    txtDate.VerticalContentAlignment = VerticalAlignment.Bottom;
                    break;
                case GnosisController.VerticalAlignmentType.STRETCH:
                    picker.VerticalContentAlignment = VerticalAlignment.Stretch;
                    txtDate.VerticalContentAlignment = VerticalAlignment.Stretch;
                    break;
            }
        }

        public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case GnosisController.HorizontalAlignmentType.LEFT:
                    picker.HorizontalContentAlignment = HorizontalAlignment.Left;
                    txtDate.HorizontalContentAlignment = HorizontalAlignment.Left;
                    break;
                case GnosisController.HorizontalAlignmentType.CENTRE:
                    picker.HorizontalContentAlignment = HorizontalAlignment.Center;
                    txtDate.HorizontalContentAlignment = HorizontalAlignment.Center;
                    break;
                case GnosisController.HorizontalAlignmentType.RIGHT:
                    picker.HorizontalContentAlignment = HorizontalAlignment.Right;
                    txtDate.HorizontalContentAlignment = HorizontalAlignment.Right;
                    break;
                case GnosisController.HorizontalAlignmentType.STRETCH:
                    picker.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    txtDate.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    break;
            }
        }

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    picker.IsEnabled = IsEnabled;
        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }
        //public void SetTextLength(int numCharacters)
        //{
        //    double width = numCharacters * StyleController.GetCharacterWidth(picker.FontFamily, picker.FontSize, picker.FontStyle, picker.FontWeight, picker.FontStretch);
        //    picker.Width = width;
        //    txtDate.Width = width;
        //}

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            
            switch (horizontalAlignment)
            {
                case GnosisController.HorizontalAlignmentType.LEFT:
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case GnosisController.HorizontalAlignmentType.CENTRE:
                    this.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case GnosisController.HorizontalAlignmentType.RIGHT:
                    this.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
                case GnosisController.HorizontalAlignmentType.STRETCH:
                    this.HorizontalAlignment = HorizontalAlignment.Stretch;
                    break;
            }
        }

        public void SetMaxPrintWidth(double maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetCaption(string caption)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        public void SetVisible(bool visible)
        {
            if (visible)
            {
                if (Locked)
                {
                    txtDate.Visibility = Visibility.Visible;
                }
                else
                {
                    picker.Visibility = Visibility.Visible;
                }
            }
            else
            {
                txtDate.Visibility = Visibility.Collapsed;
                picker.Visibility = Visibility.Collapsed;
            }
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisDateFieldWPF_MouseDown;
        //}

        private void GnosisDateFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

            

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisDateFieldWPF_MouseUp;
        //}

        private void GnosisDateFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisDateFieldWPF_MouseEnter;
        //}

        private void GnosisDateFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetBorderColour(string borderColour)
        //{
        //    Brush brush = StyleHelper.GetBrushFromHex(borderColour);
        //    picker.BorderBrush = brush;
        //    picker.BorderThickness = new Thickness(1);

        //    txtDate.BorderBrush = brush;
        //    txtDate.BorderThickness = new Thickness(1);
        //}

        //public void SetFont(string font)
        //{
        //    picker.FontFamily = new FontFamily(font);
        //    txtDate.FontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    picker.FontSize = fontSize;
        //    txtDate.FontSize = fontSize;
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    Brush brush = StyleHelper.GetBrushFromHex(backgroundColour);
        //    picker.Background = brush;
        //    txtDate.Background = brush;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    Brush brush = StyleHelper.GetBrushFromHex(contentColour);
        //    picker.Foreground = brush;
        //    txtDate.Foreground = brush;
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisDateFieldWPF_MouseLeave;
        //}

        private void GnosisDateFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetStrikethrough(bool strikethrough)
        {
            
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveOutlineColour()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisDateFieldController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            picker.Padding = new Thickness(paddingHorizontal, picker.Padding.Top, paddingHorizontal, picker.Padding.Bottom);
            txtDate.Padding = new Thickness(paddingHorizontal, txtDate.Padding.Top, paddingHorizontal, txtDate.Padding.Bottom);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            picker.Padding = new Thickness(picker.Padding.Left, paddingVertical, picker.Padding.Right, paddingVertical);
            txtDate.Padding = new Thickness(txtDate.Padding.Left, paddingVertical, txtDate.Padding.Right, paddingVertical);
        }



        public double GetPaddingHorizontal()
        {
            return picker.Padding.Left;
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
            this.GotFocus += GnosisDateFieldWPF_GotFocus;
        }

        private void GnosisDateFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisDateFieldWPF_LostFocus;
        }

        private void GnosisDateFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
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
