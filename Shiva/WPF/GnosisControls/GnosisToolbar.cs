using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls.Primitives;
using Shiva.Shared.Interfaces;
using System.Windows.Data;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using System.Windows.Markup;
using ShivaWPF3.UtilityWPF;

namespace GnosisControls
{
    public partial class GnosisToolbar : StackPanel, IGnosisToolbarImplementation
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

       

        public GnosisToolbar()
        {
            this.Orientation = Orientation.Horizontal;

            //this.MouseDown += GnosisToolbar_MouseDown;
            //this.MouseUp += GnosisToolbar_MouseUp;
            //this.MouseEnter += GnosisToolbar_MouseEnter;
            //this.MouseLeave += GnosisToolbar_MouseLeave;

            this.PropertyChanged += GnosisToolbar_PropertyChanged;
            //this.Height = 30;
            //FixupToolBarOverflowArrow(this);
            // this.Background = (Brush)Application.Current.FindResource("ToolbarBackground");
        }

        private void GnosisToolbar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Caption":
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        public void AddToolbarButton(GnosisButton buttonImplementation)
        {
            //this.Items.Add((GnosisToolbarButton)buttonImplementation);
            this.Children.Add((GnosisButton)buttonImplementation);
        }

        public void AddMenuButton(GnosisMenuButton menuBtnImplementation)
        {
            // this.Items.Add((GnosisToolbarMenuButton)menuBtnImplementation);
            this.Children.Add(menuBtnImplementation);
        }

        public void AddToggleButton(GnosisToggleButton toggleButton)
        {
            //this.Items.Add((GnosisToggleButton)toggleButton);
            this.Children.Add((GnosisToggleButton)toggleButton);
        }

        public void AddSystemAddressField(GnosisSystemAddressField systemAddressField)
        {
           //this.Items.Add((GnosisSystemAddressField)systemAddressField);
           this.Children.Add((GnosisSystemAddressField)systemAddressField);
        }

        private static void FixupToolBarOverflowArrow(ToolBar toolBar)
        {
            Action fixup = () =>
            {
                var overflowButton = toolBar.Template.FindName("OverflowButton", toolBar) as ButtonBase;
                if (overflowButton != null)
                {
                    overflowButton.SetBinding(
                        VisibilityProperty,
                        new Binding("IsEnabled")
                        {
                            RelativeSource = RelativeSource.Self,
                            Converter = new BooleanToVisibilityConverter()
                        });
                }
            };

            if (toolBar.IsLoaded)
            {
                fixup();
            }
            else
            {
                RoutedEventHandler handler = null;
                handler = (sender, e) =>
                {
                    fixup();
                    toolBar.Loaded -= handler;
                };

                toolBar.Loaded += handler;
            }
        }

      

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetEnabled(bool enabled)
        //{
        //    this.IsEnabled = enabled;
        //}

        public double GetPaddingHorizontal()
        {
            // return this.Padding.Left;
            throw new NotImplementedException();
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
           // this.SetPaddingHorizontalExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
           // this.SetPaddingVerticalExt(paddingVertical);
        }

       

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisToolbar_MouseDown;
        //}

        //private void GnosisToolbar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //MouseDownHandler.Invoke();
        //    HasMouseDown = true;
        //}

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisToolbar_MouseUp;
        //}

        //private void GnosisToolbar_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //MouseUpHandler.Invoke();
        //    HasMouseDown = false;
        //}

            

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisToolbar_MouseEnter;
        //}

        //private void GnosisToolbar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    //GotMouseFocusHandler.Invoke();
        //    HasMouseFocus = true;
        //    //string xaml = XamlWriter.Save(this.Style);
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisToolbar_MouseLeave;
        //}

        //private void GnosisToolbar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    //LostMouseFocusHandler.Invoke();
        //    HasMouseFocus = false;
        //}

        //public void SetTooltip(string toolTip)
        //{
        //    this.ToolTip = toolTip;
        //}


        //public void SetFont(string font)
        //{
            
        //}

        //public void SetFontSize(int fontSize)
        //{
            
        //}

        //public void SetForegroundColour(string contentColour)
        //{
            
        //}

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisToolbar_GotFocus;
        }

        private void GnosisToolbar_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisToolbar_LostFocus;
        }

        private void GnosisToolbar_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }


        public void GnosisAddChild(IGnosisObject child)
        {

            if (child is GnosisSystemAddressField)
            {
                systemAddressField = (GnosisSystemAddressField)child;
                this.Children.Add(systemAddressField);
            }
            else if (child is GnosisToolbarButton)
            {
                this.Children.Add((GnosisButton)child);
            }
            //else if (child is GnosisToolbarMenuButton)
            //{
            //    this.Children.Add((GnosisToolbarMenuButton)child);
            //}
            else if (child is GnosisToggleButton)
            {
                this.Children.Add((GnosisToggleButton)child);
            }
        }

        //public void AddMenuButton(IGnosisToolbarMenuButtonImplementation menuBtnImplementation)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
