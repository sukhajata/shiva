using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

using Shiva.Shared.Interfaces;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ShivaWPF3.UtilityWPF;
using Shiva.Shared.BaseControllers;
using System.ComponentModel;
using Shiva.Shared.Data;

namespace GnosisControls
{
    public class GnosisToolbarMenuButton : DropDownButton, IGnosisToolbarMenuButtonImplementation, INotifyPropertyChanged
    {
        protected Action clickHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
      //  protected Action MouseDownHandler;
       // protected Action MouseUpHandler;
      //  protected Action GotMouseFocusHandler;
       // protected Action LostMouseFocusHandler;
        private Menu dropDownMenu;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private string controlType;
        private bool disabled;
        private string gnosisIcon;
        private int iconSize;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private GnosisController.MenuTagEnum menuTag;
        public int order;
        private string shortcut;
        private string tooltip;

        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;

        [GnosisProperty]
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

        [GnosisProperty]
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

        [GnosisProperty]
        public int HorizontalMargin
        {
            get { return horizontalMargin; }
            set
            {
                horizontalMargin = value;
                this.SetHorizontalMarginExt(horizontalMargin);
            }
        }

        [GnosisProperty]
        public int VerticalMargin
        {
            get { return verticalMargin; }
            set
            {
                verticalMargin = value;
                this.SetVerticalMarginExt(verticalMargin);
            }
        }


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
       
        [GnosisProperty]
        public bool Disabled
        {
            get { return disabled; }
            set
            {
                disabled = value;
                this.IsEnabled = !disabled;
                OnPropertyChanged("Disabled");
            }
        }

        [GnosisProperty]
        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                this.Content = caption;
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
        public string GnosisIcon
        {
            get { return gnosisIcon; }
            set
            {
                gnosisIcon = value;
                //this.Content = new Image
                //{
                //    Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(gnosisIcon, this.IsEnabled)))
                //};
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

                    BitmapImage bi = StyleHelper.GetIcon(gnosisIcon, iconSize, (SolidColorBrush)this.Foreground, disabled);

                    this.Content = new Image { Source = bi };
                }
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
            }
        }

        [GnosisProperty]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [GnosisProperty]
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

        [GnosisProperty]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        [GnosisProperty]
        public string Shortcut
        {
            get { return shortcut; }
            set { shortcut = value; }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get { return tooltip; }
            set

            {
                tooltip = value;
                this.ToolTip = tooltip;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public GnosisToolbarMenuButton() : base()
        {
            this.MouseEnter += GnosisToolbarMenuButtonWPF_MouseEnter;
            this.MouseLeave += GnosisToolbarMenuButtonWPF_MouseLeave;
            this.MouseDown += GnosisToolbarMenuButtonWPF_MouseDown;
            this.MouseUp += GnosisToolbarMenuButtonWPF_MouseUp;

        }

        public void AddItem(IGnosisToolbarMenuButtonItemImplementation item)
        {
            if (dropDownMenu == null)
            {
                dropDownMenu = new Menu();
            }
            dropDownMenu.Items.Add(item);
        }

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }



        //public void SetCaption(string caption)
        //{
        //    this.Content = caption;
        //}

        //public void SetStrikethrough(bool strikethrough)
        //{

        //}


        public void SetClickHandler(Action _clickHandler)
        {
            clickHandler = _clickHandler;
            this.Click += GnosisToolbarMenuButtonWPF_Click;
        }


        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }


        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisToolbarMenuButtonWPF_GotFocus;
        }

        private void GnosisToolbarMenuButtonWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisToolbarMenuButtonWPF_MouseEnter;
        //}

        private void GnosisToolbarMenuButtonWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);
        }

        //public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        //{
        //    this.SetHorizontalContentAlignmentExt(horizontalAlignment);
        //}

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        public void SetIcon(string iconName, bool enabled)
        {
            this.Content = new Image
            {
                Source = new BitmapImage(new Uri(GnosisIOHelperWPF.GetIconPath(iconName, enabled)))
            };
        }

        //public void SetIsEnabled(bool isEnabled)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetLocked(bool locked)
        //{
        //    throw new NotImplementedException();
        //}

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisToolbarMenuButtonWPF_LostFocus;
        }

        private void GnosisToolbarMenuButtonWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisToolbarMenuButtonWPF_MouseLeave;
        //}

        private void GnosisToolbarMenuButtonWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisToolbarMenuButtonWPF_MouseDown;
        //}


        private void GnosisToolbarMenuButtonWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisToolbarMenuButtonWPF_MouseUp;
        //}

        private void GnosisToolbarMenuButtonWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }



        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.SetVerticalPaddingExt(paddingVertical);
        }

        //public void SetTooltip(string tooltip)
        //{
        //    this.ToolTip = tooltip;
        //}

        public void SetTooltipVisible(bool enabled)
        {
            ToolTipService.SetIsEnabled(this, enabled);

        }

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

        private void GnosisToolbarMenuButtonWPF_Click(object sender, RoutedEventArgs e)
        {
            clickHandler.Invoke();
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
