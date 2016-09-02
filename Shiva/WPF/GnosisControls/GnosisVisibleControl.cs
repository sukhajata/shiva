using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GnosisControls
{
    public partial class GnosisVisibleControl : UserControl, IGnosisVisibleControlImplementation, INotifyPropertyChanged
    {
        protected string gnosisName;
        protected IGnosisVisibleControlImplementation gnosisParent;
        protected int id;
        protected int order;
        protected string controlType;

        protected bool hidden;
        protected bool hasFocusField;
        protected bool hasMouseDown;
        protected bool hasMouseFocus;
        protected string tooltip;
        protected string caption;



        public bool HasFocus
        {
            get { return hasFocusField; }
            set
            {
                hasFocusField = value;
                OnPropertyChanged("HasFocus");
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

        public bool HasMouseFocus
        {
            get { return hasMouseFocus; }
            set
            {
                hasMouseFocus = value;
                OnPropertyChanged("HasMouseFocus");
            }
        }

        [GnosisProperty]
        public string ControlType
        {
            get { return controlType; }
            set { controlType = value; }
        }


        //public enum GnosisTypeEnum
        //{
        //    GnosisGlobalGeneric, GnosisConnection, GnosisSystemGeneric, GnosisNavFrame, GnosisDocFrame, GnosisSearchFrame,
        //    GnosisParentWindow, GnosisChildWindow, GnosisPrimarySplit, GnosisSplit, GnosisSplitDetail, GnosisTile, GnosisNavTile,
        //    GnosisToolbarTray, GnosisToolbar, GnosisToolbarButton, GnosisToolbarButtonItem, GnosisGenericControl,
        //    GnosisSystemAddressField, GnosisGallery, GnosisGalleryItem, GnosisPanel, GnosisTab, GnosisGrid, GnosisTree,
        //    GnosisCalendar, GnosisTabItem, GnosisButton, GnosisButtonItem, GnosisMessageGrid, GnosisUnsavedGrid,
        //    GnosisTextField, GnosisDateTimeField, GnosisJoinField, GnosisListField, GnosisComboField, GnosisCheckField,
        //    GnosisRadioField, GnosisLabelField, GnosisTextColumn, GnosisDateTimeColumn, GnosisJoinColumn,
        //    GnosisComboColumn, GnosisCheckColumn, GnosisButtonColumn, GnosisTreeAttribute, GnosisCalendarAttribute,
        //    GnosisPanelColumn, GnosisGenericMenu, GnosisGenericMenuItem,GnosisGenericMenuReference
        //};




        [GnosisProperty]
        public string GnosisName
        {
            get
            {
                return this.gnosisName;
            }
            set
            {
                this.gnosisName = value;
            }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisProperty]
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        [GnosisProperty]
        public int Order
        {
            get
            {
                return this.order;
            }
            set
            {
                this.order = value;
            }
        }


        public virtual void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
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

        [GnosisProperty]
        public bool Hidden
        {
            get
            {
                return this.hidden;
            }
            set
            {
                this.hidden = value;
                OnPropertyChanged("Hidden");
            }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get
            {
                return this.tooltip;
            }
            set
            {
                this.tooltip = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //public double GetPaddingHorizontal()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    throw new NotImplementedException();
        //}

        public void SetTooltipVisible(bool visible)
        {
            throw new NotImplementedException();
        }

        public void SetGotFocusHandler(Action action)
        {
            throw new NotImplementedException();
        }

        public void SetLostFocusHandler(Action action)
        {
            throw new NotImplementedException();
        }
    }
}
