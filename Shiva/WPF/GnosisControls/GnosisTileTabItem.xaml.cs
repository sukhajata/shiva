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
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Markup;
using ShivaShared3.Data;
using System.Windows.Automation.Peers;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTileTabItemWPF.xaml
    /// </summary>
    public partial class GnosisTileTabItem : TabItem, IGnosisTileTabItemImplementation
    {
        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action<double> loadedHandler;
        protected Action closeHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;


        private GnosisTabHeaderButton headerButton;
        private GnosisSearchFrame searchFrame;
        private GnosisFrame frame;

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

        public void LoadFrame(IGnosisFrameImplementation frameImplementation, IGnosisTabHeaderButtonImplementation _headerButton)
        {
            gridContent.Children.Clear();
            if (headerButton != null && pnlHeader.Children.Contains(headerButton))
            {
                pnlHeader.Children.Remove(headerButton);
            }
            headerButton = (GnosisTabHeaderButton)_headerButton;
            headerButton.Click += headerButton_Click;
            Binding binding = new Binding("IsSelected");
            binding.Source = this;
            headerButton.SetBinding(GnosisToggleButton.IsCheckedProperty, binding);
            // headerButton.Margin = new Thickness(0);
            //headerButton.Padding = new Thickness(5);
            //headerButton.BorderThickness = new Thickness(0);
            pnlHeader.Children.Add(headerButton);
            btnClose.Visibility = Visibility.Visible;
          //  btnClose.CopyStyle(headerButton.Style);
            //string xaml = XamlWriter.Save(btnClose.Style);

            if (frameImplementation is GnosisSearchFrame)
            {
                searchFrame = (GnosisSearchFrame)frameImplementation;
                frame = null;
                gridContent.Children.Add(searchFrame);
               // headerButton.Style = searchFrame.Style;
               // pnlHeader.Background = searchFrame.Background;
            }
            else
            {
                frame = (GnosisFrame)frameImplementation;
                searchFrame = null;
                gridContent.Children.Add(frame);
               // headerButton.Style = frame.Style;
            }

            HideLoadingAnimation();
        }

        public void SetMarginBottom(int marginBottom)
        {
            this.Margin = new Thickness { Bottom = marginBottom };
        }


        public void SetCaption(string caption)
        {
            //headerButton.Content = caption;
            this.Header = caption;
        }

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

        public void SetFont(string font)
        {
            this.FontFamily = new FontFamily(font);
        }

        public void SetFontSize(int fontSize)
        {
            this.FontSize = fontSize;
        }

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

        public void SetMargin(int left, int top, int right, int bottom)
        {
            this.Margin = new System.Windows.Thickness(left, top, right, bottom);
        }

        public void SetMargin(int margin)
        {
            this.Margin = new System.Windows.Thickness(margin);
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            ExtensionMethodsWPF.SetHorizontalPaddingExt(this, paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            ExtensionMethodsWPF.SetVerticalPaddingExt(this, paddingVertical);
        }



        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public int GetOrder()
        {
            return order;
        }

        public void SetOrder(int _order)
        {
            order = _order;
        }

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

        public void SetHeaderButton(IGnosisTabHeaderButtonImplementation _headerButton)
        {
            if (headerButton != null && pnlHeader.Children.Contains(headerButton))
            {
                pnlHeader.Children.Remove(headerButton);
            }
            headerButton = (GnosisTabHeaderButton)_headerButton;
            headerButton.Click += headerButton_Click;
            Binding binding = new Binding("IsSelected");
            binding.Source = this;
            binding.Mode = BindingMode.TwoWay;
            headerButton.SetBinding(GnosisToggleButton.IsCheckedProperty, binding);
           // headerButton.Padding = new Thickness(5);
            //headerButton.BorderThickness = new Thickness(0);
            //headerButton.Margin = new Thickness(0);
            pnlHeader.Children.Add(headerButton);

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

        private void headerButton_Click(object sender, RoutedEventArgs e)
        {
            //string xaml = XamlWriter.Save(headerButton.Style);
            ((GnosisTileTab)this.Parent).SelectedItem = this;
            headerButton.RaiseEvent(new RoutedEventArgs(GnosisToggleButton.CheckedEvent));

            HideLoadingAnimation();

            //create dummy content if needed
            if (gridContent.Children.Count == 0)
            {
                headerButton.Content = "          ";
                Grid grid = new Grid();
                grid.CopyStyle(headerButton.Style);
                gridContent.Children.Add(grid);
            }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
