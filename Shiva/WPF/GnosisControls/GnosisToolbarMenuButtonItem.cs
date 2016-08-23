using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShivaShared3.Interfaces;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ShivaWPF3.UtilityWPF;
using ShivaShared3.BaseControllers;
using System.ComponentModel;

namespace GnosisControls
{
    public class GnosisToolbarMenuButtonItem : MenuItem, IGnosisMenuItemImplementation, INotifyPropertyChanged
    {
        private Action clickHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
       
        private Menu subMenu;

        public GnosisToolbarMenuButtonItem():base()
        {
            this.MouseDown += GnosisToolbarMenuButtonItemWPF_MouseDown;
            this.MouseUp += GnosisToolbarMenuButtonItemWPF_MouseUp;
            this.MouseEnter += GnosisToolbarMenuButtonItemWPF_MouseEnter;
            this.MouseLeave += GnosisToolbarMenuButtonItemWPF_MouseLeave;

        }

        public void AddItem(IGnosisToolbarMenuButtonItemImplementation item)
        {
            if (subMenu == null)
            {
                subMenu = new Menu();
            }
            subMenu.Items.Add(item);
        }

        //public void SetCaption(string caption)
        //{
        //    this.Header = caption;
        //}

        public void SetClickHandler(Action _clickHandler)
        {
            clickHandler = _clickHandler;
            this.Click += GnosisToolbarMenuButtonItemWPF_Click1;
        }

        private void GnosisToolbarMenuButtonItemWPF_Click1(object sender, RoutedEventArgs e)
        {
            clickHandler.Invoke();
        }

        //public void SetDepressed(bool depressed)
        //{
        //    if (depressed)
        //    {
        //        this.Background = Brushes.Gray;
        //    }
        //    else
        //    {
        //        this.Background = Brushes.Transparent;
        //    }
        //}

        //public void SetEnabled(bool enabled)
        //{
        //    this.IsEnabled = enabled;
        //}

        //public void SetIcon(string iconName, bool enabled)
        //{
        //    this.Icon = new Image
        //    {
        //        Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(iconName, enabled)))
        //    };
        //}

        public void SetStrikethrough(bool strikethrough)
        {

        }


        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        public void HandleClick()
        {
            clickHandler.Invoke();
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }


        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}


        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.SetPaddingHorizontalExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.SetPaddingVerticalExt(paddingVertical);
        }

      

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisToolbarMenuButtonItemWPF_MouseDown;
        //}

        private void GnosisToolbarMenuButtonItemWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisToolbarMenuButtonItemWPF_MouseUp;
        //}

        private void GnosisToolbarMenuButtonItemWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisToolbarMenuButtonItemWPF_MouseEnter;
        //}

            

        private void GnosisToolbarMenuButtonItemWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisToolbarMenuButtonItemWPF_MouseLeave;
        //}

        private void GnosisToolbarMenuButtonItemWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetController(GnosisVisibleController gnosisVisibleController)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetFont(string font)
        //{
        //    this.FontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    this.FontSize = fontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisToolbarMenuButtonItemWPF_GotFocus;
        }

        private void GnosisToolbarMenuButtonItemWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisToolbarMenuButtonItemWPF_LostFocus;
        }

        private void GnosisToolbarMenuButtonItemWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    this.SetVerticalContentAlignmentExt(verticalAlignment);
        //}

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
        //    this.IsEnabled = isEnabled;
        //}

        //public void SetLocked(bool locked)
        //{
        //    throw new NotImplementedException();
        //}

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);
        }

        public void SetWidth(double width)
        {
            this.Width = width;
        }
    }
}
