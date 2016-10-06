using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using Shiva.Shared.PanelFieldControllers;
using ShivaWPF3.UtilityWPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisDateFieldWPF.xaml
    /// </summary>
    public partial class GnosisDateField : UserControl, IGnosisDateFieldImplementation, INotifyPropertyChanged
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool locked;

        private string caption;
        private GnosisCaptionLabel captionLabel;
        private GnosisController.VerticalAlignmentType contentVerticalAlignment;
        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
        private string controlType;
        private bool datasetCreated;
        private bool datasetUpdated;
        private bool datasetDeleted;
        private string dataset;
        private string datasetItem;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private bool longDateFormat;
        private int minDisplayChars;
        private int maxDisplayChars;
        private int order;
        private bool readOnly;
        private string toolip;
        private string valueField;
        private int variableControlID;
        private int variableSystemID;
        private bool variableIsInput;
        private bool variableIsOutput;

        public GnosisCaptionLabel CaptionLabel
        {
            get { return captionLabel; }
            set { captionLabel = value; }
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
                // string xaml = XamlWriter.Save(this);
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
        public string ContentVerticalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), contentVerticalAlignment);
            }
            set
            {
                try
                {
                    _ContentVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
                    //OnPropertyChanged("ContentVerticalAlignment");
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public string ContentHorizontalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), contentHorizontalAlignment);
            }
            set
            {
                try
                {
                    _ContentHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                    //OnPropertyChanged("ContentHorizontalAlignment");
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.VerticalAlignmentType _ContentVerticalAlignment
        {
            get { return contentVerticalAlignment; }
            set
            {
                contentVerticalAlignment = value;
                SetVerticalContentAlignment(contentVerticalAlignment);
            }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get { return contentHorizontalAlignment; }
            set
            {
                contentHorizontalAlignment = value;
                SetHorizontalContentAlignment(contentHorizontalAlignment);
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
        public bool DatasetCreated
        {
            get
            {
                return datasetCreated;
            }

            set
            {
                datasetCreated = value;
                OnPropertyChanged("DatasetCreated");
            }
        }

        [GnosisPropertyAttribute]
        public bool DatasetUpdated
        {
            get
            {
                return datasetUpdated;
            }

            set
            {
                datasetUpdated = value;
                OnPropertyChanged("DatasetUpdated");
            }
        }

        [GnosisPropertyAttribute]
        public bool DatasetDeleted
        {
            get
            {
                return datasetDeleted;
            }

            set
            {
                datasetDeleted = value;
                OnPropertyChanged("DatasetDeleted");
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
                //OnPropertyChanged("Dataset");
            }
        }

        [GnosisPropertyAttribute]
        public string DatasetItem
        {
            get
            {
                return datasetItem;
            }

            set
            {
                datasetItem = value;
                // OnPropertyChanged("DatasetItem");
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

        [GnosisProperty]
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
        public int MinDisplayChars
        {
            get
            {
                return minDisplayChars;
            }
            set
            {
                minDisplayChars = value;
                OnPropertyChanged("MinDisplayChars");
            }
        }

        [GnosisPropertyAttribute]
        public int MaxDisplayChars
        {
            get
            {
                return maxDisplayChars;
            }
            set
            {
                maxDisplayChars = value;
                OnPropertyChanged("MaxDisplayChars");
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
                return toolip;
            }

            set
            {
                toolip = value;
                this.ToolTip = toolip;
            }
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
                OnPropertyChanged("ReadOnly");
                Locked = readOnly;
            }
        }

        [GnosisPropertyAttribute]
        public string Value
        {
            get
            {
                return valueField;
            }

            set
            {
                valueField = value;
            }
        }

        [GnosisPropertyAttribute]
        public int VariableControlID
        {
            get
            {
                return variableControlID;
            }

            set
            {
                variableControlID = value;
            }
        }

        [GnosisPropertyAttribute]
        public int VariableSystemID
        {
            get
            {
                return variableSystemID;
            }

            set
            {
                variableSystemID = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool VariableIsInput
        {
            get
            {
                return variableIsInput;
            }

            set
            {
                variableIsInput = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool VariableIsOutput
        {
            get
            {
                return variableIsOutput;
            }

            set
            {
                variableIsOutput = value;
            }
        }

        public bool Locked
        {
            get { return locked; }
            set
            {
                if (!readOnly)
                {
                    locked = value;
                }
                if (locked || readOnly)
                {
                    //replace datepicker with textbox
                    string text = picker.Text;
                    picker.Visibility = System.Windows.Visibility.Collapsed;

                    txtDate.Visibility = System.Windows.Visibility.Visible;
                    txtDate.Text = text;
                }
                else
                {
                    if (txtDate.Visibility == System.Windows.Visibility.Visible)
                    {
                        string text = txtDate.Text;
                        txtDate.Visibility = System.Windows.Visibility.Collapsed;

                        picker.SelectedDate = Convert.ToDateTime(text);
                        picker.Visibility = System.Windows.Visibility.Visible;
                    }
                }

                OnPropertyChanged("Locked");
            }
        }

        [GnosisPropertyAttribute]
        public bool LongDateFormat
        {
            get
            {
                return longDateFormat;
            }

            set
            {
                longDateFormat = value;
                if (longDateFormat)
                {
                    picker.SelectedDateFormat = DatePickerFormat.Long;
                }
                else
                {
                    picker.SelectedDateFormat = DatePickerFormat.Short;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }

        private Action GotFocusHandler;
        private Action LostFocusHandler;

        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;

        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set
            {
                horizontalPadding = value;
                this.SetHorizontalPaddingExt(horizontalPadding);
            }
        }

        public int VerticalPadding
        {
            get { return verticalPadding; }
            set
            {
                verticalPadding = value;
                this.SetVerticalPaddingExt(verticalPadding);
            }
        }

        public int HorizontalMargin
        {
            get { return horizontalMargin; }
            set
            {
                horizontalMargin = value;
                this.SetHorizontalMarginExt(horizontalMargin);
            }
        }

        public int VerticalMargin
        {
            get { return verticalMargin; }
            set
            {
                verticalMargin = value;
                this.SetVerticalMarginExt(verticalMargin);
            }
        }

        public static readonly DependencyProperty ControlThicknessProperty =
             DependencyProperty.RegisterAttached("ControlThickness",
             typeof(int), typeof(GnosisDateField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
        //new FrameworkPropertyMetadata(0,
        //    FrameworkPropertyMetadataOptions.Inherits));

        public static void SetControlThickness(UIElement element, int value)
        {
            element.SetValue(ControlThicknessProperty, value);
        }

        public static int GetControlThickness(UIElement element)
        {
            return (int)element.GetValue(ControlThicknessProperty);
        }

        public static void ControlThicknessPropertyChanged(object source, DependencyPropertyChangedEventArgs e)
        {
            GnosisDateField panelField = source as GnosisDateField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease padding
                marginHorizontal = panelField.Margin.Left - newThickness;
                marginVertical = panelField.Margin.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase padding
                marginHorizontal = panelField.Margin.Left + oldThickness;
                marginVertical = panelField.Margin.Top + oldThickness;
            }

            panelField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
            panelField.BorderThickness = new Thickness(newThickness);

        }

        public GnosisDateField()
        {
            InitializeComponent();

            this.MouseDown += GnosisDateFieldWPF_MouseDown;
            this.MouseUp += GnosisDateFieldWPF_MouseUp;
            this.MouseEnter += GnosisDateFieldWPF_MouseEnter;
            this.MouseLeave += GnosisDateFieldWPF_MouseLeave;

          //  this.PropertyChanged += GnosisDateField_PropertyChanged;
        }

        //private void GnosisDateField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "ContentVerticalAlignment":
        //            SetVerticalContentAlignment(contentVerticalAlignment);
        //            break;
        //        case "ContentHorizontalAlignment":
        //            SetHorizontalContentAlignment(contentHorizontalAlignment);
        //            break;
        //        case "Hidden":
        //            this.SetVisible(!hidden);
        //            break;
        //        case "Locked":
        //            if (locked || readOnly)
        //            {
        //                //replace datepicker with textbox
        //                string text = picker.Text;
        //                picker.Visibility = System.Windows.Visibility.Collapsed;

        //                txtDate.Visibility = System.Windows.Visibility.Visible;
        //                txtDate.Text = text;
        //            }
        //            else
        //            {
        //                if (txtDate.Visibility == System.Windows.Visibility.Visible)
        //                {
        //                    string text = txtDate.Text;
        //                    txtDate.Visibility = System.Windows.Visibility.Collapsed;

        //                    picker.SelectedDate = Convert.ToDateTime(text);
        //                    picker.Visibility = System.Windows.Visibility.Visible;
        //                }
        //            }
        //            break;
        //        case "LongDateFormat":
        //            if (longDateFormat)
        //            {
        //                picker.SelectedDateFormat = DatePickerFormat.Long;
        //            }
        //            else
        //            {
        //                picker.SelectedDateFormat = DatePickerFormat.Short;
        //            }
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = toolip;
        //            break;
        //    }

        //}

        public void SetDate(DateTime date)
        {
            if (Locked)
            {
                picker.SelectedDate = date;
            }
            else
            {
                txtDate.IsReadOnly = false;
                if (picker.SelectedDateFormat == DatePickerFormat.Short)
                {
                    txtDate.Text = date.ToShortDateString();
                }
                else
                {
                    txtDate.Text = date.ToLongDateString();
                }
                txtDate.IsReadOnly = true;
            }
        }

        //public void SetDateFormat(GlobalData.GnosisDateFormat dateFormat)
        //{
        //    if (dateFormat == GlobalData.GnosisDateFormat.SHORT)
        //    {
        //        picker.SelectedDateFormat = DatePickerFormat.Short;
        //    }
        //    else if (dateFormat == GlobalData.GnosisDateFormat.LONG)
        //    {
        //        picker.SelectedDateFormat = DatePickerFormat.Long;
        //    }

        //}

        //public void SetLocked(bool locked)
        //{
        //    if (locked)
        //    {
        //        //replace datepicker with textbox
        //        string text = picker.Text;
        //        picker.Visibility = System.Windows.Visibility.Collapsed;

        //        txtDate.Visibility = System.Windows.Visibility.Visible;
        //        txtDate.Text = text;
        //    }
        //    else
        //    {
        //        if (txtDate.Visibility == System.Windows.Visibility.Visible)
        //        {
        //            string text = txtDate.Text;
        //            txtDate.Visibility = System.Windows.Visibility.Collapsed;

        //            picker.SelectedDate = Convert.ToDateTime(text);
        //            picker.Visibility = System.Windows.Visibility.Visible;
        //        }
        //    }
        //    Locked = locked;
        //}

        public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {

            switch (verticalAlignment)
            {
                case GnosisController.VerticalAlignmentType.TOP:
                    picker.VerticalContentAlignment = VerticalAlignment.Top;
                    txtDate.VerticalContentAlignment = VerticalAlignment.Top;
                    break;
                case GnosisController.VerticalAlignmentType.CENTRE:
                    picker.VerticalContentAlignment = VerticalAlignment.Center;
                    txtDate.VerticalContentAlignment = VerticalAlignment.Center;
                    break;
                case GnosisController.VerticalAlignmentType.BOTTOM:
                    picker.VerticalContentAlignment = VerticalAlignment.Bottom;
                    txtDate.VerticalContentAlignment = VerticalAlignment.Bottom;
                    break;
                case GnosisController.VerticalAlignmentType.STRETCH:
                    picker.VerticalContentAlignment = VerticalAlignment.Stretch;
                    txtDate.VerticalContentAlignment = VerticalAlignment.Stretch;
                    break;
            }
        }

        public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case GnosisController.HorizontalAlignmentType.LEFT:
                    picker.HorizontalContentAlignment = HorizontalAlignment.Left;
                    txtDate.HorizontalContentAlignment = HorizontalAlignment.Left;
                    break;
                case GnosisController.HorizontalAlignmentType.CENTRE:
                    picker.HorizontalContentAlignment = HorizontalAlignment.Center;
                    txtDate.HorizontalContentAlignment = HorizontalAlignment.Center;
                    break;
                case GnosisController.HorizontalAlignmentType.RIGHT:
                    picker.HorizontalContentAlignment = HorizontalAlignment.Right;
                    txtDate.HorizontalContentAlignment = HorizontalAlignment.Right;
                    break;
                case GnosisController.HorizontalAlignmentType.STRETCH:
                    picker.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    txtDate.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    break;
            }
        }

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    picker.IsEnabled = IsEnabled;
        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }
        //public void SetTextLength(int numCharacters)
        //{
        //    double width = numCharacters * StyleController.GetCharacterWidth(picker.FontFamily, picker.FontSize, picker.FontStyle, picker.FontWeight, picker.FontStretch);
        //    picker.Width = width;
        //    txtDate.Width = width;
        //}

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            
            switch (horizontalAlignment)
            {
                case GnosisController.HorizontalAlignmentType.LEFT:
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case GnosisController.HorizontalAlignmentType.CENTRE:
                    this.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case GnosisController.HorizontalAlignmentType.RIGHT:
                    this.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
                case GnosisController.HorizontalAlignmentType.STRETCH:
                    this.HorizontalAlignment = HorizontalAlignment.Stretch;
                    break;
            }
        }

        public void SetMaxPrintWidth(double maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetCaption(string caption)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        public void SetVisible(bool visible)
        {
            if (visible)
            {
                if (Locked)
                {
                    txtDate.Visibility = Visibility.Visible;
                }
                else
                {
                    picker.Visibility = Visibility.Visible;
                }
            }
            else
            {
                txtDate.Visibility = Visibility.Collapsed;
                picker.Visibility = Visibility.Collapsed;
            }
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisDateFieldWPF_MouseDown;
        //}

        private void GnosisDateFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

            

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisDateFieldWPF_MouseUp;
        //}

        private void GnosisDateFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisDateFieldWPF_MouseEnter;
        //}

        private void GnosisDateFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetBorderColour(string borderColour)
        //{
        //    Brush brush = StyleHelper.GetBrushFromHex(borderColour);
        //    picker.BorderBrush = brush;
        //    picker.BorderThickness = new Thickness(1);

        //    txtDate.BorderBrush = brush;
        //    txtDate.BorderThickness = new Thickness(1);
        //}

        //public void SetFont(string font)
        //{
        //    picker.FontFamily = new FontFamily(font);
        //    txtDate.FontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    picker.FontSize = fontSize;
        //    txtDate.FontSize = fontSize;
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    Brush brush = StyleHelper.GetBrushFromHex(backgroundColour);
        //    picker.Background = brush;
        //    txtDate.Background = brush;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    Brush brush = StyleHelper.GetBrushFromHex(contentColour);
        //    picker.Foreground = brush;
        //    txtDate.Foreground = brush;
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisDateFieldWPF_MouseLeave;
        //}

        private void GnosisDateFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetStrikethrough(bool strikethrough)
        {
            
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveOutlineColour()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisDateFieldController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    picker.Padding = new Thickness(paddingHorizontal, picker.Padding.Top, paddingHorizontal, picker.Padding.Bottom);
        //    txtDate.Padding = new Thickness(paddingHorizontal, txtDate.Padding.Top, paddingHorizontal, txtDate.Padding.Bottom);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    picker.Padding = new Thickness(picker.Padding.Left, paddingVertical, picker.Padding.Right, paddingVertical);
        //    txtDate.Padding = new Thickness(txtDate.Padding.Left, paddingVertical, txtDate.Padding.Right, paddingVertical);
        //}



        //public double GetPaddingHorizontal()
        //{
        //    return picker.Padding.Left;
        //}

        public void SetMinWidth(double minWidth)
        {
            this.MinWidth = minWidth;
        }

        public void SetMaxWidth(double maxWidth)
        {
            this.MaxWidth = maxWidth;
        }

        public void SetWidth(double width)
        {
            this.Width = width;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisDateFieldWPF_GotFocus;
        }

        private void GnosisDateFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisDateFieldWPF_LostFocus;
        }

        private void GnosisDateFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }

        public double GetWidth()
        {
            return this.ActualWidth;
        }


    }
}
