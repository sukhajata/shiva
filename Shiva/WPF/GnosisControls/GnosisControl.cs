

using Shiva.Shared.Interfaces;
using System;

namespace GnosisControls
{
    public class GnosisControl : IGnosisControl
    {
        protected string gnosisName;

        protected int id;

        protected int order;

        protected string controlType;


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

    }
}