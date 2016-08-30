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
using ShivaWPF3.UtilityWPF;
using System.ComponentModel;
using Shiva.Shared.BaseControllers;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisCalendarWPF.xaml
    /// </summary>
    public partial class GnosisCalendar : UserControl, IGnosisCalendarImplementation, INotifyPropertyChanged
    {
        
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private string controlType;
        private string dataset;
        private string datasetItem;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int maxSectionSpan;
        private int order;
        private bool readOnly;
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
        public string Caption
        {
            get
            {
                return caption;
            }

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
                this.Tooltip = tooltip;
                //OnPropertyChanged("Tooltip");
            }
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
            }
        }

        [GnosisPropertyAttribute]
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }

            set
            {
                readOnly = value;
                this.IsEnabled = !readOnly;
              //  OnPropertyChanged("ReadOnly");
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




        public GnosisCalendar()
        {
            InitializeComponent();

            this.MouseEnter += GnosisCalendarWPF_MouseEnter;
            this.MouseLeave += GnosisCalendarWPF_MouseLeave;
            this.MouseDown += GnosisCalendarWPF_MouseDown;
            this.MouseUp += GnosisCalendarWPF_MouseUp;

           // this.PropertyChanged += GnosisCalendar_PropertyChanged;
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

        //private void GnosisCalendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Tooltip":
        //            this.ToolTip = tooltip;
        //            break;
        //        case "ReadOnly":
        //            this.IsEnabled = !readOnly;
        //            break;
        //    }
        //}

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

        //public void RemoveOutlineColour()
        //{
            
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    calendar.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        //}

        //public void SetBorderColour(string borderColour)
        //{
        //    this.BorderBrush = StyleHelper.GetBrushFromHex(borderColour);
        //    this.BorderThickness = new Thickness(1);
        //}

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisCalendar_GotFocus;
        }

        private void GnosisCalendar_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisCalendarWPF_MouseEnter;
        //}

            

        private void GnosisCalendarWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //GotMouseFocusHandler.Invoke();
            HasMouseFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisCalendarWPF_LostFocus;
        }

        private void GnosisCalendarWPF_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        //public void SetLostMouseFocusHandler(Action action)
        //{
        //    LostMouseFocusHandler = action;
        //    this.MouseLeave += GnosisCalendarWPF_MouseLeave;
        //}

        private void GnosisCalendarWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //LostMouseFocusHandler.Invoke();
            HasMouseFocus = false;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisCalendarWPF_MouseDown;
        ////}

        private void GnosisCalendarWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisCalendarWPF_MouseUp;
        //}

        private void GnosisCalendarWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
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

        public double GetHeight()
        {
            return this.ActualHeight;
        }

        public void SetMarginLeft(int horizontalSpacing)
        {
            this.Margin = new Thickness(horizontalSpacing, 0, 0, 0);
        }
    }
}
