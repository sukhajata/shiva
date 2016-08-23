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
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisMenuButtonWPF.xaml
    /// </summary>
    public partial class GnosisMenuButton : UserControl, IGnosisMenuButtonImplementation 
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

       

        public GnosisMenuButton()
        {
            InitializeComponent();

            menuItems = new List<GnosisMenuItem>();

            this.MouseEnter += GnosisMenuButtonWPF_MouseEnter;
            this.MouseLeave += GnosisMenuButtonWPF_MouseLeave;
            this.MouseDown += GnosisMenuButtonWPF_MouseDown;
            this.MouseUp += GnosisMenuButtonWPF_MouseUp;

            this.PropertyChanged += GnosisMenuButton_PropertyChanged;
        }

        private void GnosisMenuButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    btnMenu.Content = caption;
                    break;
                case "Disabled":
                    btnMenu.IsEnabled = !disabled;
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "GnosisIcon":
                    btnMenu.Content = new Image
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

        //public void RemoveOutlineColour()
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

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisMenuButtonWPF_GotFocus;
        }

        private void GnosisMenuButtonWPF_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisMenuButtonWPF_MouseEnter;
        //}

        private void GnosisMenuButtonWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisMenuButtonWPF_LostFocus;
        }

        private void GnosisMenuButtonWPF_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisMenuButtonWPF_MouseLeave;
        //}

        private void GnosisMenuButtonWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisMenuButtonWPF_MouseDown;
        //}

        private void GnosisMenuButtonWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisMenuButtonWPF_MouseUp;
        //}

        private void GnosisMenuButtonWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        public void AddMenuItem(IGnosisMenuItemImplementation item)
        {
            mnu.Items.Add((GnosisMenuItem)item);
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            menuItems.Add((GnosisMenuItem)child);
            mnu.Items.Add((GnosisMenuItem)child);
        }

        public void AddToggleButton(GnosisToggleButton toggleButton)
        {
            mnu.Items.Add(toggleButton);
        }

        public void AddButton(GnosisButton button)
        {
            mnu.Items.Add(button);
        }

        public void AddMenuButton(GnosisMenuButton menuButton)
        {
            mnu.Items.Add(menuButton);
        }
    }
}
