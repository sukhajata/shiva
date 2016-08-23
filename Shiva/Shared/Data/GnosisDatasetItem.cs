using System;
using System.Collections.Generic;
using ShivaShared3.BaseControllers;
using ShivaShared3.Interfaces;
using ShivaShared3.Data;

namespace GnosisControls
{
    public class GnosisDatasetItem : IGnosisObject
    {
        private string name;
        private string attribute;
        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
        private int order;
        private string column;
        private string gnosisDefault;
        private string gnosisObject;
        private string schema;
        private string database;
        private string dataType;
        private bool isInstanceName;
        private bool isItemList;
        private bool isSearchInput;
        private int minDisplayChars;
        private int maxDisplayChars;
        private int minTextDisplayWithChars;
        private int maxTextDisplayWidthChars;
        private int maxCharacters;
        private GnosisController.CaptionPosition measureRelativePosition;
        private bool optional;
        private int outputFieldOrder;
        private string outputOrdering;
        private SQLDataType sqlDataType;
        private string unitOfMeasure;

        private List<GnosisControlReference> controlReferences;
        private List<GnosisDataCache> dataCaches;

        public enum SQLDataType
        {
            BIGINT,
            BINARY,
            BIT,
            DATETIME,
            INT,
            NCHAR,
            NUMERIC,
            NVARCHAR,
            SMALLINT,
            SYSNAME,
            TINYINT,
            UNIQUEIDENTIFIER,
            VARBINARY,
            VARCHAR,
            XML
        }

        [GnosisProperty]
        public string GnosisName
        {
            get { return name; }
            set { name = value; }
        }

        [GnosisProperty]
        public string Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }

        [GnosisProperty]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        [GnosisProperty]
        public string Column
        {
            get { return column; }
            set { column = value; }
        }

        [GnosisProperty]
        public string GnosisObject
        {
            get { return gnosisObject; }
            set { gnosisObject = value; }
        }

        [GnosisProperty]
        public string Default
        {
            get { return gnosisDefault; }
            set { gnosisDefault = value; }
        }

        [GnosisProperty]
        public bool IsSearchInput
        {
            get { return isSearchInput; }
            set { isSearchInput = value; }
        }

        [GnosisProperty]
        public string Schema
        {
            get { return schema; }
            set { schema = value; }
        }

        [GnosisProperty]
        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        [GnosisProperty]
        public string SqlDataType
        {
            get
            {
                return Enum.GetName(typeof(SQLDataType), sqlDataType).ToLower();
            }
            set
            {
                try
                {
                    sqlDataType = (SQLDataType)Enum.Parse(typeof(SQLDataType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        //[System.Xml.Serialization.XmlIgnore]
        //public SQLDataType SqlDataType
        //{
        //    get { return sqlDataType; }
        //    set { sqlDataType = value; }
        //}

        [GnosisProperty]
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        [GnosisProperty]
        public bool IsInstanceName
        {
            get { return isInstanceName; }
            set { isInstanceName = value; }
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
        public int MinTextDisplayWidthChars
        {
            get { return minTextDisplayWithChars; }
            set { minTextDisplayWithChars = value; }
        }

        [GnosisProperty]
        public int MaxTextDisplayWidthChars
        {
            get { return maxTextDisplayWidthChars; }
            set { maxTextDisplayWidthChars = value; }
        }

        [GnosisProperty]
        public int MaxChars
        {
            get { return maxCharacters; }
            set { maxCharacters = value; }
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
                catch(Exception ex)
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
        public int OutputFieldOrder
        {
            get { return outputFieldOrder; }
            set { outputFieldOrder = value; }
        }

        [GnosisProperty]
        public string OutputOrdering
        {
            get { return outputOrdering; }
            set { outputOrdering = value; }
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

        public GnosisController.HorizontalAlignmentType ContentHorizontalAlignmentType
        {
            get { return contentHorizontalAlignment; }
        }

        [GnosisCollection]
        public List<GnosisControlReference> ControlReferences
        {
            get { return controlReferences; }
            set { controlReferences = value; }
        }

        [GnosisProperty]
        public bool Optional
        {
            get { return optional; }
            set { optional = value; }
        }

        [GnosisProperty]
        public bool IsItemList
        {
            get { return isItemList; }
            set { isItemList = value; }
        }

        [GnosisProperty]
        public string UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set { unitOfMeasure = value; }
        }

        [GnosisCollection]
        public List<GnosisDataCache> DataCaches
        {
            get { return dataCaches; }
            set { dataCaches = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisControlReference)
            {
                if (controlReferences == null)
                {
                    controlReferences = new List<GnosisControlReference>();
                }
                controlReferences.Add((GnosisControlReference)child);
            }
            else if (child is GnosisDataCache)
            {
                if (dataCaches == null)
                {
                    dataCaches = new List<GnosisDataCache>();
                }
                dataCaches.Add((GnosisDataCache)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisDatasetItem", child.GetType().Name);
            }
        }
    }
}
