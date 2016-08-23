using System;
using System.Collections.Generic;
using System.Text;

using ShivaShared3.Events;
using ShivaShared3.Data;
using GnosisControls;
using ShivaShared3.DataControllers;
using System.Xml.Linq;
using ShivaShared3.Interfaces;

namespace ShivaShared3.Utility
{
    public class GnosisControlCreator
    {
        public static GnosisTileTab CreateGnosisTab()
        {
            GnosisTileTab tab = new GnosisTileTab();
            tab.ID = GlobalData.Singleton.GetNewControlID();
            tab.Order = 1;
            tab.GnosisName = "Tab" + tab.ID.ToString();
            tab.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisTileTab));

            return tab;
        }

        public static GnosisTileTabItem CreateGnosisTabItem(int order)
        {
            GnosisTileTabItem tabItem = new GnosisTileTabItem();
            tabItem.ID = GlobalData.Singleton.GetNewControlID();
            tabItem.GnosisName = "TabItem" + tabItem.ID.ToString();
            tabItem.Order = order;
            tabItem.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisTileTabItem));

            return tabItem;
        }

        internal static IGnosisContainerTreeViewItemImplementation CreateGnosisContainerTreeViewItem()
        {
            return new GnosisContainerTreeViewItem();
        }

        public static GnosisTabHeaderButton CreateGnosisTabHeaderButton(int order, string caption)
        {
            GnosisTabHeaderButton btn = new GnosisTabHeaderButton();
            btn.ID = GlobalData.Singleton.GetNewControlID();
            btn.GnosisName = "HeaderButton" + btn.ID.ToString();
            btn.Order = order;
            btn.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisTabHeaderButton));
            btn.Caption = caption;

            return btn;
        }

        public static GnosisDocumentFrame CreateGnosisDocFrame(string systemName, int systemVersion, string url)
        {
            GnosisDocumentFrame docFrame = new GnosisDocumentFrame();
            docFrame.ID = GlobalData.Singleton.GetNewControlID();
            docFrame.GnosisName = "DocFrame" + docFrame.ID.ToString();
            docFrame.Order = 1;
            docFrame._Created = true;
            docFrame.IsEmpty = true;
            docFrame.IsEditing = false;
          //  docFrame.URL = url;
            docFrame.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisDocumentFrame));

            return docFrame;
        }

        internal static GnosisMenuButton CreateGnosisMenuButton()
        {
            return new GnosisMenuButton();
        }

        internal static GnosisToolbarButton CreateGnosisToolbarButton()
        {
            throw new NotImplementedException();
        }

        public static GnosisGalleryItem CreateGnosisGalleryItem(int order, bool expanded)
        {
            GnosisGalleryItem galleryItem = new GnosisGalleryItem();
            galleryItem.ID = GlobalData.Singleton.GetNewControlID();
            galleryItem.GnosisName = "GalleryItem" + galleryItem.ID;
            galleryItem.Order = order;
            galleryItem.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisGalleryItem));
            galleryItem.GnosisExpanded = expanded;

            return galleryItem;
        }

        internal static GnosisSplit CreateGnosisSplit(int order)
        {
            GnosisSplit split = new GnosisSplit();
            split.ID = GlobalData.Singleton.GetNewControlID();
            split.GnosisName = "Split" + split.ID.ToString();
            split.Order = order;

            return split;
        }

        internal static GnosisGalleryDocumentItem CreateGnosisGalleryDocumentItem(GnosisGalleryDocumentItem docItem)
        {
            GnosisGalleryDocumentItem galleryItem = new GnosisGalleryDocumentItem();

            galleryItem.ID = GlobalData.Singleton.GetNewControlID();
            galleryItem.GnosisName = "GallerySearchItem" + galleryItem.ID.ToString();
            galleryItem.Order = docItem.Order;
            galleryItem.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisGalleryDocumentItem));
            galleryItem.Caption = docItem.Caption;
            galleryItem.GnosisExpanded = docItem.GnosisExpanded;
            galleryItem.DocumentEntityID = docItem.DocumentEntityID;
            galleryItem.DocumentSystemID = docItem.DocumentSystemID;
            galleryItem.DocumentAction = docItem.DocumentAction;

            return galleryItem;
        }

        internal static GnosisComboOption CreateGnosisComboOption(int order)
        {
            GnosisComboOption comboOption = new GnosisComboOption();
            comboOption.ID = GlobalData.Singleton.GetNewControlID();
            comboOption.GnosisName = "Option" + comboOption.ID.ToString();
            comboOption.Order = order;

            return comboOption;
        }

        internal static GnosisTextField CreateGnosisTextField(int order)
        {
            GnosisTextField textField = new GnosisTextField();
            textField.ID = GlobalData.Singleton.GetNewControlID();
            textField.GnosisName = "Text Field" + textField.ID.ToString();
            textField.Order = order;
            textField.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisTextField));

            return textField;
        }

        internal static GnosisGridTextField CreateGnosisGridTextField(GnosisTextColumn textColumn)
        {
            GnosisGridTextField gridTextField = new GnosisGridTextField();
            AssignGridFieldProperties(gridTextField, textColumn);

            gridTextField.GnosisName = "Text Field" + gridTextField.ID.ToString();
            gridTextField.MaxChars = textColumn.MaxChars;
            gridTextField.MinTextDisplayWidthChars = textColumn.MinTextDisplayWidthChars;
            gridTextField.MaxTextDisplayWidthChars = textColumn.MaxTextDisplayWidthChars;

            return gridTextField;

        }

        internal static IGnosisCaptionLabelImplementation CreateGnosisCaptionLabel()
        {
            return new GnosisCaptionLabel();
        }

        internal static GnosisGridCheckField CreateGnosisGridCheckField(GnosisCheckColumn checkColumn)
        {
            GnosisGridCheckField gridCheckField = new GnosisGridCheckField();
            AssignGridFieldProperties(gridCheckField, checkColumn);

            gridCheckField.GnosisName = "Grid Check Field " + gridCheckField.ID.ToString();
          //  gridCheckField.GnosisGroupName = checkColumn.GnosisGroupName;
           // gridCheckField.CheckedFactor = checkColumn.CheckedFactor;
            gridCheckField.Value = checkColumn.Value;

            return gridCheckField;
        }

        private static void AssignGridFieldProperties(IGnosisGridFieldImplementation gridField, GnosisGridColumn column)
        {
            gridField.ID = GlobalData.Singleton.GetNewControlID();
            gridField.Order = column.Order;
            gridField.ControlType = ControlTypeMapping.GetControlTypeName(gridField.GetType());
            gridField.Tooltip = column.Tooltip;
            gridField.ContentHorizontalAlignment = column.ContentHorizontalAlignment;
            gridField.ContentVerticalAlignment = column.ContentVerticalAlignment;
            gridField.MinDisplayChars = column.MinDisplayChars;
            gridField.MaxDisplayChars = column.MaxDisplayChars;
            gridField.Hidden = column.Hidden;
            gridField.ReadOnly = column.ReadOnly;
            gridField.Dataset = column.Dataset;
            gridField.DatasetItem = column.DatasetItem;
            gridField.IsEvenRow = column.IsEvenRow;
            gridField.Value = column.Value;
        }

        

        internal static GnosisGridDateField CreateGnosisGridDateField(GnosisDateColumn dateColumn)
        {
            GnosisGridDateField gridDateField = new GnosisGridDateField();
            AssignGridFieldProperties(gridDateField, dateColumn);

            gridDateField.GnosisName = "Grid Date Field " + gridDateField.ID.ToString();

            return gridDateField;
        }

        internal static GnosisGridDateTimeField CreateGnosisGridDateTimeField(GnosisDateTimeColumn dateTimeColumn)
        {
            GnosisGridDateTimeField gridDateTimeField = new GnosisGridDateTimeField();
            AssignGridFieldProperties(gridDateTimeField, dateTimeColumn);

            gridDateTimeField.GnosisName = "Grid Date Time Field " + gridDateTimeField.ID.ToString();

            return gridDateTimeField;
        }

        internal static GnosisGridComboField CreateGnosisGridComboField(GnosisComboColumn comboColumn)
        {
            GnosisGridComboField gridComboField = new GnosisGridComboField();
            AssignGridFieldProperties(gridComboField, comboColumn);

            gridComboField.GnosisName = "Grid Combo Field " + gridComboField.ID.ToString();
            gridComboField.DocumentSystemID = comboColumn.DocumentSystemID;
            gridComboField.DocumentEntityID = comboColumn.DocumentEntityID;

            return gridComboField;
        }

        internal static GnosisGridLinkField CreateGnosisGridLinkField(GnosisLinkColumn linkColumn)
        {
            GnosisGridLinkField gridLinkField = new GnosisGridLinkField();
            AssignGridFieldProperties(gridLinkField, linkColumn);

            gridLinkField.GnosisName = "Grid Link Field " + gridLinkField.ID.ToString();
            gridLinkField.DocumentSystemID = linkColumn.DocumentSystemID;
            gridLinkField.DocumentEntityID = linkColumn.DocumentEntityID;
            gridLinkField.Perspective = linkColumn.Perspective;

            return gridLinkField;
        }

        internal static GnosisResultsTextField CreateGnosisResultsTextField(GnosisTextResults textResults)
        {
            GnosisResultsTextField resultsTextField = new GnosisResultsTextField();
            AssignGridFieldProperties(resultsTextField, textResults);

            resultsTextField.GnosisName = "Text Results Field" + resultsTextField.ID.ToString();
            resultsTextField.MaxChars = textResults.MaxChars;
            resultsTextField.MinTextDisplayWidthChars = textResults.MinTextDisplayWidthChars;
            resultsTextField.MaxTextDisplayWidthChars = textResults.MaxTextDisplayWidthChars;

            return resultsTextField;
        }

        internal static GnosisResultsCheckField CreateGnosisResultsCheckField(GnosisCheckResults checkResults)
        {
            GnosisResultsCheckField resultsCheckField = new GnosisResultsCheckField();
            AssignGridFieldProperties(resultsCheckField, checkResults);

            resultsCheckField.GnosisName = "Check Results Field" + resultsCheckField.ID.ToString();

            return resultsCheckField;
        }

        internal static GnosisResultsDateField CreateGnosisResultsDateField(GnosisDateResults dateResults)
        {
            GnosisResultsDateField resultsDateField = new GnosisResultsDateField();
            AssignGridFieldProperties(resultsDateField, dateResults);

            resultsDateField.GnosisName = "Date Results Field " + resultsDateField.ID.ToString();

            return resultsDateField;
        }

        internal static GnosisResultsDateTimeField CreateGnosisResultsDateTimeField(GnosisDateTimeResults dateTimeResults)
        {
            GnosisResultsDateTimeField resultsDateTimeField = new GnosisResultsDateTimeField();
            AssignGridFieldProperties(resultsDateTimeField, dateTimeResults);

            resultsDateTimeField.GnosisName = "DateTime Results " + resultsDateTimeField.ID.ToString();

            return resultsDateTimeField;
        }

        internal static GnosisGridNumberField CreateGnosisGridNumberField(GnosisNumberColumn numberColumn)
        {
            GnosisGridNumberField gridNumberField = new GnosisGridNumberField();
            AssignGridFieldProperties(gridNumberField, numberColumn);

            gridNumberField.GnosisName = "Grid Number Field " + gridNumberField.ID.ToString();
            gridNumberField.MaxChars = numberColumn.MaxChars;
            gridNumberField.UnitOfMeasure = numberColumn.UnitOfMeasure;
            gridNumberField._MeasureRelativePosition = numberColumn._MeasureRelativePosition;

            return gridNumberField;
        }

        internal static GnosisResultsNumberField CreateGnosisResultsNumberField(GnosisNumberResults numberResults)
        {
            GnosisResultsNumberField resultsNumberField = new GnosisResultsNumberField();
            AssignGridFieldProperties(resultsNumberField, numberResults);

            resultsNumberField.GnosisName = "Number Results Field " + resultsNumberField.ID.ToString();
            resultsNumberField.MaxChars = numberResults.MaxChars;
            resultsNumberField.UnitOfMeasure = numberResults.UnitOfMeasure;
            resultsNumberField._MeasureRelativePosition = numberResults._MeasureRelativePosition;

            return resultsNumberField;
        }


        public static GnosisGallerySearchItem CreateGnosisGallerySearchItem(GnosisGallerySearchItem searchItem)
        {
            GnosisGallerySearchItem galleryItem = new GnosisGallerySearchItem();

            galleryItem.ID = GlobalData.Singleton.GetNewControlID();
            galleryItem.GnosisName = "GallerySearchItem" + galleryItem.ID.ToString();
            galleryItem.Order = searchItem.Order;
            galleryItem.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisGallerySearchItem));
            galleryItem.Caption = searchItem.Caption;
            galleryItem.GnosisExpanded = searchItem.GnosisExpanded;
            galleryItem.AutoSearchAction = searchItem.AutoSearchAction;
            galleryItem.SearchAction = searchItem.SearchAction;
            galleryItem.SearchSystemID = searchItem.SearchSystemID;
            galleryItem.SearchEntityID = searchItem.SearchEntityID;
            galleryItem.SearchParameters = searchItem.SearchParameters;

            return galleryItem;
        }

        public static GnosisSearchParameter CreateGnosisSearchParameter(
            int order,
            string dataset,
            string datasetItem,
            string parameter)
        {
            GnosisSearchParameter searchParameter = new GnosisSearchParameter();
            searchParameter.ID = GlobalData.Singleton.GetNewControlID();
            searchParameter.GnosisName = "SearchParameter" + searchParameter.ID.ToString();
            searchParameter.Order = order;
            searchParameter.ControlType = ControlTypeMapping.GetControlTypeName(typeof(GnosisSearchParameter));
            searchParameter.Dataset = dataset;
            searchParameter.DatasetItem = datasetItem;
            searchParameter.Parameter = parameter;

            return searchParameter;
        }

        public static GnosisInstance CreateInstance(int entityID, string entityName, string systemURL, XElement content)
        {
            GnosisInstance instance = new GnosisInstance();
            instance.InstanceID = GlobalData.Singleton.GetNewControlID().ToString();
            instance.SystemURL = systemURL;
            instance.EntityID = entityID;
            instance.EntityName = entityName;
            instance.Content = content;

            return instance;
        }

        public static GnosisInstance CreateInstance(XElement XInstance)
        {
            GnosisInstance instance = new GnosisInstance();
            instance.InstanceID = GlobalData.Singleton.GetNewControlID().ToString();
            instance.SystemURL = XInstance.Attribute("SystemURL").Value;
            instance.EntityID = Convert.ToInt32(XInstance.Attribute("EntityID").Value);
            instance.UserID = Convert.ToInt32(XInstance.Attribute("UserID").Value);
            instance.Content = XInstance;

            return instance;
        }

        public static GnosisGenericToggleMenuItem CreateGenericToggleMenuItem(string name, int order, string icon, string tooltip, int variableSystemID, int variableControlID, int code)
        {
            GnosisGenericToggleMenuItem toggleItem = new GnosisGenericToggleMenuItem();
            toggleItem.ID = GlobalData.Singleton.GetNewControlID();
            toggleItem.GnosisName = name;
            toggleItem.Order = order;
            toggleItem.GnosisIcon = icon;
            toggleItem.Tooltip = tooltip;
            toggleItem.VariableSystemID = variableSystemID;
            toggleItem.VariableControlID = variableControlID;
            toggleItem.Code = code;

            return toggleItem;
        }

        internal static IGnosisGridHeaderFieldImplementation CreateGnosisGridHeaderField()
        {
            return new GnosisGridHeaderField();
        }

        internal static IGnosisGridIndentFieldImplementation CreateGnosisGridIndentField()
        {
            return new GnosisGridIndentField();
        }
    }
}
