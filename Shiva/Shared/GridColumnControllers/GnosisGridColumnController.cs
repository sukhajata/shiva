using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Data;
using Shiva.Shared.DataControllers;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Utility;

namespace Shiva.Shared.GridColumnControllers
{
    public class GnosisGridColumnController
    {
        protected GnosisGridColumn columnModel;
        private IGnosisGridHeaderFieldImplementation header;
        private IGnosisCaptionLabelImplementation columnarHeader;
        //private GnosisGridFieldController fieldController;
        GnosisInstanceController instanceController;
        GnosisGridController parent;
        private int colSpan;
        private List<IGnosisGridFieldImplementation> fields;
        private IGnosisGridFieldImplementation fieldClone;
        private string font;
        private int fontSize;

        private double characterWidth;
        private double fieldHeight;
       // private double minFieldWidth;
        private double maxFieldWidth;
        private int numLines;

        public List<IGnosisGridFieldImplementation> Fields
        {
            get { return fields; }
        }
        public int ID
        {
            get { return columnModel.ID; }
        }
        public int ColSpan
        {
            get { return colSpan; }
            set { colSpan = value; }
        }
        public string Caption
        {
            get { return columnModel.Caption; }
        }

        public string ColumnType
        {
            get { return columnModel.ControlType; }
        }

        public IGnosisGridHeaderFieldImplementation Header
        {
            get { return header; }
        }

        public GnosisGridColumn ColumnModel
        {
            get { return columnModel; }
        }

        public string Dataset
        {
            get { return columnModel.Dataset; }
        }

        public string DatasetItem
        {
            get { return columnModel.DatasetItem; }
        }

        /// <summary>
        /// If the field has MinTextDisplayWidthChars, this determines the minimum field width.
        /// </summary>
        public double MinFieldWidth
        {
            get
            {
                if (columnModel is GnosisTextColumn && ((GnosisTextColumn)columnModel).MinTextDisplayWidthChars > 0)
                {
                    double minWidth = ((GnosisTextColumn)columnModel).MinTextDisplayWidthChars * CharacterWidth + (2 * fieldClone.HorizontalPadding);
                    return minWidth;
                }
                else if (columnModel is GnosisTextResults && ((GnosisTextResults)columnModel).MinTextDisplayWidthChars > 0)
                {
                    double minWidth = ((GnosisTextResults)columnModel).MinTextDisplayWidthChars * CharacterWidth + (2 * fieldClone.HorizontalPadding);
                    return minWidth;

                }
                else if (MinDisplayChars > 0)
                {
                    //if (numLines > 1)
                    //{
                    //    double totalMinWidth = CharacterWidth * MinDisplayChars;
                    //    //padding on each line
                    //    double lineMinWidth = (totalMinWidth / NumLines) + (2 * fieldClone.HorizontalPadding);

                    //    return lineMinWidth;
                    //}
                    //else
                    //{
                    //    double minWidth = MinDisplayChars * CharacterWidth + (2 * fieldClone.HorizontalPadding);
                    //    return minWidth;
                    //}
                    return 2 * characterWidth + (2 * fieldClone.HorizontalPadding);
                }
                else
                {
                    return 0;
                }
                //else if (MaxChars > 0)
                //{
                //    // MinFieldWidth = styles.GetMinFieldWidth(MaxChars);
                //    MinFieldWidth = GlobalData.Singleton.StyleHelper.GetMinFieldWidth(gridField, font, fontSize, MaxChars);
                //}
            }
            //set { minFieldWidth = value; }
        }

