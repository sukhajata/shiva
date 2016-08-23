using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ShivaShared3.GridFieldControllers;
using System.Windows;
using ShivaShared3.BaseControllers;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;

namespace ShivaWPF3.GridControlsWPF
{
    public class GnosisGridTextFieldWPF : TextBox, IGnosisGridTextFieldImplementation, INotifyPropertyChanged
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        private bool locked;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool isEvenRow;
        private bool rowSelected;

        public int Order { get; set; }
        public bool IsEvenRow
        {
            get { return isEvenRow; }
            set
            {
                isEvenRow = value;
                OnPropertyChanged("IsEvenRow");
            }
        }
        public bool RowSelected
        {
            get { return rowSelected; }
            set
            {
                rowSelected = value;
                OnPropertyChanged("RowSelected");
            }
        }
        public bool Locked
        {
            get
            {
                return locked;
            }

            set
            {
                locked = value;
                this.IsReadOnly = locked;
                OnPropertyChanged("Locked");
            }
        }
        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
            }
        }
        public bool HasMouseFocus
        {
            get { return hasMouseFocus; }
            set
            {
                hasMouseFocus = value;
                OnPropertyChanged("HasMouseFocus");
            }
        }
        public bool HasMouseDown
        {
            get { return hasMouseDown; }
            set
            {
                hasMouseDown = value;
                OnPropertyChanged("HasMouseDown");
            }
        }

        public int NumLines
        {
            get; set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public GnosisGridTextFieldWPF()
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

            // txt.MaxLines = 1;

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

        public void SetCaption(string caption)
        {

        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public void SetFont(string font)
        {
            this.FontFamily = new System.Windows.Media.FontFamily(font);
        }

        public void SetFontSize(int fontSize)
        {
            this.FontSize = fontSize;
        }

        public void SetForegroundColour(string contentColour)
        {
            this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
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
            this.SetHorizontalContentAlignmentExt(horizontalAlignment);
            
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

        public void SetStrikethrough()
        {

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
            this.SetPaddingHorizontalExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.SetPaddingVerticalExt(paddingVertical);
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

        public void SetReadOnly(bool isReadOnly)
        {
            this.IsReadOnly = isReadOnly;
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
