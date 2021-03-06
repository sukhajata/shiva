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
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Interfaces;
using System.ComponentModel;
using ShivaWPF3.UtilityWPF;
using System.Windows.Markup;
using Shiva.Shared.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTileTabWPF.xaml
    /// </summary>
    public partial class GnosisTileTab : TabControl, IGnosisTileTabImplementation, INotifyPropertyChanged
    {
        private List<GnosisTileTabItem> tabItems;
        private List<IGnosisFrameImplementation> frames;

        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool hidden;
        private int id;
        private GnosisTileTabItem currentTabItem;
        private int order;
        private string tooltip;

        public GnosisTileTabItem CurrentTileTabItem
        {
            get
            {
                return currentTabItem;
            }

            set
            {
                currentTabItem = value;
                OnPropertyChanged("CurrentTileTabItem");
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
                //string xaml = XamlWriter.Save(this);
                //GnosisIOHelperWPF.WriteXamlToFile(xaml);
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        [GnosisCollection]
        public List<GnosisTileTabItem> TabItems
        {
            get { return tabItems; }
            set { tabItems = value; }
        }

        [GnosisCollection]
        public List<IGnosisFrameImplementation> Frames
        {
            get { return frames; }
            set { frames = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisTileTabItem)
            {
                tabItems.Add((GnosisTileTabItem)child);
            }
            else if (child is IGnosisFrameImplementation)
            {
                frames.Add((IGnosisFrameImplementation)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown type added to GnosisTileTab: " + child.GetType().ToString(), "GnosisTileTab.AddChild()");
            }
        }


        //protected Action GotMouseFocusHandler;
        //protected Action LostMouseFocusHandler;
        //protected Action MouseDownHandler;
        //protected Action MouseUpHandler;
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;
        protected Action<IGnosisTileTabItemImplementation> CloseTabItemHandler;

      

        public GnosisTileTab()
        {
            InitializeComponent();
          //  this.SelectionChanged += GnosisTileTabWPF_SelectionChanged;
            this.MouseDown += GnosisTabWPF_MouseDown;
            this.MouseUp += GnosisTabWPF_MouseUp;
            this.MouseEnter += GnosisTabWPF_MouseEnter;
            this.MouseLeave += GnosisTabWPF_MouseLeave;

           // this.PropertyChanged += GnosisTileTab_PropertyChanged;
        }

        //private void GnosisTileTab_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //switch (e.PropertyName)
        //    //{
                
        //    //}
        //}

        public void SelectTabItem(GnosisTileTabItem item)
        {
            CurrentTileTabItem = item;
            Dispatcher.BeginInvoke((Action)(() => this.SelectedItem = item));

            foreach (GnosisTileTabItem child in this.Items)
            {
                if (child.HeaderButton != null)
                {
                    if (child != item)
                    {
                        child.HeaderButton.Active = false;
                    }
                    else
                    {
                        child.HeaderButton.Active = true;
                    }
                }
            }
        }

        public void LoadTabItem(IGnosisTileTabItemImplementation tabItemImplementation)
        {
            GnosisTileTabItem tabItem = (GnosisTileTabItem)tabItemImplementation;
            //tabItem.HeaderTemplate = this.FindResource("TabHeader") as DataTemplate;
            this.Items.Add(tabItem);
            tabItem.Tag = this.Items.Count - 1;
            CurrentTileTabItem = tabItem;
            // string xaml = XamlWriter.Save(this);
        }


        //public void SetCaption(string caption)
        //{

        //}

        public void SetMarginBottom(int marginBottom)
        {
            this.Margin = new Thickness { Bottom = marginBottom };
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

        public double GetAvailableWidth()
        {
            return this.ActualWidth;
        }

        //public void SetMouseDownHandler(Action action)
        //{
        //    MouseDownHandler = action;
        //    this.MouseDown += GnosisTabWPF_MouseDown;
        //}

        private void GnosisTabWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseDownHandler.Invoke();
            HasMouseDown = true;
        }

        //public void SetMouseUpHandler(Action action)
        //{
        //    MouseUpHandler = action;
        //    this.MouseUp += GnosisTabWPF_MouseUp;
        //}

        private void GnosisTabWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MouseUpHandler.Invoke();
            HasMouseDown = false;
        }
            

        //public void SetGotMouseFocusHandler(Action action)
        //{
        //    GotMouseFocusHandler = action;
        //    this.MouseEnter += GnosisTabWPF_MouseEnter;
        //}

        private void GnosisTabWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
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
        //    this.MouseLeave += GnosisTabWPF_MouseLeave;
        //}

        private void GnosisTabWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // LostMouseFocusHandler.Invoke();
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
            this.Items.Clear();
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisTabWPF_GotFocus;
        }

        private void GnosisTabWPF_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocusHandler.Invoke();
            HasFocus = true;
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisTabWPF_LostFocus;
        }

        private void GnosisTabWPF_LostFocus(object sender, RoutedEventArgs e)
        {
            LostFocusHandler.Invoke();
            HasFocus = false;
        }

        public void SetCloseTabItemHandler(Action<IGnosisTileTabItemImplementation> action)
        {
            CloseTabItemHandler = action;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            GnosisTileTabItem item = (sender as Button).CommandParameter as GnosisTileTabItem;
            CloseTabItemHandler.Invoke(item);
        }

        public void RemoveTabItem(IGnosisTileTabItemImplementation tabItemImplementation)
        {
            this.Items.Remove((GnosisTileTabItem)tabItemImplementation);
        }

        private void btnNewTab_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedItem = sender as GnosisTileTabItem;
        }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            GnosisTileTabItem tabItem = (sender as GnosisToggleButton).CommandParameter as GnosisTileTabItem;
            int index = (int)tabItem.Tag;
            this.SelectedIndex = index;
        }

        
    }
}