        public double MaxFieldWidth
        {
            get
            {
                if (columnModel is GnosisTextColumn && ((GnosisTextColumn)columnModel).MaxTextDisplayWidthChars > 0)
                {
                    double maxWidth = ((GnosisTextColumn)columnModel).MaxTextDisplayWidthChars * CharacterWidth + (2 * fieldClone.HorizontalPadding);
                    return maxWidth;
                }
                else if (columnModel is GnosisTextResults && ((GnosisTextResults)columnModel).MaxTextDisplayWidthChars > 0)
                {
                    double maxWidth = ((GnosisTextResults)columnModel).MaxTextDisplayWidthChars * CharacterWidth + (2 * fieldClone.HorizontalPadding);
                    return maxWidth;
                }
                else if (MaxDisplayChars > 0)
                {
                    if (numLines > 1)
                    {
                        double totalMaxWidth = MaxDisplayChars * CharacterWidth;
                        double lineMaxWidth = totalMaxWidth / numLines + (2 * fieldClone.HorizontalPadding);

                        return lineMaxWidth;
                    }
                    else
                    {
                        double maxWidth = MaxDisplayChars * CharacterWidth + (2 * fieldClone.HorizontalPadding);
                        return maxWidth;

                    }
                }
                else if (MaxChars > 0)
                {
                    double maxWidth = MaxChars * CharacterWidth + (2 * fieldClone.HorizontalPadding);
                    return maxWidth;
                }
                else
                {
                    return 5000;
                }
            }
            // set { maxFieldWidth = value; }
        }

        public int MinDisplayChars
        {
            get { return columnModel.MinDisplayChars; }
            set { columnModel.MinDisplayChars = value; }
        }

        public int MaxDisplayChars
        {
            get { return columnModel.MaxDisplayChars; }
            set { columnModel.MaxDisplayChars = value; }
        }

        public double CharacterWidth
        {
            get { return characterWidth; }
            set { characterWidth = value; }
        }

        public double FieldHeight
        {
            get { return fieldHeight; }
        }

        public double TextHeight
        {
            get;set;
        }

        public int VerticalPadding
        {
            get
            {
                return fieldClone.VerticalPadding;
            }
        }

        public int HorizontalPadding
        {
            get { return fieldClone.HorizontalPadding; }
        }

        public int MaxChars
        {
            get { return columnModel.MaxChars; }
        }

        public int Order
        {
            get { return columnModel.Order; }
        }


        public bool IsEvenRow
        {
            get { return columnModel.IsEvenRow; }
            set { columnModel.IsEvenRow = value; }
        }

        public int NumLines
        {
            get { return numLines; }
            set { numLines = value; }
        }

        //public GnosisGridFieldController FieldController
        //{
        //    get { return fieldController; }
        //}

