using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.Data;
using GnosisControls;
using Shiva.Shared.Events;
using Shiva.Shared.BaseControllers;

namespace GnosisControls
{
    public class GnosisContentControl : GnosisVisibleControl
    {
        protected GnosisController.VerticalAlignmentType contentVerticalAlignment;

        protected GnosisController.HorizontalAlignmentType contentHorizontalAlignment;

        //protected string defaultField;

        //protected bool lockedField;

        //protected bool lockIgnoreProtectionField;

        //protected bool readOnly;

        protected int colSpan;

        protected bool datasetCreated;

        protected bool datasetUpdated;

        protected bool datasetDeleted;

        //protected string typeField;

        //protected bool disabled;

        //protected int textLength = 0;

        protected int maxCharacters;

        protected string dataset;

        protected string datasetItem;

        protected List<GnosisEventHandler> eventHandlers;

        private int maxDisplayCharacters;

        private int minDisplayCharacters;

        [GnosisProperty]
        public int MaxDisplayChars
        {
            get { return maxDisplayCharacters; }
            set { maxDisplayCharacters = value; }
        }

        [GnosisProperty]
        public int MinDisplayChars
        {
            get { return minDisplayCharacters; }
            set { minDisplayCharacters = value; }
        }

        [GnosisProperty]
        public string ContentVerticalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), contentVerticalAlignment);
            }
            set
            {
                try
                {
                    contentVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
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


        public GnosisController.VerticalAlignmentType _ContentVerticalAlignment
        {
            get { return contentVerticalAlignment; }
            set { contentVerticalAlignment = value; }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get { return contentHorizontalAlignment; }
            set { contentHorizontalAlignment = value; }
        }


        //[System.Xml.Serialization.XmlAttributeAttribute]
        //public string Default
        //{
        //    get { return defaultField; }
        //    set { defaultField = value; }
        //}

        [GnosisProperty]
        public bool DatasetCreated
        {
            get { return datasetCreated; }
            set { datasetCreated = value; }
        }

        [GnosisProperty]
        public bool DatasetUpdated
        {
            get { return datasetUpdated; }
            set { datasetUpdated = value; }
        }

        [GnosisProperty]
        public bool DatasetDeleted
        {
            get { return datasetDeleted; }
            set { datasetDeleted = value; }
        }


        [GnosisProperty]
        public int ColSpan
        {
            get { return colSpan; }
            set { colSpan = value; }
        }

        //[System.Xml.Serialization.XmlAttributeAttribute]
        //public int MaxLines
        //{
        //    get { return maxLinesField; }
        //    set { maxLinesField = value; }
        //}

        //[System.Xml.Serialization.XmlAttributeAttribute]
        //public int Prec
        //{
        //    get { return precisionField; }
        //    set { precisionField = value; }
        //}

        //[System.Xml.Serialization.XmlAttribute]
        //public int TextLength
        //{
        //    get { return textLength; }
        //    set { textLength = value; }
        //}

        //[System.Xml.Serialization.XmlAttributeAttribute]
        //public int Scale
        //{
        //    get { return scaleField; }
        //    set { scaleField = value; }
        //}

        //[System.Xml.Serialization.XmlAttributeAttribute]
        //public string SQLType
        //{
        //    get { return SQLTypeField; }
        //    set { SQLTypeField = value; }
        //}

        //[System.Xml.Serialization.XmlAttributeAttribute]
        //public string Type
        //{
        //    get { return typeField; }
        //    set { typeField = value; }
        //}

        //[System.Xml.Serialization.XmlAttribute]
        //public bool Disabled
        //{
        //    get { return disabled; }
        //    set { disabled = value; }
        //}


        [GnosisProperty]
        public int MaxChars
        {
            get { return maxCharacters; }
            set { maxCharacters = value; }
        }

        [GnosisProperty]
        public string Dataset
        {
            get { return dataset; }
            set { dataset = value; }
        }

        [GnosisProperty]
        public string DatasetItem
        {
            get { return datasetItem; }
            set { datasetItem = value; }
        }


        [GnosisCollection]
        public List<GnosisEventHandler> EventHandlers
        {
            get { return eventHandlers; }
            set { eventHandlers = value; }
        }

    }
}
