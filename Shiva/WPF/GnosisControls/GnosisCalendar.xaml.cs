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
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;
using Shiva.Shared.BaseControllers;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisCalendarWPF.xaml
    /// </summary>
    public partial class GnosisCalendar : UserControl, IGnosisCalendarImplementation, INotifyPropertyChanged
    {
        
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

       

        public GnosisCalendar()
        {
            InitializeComponent();

            this.MouseEnter += GnosisCalendarWPF_MouseEnter;
            this.MouseLeave += GnosisCalendarWPF_MouseLeave;
            this.MouseDown += GnosisCalendarWPF_MouseDown;
            this.MouseUp += GnosisCalendarWPF_MouseUp;

            this.PropertyChanged += GnosisCalendar_PropertyChanged;
        }

        private void GnosisCalendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
                case "ReadOnly":
                    this.IsEnabled = !readOnly;
                    break;
            }
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        //public void RemoveOutlineColour()
        //{
            
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    calendar.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new Thickness(1);
        //}

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisCalendar_GotFocus;
        }

        private void GnosisCalendar_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisCalendarWPF_MouseEnter;
        //}

            

        private void GnosisCalendarWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisCalendarWPF_LostFocus;
        }

        private void GnosisCalendarWPF_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisCalendarWPF_MouseLeave;
        //}

        private void GnosisCalendarWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisCalendarWPF_MouseDown;
        ////}

        private void GnosisCalendarWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisCalendarWPF_MouseUp;
        //}

        private void GnosisCalendarWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetOutlineColour(string outlineColour)
        //{
            
        //}

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.SetVerticalPaddingExt(paddingVertical);
        }

        //public void SetTooltip(string toolTip)
        //{
        //    this.ToolTip = toolTip;
        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
        //}

        public double GetHeight()
        {
            return this.ActualHeight;
        }

        public void SetMarginLeft(int horizontalSpacing)
        {
            this.Margin = new Thickness(horizontalSpacing, 0, 0, 0);
        }
    }
}
