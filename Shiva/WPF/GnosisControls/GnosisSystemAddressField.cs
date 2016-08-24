using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;

namespace GnosisControls
{
    public partial class GnosisSystemAddressField : TextBox, IGnosisSystemAddressFieldImplementation
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

      
        public GnosisSystemAddressField()
            :base()
        {
            this.Padding = new System.Windows.Thickness(3, 0, 3, 0);
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            this.MouseEnter += GnosisSystemAddressFieldWPF_MouseEnter;
            this.MouseLeave += GnosisSystemAddressFieldWPF_MouseLeave;
            this.MouseDown += GnosisSystemAddressFieldWPF_MouseDown;
            this.MouseUp += GnosisSystemAddressFieldWPF_MouseUp;

            this.PropertyChanged += GnosisSystemAddressField_PropertyChanged;
        }

        private void GnosisSystemAddressField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Locked":
                    this.IsEnabled = !locked;
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
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
            this.GotFocus += GnosisSystemAddressFieldWPF_GotFocus;
        }

        private void GnosisSystemAddressFieldWPF_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisSystemAddressFieldWPF_MouseEnter;
        //}

        private void GnosisSystemAddressFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisSystemAddressFieldWPF_LostFocus;
        }

        private void GnosisSystemAddressFieldWPF_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisSystemAddressFieldWPF_MouseLeave;
        //}

        private void GnosisSystemAddressFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisSystemAddressFieldWPF_MouseDown;
        //}

        private void GnosisSystemAddressFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisSystemAddressFieldWPF_MouseUp;
        //}

        private void GnosisSystemAddressFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
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

        public void SetAddress(string address)
        {
            this.Text = address;
        }
    }
}
