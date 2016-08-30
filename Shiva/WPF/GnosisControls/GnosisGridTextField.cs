using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Shiva.Shared.BaseControllers;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;
using Shiva.Shared.Data;

namespace GnosisControls
{
    public partial class GnosisGridTextField : TextBox, IGnosisGridTextFieldImplementation, INotifyPropertyChanged
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        protected int horizontalPadding;
        protected int verticalPadding;

        private bool isEvenRow;
        private bool rowSelected;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

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
        private bool locked;
        private int maxChars;
        private int maxDisplayChars;
        private int maxTextDisplayWidthChars;
        private int maxSectionSpan;
        private int minDisplayChars;
        private int minTextDisplayWidthChars;
        private bool optional;
        private int order;
        private bool readOnly;
        private string tooltip;
        private string valueField;


        //Dynamic Properties
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

        public bool RowSelected
        {
            get { return rowSelected; }
            set
            {
                rowSelected = value;
                OnPropertyChanged("RowSelected");
                // string xaml = XamlWriter.Save(this.Style);
            }
        }

        //Static properties
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
                    contentVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
                    OnPropertyChanged("ContentVerticalAlignment");
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
            set { contentVerticalAlignment = value; }
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
                    contentHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                    OnPropertyChanged("ContentHorizontalAlignment");
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get { return contentHorizontalAlignment; }
            set { contentHorizontalAlignment = value; }
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

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisProperty]
        public bool IsEvenRow
        {
            get { return isEvenRow; }
            set
            {
                isEvenRow = value;
                OnPropertyChanged("IsEvenRow");
            }
        }

        [GnosisPropertyAttribute]
        public int MaxChars
        {
            get
            {
                return maxChars;
            }

            set
            {
                maxChars = value;
                OnPropertyChanged("MaxChars");
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
            get
            {
                return gnosisName;
            }

            set
            {
                gnosisName = value;
            }
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
                return tooltip;
            }

            set
            {
                tooltip = value;
                OnPropertyChanged("Tooltip");
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

        [GnosisPropertyAttribute]
        public int MaxTextDisplayWidthChars
        {
            get
            {
                return maxTextDisplayWidthChars;
            }

            set
            {
                maxTextDisplayWidthChars = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MinTextDisplayWidthChars
        {
            get
            {
                return minTextDisplayWidthChars;
            }

            set
            {
                minTextDisplayWidthChars = value;
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
            }
        }

        [GnosisPropertyAttribute]
        public bool Locked
        {
            get { return locked; }
            set
            {
                locked = value;

                OnPropertyChanged("Locked");
            }
        }

        [GnosisPropertyAttribute]
        public bool Optional
        {
            get { return optional; }
            set
            {
                optional = value;
                OnPropertyChanged("Optional");
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }

    

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

        public static readonly DependencyProperty ControlThicknessProperty =
            DependencyProperty.RegisterAttached("ControlThickness",
            typeof(int), typeof(GnosisGridTextField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisGridTextField textField = source as GnosisGridTextField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin
                marginHorizontal = textField.Margin.Left - newThickness;
                marginVertical = textField.Margin.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase margin
                marginHorizontal = textField.Margin.Left + oldThickness;
                marginVertical = textField.Margin.Top + oldThickness;
            }

            textField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
            textField.BorderThickness = new Thickness(newThickness);

        }



        public int NumLines
        {
            get; set;
        }

     

        public GnosisGridTextField()
        {
           // this.Background = Brushes.Transparent;  //allow alternate row colour to show
            this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.Margin = new System.Windows.Thickness(0, 0, 1, 1);

            this.MouseEnter += GnosisTextFieldWPF_MouseEnter;
            this.MouseLeave += GnosisTextFieldWPF_MouseLeave;
            this.PreviewMouseDown += GnosisTextFieldWPF_MouseDown;
            this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;

            this.PropertyChanged += GnosisGridTextField_PropertyChanged;

        }

        protected virtual void GnosisGridTextField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "ContentVerticalAlignment":
                    this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
                    break;
                case "ContentHorizontalAlignment":
                    this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Locked":
                    if (!readOnly)
                    {
                        this.IsReadOnly = locked;
                    }
                    break;
                case "MaxChars":
                    this.MaxLength = maxChars;
                    break;
                case "ReadOnly":
                    this.IsReadOnly = readOnly;
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        //public void SetController(GnosisVisibleController _controller)
        //{
        //    controller = (GnosisGridFieldController)_controller;
        //}

        public void CentreText()
        {
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        }

        public string GetText()
        {
            return this.Text;
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }


        public void SetText(string text)
        {
            this.Text = text;
        }

        public void SetMaxLines(int maxLines)
        {
            this.MaxLines = maxLines;
        }

        //public void SetLocked(bool locked)
        //{
        //    this.IsReadOnly = locked;
        //}

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

        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetFont(string font)
        //{
        //    this.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    this.FontSize = fontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisTextFieldWPF_MouseEnter;
        //}

        private void GnosisTextFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }


        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            //breaks textbox filling available space
            this.SetHorizontalAlignmentExt(horizontalAlignment);
            
        }
        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    this.SetHorizontalContentAlignmentExt(horizontalAlignment);
            
        //}

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    this.IsEnabled = IsEnabled;
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisTextFieldWPF_MouseLeave;
        //}

        private void GnosisTextFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
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
        //    this.MaxWidth = MaxWidth;
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.PreviewMouseDown += GnosisTextFieldWPF_MouseDown;
        //}

        private void GnosisTextFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;
        //}

        private void GnosisTextFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
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

        //public void SetStrikethrough()
        //{

        //}

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    this.SetVerticalContentAlignmentExt(verticalAlignment);
            
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        public void SetWidth(double width)
        {
            this.Width = width;
        }


        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.SetVerticalPaddingExt(paddingVertical);
        }

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

        public void SetMinWidth(double minWidth)
        {
            this.MinWidth = minWidth;
        }

        public void SetMaxWidth(double maxWidth)
        {
            this.MaxWidth = maxWidth;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisGridTextFieldWPF_GotFocus;
        }

        private void GnosisGridTextFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisGridTextFieldWPF_LostFocus;
        }

        private void GnosisGridTextFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetReadOnly(bool isReadOnly)
        //{
        //    this.IsReadOnly = isReadOnly;
        //}

        public void SetStrikethrough(bool strikethrough)
        {

            if (strikethrough)
            {
                this.TextDecorations = System.Windows.TextDecorations.Strikethrough;
            }
            else
            {
                this.TextDecorations = null;
            }
        }

        public void SetTextWrapping(bool wrap)
        {
            if (wrap)
            {
                this.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                this.TextWrapping = TextWrapping.NoWrap;
            }
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
