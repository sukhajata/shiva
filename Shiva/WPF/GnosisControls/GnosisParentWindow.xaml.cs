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
using System.Windows.Shapes;

using ShivaShared3.ContainerControllers;
using ShivaShared3.Utility;
using ShivaShared3.ContentControllers;
using ShivaWPF3.UtilityWPF;
using ShivaShared3.Interfaces;
using ShivaShared3.Data;
using ShivaShared3.WindowControllers;
using System.IO;
using ShivaShared3.BaseControllers;
using System.ComponentModel;
using ShivaWPF3;
using Shiva;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisParentWindowWPF.xaml
    /// </summary>
    public partial class GnosisParentWindow : Window, IGnosisParentWindowImplementation
    {
        protected GnosisParentWindowController controller;
        protected GnosisApplicationManager appManager;
        private GnosisPanel currentPanel;

        private Action<double> LoadedHandler;
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

      

        public GnosisParentWindow()
        {
            InitializeComponent();

            this.MouseDown += GnosisParentWindowWPF_MouseDown;
            this.MouseUp += GnosisParentWindowWPF_MouseUp;
            this.MouseEnter += GnosisParentWindowWPF_MouseEnter;
            this.MouseLeave += GnosisParentWindowWPF_MouseLeave;

            GlobalData.Singleton.ParentWindowImplementation = this;
      
            this.PropertyChanged += GnosisParentWindow_PropertyChanged;

            this.Closed += GnosisParentWindow_Closed;
        }

        private void GnosisParentWindow_Closed(object sender, EventArgs e)
        {
            ((LoginDialog)GlobalData.Singleton.Login).Dispatcher.InvokeShutdown();
            //.Invoke((Action)(() =>
            //{
            //    ((LoginDialog)GlobalData.Singleton.Login).Close();
            //}));

        }

        private void GnosisParentWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "GnosisIcon":
                    this.Icon = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, true)));
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;

            }
        }

        ///// <summary>
        ///// TitleBar_MouseDown - Drag if single-click, resize if double-click
        ///// </summary>
        //private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ChangedButton == MouseButton.Left)
        //        if (e.ClickCount == 2)
        //        {
        //            AdjustWindowSize();
        //        }
        //        else
        //        {
        //            Application.Current.MainWindow.DragMove();
        //        }
        //}

        ///// <summary>
        ///// CloseButton_Clicked
        ///// </summary>
        //private void CloseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}

        ///// <summary>
        ///// MaximizedButton_Clicked
        ///// </summary>
        //private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AdjustWindowSize();
        //}

        ///// <summary>
        ///// Minimized Button_Clicked
        ///// </summary>
        //private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = WindowState.Minimized;
        //}

        ///// <summary>
        ///// Adjusts the WindowSize to correct parameters when Maximize button is clicked
        ///// </summary>
        //private void AdjustWindowSize()
        //{
        //    if (this.WindowState == WindowState.Maximized)
        //    {
        //        this.WindowState = WindowState.Normal;
        //        MaxButton.Content = "1";
        //    }
        //    else
        //    {
        //        this.WindowState = WindowState.Maximized;
        //        MaxButton.Content = "2";
        //    }

        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public void SetLoadedHandler(Action<double> loadedHandler)
        {
            this.Loaded += GnosisParentWindowWPF_Loaded;
            LoadedHandler = loadedHandler;
        }

        private void GnosisParentWindowWPF_Loaded(object sender, RoutedEventArgs e)
        {
            double width = this.ActualWidth;
            LoadedHandler.Invoke(width);
        }

        //public void LoadPrimarySplit(IGnosisPrimarySplitImplementation primarySplit)
        //{
        //    contentRoot.Children.Add((GnosisPrimarySplit)primarySplit);
        //}

        //public void LoadToolbarTray(IGnosisToolbarTrayImplementation toolbarTray)
        //{
        //    switch (((GnosisToolbarTray)toolbarTray).HorizontalAlignment)
        //    {
        //        case HorizontalAlignment.Left:
        //            pnlToolsLeft.Children.Add((GnosisToolbarTray)toolbarTray);
        //            break;
        //        case HorizontalAlignment.Center:
        //            pnlToolsCentre.Children.Add((GnosisToolbarTray)toolbarTray);
        //            break;
        //        case HorizontalAlignment.Right:
        //            pnlToolsRight.Children.Add((GnosisToolbarTray)toolbarTray);
        //            break;
        //    }
        //}

        public void SetController(GnosisVisibleController _controller)
        {
            controller = (GnosisParentWindowController)_controller;
        }

        public void UnHighlight()
        {
            throw new NotImplementedException();
        }

        public void WriteToWindow(string output)
        {
            txtStatus.Text = output;
            
        }

        private void btnShowGridLines_Click(object sender, RoutedEventArgs e)
        {
            if (currentPanel != null)
            {
                currentPanel.ShowGridLines();
            }
            else
            {
                MessageBox.Show("Please select a panel");
            }
        }

        public void SetCurrentPanel(GnosisPanel panel)
        {
            currentPanel = panel;
        }

        public void SetOrder(int _order)
        {
            order = _order;
        }

        private void btnShowDesigner_Click(object sender, RoutedEventArgs e)
        {
            ShivaDesigner designer = new ShivaDesigner();
            designer.Show();
        }

        //public int GetOrder()
        //{
        //    throw new NotImplementedException();
        //}

        public void Highlight()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
        //}

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            ExtensionMethodsWPF.SetHorizontalPaddingExt(this, paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            ExtensionMethodsWPF.SetVerticalPaddingExt(this, paddingVertical);
        }

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //    //TitleBar.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetOutlineColour(string outlineColour)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveOutlineColour()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisParentWindowWPF_MouseDown;
        //}
        

        private void GnosisParentWindowWPF_MouseDown(object sender, MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisParentWindowWPF_MouseUp;
        //}

        private void GnosisParentWindowWPF_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisParentWindowWPF_MouseEnter;
        //}

        private void GnosisParentWindowWPF_MouseEnter(object sender, MouseEventArgs e)
        {
           // GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisParentWindowWPF_MouseLeave;
        //}

        private void GnosisParentWindowWPF_MouseLeave(object sender, MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

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
            this.GotFocus += GnosisParentWindowWPF_GotFocus;
        }

        private void GnosisParentWindowWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisParentWindowWPF_LostFocus;
        }

        private void GnosisParentWindowWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        private void btnShowXML_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.Singleton.SystemController.ShowXML();
        }

        public void ShowXML(string xml)
        {
            XMLViewer viewer = new XMLViewer();
            viewer.SetText(xml);
            viewer.ShowDialog();
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisPrimarySplit)
            {
                
                primarySplit = (GnosisPrimarySplit)child;
                // LoadPrimarySplit((GnosisPrimarySplit)child);
                contentRoot.Children.Add((GnosisPrimarySplit)primarySplit);

            }
            else if (child is GnosisToolbarTray)
            {
                if (toolbarTrays == null)
                {
                    toolbarTrays = new List<GnosisToolbarTray>();
                }
                
                toolbarTrays.Add((GnosisToolbarTray)child);
                // LoadToolbarTray((GnosisToolbarTray)child);
                switch (((GnosisToolbarTray)child)._TrayHorizontalAlignment)
                {
                    case GnosisController.HorizontalAlignmentType.LEFT:
                        pnlToolsLeft.Children.Add((GnosisToolbarTray)child);
                        break;
                    case GnosisController.HorizontalAlignmentType.CENTRE:
                        pnlToolsCentre.Children.Add((GnosisToolbarTray)child);
                        break;
                    case GnosisController.HorizontalAlignmentType.RIGHT:
                        pnlToolsRight.Children.Add((GnosisToolbarTray)child);
                        break;
                }
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisParentWindow", child.GetType().Name);
            }
        }
    }
}
