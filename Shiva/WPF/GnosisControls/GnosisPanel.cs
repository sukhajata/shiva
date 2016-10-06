using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Shiva.Shared.Interfaces;
using System.Windows;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;
using System.Windows.Markup;
using Shiva.Shared.Data;
using Shiva.Shared.BaseControllers;
using System.Windows.Media;

namespace GnosisControls
{
    public partial class GnosisPanel : Border, IGnosisPanelImplementation, INotifyPropertyChanged
    {
        //Collections
        private List<GnosisButton> buttons;
        private List<GnosisCheckField> checkFields;
        //private List<GnosisCheckGroup> checkGroups;
        private List<GnosisComboField> comboFields;
        private List<GnosisDateField> dateFields;
        private List<GnosisDateTimeField> dateTimeFields;
        //private List<GnosisLabel> labels;
        private List<GnosisLinkField> linkFields;
        private List<GnosisListField> listFields;
        private List<GnosisNumberField> numberFields;
        private List<GnosisRadioField> radioFields;
        private List<GnosisRadioGroup> radioGroups;
        private List<GnosisTextField> textFields;


        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private GnosisController.HorizontalAlignmentType captionHorizontalAlignment;
        private GnosisController.VerticalAlignmentType captionVerticalAlignment;
        private GnosisController.CaptionPosition captionRelativePosition;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int maxSectionSpan;
        private int numColumns;
        private int order;
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

        public int NumColumns
        {
            get
            {
                return numColumns;
            }

            set
            {
                numColumns = value;
                contentGrid.ColumnDefinitions.Clear();

                for (int i = 0; i < numColumns; i++)
                {
                    ColumnDefinition colDef = new ColumnDefinition();
                    colDef.Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
                    contentGrid.ColumnDefinitions.Add(colDef);
                }
            }
        }



        //private List<GnosisToggleButtonGroup> toggleButtonGroups;



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private GnosisController.CaptionPosition captionRelativePositionField;


        [GnosisCollection]
        public List<GnosisButton> Buttons
        {
            get { return buttons; }
            set { buttons = value; }
        }


        [GnosisCollection]
        public List<GnosisCheckField> CheckFields
        {
            get { return checkFields; }
            set { checkFields = value; }
        }

        [GnosisCollection]
        public List<GnosisComboField> ComboFields
        {
            get { return comboFields; }
            set { comboFields = value; }
        }

        [GnosisCollection]
        public List<GnosisDateField> DateFields
        {
            get { return dateFields; }
            set { dateFields = value; }
        }

        [GnosisCollection]
        public List<GnosisDateTimeField> DateTimeFields
        {
            get { return dateTimeFields; }
            set { dateTimeFields = value; }
        }


        //[System.Xml.Serialization.XmlElementAttribute("GnosisLabelField")]
        //public List<GnosisLabelField> LabelFields
        //{
        //    get { return labelFields; }
        //    set { labelFields = value; }
        //}

        [GnosisCollection]
        public List<GnosisLinkField> LinkFields
        {
            get { return linkFields; }
            set { linkFields = value; }
        }

        [GnosisCollection]
        public List<GnosisListField> ListFields
        {
            get { return listFields; }
            set { listFields = value; }
        }


        [GnosisCollection]
        public List<GnosisRadioField> RadioFields
        {
            get { return radioFields; }
            set { radioFields = value; }
        }


        [GnosisCollection]
        public List<GnosisTextField> TextFields
        {
            get { return textFields; }
            set { textFields = value; }
        }

