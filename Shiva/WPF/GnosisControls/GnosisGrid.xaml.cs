using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using Shiva.Shared.Data;
using System.Windows.Markup;
using System.Collections.Generic;

namespace GnosisControls
{

    public partial class GnosisGrid : Border, IGnosisGridImplementation, INotifyPropertyChanged
    {
        private List<GnosisCheckColumn> checkColumns;
        private List<GnosisComboColumn> comboColumns;
        private List<GnosisDateColumn> dateColumns;
        private List<GnosisDateTimeColumn> dateTimeColumns;
        private List<GnosisLinkColumn> linkColumns;
        private List<GnosisNumberColumn> numberColumns;
        private List<GnosisTextColumn> textColumns;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private GnosisController.HorizontalAlignmentType captionHorizontalAlignment;
        private GnosisController.VerticalAlignmentType captionVerticalAlignment;
        private GnosisController.CaptionPosition captionRelativePosition;
        private string controlType;
        private string dataset;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int maxDisplayRows;
        private int maxLines;
        private int maxSectionSpan;
        private int maxWrapRows;
        private int minDisplayRows;
        private bool multipleRowSelection;
        private int numColumns;
        private int order;
        private bool readOnly;
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
                lblCaption.Content = caption;
                lblCaption.Visibility = Visibility.Visible;
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
                this.SetVisibleExt(!hidden);
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

        public int NumColumns
        {
            get { return numColumns; }
            set
            {
                numColumns = value;
                gridHeaders.ColumnDefinitions.Clear();
                gridContent.ColumnDefinitions.Clear();

                for (int i = 0; i < numColumns; i++)
                {
                    gridHeaders.ColumnDefinitions.Add(new ColumnDefinition());
                    gridContent.ColumnDefinitions.Add(new ColumnDefinition());
                }
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



        public bool GridIsLoaded
        {
            get
            {
                return this.IsLoaded;
            }
        }

        public bool GridIsVisible
        {
            get
            {
                return this.IsVisible;
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionRelativePosition
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.CaptionPosition), captionRelativePosition);
            }

            set
            {
                try
                {
                    captionRelativePosition = (GnosisController.CaptionPosition)Enum.Parse(typeof(GnosisController.CaptionPosition), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionAlignmentHorizontal
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), captionHorizontalAlignment);
            }

            set
            {
                try
                {
                    captionHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionAlignmentVertical
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), captionVerticalAlignment);
            }

