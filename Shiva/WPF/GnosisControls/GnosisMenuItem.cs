using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace GnosisControls
{
    public partial class GnosisMenuItem : MenuItem, IGnosisMenuItemImplementation
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        protected Action ClickHandler;

        

        public GnosisMenuItem() : base()
        {
            this.MouseEnter += GnosisMenuItemWPF_MouseEnter;
            this.MouseLeave += GnosisMenuItemWPF_MouseLeave;
            this.MouseDown += GnosisMenuItemWPF_MouseDown;
            this.MouseUp += GnosisMenuItemWPF_MouseUp;

            this.PropertyChanged += GnosisMenuItem_PropertyChanged;
        }

        private void GnosisMenuItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    this.Header = caption;
                    break;
                case "Disabled":
                    this.IsEnabled = !disabled;
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "GnosisIcon":
                    this.Header = new Image
                    {
                        Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, this.IsEnabled)))
                    };
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
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

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisMenuItemWPF_GotFocus;
        }

        private void GnosisMenuItemWPF_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisMenuItemWPF_MouseEnter;
        //}

        private void GnosisMenuItemWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisMenuItemWPF_LostFocus;
        }

        private void GnosisMenuItemWPF_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        private void GnosisMenuItemWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisMenuItemWPF_MouseLeave;

        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisMenuItemWPF_MouseDown;
        //}

        private void GnosisMenuItemWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisMenuItemWPF_MouseUp;
        //}

        private void GnosisMenuItemWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        public void SetClickHandler(Action action)
        {
            ClickHandler = action;
            this.Click += GnosisMenuItemWPF_Click;
        }

        private void GnosisMenuItemWPF_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ClickHandler.Invoke();
        }


        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
