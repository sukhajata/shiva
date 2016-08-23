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
using ShivaShared3.Interfaces;
using ShivaWPF3.UtilityWPF;
using ShivaShared3.BaseControllers;
using ShivaShared3.Data;
using System.Windows.Markup;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisGalleryItemWPF.xaml
    /// </summary>
    public partial class GnosisGalleryItem : TreeViewItem, IGnosisGalleryItemImplementation, INotifyPropertyChanged
    {
        //private Action GotMouseFocusHandler;
        //private Action LostMouseFocusHandler;
        //private Action MouseDownHandler;
        //private Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        private Action ItemSelectedHandler;
        private Action ItemUnselectedHandler;

        protected string gnosisIcon;
        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;
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
                //this.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin, horizontalMargin + horizontalSpacing);
                //this.Margin = new Thickness(0, 0, 0, 3);
                //for (int i = 0; i < this.Items.Count; i++)
                //{
                //    ((GnosisGalleryItem)Items[i]).HorizontalSpacing = horizontalSpacing;
                //    //GnosisGalleryItem item = (GnosisGalleryItem)Items[i];
                //    //item.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin, horizontalMargin + horizontalSpacing);
                //}
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
               // this.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin + verticalSpacing, horizontalMargin);

                //for (int i = 0; i < Items.Count; i++)
                //{
                //    ((GnosisGalleryItem)Items[i]).VerticalSpacing = verticalSpacing;
                //    //GnosisGalleryItem item = (GnosisGalleryItem)Items[i];
                //    //item.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin + verticalSpacing, horizontalMargin);
                //}
            }
        }

        [GnosisProperty]
        public string GnosisIcon
        {
            get { return gnosisIcon; }
            set
            {
                gnosisIcon = value;
                this.Header = new Image
                {
                    Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(gnosisIcon, !this.Disabled)))
                };
            }
        }

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
            typeof(int), typeof(GnosisGalleryItem), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisGalleryItem galleryItem = source as GnosisGalleryItem;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double paddingHorizontal;
            double paddingVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease padding
                paddingHorizontal = galleryItem.Padding.Left - newThickness;
                paddingVertical = galleryItem.Padding.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase padding
                paddingHorizontal = galleryItem.Padding.Left + oldThickness;
                paddingVertical = galleryItem.Padding.Top + oldThickness;
            }

            if (paddingHorizontal >= 0 && paddingVertical >= 0)
            {
                galleryItem.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
                galleryItem.BorderThickness = new Thickness(newThickness);
            }

        }

        public GnosisGalleryItem()
        {
            InitializeComponent();

            //galleryDatasetItems = new List<GnosisGalleryDatasetItem>();
            //galleryDocumentItems = new List<GnosisGalleryDocumentItem>();
            //galleryItems = new List<GnosisGalleryItem>();
            //gallerySearchItems = new List<GnosisGallerySearchItem>();

            Binding binding = new Binding("Active");
            binding.Mode = BindingMode.TwoWay;
            binding.Source = this;
            this.SetBinding(TreeViewItem.IsSelectedProperty, binding);

            this.PreviewMouseDown += GnosisGalleryItemWPF_MouseDown;
            this.PreviewMouseUp += GnosisGalleryItemWPF_MouseUp;
            this.MouseEnter += GnosisGalleryItemWPF_MouseEnter;
            this.MouseLeave += GnosisGalleryItemWPF_MouseLeave;

            this.PropertyChanged += GnosisGalleryItemWPF_PropertyChanged;

        }

        private void GnosisGalleryItemWPF_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    this.Header = caption;
                    break;
                case "Disabled":
                    this.Focusable = !disabled;
                    break;
                case "GnosisExpanded":
                    this.IsExpanded = gnosisExpanded;
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        public void SetItemSelectedHandler(Action action)
        {
            ItemSelectedHandler = action;
            this.Selected += GnosisGalleryItemWPF_Selected;
        }

        public void SetItemUnselectedHandler(Action action)
        {
            ItemUnselectedHandler = action;
            this.Unselected += GnosisGalleryItemWPF_Unselected;
        }

        private void GnosisGalleryItemWPF_Unselected(object sender, RoutedEventArgs e)
        {
            ItemUnselectedHandler.Invoke();
            Active = false;
        }

        private void GnosisGalleryItemWPF_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            ItemSelectedHandler.Invoke();
            Active = true;
        }

        public void AddGalleryItem(IGnosisGalleryItemImplementation childImplementation)
        {
            if (horizontalSpacing > 0)
            {
                Label lbl = new Label();
                lbl.Height = horizontalSpacing;
                lbl.Background = Brushes.Green;
                this.Items.Add(lbl);

            }
            this.Items.Add((GnosisGalleryItem)childImplementation);
        }

        //public void SetCaption(string caption)
        //{
        //    this.Header = caption;
        //}
        //public void SetMarginBottom(int marginBottom)
        //{
        //    this.Margin = new Thickness { Bottom = marginBottom };
        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetExpanded(bool expanded)
        //{
        //    this.IsExpanded = expanded;
        //}

        //public void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalContentAlignment)
        //{
        //    this.SetVerticalContentAlignmentExt(verticalContentAlignment);

        //}

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalContentAlignment)
        //{
        //    this.SetHorizontalContentAlignmentExt(horizontalContentAlignment);

        //}

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);

        }

        //public void SetMargin(int left, int top, int right, int bottom)
        //{
        //    this.Margin = new System.Windows.Thickness(left, top, right, bottom);
        //}

        //public void SetMargin(int margin)
        //{
        //    this.Margin = new System.Windows.Thickness(margin);
        //}

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    this.IsEnabled = isEnabled;
        //    Disabled = !isEnabled;
        //}

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);

        //}

        public double GetAvailableWidth()
        {
            throw new NotImplementedException();
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.PreviewMouseDown += GnosisGalleryItemWPF_MouseDown;
        //}

        private void GnosisGalleryItemWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

            

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisGalleryItemWPF_MouseUp;
        //}

        private void GnosisGalleryItemWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisGalleryItemWPF_MouseEnter;
        //}

        private void GnosisGalleryItemWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetBorderColour(string borderColour)
        //{

        //}

        //public void SetFont(string font)
        //{
        //    this.FontFamily = new System.Windows.Media.FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    this.FontSize = fontSize;
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    //this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //    //this.Style.Setters.Add(new Setter(Control.BackgroundProperty, StyleHelper.GetBrushFromHex(backgroundColour)));
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisGalleryItemWPF_MouseLeave;
        //}

        private void GnosisGalleryItemWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetStrikethrough(bool strikethrough)
        //{

        //}

        //public void SetOutlineColour(string outlineColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    this.BorderThickness = new Thickness(2);
        //}

        //public void RemoveOutlineColour()
        //{
        //    this.BorderThickness = new Thickness(0);
        //}


        //public void SetMaxWidth(int maxWidth)
        //{
        //    this.MaxWidth = maxWidth;
        //}

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisGalleryItemController)gnosisLayoutController;
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
        public void SetWidth(double width)
        {
            this.Width = width;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisGalleryItemWPF_GotFocus;
        }

        private void GnosisGalleryItemWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisGalleryItemWPF_LostFocus;
        }

        private void GnosisGalleryItemWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }

        public virtual void GnosisAddChild(IGnosisObject child)
        {

            if (child is GnosisGalleryDocumentItem)
            {
                if (galleryDocumentItems == null)
                {
                    galleryDocumentItems = new List<GnosisGalleryDocumentItem>();
                }

                galleryDocumentItems.Add((GnosisGalleryDocumentItem)child);
                AddGalleryItem((GnosisGalleryDocumentItem)child);
               // this.Items.Add((GnosisGalleryDocumentItem)child);
            }
            else if (child is GnosisGalleryDatasetItem)
            {
                if (galleryDatasetItems == null)
                {
                    galleryDatasetItems = new List<GnosisGalleryDatasetItem>();
                }

                galleryDatasetItems.Add((GnosisGalleryDatasetItem)child);
                // this.Items.Add((GnosisGalleryDatasetItem)child);
            }
            else if (child is GnosisGallerySearchItem)
            {
                if (gallerySearchItems == null)
                {
                    gallerySearchItems = new List<GnosisGallerySearchItem>();
                }
                GnosisGallerySearchItem item = (GnosisGallerySearchItem)child;
                gallerySearchItems.Add(item);
                AddGalleryItem(item);
               // item.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin, horizontalMargin + horizontalSpacing);
               // this.Items.Add((GnosisGallerySearchItem)child);
            }
            else if (child is GnosisGalleryItem)
            {
                if (galleryItems == null)
                {
                    galleryItems = new List<GnosisGalleryItem>();
                }

                galleryItems.Add((GnosisGalleryItem)child);
                AddGalleryItem((GnosisGalleryItem)child);
              //  ((GnosisGalleryItem)child).Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin, horizontalMargin + horizontalSpacing);
               // this.Items.Add((GnosisGalleryItem)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown type added to GnosisGalleryItem: " + child.GetType().ToString(),
                    "GnosisGalleryItem.GnosisAddChild");
            }
        }


    }
}
