using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShivaShared.Events;

using ShivaShared3.Interfaces;
using ShivaWPF3.ContainerControlsWPF;
using ShivaWPF3.ContentControlsWPF;
using ShivaWPF3.LayoutControlsWPF;
using ShivaShared3.Data;
using ShivaWPF3.GridControlsWPF;
using ShivaWPF3.ToolbarControlsWPF;

namespace ShivaWPF3.UtilityWPF
{
    public class GnosisImplementationCreatorWPF : IGnosisImplementationCreator
    {
        //public IGnosisButtonColumnImplementation GetGnosisButtonColumnImplementation()
        //{
        //    return new GnosisButtonColumnWPF();
        //}

        public IGnosisButtonImplementation GetGnosisButtonImplementation()
        {
            return new GnosisButton();
        }

        //public IGnosisButtonItemImplementation GetGnosisButtonItemImplementation()
        //{
        //    return new GnosisButtonItemWPF();
        //}

        //public IGnosisCalendarImplementation GetGnosisCalendarImplementation()
        //{
        //    return new GnosisCalendarWPF();
        //}

        //public IGnosisCheckColumnImplementation GetGnosisCheckColumnImplementation()
        //{
        //    return new GnosisCheckColumnWPF();
        //}

        public IGnosisCheckFieldImplementation GetGnosisCheckFieldImplementation()
        {
            return new GnosisCheckFieldWPF();
        }

        //public IGnosisComboColumnImplementation GetGnosisComboColumnImplementation()
        //{
        //    return new GnosisComboColumnWPF();
        //}

        public IGnosisComboFieldImplementation GetGnosisComboFieldImplementation()
        {
            return new GnosisComboFieldWPF();
        }

        public IGnosisComboOptionImplementation GetGnosisComboOptionImplementation()
        {
            return new GnosisComboOptionWPF();
        }

        public IGnosisContainerTreeViewItemImplementation GetGnosisContainerTreeViewItemImplementation()
        {
            return new GnosisContainerTreeViewItemWPF();
        }

        //public IGnosisDateTimeColumnImplementation GetGnosisDateTimeColumnImplementation()
        //{
        //    return new GnosisDateTimeColumnWPF();
        //}

        public IGnosisDateTimeFieldImplementation GetGnosisDateTimeFieldImplementation()
        {
            return new GnosisDateTimeFieldWPF();
        }

        public IGnosisGalleryImplementation GetGnosisGalleryImplementation()
        {
            return new GnosisGalleryWPF();
        }

        public IGnosisGalleryItemImplementation GetGnosisGalleryItemImplementation()
        {
            return new GnosisGalleryItemWPF();
        }

        public IGnosisGridImplementation GetGnosisGridImplementation()
        {
            return new GnosisGridWPF();
        }

        //public IGnosisJoinColumnImplementation GetGnosisJoinColumnImplementation()
        //{
        //    return new GnosisJoinColumnWPF();
        //}

        //public IGnosisJoinFieldImplementation GetGnosisJoinFieldImplementation()
        //{
        //    return new GnosisJoinFieldWPF();
        //}

        //public IGnosisLabelFieldImplementation GetGnosisLabelFieldImplementation()
        //{
        //    return new GnosisLabelFieldWPF();
        //}

        //public IGnosisListFieldImplementation GetGnosisListFieldImplementation()
        //{
        //    return new GnosisListFieldWPF();
        //}

        public IGnosisMessageGridImplementation GetGnosisMessageGridImplementation()
        {
            return new GnosisMessageGridWPF();
        }

        public IGnosisNavTileImplementation GetGnosisNavTileImplementation()
        {
            return new GnosisNavTileWPF();
        }

        public IGnosisPanelImplementation GetGnosisPanelImplementation()
        {
            return new GnosisPanelWPF();
        }


        public IGnosisDocFrameImplementation GetGnosisDocFrameImplementation()
        {
            return new GnosisDocFrameWPF();
        }

        public IGnosisPrimarySplitImplementation GetGnosisPrimarySplitImplementation()
        {
            return new GnosisPrimarySplitWPF();
        }

        //public IGnosisRadioFieldImplementation GetGnosisRadioFieldImplementation()
        //{
        //    return new GnosisRadioFieldWPF();
        //}

        public IGnosisSearchFrameImplementation GetGnosisSearchFrameImplementation()
        {
            return new GnosisSearchFrameWPF();
        }

        public IGnosisSplitImplementation GetGnosisSplitImplementation()
        {
            return new GnosisSplitWPF();
        }

        //public IGnosisSystemAddressFieldImplementation GetGnosisSystemAddressFieldImplementation()
        //{
        //    return new GnosisSystemAddressFieldWPF();
        //}

        public IGnosisTileTabImplementation GetGnosisTabImplementation()
        {
            return new GnosisTileTabWPF();
        }

        public IGnosisTileTabItemImplementation GetGnosisTabItemImplementation()
        {
            return new GnosisTileTabItemWPF();
        }

        public IGnosisTextFieldImplementation GetGnosisTextFieldImplementation()
        {
            return new GnosisTextFieldWPF();
        }

        public IGnosisTileImplemenation GetGnosisTileImplementation()
        {
            return new GnosisTileWPF();
        }

