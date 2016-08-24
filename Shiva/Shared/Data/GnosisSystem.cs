using GnosisControls;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisSystem : IGnosisObject
    {
        private string userName;
        private string systemURL;
        private string systemName;
        private GnosisDeviceType deviceType;
        private string machineID;
        private string hostName;
        private string location;
        private DateTime locationDateTime;
        private string timezone;
        private string language;
        private int userID;
        private int systemID;
        private int versionNo;
        private DateTime validStartDate;
        private DateTime validFinishDate;
        private int protectionStatus;
        private bool showTooltips;
        private double scalePercentage;
        private string defaultLayout;

        private List<GnosisEntity> entities;
        private List<GnosisInstance> instances;

        private GnosisMessageList messageList;
        private GnosisStyleSet styleSet;
        private GnosisDataDefinition dataDefinition;
        private GnosisSystemDefinition systemDefinition;
        private GnosisParentWindow parentWindow;

        public enum GnosisDeviceType
        {
            Desktop,
            Tablet,
            Laptop
        }

        [GnosisProperty]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [GnosisProperty]
        public string SystemURL
        {
            get { return systemURL; }
            set { systemURL = value; }
        }

        [GnosisProperty]
        public string SystemName
        {
            get { return systemName; }
            set { systemName = value; }
        }

        [GnosisProperty]
        public string DeviceType
        {
            get
            {
                return Enum.GetName(typeof(GnosisDeviceType), deviceType);
            }
            set
            {
                try
                {
                    deviceType = (GnosisDeviceType)Enum.Parse(typeof(GnosisDeviceType), value);
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisDeviceType _DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }

        [GnosisProperty]
        public string MachineID
        {
            get { return machineID; }
            set { machineID = value; }
        }

        [GnosisProperty]
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        [GnosisProperty]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        [GnosisProperty]
        public DateTime LocationDateTime
        {
            get { return locationDateTime; }
            set { locationDateTime = value; }
        }

        [GnosisProperty]
        public string TimeZone
        {
            get { return timezone; }
            set{ timezone = value;            }
        }

        [GnosisProperty]
        public string Language
        {
            get { return language; }
            set { language = value; }
        }




        [GnosisProperty]
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
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
        public DateTime ValidStartDate
        {
            get { return validStartDate; }
            set { validStartDate = value; }
        }

        [GnosisProperty]
        public DateTime ValidFinishDate
        {
            get { return validFinishDate; }
            set { validFinishDate = value; }
        }

        [GnosisProperty]
        public int ProtectionStatus
        {
            get { return protectionStatus; }
            set { protectionStatus = value; }
        }

        [GnosisProperty]
        public bool ShowTooltips
        {
            get { return showTooltips; }
            set { showTooltips = value; }
        }

        [GnosisChild]
        public GnosisStyleSet StyleSet
        {
            get { return styleSet; }
            set { styleSet = value; }
        }

        [GnosisCollection]
        public List<GnosisEntity> GnosisEntities
        {
            get { return entities; }
            set { entities = value; }
        }

        [GnosisCollection]
        public List<GnosisInstance> GnosisInstances
        {
            get { return instances; }
            set { instances = value; }
        }

        [GnosisChild]
        public GnosisDataDefinition DataDefinition
        {
            get { return dataDefinition; }
            set { dataDefinition = value; }
        }

        [GnosisChild]
        public GnosisSystemDefinition SystemDefinition
        {
            get { return systemDefinition; }
            set { systemDefinition = value; }
        }

        [GnosisChild]
        public GnosisParentWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }
        
        [GnosisChild]
        public GnosisMessageList MessageList
        {
            get { return messageList; }
            set { messageList = value; }
        }

        [GnosisProperty]
        public double ScalePercentage
        {
            get { return scalePercentage; }
            set { scalePercentage = value; }
        }

        [GnosisProperty]
        public string DefaultLayout
        {
            get { return defaultLayout; }
            set { defaultLayout = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisEntity)
            {
                if (entities == null)
                {
                    entities = new List<GnosisEntity>();
                }
                entities.Add((GnosisEntity)child);
            }
            else if (child is GnosisInstance)
            {
                if (instances == null)
                {
                    instances = new List<GnosisInstance>();
                }
                instances.Add((GnosisInstance)child);
            }
            else if (child is GnosisStyleSet)
            {
                styleSet = (GnosisStyleSet)child;
            }
            else if  (child is GnosisDataDefinition)
            {
                dataDefinition = (GnosisDataDefinition)child;
            }
            else if (child is GnosisSystemDefinition)
            {
                systemDefinition = (GnosisSystemDefinition)child;
            }
            else if (child is GnosisParentWindow)
            {
                parentWindow = (GnosisParentWindow)child;
            }
            else if (child is GnosisMessageList)
            {
                messageList = (GnosisMessageList)child;
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisSystem", child.GetType().Name);
            }
        }
    }
}