        public GnosisGridColumnController(
            GnosisGridColumn column,
            GnosisInstanceController _instanceController,
            GnosisGridController _parent)
        {
            columnModel = column;
            instanceController = _instanceController;
            parent = _parent;
            fields = new List<IGnosisGridFieldImplementation>();

            //header
            header = GnosisControlCreator.CreateGnosisGridHeaderField();
            GnosisStyle captionStyle = _instanceController.EntityController.GetCaptionStyle();
            GlobalData.Singleton.StyleHelper.ApplyCaptionStyle(header, captionStyle);
            double height = GlobalData.Singleton.StyleHelper.GetFieldHeight(header, captionStyle.Font, captionStyle.FontSize);
            ((GnosisGridHeaderField)header).Height = height;
            header.Caption = column.Caption;

            //columnar header
            columnarHeader = GnosisControlCreator.CreateGnosisCaptionLabel();
            GlobalData.Singleton.StyleHelper.ApplyCaptionStyle(columnarHeader, _instanceController.EntityController.GetCaptionStyle());

            SetDisplayWidths();

            ////field for cloning
            //if (column is GnosisTextColumn)
            //{
            //    // IGnosisGridTextFieldImplementation textFieldImp = GlobalData.Singleton.ImplementationCreator.GetGnosisGridTextFieldImplementation();
            //    ModelCreator.CreateGnosisGridTextField(1);
            //    fieldController = new GnosisGridTextFieldController(this, textFieldImp, instanceController, parent, 0);
            //    GlobalData.Singleton.StyleHelper.ApplyStyle(textFieldImp, fieldController, instanceController.EntityController.GetNormalStyle());
            //}
            //else if (column is GnosisTextResults)
            //{
            //    IGnosisTextResultsFieldImplementation textFieldImp = GlobalData.Singleton.ImplementationCreator.GetGnosisTextResultsFieldImplementation();
            //    fieldController = new GnosisTextResultsFieldController(this, textFieldImp, instanceController, (GnosisSearchResultsGridController)parent, 0);
            //    GlobalData.Singleton.StyleHelper.ApplyStyle(textFieldImp, fieldController, instanceController.EntityController.GetNormalStyle());
            //}
            //else if (column is GnosisCheckColumn)
            //{
            //    IGnosisGridCheckFieldImplementation checkFieldImp = GlobalData.Singleton.ImplementationCreator.GetGnosisGridCheckFieldImplementation();
            //    fieldController = new GnosisGridCheckFieldController(this, checkFieldImp, instanceController, parent, 0);
            //    GlobalData.Singleton.StyleHelper.ApplyStyle(checkFieldImp, fieldController, instanceController.EntityController.GetNormalStyle());
            //}
            //else if (column is GnosisCheckResults)
            //{
            //    IGnosisCheckResultsFieldImplementation checkFieldImp = GlobalData.Singleton.ImplementationCreator.GetGnosisCheckResultsFieldImplementation();
            //    fieldController = new GnosisCheckResultsFieldController(this, checkFieldImp, instanceController, (GnosisSearchResultsGridController)parent, 0);
            //}
            //else if (column is GnosisComboColumn)
            //{
            //    IGnosisGridComboFieldImplementation comboFieldImp = GlobalData.Singleton.ImplementationCreator.GetGnosisGridComboFieldImplementation();
            //    fieldController = new GnosisGridComboFieldController(this, comboFieldImp, instanceController, parent, 0);
            //}
            //else if (column is GnosisDateColumn)
            //{
            //    IGnosisGridDateFieldImplementation dateFieldImp = GlobalData.Singleton.ImplementationCreator.GetGnosisGridDateFieldImplementation();
            //    fieldController = new GnosisGridDateFieldController(this, dateFieldImp, instanceController, parent, 0);
            //}
            //else if (column is GnosisDateResults)
            //{
            //    IGnosisDateResultsFieldImplementation dateResultsImp = GlobalData.Singleton.ImplementationCreator.GetGnosisDateResultsFieldImplementation();
            //    fieldController = new GnosisDateResultsFieldController(this, dateResultsImp, instanceController, (GnosisSearchResultsGridController)parent, 0);
            //}
            //else if (column is GnosisDateTimeColumn)
            //{
            //    IGnosisGridDateTimeFieldImplementation dateTimeFieldImp = GlobalData.Singleton.ImplementationCreator.GetGnosisGridDateTimeFieldImplementation();
            //    fieldController = new GnosisGridDateTimeFieldController(this, dateTimeFieldImp, instanceController, parent, 0);
            //}
            //else if (column is GnosisDateTimeResults)
            //{
            //    IGnosisDateTimeResultsFieldImplementation dateTimeResultsImp = GlobalData.Singleton.ImplementationCreator.GetGnosisDateTimeResultsFieldImplementation();
            //    fieldController = new GnosisGridFieldController(this, dateTimeResultsImp, instanceController, (GnosisSearchResultsGridController)parent, 0);
            //}
            //fieldController.ControlImplementation.Order = column.Order;

            //get min and max field width

        }

        //public virtual IGnosisGridFieldImplementation GetFieldClone()
        //{
        //    IGnosisGridFieldImplementation gridFieldImp = fieldController.GetClone();
        //    gridFieldImp.Order = columnModel.Order;
        //    fields.Add(gridFieldImp);

        //    return gridFieldImp;
        //}