        [GnosisCollection]
        public List<GnosisNumberField> NumberFields
        {
            get { return numberFields; }
            set { numberFields = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisComboField)
            {
                if (comboFields == null)
                {
                    comboFields = new List<GnosisComboField>();
                }
                comboFields.Add((GnosisComboField)child);
            }
            else if (child is GnosisCheckField)
            {
                if (checkFields == null)
                {
                    checkFields = new List<GnosisCheckField>();
                }
                checkFields.Add((GnosisCheckField)child);
            }
            else if (child is GnosisButton)
            {
                if (buttons == null)
                {
                    buttons = new List<GnosisButton>();
                }
                buttons.Add((GnosisButton)child);
            }
            else if (child is GnosisDateField)
            {
                if (dateFields == null)
                {
                    dateFields = new List<GnosisDateField>();
                }
                dateFields.Add((GnosisDateField)child);
            }
            else if (child is GnosisDateTimeField)
            {
                if (dateTimeFields == null)
                {
                    dateTimeFields = new List<GnosisDateTimeField>();
                }
                dateTimeFields.Add((GnosisDateTimeField)child);
            }
            else if (child is GnosisLinkField)
            {
                if (linkFields == null)
                {
                    linkFields = new List<GnosisLinkField>();
                }
                linkFields.Add((GnosisLinkField)child);
            }
            else if (child is GnosisListField)
            {
                if (listFields == null)
                {
                    listFields = new List<GnosisListField>();
                }
                listFields.Add((GnosisListField)child);
            }
            else if (child is GnosisNumberField)
            {
                if (numberFields == null)
                {
                    numberFields = new List<GnosisNumberField>();
                }
                numberFields.Add((GnosisNumberField)child);
            }
            else if (child is GnosisRadioField)
            {
                if (radioFields == null)
                {
                    radioFields = new List<GnosisRadioField>();
                }
                radioFields.Add((GnosisRadioField)child);
            }
            else if (child is GnosisTextField)
            {
                if (textFields == null)
                {
                    textFields = new List<GnosisTextField>();
                }
                textFields.Add((GnosisTextField)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisPanel", child.GetType().Name);
            }
        }

        Grid contentGrid;
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action<double> LoadedHandler;
        protected Action<bool> IsVisibleChangedHandler;
        protected Action<double> WidthChangedHandler;
        protected Action<double> HeightChangedHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        private StackPanel[,] matrix;

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



        public GnosisPanel()
            :base()
        {
            contentGrid = new Grid();
            matrix = new StackPanel[100, 100];
            this.Child = contentGrid;

            //contentGrid.ShowGridLines = true;

            //contentGrid.ShowGridLines = true;
           // this.GotFocus += GnosisPanelWPF_GotFocus;

            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Stretch;

            this.MouseDown += GnosisPanelWPF_MouseDown;
            this.MouseUp += GnosisPanelWPF_MouseUp;
            this.MouseEnter += GnosisPanelWPF_MouseEnter;
            this.MouseLeave += GnosisPanelWPF_MouseLeave;

           // this.PropertyChanged += GnosisPanel_PropertyChanged;
        }

        //private void GnosisPanel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "CaptionRelativePosition":
        //            break;
        //        case "CaptionAlignmentHorizontal":
        //            break;
        //        case "CaptionAlignmentVertical":
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "NumColumns":
        //            contentGrid.ColumnDefinitions.Clear();

        //            for (int i = 0; i < numColumns; i++)
        //            {
        //                ColumnDefinition colDef = new ColumnDefinition();
        //                colDef.Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
        //                contentGrid.ColumnDefinitions.Add(colDef);
        //            }
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        public void ShowGridLines()
        {
            contentGrid.ShowGridLines = true;
        }

        public void AddGnosisCaptionLabel(GnosisCaptionLabel captionLabel, int column, int row, int colSpan, int rowSpan)
        {
            //each grid cell contains a stack panel
            StackPanel panel;
            if (matrix[column, row] == null)
            {
                panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                matrix[column, row] = panel;

                Grid.SetColumn(panel, column);
                Grid.SetRow(panel, row);
                Grid.SetColumnSpan(panel, colSpan);
                Grid.SetRowSpan(panel, rowSpan);
                contentGrid.Children.Add(panel);
            }
            else
            {
                panel = matrix[column, row];
            }

            if (captionLabel.RelativePosition == GnosisController.CaptionPosition.LEFT ||
                captionLabel.RelativePosition == GnosisController.CaptionPosition.RIGHT)
            {
                captionLabel.VerticalAlignment = VerticalAlignment.Center;
            }

            panel.Children.Add(captionLabel);

            
        }

        public void AddGnosisContentControlImplementation(IGnosisContentControlImplementation contentControlImplementation, int column, int row, int colSpan, int rowSpan)
        {

            UIElement control = (UIElement)contentControlImplementation;

            StackPanel panel;
            if (matrix[column, row] == null)
            {
                panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                matrix[column, row] = panel;

                Grid.SetColumn(panel, column);
                Grid.SetRow(panel, row);
                Grid.SetColumnSpan(panel, colSpan);
                Grid.SetRowSpan(panel, rowSpan);
                contentGrid.Children.Add(panel);
            }
            else
            {
                panel = matrix[column, row];
            }

            panel.Children.Add(control);

        }

        public void AddGnosisLayoutControlImplementation(IGnosisInnerLayoutControlImplementation layoutControlImplementation, int column, int row, int colSpan, int rowSpan)
        {

            Control control = (Control)layoutControlImplementation;
            Grid.SetColumn(control, column);
            Grid.SetColumnSpan(control, colSpan);
            Grid.SetRow(control, row);
            Grid.SetRowSpan(control, rowSpan);
            contentGrid.Children.Add(control);

            //if (layoutControlImplementation is IGnosisGridImplementation)
            //{
            //    this.Children.Add((GnosisGridWPF)layoutControlImplementation);
            //}
        }

        public void AddGnosisDateTimeFieldImplementation(IGnosisDateTimeFieldImplementation gnosisControlImplementation, int column, int row, int colSpan, int rowSpan)
        {
            GnosisDateTimeField dateTimeField = (GnosisDateTimeField)gnosisControlImplementation;
            Grid.SetColumn(dateTimeField, column);
            Grid.SetColumnSpan(dateTimeField, colSpan);
            Grid.SetRow(dateTimeField, row);
            Grid.SetRowSpan(dateTimeField, rowSpan);
            contentGrid.Children.Add(dateTimeField);
        }

        public void SetMarginBottom(int marginBottom)
        {
            this.Margin = new Thickness { Bottom = marginBottom };
        }

        public void Clear()
        {
            contentGrid.Children.Clear();
            contentGrid.ColumnDefinitions.Clear();
            contentGrid.RowDefinitions.Clear();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] != null)
                    {
                        matrix[i, j].Children.Clear();
                    }
                }
            }

            matrix = new StackPanel[100, 100];
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }


        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }


        //public void SetCaption(string caption)
        //{
            
        //}

        //public void SetController(GnosisController _controller)
        //{
        //    this.controller = (GnosisPanelController)_controller;
        //}

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetNumColumns(int numColumns)
        //{
        //    contentGrid.ColumnDefinitions.Clear();

        //    for (int i = 0; i < numColumns; i++)
        //    {
        //        ColumnDefinition colDef = new ColumnDefinition();
        //        colDef.Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
        //        contentGrid.ColumnDefinitions.Add(colDef);
        //    }
        //}

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        public void AddRowAutoHeight()
        {
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto);
            //new System.Windows.GridLength(GlobalData.Instance.LineHeight * 3, System.Windows.GridUnitType.Pixel);//new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
            contentGrid.RowDefinitions.Add(rowDef);
        }

        public void AddRowFixedHeight(double height)
        {
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new System.Windows.GridLength(height, System.Windows.GridUnitType.Pixel);
            contentGrid.RowDefinitions.Add(rowDef);
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisPanelWPF_MouseDown;
        //}

        private void GnosisPanelWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisPanelWPF_MouseUp;
        //}

        private void GnosisPanelWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisPanelWPF_MouseEnter;
        //}

            

        private void GnosisPanelWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        public void SetHeightChangedHandler(Action<double> action)
        {
            HeightChangedHandler = action;
        }

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new Thickness(2);
        //}


        //public void SetFont(string font)
        //{
            
        //}

        //public void SetFontSize(int fontSize)
        //{
           
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetForegroundColour(string contentColour)
        //{
            
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisPanelWPF_MouseLeave;
        //}

        private void GnosisPanelWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    this.BorderThickness = new System.Windows.Thickness(2);
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
        //    controller = (GnosisPanelController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        public double GetColumnWidth(int currentCol)
        {
            if (contentGrid.ColumnDefinitions.Count() > currentCol)
            {
                return contentGrid.ColumnDefinitions[currentCol].Width.Value;
            }
            else
            {
                return 0;
            }
        }

        public void SetLoadedHandler(Action<double> action)
        {
            LoadedHandler = action;
            this.Loaded += GnosisPanelWPF_Loaded;
        }

        private void GnosisPanelWPF_Loaded(object sender, RoutedEventArgs e)
        {
            this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
           // this.Arrange(new Rect(0, 0, DesiredWidth, this.DesiredHeight));

            double width = this.ActualWidth;
            LoadedHandler.Invoke(width);
        }

        public void SetIsVisibleChangedHandler(Action<bool> action)
        {
            IsVisibleChangedHandler = action;
            this.IsVisibleChanged += GnosisPanelWPF_IsVisibleChanged;
        }

        private void GnosisPanelWPF_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsVisibleChangedHandler.Invoke(this.IsVisible);
        }

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.Padding = new System.Windows.Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        //}

        //public void SetPaddingVertical (double paddingVertical)
        //{
        //    this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Right, paddingVertical);
        //}

      

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

        public void SetWidthChangedHandler(Action<double> action)
        {
            WidthChangedHandler = action;
            this.SizeChanged += GnosisPanelWPF_SizeChanged;
        }

        private void GnosisPanelWPF_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged && Math.Abs(e.NewSize.Width - e.PreviousSize.Width) > 2)
            {
                WidthChangedHandler.Invoke(GetAvailableWidth());
            }
            else if (e.HeightChanged && Math.Abs(e.NewSize.Height - e.PreviousSize.Height) > 2)
            {
                HeightChangedHandler.Invoke(e.NewSize.Height);
            }
        }

        public void SetRowAutoHeight(int row)
        {
            contentGrid.RowDefinitions[row].Height = GridLength.Auto;
        }

        public void SetRowFixedHeight(int row, double newHeight)
        {
            contentGrid.RowDefinitions[row].Height = new GridLength(newHeight);
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisPanelWPF_GotFocus1;
        }

        private void GnosisPanelWPF_GotFocus1(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisPanelWPF_LostFocus;
        }

        private void GnosisPanelWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetMarginLeft(int marginLeft)
        {
            this.Margin = new Thickness(marginLeft, 0, 0, 0);
        }

        public void AddHorizontalSpacing(int column, int row, int colSpan, int rowSpan)
        {
            Label lbl = new Label();
            lbl.Width = HorizontalSpacing;
            lbl.Background = Brushes.Transparent;

            StackPanel panel;
            if (matrix[column, row] == null)
            {
                panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                matrix[column, row] = panel;

                Grid.SetColumn(panel, column);
                Grid.SetRow(panel, row);
                Grid.SetColumnSpan(panel, colSpan);
                Grid.SetRowSpan(panel, rowSpan);
                contentGrid.Children.Add(panel);
            }
            else
            {
                panel = matrix[column, row];
            }

            panel.Children.Add(lbl);
        }
    }
}
