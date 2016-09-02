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

using Shiva.Shared.ContainerControllers;
using Shiva.Shared.Utility;
using Shiva.Shared.ContentControllers;
using ShivaWPF3.UtilityWPF;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Data;
using Shiva.Shared.WindowControllers;
using System.IO;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using ShivaWPF3;
using Shiva;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisParentWindowWPF.xaml
    /// </summary>
    public partial class GnosisParentWindow : Window, IGnosisParentWindowImplementation, INotifyPropertyChanged
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private string controlType;
        private string gnosisName;
        private bool hidden;
        private string icon;
        private int id;
        private int order;
        private GnosisController.OrientationType orientation;
        private string tooltip;

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

        [GnosisPropertyAttribute]
        public string ControlType
        {
            get
            {
                return controlType;
            }

            set
            {
                controlType = value;
            }
        }

        [GnosisPropertyAttribute]
        public string Caption
        {
            get
            {
                return caption;
            }

            set
            {
                caption = value;
                OnPropertyChanged("Caption");
            }
        }

        [GnosisPropertyAttribute]
        public string GnosisName
        {
            get { return gnosisName; }
            set { gnosisName = value; }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return null; }
            set { }
        }


        [GnosisPropertyAttribute]
        public bool Hidden
        {
            get
            {
                return hidden;
            }

            set
            {
                hidden = value;
                this.SetVisibleExt(!hidden);
                if (hidden)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("Parent window hidden", "GnosisParentWindow");
                }
                OnPropertyChanged("Hidden");
            }
        }

        [GnosisPropertyAttribute]
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                // OnPropertyChanged("ID");
            }
        }

        [GnosisPropertyAttribute]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
                //OnPropertyChanged("Order");
            }
        }

        [GnosisPropertyAttribute]
        public string Tooltip
        {
            get
            {
                return tooltip;
            }

            set
            {
                tooltip = value;
                this.ToolTip = tooltip;
            }
        }

        [GnosisPropertyAttribute]
        public string GnosisIcon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
                this.Icon = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, true)));
            }
        }

        [GnosisPropertyAttribute]
        public string GnosisOrientation
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.OrientationType), orientation);
            }

            set
            {
                try
                {
                    orientation = (GnosisController.OrientationType)Enum.Parse(typeof(GnosisController.OrientationType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.OrientationType _GnosisOrientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private List<GnosisToolbarTray> toolbarTrays;

        private GnosisPrimarySplit primarySplit;


        //  private bool visibleField;

        private List<GnosisChildWindow> childWindows;

        private List<GnosisEventHandler> eventHandlers;



        [GnosisCollection]
        public List<GnosisToolbarTray> ToolbarTrays
        {
            get
            {
                return this.toolbarTrays;
            }
            set
            {
                this.toolbarTrays = value;
            }
        }

        [GnosisChild]
        public GnosisPrimarySplit PrimarySplit
        {
            get
            {
                return this.primarySplit;
            }
            set
            {
                this.primarySplit = value;
            }
        }

        [GnosisCollection]
        public List<GnosisChildWindow> ChildWindows
        {
            get { return childWindows; }
            set { childWindows = value; }
        }
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
      
           // this.PropertyChanged += GnosisParentWindow_PropertyChanged;

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

        //private void GnosisParentWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "GnosisIcon":
        //            this.Icon = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, true)));
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;

        //    }
        //}

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
