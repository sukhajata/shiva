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
using System.Windows.Markup;
using Shiva.Shared.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisToolbarButtonWPF.xaml
    /// </summary>
    public partial class GnosisToolbarButton : Button, IGnosisToolbarButtonImplementation, INotifyPropertyChanged
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private bool disabled;
        private GnosisController.VerticalAlignmentType contentVerticalAlignment;
        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
        private int colSpan;
        private string controlType;
        private bool datasetCreated;
        private bool datasetUpdated;
        private bool datasetDeleted;
        private string dataset;
        private string datasetItem;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private string gnosisIcon;
        private int iconSize;
        private int id;
        private int maxChars;
        private GnosisController.MenuTagEnum menuTag;
        private int minDisplayChars;
        private int maxDisplayChars;
        private int order;
        private string shortcut;
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
        public bool Disabled
        {
            get { return disabled; }
            set
            {
                disabled = value;
                btn.IsEnabled = !disabled;
                if (gnosisIcon != null && IconSize  > 0)
                {
                    BitmapImage bi = StyleHelper.GetIcon(gnosisIcon, iconSize, disabled);
                    btn.Content = new Image { Source = bi };
                }
                OnPropertyChanged("Disabled");
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
            get { return contentHorizontalAlignment; }
            set
            {
                contentHorizontalAlignment = value;
                this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
            }
        }

        [GnosisPropertyAttribute]
        public int ColSpan
        {
            get
            {
                return colSpan;
            }

            set
            {
                colSpan = value;
                OnPropertyChanged("ColSpan");
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
                btn.Content = caption;
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
        public string MenuTag
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.MenuTagEnum), menuTag);
            }
            set
            {
                try
                {
                    menuTag = (GnosisController.MenuTagEnum)Enum.Parse(typeof(GnosisController.MenuTagEnum), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.MenuTagEnum _MenuTag
        {
            get { return menuTag; }
            set { menuTag = value; }
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
        public string GnosisIcon
        {
            get
            {
                return gnosisIcon;
            }

            set
            {
                gnosisIcon = value;
               
            }
        }

        public int IconSize
        {
            get { return iconSize; }
            set
            {
                iconSize = value;
                if (gnosisIcon != null)
                {
                    BitmapImage bi = StyleHelper.GetIcon(gnosisIcon, iconSize, disabled);

                    btn.Content = new Image { Source = bi } ;
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


        protected GnosisGenericMenuItem genericMenuItem;

        protected Action clickHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
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
           typeof(int), typeof(GnosisToolbarButton), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));


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
            GnosisToolbarButton button = source as GnosisToolbarButton;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin, increase height
                marginHorizontal = button.Margin.Left - newThickness;
                marginVertical = button.Margin.Top - newThickness;
                button.Height = button.Height + (newThickness - oldThickness);
            }
            else
            {
                //decrease border thickness, increase margin, decrease height
                marginHorizontal = button.Margin.Left + oldThickness;
                marginVertical = button.Margin.Top + oldThickness;
                button.Height = button.Height - (oldThickness - newThickness);
            }

            button.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
            button.BorderThickness = new Thickness(newThickness);

        }

        public GnosisToolbarButton(GnosisGenericMenuItem _genericMenuItem)
        {
            genericMenuItem = _genericMenuItem;

            InitializeComponent();

            this.MouseDown += GnosisToolbarButtonWPF_MouseDown;
            this.PreviewMouseUp += GnosisToolbarButtonWPF_MouseUp;
            this.MouseEnter += GnosisToolbarButtonWPF_MouseEnter;
            this.MouseLeave += GnosisToolbarButtonWPF_MouseLeave;

            genericMenuItem.PropertyChanged += GenericMenuItem_PropertyChanged;

         //   this.PropertyChanged += GnosisToolbarButton_PropertyChanged;
        }

        private void GenericMenuItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Hidden":
                    this.Hidden = genericMenuItem.Hidden;
                    break;
                case "Disabled":
                    this.Disabled = genericMenuItem.Disabled;
                    break;
            }
        }

        //private void GnosisToolbarButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch(e.PropertyName)
        //    {
        //        case "Caption":
        //            btn.Content = caption;
        //            break;
        //        case "ContentVerticalAlignment":
        //            this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
        //            break;
        //        case "ContentHorizontalAlignment":
        //            this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
        //            break;
        //        case "Disabled":
        //            btn.IsEnabled = !disabled;
        //            btn.Content = new Image
        //            {
        //                Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, btn.IsEnabled)))
        //            };
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "GnosisIcon":
        //            btn.Content = new Image
        //            {
        //                Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, btn.IsEnabled)))
        //            };
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        //public void SetCaption(string caption)
        //{
        //    btn.Content = caption;
        //}

        //public void SetClickHandler(Action _clickHandler)
        //{
        //    clickHandler = _clickHandler;
        //    btn.Click += GnosisToolbarButtonWPF_Click;
        //}

        //private void GnosisToolbarButtonWPF_Click(object sender, RoutedEventArgs e)
        //{
        //    clickHandler.Invoke();
        //}

        //public void SetDepressed(bool depressed)
        //{

        //}

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }


        //public void SetEnabled(bool enabled)
        //{
        //    btn.IsEnabled = enabled;
        //}

        public void SetIcon(string iconName, bool enabled)
        {
            btn.Content = new Image
            {
                Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(iconName, enabled)))
            };

        }

        public void SetTooltip(string tooltip)
        {
            this.ToolTip = tooltip;
        }

        public void SetVisible(bool visible)
        {
            this.SetVisibleExt(visible);

        }


        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public double GetPaddingHorizontal()
        {
            return btn.Padding.Left;
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            btn.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            btn.SetVerticalPaddingExt(paddingVertical);
        }

        //public void SetBorderColour(string borderColour)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetOutlineColour(string outlineColour)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveOutlineColour()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisToolbarButtonWPF_MouseDown;
        //}

        private void GnosisToolbarButtonWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisToolbarButtonWPF_MouseUp;
        //}
            

        private void GnosisToolbarButtonWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisToolbarButtonWPF_MouseEnter;
        //}

        private void GnosisToolbarButtonWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
            //string xaml = XamlWriter.Save(this.Style);
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisToolbarButtonWPF_MouseLeave;
        //}

        private void GnosisToolbarButtonWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;

        }

        //public void SetController(GnosisVisibleController gnosisVisibleController)
        //{
        //    throw new NotImplementedException();
        //}

        public void SetFont(string font)
        {
            btn.FontFamily = new FontFamily(font);
        }

        public void SetFontSize(int fontSize)
        {
            btn.FontSize = fontSize;
        }

        public void SetForegroundColour(string contentColour)
        {
            btn.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisToolbarButtonWPF_GotFocus;
        }

        private void GnosisToolbarButtonWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;

        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisToolbarButtonWPF_LostFocus;
        }

        private void GnosisToolbarButtonWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            btn.SetVerticalContentAlignmentExt(verticalAlignment);
        }

        public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            btn.SetHorizontalContentAlignmentExt(horizontalAlignment);
        }

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
        //    throw new NotImplementedException();
        //}

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);
        }

        public void SetWidth(double width)
        {
            this.Width = width;
        }

        public void SetStrikethrough(bool strikethrough)
        {

        }


    }
}
