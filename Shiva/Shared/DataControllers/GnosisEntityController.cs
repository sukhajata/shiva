using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;

using ShivaShared3.Events;
using ShivaShared3.Data;
using GnosisControls;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.DataControllers
{
    public class GnosisEntityController 
    {
        protected GnosisEntity entity;
        protected XElement xEntity;
        protected Stack<GnosisDocChange> undoStack;
        protected Stack<GnosisDocChange> redoStack;
        //protected GnosisInstance instance;
        protected GnosisStyle normalStyle;
        protected GnosisStyle captionStyle;

        //protected GnosisVisibleController visibleController;

        //protected GnosisDocFrameController docFrameController;
        //protected GnosisConnectionFrameController connectionFrameController;
        //protected GnosisNavFrameController navFrameController;
        //protected GnosisParentWindowController parentWindowController;
        //protected GnosisSearchFrameController searchFrameController;


        public int EntityID
        {
            get { return entity.EntityID; }
        }

        public int SystemID
        {
            get { return entity.SystemID; }
        }

        public GnosisEntity Entity
        {
            get { return entity; }
        }

        public string EntityName
        {
            get { return entity.Entity; }
        }

        public GnosisEntity.EntityTypeEnum EntityType
        {
            get { return entity.GnosisEntityType; }
        }

        //public GnosisInstance Instance
        //{
        //    get { return instance; }
        //    set
        //    {
        //        instance = value;
        //    }
        //}

        //public GnosisVisibleController VisibleController
        //{
        //    get { return visibleController; }
        //}

        public GnosisEntityController(GnosisEntity _entity, XElement _xEntity) 
        {
            entity = _entity;
            xEntity = _xEntity;

            undoStack = new Stack<GnosisDocChange>();
            redoStack = new Stack<GnosisDocChange>();

            LoadDataDefinitions();
        }


        public virtual GnosisInstanceController CreateInstanceController(GnosisInstance instance)
        {
            GnosisInstanceController instanceController = new GnosisInstanceController(instance, this);

            return instanceController;

            //if (entity.ParentWindow != null)
            //{
            //    IGnosisParentWindowImplementation parentWindowImplementation = GlobalData.Singleton.ParentWindowImplementation;
            //    visibleController = new GnosisParentWindowController(entity.ParentWindow, parentWindowImplementation);
            //    ((GnosisParentWindowController)visibleController).Setup();
            //}
            //if (entity.DocumentFrame != null)
            //{
            //    GnosisTileController firstTile = GlobalData.Singleton.PrimarySplitController.GetFirstTile();

            //    IGnosisDocFrameImplementation docFrameImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisDocFrameImplementation();
            //    visibleController = new GnosisDocFrameController(entity.DocumentFrame, docFrameImplementation, instanceController, firstTile);
            //    ((GnosisDocFrameController)visibleController).Setup();

            //  //  firstTile.LoadFrame(docFrameController);
            //}
            //else if (entity.ConnectionFrame != null)
            //{
            //    IGnosisConnectionFrameImplementation connectionFrameImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisConnectionFrameImplementation();
            //    visibleController = new GnosisConnectionFrameController(entity.ConnectionFrame, connectionFrameImplementation, instanceController, null);
            //    ((GnosisConnectionFrameController)visibleController).Setup();
               
            //}
            //else if (entity.NavigatorFrame != null)
            //{
            //    IGnosisNavFrameImplementation navFrameImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisNavFrameImplementation();
            //    visibleController = new GnosisNavFrameController(entity.NavigatorFrame, navFrameImplementation, instanceController, GlobalData.Singleton.PrimarySplitController.NavTileController);
            //    ((GnosisNavFrameController)visibleController).Setup();
                
            //    GlobalData.Singleton.PrimarySplitController.AddNavFrame((GnosisNavFrameController)visibleController);
            //}
            //else if (entity.SearchFrame != null)
            //{
            //    GnosisTileController firstTile = GlobalData.Singleton.PrimarySplitController.GetFirstTile();

            //    IGnosisSearchFrameImplementation searchFrameImp = GlobalData.Singleton.ImplementationCreator.GetGnosisSearchFrameImplementation();
            //    visibleController = new GnosisSearchFrameController(entity.SearchFrame, searchFrameImp, instanceController, firstTile);
            //    ((GnosisSearchFrameController)visibleController).Setup();

            //   // firstTile.LoadFrame(searchFrameController);
            //}

            //else if (entity.SystemDefinitions.Count > 0)
            //{
            //    foreach (GnosisGenericControl genericControl in entity.SystemDefinitions.First().GenericControls)
            //    {
            //        GnosisGenericControlController genericControlController = new GnosisGenericControlController(genericControl);
            //        genericControlControllers.Add(genericControlController);
            //    }
            //    foreach (GnosisGenericMenu genericMenu in entity.SystemDefinitions.First().GenericMenus)
            //    {
            //        GnosisGenericMenuController genericMenuController = new GnosisGenericMenuController(genericMenu);
            //        genericMenuControllers.Add(genericMenuController);
            //    }
            //}
            
        }

        internal GnosisController.HorizontalAlignmentType GetContentHorizontalAlignment(string datasetName, string datasetItemName)
        {
            GnosisController.HorizontalAlignmentType alignment = GnosisController.HorizontalAlignmentType.NONE;

            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);

                if (dataset == null)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("dataset not found: " + datasetName + " in entity controller", "GnosisEntityController GetContentHorizontalAlignment");
                    return GnosisController.HorizontalAlignmentType.NONE;
                }

                GnosisDatasetItem datasetItem = dataset.DatasetItems.Where(x => x.GnosisName.Equals(datasetItemName)).First();

                if (datasetItem.ContentHorizontalAlignmentType != GnosisController.HorizontalAlignmentType.NONE)
                {
                    alignment = datasetItem.ContentHorizontalAlignmentType;
                }
                else if (datasetItem.DataType != null)
                {
                    GnosisDataType dataType = entity.DataDefinition.DataTypes.Find(d => d.GnosisName.Equals(datasetItem.DataType));

                    if (dataType._ContentHorizontalAlignment != GnosisController.HorizontalAlignmentType.NONE)
                    {
                        alignment = dataType._ContentHorizontalAlignment;
                    }
                }
                else
                {
                    alignment = GlobalData.Singleton.SystemController.GetContentHorizontalAlignment(datasetItem.SqlDataType);
                }

            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return alignment;
        }

        internal int GetMaxChars(string datasetName, string datasetItemName)
        {
            int maxChars = 0;

            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);

                if (dataset == null)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("dataset not found: " + datasetName + " in entity controller", "GnosisEntityController.GetMaxChars");
                    return 0; ;
                }

                GnosisDatasetItem datasetItem = dataset.DatasetItems.Where(x => x.GnosisName.Equals(datasetItemName)).First();

                if (datasetItem.MaxChars > 0)
                {
                    maxChars = datasetItem.MaxChars;
                }
                else if (datasetItem.DataType != null)
                {
                    GnosisDataType dataType = entity.DataDefinition.DataTypes.Find(d => d.GnosisName.Equals(datasetItem.DataType));

                    if (dataType.MaxChars > 0)
                    {
                        maxChars = dataType.MaxChars;
                        
                    }
                }


                if (maxChars == 0)
                {
                    maxChars = GlobalData.Singleton.SystemController.GetMaxChars(datasetItem.SqlDataType);
                }


            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return maxChars;

        }

       

        internal int GetMinDisplayChars(string datasetName, string datasetItemName)
        {
            int minDisplayChars = 0;

            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);

                if (dataset == null)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("dataset not found: " + datasetName + " in entity controller", "GnosisEntityController GetMinDisplayChars");
                    return 0;
                }
                GnosisDatasetItem datasetItem = dataset.DatasetItems.Where(x => x.GnosisName.Equals(datasetItemName)).First();

                if (datasetItem.MinDisplayChars > 0)
                {
                    minDisplayChars = datasetItem.MinDisplayChars;
                }
                else if (datasetItem.MaxChars > 0)
                {
                    minDisplayChars = datasetItem.MaxChars;
                }
                else if (datasetItem.DataType != null)
                {
                    GnosisDataType dataType = entity.DataDefinition.DataTypes.Find(d => d.GnosisName.Equals(datasetItem.DataType));

                    if (dataType.MinDisplayChars == 0)
                    {
                        if (dataType.MaxChars == 0)
                        {
                            GlobalData.Singleton.ErrorHandler.HandleError("MinDisplayChars and MaxCharacters not set for datatype " + dataType.GnosisName, "GnosisEntityController GetMinDisplayChars");

                        }
                        else
                        {
                            minDisplayChars = dataType.MaxChars;
                        }
                    }
                    else
                    {
                        minDisplayChars = dataType.MinDisplayChars;
                    }
                }
                else
                {
                    minDisplayChars = GlobalData.Singleton.SystemController.GetMinDisplayChars(datasetItem.SqlDataType);
                }


            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return minDisplayChars;
        }



        internal int GetMaxDisplayChars(string datasetName, string datasetItemName)
        {
            int maxDisplayChars = 0;

            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);

                if (dataset == null)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("dataset not found: " + datasetName + " in entity controller", "GnosisEntityController GetMinDisplayChars");
                    return 0;
                }
                GnosisDatasetItem datasetItem = dataset.DatasetItems.Where(x => x.GnosisName.Equals(datasetItemName)).First();

                if (datasetItem.MaxDisplayChars > 0)
                {
                    maxDisplayChars = datasetItem.MaxDisplayChars;
                }
                else if (datasetItem.MaxChars > 0)
                {
                    maxDisplayChars = datasetItem.MaxChars;
                }
                else if (datasetItem.DataType != null)
                {
                    GnosisDataType dataType = entity.DataDefinition.DataTypes.Find(d => d.GnosisName.Equals(datasetItem.DataType));

                    if (dataType.MaxDisplayChars == 0)
                    {
                        if (dataType.MaxChars > 0)
                        {
                            maxDisplayChars = dataType.MaxChars;
                        }
                        else
                        {
                            GlobalData.Singleton.ErrorHandler.HandleError("MaxDisplayChars and MaxCharacters not set for datatype " + dataType.GnosisName, "GnosisEntityController GetMinDisplayChars");
                        }
                    }
                    else
                    {
                        maxDisplayChars = dataType.MaxDisplayChars;
                    }
                }
                else
                {
                    maxDisplayChars = GlobalData.Singleton.SystemController.GetMaxDisplayChars(datasetItem.SqlDataType);
                }


            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return maxDisplayChars;

        }


        private void LoadDataDefinitions()
        {

            //Store the xml of DataCaches and DataTypes within the respective objects since the content is not predefined.
            //Store the xml content of SystemDefinition since there may be new variables which are not predefined

            if (entity.DataDefinition != null)
            {
                var xData = xEntity.Descendants("GnosisDataDefinition")
                                              .First();

                if (entity.DataDefinition.DataCaches != null && entity.DataDefinition.DataCaches.Count > 0)
                {
                    foreach (GnosisDataCache dataCache in entity.DataDefinition.DataCaches)
                    {
                        var xDataCache = xData.Descendants("GnosisDataCache")
                                                               .Where(x => x.Attribute("Element").Value.Equals(dataCache.Element))
                                                               .First();
                        dataCache.Content = xDataCache;

                    }

                }

                if (entity.DataDefinition.DataTypes != null && entity.DataDefinition.DataTypes.Count > 0)
                {
                    foreach (GnosisDataType dataType in entity.DataDefinition.DataTypes)
                    {
                        var xDataType = xData.Descendants("GnosisDataType")
                                                            .Where(x => x.Attribute("Name").Value.Equals(dataType.GnosisName))
                                                            .First();
                        dataType.Content = xDataType;
                    }

                }

            }

        }


        //public void SetInstance(GnosisInstance _instance)
        //{
        //    instance = _instance;
        //}

        public void SetStyles(GnosisStyle _normalStyle, GnosisStyle _captionStyle)
        {
            normalStyle = _normalStyle;
            captionStyle = _captionStyle;
        }

        public GnosisStyle GetNormalStyle()
        {
            return normalStyle;
        }

        public GnosisStyle GetCaptionStyle()
        {
            return captionStyle;
        }

        //public IGnosisFrameImplementation GetFrame()
        //{
        //    if (entity.EntityType == GnosisEntity.EntityTypeEnum.Connection)
        //    {
        //        return (IGnosisFrameImplementation)visibleController.ControlImplementation;
        //    }
        //    else if (entity.EntityType == GnosisEntity.EntityTypeEnum.Document)
        //    {
        //        return (IGnosisFrameImplementation)visibleController.ControlImplementation;
        //    }
        //    else if (entity.EntityType == GnosisEntity.EntityTypeEnum.Navigator)
        //    {
        //        return (IGnosisFrameImplementation)visibleController.ControlImplementation;
        //    }
        //    else if (entity.EntityType == GnosisEntity.EntityTypeEnum.Search)
        //    {
        //        return (IGnosisFrameImplementation)visibleController.ControlImplementation;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        internal void SetDataUpdated(string datasetName, int rowNo, GnosisInstance instance)
        {
            string attributeName = "_Updated";
;            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);
                string elementName = dataset.Element;

                if (instance.Content.Descendants(elementName).Count() > 0)
                {
                    var dataItem = instance.Content.Descendants(elementName).ElementAt(rowNo);

                    if (dataItem.Attribute(attributeName) == null)
                    {
                        XAttribute att = new XAttribute(attributeName, "1");
                        dataItem.Add(att);
                    }
                    else
                    {
                        dataItem.Attribute(attributeName).Value = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }
        }

        internal void PutDataString(string datasetName, string datasetItemName, GnosisInstance instance, int rowNo, string value)
        {
            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);
                GnosisDatasetItem datasetItem = dataset.DatasetItems.Where(x => x.GnosisName.Equals(datasetItemName)).First();
                string elementName = dataset.Element;
                string attributeName = datasetItem.Attribute;

                if (instance.Content.Descendants(elementName).Count() > 0)
                {
                    var dataItem = instance.Content.Descendants(elementName).ElementAt(rowNo);
                    if (dataItem.Attribute(attributeName) != null)
                    {
                        dataItem.Attribute(attributeName).Value = value;
                    }
                    else
                    {
                        XAttribute att = new XAttribute(attributeName, value);
                        dataItem.Add(att);
                    }
                }

            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

        }


        public XElement GetOptionsXML(string datasetName, string datasetItemName)
        {
            var dataset = entity.DatasetDefinitions
                                          .Where(x => x.GnosisName.Equals(datasetName))
                                          .First();
            var datasetItem1 = dataset.DatasetItems
                                                        .Where(x => x.GnosisName.Equals(datasetItemName))
                                                        .First();
            string attributeName1 = datasetItem1.Attribute;

            //Case 1: IsItemList=1. List is found under DataType
            if (datasetItem1.IsItemList)
            {
                var dataType = entity.DataDefinition
                                            .DataTypes
                                            .Where(x => x.GnosisName.Equals(datasetItem1.DataType))
                                            .First();

                
                return dataType.Content;

            }
            //Case 2: Find the DataCache referenced by the DatasetItem
            else
            {
                //There are two DataCache elements. One in the DatasetItem and one in the Data section of the entity.
                //The actual data is stored in the latter. The first just contains a reference to the second.
                var datasetItemDataCache = datasetItem1.DataCaches.First();

                var actualDataCache = entity.DataDefinition
                                                                .DataCaches
                                                                .Where(x => x.Element.Equals(datasetItemDataCache.Element))
                                                                .First();

                string elementName = actualDataCache.Element;

                return  actualDataCache.Content;
            }
        }

        /// <summary>
        /// Get a list of options from a dataset.
        /// </summary>
        /// <param name="datasetName"></param>
        /// <param name="datasetItemName"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public List<string> GetOptionsList(string datasetName, string datasetItemName)
        {
            List<string> values = new List<string>();
            var dataset = entity.DatasetDefinitions
                                            .Where(x => x.GnosisName.Equals(datasetName))
                                            .First();
            var datasetItem1 = dataset.DatasetItems
                                                        .Where(x => x.GnosisName.Equals(datasetItemName))
                                                        .First();
            string attributeName1 = datasetItem1.Attribute;

            //Case 1: IsItemList=1. List is found under DataType
            if (datasetItem1.IsItemList)
            {
                var dataType = entity.DataDefinition
                                            .DataTypes
                                            .Where(x => x.GnosisName.Equals(datasetItem1.DataType))
                                            .First();

                //Since the content of the DataType element is not predefined, we deal with raw XML
                var items = dataType.Content.Descendants();

                foreach (var item in items)
                {
                    string value = item.Attribute("Name").Value;
                    values.Add(value);
                }

            }
            //Case 2: Find the DataCache referenced by the DatasetItem
            else
            {
                //There are two DataCache elements. One in the DatasetItem and one in the Data section of the entity.
                //The actual data is stored in the latter. The first just contains a reference to the second.
                var datasetItemDataCache = datasetItem1.DataCaches.First();
                var actualDataCache = entity.DataDefinition
                                                                .DataCaches
                                                                .Where(x => x.Element.Equals(datasetItemDataCache.Element))
                                                                .First();

                string elementName = actualDataCache.Element;

                //Since the content of the datacache is not predefined, we deal with the raw xml
                var items = actualDataCache.Content.Descendants(elementName);
                foreach (var item in items)
                {
                    string value = item.Attribute(datasetItemDataCache.Attribute).Value;
                    values.Add(value);
                }
            }

            return values;
        }

        //internal void LoadData()
        //{
        //    if (entity.EntityType == GnosisEntity.EntityTypeEnum.Connection)
        //    {
        //        ((GnosisConnectionFrameController)visibleController).LoadData();
        //    }
        //    else if (entity.EntityType == GnosisEntity.EntityTypeEnum.Document)
        //    {
        //        ((GnosisDocFrameController)visibleController).LoadData();
        //    }
        //    else if (entity.EntityType == GnosisEntity.EntityTypeEnum.Navigator)
        //    {
        //        ((GnosisNavFrameController)visibleController).LoadData();
        //    }
        //    else if (entity.EntityType == GnosisEntity.EntityTypeEnum.Search)
        //    {
        //        ((GnosisSearchFrameController)visibleController).LoadData();
        //    }


        //}

        //public void LoadNew(List<GnosisSearchParameter> searchParameters, int newEntityID)
        //{
           

        //}

        //public void LoadSearch(List<GnosisSearchParameter> searchParameters, string autoSearchAction)
        //{
        //    //The search parameters passed in map to search parameters owned by this entity
        //    //Search parameters passed in contain values in the 'content' attribute
        //    //Search parameters belonging to this entity contain references to the local dataset which describe where to put those values
        //    //Create a new instance and put in the values
        //    //Once the search frame is loaded it will look for the values
        //    if (EntityType == GnosisEntity.EntityTypeEnum.Search)
        //    {
        //        string datasetName = ((GnosisSearchFrame)visibleController.ControlImplementation).SearchParameters.First().Dataset;
        //        string elementName = GetElementName(datasetName);
        //        XElement xContent = new XElement(elementName);

        //        List<GnosisSearchParameter> localSearchParameters = ((GnosisSearchFrame)visibleController.ControlImplementation).SearchParameters;

        //        for (int i = 0; i < localSearchParameters.Count; i++)
        //        {
        //            string attributeName = GetAttributeName(localSearchParameters[i].Dataset, localSearchParameters[i].DatasetItem);
        //            XAttribute attribute = new XAttribute(attributeName, searchParameters[i].Content);
        //            xContent.Add(attribute);
        //        }

        //        //assign to the instance of this entity
        //        instance = GlobalData.Singleton.SystemController.GetInstance(EntityID, SystemID, "Search", xContent);

        //        if (autoSearchAction != null && autoSearchAction.Equals("Search"))
        //        {
        //            ((GnosisSearchFrameController)visibleController).SetAutoSearch(true);
        //        }

        //        GlobalData.Singleton.PrimarySplitController.GetFirstTile().AddFrame((GnosisSearchFrameController)visibleController);

        //    }
        //}

        public string GetElementName(string datasetName)
        {

            var dataset = GetDataset(entity.DatasetDefinitions, datasetName);
            return dataset.Element;

        }

        public GnosisDatasetDefinition GetDataset(List<GnosisDatasetDefinition> datasets, string datasetName)
        {
            GnosisDatasetDefinition ds = null;

            if (datasets == null)
            {
                int i = 1;
            }

            if (datasets.Where(x => x.GnosisName.Equals(datasetName)).Count() > 0)
            {
                ds = datasets.Where(x => x.GnosisName.Equals(datasetName)).First();
            }
            else
            {
                foreach (GnosisDatasetDefinition dataset in datasets)
                {
                    ds = GetDataset(dataset.DatasetDefinitions, datasetName);
                    
                    if (ds != null)
                    {
                        break;
                    }
                }
            }

            return ds;
        }

        public IEnumerable<XElement> GetDataRows(string datasetName, GnosisInstance instance)
        {
            IEnumerable<XElement> rows = null;

            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);
               // GnosisDatasetItem datasetItem = dataset.DatasetItems.Where(x => x.GnosisName.Equals(datasetItemName)).First();
                string elementName = dataset.Element;

                rows = instance.Content.Descendants(elementName);
                                                
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return rows;

        }

        public bool GetDatasetItemOptional(string datasetName, string datasetItemName)
        {
            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);
                GnosisDatasetItem datasetItem = dataset.DatasetItems.Find(x => x.GnosisName.Equals(datasetItemName));
                return datasetItem.Optional;
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return false;
        }

        public string GetDataString(string datasetName, string datasetItemName, GnosisInstance instance, int rowNo)
        {
            string attributeValue = "";

            try
            {
                GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);
                GnosisDatasetItem datasetItem = dataset.DatasetItems.Where(x => x.GnosisName.Equals(datasetItemName)).First();
                string elementName = dataset.Element;
                string attributeName = datasetItem.Attribute;

                if (instance.Content.Descendants(elementName).Count() > 0)
                {
                    var dataItem = instance.Content.Descendants(elementName).ElementAt(rowNo);
                    if (dataItem.Attribute(attributeName) != null)
                    {
                        attributeValue = dataItem.Attribute(attributeName).Value;
                    }
                }
               
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return attributeValue;
                       
        }

        internal double GetDataDouble(string datasetName, string datasetItemName, GnosisInstance instance, int rowNo)
        {
            string txt = GetDataString(datasetName, datasetItemName, instance, rowNo);
            double result = 0;

            try
            {
                result = Convert.ToDouble(txt);
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return result;
        }

        public DateTime GetDataDateTime(string datasetName, string datasetItemName, GnosisInstance instance, int rowNo)
        {
            string dateString = GetDataString(datasetName, datasetItemName, instance, rowNo);
            DateTime dateTime = new DateTime();
            
            try
            {
                dateTime = DateTime.Parse(dateString);

            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return dateTime;
        }

        internal void PutDataDateTime(string datasetName, string datasetItemName, GnosisInstance instance, int rowNo, DateTime value)
        {
            string dateTimeString = value.ToString("yyyy-MM-dd HH:mm:ss");

            PutDataString(datasetName, datasetItemName, instance, rowNo, dateTimeString);
        }


        public bool GetDataBool(string datasetName, string datasetItemName, GnosisInstance instance, int rowNo)
        {
            bool result = false;

            string strResult = GetDataString(datasetName, datasetItemName, instance, rowNo);
            if (strResult.Equals("1"))
            {
                result = true;
            }

            return result;

        }


        public string GetTargetAttributeName(string datasetName, string datasetItemName)
        {
            GnosisDatasetDefinition dataset = GetDataset(entity.DatasetDefinitions, datasetName);
            GnosisDatasetItem datasetItem = dataset.DatasetItems.Where(x => x.GnosisName.Equals(datasetItemName)).First();
            return datasetItem.Attribute;

        }

        internal string GetSourceAttributeName(string datasetName, string datasetItemName)
        {
            var dataset = entity.DatasetDefinitions
                                    .Where(x => x.GnosisName.Equals(datasetName))
                                    .First();
            var datasetItem = dataset.DatasetItems
                                                        .Where(x => x.GnosisName.Equals(datasetItemName))
                                                        .First();
            string attributeName = datasetItem.Attribute;

            //Case 1: IsItemList=1. List is found under DataType
            if (datasetItem.IsItemList)
            {
                return "Name";

            }
            //Case 2: Find the DataCache referenced by the DatasetItem
            else
            {
                //There are two DataCache elements. One in the DatasetItem and one in the Data section of the entity.
                //The actual data is stored in the latter. The first just contains a reference to the second.
                return datasetItem.DataCaches.First().Attribute;

            }
        }

        public string GetKeyOptionSourceAttributeName(string datasetName, string datasetItemName)
        {
            var dataset = entity.DatasetDefinitions
                                      .Where(x => x.GnosisName.Equals(datasetName))
                                      .First();
            var datasetItem1 = dataset.DatasetItems
                                                        .Where(x => x.GnosisName.Equals(datasetItemName))
                                                        .First();
            string attributeName1 = datasetItem1.Attribute;

            //Case 1: IsItemList=1. List is found under DataType
            if (datasetItem1.IsItemList)
            {
                return "Code";

            }
            //Case 2: Find the DataCache referenced by the DatasetItem
            else
            {
                //There are two DataCache elements. One in the DatasetItem and one in the Data section of the entity.
                //The actual data is stored in the latter. The first just contains a reference to the second.
                return datasetItem1.DataCaches.First().Attribute;
                
                
            }
        }

        public void Save()
        {
            undoStack = new Stack<GnosisDocChange>();
            redoStack = new Stack<GnosisDocChange>();

           // GnosisXMLHelper.SaveGnosisDocFrame((GnosisDocFrame)ControlImplementation);
        }

        public void PushUndo(GnosisVisibleController _controller, object oldState)
        {
            undoStack.Push(new GnosisDocChange(_controller, oldState));
        }

        public void PushRedo(GnosisVisibleController _controller, object newState)
        {
            redoStack.Push(new GnosisDocChange(_controller, newState));
        }

        public void Undo()
        {
            GnosisDocChange docChange = undoStack.Pop();
            docChange.Controller.Undo(docChange.OldState);
        }

        public void Redo()
        {
            GnosisDocChange docChange = redoStack.Pop();
            docChange.Controller.Redo(docChange.OldState);
        }


    }
}
