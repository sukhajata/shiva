using GnosisControls;
using ShivaShared3.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace ShivaShared3.DataControllers
{
    public class EnvironmentVariableController
    {
        private List<GnosisEnvironmentVariable> environmentVariables;

        public EnvironmentVariableController(List<GnosisEnvironmentVariable> _environmentVariables)
        {
            environmentVariables = _environmentVariables;
        }

        public string GetEnvironmentVariableValue(int variableID)
        {
            GnosisEnvironmentVariable variable = environmentVariables.Find(v => v.ID == variableID);
            return GetEnvironmentVariableValue(variable.GnosisName);
        }

        public string GetEnvironmentVariableValue(string name)
        {
            GnosisEnvironmentVariable variable = environmentVariables.Find(v => v.GnosisName.Equals(name));

            if (variable == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Variable not found: " + name, "EnvironmentVariableController.GetEnvironmentVariable");
                return "";
            }
            string result = "";

            switch(variable.GnosisName)
            {
                case "UserName":
                    result = GlobalData.Singleton.SystemController.UserName;
                    break;
                case "SystemURL":
                    result = GlobalData.Singleton.SystemController.SystemURL;
                    break;
                case "DeviceType":
                    result = Enum.GetName(typeof(GnosisSystem.GnosisDeviceType), GlobalData.Singleton.SystemController.DeviceType);
                    break;
                case "MachineID":
                    break;
                case "HostName":
                    result = GlobalData.Singleton.SystemController.HostName;
                    break;
                case "Location":
                    break;
                case "LocalDateTime":
                    DateTime datetime = new DateTime();
                    result = datetime.ToShortDateString() + " " + datetime.ToShortTimeString();
                    break;
                case "TimeZone":
                    break;
                case "Language":
                    CultureInfo culture = Thread.CurrentThread.CurrentCulture;
                    if (culture.IsNeutralCulture)
                    {
                        result = culture.EnglishName;
                    }
                    else
                    {
                        result = culture.Parent.EnglishName;
                    }
                    break;
                case "UserID":
                    result = GlobalData.Singleton.SystemController.UserID.ToString();
                    break;
                case "SystemID":
                    result = GlobalData.Singleton.SystemController.SystemID.ToString();
                    break;
                case "VersionNo":
                    result = GlobalData.Singleton.SystemController.VersionNo.ToString();
                    break;
                case "ValidStartDate":
                    break;
                case "ValidFinishDate":
                    break;
                case "ShowNavigator":
                    break;
                case "ProtectionStatus":
                    result = GlobalData.Singleton.SystemController.ProtectionStatus.ToString();
                    break;
                case "ShowTooltips":
                    if (GlobalData.Singleton.SystemController.ShowTooltips)
                        result = "1";
                    else
                        result = "0";
                    break;
                case "ScalePercentage":
                    result = GlobalData.Singleton.SystemController.ScalePercentage.ToString();
                    break;
                case "DefaultLayout":
                    break;

            }


            return result;
        }

        public GnosisEnvironmentVariable GetEnvironmentVariable(int variableID)
        {
            GnosisEnvironmentVariable variable = environmentVariables.Find(v => v.ID == variableID);
            if (variable == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Variable not found: " + variableID.ToString(), "EnvironmentVariableController.GetEnvironmentVariable");
            }
            return variable;
        }

        public void SetEnvironmentVariable(int variableID, string value)
        {
            GnosisEnvironmentVariable variable = environmentVariables.Find(v => v.ID == variableID);
            if (variable == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Variable not found: " + variableID.ToString(), "EnvironmentVariableController.GetEnvironmentVariable");
            }

            SetEnvironmentVariable(variable.GnosisName, value);
        }

        public void SetEnvironmentVariable(string name, string value)
        {
            GnosisEnvironmentVariable variable = environmentVariables.Find(v => v.GnosisName.Equals(name));

            if (variable == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Variable not found: " + name, "EnvironmentVariableController.GetEnvironmentVariable");
            }

            switch (variable.GnosisName)
            {
                case "UserName":
                    GlobalData.Singleton.SystemController.UserName = value;
                    break;
                case "SystemURL":
                    //GlobalData.Singleton.SystemController.SystemURL = value;
                    break;
                case "DeviceType":
                    //Enum.GetName(typeof(GnosisSystem.GnosisDeviceType), GlobalData.Singleton.SystemController.DeviceType);
                    break;
                case "MachineID":
                    break;
                case "HostName":
                    GlobalData.Singleton.SystemController.HostName = value;
                    break;
                case "Location":
                    break;
                case "LocalDateTime":
                    //DateTime datetime = new DateTime();
                    //result = datetime.ToShortDateString() + " " + datetime.ToShortTimeString();
                    break;
                case "TimeZone":
                    break;
                case "Language":
                    //CultureInfo culture = Thread.CurrentThread.CurrentCulture;
                    //if (culture.IsNeutralCulture)
                    //{
                    //    result = culture.EnglishName;
                    //}
                    //else
                    //{
                    //    result = culture.Parent.EnglishName;
                    //}
                    break;
                case "UserID":
                    //GlobalData.Singleton.SystemController.UserID.ToString();
                    break;
                case "SystemID":
                    //GlobalData.Singleton.SystemController.SystemID.ToString();
                    break;
                case "VersionNo":
                    //GlobalData.Singleton.SystemController.VersionNo.ToString();
                    break;
                case "ValidStartDate":
                    break;
                case "ValidFinishDate":
                    break;
                case "ShowNavigator":
                    break;
                case "ProtectionStatus":
                    GlobalData.Singleton.SystemController.ProtectionStatus = Convert.ToInt32(value);
                    break;
                case "ShowTooltips":
                    if (value.Equals("true"))
                    {
                        GlobalData.Singleton.SystemController.ShowTooltips = true;
                    }
                    else
                    {
                        GlobalData.Singleton.SystemController.ShowTooltips = false;
                    }
                    break;
                case "ScalePercentage":
                    GlobalData.Singleton.SystemController.ScalePercentage = Convert.ToDouble(value);
                    break;
                case "DefaultLayout":
                    break;

            }

        }

        public XElement GetInstanceRequest(int entityID, int systemID, string action)
        {
            XElement xInstance = new XElement("GnosisInstance");

            XAttribute attribute = new XAttribute("EntityID", entityID);
            xInstance.Add(attribute);

            XAttribute att2 = new XAttribute("SystemID", systemID);
            xInstance.Add(att2);

            XAttribute attribute2 = new XAttribute("Action", action);
            xInstance.Add(attribute2);

            foreach (GnosisEnvironmentVariable environmentVariable in environmentVariables.Where(v => v.IsInstanceInput))
            {
                XAttribute att = new XAttribute(environmentVariable.GnosisName, GetEnvironmentVariableValue(environmentVariable.GnosisName));
                xInstance.Add(att);
            }

            return xInstance;
        }

        internal XElement GetEntityRequest(int entityID, int systemID)
        {
            XElement xEntity = new XElement("GnosisEntity");

            XAttribute att1 = new XAttribute("EntityID", entityID);
            xEntity.Add(att1);

            //XAttribute att2 = new XAttribute("SystemID", systemID);
            //xEntity.Add(att2);

            foreach (GnosisEnvironmentVariable environmentVariable in environmentVariables.Where(v => v.IsInstanceInput))
            {
                XAttribute att = new XAttribute(environmentVariable.GnosisName, GetEnvironmentVariableValue(environmentVariable.GnosisName));
                xEntity.Add(att);
            }

            return xEntity;
        }

    }
}
