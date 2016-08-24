using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GnosisControls
{
    public class GnosisSystemDefinition : GnosisControl
    {
        private List<GnosisEnvironmentVariable> environmentVariables;

        private List<GnosisPermissionVariable> permissionVariables;

        private List<GnosisInternalVariable> internalVariables;

        private List<GnosisInstanceVariable> instanceVariables;

        private List<GnosisGenericMenu> genericMenus;

        private List<GnosisGenericControl> genericControls;

        private List<GnosisConnectionDocument> connectionDocuments;

        private List<GnosisNavigatorDocument> navigatorDocuments;

        private XElement content; //raw xml

        private string caption;

        private bool hasBorder;

        [GnosisProperty]
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        [GnosisCollection]
        public List<GnosisEnvironmentVariable> EnvironmentVariables
        {
            get { return environmentVariables; }
            set { environmentVariables = value; }
        }

        [GnosisCollection]
        public List<GnosisPermissionVariable> PermissionVariables
        {
            get { return permissionVariables; }
            set { permissionVariables = value; }
        }

        [GnosisCollection]
        public List<GnosisInternalVariable> InternalVariables
        {
            get { return internalVariables; }
            set { internalVariables = value; }
        }

        [GnosisCollection]
        public List<GnosisInstanceVariable> InstanceVariables
        {
            get { return instanceVariables; }
            set { instanceVariables = value; }
        }

        [GnosisCollection]
        public List<GnosisGenericMenu> GenericMenus
        {
            get { return genericMenus; }
            set { genericMenus = value; }
        }

        [GnosisCollection]
        public List<GnosisGenericControl> GenericControls
        {
            get { return genericControls; }
            set { genericControls = value; }
        }

        [GnosisCollection]
        public List<GnosisConnectionDocument> ConnectionDocuments
        {
            get { return connectionDocuments; }
            set { connectionDocuments = value; }
        }

        [GnosisCollection]
        public List<GnosisNavigatorDocument> NavigatorDocuments
        {
            get { return navigatorDocuments; }
            set { navigatorDocuments = value; }
        }

        
        [GnosisProperty]
        public bool HasBorder
        {
            get { return hasBorder; }
            set { hasBorder = value; }
        }

        public XElement Content
        {
            get { return content; }
            set { content = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        { 
            if (child is GnosisNavigatorDocument)
            {
                if (navigatorDocuments == null)
                {
                    navigatorDocuments = new List<GnosisNavigatorDocument>();
                }
                navigatorDocuments.Add((GnosisNavigatorDocument)child);
            }
            else if (child is GnosisConnectionDocument)
            {
                if (connectionDocuments == null)
                {
                    connectionDocuments = new List<GnosisConnectionDocument>();
                }
                connectionDocuments.Add((GnosisConnectionDocument)child);
            }
            else if (child is GnosisGenericControl)
            {
                if (genericControls == null)
                {
                    genericControls = new List<GnosisGenericControl>();
                }
                genericControls.Add((GnosisGenericControl)child);
            }
            else if (child is GnosisGenericMenu)
            {
                if (genericMenus == null)
                {
                    genericMenus = new List<GnosisGenericMenu>();
                }
                genericMenus.Add((GnosisGenericMenu)child);
            }
            else if (child is GnosisInstanceVariable)
            {
                if (instanceVariables == null)
                {
                    instanceVariables = new List<GnosisInstanceVariable>();
                }
                instanceVariables.Add((GnosisInstanceVariable)child);
            }
            else if (child is GnosisPermissionVariable)
            {
                if (permissionVariables == null)
                {
                    permissionVariables = new List<GnosisPermissionVariable>();
                }
                permissionVariables.Add((GnosisPermissionVariable)child);
            }
            else if (child is GnosisEnvironmentVariable)
            {
                if (environmentVariables == null)
                {
                    environmentVariables = new List<GnosisEnvironmentVariable>();
                }
                environmentVariables.Add((GnosisEnvironmentVariable)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisSystemDefinition", child.GetType().Name);
            }
        }
    }
}
