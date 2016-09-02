using Shiva.Shared.Interfaces;
using Shiva.Shared.PanelFieldControllers;
using ShivaWPF3.UtilityWPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using Shiva.Shared.Data;
using System.Windows.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisCheckFieldWPF.xaml
    /// </summary>
    public partial class GnosisCheckField : UserControl, IGnosisCheckFieldImplementation, INotifyPropertyChanged
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool locked;

        private string caption;
        private bool disabled;
        private GnosisController.VerticalAlignmentType contentVerticalAlignment;
        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
        private int checkedFactor;
        private string controlType;
        private bool datasetCreated;
        private bool datasetUpdated;
        private bool datasetDeleted;
        private string dataset;
        private string datasetItem;
        private bool gnosisChecked;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private string groupName;
        private bool hidden;
        private string icon;
        private int id;
        private int minDisplayChars;
        private int maxDisplayChars;
        private int order;
        private bool readOnly;
        private string shortcut;
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
            }
        }
        public bool HasMouseFocus
        {
            get { return hasMouseFocus; }
            set
            {
                hasMouseFocus = value;
                OnPropertyChanged("HasMouseFocus");
                //string xaml = XamlWriter.Save(this);
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
                    //this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
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
                    //this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
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
                lblCaption.SetVerticalContentAlignmentExt(contentVerticalAlignment);
            }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get { return contentHorizontalAlignment; }
            set
            {
                contentHorizontalAlignment = value;
                lblCaption.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
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
                lblCaption.Content = caption;
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
                //OnPropertyChanged("MinDisplayChars");
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
                //OnPropertyChanged("MaxDisplayChars");
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
                OnPropertyChanged("Tooltip");
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
                    chkBox.IsEnabled = !locked;
                    OnPropertyChanged("Locked");

                }
            }
        }

        [GnosisPropertyAttribute]
        public int CheckedFactor
        {
            get
            {
                return checkedFactor;
            }

            set
            {
                checkedFactor = value;
            }
        }

        [GnosisPropertyAttribute]
        public string GnosisGroupName
        {
            get
            {
                return groupName;
            }

            set
            {
                groupName = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool GnosisChecked
        {
            get
            {
                return gnosisChecked;
            }

            set
            {
                gnosisChecked = value;
                OnPropertyChanged("GnosisChecked");
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
                if (readOnly)
                {
                    locked = true;
                    OnPropertyChanged("Locked");
                }
                //OnPropertyChanged("ReadOnly");
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
        //private Action GotMouseFocusHandler;
        //private Action LostMouseFocusHandler;
        //private Action MouseDownHandler;
        //private Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

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
                lblCaption.Padding = new Thickness(0, 0, horizontalPadding, 0);
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
        typeof(int), typeof(GnosisCheckField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisCheckField checkField = source as GnosisCheckField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double paddingHorizontal;
            double paddingVertical;

            
            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease padding
                paddingHorizontal = checkField.Padding.Left - newThickness;
                paddingVertical = checkField.Padding.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase padding
                paddingHorizontal = checkField.Padding.Left + oldThickness;
                paddingVertical = checkField.Padding.Top + oldThickness;
            }

            if (paddingHorizontal >= 0 && paddingVertical >= 0)
            {
                //control thickness is around check box only. The left padding of the field will not be affected
                checkField.Padding = new Thickness(paddingHorizontal + newThickness, paddingVertical, paddingHorizontal, paddingVertical);
                checkField.lblCaption.Padding = new Thickness(0, 0, paddingHorizontal, 0);
                checkField.chkBox.BorderThickness = new Thickness(newThickness);
            }
        }

        public GnosisCheckField()
        {
            InitializeComponent();

            this.MouseEnter += GnosisCheckFieldWPF_MouseEnter;
            this.MouseLeave += GnosisCheckFieldWPF_MouseLeave;
            this.MouseDown += GnosisCheckFieldWPF_MouseDown;
            this.MouseUp += GnosisCheckFieldWPF_MouseUp;

            Binding binding = new Binding("GnosisChecked");
            binding.Source = this;
            binding.Mode = BindingMode.TwoWay;
            chkBox.SetBinding(CheckBox.IsCheckedProperty, binding);

           // this.PropertyChanged += GnosisCheckField_PropertyChanged;
        }

        //private void GnosisCheckField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            lblCaption.Content = caption;
        //            break;
        //        case "ContentVerticalAlignment":
        //            lblCaption.SetVerticalContentAlignmentExt(contentVerticalAlignment);
        //            break;
        //        case "ContentHorizontalAlignment":
        //            lblCaption.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Locked":
        //            if (!readOnly)
        //            {
        //                chkBox.IsEnabled = !locked;
        //            }
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //        case "ReadOnly":
        //            chkBox.IsEnabled = !readOnly;
        //            break;
        //    }
        //}

        //public void SetIsChecked(bool isChecked)
        //{
        //    chkBox.IsChecked = isChecked;
        //}

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }


        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new System.Windows.Thickness(1);
        //}

        //public void SetCaption(string caption)
        //{
        //    lblCaption.Content = caption;
        //}

        //public void SetFont(string font)
        //{
        //    lblCaption.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    lblCaption.FontSize = fontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    lblCaption.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisCheckFieldWPF_MouseEnter;
        //}

        private void GnosisCheckFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);

        }

        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    switch (horizontalAlignment)
        //    {
        //        case GnosisController.HorizontalAlignmentType.LEFT:
        //            lblCaption.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
        //            break;
        //        case GnosisController.HorizontalAlignmentType.CENTRE:
        //            lblCaption.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
        //            break;
        //        case GnosisController.HorizontalAlignmentType.RIGHT:
        //            lblCaption.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
        //            break;
        //    }
        //}

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    this.IsEnabled = IsEnabled;
        //}

        //public void SetLocked(bool locked)
        //{
        //    this.IsEnabled = !locked;
        //    Locked = locked;
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisCheckFieldWPF_MouseLeave;
        //}

        private void GnosisCheckFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetMaxWidth(int maxWidth)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisCheckFieldWPF_MouseDown;
        //}

        private void GnosisCheckFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisCheckFieldWPF_MouseUp;
        //}

        private void GnosisCheckFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    this.BorderThickness = new System.Windows.Thickness(2);
        //}

        //public void RemoveOutlineColour()
        //{
        //    this.BorderThickness = new System.Windows.Thickness(0);
        //}

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    switch (verticalAlignment)
        //    {
        //        case GnosisController.VerticalAlignmentType.Top:
        //            lblCaption.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
        //            break;
        //        case GnosisController.VerticalAlignmentType.Centre:
        //            lblCaption.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        //            break;
        //        case GnosisController.VerticalAlignmentType.Bottom:
        //            lblCaption.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
        //            break;
        //    }
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisCheckFieldController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}


        //public void SetTextLength(int numCharacters)
        //{
        //    this.Width = numCharacters * StyleController.GetCharacterWidth(lblCaption.FontFamily, lblCaption.FontSize, lblCaption.FontStyle, lblCaption.FontWeight, lblCaption.FontStretch);
        //}

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
        //    return lblCaption.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return lblCaption.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return lblCaption.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return lblCaption.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return lblCaption.FontStretch;
        //}

        public double GetPaddingHorizontal()
        {
            return lblCaption.Padding.Left;
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
           // this.Width = width;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisCheckFieldWPF_GotFocus;
        }

        private void GnosisCheckFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisCheckFieldWPF_LostFocus;
        }

        private void GnosisCheckFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
            double textHeight = GlobalData.Singleton.StyleHelper.GetTextHeight(this, lblCaption.FontFamily.ToString(), (int)lblCaption.FontSize);
            chkBox.Height = textHeight;
            chkBox.Width = textHeight;

        }

        //public bool GetIsChecked()
        //{
        //    return (bool)this.chkBox.IsChecked;
        //}

        public void SetStrikethrough(bool strikethrough)
        {
            
        }


    }
}
