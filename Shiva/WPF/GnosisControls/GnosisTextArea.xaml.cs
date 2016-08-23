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
using ShivaWPF3.UtilityWPF;
using ShivaShared3.Interfaces;
using ShivaShared3.BaseControllers;
using System.ComponentModel;
using ShivaShared3.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTextAreaWPF.xaml
    /// </summary>
    public partial class GnosisTextArea : Border, IGnosisTextAreaImplementation, INotifyPropertyChanged
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        protected Action<string> TextChangedHandler;
       
        private bool userInput = true;
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

        public GnosisTextArea()
        {
            InitializeComponent();

            this.MouseEnter += GnosisTextAreaWPF_MouseEnter;
            this.MouseLeave += GnosisTextAreaWPF_MouseLeave;
            this.PreviewMouseDown += GnosisTextAreaWPF_MouseDown;
            this.PreviewMouseUp += GnosisTextAreaWPF_MouseUp;

            this.PropertyChanged += GnosisTextArea_PropertyChanged;

        }

        private void GnosisTextArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Caption":
                    break;
                case "ContentVerticalAlignment":
                    txt.SetVerticalContentAlignmentExt(contentVerticalAlignment);
                    break;
                case "ContentHorizontalAlignment":
                    txt.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
                    break;
                case "MaxChars":
                    txt.MaxLength = maxChars;
                    break;
                case "HasScrollBar":
                    if (hasScrollBar)
                    {
                        scrollviewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    }
                    else
                    {
                        scrollviewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    }
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Locked":
                    if (!readOnly)
                    {
                        txt.IsReadOnly = locked;
                    }
                    break;
                case "ReadOnly":
                    txt.IsReadOnly = readOnly;
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        public string GetText()
        {
            return txt.Text;
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        //public void SetMarginLeft(int marginLeft)
        //{
        //    this.Margin = new Thickness(marginLeft, this.Margin.Top, this.Margin.Right, this.Margin.Bottom);
        //}

        public void SetText(string text)
        {
            userInput = false;
            txt.Text = text;
            userInput = true;
        }

        public void SetMaxLines(int maxLines)
        {
            txt.MaxLines = maxLines;
        }

        public int GetLineCount()
        {
            return txt.LineCount;
        }


        //public void SetLocked(bool locked)
        //{
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
        //    txtBox.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    txtBox.FontSize = FontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    txtBox.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisTextAreaWPF_MouseEnter;
        //}

        private void GnosisTextAreaWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
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
        //    this.MouseLeave += GnosisTextAreaWPF_MouseLeave;
        //}

        private void GnosisTextAreaWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetTextChangedHandler(Action<string> handler)
        {
            TextChangedHandler = handler;
            txt.TextChanged += GnosisTextAreaWPF_TextChanged;
        }

        private void GnosisTextAreaWPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userInput)
            {
                TextChangedHandler.Invoke(txt.Text);
            }
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
        //    this.PreviewMouseDown += GnosisTextAreaWPF_MouseDown;
        //}

        private void GnosisTextAreaWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisTextAreaWPF_MouseUp;
        //}

        private void GnosisTextAreaWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        public void SetStrikethrough(bool strikethrough)
        {
            if (strikethrough)
            {
                txt.TextDecorations = System.Windows.TextDecorations.Strikethrough;
            }
            else
            {
                txt.TextDecorations = null;
            }
        }

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


        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            txt.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            txt.SetVerticalPaddingExt(paddingVertical);
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

        //public void SetMaxChars(int maxChars)
        //{
        //    txt.MaxLength = maxChars;
        //}

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

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisTextAreaWPF_GotFocus;
        }

        private void GnosisTextAreaWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            this.HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisTextAreaWPF_LostFocus;
        }

        private void GnosisTextAreaWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetTextWrapping(bool wrap)
        {
            if (wrap)
            {
                txt.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                txt.TextWrapping = TextWrapping.NoWrap;
            }
        }

        public void SetMarginLeft(int horizontalSpacing)
        {
            this.Margin = new Thickness(horizontalSpacing, 0, 0, 0);
        }
    }
}