        public IGnosisParentWindowImplementation GetGnosisParentWindowImplementation()
        {
            return GlobalData.Singleton.ParentWindowImplementation;
        }

        public IGnosisGridTextFieldImplementation GetGnosisGridTextFieldImplementation()
        {
            return new GnosisGridTextField();
        }

        public IGnosisGridIndentFieldImplementation GetGnosisGridIndentFieldImplementation()
        {
            return new GnosisGridIndentFieldWPF();
        }

        public IGnosisGridHeaderFieldImplementation GetGnosisGridHeaderImplementation()
        {
            return new GnosisGridHeaderFieldWPF();
        }

        public IGnosisCaptionLabelImplementation GetGnosisCaptionLabelImplementation()
        {
            return new GnosisCaptionLabelWPF();
        }

        public IGnosisConnectionFrameImplementation GetGnosisConnectionFrameImplementation()
        {
            return new GnosisConnectionFrameWPF();
        }

        public IGnosisNavFrameImplementation GetGnosisNavFrameImplementation()
        {
            return new GnosisNavFrameWPF();
        }

        public IGnosisDateFieldImplementation GetGnosisDateFieldImplementation()
        {
            return new GnosisDateFieldWPF();
        }

        public IGnosisLinkFieldImplementation GetGnosisLinkFieldImplementation()
        {
            return new GnosisLinkFieldWPF();
        }



        public IGnosisToolbarButtonImplementation GetGnosisToolbarButtonImplementation()
        {
            return new GnosisToolbarButtonWPF();
        }

        public IGnosisToolbarImplementation GetGnosisToolbarImplementation()
        {
            return new GnosisToolbarWPF();
        }

        public IGnosisToolbarMenuButtonImplementation GetGnosisToolbarMenuButtonImplementation()
        {
            return new GnosisToolbarMenuButtonWPF();
        }

        public IGnosisToolbarMenuButtonItemImplementation GetGnosisToolbarMenuButtonItemImplementation()
        {
            return new GnosisToolbarMenuButtonItemWPF();
        }

        public IGnosisToolbarTrayImplementation GetGnosisToolbarTrayImplementation()
        {
            return new GnosisToolbarTrayWPF();
        }

        public IGnosisGallerySearchItemImplementation GetGnosisGallerySearchItemImplementation()
        {
            return new GnosisGallerySearchItemWPF();
        }

        public IGnosisGridComboFieldImplementation GetGnosisGridComboFieldImplementation()
        {
            return new GnosisGridComboFieldWPF();
        }

        public IGnosisGridDateFieldImplementation GetGnosisGridDateFieldImplementation()
        {
            return new GnosisGridDateFieldWPF();
        }

        public IGnosisGridDateTimeFieldImplementation GetGnosisGridDateTimeFieldImplementation()
        {
            return new GnosisGridDateTimeFieldWPF();
        }

        public IGnosisGridCheckFieldImplementation GetGnosisGridCheckFieldImplementation()
        {
            return new GnosisGridCheckFieldWPF();
        }

        public IGnosisToggleButtonImplementation GetGnosisToggleButtonImplementation()
        {
            return new GnosisToggleButtonWPF();
        }

        //public IGnosisLinkMenuButtonItemImplementation GetGnosisLinkMenuButtonItemImplementation()
        //{
        //    throw new NotImplementedException();
        //}

        public IGnosisMenuItemImplementation GetGnosisMenuItemImplementation()
        {
            return new GnosisMenuItemWPF();
        }

        public IGnosisCalendarImplementation GetGnosisCalendarImplementation()
        {
            return new GnosisCalendarWPF();
        }

        public IGnosisTextAreaImplementation GetGnosisTextAreaImplementation()
        {
            return new GnosisTextAreaWPF();
        }

        public IGnosisSystemAddressFieldImplementation GetGnosisSystemAddressFieldImplementation()
        {
            return new GnosisSystemAddressFieldWPF();
        }

        public IGnosisDateResultsFieldImplementation GetGnosisDateResultsFieldImplementation()
        {
            return new GnosisDateResultsFieldWPF();
        }

        public IGnosisResultsTextFieldImplementation GetGnosisTextResultsFieldImplementation()
        {
            return new GnosisTextResultsFieldWPF();
        }

        public IGnosisDateTimeResultsFieldImplementation GetGnosisDateTimeResultsFieldImplementation()
        {
            return new GnosisDateTimeResultsFieldWPF();
        }

        public IGnosisResultsCheckFieldImplementation GetGnosisCheckResultsFieldImplementation()
        {
            return new GnosisResultsCheckField();
        }

        public IGnosisTabHeaderButtonImplementation GetGnosisTabHeaderButtonImplementation()
        {
            return new GnosisTabHeaderButtonWPF();
        }

        //public IGnosisToolbarTrayImplementation GetGnosisToolbarTrayImplementation(GnosisToolbarTray toolbarTray)
        //{
        //    return new GnosisToolbarTrayWPF();
        //}

        //public IGnosisTreeImplementation GetGnosisTreeImplementation()
        //{
        //    return new GnosisTreeWPF();
        //}

        //public IGnosisUnsavedGridImplementation GetGnosisUnsavedGridImplementation()
        //{
        //    return new GnosisUnsavedGridWPF();
        //}
    }
}
