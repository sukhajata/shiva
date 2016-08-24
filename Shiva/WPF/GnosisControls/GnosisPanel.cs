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

namespace GnosisControls
{
    public partial class GnosisPanel : Border, IGnosisPanelImplementation
    {
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

            buttons = new List<GnosisButton>();
            checkFields = new List<GnosisCheckField>();
            comboFields = new List<GnosisComboField>();
            dateFields = new List<GnosisDateField>();
            dateTimeFields = new List<GnosisDateTimeField>();
            listFields = new List<GnosisListField>();
            linkFields = new List<GnosisLinkField>();
            numberFields = new List<GnosisNumberField>();
            radioFields = new List<GnosisRadioField>();
            textFields = new List<GnosisTextField>();

            //contentGrid.ShowGridLines = true;
           // this.GotFocus += GnosisPanelWPF_GotFocus;

            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Stretch;

            this.MouseDown += GnosisPanelWPF_MouseDown;
            this.MouseUp += GnosisPanelWPF_MouseUp;
            this.MouseEnter += GnosisPanelWPF_MouseEnter;
            this.MouseLeave += GnosisPanelWPF_MouseLeave;

            buttons = new List<GnosisButton>();
            checkFields = new List<GnosisCheckField>();

            this.PropertyChanged += GnosisPanel_PropertyChanged;
        }

        private void GnosisPanel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "CaptionRelativePosition":
                    break;
                case "CaptionAlignmentHorizontal":
                    break;
                case "CaptionAlignmentVertical":
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "NumColumns":
                    contentGrid.ColumnDefinitions.Clear();

                    for (int i = 0; i < numColumns; i++)
                    {
                        ColumnDefinition colDef = new ColumnDefinition();
                        colDef.Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
                        contentGrid.ColumnDefinitions.Add(colDef);
                    }
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        public void ShowGridLines()
        {
            contentGrid.ShowGridLines = true;
        }

        public void AddGnosisCaptionLabel(GnosisCaptionLabel captionLabel, int column, int row, int colSpan, int rowSpan)
        {
            //string xaml = XamlWriter.Save(captionLabel);

            if (captionLabel.RelativePosition == Shiva.Shared.BaseControllers.GnosisController.CaptionPosition.LEFT ||
                captionLabel.RelativePosition == Shiva.Shared.BaseControllers.GnosisController.CaptionPosition.RIGHT)
            {
                captionLabel.VerticalAlignment = VerticalAlignment.Center;

                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Children.Add(captionLabel);
                matrix[column, row] = panel;

                Grid.SetColumn(panel, column);
                Grid.SetRow(panel, row);
                Grid.SetColumnSpan(panel, colSpan);
                Grid.SetRowSpan(panel, rowSpan);
                contentGrid.Children.Add(panel);
            }
            else
            {
                Grid.SetColumn(captionLabel, column);
                Grid.SetRow(captionLabel, row);
                Grid.SetColumnSpan(captionLabel, colSpan);
                Grid.SetRowSpan(captionLabel, rowSpan);
                contentGrid.Children.Add(captionLabel);
            }
        }

        public void AddGnosisContentControlImplementation(IGnosisContentControlImplementation contentControlImplementation, int column, int row, int colSpan, int rowSpan)
        {

            UIElement control = (UIElement)contentControlImplementation;

            if (matrix[column, row] != null)
            {
                matrix[column, row].Children.Add(control);
            }

            else
            {
                Grid.SetColumn(control, column);
                Grid.SetColumnSpan(control, colSpan);
                Grid.SetRow(control, row);
                Grid.SetRowSpan(control, rowSpan);

                contentGrid.Children.Add(control);
            }
            //if (contentControlImplementation is GnosisTextFieldWPF)
            //{
            //    this.Children.Add((GnosisTextFieldWPF)contentControlImplementation);
            //}
            //else if (contentControlImplementation is GnosisComboFieldWPF)
            //{
            //    this.Children.Add((GnosisComboFieldWPF)contentControlImplementation);
            //}
            //else if (contentControlImplementation is GnosisCheckFieldWPF)
            //{
            //    this.Children.Add((GnosisCheckFieldWPF)contentControlImplementation);
            //}
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

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.Padding = new System.Windows.Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        }

        public void SetPaddingVertical (double paddingVertical)
        {
            this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Right, paddingVertical);
        }

      

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

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

        public void SetMarginLeft(int horizontalSpacing)
        {
            this.Margin = new Thickness(horizontalSpacing, 0, 0, 0);
        }


    }
}
