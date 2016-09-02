using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

using Shiva.Shared.Interfaces;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.ToolbarControllers;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;
using Shiva.Shared.Data;
using System.Windows.Markup;

namespace GnosisControls
{
    public partial class GnosisToolbarTray : Border, IGnosisToolbarTrayImplementation, INotifyPropertyChanged
    {
        private List<GnosisToolbar> toolbars;

        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasBorder;
        private bool hasFocus;
        private bool hidden;
        private int id;
        private int menuSystemID;
        private int menuControlID;
        private int order;
        private GnosisController.OrientationType orientation;
        private string tooltip;
        private GnosisController.HorizontalAlignmentType trayHorizontalAlignment;


        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
            }
        }

        [GnosisProperty]
        public bool HasBorder
        {
            get { return hasBorder; }
            set { hasBorder = value; }
        }

        [GnosisProperty]
        public string TrayHorizontalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), trayHorizontalAlignment);
            }

            set
            {
                try
                {
                    trayHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.HorizontalAlignmentType _TrayHorizontalAlignment
        {
            get { return trayHorizontalAlignment; }
            set { trayHorizontalAlignment = value; }
        }

        [GnosisProperty]
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

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisProperty]
        public int MenuSystemID
        {
            get
            {
                return menuSystemID;
            }

            set
            {
                menuSystemID = value;
            }
        }

        [GnosisProperty]
        public int MenuControlID
        {
            get
            {
                return menuControlID;
            }

            set
            {
                menuControlID = value;
            }
        }

        [GnosisProperty]
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

        [GnosisProperty]
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
                OnPropertyChanged("Hidden");
            }
        }

        [GnosisProperty]
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

        [GnosisProperty]
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

        [GnosisProperty]
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [GnosisProperty]
        public string GnosisName
        {
            get
            {
                return gnosisName;
            }

            set
            {
                gnosisName = value;
            }
        }

        [GnosisProperty]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        [GnosisCollection]
        public List<GnosisToolbar> Toolbars
        {
            get { return toolbars; }
            set { toolbars = value; }
        }
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
                this.SetHorizontalPaddingExt(containerHorizontalPadding);
            }
        }

        public int ContainerVerticalPadding
        {
            get { return containerVerticalPadding; }
            set
            {
                containerVerticalPadding = value;
                this.SetVerticalPaddingExt(containerVerticalPadding);
            }
        }

        public static readonly DependencyProperty GnosisBorderThicknessProperty =
         DependencyProperty.RegisterAttached("GnosisBorderThickness",
         typeof(int), typeof(GnosisToolbarTray), new FrameworkPropertyMetadata(GnosisBorderThicknessPropertyChanged));
        //new FrameworkPropertyMetadata(0,
        //    FrameworkPropertyMetadataOptions.Inherits));

        public static void SetHighlightThickness(UIElement element, int value)
        {
            element.SetValue(GnosisBorderThicknessProperty, value);
        }

        public static int GetHighlightThickness(UIElement element)
        {
            return (int)element.GetValue(GnosisBorderThicknessProperty);
        }

        public static void GnosisBorderThicknessPropertyChanged(object source, DependencyPropertyChangedEventArgs e)
        {
            GnosisToolbarTray toolbarTray = source as GnosisToolbarTray;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double paddingHorizontal;
            double paddingVertical;

            if (newThickness > oldThickness)
            {
                if (toolbarTray.ContainerHorizontalPadding > 0 && toolbarTray.ContainerVerticalPadding > 0)
                {
                    //string xaml = XamlWriter.Save(toolbarTray.Style);
                    //increase border thickness, decrease padding
                    paddingHorizontal = toolbarTray.ContainerHorizontalPadding - newThickness;
                    paddingVertical = toolbarTray.ContainerVerticalPadding - newThickness;

                    if (paddingHorizontal >= 0 && paddingVertical >= 0)
                    {
                        toolbarTray.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
                        toolbarTray.BorderThickness = new Thickness(newThickness);
                    }
                    else
                    {
                        toolbarTray.Padding = new Thickness(0);
                        toolbarTray.BorderThickness = new Thickness(toolbarTray.ContainerHorizontalPadding, toolbarTray.ContainerVerticalPadding,
                            toolbarTray.ContainerHorizontalPadding, toolbarTray.ContainerVerticalPadding);
                    }
                }

            }
            else
            {
                //decrease border thickness, increase padding
                paddingHorizontal = toolbarTray.Padding.Left + oldThickness;
                paddingVertical = toolbarTray.Padding.Top + oldThickness;

                toolbarTray.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
                toolbarTray.BorderThickness = new Thickness(newThickness);
            }



        }


        public GnosisToolbarTray()
        {
            innerPanel = new StackPanel();
            innerPanel.Orientation = Orientation.Horizontal;
            this.Child = innerPanel;


            //this.MouseDown += GnosisToolbarTrayWPF_MouseDown;
            //this.MouseUp += GnosisToolbarTrayWPF_MouseUp;
            //this.MouseLeave += GnosisToolbarTrayWPF_MouseLeave;
            //this.MouseEnter += GnosisToolbarTrayWPF_MouseEnter;
          //  this.PropertyChanged += GnosisToolbarTray_PropertyChanged;
        }

        //private void GnosisToolbarTray_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;

        //    }
        //}

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
            innerPanel.Children.Add(grid);
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
                innerPanel.Children.Add((GnosisToolbar)child);
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
