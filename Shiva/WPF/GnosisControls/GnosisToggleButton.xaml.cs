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
using Shiva.Shared.Interfaces;
using System.ComponentModel;
using ShivaWPF3.UtilityWPF;
using Shiva.Shared.BaseControllers;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using Shiva.Shared.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisToggleButtonWPF.xaml
    /// </summary>
    public partial class GnosisToggleButton : ToggleButton, IGnosisToggleButtonImplementation, INotifyPropertyChanged
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private bool active;
        private string caption;
        private GnosisController.VerticalAlignmentType contentVerticalAlignment;
        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
        private string controlType;
        private bool datasetCreated;
        private bool datasetUpdated;
        private bool datasetDeleted;
        private string dataset;
        private string datasetItem;
        private bool disabled;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private string groupName;
        private bool hidden;
        private string icon;
        private int id;
        private int minDisplayChars;
        private int maxDisplayChars;
        private int order;
        private int selectedFactor;
        private string shortcut;
        private string tooltip;
        private bool tooltipSelected;

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
               //string xaml = XamlWriter.Save(this.Style);
            }
        }
        public bool HasMouseDown
        {
            get { return hasMouseDown; }
            set
            {
                hasMouseDown = value;
                // string xaml = XamlWriter.Save(this.Style);
                OnPropertyChanged("HasMouseDown");
            }
        }

        [GnosisPropertyAttribute]
        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                // toggleButton.IsChecked = selected;
                OnPropertyChanged("Active");
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
                    //this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
                    //OnPropertyChanged("ContentHorizontalAlignment");
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get { return contentHorizontalAlignment; ; }
            set
            {
                contentHorizontalAlignment = value;
                this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
            }
        }

        [GnosisPropertyAttribute]
        public bool Disabled
        {
            get { return disabled; }
            set
            {
                disabled = value;
                toggleButton.IsEnabled = !disabled;
                if (GnosisIcon != null)
                {
                    this.Content = new Image
                    {
                        Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, toggleButton.IsEnabled)))
                    };
                }
                OnPropertyChanged("Disabled");
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
                if (this.Content == null)
                {
                    this.Content = caption;
                }
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
        public string GnosisIcon
        {
            get { return icon; }
            set
            {
                icon = value;
                this.Content = new Image
                {
                    Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, toggleButton.IsEnabled)))
                };
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
        public int SelectedFactor
        {
            get
            {
                return selectedFactor;
            }

            set
            {
                selectedFactor = value;
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
        public string Shortcut
        {
            get
            {
                return shortcut;
            }

            set
            {
                shortcut = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool TooltipSelected
        {
            get
            {
                return tooltipSelected;
            }

            set
            {
                tooltipSelected = value;
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

        private Action<bool> selectedChangedHandler;
        private Action ClickHandler;
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
                if (GnosisIcon == null)
                {
                    this.SetHorizontalPaddingExt(horizontalPadding);
                }
            }
        }

        public int VerticalPadding
        {
            get { return verticalPadding; }
            set
            {
                verticalPadding = value;
                if (GnosisIcon == null)
                {
                    this.SetVerticalPaddingExt(verticalPadding);
                }
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
            typeof(int), typeof(GnosisToggleButton), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisToggleButton panelField = source as GnosisToggleButton;
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

        public GnosisToggleButton()
        {
            InitializeComponent();

            this.MouseEnter += GnosisToggleButtonWPF_MouseEnter;
            this.MouseLeave += GnosisToggleButtonWPF_MouseLeave;
            this.PreviewMouseDown += GnosisToggleButtonWPF_MouseDown;
            this.MouseUp += GnosisToggleButtonWPF_MouseUp;

           // this.Checked += GnosisToggleButtonWPF_Checked1;
            //Binding binding = new Binding("Active");
            //binding.Source = this;
            //binding.Mode = BindingMode.TwoWay;
            //this.SetBinding(ToggleButton.IsCheckedProperty, binding);

          //  this.PropertyChanged += GnosisToggleButton_PropertyChanged;
        }

        //private void GnosisToggleButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch(e.PropertyName)
        //    {
        //        case "Active":
        //            //string xaml = XamlWriter.Save(this.Style);
        //            //break;
        //            //this.IsChecked = active;
        //            //break;
        //        case "Caption":
        //            if (this.Content == null)
        //            {
        //                this.Content = caption;
        //            }
        //            break;
        //        case "ContentVerticalAlignment":
        //            this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
        //            break;
        //        case "ContentHorizontalAlignment":
        //            this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
        //            break;
        //        case "Disabled":
        //            toggleButton.IsEnabled = !disabled;
        //            if (GnosisIcon != null)
        //            {
        //                this.Content = new Image
        //                {
        //                    Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, toggleButton.IsEnabled)))
        //                };
        //            }
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "GnosisIcon":
        //            //if (icon.Equals("Help"))
        //            //{
        //            //    int i = 1;
        //            //}
        //            this.Content = new Image
        //            {
        //                Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, toggleButton.IsEnabled)))
        //            };
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;

        //    }
        //}

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

        //public void RemoveOutlineColour()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.SetBackgroundColourExt(backgroundColour);
        //}

        //public void SetToggleBinding(object source, string dataMember)
        //{
        //    Binding binding = new Binding(dataMember);
        //    binding.Source = source;
        //    binding.Mode = BindingMode.TwoWay;
        //    this.SetBinding(ToggleButton.IsCheckedProperty, binding);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new System.Windows.Thickness(1);
        //}

        //public void SetController(GnosisVisibleController gnosisVisibleController)
        //{
        //    controller = (GnosisToggleButtonController)gnosisVisibleController;
        //}

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
        //    this.MouseEnter += GnosisToggleButtonWPF_MouseEnter;
        //}

        private void GnosisToggleButtonWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
          // string xaml = XamlWriter.Save(this.Style);
        }

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }

            


        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    this.SetHorizontalContentAlignmentExt(horizontalAlignment);
        //}

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    this.IsEnabled = isEnabled;
        //}

        //public void SetLocked(bool locked)
        //{

        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisToggleButtonWPF_MouseLeave;
        //}

        private void GnosisToggleButtonWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }


        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.PreviewMouseDown += GnosisToggleButtonWPF_MouseDown;
        //}

        private void GnosisToggleButtonWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
            Active = !Active;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisToggleButtonWPF_MouseUp;
        //}

        private void GnosisToggleButtonWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        ////public void SetOutlineColour(string outlineColour)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.SetHorizontalPaddingExt(paddingHorizontal);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    this.SetVerticalPaddingExt(paddingVertical);
        //}

        //public void SetTooltip(string toolTip)
        //{
        //    this.ToolTip = toolTip;
        //}

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        //{
        //    this.SetVerticalContentAlignmentExt(verticalAlignment);
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
        //}

        public void SetWidth(double width)
        {
            this.Width = width;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisToggleButtonWPF_GotFocus;
        }

        private void GnosisToggleButtonWPF_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisToggleButtonWPF_LostFocus;
        }

        private void GnosisToggleButtonWPF_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetClickHandler(Action handler)
        {
            ClickHandler = handler;
            toggleButton.Click += ToggleButton_Click;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ClickHandler.Invoke();
        }

        //public void SetSelectedChangedHandler(Action<bool> handler)
        //{
        //    selectedChangedHandler = handler;
        //    toggleButton.Checked += GnosisToggleButtonWPF_Checked;
        //    toggleButton.Unchecked += GnosisToggleButtonWPF_Unchecked;
        //   toggleButton.Click += ToggleButton_Click;
        //}

        //private void ToggleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    selectedChangedHandler.Invoke((bool)toggleButton.IsChecked);
        //}

        //private void GnosisToggleButtonWPF_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    selectedChangedHandler.Invoke(false);
        //    Active = false;
        //}

        //private void GnosisToggleButtonWPF_Checked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    selectedChangedHandler.Invoke(true);
        //    Active = true;
        //}


        public void SetTooltipVisible(bool showTooltips)
        {
            ToolTipService.SetIsEnabled(this, showTooltips);
        }

        //public void SetCaption(string caption)
        //{
        //    this.Content = caption;
        //}

        //public void SetIcon(string icon, bool enabled)
        //{
        //    this.Content = new Image
        //    {
        //        Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, enabled)))
        //    };
        //}

        //public void SetSelected(bool selected)
        //{
        //    toggleButton.IsChecked = selected;
        //}

        //public bool GetSelected()
        //{
        //    return (bool)toggleButton.IsChecked;
        //}

        public void SetStrikethrough(bool strikethrough)
        {

        }


    }
}
