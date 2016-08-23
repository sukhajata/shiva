using GnosisControls;
using ShivaShared3.Data;
using ShivaShared3.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.DataControllers
{
    public class ControlTypeMapping
    {
        static Dictionary<string, Type> ControlTypeMappings = new Dictionary<string, Type>() {
            {"Button", typeof(GnosisButton) },
            {"Button Group", typeof(GnosisButtonGroup) },
            //{"Button Item", typeof(GnosisButtonItem) },
            {"Calendar", typeof(GnosisCalendar) },
            {"Check Column", typeof(GnosisComboColumn) },
            {"Check Field", typeof(GnosisCheckField) },
            {"Check Group", typeof(GnosisCheckGroup) },
            {"Check Results", typeof(GnosisCheckResults) },
            {"Child Window", typeof(GnosisChildWindow) },
            {"Combo Column", typeof(GnosisComboColumn) },
            {"Combo Field", typeof(GnosisComboField) },
            {"Combo Option", typeof(GnosisComboOption) },
            {"Connection Document", typeof(GnosisConnectionDocument) },
            { "Connection Frame", typeof(GnosisConnectionFrame) },
            {"Content Control" , typeof(GnosisContentControl)},
            {"Date Column", typeof(GnosisDateColumn) },
            {"Date Field", typeof(GnosisDateField) },
            {"Date Results", typeof(GnosisDateResults) },
            {"Date Time Column", typeof(GnosisDateTimeColumn) },
            {"Date Time Field", typeof(GnosisDateTimeField) },
            {"Date Time Results", typeof(GnosisDateTimeResults) },
            {"Document Frame", typeof(GnosisDocumentFrame) },
            {"Environment Variable", typeof(GnosisEnvironmentVariable) },
            {"Event Handler", typeof(GnosisEventHandler) },
            {"Event Handler Source", typeof(GnosisEventHandlerSource) },
            {"Generic Menu", typeof(GnosisGenericMenu) },
            {"Generic Menu Item", typeof(GnosisGenericMenuItem) },
            //{"Toolbar Button", typeof(GnosisToolbarButton) },
          //  {"Toolbar Button Item", typeof(GnosisToolbarButtonItem) },
            //  {"Connection Parameter", typeof(GnosisConnectionParameter) },
            {"Gallery", typeof(GnosisGallery) } ,
            {"Gallery Dataset Attribute", typeof(GnosisGallerySearchAttribute) },
            {"Gallery Dataset Item", typeof(GnosisGalleryDatasetItem) },
            {"Gallery Detail", typeof(GnosisGalleryDetail) },
            {"Gallery Document Item", typeof(GnosisGalleryDocumentItem) },
            {"Gallery Item", typeof(GnosisGalleryItem) },
            {"Gallery Search Item", typeof(GnosisGallerySearchItem) },
            {"Generic Control", typeof(GnosisGenericControl) },
            {"Grid", typeof(GnosisGrid) },
            {"Grid Text Field", typeof(GnosisGridTextField) },
            {"Grid Check Field", typeof(GnosisGridCheckField) },
            {"Instance Variable", typeof(GnosisInstanceVariable) },
            {"Label Field", typeof(GnosisLabelField) },
            {"Link Field", typeof(GnosisLinkField) },
            {"List Field", typeof(GnosisListField) },
            {"List Option", typeof(GnosisComboOption) },
            {"Message Grid", typeof(GnosisMessageGrid) },
            {"Navigator Document", typeof(GnosisNavigatorDocument) },
            {"Navigator Frame", typeof(GnosisNavigatorFrame) },
            {"Navigator Tile", typeof(GnosisNavigatorTile) },
            {"Panel", typeof(GnosisPanel) },
            {"Parent Window", typeof(GnosisParentWindow) },
            {"Permission Variable", typeof(GnosisPermissionVariable) } ,
            {"Primary Split", typeof(GnosisPrimarySplit) },
            {"Radio Field", typeof(GnosisRadioField) },
            {"Radio Group", typeof(GnosisRadioGroup) },
            {"Search Frame", typeof(GnosisSearchFrame) },
            {"Search Parameter", typeof(GnosisSearchParameter) },
            {"Split", typeof(GnosisSplit) },
            {"Split Detail", typeof(GnosisSplitDetail) },
            {"System Address Field", typeof(GnosisSystemAddressField) },
            {"System Definition", typeof(GnosisSystemDefinition) },
            {"Tab", typeof(GnosisTileTab) },
            {"Tab Item", typeof(GnosisTileTabItem) },
            {"Tab Header Button", typeof(GnosisTabHeaderButton) },
            {"Text Column", typeof(GnosisTextColumn) },
            {"Text Field", typeof(GnosisTextField) },
            {"Text Results", typeof(GnosisTextResults) },
            {"Tile", typeof(GnosisTile) },
            {"Tile Detail", typeof(GnosisTileDetail) },
            {"Toggle Button", typeof(GnosisToggleButton) },
            {"Toolbar Tray", typeof(GnosisToolbarTray) },
            {"Toolbar",  typeof(GnosisToolbar) }
            //   {"Tree", typeof(GnosisTree) },
            //  {"Unsaved Grid", typeof(GnosisUnsavedGrid) },
            //{"Filter", typeof(GnosisFilter) },
            //{"Tree Attribute", typeof(GnosisTreeAttribute) },
            //{"Calendar Attribute", typeof(GnosisCalendarAttribute) },
            //{"Layout Column", typeof(GnosisLayoutColumn) },
//Element Reference GnosisDatasetItemReference
//Search Reference    GnosisSearchReference
            
//Drag Event  GnosisDragEvent
//Drop Event GnosisDropEvent
//Control Input   GnosisControlInput
//Control Output GnosisControlOutput
//Document Input  GnosisDocumentInput
//Document Output GnosisDocumentOutput
            
            };

        public static Type GetControlType(string controlType)
        {
            if (ControlTypeMappings.ContainsKey(controlType))
            {
                return ControlTypeMappings[controlType];
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Control type not found: " + controlType, "ControlTypeMappings");
                return null;
            }
        }

        public static string GetControlTypeName(Type type)
        {
            foreach (KeyValuePair<string, Type> pair in ControlTypeMappings)
            {
                if (pair.Value == type)
                {
                    return pair.Key;
                }
            }

            return null;

        }

    }
}
