using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GnosisControls
{
    public class GnosisInstance : IGnosisObject
    {
        private string instanceID;

        private string instanceName;

        private string systemURL;

        private int systemID;

        private int versionNo; 

        private int entityID;

        private string entityName;

        private bool isEditing;

        private bool isEmpty;

        private string machineID;

        private bool sqlSuccessful;

        private bool created;

        private bool updated;

        private bool deleted;

        private int userID;

        private string userName;

        private XElement content;

        private bool allowRead;

        private bool allowUpdate;

        private bool allowCreate;

        private bool allowDelete;

        private string key1;

        private string key2;

        private string key3;

        [GnosisProperty]
        public string InstanceID
        {
            get { return instanceID; }
            set { instanceID = value; }
        }

        [GnosisProperty]
        public string InstanceName
        {
            get { return instanceName; }
            set { instanceName = value; }
        }

        [GnosisProperty]
        public string SystemURL
        {
            get { return systemURL; }
            set { systemURL = value; }
        }

        [GnosisProperty]
        public int SystemID
        {
            get { return systemID; }
            set { systemID = value; }
        }

        [GnosisProperty]
        public int VersionNo
        {
            get { return versionNo; }
            set { versionNo = value; }
        }

        [GnosisProperty]
        public int EntityID
        {
            get { return entityID; }
            set { entityID = value; }
        }

        [GnosisProperty]
        public string EntityName
        {
            get { return entityName; }
            set { entityName = value; }
        }

        [GnosisProperty]
        public bool IsEditing
        {
            get { return isEditing; }
            set { isEditing = value; }
        }

        [GnosisProperty]
        public bool IsEmpty
        {
            get { return isEmpty; }
            set { isEmpty = value; }
        }

        [GnosisProperty]
        public string MachineID
        {
            get { return machineID; }
            set {
                machineID = value;
                    }
        }

        [GnosisProperty]
        public bool SQLSuccessful
        {
            get { return sqlSuccessful; }
            set { sqlSuccessful = value; }
        }

        [GnosisProperty]
        public bool Created
        {
            get { return created; }
            set { created = value; }
        }

        [GnosisProperty]
        public bool Updated
        {
            get { return updated; }
            set { updated = value; }
        }

        [GnosisProperty]
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }


        [GnosisProperty]
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        [GnosisProperty]
        public string UserName
        {
            get { return UserName; }
            set { userName = value; }
        }

        //the raw xml of this instance
        
        public XElement Content
        {
            get { return content; }
            set { content = value; }
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

        [GnosisProperty]
        public string Key1
        {
            get { return key1; }
            set { key1 = value; }
        }

        [GnosisProperty]
        public string Key2
        {
            get { return key2; }
            set { key2 = value; }
        }

        [GnosisProperty]
        public string Key3
        {
            get { return key3; }
            set { key3 = value; }
        }


        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