        public virtual IGnosisGridFieldImplementation GetFieldClone()
        {
            IGnosisGridFieldImplementation gridField = null;

            if (ColumnModel is GnosisTextColumn)
            {
                gridField = GnosisControlCreator.CreateGnosisGridTextField((GnosisTextColumn)ColumnModel);
            }
            else if (ColumnModel is GnosisCheckColumn)
            {
                gridField = GnosisControlCreator.CreateGnosisGridCheckField((GnosisCheckColumn)ColumnModel);
            }
            else if (ColumnModel is GnosisDateColumn)
            {
                gridField =  GnosisControlCreator.CreateGnosisGridDateField((GnosisDateColumn)ColumnModel);
            }
            else if (ColumnModel is GnosisDateTimeColumn)
            {
                gridField = GnosisControlCreator.CreateGnosisGridDateTimeField((GnosisDateTimeColumn)ColumnModel);
            }
            else if (ColumnModel is GnosisComboColumn)
            {
                gridField = GnosisControlCreator.CreateGnosisGridComboField((GnosisComboColumn)ColumnModel);
            }
            else if (ColumnModel is GnosisLinkColumn)
            {
                gridField = GnosisControlCreator.CreateGnosisGridLinkField((GnosisLinkColumn)ColumnModel);
            }
            else if (ColumnModel is GnosisNumberColumn)
            {
                gridField = GnosisControlCreator.CreateGnosisGridNumberField((GnosisNumberColumn)ColumnModel);
            }
            else if (ColumnModel is GnosisTextResults)
            {
                gridField = GnosisControlCreator.CreateGnosisResultsTextField((GnosisTextResults)ColumnModel);
            }
            else if (ColumnModel is GnosisCheckResults)
            {
                gridField = GnosisControlCreator.CreateGnosisResultsCheckField((GnosisCheckResults)ColumnModel);
            }
            else if (ColumnModel is GnosisDateResults)
            {
                gridField =  GnosisControlCreator.CreateGnosisResultsDateField((GnosisDateResults)ColumnModel);
            }
            else if (ColumnModel is GnosisDateTimeResults)
            {
                gridField =  GnosisControlCreator.CreateGnosisResultsDateTimeField((GnosisDateTimeResults)ColumnModel);
            }
            else if (ColumnModel is GnosisNumberResults)
            {
                gridField = GnosisControlCreator.CreateGnosisResultsNumberField((GnosisNumberResults)ColumnModel);
            }


            if (gridField != null)
            {
                GlobalData.Singleton.StyleHelper.ApplyStyle(gridField, instanceController.EntityController.GetNormalStyle());
                return gridField;
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown column type: " + ColumnModel.GetType().ToString(), "GnosisGridColumnController.GetFieldClone");
                return null;
            }
        }

        public virtual IGnosisCaptionLabelImplementation GetColumnarHeaderClone()
        {
            GnosisCaptionLabel captionLabel = new GnosisCaptionLabel();
            // GlobalData.Singleton.StyleHelper.CloneCaptionStyle(captionLabel, columnarHeader);
            GlobalData.Singleton.StyleHelper.ApplyCaptionStyle(captionLabel, instanceController.EntityController.GetCaptionStyle());
            captionLabel.Caption = columnModel.Caption;

            return captionLabel;
        }



        protected void SetDisplayWidths()
        {
            fieldClone = GetFieldClone();

            font = instanceController.EntityController.GetNormalStyle().Font;
            fontSize = instanceController.EntityController.GetNormalStyle().FontSize;
            characterWidth = GlobalData.Singleton.StyleHelper.GetCharacterWidth(fieldClone, font, fontSize);

            fieldHeight = GlobalData.Singleton.StyleHelper.GetFieldHeight(fieldClone, font, fontSize);


            //display chars
            if (MinDisplayChars == 0)
            {
                if (this.Dataset == null || this.DatasetItem == null)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("Dataset or DatasetItem not defined for " + columnModel.GnosisName + " " + ControlTypeMapping.GetControlTypeName(this.GetType()), "GnosisGridColumnController");
                }
                else
                {
                    MinDisplayChars = instanceController.EntityController.GetMinDisplayChars(Dataset, DatasetItem);
                }
            }

