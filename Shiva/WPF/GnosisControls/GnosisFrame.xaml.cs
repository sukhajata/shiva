using Shiva.Shared.BaseControllers;
using Shiva.Shared.Interfaces;
using Shiva.Shared.OuterLayoutControllers;
using ShivaWPF3.UtilityWPF;
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
using System.ComponentModel;
using System.Windows.Markup;
using Shiva.Shared.Data;

namespace GnosisControls
{

    /// <summary>
    /// Interaction logic for GnosisFrameWPF.xaml
    /// </summary>
    public partial class GnosisFrame : Border, IGnosisFrameImplementation, INotifyPropertyChanged
    {

        protected Action<double> widthChangedHandler;
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action<double> FrameLoadedHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        private int containerHorizontalPadding;
        private int containerVerticalPadding;

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
            typeof(int), typeof(GnosisFrame), new FrameworkPropertyMetadata(BorderThicknessPropertyChanged));
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
            GnosisFrame frame = source as GnosisFrame;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double paddingHorizontal;
            double paddingVertical;

            if (newThickness > oldThickness)
            {
                if (frame.ContainerHorizontalPadding > 0 && frame.ContainerVerticalPadding > 0)
                {
                    //increase border thickness, decrease padding
                    paddingHorizontal = frame.ContainerHorizontalPadding - newThickness;
                    paddingVertical = frame.ContainerVerticalPadding - newThickness;

                    if (paddingHorizontal >= 0 && paddingVertical >= 0)
                    {
                        frame.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
                        frame.BorderThickness = new Thickness(newThickness);
                    }
                    else
                    {
                        frame.Padding = new Thickness(0);
                        frame.BorderThickness = new Thickness(frame.ContainerVerticalPadding, frame.ContainerHorizontalPadding,
                            frame.ContainerVerticalPadding, frame.ContainerHorizontalPadding);
                    }
                }

            }
            else
            {
                //decrease border thickness, increase padding
                paddingHorizontal = frame.Padding.Left + oldThickness;
                paddingVertical = frame.Padding.Top + oldThickness;

                frame.Padding = new Thickness(paddingVertical, paddingHorizontal, paddingVertical, paddingHorizontal);
                frame.BorderThickness = new Thickness(newThickness);
            }

        }


        public GnosisFrame()
        {
            InitializeComponent();

            this.MouseDown += GnosisFrameWPF_MouseDown;
            this.MouseUp += GnosisFrameWPF_MouseUp;
            this.MouseEnter += GnosisFrameWPF_MouseEnter;
            this.MouseLeave += GnosisFrameWPF_MouseLeave;

            //calendars = new List<GnosisCalendar>();
            //grids = new List<GnosisGrid>();
            //panels = new List<GnosisPanel>();
            //messageGrids = new List<GnosisMessageGrid>();
            //textAreas = new List<GnosisTextArea>();
            //trees = new List<GnosisTree>();

            this.PropertyChanged += GnosisFrame_PropertyChanged;

        }

        protected virtual void GnosisFrame_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    
                    break;
                case "GnosisIcon":
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        private void GnosisFrameWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        private void GnosisFrameWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public double GetAvailableWidth()
        {
            double width = this.ActualWidth;
            return width;
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }


        //public void SetMarginBottom(int marginBottom)
        //{
        //    this.Margin = new Thickness { Bottom = marginBottom };
        //}


        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType alignment)
        {
            this.SetHorizontalAlignmentExt(alignment);
            
        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }


        protected void LoadControl(UIElement control, int col, int row, int colSpan, int rowSpan)
        {
            Grid.SetColumn(control, col);
            Grid.SetRow(control, row);
            Grid.SetColumnSpan(control, colSpan);
            Grid.SetRowSpan(control, rowSpan);
            gridContent.Children.Add(control);

        }

        public void LoadMessageGridImplementation(IGnosisMessageGridImplementation msgGridImp, int col, int row, int colSpan, int rowSpan)
        {
            LoadControl((GnosisMessageGrid)msgGridImp, col, row, colSpan, rowSpan);
        }

        public void LoadGrid(IGnosisGridImplementation gridImp, int col, int row, int colSpan, int rowSpan)
        {
            LoadControl((GnosisGrid)gridImp, col, row, colSpan, rowSpan);
        }

        public void LoadPanel(IGnosisPanelImplementation panelImp, int col, int row, int colSpan, int rowSpan)
        {
            LoadControl((GnosisPanel)panelImp, col, row, colSpan, rowSpan);
        }

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        public void SetMinWidth(int minWidth)
        {
            throw new NotImplementedException();
        }

        public void SetMaxWidth(int maxWidth)
        {
            this.MaxWidth = maxWidth;
        }

        private void Highlight()
        {
            if (this.Padding.Left > 1 && this.Padding.Top > 1)
            {
                this.BorderThickness = new Thickness(this.Padding.Left - 1, this.Padding.Top - 1, this.Padding.Right - 1, this.Padding.Bottom - 1);
                this.Padding = new Thickness(1);
            }
        }

        private void UnHighlight()
        {
            if (this.BorderThickness.Left > 1 && this.BorderThickness.Top > 1)
            {
                this.Padding = new Thickness(this.BorderThickness.Left + 1, this.BorderThickness.Top + 1, this.BorderThickness.Right + 1, this.BorderThickness.Bottom + 1);
                this.BorderThickness = new Thickness(0);
            }
        }

        //public void SetCaption(string _caption)
        //{
        //    caption = _caption;
        //}

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        //public double GetAvailableWidth()
        //{
        //    FrameworkElement parent = this.VisualParent as FrameworkElement;
        //    return parent.ActualWidth;
        //}

        public void SetWidthChangedHandler(Action<double> _widthChangedHandler)
        {
            widthChangedHandler = _widthChangedHandler;
            this.SizeChanged += GnosisFrameWPF_SizeChanged;
        }

        private void GnosisFrameWPF_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.IsLoaded && e.WidthChanged)
            {
                widthChangedHandler.Invoke(e.NewSize.Width);
            }
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisFrameWPF_MouseDown;
        //}

        private void GnosisFrameWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisFrameWPF_MouseUp;
        //}

            

        private void GnosisFrameWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisFrameWPF_MouseEnter;
        //}

        private void GnosisFrameWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //}

        //public void SetFont(string font)
        //{
        //   // this.FontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //   // this.FontSize = fontSize;
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    gridContent.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //   // this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisFrameWPF_MouseLeave;
        //}

        private void GnosisFrameWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    this.BorderThickness = new Thickness(2);
        //}

        //public void RemoveOutlineColour()
        //{
        //    this.BorderThickness = new Thickness(0);
        //}

        //public void SetMargin(int left, int top, int right, int bottom)
        //{
        //    this.Margin = new Thickness(left, top, right, bottom);
        //}

        //public void SetMargin(int margin)
        //{
        //    this.Margin = new Thickness(margin);
        //}

        //public string GetCaption()
        //{
        //    return caption;
        //}

        public void SetLoadedHandler(Action<double> action)
        {
            FrameLoadedHandler = action;
            this.Loaded += GnosisFrameWPF_Loaded;
        }

        private void GnosisFrameWPF_Loaded(object sender, RoutedEventArgs e)
        {
            double width = this.ActualWidth;
            FrameLoadedHandler.Invoke(width);
        }


        public void Clear()
        {
            gridContent.Children.Clear();
            gridContent.ColumnDefinitions.Clear();
            gridContent.RowDefinitions.Clear();
        }

        public void AddColumn()
        {
            ColumnDefinition col = new ColumnDefinition();
            gridContent.ColumnDefinitions.Add(col);
        }

        public void AddRowAutoHeight()
        {
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            //row.Height = new GridLength(1, GridUnitType.Star);
            gridContent.RowDefinitions.Add(row);
        }

        public void AddRow()
        {
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(1, GridUnitType.Star);
            gridContent.RowDefinitions.Add(row);
        }

        public void AddRowFixedHeight(int rowHeight)
        {
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(rowHeight);
            gridContent.RowDefinitions.Add(row);
        }

        public void SetMinWidth(double minWidth)
        {
            this.MinWidth = MinWidth;
        }

        public void SetMaxWidth(double maxWidth)
        {
            this.MaxWidth = maxWidth;
        }

        public void SetNumColumns(int numCols)
        {
            for (int i = 0; i < numCols; i++)
            {
                gridContent.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.Padding = new System.Windows.Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Right, paddingVertical);
        }

        

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public void IncreaseColSpan(IGnosisInnerLayoutControlImplementation gnosisControlImplementation, int numCols)
        {
            UIElement control = gnosisControlImplementation as UIElement;
            int currentColSpan = Grid.GetColumnSpan(control);
            Grid.SetColumnSpan(control, currentColSpan + numCols);
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisFrameWPF_GotFocus;
        }


        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisFrameWPF_LostFocus;
        }

        public void SetVerticalScrollbarVisibility(GnosisVisibleController.GnosisVerticalScrollbarVisibility visibility)
        {
            switch (visibility)
            {
                case GnosisVisibleController.GnosisVerticalScrollbarVisibility.Visible:
                    scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    break;
                case GnosisVisibleController.GnosisVerticalScrollbarVisibility.Hidden:
                    scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    break;
                case GnosisVisibleController.GnosisVerticalScrollbarVisibility.Auto:
                    scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    break;
            }
        }

        public void RemoveChild(IGnosisMouseVisibleControlImplementation gnosisControlImplementation)
        {
            gridContent.Children.Remove((UIElement)gnosisControlImplementation);
        }

        public double GetAvailableHeight()
        {
            return this.ActualHeight;
        }

        public double GetPaddingVertical()
        {
            return this.Padding.Top;
        }

       
    }
}
