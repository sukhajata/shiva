using Shiva.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    
    public class GnosisEntity: IGnosisObject
    {
        private int systemID;

        private string systemNameField;

        private int systemVersionField;

        private string systemURL;

        private string entityName;

        private int entityIDField;

        private EntityTypeEnum entityTypeField;

        private string deviceType;

        private string url;

        private bool allowRead;

        private bool allowUpdate;

        private bool allowCreate;

        private bool allowDelete;

        private GnosisDataDefinition data;

        private List<GnosisDatasetDefinition> datasets;

        private GnosisDocumentFrame docFrame;

        private GnosisConnectionFrame connectionFrame;

        private GnosisNavigatorFrame navFrame;

        private GnosisSearchFrame searchFrame;

        private GnosisParentWindow parentWindow;

        //private List<GnosisSystemDefinition> systemDefinitions;

        public enum EntityTypeEnum
        {
            Document,
            Navigator,
            Search,
            Connection,
            System,
            Layout,
            Global,
            Generic
        }



        [GnosisProperty]
        public int SystemID
        {
            get { return systemID; }
            set { systemID = value; }
        }

        [GnosisProperty]
        public string System
        {
            get
            {
                return this.systemNameField;
            }
            set
            {
                this.systemNameField = value;
            }
        }

        [GnosisProperty]
        public int VersionNo
        {
            get
            {
                return this.systemVersionField;
            }
            set
            {
                this.systemVersionField = value;
            }
        }

        [GnosisProperty]
        public string SystemURL
        {
            get { return systemURL;  }
            set { systemURL = value; }
        }

        [GnosisProperty]
        public string DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }

        [GnosisProperty]
        public string Entity
        {
            get
            {
                return this.entityName;
            }
            set
            {
                this.entityName = value;
            }
        }

        [GnosisProperty]
        public int EntityID
        {
            get
            {
                return this.entityIDField;
            }
            set
            {
                this.entityIDField = value;
            }
        }

        [GnosisProperty]
        public string EntityType
        {
            get
            {
                return Enum.GetName(typeof(EntityTypeEnum), this.entityTypeField);
            }
            set
            {
                try
                {
                    this.entityTypeField = (EntityTypeEnum) Enum.Parse(typeof(EntityTypeEnum), value);

                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }


        public EntityTypeEnum GnosisEntityType
        {
            get { return entityTypeField; }
            set { entityTypeField = value; }
        }

        [GnosisProperty]
        public string URL
        {
            get { return url; }
            set { url = value; }
        }

        [GnosisProperty]
        public bool AllowRead
        {
            get { return allowRead; }
            set { allowRead = value; }
        }

        [GnosisProperty]
        public bool AllowUpdate
        {
            get { return allowUpdate; }
            set { allowUpdate = value; }
        }

        [GnosisProperty]
        public bool AllowCreate
        {
            get { return allowCreate; }
            set { allowCreate = value; }
        }

        [GnosisProperty]
        public bool AllowDelete
        {
            get { return allowDelete; }
            set { allowDelete = value; }
        }

        [GnosisChild]
        public GnosisDataDefinition DataDefinition
        {
            get { return data; }
            set { data = value; }
        }

        [GnosisCollection]
        public List<GnosisDatasetDefinition> DatasetDefinitions
        {
            get { return datasets; }
            set { datasets = value; }
        }

        [GnosisChild]
        public GnosisDocumentFrame DocumentFrame
        {
            get { return docFrame; }
            set { docFrame = value; }
        }

        [GnosisChild]
        public GnosisNavigatorFrame NavigatorFrame
        {
            get { return navFrame; }
            set { navFrame = value; }
        }

        [GnosisChild]
        public GnosisSearchFrame SearchFrame
        {
            get { return searchFrame; }
            set { searchFrame = value; }
        }

        [GnosisChild]
        public GnosisConnectionFrame ConnectionFrame
        {
            get { return connectionFrame; }
            set { connectionFrame = value; }
        }

        [GnosisChild]
        public GnosisParentWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }

        //[System.Xml.Serialization.XmlElement("GnosisSystemDefinition")]
        //public List<GnosisSystemDefinition> SystemDefinitions
        //{
        //    get { return systemDefinitions; }
        //    set { systemDefinitions = value; }
        //}

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisDataDefinition)
            {
                data = (GnosisDataDefinition)child;
            }
            else if (child is GnosisDatasetDefinition)
            {
                if (datasets == null)
                {
                    datasets = new List<GnosisDatasetDefinition>();
                }
                datasets.Add((GnosisDatasetDefinition)child);
            }
            else if (child is GnosisDocumentFrame)
            {
                docFrame = (GnosisDocumentFrame)child;
            }
            else if (child is GnosisConnectionFrame)
            {
                connectionFrame = (GnosisConnectionFrame)child;
            }
            else if (child is GnosisNavigatorFrame)
            {
                navFrame = (GnosisNavigatorFrame)child;
            }
            else if (child is GnosisSearchFrame)
            {
                searchFrame = (GnosisSearchFrame)child;
            }
            else if (child is GnosisParentWindow)
            {
                parentWindow = (GnosisParentWindow)child;
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisEntity", child.GetType().Name);
            }
    }

    }
}
