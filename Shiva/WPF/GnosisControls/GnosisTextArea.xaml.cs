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
using Shiva.Shared.Interfaces;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using Shiva.Shared.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTextAreaWPF.xaml
    /// </summary>
    public partial class GnosisTextArea : Border, IGnosisTextAreaImplementation, INotifyPropertyChanged
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

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
        private bool hasScrollBar;
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

        public GnosisController.VerticalAlignmentType _ContentVerticalAlignment
        {
            get { return contentVerticalAlignment; }
            set
            {
                contentVerticalAlignment = value;
                txt.SetVerticalContentAlignmentExt(contentVerticalAlignment);
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
            set
            {
                contentHorizontalAlignment = value;
                txt.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
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

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
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
                txt.MaxLength = maxChars;
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
                return tooltip;
            }

            set
            {
                tooltip = value;
                this.ToolTip = tooltip;
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
                    txt.IsReadOnly = locked;
                    OnPropertyChanged("Locked");
                }
            }
        }

        [GnosisPropertyAttribute]
        public bool HasScrollBar
        {
            get
            {
                return hasScrollBar;
            }

            set
            {
                hasScrollBar = value;
                if (hasScrollBar)
                {
                    txt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
                else
                {
                    txt.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                }
                //OnPropertyChanged("HasScrollBar");
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
                if (readOnly)
                {
                    locked = true;
                    txt.IsReadOnly = true;
                    OnPropertyChanged("Locked");
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
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        protected Action<string> TextChangedHandler;
       
        private bool userInput = true;
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

        public GnosisTextArea()
        {
            InitializeComponent();

            this.MouseEnter += GnosisTextAreaWPF_MouseEnter;
            this.MouseLeave += GnosisTextAreaWPF_MouseLeave;
            this.PreviewMouseDown += GnosisTextAreaWPF_MouseDown;
            this.PreviewMouseUp += GnosisTextAreaWPF_MouseUp;

          //  this.PropertyChanged += GnosisTextArea_PropertyChanged;

        }

        //private void GnosisTextArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch(e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "ContentVerticalAlignment":
        //            txt.SetVerticalContentAlignmentExt(contentVerticalAlignment);
        //            break;
        //        case "ContentHorizontalAlignment":
        //            txt.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
        //            break;
        //        case "MaxChars":
        //            txt.MaxLength = maxChars;
        //            break;
        //        case "HasScrollBar":
        //            if (hasScrollBar)
        //            {
        //                scrollviewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        //            }
        //            else
        //            {
        //                scrollviewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        //            }
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Locked":
        //            if (!readOnly)
        //            {
        //                txt.IsReadOnly = locked;
        //            }
        //            break;
        //        case "ReadOnly":
        //            txt.IsReadOnly = readOnly;
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        public string GetText()
        {
            return txt.Text;
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        //public void SetMarginLeft(int marginLeft)
        //{
        //    this.Margin = new Thickness(marginLeft, this.Margin.Top, this.Margin.Right, this.Margin.Bottom);
        //}

        public void SetText(string text)
        {
            userInput = false;
            txt.Text = text;
            userInput = true;
        }

        public void SetMaxLines(int maxLines)
        {
            txt.MaxLines = maxLines;
        }

        public int GetLineCount()
        {
            return txt.LineCount;
        }


        //public void SetLocked(bool locked)
        //{
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
        //    txtBox.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    txtBox.FontSize = FontSize;
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    txtBox.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisTextAreaWPF_MouseEnter;
        //}

        private void GnosisTextAreaWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
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
        //    this.MouseLeave += GnosisTextAreaWPF_MouseLeave;
        //}

        private void GnosisTextAreaWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetTextChangedHandler(Action<string> handler)
        {
            TextChangedHandler = handler;
            txt.TextChanged += GnosisTextAreaWPF_TextChanged;
        }

        private void GnosisTextAreaWPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userInput)
            {
                TextChangedHandler.Invoke(txt.Text);
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
        //    this.PreviewMouseDown += GnosisTextAreaWPF_MouseDown;
        //}

        private void GnosisTextAreaWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisTextAreaWPF_MouseUp;
        //}

        private void GnosisTextAreaWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                txt.TextDecorations = System.Windows.TextDecorations.Strikethrough;
            }
            else
            {
                txt.TextDecorations = null;
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


        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    txt.SetHorizontalPaddingExt(paddingHorizontal);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    txt.SetVerticalPaddingExt(paddingVertical);
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

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

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
        //    txt.MaxLength = maxChars;
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

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisTextAreaWPF_GotFocus;
        }

        private void GnosisTextAreaWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            this.HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisTextAreaWPF_LostFocus;
        }

        private void GnosisTextAreaWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetTextWrapping(bool wrap)
        {
            if (wrap)
            {
                txt.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                txt.TextWrapping = TextWrapping.NoWrap;
            }
        }

        public void SetMarginLeft(int marginLeft)
        {
            this.Margin = new Thickness(marginLeft, 0, 0, 0);
        }
    }
}
