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

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisToolbarButtonWPF.xaml
    /// </summary>
    public partial class GnosisToolbarButton : Button, IGnosisToolbarButtonImplementation
    {
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


        public GnosisToolbarButton(GnosisGenericMenuItem _genericMenuItem)
        {
            genericMenuItem = _genericMenuItem;

            InitializeComponent();

            this.MouseDown += GnosisToolbarButtonWPF_MouseDown;
            this.PreviewMouseUp += GnosisToolbarButtonWPF_MouseUp;
            this.MouseEnter += GnosisToolbarButtonWPF_MouseEnter;
            this.MouseLeave += GnosisToolbarButtonWPF_MouseLeave;

            genericMenuItem.PropertyChanged += GenericMenuItem_PropertyChanged;

            this.PropertyChanged += GnosisToolbarButton_PropertyChanged;
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

        private void GnosisToolbarButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Caption":
                    btn.Content = caption;
                    break;
                case "ContentVerticalAlignment":
                    this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
                    break;
                case "ContentHorizontalAlignment":
                    this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
                    break;
                case "Disabled":
                    btn.IsEnabled = !disabled;
                    btn.Content = new Image
                    {
                        Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, btn.IsEnabled)))
                    };
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "GnosisIcon":
                    btn.Content = new Image
                    {
                        Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(icon, btn.IsEnabled)))
                    };
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
        }

        public void SetCaption(string caption)
        {
            btn.Content = caption;
        }

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