            if (MaxDisplayChars == 0)
            {
                if (MaxChars > 0)
                {
                    MaxDisplayChars = MaxChars;
                }
                else
                {
                    MaxDisplayChars = instanceController.EntityController.GetMaxDisplayChars(Dataset, DatasetItem);
                }
            }


            //max field width
            //if (columnModel is GnosisTextColumn && ((GnosisTextColumn)columnModel).MaxTextDisplayWidthChars > 0)
            //{
            //    //MaxFieldWidth = styles.GetMaxFieldWidth(((GnosisTextColumn)columnModel).MaxDisplayWidthChars);
            //    MaxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxFieldWidth(gridField, font, fontSize, ((GnosisTextColumn)columnModel).MaxDisplayChars);
            //}
            //else if (MaxDisplayChars > 0)
            //{
            //    //MaxFieldWidth = styles.GetMaxFieldWidth(MaxDisplayChars);
            //    MaxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxFieldWidth(gridField, font, fontSize, MaxDisplayChars);
            //}
            //else if (MaxChars > 0)
            //{
            //    //MaxFieldWidth = styles.GetMaxFieldWidth(MaxChars);
            //    MaxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxFieldWidth(gridField, font, fontSize, MaxChars);
            //}

            ////min field width
            //if (columnModel is GnosisTextColumn && ((GnosisTextColumn)columnModel).MinTextDisplayWidthChars > 0)
            //{
            //    MinFieldWidth = GlobalData.Singleton.StyleHelper.GetMinFieldWidth(gridField, font, fontSize, ((GnosisTextColumn)columnModel).MinTextDisplayWidthChars);
            //}
            //else if (columnModel is GnosisTextResults && ((GnosisTextResults)columnModel).MinTextDisplayWidthChars > 0)
            //{
            //    MinFieldWidth = GlobalData.Singleton.StyleHelper.GetMinFieldWidth(gridField, font, fontSize, ((GnosisTextResults)columnModel).MinTextDisplayWidthChars);

            //}
            //else if (MinDisplayChars > 0)
            //{
            //    //MinFieldWidth = styles.GetMinFieldWidth(MinDisplayChars);
            //    MinFieldWidth = GlobalData.Singleton.StyleHelper.GetMinFieldWidth(gridField, font, fontSize, MinDisplayChars);
            //}
            //else if (MaxChars > 0)
            //{
            //    // MinFieldWidth = styles.GetMinFieldWidth(MaxChars);
            //    MinFieldWidth = GlobalData.Singleton.StyleHelper.GetMinFieldWidth(gridField, font, fontSize, MaxChars);
            //}




            //if (MinFieldWidth > MaxFieldWidth)
            //{
            //    MinFieldWidth = MaxFieldWidth;
            //}
            //else if (MaxFieldWidth < MinFieldWidth)
            //{
            //    MaxFieldWidth = MinFieldWidth;
            //}

            if (columnModel._ContentHorizontalAlignment == GnosisController.HorizontalAlignmentType.NONE)
            {
                GnosisController.HorizontalAlignmentType ha = instanceController.EntityController.GetContentHorizontalAlignment(Dataset, DatasetItem);
                columnModel._ContentHorizontalAlignment = ha;
                header._ContentHorizontalAlignment  = ha;
            }

            if (fieldClone is GnosisGridTextField || fieldClone is GnosisResultsTextField)
            {
                TextHeight = GlobalData.Singleton.StyleHelper.GetTextHeight(
                    (GnosisGridTextField)fieldClone,
                    instanceController.EntityController.GetNormalStyle().Font,
                    instanceController.EntityController.GetNormalStyle().FontSize);

                //PaddingVertical = (int)(fieldHeight - TextHeight) / 2;
            }

        }


    }
}
