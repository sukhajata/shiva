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
using ShivaWPF3.UtilityWPF;
using Shiva.Shared.PanelFieldControllers;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Data;
using System.Windows.Markup;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTextFieldWPF.xaml
    /// </summary>
    public partial class GnosisTextField : TextBox, IGnosisTextFieldImplementation, INotifyPropertyChanged
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        protected Action<string> TextChangedHandler;

        protected bool userInput = true;
        protected string text = "";

        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;


        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool optional;

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
        private bool locked;
        private int maxChars;
        private int maxDisplayChars;
        private int maxTextDisplayWidthChars;
        private int maxSectionSpan;
        private int minDisplayChars;
        private int minTextDisplayWidthChars;
        private int order;
        private bool readOnly;
        private string tooltip;
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

        
        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
               //string xaml = XamlWriter.Save(this.Style);

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
                  //  OnPropertyChanged("ContentVerticalAlignment");
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
                this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
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
                   // OnPropertyChanged("ContentHorizontalAlignment");
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
            set
            {
                contentHorizontalAlignment = value;
                this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
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
               // string xaml = XamlWriter.Save(this.Style);
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
                this.MaxLength = maxChars;
                //OnPropertyChanged("MaxChars");
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
               // OnPropertyChanged("Caption");
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
                this.SetVisibleExt(!hidden);
                //OnPropertyChanged("Hidden");
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
               // OnPropertyChanged("Tooltip");
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
                this.IsEnabled = !readOnly;
                OnPropertyChanged("ReadOnly");

                if (readOnly)
                {
                    locked = true;
                    OnPropertyChanged("Locked");
                }
            }
        }

        [GnosisPropertyAttribute]
        public bool Locked
        {
            get { return locked; }
            set
            {
                if (!readOnly)
                {
                    locked = value;
                    this.IsEnabled = !locked;
                    OnPropertyChanged("Locked");

                }
                // string xaml = XamlWriter.Save(this.Style);
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

        public int CurrentThickness
        {
            get
            {
                return (int)this.BorderThickness.Top;
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

        public static readonly DependencyProperty ControlThicknessProperty =
            DependencyProperty.RegisterAttached("ControlThickness",
            typeof(int), typeof(GnosisTextField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisTextField textField = source as GnosisTextField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin
                marginHorizontal = textField.Margin.Left - newThickness;
                marginVertical = textField.Margin.Top - newThickness;

                if (marginHorizontal >= 0 && marginVertical >= 0)
                {
                    textField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                    textField.BorderThickness = new Thickness(newThickness);
                    
                    //The control height has changed since BorderThickness is part of height but margin is not
                    double fieldHeight = GlobalData.Singleton.StyleHelper.GetFieldHeight(textField, textField.FontFamily.ToString(), (int)textField.FontSize);
                    textField.SetHeight(fieldHeight);

                }

            }
            else
            {
                //decrease border thickness, increase margin
                marginHorizontal = textField.Margin.Left + oldThickness;
                marginVertical = textField.Margin.Top + oldThickness;

                if (marginHorizontal >= 0 && marginVertical >= 0)
                {
                    textField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                    textField.BorderThickness = new Thickness(newThickness);
                    
                    //The control height has changed since BorderThickness is part of height but margin is not
                    double fieldHeight = GlobalData.Singleton.StyleHelper.GetFieldHeight(textField, textField.FontFamily.ToString(), (int)textField.FontSize);
                    textField.SetHeight(fieldHeight);

                }
            }

           
        }

        public GnosisTextField()
        {
            InitializeComponent();

            this.MouseEnter += GnosisTextFieldWPF_MouseEnter;
            this.MouseLeave += GnosisTextFieldWPF_MouseLeave;
            this.PreviewMouseDown += GnosisTextFieldWPF_MouseDown;
            this.PreviewMouseUp += GnosisTextFieldWPF_MouseUp;

           // this.PropertyChanged += GnosisTextField_PropertyChanged;
        }

        //private void GnosisTextField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "ContentVerticalAlignment":
        //            this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
        //            break;
        //        case "ContentHorizontalAlignment":
        //            this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Locked":
        //            if (!readOnly)
        //            {
        //                this.IsReadOnly = locked;
        //            }
        //            break;
        //        case "MaxChars":
        //            this.MaxLength = maxChars;
        //            break;
        //        case "ReadOnly":
        //            this.IsReadOnly = readOnly;
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        public string GetText()
        {
            return text;
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }


        public void SetText(string _text)
        {
            userInput = false;
            this.Clear();
            this.Text = _text;
            userInput = true;

            //this.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
        }

        public void SetMaxLines(int maxLines)
        {
            this.MaxLines = maxLines;
        }

        public int GetLineCount()
        {
            return this.LineCount;
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

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
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
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetTextChangedHandler(Action<string> handler)
        {
            TextChangedHandler = handler;
            this.TextChanged += GnosisTextField_TextChanged;
        }

        private void GnosisTextField_TextChanged(object sender, TextChangedEventArgs e)
        {

            //cannot use this.Text as it does not have updated value
            TextBox txtBox = e.OriginalSource as TextBox;
            text = txtBox.Text;

            if (this.IsLoaded && userInput)
            {
                TextChangedHandler.Invoke(text);
            }
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

        //public virtual void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisTextFieldController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        //public void SetTextLength(int numCharacters)
        //{
        //    this.Width = numCharacters * StyleController.GetCharacterWidth(this.FontFamily, this.FontSize, this.FontStyle, this.FontWeight, this.FontStretch);
        //}

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

        //public void SetMaxChars(int maxChars)
        //{
        //    this.MaxLength = maxChars;
        //}

        public void SetHeight(double height)
        {
            this.Height = height;
        }

        public int GetRowNumber()
        {
            return Grid.GetRow(this);
        }

        public void SetWidth(double width)
        {
            this.Width = width;
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }

        public double GetWidth()
        {
            return this.ActualWidth;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisTextFieldWPF_GotFocus;
        }

        private void GnosisTextFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            text = this.Text;
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisTextFieldWPF_LostFocus;
        }

        private void GnosisTextFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (TextChangedHandler != null)
            //{
            //    TextChangedHandler.Invoke(text);
            //}

            LostFocusHandler.Invoke();
            HasFocus = false;
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

        
    }
}
