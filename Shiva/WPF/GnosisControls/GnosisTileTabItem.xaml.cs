﻿using System;
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
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Markup;
using Shiva.Shared.Data;
using System.Windows.Automation.Peers;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTileTabItemWPF.xaml
    /// </summary>
    public partial class GnosisTileTabItem : TabItem, IGnosisTileTabItemImplementation, INotifyPropertyChanged
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int order;
        private string tooltip;

        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action<double> loadedHandler;
        protected Action closeHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        private GnosisToggleButton headerButton;
       // private GnosisSearchFrame searchFrame;
        //private GnosisFrame frame;

        public GnosisToggleButton HeaderButton
        {
            get { return headerButton; }
            set { headerButton = value; }
        }

        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
               // headerButton.Active = true;
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



        [GnosisProperty]
        public string Caption
        {
            get
            {
                return caption;
            }

            set
            {
                caption = value;
                this.Header = caption;
                OnPropertyChanged("Caption");
            }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisProperty]
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

        [GnosisProperty]
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

        [GnosisProperty]
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

        [GnosisProperty]
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
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

        [GnosisProperty]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

      
        //public bool HasFocus
        //{
        //    get { return hasFocus; }
        //    set
        //    {
        //        hasFocus = value;
        //        //if (hasFocus)
        //        //{
        //        //    if (searchFrame != null)
        //        //    {
        //        //        pnlHeader.Background = searchFrame.BorderBrush;
        //        //    }
        //        //    else if (frame != null)
        //        //    {
        //        //        pnlHeader.Background = frame.BorderBrush;
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    if (searchFrame != null)
        //        //    {
        //        //        pnlHeader.Background = searchFrame.Background;
        //        //    }
        //        //    else if (frame != null)
        //        //    {
        //        //        pnlHeader.Background = frame.Background;
        //        //    }
        //        //}
        //        OnPropertyChanged("HasFocus");
        //    }
        //}
        //public bool HasMouseFocus
        //{
        //    get { return hasMouseFocus; }
        //    set
        //    {
        //        hasMouseFocus = value;
        //        OnPropertyChanged("HasMouseFocus");
        //        //string xaml = XamlWriter.Save(this);
        //        //GnosisIOHelperWPF.WriteXamlToFile(xaml);
        //    }
        //}
        //public bool HasMouseDown
        //{
        //    get { return hasMouseDown; }
        //    set
        //    {
        //        hasMouseDown = value;
        //        OnPropertyChanged("HasMouseDown");
        //    }
        //}

       

       
       

        public GnosisTileTabItem()
        {
            InitializeComponent();

            this.MouseDown += GnosisTabItemWPF_MouseDown;
            this.MouseUp += GnosisTabItemWPF_MouseUp;
            this.MouseEnter += GnosisTabItemWPF_MouseEnter;
            this.MouseLeave += GnosisTabItemWPF_MouseLeave;

            
        }

      

        public void LoadFrame(IGnosisFrameImplementation frameImplementation, IGnosisToggleButtonImplementation _headerButton)
        {
           // gridContent.Children.Clear();
            //if (headerButton != null && pnlHeader.Children.Contains(headerButton))
            //{
            //    pnlHeader.Children.Remove(headerButton);
            //}
            headerButton = (GnosisToggleButton)_headerButton;
            headerButton.GotFocus += headerButton_GotFocus;
            //Binding binding = new Binding("IsSelected");
            //binding.Source = this;
            //headerButton.SetBinding(GnosisToggleButton.IsCheckedProperty, binding);
            // headerButton.Margin = new Thickness(0);
            //headerButton.Padding = new Thickness(5);
            //headerButton.BorderThickness = new Thickness(0);
            this.Header = headerButton;
           // btnClose.Visibility = Visibility.Visible;
          //  btnClose.CopyStyle(headerButton.Style);
            //string xaml = XamlWriter.Save(btnClose.Style);

            if (frameImplementation is GnosisSearchFrame)
            {
                gridContent.Children.Add((GnosisSearchFrame)frameImplementation);
            }
            else
            {
                gridContent.Children.Add((GnosisFrame)frameImplementation);
            }

            HideLoadingAnimation();
        }

        public void SetMarginBottom(int marginBottom)
        {
            this.Margin = new Thickness { Bottom = marginBottom };
        }


        //public void SetCaption(string caption)
        //{
        //    //headerButton.Content = caption;
        //    this.Header = caption;
        //}

        public void SetMaxPrintWidth(int maxPrintWidth)
        {
            this.SetMaxPrintWidth(maxPrintWidth);
        }

        public void SetMaxWidth(int maxWidth)
        {
            this.MaxWidth = MaxWidth;
        }

        public void SetMinWidth(int minWidth)
        {
            this.MinWidth = minWidth;
        }

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);

        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }


        public void RemoveFrame(IGnosisFrameImplementation gnosisControlImplementation)
        {
            throw new NotImplementedException();
        }

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisTabItemWPF_MouseDown;
        //}

        private void GnosisTabItemWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisTabItemWPF_MouseUp;
        //}

        private void GnosisTabItemWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }
            


        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisTabItemWPF_MouseEnter;
        //}

        private void GnosisTabItemWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //}

        //public void SetFont(string font)
        //{
        //    this.FontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    this.FontSize = fontSize;
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisTabItemWPF_MouseLeave;
        //}

        private void GnosisTabItemWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        public void SetStrikethrough()
        {
            throw new NotImplementedException();
        }

        //public void SetOutlineColour(string outlineColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(outlineColour);
        //    this.BorderThickness = new Thickness(2);
        //}

        //public void RemoveOutlineColour()
        //{
        //    this.BorderThickness = new Thickness(0);
        //}

        //public void SetMargin(int left, int top, int right, int bottom)
        //{
        //    this.Margin = new System.Windows.Thickness(left, top, right, bottom);
        //}

        //public void SetMargin(int margin)
        //{
        //    this.Margin = new System.Windows.Thickness(margin);
        //}

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    ExtensionMethodsWPF.SetHorizontalPaddingExt(this, paddingHorizontal);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    ExtensionMethodsWPF.SetVerticalPaddingExt(this, paddingVertical);
        //}



        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

        //public int GetOrder()
        //{
        //    return order;
        //}

        //public void SetOrder(int _order)
        //{
        //    order = _order;
        //}

        public void Highlight()
        {
            throw new NotImplementedException();
        }

        public void UnHighlight()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            gridContent.Children.Clear();
            Header = null;
            //if (headerButton != null && pnlHeader.Children.Contains(headerButton))
            //{
            //    pnlHeader.Children.Remove(headerButton);
            //}
            //pnlHeader.Visibility = Visibility.Collapsed;
        }

        public void SetLoadedHandler(Action<double> action)
        {
            loadedHandler = action;
            this.Loaded += GnosisTabItemWPF_Loaded;
        }

        private void GnosisTabItemWPF_Loaded(object sender, RoutedEventArgs e)
        {
            double width = this.ActualWidth;
            loadedHandler.Invoke(width);
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisTabItemWPF_GotFocus;
        }

        private void GnosisTabItemWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
            ((GnosisTileTab)this.Parent).SelectTabItem(this);
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisTabItemWPF_LostFocus;
        }

        private void GnosisTabItemWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetHeaderButton(IGnosisToggleButtonImplementation _headerButton)
        {
            //if (headerButton != null && pnlHeader.Children.Contains(headerButton))
            //{
            //    pnlHeader.Children.Remove(headerButton);
            //}
            headerButton = (GnosisToggleButton)_headerButton;
            headerButton.GotFocus += headerButton_GotFocus;
            //Binding binding = new Binding("IsSelected");
            //binding.Source = this;
            //binding.Mode = BindingMode.TwoWay;
            //headerButton.SetBinding(GnosisToggleButton.IsCheckedProperty, binding);
            // headerButton.Padding = new Thickness(5);
            //headerButton.BorderThickness = new Thickness(0);
            //headerButton.Margin = new Thickness(0);
            //pnlHeader.Children.Add(headerButton);
            this.Header = headerButton;
            //Canvas.SetZIndex(headerButton, 99);
            ////headerButton.IsHitTestVisible = true;
            ////headerButton.Background = Brushes.Blue;
            //btnClose.Style = headerButton.Style;
            //borderHeader.Visibility = Visibility.Visible;
            //string xaml = XamlWriter.Save(headerButton.Style);
            //DockPanel.SetDock(headerButton, Dock.Left);
            //pnlHeader.Children.Add(headerButton);
        }

        public void DisplayLoadingAnimation(string barColour)
        {
            viewLoading.Visibility = Visibility.Visible;           

        }

        public void HideLoadingAnimation()
        {
            viewLoading.Visibility = Visibility.Collapsed;
        }

        public void SetCloseHandler(Action action)
        {
            closeHandler = action;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            closeHandler.Invoke();
        }

        private void headerButton_GotFocus(object sender, RoutedEventArgs e)
        {
            //string xaml = XamlWriter.Save(headerButton.Style);
            ((GnosisTileTab)this.Parent).SelectTabItem( this);
            // headerButton.RaiseEvent(new RoutedEventArgs(GnosisToggleButton.CheckedEvent));

            //HideLoadingAnimation();


            //create dummy content if needed
            if (gridContent.Children.Count == 0)
            {
                headerButton.Caption = "          ";
                Grid grid = new Grid();
                grid.Background = headerButton.Background;
                grid.VerticalAlignment = VerticalAlignment.Stretch;
                grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                gridContent.Children.Add(grid);
            }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
