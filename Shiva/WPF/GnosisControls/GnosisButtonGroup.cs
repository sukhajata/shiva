﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Shiva.Shared.Interfaces;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using ShivaWPF3.UtilityWPF;
using System.Windows;
using System.ComponentModel;

namespace GnosisControls
{
    public class GnosisButtonGroup : Border, IGnosisButtonGroupImplementation
    {
        private StackPanel pnlContent;

        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;

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
        private string icon;
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
                    //nPropertyChanged("ContentVerticalAlignment");
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
               // this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
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
               // this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
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

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
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
               // btn.Content = caption;
               // OnPropertyChanged("Caption");
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


        //[GnosisPropertyAttribute]
        //public string MenuTag
        //{
        //    get
        //    {
        //        return Enum.GetName(typeof(GnosisController.MenuTagEnum), menuTag);
        //    }
        //    set
        //    {
        //        try
        //        {
        //            menuTag = (GnosisController.MenuTagEnum)Enum.Parse(typeof(GnosisController.MenuTagEnum), value.ToUpper());
        //        }
        //        catch (Exception ex)
        //        {
        //            GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //        }
        //    }
        //}

        //public GnosisController.MenuTagEnum _MenuTag
        //{
        //    get { return menuTag; }
        //    set { menuTag = value; }
        //}

      

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
                // OnPropertyChanged("Tooltip");
            }
        }

       




        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set
            {
                horizontalPadding = value;
                this.SetHorizontalPaddingExt(horizontalPadding);
                //if (GnosisIcon == null)
                //{
                //    this.SetHorizontalPaddingExt(horizontalPadding);
                //}
            }
        }

        public int VerticalPadding
        {
            get { return verticalPadding; }
            set
            {
                verticalPadding = value;
                this.SetVerticalPaddingExt(verticalPadding);
                //if (GnosisIcon == null)
                //{
                //    this.SetVerticalPaddingExt(verticalPadding);
                //}
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

        public int CurrentThickness
        {
            get
            {
                return (int)this.BorderThickness.Top;
            }
        }

        public static readonly DependencyProperty ControlThicknessProperty =
         DependencyProperty.RegisterAttached("ControlThickness",
            typeof(int), typeof(GnosisButtonGroup), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));


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
            GnosisButtonGroup buttonGroup = source as GnosisButtonGroup;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin, increase height
                marginHorizontal = buttonGroup.Margin.Left - newThickness;
                marginVertical = buttonGroup.Margin.Top - newThickness;
                buttonGroup.Height = buttonGroup.Height + (newThickness - oldThickness);
            }
            else
            {
                //decrease border thickness, increase margin, decrease height
                marginHorizontal = buttonGroup.Margin.Left + oldThickness;
                marginVertical = buttonGroup.Margin.Top + oldThickness;
                buttonGroup.Height = buttonGroup.Height - (oldThickness - newThickness);
            }

            if (marginHorizontal >= 0 && marginVertical >= 0)
            {
                buttonGroup.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                buttonGroup.BorderThickness = new Thickness(newThickness);

                
            }

        }



        public GnosisButtonGroup()
        {
            //InitializeComponent();
            pnlContent = new StackPanel();
            pnlContent.Orientation = Orientation.Horizontal;
            this.Child = pnlContent;

            this.MouseEnter += GnosisButtonWPF_MouseEnter;
            this.MouseLeave += GnosisButtonWPF_MouseLeave;
            this.PreviewMouseDown += GnosisButtonWPF_MouseDown;
            this.PreviewMouseUp += GnosisButtonWPF_MouseUp;

            //  this.PropertyChanged += GnosisButton_PropertyChanged;


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


        //private void GnosisButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
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
        //            this.IsEnabled = !disabled;
        //            if (GnosisIcon != null)
        //            {
        //                btn.Content = new Image
        //                {
        //                    Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, btn.IsEnabled)))
        //                };
        //            }
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



        public double GetAvailableWidth()
        {
            return this.ActualWidth - (horizontalMargin * 2);
        }




        private void GnosisButtonWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
            // string xaml = XamlWriter.Save(this.Style);
        }

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);

        }


        private void GnosisButtonWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            throw new NotImplementedException();
        }


        private void GnosisButtonWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.PreviewMouseUp += GnosisButtonWPF_MouseUp;
        //}

        private void GnosisButtonWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }


        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    if (GnosisIcon == null)
        //    {
        //        btn.SetHorizontalPaddingExt(paddingHorizontal);
        //    }
        //}

        //public double GetPaddingHorizontal()
        //{
        //    return btn.Padding.Left;
        //}

        public void SetWidth(double width)
        {
            this.Width = width;
        }

        public void SetMinWidth(double minWidth)
        {
            this.MinWidth = minWidth;
        }

        public void SetMaxWidth(double maxWidth)
        {
            this.MaxWidth = maxWidth;
        }

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    if (GnosisIcon == null)
        //    {
        //        btn.SetVerticalPaddingExt(paddingVertical);
        //    }
        //}

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisButtonWPF_GotFocus;
        }

        private void GnosisButtonWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisButtonWPF_LostFocus;
        }

        private void GnosisButtonWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetTooltipVisible(bool showTooltips)
        {
            ToolTipService.SetIsEnabled(this, showTooltips);
        }


        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        public void SetStrikethrough(bool strikethrough)
        {

        }
    }
}