            set
            {
                try
                {
                    captionVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public string Dataset
        {
            get
            {
                return dataset;
            }

            set
            {
                dataset = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MinDisplayRows
        {
            get
            {
                return minDisplayRows;
            }

            set
            {
                minDisplayRows = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxDisplayRows
        {
            get
            {
                return maxDisplayRows;
            }

            set
            {
                maxDisplayRows = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxLines
        {
            get
            {
                return maxLines;
            }

            set
            {
                maxLines = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool MultipleRowSelection
        {
            get
            {
                return multipleRowSelection;
            }

            set
            {
                multipleRowSelection = value;
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

        [GnosisProperty]
        public int MaxWrapRows
        {
            get { return maxWrapRows; }
            set { maxWrapRows = value; }
        }

        [GnosisPropertyAttribute]
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }

            set
            {
                readOnly = value;
                gridContent.IsEnabled = !readOnly;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        [GnosisCollection]
        public List<GnosisComboColumn> ComboColumns
        {
            get { return comboColumns; }
            set { comboColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisCheckColumn> CheckColumns
        {
            get { return checkColumns; }
            set { checkColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisDateTimeColumn> DateTimeColumns
        {
            get { return dateTimeColumns; }
            set { dateTimeColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisDateColumn> DateColumns
        {
            get { return dateColumns; }
            set { dateColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisLinkColumn> LinkColumns
        {
            get { return linkColumns; }
            set { linkColumns = value; }

        }

        [GnosisCollection]
        public List<GnosisTextColumn> TextColumns
        {
            get { return textColumns; }
            set { textColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisNumberColumn> NumberColumns
        {
            get { return numberColumns; }
            set { numberColumns = value; }
        }

        public virtual void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisCheckColumn)
            {
                checkColumns.Add((GnosisCheckColumn)child);
            }
            else if (child is GnosisComboColumn)
            {
                comboColumns.Add((GnosisComboColumn)child);
            }
            else if (child is GnosisDateColumn)
            {
                dateColumns.Add((GnosisDateColumn)child);
            }
            else if (child is GnosisDateTimeColumn)
            {
                dateTimeColumns.Add((GnosisDateTimeColumn)child);
            }
            else if (child is GnosisLinkColumn)
            {
                linkColumns.Add((GnosisLinkColumn)child);
            }
            else if (child is GnosisNumberColumn)
            {
                numberColumns.Add((GnosisNumberColumn)child);
            }
            else if (child is GnosisTextColumn)
            {
                textColumns.Add((GnosisTextColumn)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError(this.GetType().Name, child.GetType().Name);
            }
        }


        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action<double> WidthChangedHandler;
        protected Action LoadedHandler;
        protected Action<bool> IsVisibleChangedHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

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



        public GnosisGrid()
        {
            InitializeComponent();

            checkColumns = new System.Collections.Generic.List<GnosisCheckColumn>();
            comboColumns = new System.Collections.Generic.List<GnosisComboColumn>();
            dateColumns = new System.Collections.Generic.List<GnosisDateColumn>();
            dateTimeColumns = new System.Collections.Generic.List<GnosisDateTimeColumn>();
            linkColumns = new System.Collections.Generic.List<GnosisLinkColumn>();
            numberColumns = new System.Collections.Generic.List<GnosisNumberColumn>();
            textColumns = new System.Collections.Generic.List<GnosisTextColumn>();

            this.PreviewMouseDown += GnosisGridWPF_MouseDown;
            this.PreviewMouseUp += GnosisGridWPF_MouseUp;
            this.MouseEnter += GnosisGridWPF_MouseEnter;
            this.MouseLeave += GnosisGridWPF_MouseLeave;

            //this.PropertyChanged += GnosisGrid_PropertyChanged;
        }

        //private void GnosisGrid_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            lblCaption.Content = caption;
        //            lblCaption.Visibility = Visibility.Visible;
        //            break;
        //        case "CaptionVerticalAlignment":
        //            break;
        //        case "CaptionHorizontalAlignment":
        //            break;
        //        case "CaptionRelativePostion":
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "NumColumns":
        //            gridHeaders.ColumnDefinitions.Clear();
        //            gridContent.ColumnDefinitions.Clear();

        //            for (int i = 0; i < numColumns; i++)
        //            {
        //                gridHeaders.ColumnDefinitions.Add(new ColumnDefinition());
        //                gridContent.ColumnDefinitions.Add(new ColumnDefinition());
        //            }
        //            break;
        //        case "ReadOnly":
        //            gridContent.IsEnabled = !readOnly;
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        //public void SetNumColumns(int numColumns)
        //{
        //    gridHeaders.ColumnDefinitions.Clear();
        //    gridContent.ColumnDefinitions.Clear();

        //    for (int i = 0; i < numColumns; i++)
        //    {
        //        gridHeaders.ColumnDefinitions.Add(new ColumnDefinition());
        //        gridContent.ColumnDefinitions.Add(new ColumnDefinition());
        //    }
        //}

        //public void SetLocked(bool locked)
        //{
        //    if (locked)
        //    {
        //        gridContent.IsEnabled = false;
        //    }
        //    else
        //    {
        //        gridContent.IsEnabled = true;
        //    }
        //}

        //public void SetMultipleRowSelectionEnabled(bool multipleRowSelect)
        //{
        //    if (multipleRowSelect)
        //    {
        //        //dataGrid.SelectionMode = DataGridSelectionMode.Extended;
        //    }
        //    else
        //    {
        //        //dataGrid.SelectionMode = DataGridSelectionMode.Single;
        //    }
        //}

        //public void SetCaptionRelativePosition(GnosisController.CaptionPosition captionRelativePosition)
        //{

        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetNumRows(int numRows)
        //{
        //    throw new NotImplementedException();
        //}

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        public void SetMinWidth(int minWidth)
        {
            this.MinWidth = minWidth;
        }

        public void SetMaxWidth(int maxWidth)
        {
            this.MaxWidth = maxWidth;
        }

        //public void SetCaption(string caption)
        //{
        //    lblCaption.Content = caption;
        //    lblCaption.Visibility = Visibility.Visible;
        //}

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        public double GetHeight()
        {
            return this.ActualHeight;
        }

        //public void AddButtonColumnImplementation(IGnosisButtonColumnImplementation buttonColumnImplementation)
        //{
        //    this.Columns.Add((GnosisButtonColumnWPF)buttonColumnImplementation);
        //}

        //public void AddCheckColumnImplementation(IGnosisCheckColumnImplementation checkColumnImplementation)
        //{
        //    this.Columns.Add((GnosisCheckColumnWPF)checkColumnImplementation);
        //}

        //public void AddComboColumnImplementation(IGnosisComboColumnImplementation comboColumnImplementation)
        //{
        //    this.Columns.Add((GnosisComboColumnWPF)comboColumnImplementation);
        //}

        //public void AddDateTimeColumnImplementation(IGnosisDateTimeColumnImplementation dateTimeColumnImplementation)
        //{
        //    this.Columns.Add((GnosisDateTimeColumnWPF)dateTimeColumnImplementation);
        //}

        //public void AddJoinColumnImplementation(IGnosisJoinColumnImplementation joinColumnImplementation)
        //{
        //    this.Columns.Add((GnosisJoinColumnWPF)joinColumnImplementation);
        //}

        public void LoadCell(IGnosisGridFieldImplementation gridField, int column, int row, int colSpan, int rowSpan, bool alternateRowColour)
        {
            //while (gridContent.RowDefinitions.Count() < row + rowSpan)
            //{
            //    RowDefinition rowdef = new RowDefinition();
            //    rowdef.Height = new GridLength(controller.StyleManager.GetFieldHeight(), GridUnitType.Pixel);
            //    gridContent.RowDefinitions.Add(rowdef);
            //}

            UIElement gridFieldWPF = (UIElement)gridField;
            //if (alternateRowColour)
            //{
            //    txtWPF.Background = (Brush)Application.Current.FindResource("LightPrimary");
            //}

            Grid.SetColumn(gridFieldWPF, column);
            Grid.SetColumnSpan(gridFieldWPF, colSpan);
            Grid.SetRow(gridFieldWPF, row);
            Grid.SetRowSpan(gridFieldWPF, rowSpan);

            gridContent.Children.Add(gridFieldWPF);
        }

        public void LoadCell(IGnosisCaptionLabelImplementation header, int col, int row, int colSpan, int rowSpan)
        {
            GnosisCaptionLabel headerWPF = (GnosisCaptionLabel)header;

            Grid.SetColumn(headerWPF, col);
            Grid.SetColumnSpan(headerWPF, colSpan);
            Grid.SetRow(headerWPF, row);
            Grid.SetRowSpan(headerWPF, rowSpan);

            gridContent.Children.Add(headerWPF);
        }

        public void LoadCell(IGnosisGridHeaderFieldImplementation header, int column, int row, int colSpan, int rowSpan)
        {
            GnosisGridHeaderField headerWPF = (GnosisGridHeaderField)header;
            Grid.SetColumn(headerWPF, column);
            Grid.SetColumnSpan(headerWPF, colSpan);
            Grid.SetRow(headerWPF, row);
            Grid.SetRowSpan(headerWPF, rowSpan);

            gridContent.Children.Add(headerWPF); 
        }

        public void AddGridHeader(IGnosisGridHeaderFieldImplementation gridHeaderFieldImplementation, int column, int row, int colSpan, int rowSpan, bool columnarFormat)
        {
            if (colSpan == 0)
            {
                return;
            }
            GnosisGridHeaderField header = (GnosisGridHeaderField)gridHeaderFieldImplementation;
            //string xaml = XamlWriter.Save(header.Style);

            Grid.SetColumn(header, column);
            Grid.SetColumnSpan(header, colSpan);
            Grid.SetRow(header, row);
            Grid.SetRowSpan(header, rowSpan);

            if (columnarFormat)
            {
                //while (gridContent.RowDefinitions.Count() < row + rowSpan)
                //{
                //    RowDefinition rowdef = new RowDefinition();
                //    rowdef.Height = new GridLength(controller.StyleManager.GetFieldHeight(), GridUnitType.Pixel);
                //    gridContent.RowDefinitions.Add(rowdef);
                //}

                gridContent.Children.Add(header);
            }
            else
            {
                //while (gridHeaders.RowDefinitions.Count() < row + rowSpan)
                //{
                //    RowDefinition rowdef = new RowDefinition();
                //    rowdef.Height = new GridLength(1, GridUnitType.Auto); 
                //    gridHeaders.RowDefinitions.Add(rowdef);
                //}

                gridHeaders.Children.Add(header);
            }

        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth - SystemParameters.VerticalScrollBarWidth;
        }

        public void Clear()
        {
            gridHeaders.Children.Clear();
            gridHeaders.RowDefinitions.Clear();
            gridHeaders.ColumnDefinitions.Clear();

            gridContent.Children.Clear();
            gridContent.RowDefinitions.Clear();
            gridContent.ColumnDefinitions.Clear();
        }

        public void CheckColumnCount(int count)
        {
            while (gridHeaders.ColumnDefinitions.Count() > count)
            {
                gridHeaders.ColumnDefinitions.RemoveAt(gridHeaders.ColumnDefinitions.Count() - 1);
                gridContent.ColumnDefinitions.RemoveAt(gridContent.ColumnDefinitions.Count() - 1);
            }
            while (gridHeaders.ColumnDefinitions.Count() < count)
            {
                gridHeaders.ColumnDefinitions.Add(new ColumnDefinition());
                gridContent.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.PreviewMouseDown += GnosisGridWPF_MouseDown;
        //}

        private void GnosisGridWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisGridWPF_MouseUp;
        //}
            

        private void GnosisGridWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisGridWPF_MouseEnter;
        //}

        private void GnosisGridWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    gridContent.Background = StyleHelper.GetBrushFromHex(backgroundColour);
          
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new Thickness(2);
        //}


        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisGridWPF_MouseLeave;
        //}

        private void GnosisGridWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
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
        //    this.Margin = new System.Windows.Thickness(left, top, right, bottom);
        //}

        //public void SetMargin(int margin)
        //{
        //    this.Margin = new System.Windows.Thickness(margin);
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisGridController)gnosisLayoutController;
            
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        public void SetWidthChangedHandler(Action<double> action)
        {
            WidthChangedHandler = action;
            this.SizeChanged += GnosisGridWPF_SizeChanged;
        }

        private void GnosisGridWPF_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
               WidthChangedHandler.Invoke(GetAvailableWidth());
            }
        }

        public void AddColumn()
        {
            gridHeaders.ColumnDefinitions.Add(new ColumnDefinition());
            gridContent.ColumnDefinitions.Add(new ColumnDefinition());
        }

        public void SetLoadedHandler(Action action)
        {
            LoadedHandler = action;
            this.Loaded += GnosisGridWPF_Loaded;
        }

        private void GnosisGridWPF_Loaded(object sender, RoutedEventArgs e)
        {
            LoadedHandler.Invoke();
        }

        public void SetIsVisibleChangedHandler(Action<bool> action)
        {
            IsVisibleChangedHandler = action;
            this.IsVisibleChanged += GnosisGridWPF_IsVisibleChanged;
        }

        private void GnosisGridWPF_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsVisibleChangedHandler.Invoke(this.IsVisible);
        }

        public void SetColumnarFormat(bool columnarFormat)
        {
            if (columnarFormat)
            {
                scrlHeaders.Visibility = Visibility.Collapsed;
            }
            else
            {
                scrlHeaders.Visibility = Visibility.Visible;
            }
        }

        public void SetHeight(double height)
        {
           // scrlContent.Height = height;
        }

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.Padding = new Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Right, paddingVertical);
        //}

        //public FontFamily GetFontFamily()
        //{
        //    return this.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return this.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return this.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return this.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return this.FontStretch;
        //}

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        //public void SetFont(string font)
        //{
            
        //}

        //public void SetFontSize(int fontSize)
        //{
            
        //}

        //public void SetForegroundColour(string contentColour)
        //{
            
        //}

        public void AddRow(double rowHeight)
        {
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(rowHeight);
            gridContent.RowDefinitions.Add(row);
        }

        public void AddRowAutoHeight()
        {
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            gridContent.RowDefinitions.Add(row);
        }

        public void AddHeaderRow()
        {
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            gridHeaders.RowDefinitions.Add(row);
        }

        public void AddHeaderRow(double rowHeight)
        {
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(rowHeight);
            gridHeaders.RowDefinitions.Add(row);
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisGridWPF_GotFocus;
        }

        private void GnosisGridWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisGridWPF_LostFocus;
        }

        private void GnosisGridWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public double GetAvailableHeight()
        {
            return this.ActualHeight - gridHeaders.ActualHeight;
        }

        public void SetMarginLeft(int marginLeft)
        {
            this.Margin = new Thickness(marginLeft, 0, 0, 0);
        }


    }
}
