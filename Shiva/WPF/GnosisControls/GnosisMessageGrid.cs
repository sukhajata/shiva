using Shiva.Shared.BaseControllers;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using Shiva.Shared.Data;

namespace GnosisControls
{
    public partial class GnosisMessageGrid : Border, IGnosisMessageGridImplementation, INotifyPropertyChanged
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private GnosisController.HorizontalAlignmentType captionHorizontalAlignment;
        private GnosisController.VerticalAlignmentType captionVerticalAlignment;
        private GnosisController.CaptionPosition captionRelativePosition;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int maxDisplayRows;
        private int maxLines;
        private int maxSectionSpan;
        private int minDisplayRows;
        private int order;
        private int maxWrapRows;
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
            }
        }

        [GnosisPropertyAttribute]
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



        public bool GridIsLoaded
        {
            get
            {
                return this.IsLoaded;
            }
        }

        public bool GridIsVisible
        {
            get
            {
                return this.IsVisible;
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionRelativePosition
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.CaptionPosition), captionRelativePosition);
            }

            set
            {
                try
                {
                    captionRelativePosition = (GnosisController.CaptionPosition)Enum.Parse(typeof(GnosisController.CaptionPosition), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionAlignmentHorizontal
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), captionHorizontalAlignment);
            }

            set
            {
                try
                {
                    captionHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionAlignmentVertical
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), captionVerticalAlignment);
            }

            set
            {
                try
                {
                    captionVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public int MinDisplayRows
        {
            get
            {
                return minDisplayRows;
            }

            set
            {
                minDisplayRows = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxDisplayRows
        {
            get
            {
                return maxDisplayRows;
            }

            set
            {
                maxDisplayRows = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxLines
        {
            get
            {
                return maxLines;
            }

            set
            {
                maxLines = value;
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
        public int MaxWrapRows
        {
            get
            {
                return maxWrapRows;
            }

            set
            {
                maxWrapRows = value;
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

        private Grid gridContent;
        //private FontFamily fontFamily;
        //private double fontSize;
        //private Brush fontColour;

      

        public GnosisMessageGrid() : base()
        {
            gridContent = new Grid();
            this.Child = gridContent;

            this.MouseDown += GnosisMessageGridWPF_MouseDown;

            this.MouseUp += GnosisMessageGridWPF_MouseUp;
            this.MouseEnter += GnosisMessageGridWPF_MouseEnter;
            this.MouseLeave += GnosisMessageGridWPF_MouseLeave;

           // this.PropertyChanged += GnosisMessageGrid_PropertyChanged;
        }

        //private void GnosisMessageGrid_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "CaptionVerticalAlignment":
        //            break;
        //        case "CaptionHorizontalAlignment":
        //            break;
        //        case "CaptionRelativePostion":
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}


        //public void SetCaption(string caption)
        //{

        //}

        //public void SetMarginBottom(int marginBottom)
        //{
        //    this.Margin = new Thickness { Bottom = marginBottom };
        //}

        public void SetMaxWidth(int maxWidth)
        {
            this.MaxWidth = MaxWidth;
        }

        public void SetMinWidth(int minWidth)
        {
            this.MinWidth = minWidth;
        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
            
        //}

        //public void SetMessage(string message)
        //{
        //    //content.Content = message;
        //}

        public double GetHeight()
        {
            return this.ActualHeight;
        }


        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }
        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    gridContent.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(borderColour);
        //}

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisMessageGridWPF_MouseDown;
        //}

        private void GnosisMessageGridWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisMessageGridWPF_MouseUp;
        //}
            
        private void GnosisMessageGridWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisMessageGridWPF_MouseEnter;
        //}

        private void GnosisMessageGridWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }


        //public void SetFont(string font)
        //{
        //    fontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int _fontSize)
        //{
        //    fontSize = _fontSize;
        //}


        //public void SetForegroundColour(string contentColour)
        //{
        //    fontColour= StyleHelper.GetBrushFromHex(contentColour);
        //}

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisMessageGridWPF_MouseLeave;
        //}

        private void GnosisMessageGridWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
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

        //public void SetController(GnosisVisibleController gnosisLayoutController)
        //{
        //    controller = (GnosisMessageGridController)gnosisLayoutController;
        //}

        //public GnosisVisibleController GetController()
        //{
        //    return controller;
        //}

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.Padding = new Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Right);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    this.Padding = new Thickness(this.Padding.Left, paddingVertical, this.Padding.Top, paddingVertical);
        //}

        //public FontFamily GetFontFamily()
        //{
        //    return content.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return content.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return content.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return content.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return content.FontStretch;
        //}

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisMessageGridWPF_GotFocus;
        }

        private void GnosisMessageGridWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisMessageGridWPF_LostFocus;
        }

        private void GnosisMessageGridWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetMarginLeft(int marginLeft)
        {
            this.Margin = new Thickness(marginLeft, 0, 0, 0);
        }


    }
}
