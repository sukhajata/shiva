using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using Shiva.Shared.PanelFieldControllers;
using ShivaWPF3.UtilityWPF;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Controls.Primitives;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisDateTimeFieldWPF.xaml
    /// </summary>
    public partial class GnosisDateTimeField : Border, IGnosisDateTimeFieldImplementation
    {
        //private GnosisDateTimeFieldController controller;
        //private Action GotMouseFocusHandler;
        //private Action LostMouseFocusHandler;
        //private Action MouseDownHandler;
        //private Action MouseUpHandler;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool locked;

        private string caption;
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
        private bool longTimeFormat;
        private int minDisplayChars;
        private int maxDisplayChars;
        private int order;
        private bool readOnly;
        private string tooltip;
        private string valueField;
        private int variableControlID;
        private int variableSystemID;
        private bool variableIsInput;
        private bool variableIsOutput;

        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
               // string xaml = XamlWriter.Save(this.Style);

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
               // OnPropertyChanged("Hidden");
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
                return tooltip;
            }

            set
            {
                tooltip = value;
                this.ToolTip = tooltip;
                //OnPropertyChanged("Tooltip");
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
                    datePicker.SelectedDateFormat = DatePickerFormat.Long;
                }
                else
                {
                    datePicker.SelectedDateFormat = DatePickerFormat.Short;
                }
                //OnPropertyChanged("LongDateFormat");
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
                    string datetime = datePicker.Text + " " + timePicker.Text + " " + cboTimeZone.Text;
                    datePicker.Visibility = Visibility.Collapsed;
                    timePicker.Visibility = Visibility.Collapsed;
                    cboTimeZone.Visibility = Visibility.Collapsed;
                    txtDateTime.Visibility = Visibility.Visible;
                    txtDateTime.Text = datetime;
                    txtDateTime.IsReadOnly = true;
                }
                else
                {
                    datePicker.Visibility = Visibility.Visible;
                    timePicker.Visibility = Visibility.Visible;
                    cboTimeZone.Visibility = Visibility.Visible;
                    txtDateTime.Visibility = Visibility.Collapsed;
                }
                OnPropertyChanged("Locked");
            }
        }

        [GnosisPropertyAttribute]
        public bool LongTimeFormat
        {
            get
            {
                return longTimeFormat;
            }

            set
            {
                longTimeFormat = value;

                if (longTimeFormat)
                {
                    timePicker.Format = Xceed.Wpf.Toolkit.DateTimeFormat.LongTime;
                }
                else
                {
                    timePicker.Format = Xceed.Wpf.Toolkit.DateTimeFormat.ShortTime;
                }
                //OnPropertyChanged("LongTimeFormat");
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
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;

        private GlobalData.GnosisTimeFormat timeFormat;
      

        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set
            {
                horizontalPadding = value;

                txtDate.SetHorizontalPaddingExt(horizontalPadding);
                timePicker.SetHorizontalPaddingExt(horizontalPadding);
                cboTimeZone.SetHorizontalPaddingExt(horizontalPadding);
                txtDateTime.SetHorizontalPaddingExt(horizontalPadding);
                txtTimeZone.SetHorizontalPaddingExt(horizontalPadding);
            }
        }

        public int VerticalPadding
        {
            get { return verticalPadding; }
            set
            {
                verticalPadding = value;

                txtDate.SetVerticalPaddingExt(verticalPadding);
                timePicker.SetVerticalPaddingExt(verticalPadding);
                cboTimeZone.SetVerticalPaddingExt(verticalPadding);
                txtDateTime.SetVerticalPaddingExt(verticalPadding);
                txtTimeZone.SetVerticalPaddingExt(verticalPadding);
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
            typeof(int), typeof(GnosisDateTimeField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisDateTimeField panelField = source as GnosisDateTimeField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin
                marginHorizontal = panelField.Margin.Left - newThickness;
                marginVertical = panelField.Margin.Top - newThickness;

                if (marginHorizontal >= 0 && marginVertical >= 0)
                {
                    panelField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                    panelField.BorderThickness = new Thickness(newThickness);
                    panelField.Height = panelField.Height + (newThickness - oldThickness);
                }
            }
            else
            {
                //decrease border thickness, increase margin
                marginHorizontal = panelField.Margin.Left + oldThickness;
                marginVertical = panelField.Margin.Top + oldThickness;

                if (marginHorizontal >= 0 && marginVertical >= 0)
                {
                    panelField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                    panelField.BorderThickness = new Thickness(newThickness);
                    panelField.Height = panelField.Height - (oldThickness - newThickness);
                }
            }

            
        }

        public GnosisDateTimeField()
        {
            InitializeComponent();

            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach (var timezone in timeZones)
            {
                
                GnosisTimeZone gtz = new GnosisTimeZone(timezone.DisplayName, timezone.DisplayName.Substring(0, 11));
                cboTimeZone.Items.Add(gtz);
                if (timezone.Equals(TimeZoneInfo.Local))
                {
                    cboTimeZone.SelectedItem = gtz;
                }
                
            }
            //cboTimeZone.ItemsSource = timeZones;

            this.MouseEnter += GnosisDateTimeFieldWPF_MouseEnter;
            this.MouseDown += GnosisDateTimeFieldWPF_MouseDown;
            this.MouseUp += GnosisDateTimeFieldWPF_MouseUp;
            this.MouseLeave += GnosisDateTimeFieldWPF_MouseLeave;

           // this.PropertyChanged += GnosisDateTimeField_PropertyChanged;


            SetDateFormat(GlobalData.GnosisDateFormat.LONG);
            SetTimeFormat(GlobalData.GnosisTimeFormat.MINUTE);
        }

        //private void GnosisDateTimeField_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Locked":
        //            if (locked || readOnly)
        //            {
        //                string datetime = datePicker.Text + " " + timePicker.Text + " " + cboTimeZone.Text;
        //                datePicker.Visibility = Visibility.Collapsed;
        //                timePicker.Visibility = Visibility.Collapsed;
        //                cboTimeZone.Visibility = Visibility.Collapsed;
        //                txtDateTime.Visibility = Visibility.Visible;
        //                txtDateTime.Text = datetime;
        //                txtDateTime.IsReadOnly = true;
        //            }
        //            else
        //            {
        //                datePicker.Visibility = Visibility.Visible;
        //                timePicker.Visibility = Visibility.Visible;
        //                cboTimeZone.Visibility = Visibility.Visible;
        //                txtDateTime.Visibility = Visibility.Collapsed;
        //            }
        //            break;
        //        case "LongDateFormat":
        //            if (longDateFormat)
        //            {
        //                datePicker.SelectedDateFormat = DatePickerFormat.Long;
        //            }
        //            else
        //            {
        //                datePicker.SelectedDateFormat = DatePickerFormat.Short;
        //            }
        //            break;
        //        case "LongTimeFormat":
        //            if (longTimeFormat)
        //            {
        //                timePicker.Format = Xceed.Wpf.Toolkit.DateTimeFormat.LongTime;
        //            }
        //            else
        //            {
        //                timePicker.Format = Xceed.Wpf.Toolkit.DateTimeFormat.ShortTime;
        //            }
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        public void SetDateTime(DateTime dateTime)
        {
            datePicker.SelectedDate = dateTime;
            timePicker.Value = dateTime;
            if (datePicker.SelectedDateFormat == DatePickerFormat.Long)
            {
                txtDate.Text = dateTime.ToLongDateString();
            }
            else
            {
                txtDate.Text = dateTime.ToShortDateString();
            }
            txtDateTime.Text = datePicker.Text + " " + timePicker.Text;
            
        }

        public void SetDateFormat(GlobalData.GnosisDateFormat dateFormat)
        {
            if (dateFormat == GlobalData.GnosisDateFormat.SHORT)
            {
                datePicker.SelectedDateFormat = DatePickerFormat.Short;
            }
            else if (dateFormat == GlobalData.GnosisDateFormat.LONG)
            {
                datePicker.SelectedDateFormat = DatePickerFormat.Long;
            }

        }

        public void SetTimeFormat(GlobalData.GnosisTimeFormat _timeFormat)
        {
            timeFormat = _timeFormat;
            timePicker.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;

            switch (timeFormat)
            {
                case GlobalData.GnosisTimeFormat.HOUR:
                    timePicker.FormatString = "h tt";
                    break;
                case GlobalData.GnosisTimeFormat.MINUTE:
                    timePicker.FormatString = "t"; //"h:mm tt"
                    break;
                case GlobalData.GnosisTimeFormat.SECOND:
                    timePicker.FormatString = "T"; //"h:mm:ss tt"
                    break;
                case GlobalData.GnosisTimeFormat.MILLISECOND:
                    timePicker.FormatString = "h:mm:ss.fff tt";
                    break;
            }

        }

        public void SetMarginBottom(int marginBottom)
        {
            this.Margin = new Thickness { Bottom = marginBottom };
        }

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    string hex = "#" + backgroundColour;
        //    SolidColorBrush backBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        //    this.Background = backBrush;
        //    cboTimeZone.Background = backBrush;
        //    datePicker.Background = backBrush;
        //    timePicker.Background = backBrush;
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    string hex = "#" + borderColour;
        //    SolidColorBrush borderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        //    cboTimeZone.BorderBrush = borderBrush;
        //    datePicker.BorderBrush = borderBrush;
        //    timePicker.BorderBrush = borderBrush;
        //}


        public void SetDefaultTimeZone(TimeZoneInfo timezone)
        {
            cboTimeZone.SelectedItem = timezone;
        }

        //public void SetFont(string font)
        //{
        //    cboTimeZone.FontFamily = new FontFamily(font);
        //    datePicker.FontFamily = new FontFamily(font);
        //    timePicker.FontFamily = new FontFamily(font);
        //    txtDateTime.FontFamily = new FontFamily(font);
        //    txtTimeZone.FontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    cboTimeZone.FontSize = fontSize;
        //    datePicker.FontSize = fontSize;
        //    timePicker.FontSize = fontSize;
        //    txtDateTime.FontSize = fontSize;
        //    txtTimeZone.FontSize = fontSize;
        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetForegroundColour(string contentColour)
        //{
        //    string hex = "#" + contentColour;
        //    SolidColorBrush fontBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        //    cboTimeZone.Foreground = fontBrush;
        //    datePicker.Foreground = fontBrush;
        //    timePicker.Foreground = fontBrush;
        //}

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisDateTimeFieldWPF_MouseEnter;
        //}

        private void GnosisDateTimeFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);
            
        }

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        public virtual void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            datePicker.SetHorizontalContentAlignmentExt(horizontalAlignment);
            timePicker.SetHorizontalContentAlignmentExt(horizontalAlignment);
            cboTimeZone.SetHorizontalContentAlignmentExt(horizontalAlignment);
            
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    datePicker.IsEnabled = isEnabled;
        //    timePicker.IsEnabled = isEnabled;
        //    cboTimeZone.IsEnabled = isEnabled;
        //}

        //public void SetLocked(bool locked)
        //{

        //    if (locked)
        //    {
        //        string datetime = datePicker.Text + " " + timePicker.Text + " " + cboTimeZone.Text;
        //        datePicker.Visibility = Visibility.Collapsed;
        //        timePicker.Visibility = Visibility.Collapsed;
        //        cboTimeZone.Visibility = Visibility.Collapsed;
        //        txtDateTime.Visibility = Visibility.Visible;
        //        txtDateTime.Text = datetime;
        //        txtDateTime.IsReadOnly = true;
        //    }
        //    else
        //    {
        //        datePicker.Visibility = Visibility.Visible;
        //        timePicker.Visibility = Visibility.Visible;
        //        cboTimeZone.Visibility = Visibility.Visible;
        //        txtDateTime.Visibility = Visibility.Collapsed;
        //    }

        //    Locked = locked;

        //}


        //public void SetMargin(int left, int top, int right, int bottom)
        //{
        //    this.Margin = new System.Windows.Thickness(left, top, right, bottom);
        //}


        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisDateTimeFieldWPF_MouseDown;
        //    datePicker.PreviewMouseDown += GnosisDateTimeFieldWPF_MouseDown;
        //    timePicker.PreviewMouseDown += GnosisDateTimeFieldWPF_MouseDown;
        //    cboTimeZone.PreviewMouseDown += GnosisDateTimeFieldWPF_MouseDown;
        //}

        private void GnosisDateTimeFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisDateTimeFieldWPF_MouseUp;
        //    datePicker.PreviewMouseUp += GnosisDateTimeFieldWPF_MouseUp;
        //    timePicker.PreviewMouseUp += GnosisDateTimeFieldWPF_MouseUp;
        //    cboTimeZone.PreviewMouseUp += GnosisDateTimeFieldWPF_MouseUp;
        //}

        private void GnosisDateTimeFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetTooltip(string tooltip)
        //{
        //    cboTimeZone.ToolTip = tooltip;
        //    datePicker.ToolTip = tooltip;
        //    timePicker.ToolTip = tooltip;
        //}

        public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            cboTimeZone.SetVerticalContentAlignmentExt(verticalAlignment);
            datePicker.SetVerticalContentAlignmentExt(verticalAlignment);
            timePicker.SetVerticalContentAlignmentExt(verticalAlignment);
            
        }

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        ////public void SetLostMouseFocusHandler(Action action)
        ////{
        ////    LostMouseFocusHandler = action;
        ////    this.MouseLeave += GnosisDateTimeFieldWPF_MouseLeave;
        ////}

        private void GnosisDateTimeFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetStrikethrough(bool strikethrough)
        {
            txtDateTime.TextDecorations = TextDecorations.Strikethrough;
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

        //public void SetMargin(int margin)
        //{
        //    this.Margin = new Thickness(margin);
        //}

        //public void SetMaxWidth(int maxWidth)
        //{
        //    this.MaxWidth = maxWidth;
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisDateTimeFieldController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        //public void SetTextLength(int numCharacters)
        //{
        //    this.Width = numCharacters * StyleController.GetCharacterWidth(datePicker.FontFamily, datePicker.FontSize, datePicker.FontStyle, datePicker.FontWeight, datePicker.FontStretch);
        //}

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            datePicker.SetHorizontalPaddingExt(paddingHorizontal);
            timePicker.SetHorizontalPaddingExt(paddingHorizontal);
            cboTimeZone.SetHorizontalPaddingExt(paddingHorizontal);
            txtDateTime.SetHorizontalPaddingExt(paddingHorizontal);
            txtTimeZone.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            datePicker.SetVerticalPaddingExt(paddingVertical);
            timePicker.SetVerticalPaddingExt(paddingVertical);
            cboTimeZone.SetVerticalPaddingExt(paddingVertical);
            txtDateTime.SetVerticalPaddingExt(paddingVertical);
            txtTimeZone.SetVerticalPaddingExt(paddingVertical);
        }


        //public FontFamily GetFontFamily()
        //{
        //    return datePicker.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return datePicker.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return datePicker.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return datePicker.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return datePicker.FontStretch;
        //}

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

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

        public double GetSetWidth()
        {
            //this.Width = width;

            double characterWidth = GlobalData.Singleton.StyleHelper.GetCharacterWidth(datePicker.FontFamily.ToString(), (int)datePicker.FontSize);
            if (datePicker.SelectedDateFormat == DatePickerFormat.Short)
            {
                txtDate.Width = (10 * characterWidth) + (2 * verticalPadding);
            }
            else
            {
                txtDate.Width = (26 * characterWidth) + (2 * verticalPadding);
            }

            datePicker.Width = 25;

            switch (timeFormat)
            {
                case GlobalData.GnosisTimeFormat.HOUR:
                    timePicker.Width = 6 * characterWidth + 30 + 2 * verticalPadding;
                    break;
                case GlobalData.GnosisTimeFormat.MINUTE:
                    timePicker.Width = 9 * characterWidth + 30 + 2 * verticalPadding;
                    break;
                case GlobalData.GnosisTimeFormat.SECOND:
                    timePicker.Width = 12 * characterWidth + 30 + 2 * verticalPadding;
                    break;
                case GlobalData.GnosisTimeFormat.MILLISECOND:
                    timePicker.Width = 16 * characterWidth + 30 + 2 * verticalPadding;
                    break;
            }

            cboTimeZone.Width = 12 * characterWidth + 20 + 2 * verticalPadding;

            double width = txtDate.Width + datePicker.Width + timePicker.Width + cboTimeZone.Width;
            this.Width = width;

            return width;
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }

        //public double GetWidthNeeded()
        //{
        //    double width;
        //    double numChars;
        //    if (datePicker.SelectedDateFormat == DatePickerFormat.Short)
        //    {
        //        numChars = 11;
        //    }
        //    else
        //    {
        //        numChars = 28;
        //    }

        //    switch (timeFormat)
        //    {
        //        case GlobalData.GnosisTimeFormat.HOUR:
        //            numChars += 6;
        //            break;
        //        case GlobalData.GnosisTimeFormat.MINUTE:
        //            numChars += 9;
        //            break;
        //        case GlobalData.GnosisTimeFormat.SECOND:
        //            numChars += 12;
        //            break;
        //        case GlobalData.GnosisTimeFormat.MILLISECOND:
        //            numChars += 16;
        //            break;
        //    }

        //    //timezone
        //    numChars += 12;

        //    width = numChars * GlobalData.Singleton.StyleHelper.GetCharacterWidth(datePicker.FontFamily.ToString(), (int)datePicker.FontSize);

        //    //icons
        //    width += 20 + 30 + 20;

        //    return width;
        //}

        public double GetWidth()
        {
            return this.ActualWidth;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisDateTimeFieldWPF_GotFocus;
        }

        private void GnosisDateTimeFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisDateTimeFieldWPF_LostFocus;
        }

        private void GnosisDateTimeFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public DateTime GetDateTime()
        {
            DateTime dateTime = datePicker.DisplayDate.Add(timePicker.TimeInterval);

            return dateTime;
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }

        private void cboTimeZone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTimeZone.SelectedItem != null)
            {
                string timeZone = cboTimeZone.SelectedItem as string;
              //  cboTimeZone.Text = timeZone.Substring(0, 11);
            }
            
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dateTime = (DateTime)datePicker.SelectedDate;
            if (datePicker.SelectedDateFormat == DatePickerFormat.Long)
            {
                txtDate.Text = dateTime.ToLongDateString();
            }
            else
            {
                txtDate.Text = dateTime.ToShortDateString();
            }
        }
    }



    public class GnosisTimeZone
    {
        private string longTime;
        private string shortTime;

        public string LongTime
        {
            get { return longTime; }
        }

        public string ShortTime
        {
            get { return shortTime; }
        }

        public override string ToString()
        {
            return shortTime;
        }

        public GnosisTimeZone(string _longTime, string _shortTime)
        {
            longTime = _longTime;
            shortTime = _shortTime;
        }
    }
}
