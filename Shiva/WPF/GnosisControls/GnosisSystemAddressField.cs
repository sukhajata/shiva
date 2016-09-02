using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;

namespace GnosisControls
{
    public partial class GnosisSystemAddressField : TextBox, IGnosisSystemAddressFieldImplementation, INotifyPropertyChanged
    {
        private string address;
        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool hidden;
        private int id;
        private bool locked;
        private string menuTag;
        private int order;
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

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                this.Text = address;
            }
        }

        [GnosisProperty]
        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                OnPropertyChanged("Caption");
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
            get { return hidden; }
            set
            {
                hidden = value;
                this.SetVisibleExt(!hidden);
                OnPropertyChanged("Hidden");
            }

        }

        [GnosisProperty]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [GnosisProperty]
        public bool Locked
        {
            get { return locked; }
            set
            {
                locked = value;
                this.IsEnabled = !locked;
                OnPropertyChanged("Locked");
            }
        }


        [GnosisProperty]
        public string MenuTag
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

      
        public GnosisSystemAddressField()
            :base()
        {
            this.Padding = new System.Windows.Thickness(3, 0, 3, 0);
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            this.MouseEnter += GnosisSystemAddressFieldWPF_MouseEnter;
            this.MouseLeave += GnosisSystemAddressFieldWPF_MouseLeave;
            this.MouseDown += GnosisSystemAddressFieldWPF_MouseDown;
            this.MouseUp += GnosisSystemAddressFieldWPF_MouseUp;

          //  this.PropertyChanged += GnosisSystemAddressField_PropertyChanged;
        }

        //private void GnosisSystemAddressField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch(e.PropertyName)
        //    {
        //        case "Locked":
        //            this.IsEnabled = !locked;
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //    }
        //}

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

      
        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new System.Windows.Thickness(1);
        //}

        
        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisSystemAddressFieldWPF_GotFocus;
        }

        private void GnosisSystemAddressFieldWPF_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisSystemAddressFieldWPF_MouseEnter;
        //}

        private void GnosisSystemAddressFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

            

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisSystemAddressFieldWPF_LostFocus;
        }

        private void GnosisSystemAddressFieldWPF_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisSystemAddressFieldWPF_MouseLeave;
        //}

        private void GnosisSystemAddressFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisSystemAddressFieldWPF_MouseDown;
        //}

        private void GnosisSystemAddressFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisSystemAddressFieldWPF_MouseUp;
        //}

        private void GnosisSystemAddressFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // MouseUpHandler.Invoke();
            HasMouseDown = false;
        }

        //public void SetOutlineColour(string outlineColour)
        //{
            
        //}

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    this.SetHorizontalPaddingExt(paddingHorizontal);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    this.SetVerticalPaddingExt(paddingVertical);
        //}

        //public void SetTooltip(string toolTip)
        //{
        //    this.ToolTip = toolTip;
        //}

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        //public void SetVisible(bool visible)
        //{
        //    this.SetVisibleExt(visible);
        //}

        //public void SetAddress(string address)
        //{
        //    this.Text = address;
        //}
    }
}
