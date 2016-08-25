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

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisGalleryWPF.xaml
    /// </summary>
    public partial class GnosisGallery : TreeView, IGnosisGalleryImplementation
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        private int containerVerticalPadding;
        private int containerHorizontalPadding;
        private int horizontalSpacing;
        private int verticalSpacing;

        public int ContainerHorizontalPadding
        {
            get { return containerHorizontalPadding; }
            set
            {
                containerHorizontalPadding = value;
                this.SetHorizontalPaddingExt(containerHorizontalPadding);
            }
        }

        public int ContainerVerticalPadding
        {
            get { return containerVerticalPadding; }
            set
            {
                containerVerticalPadding = value;
                this.SetVerticalPaddingExt(containerVerticalPadding);
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
                //for (int i = 0; i < this.Items.Count; i++) 
                //{
                //    GnosisGalleryItem item = (GnosisGalleryItem)Items[i];
                //    item.Margin = new Thickness(item.Margin.Left, item.Margin.Top, item.Margin.Right, item.Margin.Bottom + horizontalSpacing);
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
                //for (int i = 0; i < Items.Count; i++)
                //{
                //    GnosisGalleryItem item = (GnosisGalleryItem)Items[i];
                //    item.Margin = new Thickness(item.Margin.Left, item.Margin.Top, item.Margin.Right + verticalSpacing, item.Margin.Bottom);
                //}
            }
        }

        public static readonly DependencyProperty GnosisBorderThicknessProperty =
          DependencyProperty.RegisterAttached("GnosisBorderThickness",
          typeof(int), typeof(GnosisGallery), new FrameworkPropertyMetadata(GnosisBorderThicknessPropertyChanged));
        //new FrameworkPropertyMetadata(0,
        //    FrameworkPropertyMetadataOptions.Inherits));

        public static void SetHighlightThickness(UIElement element, int value)
        {
            element.SetValue(GnosisBorderThicknessProperty, value);
        }

        public static int GetHighlightThickness(UIElement element)
        {
            return (int)element.GetValue(GnosisBorderThicknessProperty);
        }

        public static void GnosisBorderThicknessPropertyChanged(object source, DependencyPropertyChangedEventArgs e)
        {
            GnosisGallery gallery = source as GnosisGallery;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double paddingHorizontal;
            double paddingVertical;

            if (newThickness > oldThickness)
            {
                if (gallery.ContainerHorizontalPadding > 0 && gallery.ContainerVerticalPadding > 0)
                {
                    //increase border thickness, decrease padding
                    paddingHorizontal = gallery.ContainerHorizontalPadding - newThickness;
                    paddingVertical = gallery.ContainerVerticalPadding - newThickness;

                    if (paddingHorizontal >= 0 && paddingVertical >= 0)
                    {
                        gallery.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
                        gallery.BorderThickness = new Thickness(newThickness);
                    }
                    else
                    {
                        gallery.Padding = new Thickness(0);
                        gallery.BorderThickness = new Thickness(gallery.ContainerVerticalPadding, gallery.ContainerHorizontalPadding,
                            gallery.ContainerVerticalPadding, gallery.ContainerHorizontalPadding);
                    }
                }

            }
            else
            {
                //decrease border thickness, increase padding
                paddingHorizontal = gallery.Padding.Left + oldThickness;
                paddingVertical = gallery.Padding.Top + oldThickness;

                gallery.Padding = new Thickness(paddingVertical, paddingHorizontal, paddingVertical, paddingHorizontal);
                gallery.BorderThickness = new Thickness(newThickness);
            }



        }

        public GnosisGallery()
        {
            InitializeComponent();

         

            this.MouseDown += GnosisGalleryWPF_MouseDown;
            this.MouseUp += GnosisGalleryWPF_MouseUp;
            this.MouseEnter += GnosisGalleryWPF_MouseEnter;
            this.MouseLeave += GnosisGalleryWPF_MouseLeave;

            this.PropertyChanged += GnosisGallery_PropertyChanged;
        }

        private void GnosisGallery_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        private void AddGalleryItem(GnosisGalleryItem item)
        {
          //  item.Margin = new Thickness(item.Margin.Left, item.Margin.Top, item.Margin.Right, horizontalSpacing);
            this.Items.Add(item);

        }


        //Because of the complex way that margins are applied in TreeViews, the following rules must be used to get the desired effect
        //1.first child has top and bottom margin unless it has no parent, in which case only bottom margin
        //2.middle children have bottom margin only
        //3.last child has no margin
        public void ApplySpacing()
        {
            if (horizontalSpacing > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    GnosisGalleryItem galleryItem = ((GnosisGalleryItem)Items[i]);
                    if (i > 0)
                    {
                        if (i < Items.Count - 1)
                        {
                            galleryItem.Margin = new Thickness(galleryItem.VerticalMargin, galleryItem.HorizontalMargin,
                                galleryItem.VerticalMargin, galleryItem.HorizontalMargin + horizontalSpacing);

                        }
                    }

                    if (galleryItem.Items.Count > 0)
                    {
                        galleryItem.ApplySpacing(horizontalSpacing);
                    }
                }
            }
        }

       

        //private void AddChildren(TreeViewItem parent, TreeViewItem parentCopy)
        //{
        //    foreach (TreeViewItem child in parent.Items)
        //    {
        //        TreeViewItem ti = new TreeViewItem();
        //        ti.Header = child.Header;
        //        if (child.HasItems)
        //        {
        //            AddChildren(child, ti);
        //        }
        //        parentCopy.Items.Add(ti);
        //    }
        //}

        //public void AddRootItem(GnosisGalleryItem galleryItem)
        //{
        //    HierarchicalDataTemplate dataTemplate = new HierarchicalDataTemplate();
        //    Binding bd = new Binding();
        //    bd.Source = galleryItem;
        //    dataTemplate.ItemsSource = bd;
        //}

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }


        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        public double GetHeight()
        {
            return this.ActualHeight;
        }


        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisGalleryWPF_MouseDown;
        //}

        private void GnosisGalleryWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

            

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisGalleryWPF_MouseUp;
        //}

        private void GnosisGalleryWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisGalleryWPF_MouseEnter;
        //}

        private void GnosisGalleryWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;

        }


        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisGalleryWPF_MouseLeave;
        //}

        private void GnosisGalleryWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }


        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.SetHorizontalPaddingExt(paddingHorizontal);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    this.SetVerticalPaddingExt(paddingVertical);
        //}

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisGalleryWPF_GotFocus;
        }

        private void GnosisGalleryWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisGalleryWPF_LostFocus;
        }

        private void GnosisGalleryWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetMarginLeft(int horizontalSpacing)
        //{
        //    this.Margin = new Thickness(horizontalSpacing, 0, 0, 0);
        //}

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisGallerySearchAttribute)
            {
                if (galleryAttributes == null)
                {
                    galleryAttributes = new List<GnosisGallerySearchAttribute>();
                }
                galleryAttributes.Add((GnosisGallerySearchAttribute)child);
            }
            else if (child is GnosisGalleryDocumentItem)
            {
                if (galleryDocumentItems == null)
                {
                    galleryDocumentItems = new List<GnosisGalleryDocumentItem>();
                }
                GnosisGalleryDocumentItem item = (GnosisGalleryDocumentItem)child;
                galleryDocumentItems.Add(item);

                AddGalleryItem(item);
            }
            else if (child is GnosisGalleryDetail)
            {
                if (galleryDetails == null)
                {
                    galleryDetails = new List<GnosisGalleryDetail>();
                }
                galleryDetails.Add((GnosisGalleryDetail)child);
            }
            else if (child is GnosisGallerySearchItem)
            {
                if (gallerySearchItems == null)
                {
                    gallerySearchItems = new List<GnosisGallerySearchItem>();
                }
                gallerySearchItems.Add((GnosisGallerySearchItem)child);
                AddGalleryItem((GnosisGallerySearchItem)child);
            }
            else if (child is GnosisGalleryItem)
            {
                if (galleryItems == null)
                {
                    galleryItems = new List<GnosisGalleryItem>();
                }
                galleryItems.Add((GnosisGalleryItem)child);
                AddGalleryItem((GnosisGalleryItem)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisGallery", child.GetType().Name);
            }
        }

    }
}
