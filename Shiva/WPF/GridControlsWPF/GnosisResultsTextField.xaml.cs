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
using ShivaShared3.Interfaces;
using System.ComponentModel;
using ShivaWPF3.UtilityWPF;
using ShivaShared3.BaseControllers;
using System.Windows.Markup;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTextResultsFieldWPF.xaml
    /// </summary>
    public partial class GnosisResultsTextField : GnosisGridTextField
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        //protected Action GotFocusHandler;
        //protected Action LostFocusHandler;

      
        public int NumLines
        {
            get; set;
        }


        public GnosisResultsTextField()
        {
            InitializeComponent();

            //this.MouseEnter += GnosisTextFieldWPF_MouseEnter;
            //this.MouseLeave += GnosisTextFieldWPF_MouseLeave;
            //this.PreviewMouseDown += GnosisTextFieldWPF_MouseDown;
            //this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;

        }

        //public void CentreText()
        //{
        //    this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        //}

        //public string GetText()
        //{
        //    return this.Text;
        //}


        //public void SetText(string text)
        //{
        //    this.Text = text;
        //}

        //public void SetMaxLines(int maxLines)
        //{
        //    this.MaxLines = maxLines;
        //}

       

        //public void SetTooltipVisible(bool visible)
        //{
        //    ToolTipService.SetIsEnabled(this, visible);
        //}

      

        public void SetGotMouseFocusHandler(Action action)
        {
            GotMouseFocusHandler = action;
            this.MouseEnter += GnosisTextFieldWPF_MouseEnter;
        }

        private void GnosisTextFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HasMouseFocus = true;
            GotMouseFocusHandler.Invoke();
        }

        //public void SetHeight(double fieldHeight)
        //{
        //    this.Height = fieldHeight;
        //}



        //public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    //stops textbox from filling available space
        //    this.SetHorizontalAlignmentExt(horizontalAlignment);

        //}
        //public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    this.SetVerticalAlignmentExt(verticalAlignment);
        //}


        public void SetLostMouseFocusHandler(Action action)
        {
            LostMouseFocusHandler = action;
            this.MouseLeave += GnosisTextFieldWPF_MouseLeave;
        }

        private void GnosisTextFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HasMouseFocus = false;
            LostMouseFocusHandler.Invoke();

        }

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetMaxWidth(int maxWidth)
        //{
        //    this.MaxWidth = MaxWidth;
        //}

        public void SetMouseDownHandler(Action action)
        {
            MouseDownHandler = action;
            this.PreviewMouseDown += GnosisTextFieldWPF_MouseDown;
        }

        private void GnosisTextFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HasMouseDown = true;
            MouseDownHandler.Invoke();
        }

        public void SetMouseUpHandler(Action action)
        {
            MouseUpHandler = action;
            this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;
        }

        private void GnosisTextFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HasMouseDown = false;
            MouseUpHandler.Invoke();
        }


        //public void SetWidth(double width)
        //{
        //    this.Width = width;
        //}


        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.SetPaddingHorizontalExt(paddingHorizontal);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    this.SetPaddingVerticalExt(paddingVertical);
        //}

      

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

        //public void SetMinWidth(double minWidth)
        //{
        //    this.MinWidth = minWidth;
        //}

        //public void SetMaxWidth(double maxWidth)
        //{
        //    this.MaxWidth = maxWidth;
        //}

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisGridTextFieldWPF_GotFocus;
        }

        private void GnosisGridTextFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            HasFocus = true;
            GotFocusHandler.Invoke();
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisGridTextFieldWPF_LostFocus;
        }

        private void GnosisGridTextFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            HasFocus = false;
            LostFocusHandler.Invoke();
        }

     

        //public void SetStrikethrough(bool strikethrough)
        //{

        //    if (strikethrough)
        //    {
        //        this.TextDecorations = System.Windows.TextDecorations.Strikethrough;
        //    }
        //    else
        //    {
        //        this.TextDecorations = null;
        //    }
        //}

        //public void SetTextWrapping(bool wrap)
        //{
        //    if (wrap)
        //    {
        //        this.TextWrapping = TextWrapping.Wrap;
        //    }
        //    else
        //    {
        //        this.TextWrapping = TextWrapping.NoWrap;
        //    }
        //}

        //public double GetHeight()
        //{
        //    return this.ActualHeight;
        //}

        //public double GetWidth()
        //{
        //    return this.ActualWidth;
        //}


    }
}
