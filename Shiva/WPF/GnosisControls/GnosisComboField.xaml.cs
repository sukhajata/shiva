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
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using System.Windows.Markup;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisComboFieldWPF.xaml
    /// </summary>
    public partial class GnosisComboField : ComboBox, IGnosisComboFieldImplementation
    {
        private Action<GnosisComboOption> optionChangedHandler;
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

        private List<GnosisComboAttribute> comboAttributes;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool locked;
        private bool optional;

        private string caption;
        private GnosisController.VerticalAlignmentType contentVerticalAlignment;
        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
        private string controlType;
        private int documentEntityID;
        private int documentSystemID;
        private bool datasetCreated;
        private bool datasetUpdated;
        private bool datasetDeleted;
        private string dataset;
        private string datasetItem;
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
                   // OnPropertyChanged("ContentHorizontalAlignment");
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
               // OnPropertyChanged("MinDisplayChars");
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
               // OnPropertyChanged("Tooltip");
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
                cbo.IsEnabled = !readOnly;
               // OnPropertyChanged("ReadOnly");

                if (readOnly)
                {
                    locked = true;
                    OnPropertyChanged("Locked");
                }
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
                    cbo.IsEnabled = !locked;
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
               // OnPropertyChanged("Optional");
            }
        }

        [GnosisPropertyAttribute]
        public int DocumentSystemID
        {
            get
            {
                return documentSystemID;
            }

            set
            {
                documentSystemID = value;
            }
        }

        [GnosisPropertyAttribute]
        public int DocumentEntityID
        {
            get
            {
                return documentEntityID;
            }

            set
            {
                documentEntityID = value;
            }
        }

      

        [GnosisCollection]
        public List<GnosisComboAttribute> ComboAttributes
        {
            get { return comboAttributes; }
            set { comboAttributes = value; }
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

        public static readonly DependencyProperty ControlThicknessProperty =
         DependencyProperty.RegisterAttached("ControlThickness",
         typeof(int), typeof(GnosisComboField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisComboField panelField = source as GnosisComboField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease padding
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
                //decrease border thickness, increase padding
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

        public GnosisComboField()
        {
            InitializeComponent();

            cbo.MouseEnter += GnosisComboFieldWPF_MouseEnter;
            cbo.MouseLeave += GnosisComboFieldWPF_MouseLeave;
            cbo.PreviewMouseDown += GnosisComboFieldWPF_MouseDown;
            cbo.PreviewMouseUp += GnosisComboFieldWPF_MouseUp;

          //  this.PropertyChanged += GnosisComboField_PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //private void GnosisComboField_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
        //                cbo.IsReadOnly = locked;
        //            }
        //            break;
        //        case "ReadOnly":
        //            cbo.IsReadOnly = readOnly; ;
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        public void LoadComboOptionImplementations(List<GnosisComboOption> comboOptionImplementations)
        {
            foreach (GnosisComboOption comboOptionImp in comboOptionImplementations)
            {
                cbo.Items.Add(comboOptionImp);
            }

        }


        public void SetOptionChangedHandler(Action<GnosisComboOption> _optionChangedHandler)
        {
            optionChangedHandler = _optionChangedHandler;
            cbo.SelectionChanged += GnosisComboFieldWPF_SelectionChanged;

        }

        private void GnosisComboFieldWPF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GnosisComboOption selectedOption = (GnosisComboOption)cbo.SelectedItem;
            optionChangedHandler.Invoke(selectedOption);
        }

        public double GetWidth()
        {
            return cbo.ActualWidth;
        }

        public double GetHeight()
        {
            return cbo.ActualHeight;
        }

       

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(cbo, visible);
        }

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    cbo.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    cbo.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    cbo.BorderThickness = new System.Windows.Thickness(2);
        //}

        //public void SetCaption(string caption)
        //{

        //}

        //public void SetFont(string font)
        //{
        //    cbo.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    cbo.FontSize = FontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    cbo.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    cbo.MouseEnter += GnosisComboFieldWPF_MouseEnter;
        //}

        private void GnosisComboFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }


        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            cbo.SetHorizontalAlignmentExt(horizontalAlignment);

        }

        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    cbo.SetHorizontalContentAlignmentExt(horizontalAlignment);

        //}

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            cbo.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    cbo.IsEnabled = isEnabled;
        //}

        //public void SetLocked(bool locked)
        //{
        //    cbo.IsEnabled = !locked;
        //    Locked = locked;
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    cbo.MouseLeave += GnosisComboFieldWPF_MouseLeave;
        //}

        private void GnosisComboFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }


        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    cbo.PreviewMouseDown += GnosisComboFieldWPF_MouseDown;
        //}

        private void GnosisComboFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    cbo.PreviewMouseUp += GnosisComboFieldWPF_MouseUp;
        //}

        private void GnosisComboFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    cbo.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    cbo.BorderThickness = new System.Windows.Thickness(2);
        //}

        //public void RemoveOutlineColour()
        //{
        //    cbo.BorderThickness = new System.Windows.Thickness(0);
        //}

        public void SetStrikethrough(bool strikethrough)
        {
            foreach (var item in cbo.Items)
            {
                GnosisComboOption option = item as GnosisComboOption;
                //TextBox txtBox = (TextBox)option.Template.FindName("PART_EditableTextBox", option);

                if (strikethrough)
                {
                    option.TextDecorations = TextDecorations.Strikethrough;
                }
                else
                {
                    option.TextDecorations = null;
                }
            }

        }

        //public void SetTooltip(string tooltip)
        //{
        //    cbo.ToolTip = tooltip;
        //}

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    cbo.SetVerticalContentAlignmentExt(verticalAlignment);

        //}

        //public void SetVisible(bool visible)
        //{
        //    cbo.SetVisibleExt(visible);

        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisComboFieldController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        //public void SetTextLength(int numCharacters)
        //{
        //    cbo.Width = numCharacters * StyleController.GetCharacterWidth(cbo.FontFamily, cbo.FontSize, cbo.FontStyle, cbo.FontWeight, cbo.FontStretch);
        //}

        public void SetSelectedOption(GnosisComboOption selectedOption)
        {
            cbo.SelectedItem = selectedOption;
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            cbo.SetHorizontalPaddingExt(paddingHorizontal);

        }

        public void SetPaddingVertical(double paddingVertical)
        {
            cbo.SetVerticalPaddingExt(paddingVertical);

        }

        //public FontFamily GetFontFamily()
        //{
        //    return cbo.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return cbo.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return cbo.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return cbo.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return cbo.FontStretch;
        //}

        public double GetPaddingHorizontal()
        {
            return cbo.Padding.Left;
        }

        public void SetMinWidth(double minWidth)
        {
            cbo.MinWidth = minWidth;
        }

        public void SetMaxWidth(double maxWidth)
        {
            cbo.MaxWidth = maxWidth;
        }

        public void SetWidth(double width)
        {
            cbo.Width = width;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            cbo.GotFocus += GnosisComboFieldWPF_GotFocus;
        }

        private void GnosisComboFieldWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            cbo.LostFocus += GnosisComboFieldWPF_LostFocus;
        }

        private void GnosisComboFieldWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public GnosisComboOption GetSelectedOption()
        {
            return (GnosisComboOption)cbo.SelectedItem;
        }

        public void SetHeight(double fieldHeight)
        {
            cbo.Height = fieldHeight;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (optional)
            {
                if (e.Key == Key.Back || e.Key == Key.Delete)
                {
                    TextBox txtBox = sender as TextBox;
                    txtBox.Text = string.Empty;
                }
            }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisComboAttribute)
            {
                if (comboAttributes == null)
                {
                    comboAttributes = new List<GnosisComboAttribute>();
                }
                comboAttributes.Add((GnosisComboAttribute)child);
            }
        }
    }

   

}
