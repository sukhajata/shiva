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
    /// Interaction logic for GnosisGalleryItemWPF.xaml
    /// </summary>
    public partial class GnosisGalleryItem : UserControl, IGnosisGalleryItemImplementation, INotifyPropertyChanged
    {
        private List<GnosisGalleryDatasetItem> galleryDatasetItems;
        private List<GnosisGalleryItem> galleryItems;
        private List<GnosisGalleryDocumentItem> galleryDocumentItems;
        private List<GnosisGallerySearchItem> gallerySearchItems;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private bool active;
        private string caption;
        private bool disabled;
        private bool gnosisExpanded;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private string controlType;
        private bool hidden;
        private int id;
        private int order;
        private int selectedFactor;
        private string tooltip;

        //private Action GotMouseFocusHandler;
        //private Action LostMouseFocusHandler;
        //private Action MouseDownHandler;
        //private Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        private Action ItemSelectedHandler;
        private Action ItemUnselectedHandler;

        protected string gnosisIcon;
        protected int iconSize;
        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;
        private int horizontalSpacing;
        private int verticalSpacing;

        [GnosisPropertyAttribute]
        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                OnPropertyChanged("Active");
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
        public string Caption
        {
            get
            {
                return caption;
            }

            set
            {
                caption = value;
                btnCaption.Content = caption;
                //OnPropertyChanged("Caption");
            }
        }

        [GnosisPropertyAttribute]
        public bool GnosisExpanded
        {
            get { return gnosisExpanded; }
            set
            {
                gnosisExpanded = value;
                if (gnosisExpanded)
                {
                    pnlItems.Visibility = Visibility.Visible;
                }
                else
                {
                    pnlItems.Visibility = Visibility.Collapsed;
                }
                //OnPropertyChanged("GnosisExpanded");
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

        [GnosisProperty]
        public int SelectedFactor
        {
            get { return selectedFactor; }
            set { selectedFactor = value; }
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
        public bool Disabled
        {
            get { return disabled; }
            set
            {
                disabled = value;
                btnCaption.IsEnabled = !disabled;
                if (gnosisIcon != null && iconSize > 0)
                {
                    BitmapImage bi = StyleHelper.GetIcon(gnosisIcon, iconSize, disabled);
                    btnCaption.Content = new Image { Source = bi };
                }
                OnPropertyChanged("Disabled");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        [GnosisCollection]
        public List<GnosisGalleryDatasetItem> GalleryDatasetItems
        {
            get { return galleryDatasetItems; }
            set { galleryDatasetItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGalleryDocumentItem> GalleryDocumentItems
        {
            get { return galleryDocumentItems; }
            set { galleryDocumentItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGalleryItem> GalleryItems
        {
            get { return galleryItems; }
            set { galleryItems = value; }
        }

        [GnosisCollection]
        public List<GnosisGallerySearchItem> GallerySearchItems
        {
            get { return gallerySearchItems; }
            set { gallerySearchItems = value; }
        }




        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                if (hasFocus)
                {
                    Active = true;
                }
                else
                {
                    Active = false;
                }
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
                // this.Background = StyleHelper.GetBrushFromHex("CC00CC");
               // string xaml = XamlWriter.Save(this.Style);
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

        public int HorizontalSpacing
        {
            get
            {
                return horizontalSpacing;
            }

            set
            {
                horizontalSpacing = value;
                this.Margin = new Thickness(horizontalSpacing, 0, 0, 0);
                //this.Margin = new Thickness(horizontalMargin + horizontalSpacing, verticalMargin, horizontalMargin, verticalMargin);
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
               // this.Margin = new Thickness(horizontalMargin, verticalMargin + verticalSpacing, horizontalMargin, verticalMargin);

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
                    //btnCaption.Content = new Image
                    //{
                    //    Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(gnosisIcon, !this.disabled)))
                    //};
                    //System.Drawing.Image icon = System.Drawing.Image.FromFile(GnosisIOHelperWPF.GetIconPath(gnosisIcon, !disabled));
                    //System.Drawing.Image resizedIcon = StyleHelper.ResizeImage(icon, iconSize, iconSize);
                    BitmapImage bi = StyleHelper.GetIcon(gnosisIcon, iconSize, disabled);

                    btnCaption.Content = new Image { Source = bi };
                }
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
                    btnCaption.SetHorizontalPaddingExt(horizontalPadding);
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
                    btnCaption.SetVerticalPaddingExt(verticalPadding);
                }
            }
        }

        public int HorizontalMargin
        {
            get { return horizontalMargin; }
            set
            {
                horizontalMargin = value;
               // this.SetHorizontalMarginExt(horizontalMargin);
            }
        }

        public int VerticalMargin
        {
            get { return verticalMargin; }
            set
            {
                verticalMargin = value;
               // this.SetVerticalMarginExt(verticalMargin);
            }
        }

        public static readonly DependencyProperty ItemBackgroundColourProperty =
            DependencyProperty.RegisterAttached("ItemBackgroundColour", typeof(Brush), typeof(GnosisGalleryItem),
                new FrameworkPropertyMetadata(ItemBackgroundColourPropertyChanged));


        public static void ItemBackgroundColourPropertyChanged(object source, DependencyPropertyChangedEventArgs e)
        {
            GnosisGalleryItem galleryItem = source as GnosisGalleryItem;
            //string backgroundColour = (string)e.NewValue;

            galleryItem.btnCaption.Background = (Brush)e.NewValue;// StyleHelper.GetBrushFromHex(backgroundColour);
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

            if (newThickness > galleryItem.btnCaption.BorderThickness.Left)
            {
                //increase border thickness, decrease padding
                paddingHorizontal = galleryItem.HorizontalPadding - newThickness;
                paddingVertical = galleryItem.VerticalPadding - newThickness;
            }
            else
            {
                //decrease border thickness, increase padding
                paddingHorizontal = galleryItem.HorizontalPadding + oldThickness;
                paddingVertical = galleryItem.VerticalPadding + oldThickness;
            }

            if (paddingHorizontal >= 0 && paddingVertical >= 0)
            {
                galleryItem.btnCaption.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
                galleryItem.btnCaption.BorderThickness = new Thickness(newThickness);
            }

        }

        public GnosisGalleryItem()
        {
            InitializeComponent();

            //galleryDatasetItems = new List<GnosisGalleryDatasetItem>();
            //galleryDocumentItems = new List<GnosisGalleryDocumentItem>();
            //galleryItems = new List<GnosisGalleryItem>();
            //gallerySearchItems = new List<GnosisGallerySearchItem>();

            //Binding binding = new Binding("Active");
            //binding.Mode = BindingMode.TwoWay;
            //binding.Source = this;
            //this.SetBinding(TreeViewItem.IsSelectedProperty, binding);

            this.PreviewMouseDown += GnosisGalleryItemWPF_MouseDown;
            this.PreviewMouseUp += GnosisGalleryItemWPF_MouseUp;
            this.MouseEnter += GnosisGalleryItemWPF_MouseEnter;
            this.MouseLeave += GnosisGalleryItemWPF_MouseLeave;

           // this.PropertyChanged += GnosisGalleryItemWPF_PropertyChanged;

        }

        //private void GnosisGalleryItemWPF_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            this.Header = caption;
        //            break;
        //        case "Disabled":
        //            this.Focusable = !disabled;
        //            break;
        //        case "GnosisExpanded":
        //            this.IsExpanded = gnosisExpanded;
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        public void SetItemSelectedHandler(Action action)
        {
            ItemSelectedHandler = action;
            btnCaption.Checked += GnosisGalleryItemWPF_Selected;
        }

        public void SetItemUnselectedHandler(Action action)
        {
            ItemUnselectedHandler = action;
            btnCaption.Unchecked += GnosisGalleryItemWPF_Unselected;
        }

        private void GnosisGalleryItemWPF_Unselected(object sender, RoutedEventArgs e)
        {
            ItemUnselectedHandler.Invoke();
           // Active = false;
        }

        private void GnosisGalleryItemWPF_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            ItemSelectedHandler.Invoke();
           // Active = true;
        }

        public void AddGalleryItem(GnosisGalleryItem child)
        {
            //if (horizontalSpacing > 0)
            //{
            //    if (Items.Count == 0)
            //    {
            //        child.Margin = new Thickness()
            //    }
            //    child.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin, horizontalMargin + horizontalSpacing);

            //}
           // child.PropertyChanged += Child_PropertyChanged; 
            pnlItems.Children.Add(child);
            toggle.Visibility = Visibility.Visible;
        }

        //private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("Active") && ((GnosisGalleryItem)sender).Active)
        //    {
        //        foreach (GnosisGalleryItem child in pnlItems.Children)
        //        {
        //            if (child != sender)
        //            {
        //                if (child.Active)
        //                {
        //                    child.Active = false;
        //                }
        //            }

        //        }
        //    }
        //}


        //Because of the complex way that margins are applied in TreeViews, the following rules must be applied to get the desired effect
        //1.first child has top and bottom spacing
        //2.middle children have bottom spacing only
        //3.last child has no vertical spacing
        //public void ApplySpacing()
        //{
        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        GnosisGalleryItem galleryItem = ((GnosisGalleryItem)Items[i]);
        //        if (i == 0)
        //        {
        //            galleryItem.Margin = new Thickness(galleryItem.HorizontalMargin + horizontalSpacing, 
        //                galleryItem.VerticalMargin + verticalSpacing, galleryItem.HorizontalMargin, galleryItem.VerticalMargin + verticalSpacing);
        //        }
        //        if (i > 0)
        //        {
        //            if (i < Items.Count - 1)
        //            {
        //                galleryItem.Margin = new Thickness(galleryItem.HorizontalMargin + horizontalSpacing, galleryItem.VerticalMargin,
        //                    galleryItem.HorizontalMargin, galleryItem.VerticalMargin + verticalSpacing);

        //            }
        //            else
        //            {
        //                galleryItem.Margin = new Thickness(galleryItem.HorizontalMargin + horizontalSpacing, galleryItem.VerticalMargin,
        //                   galleryItem.HorizontalMargin, galleryItem.VerticalMargin);

        //            }
        //        }

        //        if (galleryItem.Items.Count > 0)
        //        {
        //            galleryItem.ApplySpacing();
        //        }
        //    }
        //}

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
            btnCaption.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            btnCaption.SetVerticalPaddingExt(paddingVertical);
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

        private void toggle_Click(object sender, RoutedEventArgs e)
        {
            if (pnlItems.Visibility == Visibility.Collapsed)
            {
                pnlItems.Visibility = Visibility.Visible;
            }
            else
            {
                pnlItems.Visibility = Visibility.Collapsed;
            }
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
