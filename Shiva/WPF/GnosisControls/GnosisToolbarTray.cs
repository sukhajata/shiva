using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

using ShivaShared3.Interfaces;
using ShivaShared3.BaseControllers;
using ShivaShared3.ToolbarControllers;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;
using ShivaShared3.Data;

namespace GnosisControls
{
    public partial class GnosisToolbarTray : StackPanel, IGnosisToolbarTrayImplementation
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        private Action GotFocusHandler;
        private Action LostFocusHandler;

        private int containerVerticalPadding;
        private int containerHorizontalPadding;

        private StackPanel innerPanel;

        public int ContainerHorizontalPadding
        {
            get { return containerHorizontalPadding; }
            set
            {
                containerHorizontalPadding = value;
                innerPanel.SetHorizontalMarginExt(containerHorizontalPadding);
            }
        }

        public int ContainerVerticalPadding
        {
            get { return containerVerticalPadding; }
            set
            {
                containerVerticalPadding = value;
                innerPanel.SetVerticalMarginExt(containerVerticalPadding);
            }
        }

        public GnosisToolbarTray()
        {
            this.Orientation = Orientation.Horizontal;
            innerPanel = new StackPanel();
            innerPanel.Orientation = Orientation.Horizontal;
            this.Children.Add(innerPanel);


            //this.MouseDown += GnosisToolbarTrayWPF_MouseDown;
            //this.MouseUp += GnosisToolbarTrayWPF_MouseUp;
            //this.MouseLeave += GnosisToolbarTrayWPF_MouseLeave;
            //this.MouseEnter += GnosisToolbarTrayWPF_MouseEnter;
            this.PropertyChanged += GnosisToolbarTray_PropertyChanged;
        }

        private void GnosisToolbarTray_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Caption":
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;

            }
        }

        //public void AddToolbar(IGnosisToolbarImplementation toolbar)
        //{
        //    this.Children.Add((GnosisToolbar)toolbar);
        //}

        public void AddSplitter(string splitterColour)
        {
            Grid grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.Width = 2;
            grid.Margin = new Thickness(3, 0, 3, 0);
            grid.Background = StyleHelper.GetBrushFromHex(splitterColour);
            this.Children.Add(grid);
        }

        //public double GetPaddingHorizontal()
        //{
        //    throw new NotImplementedException();
        //}

       

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }


        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisToolbarTrayWPF_MouseEnter;
        //}

        //private void GnosisToolbarTrayWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    //GotMouseFocusHandler.Invoke();
        //    HasMouseFocus = true;
        //}

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType alignment)
        {
            this.SetHorizontalAlignmentExt(alignment);

        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisToolbarTrayWPF_MouseLeave;
        //}

        //private void GnosisToolbarTrayWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    //LostMouseFocusHandler.Invoke();
        //    HasMouseFocus = false;
        //}

        ////public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisToolbarTrayWPF_MouseDown;
        //}

        //private void GnosisToolbarTrayWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //MouseDownHandler.Invoke();
        //    HasMouseDown = false;
        //}

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisToolbarTrayWPF_MouseUp;
        //}

        //private void GnosisToolbarTrayWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //MouseUpHandler.Invoke();
        //    HasMouseDown = false;
        //}

            

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
            
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
            
        //}

        
        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisToolbarTrayWPF_GotFocus;
        }

        private void GnosisToolbarTrayWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisToolbarTrayWPF_LostFocus;
        }

        private void GnosisToolbarTrayWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisToolbar)
            {
                if (toolbars == null)
                {
                    toolbars = new List<GnosisToolbar>();
                }
                toolbars.Add((GnosisToolbar)child);
                this.Children.Add((GnosisToolbar)child);
                //splitter
                //if (this.Children.Count > 1 && i < count)
                //{
                //    ((IGnosisToolbarTrayImplementation)ControlImplementation).AddSplitter(splitterColour);
                //}
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisToolbarTray", child.GetType().Name);
            }


        }


    }
}
