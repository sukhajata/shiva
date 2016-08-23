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
using System.ComponentModel;
using ShivaShared3.Interfaces;
using ShivaWPF3.UtilityWPF;
using ShivaShared3.BaseControllers;
using ShivaShared3.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisComboFieldWPF.xaml
    /// </summary>
    public partial class GnosisComboField : ComboBox, IGnosisComboFieldImplementation
    {
        private Action<GnosisComboOption> optionChangedHandler;
        //private Action GotMouseFocusHandler;
        //private Action LostMouseFocusHandler;
        //private Action MouseDownHandler;
        //private Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

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
         typeof(int), typeof(GnosisComboField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisComboField panelField = source as GnosisComboField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease padding
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
                //decrease border thickness, increase padding
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

        public GnosisComboField()
        {
            InitializeComponent();

            cbo.MouseEnter += GnosisComboFieldWPF_MouseEnter;
            cbo.MouseLeave += GnosisComboFieldWPF_MouseLeave;
            cbo.PreviewMouseDown += GnosisComboFieldWPF_MouseDown;
            cbo.PreviewMouseUp += GnosisComboFieldWPF_MouseUp;

            this.PropertyChanged += GnosisComboField_PropertyChanged;
        }

        private void GnosisComboField_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
                case "Disabled":
                    this.IsEnabled = !disabled;
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Locked":
                    if (!readOnly)
                    {
                        cbo.IsReadOnly = locked;
                    }
                    break;
                case "ReadOnly":
                    cbo.IsReadOnly = readOnly; ;
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        public void LoadComboOptionImplementations(List<GnosisComboOption> comboOptionImplementations)
        {
            foreach (GnosisComboOption comboOptionImp in comboOptionImplementations)
            {
                cbo.Items.Add(comboOptionImp);
            }

        }


        public void SetOptionChangedHandler(Action<GnosisComboOption> _optionChangedHandler)
        {
            optionChangedHandler = _optionChangedHandler;
            cbo.SelectionChanged += GnosisComboFieldWPF_SelectionChanged;

        }

        private void GnosisComboFieldWPF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GnosisComboOption selectedOption = (GnosisComboOption)cbo.SelectedItem;
            optionChangedHandler.Invoke(selectedOption);
        }

        public double GetWidth()
        {
            return cbo.ActualWidth;
        }

        public double GetHeight()
        {
            return cbo.ActualHeight;
        }

       

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(cbo, visible);
        }

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    cbo.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    cbo.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    cbo.BorderThickness = new System.Windows.Thickness(2);
        //}

        //public void SetCaption(string caption)
        //{

        //}

        //public void SetFont(string font)
        //{
        //    cbo.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    cbo.FontSize = FontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    cbo.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    cbo.MouseEnter += GnosisComboFieldWPF_MouseEnter;
        //}

        private void GnosisComboFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }


        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            cbo.SetHorizontalAlignmentExt(horizontalAlignment);

        }

        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    cbo.SetHorizontalContentAlignmentExt(horizontalAlignment);

        //}

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            cbo.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    cbo.IsEnabled = isEnabled;
        //}

        //public void SetLocked(bool locked)
        //{
        //    cbo.IsEnabled = !locked;
        //    Locked = locked;
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    cbo.MouseLeave += GnosisComboFieldWPF_MouseLeave;
        //}

        private void GnosisComboFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }


        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    cbo.PreviewMouseDown += GnosisComboFieldWPF_MouseDown;
        //}

        private void GnosisComboFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    cbo.PreviewMouseUp += GnosisComboFieldWPF_MouseUp;
        //}

        private void GnosisComboFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    cbo.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    cbo.BorderThickness = new System.Windows.Thickness(2);
        //}

        //public void RemoveOutlineColour()
        //{
        //    cbo.BorderThickness = new System.Windows.Thickness(0);
        //}

        public void SetStrikethrough(bool strikethrough)
        {
            foreach (var item in cbo.Items)
            {
                GnosisComboOption option = item as GnosisComboOption;
                //TextBox txtBox = (TextBox)option.Template.FindName("PART_EditableTextBox", option);

                if (strikethrough)
                {
                    option.TextDecorations = TextDecorations.Strikethrough;
                }
                else
                {
                    option.TextDecorations = null;
                }
            }

        }

        //public void SetTooltip(string tooltip)
        //{
        //    cbo.ToolTip = tooltip;
        //}

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    cbo.SetVerticalContentAlignmentExt(verticalAlignment);

        //}

        //public void SetVisible(bool visible)
        //{
        //    cbo.SetVisibleExt(visible);

        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisComboFieldController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        //public void SetTextLength(int numCharacters)
        //{
        //    cbo.Width = numCharacters * StyleController.GetCharacterWidth(cbo.FontFamily, cbo.FontSize, cbo.FontStyle, cbo.FontWeight, cbo.FontStretch);
        //}

        public void SetSelectedOption(GnosisComboOption selectedOption)
        {
            cbo.SelectedItem = selectedOption;
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            cbo.SetHorizontalPaddingExt(paddingHorizontal);

        }

        public void SetPaddingVertical(double paddingVertical)
        {
            cbo.SetVerticalPaddingExt(paddingVertical);

        }

        //public FontFamily GetFontFamily()
        //{
        //    return cbo.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return cbo.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return cbo.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return cbo.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return cbo.FontStretch;
        //}

        public double GetPaddingHorizontal()
        {
            return cbo.Padding.Left;
        }

        public void SetMinWidth(double minWidth)
        {
            cbo.MinWidth = minWidth;
        }

        public void SetMaxWidth(double maxWidth)
        {
            cbo.MaxWidth = maxWidth;
        }

        public void SetWidth(double width)
        {
            cbo.Width = width;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            cbo.GotFocus += GnosisComboFieldWPF_GotFocus;
        }

        private void GnosisComboFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            cbo.LostFocus += GnosisComboFieldWPF_LostFocus;
        }

        private void GnosisComboFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public GnosisComboOption GetSelectedOption()
        {
            return (GnosisComboOption)cbo.SelectedItem;
        }

        public void SetHeight(double fieldHeight)
        {
            cbo.Height = fieldHeight;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (optional)
            {
                if (e.Key == Key.Back || e.Key == Key.Delete)
                {
                    TextBox txtBox = sender as TextBox;
                    txtBox.Text = string.Empty;
                }
            }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisComboAttribute)
            {
                if (comboAttributes == null)
                {
                    comboAttributes = new List<GnosisComboAttribute>();
                }
                comboAttributes.Add((GnosisComboAttribute)child);
            }
        }
    }

   

}
