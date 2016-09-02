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
using System.ComponentModel;
using ShivaWPF3.UtilityWPF;
using Shiva.Shared.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTabItem.xaml
    /// </summary>
    public partial class GnosisTabItem : UserControl, IGnosisTabItemImplementation, INotifyPropertyChanged
    {
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private List<GnosisPanel> panels;
        private List<GnosisGrid> grids;
        private List<GnosisTextArea> textAreas;
        private List<GnosisTree> trees;
        private List<GnosisCalendar> calendars;

        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int maxSectionSpan;
        private int order;
        private string tooltip;

        private int containerHorizontalPadding;
        private int containerVerticalPadding;
        private bool hasBorder;
        private int horizontalSpacing;
        private int verticalSpacing;

        public int HorizontalSpacing
        {
            get
            {
                return horizontalSpacing;
            }

            set
            {
                horizontalSpacing = value;
            }
        }

        public int VerticalSpacing
        {
            get
            {
                return verticalSpacing;
            }

            set
            {
                verticalSpacing = value;
            }
        }

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

        [GnosisProperty]
        public bool HasBorder
        {
            get { return hasBorder; }
            set
            {
                hasBorder = value;
                OnPropertyChanged("HasBorder");
            }
        }

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

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
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
                tabItem.SetVisibleExt(!hidden);
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
                this.Tooltip = tooltip;
                //OnPropertyChanged("Tooltip");
            }
        }




        [GnosisPropertyAttribute]
        public int MaxSectionSpan
        {
            get
            {
                return maxSectionSpan;
            }

            set
            {
                maxSectionSpan = value;
            }
        }

        public static readonly DependencyProperty GnosisBorderThicknessProperty =
           DependencyProperty.RegisterAttached("GnosisBorderThickness",
           typeof(int), typeof(GnosisTabItem), new FrameworkPropertyMetadata(BorderThicknessPropertyChanged));
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

        public static void BorderThicknessPropertyChanged(object source, DependencyPropertyChangedEventArgs e)
        {
            GnosisTabItem tabItem = source as GnosisTabItem;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double paddingHorizontal;
            double paddingVertical;

            if (newThickness > oldThickness)
            {
                if (tabItem.ContainerHorizontalPadding > 0 && tabItem.ContainerVerticalPadding > 0)
                {
                    //increase border thickness, decrease padding
                    paddingHorizontal = tabItem.ContainerHorizontalPadding - newThickness;
                    paddingVertical = tabItem.ContainerVerticalPadding - newThickness;

                    if (paddingHorizontal >= 0 && paddingVertical >= 0)
                    {
                        tabItem.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
                        tabItem.BorderThickness = new Thickness(newThickness);
                    }
                    else
                    {
                        tabItem.Padding = new Thickness(0);
                        tabItem.BorderThickness = new Thickness(tabItem.ContainerHorizontalPadding, tabItem.ContainerVerticalPadding,
                            tabItem.ContainerHorizontalPadding, tabItem.ContainerVerticalPadding);
                    }
                }

            }
            else
            {
                //decrease border thickness, increase padding
                paddingHorizontal = tabItem.Padding.Left + oldThickness;
                paddingVertical = tabItem.Padding.Top + oldThickness;

                tabItem.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
                tabItem.BorderThickness = new Thickness(newThickness);
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public GnosisTabItem()
        {
            InitializeComponent();

            tabItem.MouseEnter += GnosisCalendarWPF_MouseEnter;
            tabItem.MouseLeave += GnosisCalendarWPF_MouseLeave;
            tabItem.MouseDown += GnosisCalendarWPF_MouseDown;
            tabItem.MouseUp += GnosisCalendarWPF_MouseUp;
        }
        

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisCalendar)
            {
                if (calendars == null)
                {
                    calendars = new List<GnosisCalendar>();
                }
                calendars.Add((GnosisCalendar)child);
            }
            else if (child is GnosisGrid)
            {
                if (grids == null)
                {
                    grids = new List<GnosisGrid>();
                }
                grids.Add((GnosisGrid)child);
            }
            else if (child is GnosisPanel)
            {
                if (panels == null)
                {
                    panels = new List<GnosisPanel>();
                }
                panels.Add((GnosisPanel)child);
            }
            else if (child is GnosisTextArea)
            {
                if (textAreas == null)
                {
                    textAreas = new List<GnosisTextArea>();
                }
                textAreas.Add((GnosisTextArea)child);
            }
            else if (child is GnosisTree)
            {
                if (trees == null)
                {
                    trees = new List<GnosisTree>();

                }
                trees.Add((GnosisTree)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError(this.GetType().Name, child.GetType().Name);
            }
        }

        //private void GnosisCalendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //        case "ReadOnly":
        //            this.IsEnabled = !readOnly;
        //            break;
        //    }
        //}

        public double GetAvailableWidth()
        {
            return tabItem.ActualWidth;
        }

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

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
            tabItem.GotFocus += GnosisCalendar_GotFocus;
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
            tabItem.LostFocus += GnosisCalendarWPF_LostFocus;
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

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.SetHorizontalPaddingExt(paddingHorizontal);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    this.SetVerticalPaddingExt(paddingVertical);
        //}

        //public void SetTooltip(string toolTip)
        //{
        //    this.ToolTip = toolTip;
        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(tabItem, visible);
        }

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
        //}

        public double GetHeight()
        {
            return tabItem.ActualHeight;
        }

        //public void SetMarginLeft(int horizontalSpacing)
        //{
        //    tabItem.Margin = new Thickness(horizontalSpacing, 0, 0, 0);
        //}

    }
}
