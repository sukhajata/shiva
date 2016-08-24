using System;
using System.Collections.Generic;
using System.Text;


namespace Shiva.Shared.Interfaces
{
    public interface IGnosisImplementationCreator
    {
        //Layout Controls
        IGnosisParentWindowImplementation GetGnosisParentWindowImplementation();
        IGnosisSplitImplementation GetGnosisSplitImplementation();
        IGnosisTileImplemenation GetGnosisTileImplementation();
        IGnosisToolbarButtonImplementation GetGnosisToolbarButtonImplementation();
        IGnosisToolbarMenuButtonImplementation GetGnosisToolbarMenuButtonImplementation();
        IGnosisToolbarMenuButtonItemImplementation GetGnosisToolbarMenuButtonItemImplementation();
        IGnosisContainerTreeViewItemImplementation GetGnosisContainerTreeViewItemImplementation();

        //IGnosisSystemAddressFieldImplementation GetGnosisSystemAddressFieldImplementation();
        IGnosisToolbarTrayImplementation GetGnosisToolbarTrayImplementation();
        IGnosisToolbarImplementation GetGnosisToolbarImplementation();
        IGnosisPrimarySplitImplementation GetGnosisPrimarySplitImplementation();
        IGnosisDateResultsFieldImplementation GetGnosisDateResultsFieldImplementation();
        IGnosisResultsTextFieldImplementation GetGnosisTextResultsFieldImplementation();
        IGnosisDateTimeResultsFieldImplementation GetGnosisDateTimeResultsFieldImplementation();
        IGnosisResultsCheckFieldImplementation GetGnosisCheckResultsFieldImplementation();
        IGnosisDocFrameImplementation GetGnosisDocFrameImplementation();
        IGnosisSearchFrameImplementation GetGnosisSearchFrameImplementation();
        IGnosisNavTileImplementation GetGnosisNavTileImplementation();


        //   IGnosisContainerTreeViewItemImplementation GetGnosisContainerTreeViewItemImplementation();

        //Content Controls
        IGnosisCaptionLabelImplementation GetGnosisCaptionLabelImplementation();
        IGnosisButtonImplementation GetGnosisButtonImplementation();
        IGnosisMenuItemImplementation GetGnosisMenuItemImplementation();

        //IGnosisButtonColumnImplementation GetGnosisButtonColumnImplementation();
        //IGnosisButtonItemImplementation GetGnosisButtonItemImplementation();
        //IGnosisCalendarImplementation GetGnosisCalendarImplementation();
        // IGnosisCalendarAttributeImplementation GetGnosisCalendarAttributeImplementation();
        //     IGnosisCheckColumnImplementation GetGnosisCheckColumnImplementation();
        IGnosisCheckFieldImplementation GetGnosisCheckFieldImplementation();
    //    IGnosisComboColumnImplementation GetGnosisComboColumnImplementation();
        IGnosisComboFieldImplementation GetGnosisComboFieldImplementation();
        IGnosisComboOptionImplementation GetGnosisComboOptionImplementation();
        IGnosisConnectionFrameImplementation GetGnosisConnectionFrameImplementation();

        IGnosisDateFieldImplementation GetGnosisDateFieldImplementation();
        IGnosisDateTimeFieldImplementation GetGnosisDateTimeFieldImplementation();
        IGnosisTabHeaderButtonImplementation GetGnosisTabHeaderButtonImplementation();
        IGnosisGalleryImplementation GetGnosisGalleryImplementation();
        IGnosisGallerySearchItemImplementation GetGnosisGallerySearchItemImplementation();
        IGnosisGalleryItemImplementation GetGnosisGalleryItemImplementation();
        IGnosisGridImplementation GetGnosisGridImplementation();
        IGnosisLinkFieldImplementation GetGnosisLinkFieldImplementation();
        IGnosisNavFrameImplementation GetGnosisNavFrameImplementation();
        IGnosisGridHeaderFieldImplementation GetGnosisGridHeaderImplementation();
        //IGnosisJoinColumnImplementation GetGnosisJoinColumnImplementation();
        //IGnosisJoinFieldImplementation GetGnosisJoinFieldImplementation();
        //IGnosisLabelFieldImplementation GetGnosisLabelFieldImplementation();
        //    IGnosisListFieldImplementation GetGnosisListFieldImplementation();
        IGnosisGridTextFieldImplementation GetGnosisGridTextFieldImplementation();
        IGnosisMessageGridImplementation GetGnosisMessageGridImplementation();
        IGnosisPanelImplementation GetGnosisPanelImplementation();
    //    IGnosisRadioFieldImplementation GetGnosisRadioFieldImplementation();
        IGnosisTileTabImplementation GetGnosisTabImplementation();
        IGnosisTileTabItemImplementation GetGnosisTabItemImplementation();
        IGnosisToggleButtonImplementation GetGnosisToggleButtonImplementation();

        //     IGnosisTextColumnImplementation GetGnosisTextColumnImplementation();
        IGnosisTextFieldImplementation GetGnosisTextFieldImplementation();
        IGnosisGridComboFieldImplementation GetGnosisGridComboFieldImplementation();
        IGnosisGridDateFieldImplementation GetGnosisGridDateFieldImplementation();
        IGnosisGridDateTimeFieldImplementation GetGnosisGridDateTimeFieldImplementation();
        IGnosisGridCheckFieldImplementation GetGnosisGridCheckFieldImplementation();
        IGnosisCalendarImplementation GetGnosisCalendarImplementation();
        IGnosisTextAreaImplementation GetGnosisTextAreaImplementation();
        IGnosisSystemAddressFieldImplementation GetGnosisSystemAddressFieldImplementation();
        IGnosisGridIndentFieldImplementation GetGnosisGridIndentFieldImplementation();
        //     IGnosisTreeImplementation GetGnosisTreeImplementation();
        //IGnosisTreeAttributeImplementation GetGnosisTreeAttributeImplementation();
        //       IGnosisUnsavedGridImplementation GetGnosisUnsavedGridImplementation();



    }
}
