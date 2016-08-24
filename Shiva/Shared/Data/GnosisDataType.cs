using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisDataType : IGnosisObject
    {
        private string name;

        private bool isItemList;

        private string defaultField;

        private string errorMessage;

        private int minDisplayChars;

        private int maxDisplayChars;

        private int minTextDisplayWidthChars;

        private int maxTextDisplayWidthChars;

        private int maxChars;

        private GnosisController.CaptionPosition measureRelativePosition;

        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;

        private string sqlDataType;

        private string unitOfMeasure;

        private int precision;

        private XElement content;

        private List<GnosisDataItem> dataItems;

        [GnosisProperty]
        public string GnosisName
        {
            get { return name; }
            set { name = value; }
        }

        [GnosisProperty]
        public bool IsItemList
        {
            get { return isItemList; }
            set { isItemList = value; }
        }

        [GnosisProperty]
        public string Default
        {
            get { return defaultField; }
            set { defaultField = value; }
        }

        [GnosisProperty]
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        [GnosisProperty]
        public int MinDisplayChars
        {
            get { return minDisplayChars; }
            set { minDisplayChars = value; }
        }

        [GnosisProperty]
        public int MaxDisplayChars
        {
            get { return maxDisplayChars; }
            set { maxDisplayChars = value; }
        }

        [GnosisProperty]
        public int MaxChars
        {
            get { return maxChars; }
            set { maxChars = value; }
        }

        [GnosisProperty]
        public int MinTextDisplayWidthChars
        {
            get { return minTextDisplayWidthChars; }
            set { minTextDisplayWidthChars = value; }
        }

        [GnosisProperty]
        public int MaxTextDisplayWidthChars
        {
            get { return maxTextDisplayWidthChars; }
            set { maxTextDisplayWidthChars = value; }
        }

        [GnosisProperty]
        public string MeasureRelativePosition
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.CaptionPosition), measureRelativePosition);
            }
            set
            {
                try
                {
                    measureRelativePosition = (GnosisController.CaptionPosition)Enum.Parse(typeof(GnosisController.CaptionPosition), value.ToUpper());

                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.CaptionPosition _MeasureRelativePosition
        {
            get { return measureRelativePosition; }
            set { measureRelativePosition = value; }
        }

        [GnosisProperty]
        public int Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        [GnosisProperty]
        public string ContentHorizontalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), contentHorizontalAlignment);
            }
            set
            {
                try
                {
                    contentHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get { return contentHorizontalAlignment; }
        }


        [GnosisProperty]
        public string SqlDataType
        {
            get { return sqlDataType; }
            set { sqlDataType = value; }
        }

        [GnosisProperty]
        public string UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set { unitOfMeasure = value; }
        }

        
        public XElement Content
        {
            get { return content; }
            set { content = value; }
        }

        [GnosisCollection]
        public List<GnosisDataItem> DataItems
        {
            get { return dataItems; }
            set { dataItems = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisDataItem)
            {
                if (dataItems == null)
                {
                    dataItems = new List<GnosisDataItem>();
                }
                dataItems.Add((GnosisDataItem)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown object added to GnosisDataType: " + child.GetType().Name,
                    "GnosisDataType.GnosisAddChild");
            }

        }
    }
}
