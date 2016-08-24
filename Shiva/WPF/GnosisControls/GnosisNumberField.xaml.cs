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
using Shiva.Shared.Interfaces;
using System.ComponentModel;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using ShivaWPF3.UtilityWPF;
using System.Text.RegularExpressions;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisNumberFields.xaml
    /// </summary>
    public partial class GnosisNumberField : TextBox, IGnosisNumberFieldImplementation
    {
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        protected Action<string> TextChangedHandler;

        protected bool userInput = true;

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
           typeof(int), typeof(GnosisNumberField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisNumberField panelField = source as GnosisNumberField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin
                marginHorizontal = panelField.Margin.Left - newThickness;
                marginVertical = panelField.Margin.Top - newThickness;

                if (marginHorizontal >= 0 && marginVertical >= 0)
                {
                    panelField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                    panelField.BorderThickness = new Thickness(newThickness);
                    panelField.Height = panelField.Height + (newThickness - oldThickness);
                }
            }
            else
            {
                //decrease border thickness, increase margin
                marginHorizontal = panelField.Margin.Left + oldThickness;
                marginVertical = panelField.Margin.Top + oldThickness;

                if (marginHorizontal >= 0 && marginVertical >= 0)
                {
                    panelField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                    panelField.BorderThickness = new Thickness(newThickness);
                    panelField.Height = panelField.Height - (oldThickness - newThickness);
                }
            }

           
        }

        public GnosisNumberField()
        {
            InitializeComponent();

            this.HorizontalAlignment = HorizontalAlignment.Left;

            this.MouseEnter += GnosisNumberFieldWPF_MouseEnter;
            this.MouseLeave += GnosisNumberFieldWPF_MouseLeave;
            this.PreviewMouseDown += GnosisNumberFieldWPF_MouseDown;
            this.PreviewMouseUp += GnosisNumberFieldWPF_MouseUp;

            this.PropertyChanged += GnosisNumberField_PropertyChanged;
        }

        private void GnosisNumberField_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
                    this.Tooltip = tooltip;
                    break;


            }
        }

        public double GetNumber()
        {
            return Convert.ToDouble(this.Text);
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }


        public void SetNumber(double number)
        {
            userInput = false;
            this.Text = number.ToString();
            userInput = true;
        }
        
        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }
        
        private void GnosisNumberFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HasMouseFocus = true;
        }

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);

        }

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }
        
        private void GnosisNumberFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetTextChangedHandler(Action<string> handler)
        {
            TextChangedHandler = handler;
            this.TextChanged += GnosisTextFieldWPF_TextChanged;
        }

        private void GnosisTextFieldWPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userInput)
            {
                TextChangedHandler.Invoke(this.Text);
            }
        }

        
        private void GnosisNumberFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HasMouseDown = true;
        }
        
        private void GnosisNumberFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HasMouseDown = false;
        }
        
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
        
        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.SetVerticalPaddingExt(paddingVertical);
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
        
        public void SetHeight(double height)
        {
            this.Height = height;
        }

        public int GetRowNumber()
        {
            return Grid.GetRow(this);
        }

        public void SetWidth(double width)
        {
            this.Width = width;
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }

        public double GetWidth()
        {
            return this.ActualWidth;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisTextFieldWPF_GotFocus;
        }

        private void GnosisTextFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisTextFieldWPF_LostFocus;
        }

        private void GnosisTextFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }
        
        public void GnosisAddChild(IGnosisMouseVisibleControlImplementation child)
        {
            throw new NotImplementedException();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        //Check if pasted text is allowed
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
        
    }
}
