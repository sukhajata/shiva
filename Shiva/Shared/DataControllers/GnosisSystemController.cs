using Shiva.Shared.ContentControllers;
using Shiva.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Shiva.Shared.GenericControllers;
using GnosisControls;
using Shiva.Shared.Utility;
using Shiva.Shared.OuterLayoutControllers;
using System.ComponentModel;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.BaseControllers;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows;
using Shiva.Shared.Interfaces;

namespace Shiva.Shared.DataControllers
{
    public class GnosisSystemController : INotifyPropertyChanged
    {
        private GnosisSystem system;
        private XElement xSystem;
        private List<GnosisEntityController> entityControllers;
        private List<GnosisInstanceController> instanceControllers;
        private List<GnosisGenericControlController> genericControlControllers;
        private List<GnosisGenericMenuController> genericMenuControllers;
        private Dictionary<GnosisGenericMenuItem.MenuTagEnum, GnosisGenericMenuItemController> documentMenuItemDictionary;
        private EnvironmentVariableController environmentVariableController;
        private GnosisInstanceController currentInstanceController;
        private GnosisSearchRequest currentSearchRequest;
       // private bool loadSearchComplete;

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName
        {
            get { return system.UserName; }
            set { system.UserName = value; }
        }

        public GnosisSystem.GnosisDeviceType DeviceType
        {
            get { return system._DeviceType; }
        }


        public string HostName
        {
            get { return system.HostName; }
            set { system.HostName = value; }
        }

        public int UserID
        {
            get { return system.UserID; }
        }
        public int SystemID
        {
            get { return system.SystemID; }
        }
        public int VersionNo
        {
            get { return system.VersionNo; }
        }
        public int ProtectionStatus
        {
            get { return system.ProtectionStatus; }
            set
            {
                system.ProtectionStatus = value;
                OnPropertyChanged("ProtectionStatus");
            }
        }

        public GnosisInstanceController CurrentInstanceController
        {
            get { return currentInstanceController; }
            set
            {
                currentInstanceController = value;
                OnPropertyChanged("CurrentInstanceController");
            }
        }

        public bool ShowTooltips
        {
            get { return system.ShowTooltips; }
            set
            {
                system.ShowTooltips = value;
                OnPropertyChanged("ShowTooltips");
            }
        }

        public double ScalePercentage
        {
            get { return system.ScalePercentage; }
            set { system.ScalePercentage = value; }
        }
        public string SystemURL
        {
            get { return system.SystemURL; }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }



        public GnosisSystemController(GnosisSystem _system, XElement _xSystem)
        {
            system = _system;
            xSystem = _xSystem;
        }



        public void SetupSystem()
        {
            //environment variables
            environmentVariableController = new EnvironmentVariableController(system.SystemDefinition.EnvironmentVariables);

            //generic
            genericControlControllers = new List<GnosisGenericControlController>();
            genericMenuControllers = new List<GnosisGenericMenuController>();

            foreach (GnosisGenericControl genericControl in system.SystemDefinition.GenericControls)
            {
                GnosisGenericControlController genericControlController = new GnosisGenericControlController(genericControl);
                genericControlControllers.Add(genericControlController);
            }
            foreach (GnosisGenericMenu genericMenu in system.SystemDefinition.GenericMenus)
            {
                GnosisGenericMenuController genericMenuController = new GnosisGenericMenuController(genericMenu);
                genericMenuController.Setup();
                genericMenuControllers.Add(genericMenuController);
            }

            //store menu references
            documentMenuItemDictionary = new Dictionary<GnosisGenericMenuItem.MenuTagEnum, GnosisGenericMenuItemController>();

            GnosisGenericMenuItemController undoMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.UNDO);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.UNDO, undoMenuController);
            GnosisGenericMenuItemController redoMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.REDO);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.REDO, redoMenuController);
            GnosisGenericMenuItemController editMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.EDIT);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.EDIT, editMenuController);
            GnosisGenericMenuItemController deleteMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.DELETE);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.DELETE, deleteMenuController);
            GnosisGenericMenuItemController refreshMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.REFRESH);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.REFRESH, refreshMenuController);
            GnosisGenericMenuItemController cancelMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.CANCEL);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.CANCEL, cancelMenuController);
            GnosisGenericMenuItemController insertMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.INSERT);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.INSERT, insertMenuController);
            GnosisGenericMenuItemController saveMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.SAVE);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.SAVE, saveMenuController);
            GnosisGenericMenuItemController removeMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.REMOVE);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.REMOVE, removeMenuController);
            GnosisGenericMenuItemController searchMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.SEARCH);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.SEARCH, searchMenuController);
            GnosisGenericMenuItemController resetMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.RESET);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.RESET, resetMenuController);
            GnosisGenericMenuItemController backMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.BACKWARD);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.BACKWARD, backMenuController);
            GnosisGenericMenuItemController forwardMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.FORWARD);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.FORWARD, forwardMenuController);
            GnosisGenericMenuItemController printMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.PRINT);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.PRINT, printMenuController);
            GnosisGenericMenuItemController cutMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.CUT);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.CUT, cutMenuController);
            GnosisGenericMenuItemController copyMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.COPY);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.COPY, copyMenuController);
            GnosisGenericMenuItemController pasteMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.PASTE);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.PASTE, pasteMenuController);
            GnosisGenericMenuItemController newMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.NEW);
            documentMenuItemDictionary.Add(GnosisGenericMenuItem.MenuTagEnum.NEW, newMenuController);

            //create Entity Controllers
            entityControllers = new List<GnosisEntityController>();
            foreach (GnosisEntity entity in system.GnosisEntities)
            {
                var xEntity = xSystem.Descendants("GnosisEntity")
                                    .Where(x => x.Attribute("EntityID").Value.Equals(entity.EntityID.ToString()))
                                    .First();

                GnosisEntityController entityController = new GnosisEntityController(entity, xEntity);
                LoadStyles(entityController);

                entityControllers.Add(entityController);

            }

            //create instanceControllers
            instanceControllers = new List<GnosisInstanceController>();
            foreach (GnosisInstance instance in system.GnosisInstances)
            {
                //Get the XML for this instance
                var data = xSystem.Descendants("GnosisInstance").
                                                        Where(x => x.Attribute("EntityID").Value.Equals(instance.EntityID.ToString()));
                if (data.Count() > 0)
                {
                    var xInstance = data.First();
                    instance.Content = xInstance;
                }

                GnosisInstanceController instanceController = new GnosisInstanceController(instance, GetEntityController(instance.EntityID, SystemID));
                instanceControllers.Add(instanceController);

            }

            //Parent Window has no instance
            GnosisEntityController parentWindowController = entityControllers.Find(e => e.EntityType == GnosisEntity.EntityTypeEnum.Layout);
            GnosisInstanceController parentWindowInstanceController = parentWindowController.CreateInstanceController(null);
            instanceControllers.Add(parentWindowInstanceController);

        }

        public void SetupUI()
        {
            //Setup instance controllers. This creates the visible controls
            GnosisInstanceController parentWindowInstanceController = instanceControllers.Find(i => i.EntityController.EntityType == GnosisEntity.EntityTypeEnum.Layout);
            parentWindowInstanceController.Setup();

            //Navigator instance
            GnosisInstanceController navigatorInstanceController = instanceControllers.Find(i => i.EntityController.EntityType == GnosisEntity.EntityTypeEnum.Navigator);
            navigatorInstanceController.Setup();
            GlobalData.Singleton.PrimarySplitController.NavTileController.LoadFrame((GnosisNavigatorFrameController)navigatorInstanceController.VisibleController);

            //Then the rest
            foreach (GnosisInstanceController instanceController in instanceControllers.Where(i => i.EntityController.EntityType != GnosisEntity.EntityTypeEnum.Layout && i.EntityController.EntityType != GnosisEntity.EntityTypeEnum.Navigator))
            {
                instanceController.Setup();
            }

            //menus
            SetSystemMenuPermissions();

            //listen for property changes
            this.PropertyChanged += GnosisSystemController_PropertyChanged;
        }


        internal void ShowXML()
        {
            if (currentInstanceController != null)
            {
                string xml = currentInstanceController.Instance.Content.ToString();
                GlobalData.Singleton.ParentWindowImplementation.ShowXML(xml);
            }
        }

        internal void Delete()
        {

            currentInstanceController.Deleted = true;
            //currentInstanceController.IsEditing = false;
            currentInstanceController.Editable = false;

            UpdateDocumentPermissions();
           // ((GnosisDocumentFrameController)currentInstanceController.VisibleController).SetStrikethrough(true);
        }

        internal void Remove()
        {
            if (currentInstanceController.Deleted)
            {
                currentInstanceController.SaveDelete();

                //GnosisInstance instance = GetInstance(currentInstanceController.Instance);
                GnosisDocumentFrameController docFrameController = (GnosisDocumentFrameController)currentInstanceController.VisibleController;
                //IGnosisVisibleControlImplementation parent = ((GnosisDocumentFrame)docFrameController.ControlImplementation).GnosisParent;
                //((GnosisContainer)parent).RemoveFrame(docFrameController);

            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Instance not deleted", "GnosisSystemController.Remove");
            }
        }

        internal void Undo()
        {
            if (currentInstanceController != null && currentInstanceController.UndoCount > 0)
            {
                CurrentInstanceController.Undo();
            }

            if (currentInstanceController.UndoCount == 0)
            {
                ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.UNDO].ControlImplementation).Disabled = true;
            }

            if (currentInstanceController.RedoCount > 0)
            {
                ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REDO].ControlImplementation).Disabled = false;
            }
        }

        internal void Redo()
        {
            if (currentInstanceController != null && currentInstanceController.RedoCount > 0)
            {
                CurrentInstanceController.Redo();
            }

            if (currentInstanceController.RedoCount == 0)
            {
                ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REDO].ControlImplementation).Disabled = true;
            }

            if (currentInstanceController.UndoCount > 0)
            {
                ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.UNDO].ControlImplementation).Disabled = false;
            }
        }

        internal void ToggleEdit()
        { 
            //IsEditing means there are unsaved changes. Editable means the controls are unlocked.
            //Unpressing edit locks the controls but the document remains in IsEditing mode
            if (!currentInstanceController.IsEditing)
            {
                CurrentInstanceController.IsEditing = true;
            }
            CurrentInstanceController.Editable = !CurrentInstanceController.Editable;
            ((GnosisGenericToggleMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.EDIT].ControlImplementation).Active =
                CurrentInstanceController.Editable;
        }


        internal void Cancel()
        {
            //GnosisInstance original = currentInstanceController.Instance;
            //GnosisEntityController entityController = entityControllers.Find(e => e.EntityID == original.EntityID && e.SystemID == SystemID);
            //entityControllers.Remove(entityController);

            //XElement entityRequest = environmentVariableController.GetEntityRequest(original.EntityID, SystemID);
            //XElement xEntity = GlobalData.Singleton.DatabaseConnection.GetGnosisEntityXML(entityRequest);
            //GnosisEntity entity = GnosisXMLHelper.LoadGnosisEntity(xEntity);

            //GnosisEntityController newEntityController = new GnosisEntityController(entity, xEntity);
            //LoadStyles(newEntityController);

            //entityControllers.Add(newEntityController);

            //currentInstanceController = new GnosisInstanceController(original, newEntityController);
            //currentInstanceController.Setup();

            if (currentInstanceController.Deleted)
            {
                currentInstanceController.Deleted = false;
                // ((GnosisDocumentFrameController)currentInstanceController.VisibleController).SetStrikethrough(false);
                ((GnosisGenericToggleMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.DELETE].ControlImplementation).Active = false;
            }
            else if (currentInstanceController.Updated)
            {
                currentInstanceController.Updated = false;
                ((GnosisGenericToggleMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.EDIT].ControlImplementation).Active = false;
                // LoadDocument(CurrentInstanceController);
            }
            else if (currentInstanceController.Created)
            {
                currentInstanceController.Created = false;
            }

            currentInstanceController.IsEditing = false;
            currentInstanceController.Editable = false;
            // currentInstanceController.Cancel();

            ((GnosisFrameController)currentInstanceController.VisibleController).LoadData(); 

            UpdateDocumentPermissions();

        }

        internal void Save()
        {
            currentInstanceController.Save();

            currentInstanceController.IsEditing = false;

            //GnosisInstance instance = GetInstance(currentInstanceController.Instance);
        }

        private void GnosisSystemController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ProtectionStatus"))
            {
                UpdateDocumentPermissions();
            }
            else if (e.PropertyName.Equals("CurrentInstanceController"))
            {
                currentInstanceController.PropertyChanged -= CurrentInstanceController_PropertyChanged;

                if (currentInstanceController != null)
                {
                    if (currentInstanceController.EntityController.EntityType == GnosisEntity.EntityTypeEnum.Document)
                    {
                        currentInstanceController.PropertyChanged += CurrentInstanceController_PropertyChanged;
                    }
                    UpdateDocumentPermissions();
                }
            }
            else if (e.PropertyName.Equals("ShowTooltips"))
            {
                foreach (GnosisInstanceController instanceController in instanceControllers)
                {
                    if (ShowTooltips)
                    {
                        instanceController.ShowTooltips();
                    }
                    else
                    {
                        instanceController.HideTooltips();
                    }
                }
            }
        }


        private void CurrentInstanceController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Updated"))
            {
                UpdateDocumentPermissions();
            }
            //else if (e.PropertyName.Equals("IsEditing"))
            //{
            //    if (CurrentInstanceController.IsEditing)
            //    {
            //        CurrentInstanceController.Editable = true;
            //    }
            //    else
            //    {
            //        CurrentInstanceController.Editable = false;
            //    }
            //    UpdateDocumentPermissions();
            //}
        }

        private void SetSystemMenuPermissions()
        {
            // GnosisGenericToggleMenuItemController navMenuController = (GnosisGenericToggleMenuItemController)GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.NAVIGATOR);
            // ((GnosisGenericToggleMenuItem)navMenuController.ControlImplementation).Active = true;
            GnosisGenericMenu navMenu = system.SystemDefinition.GenericMenus.Find(m => m.GnosisName.Equals("NavigatorGroup"));
            GnosisGenericToggleMenuItem navMenuItem = navMenu.ToggleMenuItems.Find(t => t._MenuTag == GnosisGenericMenuItem.MenuTagEnum.NAVIGATOR);
            navMenuItem.Active = true;
            navMenuItem.Disabled = false;

            //GnosisGenericToggleMenuItemController helpMenuController = (GnosisGenericToggleMenuItemController)GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.TOOLTIP);
            //GnosisGenericMenuItemController propertiesMenuController = GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.PROPERTIES);
            //GnosisGenericMenuGroupController protectionStatusMenuController = (GnosisGenericMenuGroupController)GetMenuItem(GnosisGenericMenuItem.MenuTagEnum.PROTECTIONSTATUS);
            //((GnosisGenericToggleMenuItem)navMenuController.ControlImplementation).Disabled = false;
            //((GnosisGenericToggleMenuItem)helpMenuController.ControlImplementation).Disabled = false;

            GnosisGenericMenu miscMenu = system.SystemDefinition.GenericMenus.Find(m => m.GnosisName.Equals("MiscGroup"));
            GnosisGenericToggleMenuItem helpMenuItem = miscMenu.ToggleMenuItems.Find(t => t._MenuTag == GnosisGenericMenuItem.MenuTagEnum.TOOLTIP);
            helpMenuItem.Disabled = false;

            foreach (GnosisInstanceController instanceController in instanceControllers)
            {
                if (ShowTooltips)
                {
                    instanceController.ShowTooltips();
                }
                else
                {
                    instanceController.HideTooltips();
                }
            }
            // ((GnosisGenericToggleMenuItem)helpMenuController.ControlImplementation).Active = ShowTooltips;
            helpMenuItem.Active = ShowTooltips;

            GnosisGenericMenuItem propertiesMenuItem = miscMenu.GenericMenuItems.Find(m => m._MenuTag == GnosisGenericMenuItem.MenuTagEnum.PROPERTIES);
            propertiesMenuItem.Disabled = false;
            //((GnosisGenericToggleMenuItem)propertiesMenuController.ControlImplementation).Disabled = false;

            //select the protection status menu item
            GnosisGenericMenuGroup protectionStatusGroup = miscMenu.MenuGroups.Find(g => g._MenuTag == GnosisGenericMenuItem.MenuTagEnum.PROTECTIONSTATUS);
            GnosisGenericToggleMenuItem activeProtectionStatusToggleButton =
                protectionStatusGroup.ToggleMenuItems.Find(t => t.Code == this.ProtectionStatus);
            activeProtectionStatusToggleButton.Active = true;

            //var currentProtectionStatusToggleMenuItem = protectionStatusMenuController.ToggleMenuItemControllers
            //    .Where(c => ((GnosisGenericToggleMenuItem)c.ControlImplementation).Code == ProtectionStatus).First();
            //((GnosisGenericToggleMenuItem)currentProtectionStatusToggleMenuItem.ControlImplementation).Active = true;
        }

        public void UpdateDocumentPermissions()
        {

            if (currentInstanceController == null)
            {
                foreach (GnosisGenericMenuItemController menuItemController in documentMenuItemDictionary.Values)
                {
                    ((GnosisGenericMenuItem)menuItemController.ControlImplementation).Disabled = true;
                }

            }
            else if (currentInstanceController.EntityController.EntityType == GnosisEntity.EntityTypeEnum.Search)
            {
                //disable all except search and reset
                foreach (GnosisGenericMenuItemController menuItemController in documentMenuItemDictionary.Values)
                {
                    ((GnosisGenericMenuItem)menuItemController.ControlImplementation).Disabled = true;
                }

                ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.SEARCH].ControlImplementation).Disabled = false;
                ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.RESET].ControlImplementation).Disabled = false;

            }
            else if (currentInstanceController.EntityController.EntityType == GnosisEntity.EntityTypeEnum.Document)
            {
                //disable search functions
                ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.SEARCH].ControlImplementation).Disabled = true;
                ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.RESET].ControlImplementation).Disabled = true;


                //undo, redo
                if (currentInstanceController.UndoCount > 0)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.UNDO].ControlImplementation).Disabled = false;
                }
                else
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.UNDO].ControlImplementation).Disabled = true;
                }

                if (currentInstanceController.RedoCount > 0)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REDO].ControlImplementation).Disabled = false;
                }
                else
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REDO].ControlImplementation).Disabled = true;
                }

                //edit
                if (ProtectionStatus == 0)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.EDIT].ControlImplementation).Disabled = true;
                    currentInstanceController.Editable = true;
                }
                else if (ProtectionStatus == 1)
                {
                    if (!currentInstanceController.IsEmpty && !currentInstanceController.Deleted)// && !currentInstanceController.IsEditing)
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.EDIT].ControlImplementation).Disabled = false;
                    }
                    else
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.EDIT].ControlImplementation).Disabled = true;
                    }

                    if (currentInstanceController.IsEmpty || currentInstanceController.Deleted || !currentInstanceController.IsEditing)
                    {
                        currentInstanceController.Editable = false;
                    }

                    //if (currentInstanceController.IsEditing)
                    //{
                    //    ((GnosisGenericToggleMenuItemController)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.EDIT]).Active = true;
                    //}

                }
                else if (ProtectionStatus == 2)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.EDIT].ControlImplementation).Disabled = true;
                    currentInstanceController.Editable = false;
                }


                //delete
                if (ProtectionStatus != 2 && !currentInstanceController.IsEmpty && !currentInstanceController.Deleted && !currentInstanceController.IsEditing)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.DELETE].ControlImplementation).Disabled = false;
                }
                else
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.DELETE].ControlImplementation).Disabled = true;
                }

                // refresh, cancel
                if (currentInstanceController.IsEditing)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REFRESH].ControlImplementation).Hidden = true;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.CANCEL].ControlImplementation).Hidden = false;

                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.CANCEL].ControlImplementation).Disabled = false;

                }
                else if (currentInstanceController.Updated || currentInstanceController.Created || currentInstanceController.Deleted)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REFRESH].ControlImplementation).Hidden = true;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.CANCEL].ControlImplementation).Hidden = false;

                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.CANCEL].ControlImplementation).Disabled = false;

                }
                else
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REFRESH].ControlImplementation).Hidden = false;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.CANCEL].ControlImplementation).Hidden = true;

                    if (currentInstanceController.IsEmpty)
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REFRESH].ControlImplementation).Disabled = true;
                    }
                    else
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REFRESH].ControlImplementation).Disabled = false;
                    }
                }

                //insert, save, remove
                if (currentInstanceController.Created)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.INSERT].ControlImplementation).Hidden = false;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.SAVE].ControlImplementation).Hidden = true;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REMOVE].ControlImplementation).Hidden = true;

                    if (ProtectionStatus != 2)
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.INSERT].ControlImplementation).Disabled = false;
                    }
                    else
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.INSERT].ControlImplementation).Disabled = true;
                    }
                }
                else if (currentInstanceController.Deleted)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.INSERT].ControlImplementation).Hidden = true;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.SAVE].ControlImplementation).Hidden = true;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REMOVE].ControlImplementation).Hidden = false;

                    if (ProtectionStatus != 2)
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REMOVE].ControlImplementation).Disabled = false;
                    }
                    else
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REMOVE].ControlImplementation).Disabled = true;
                    }
                }
                else if (currentInstanceController.Updated)
                {
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.INSERT].ControlImplementation).Hidden = true;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.SAVE].ControlImplementation).Hidden = false;
                    ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.REMOVE].ControlImplementation).Hidden = true;

                    if (ProtectionStatus != 2 && currentInstanceController.Updated)
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.SAVE].ControlImplementation).Disabled = false;
                    }
                    else
                    {
                        ((GnosisGenericMenuItem)documentMenuItemDictionary[GnosisGenericMenuItem.MenuTagEnum.SAVE].ControlImplementation).Disabled = true;
                    }
                }

            }



        }


        public GnosisGenericMenuItemController GetMenuItem(GnosisGenericMenuItem.MenuTagEnum menuTag)
        {
            GnosisGenericMenuItemController menuItemController = null;

            foreach (GnosisGenericMenuController genericMenuController in genericMenuControllers)
            {
                foreach (GnosisGenericMenuItemController genericMenuItemController in genericMenuController.GenericMenuItemControllers)
                {
                    menuItemController = genericMenuItemController.GetMenuItemByTag(menuTag);

                    if (menuItemController != null)
                    {
                        break;
                    }
                }

                if (menuItemController != null)
                {
                    break;
                }
            }

            return menuItemController;
        }

        //internal void LoadDocument(GnosisEntityController entityController)
        //{
        //    if (entityController.EntityType == GnosisEntity.EntityTypeEnum.Document)
        //    {
        //        GnosisDocFrameController docFrameController = (GnosisDocFrameController)entityController.VisibleController;
        //        GlobalData.Singleton.PrimarySplitController.LoadDocumentFrame(docFrameController);
        //    }
        //}

        internal void LoadDocument(int docEntityID, int docSystemID, string docAction, Dictionary<int, string> keys)
        {
            GnosisEntityController entityController = GetEntityController(docEntityID, docSystemID);

            if (entityController.EntityType != GnosisEntity.EntityTypeEnum.Document)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Can not load document in non document entity - " + entityController.EntityName, "GnosisSystemController.LoadDocument");
            }
            else
            {
                //GnosisDocFrameController docFrameController = (GnosisDocFrameController)entityController.VisibleController;
                GnosisInstance instance = GetInstance(docEntityID, docSystemID, docAction, keys);
                GnosisInstanceController instanceController = new GnosisInstanceController(instance, entityController);
                instanceController.Setup();
                GlobalData.Singleton.PrimarySplitController.LoadDocumentFrame((GnosisDocumentFrameController)instanceController.VisibleController);
                if (ProtectionStatus == 0)
                {
                    instanceController.Editable = true;
                }
                else
                {
                    instanceController.Editable = false;
                }
                //CurrentInstanceController = instanceController;

            }
        }

        internal void LoadDocument(GnosisInstanceController instanceController)
        {
            GlobalData.Singleton.PrimarySplitController.LoadDocumentFrame((GnosisDocumentFrameController)instanceController.VisibleController);

            if (ProtectionStatus == 0)
            {
                instanceController.Editable = true;
            }
            else
            {
                instanceController.Editable = false;
            }
           // CurrentInstanceController = instanceController;

        }


        private delegate void SearchDelegate();
        private delegate void FinishedSearchDelegate(GnosisInstanceController instanceController);

        private void LoadSearchBackground(object sender, DoWorkEventArgs e)
        {
            //The search parameters passed in map to search parameters owned by the entity
            //Search parameters passed in contain values in the 'content' attribute
            //Search parameters belonging to the entity contain references to the local dataset which describe where to put those values
            //Create a new instance and put in the values
            //Once the search frame is loaded it will look for the values
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //  loadSearchComplete = false;
            GnosisEntityController entityController = null;

            //touch UI with parent window thread in synchronous call
            ((GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation).Dispatcher.Invoke((Action)(() =>
            {
                entityController = GetEntityController(currentSearchRequest.EntityID, currentSearchRequest.SystemID);
            }));
           

            if (entityController.EntityType != GnosisEntity.EntityTypeEnum.Search)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Can not load search in non search entity: " + entityController.EntityName, "GnosisSystemController.LoadSearch");

            }
            else
            {
                //build content of instance request
                string datasetName = entityController.Entity.SearchFrame.SearchParameters.First().Dataset;
                string elementName = entityController.GetElementName(datasetName);
                XElement xContent = new XElement(elementName);

                List<GnosisSearchParameter> localSearchParameters = entityController.Entity.SearchFrame.SearchParameters;



                for (int i = 0; i < localSearchParameters.Count; i++)
                {
                    string attributeName = entityController.GetTargetAttributeName(localSearchParameters[i].Dataset, localSearchParameters[i].DatasetItem);
                    XAttribute attribute = new XAttribute(attributeName, currentSearchRequest.SearchParams[i].Content);
                    xContent.Add(attribute);
                }



                //must use the Dispatcher of Parent Window to touch UI from a background thread. Asynchronous call
                ((GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation).Dispatcher.BeginInvoke((Action)(() =>
                {
                    GnosisInstance instance = GetInstance(currentSearchRequest.EntityID, currentSearchRequest.SystemID, "Search", xContent);
                    GnosisInstanceController instanceController = new GnosisInstanceController(instance, entityController);

                    instanceController.Setup();
                    if (currentSearchRequest.AutoSearchAction != null && currentSearchRequest.AutoSearchAction.Equals("Search"))
                    {
                        ((GnosisSearchFrameController)instanceController.VisibleController).SetAutoSearch(true);
                    }

                    CurrentInstanceController = instanceController;

                    GlobalData.Singleton.PrimarySplitController.LoadSearchFrame((GnosisSearchFrameController)currentInstanceController.VisibleController);
                    currentInstanceController.Editable = true;

                }));

               

                //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                //    new FinishedSearchDelegate(LoadSearchResults), instanceController);

            }

            timer.Stop();
            Debug.WriteLine("GnosisSystemController, Search, Milliseconds elapsed: {0}", timer.ElapsedMilliseconds);

            // loadSearchComplete = true;
        }


        //private void SearchClick(object sender, RoutedEventArgs e)
        //{
        //    LoadingAnimation.Show();

        //    new StringDelegate(DoSearch).BeginInvoke("TextToSearch", null, null);
        //}

        //private void DoSearch(string searchText)
        //{
        //    object result = /* Do the time consuming work */

        //    Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        //         new ResultDelagate(UpdateUserInterface), result);
        //}

        //private void UpdateUserInterface(object result)
        //{
        //    LoadingAnimation.Hide();

        //    DataContext = result as /* what you want */;
        //}

        public void LoadSearch()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            GnosisEntityController entityController = GetEntityController(currentSearchRequest.EntityID, currentSearchRequest.SystemID);
            Debug.WriteLine("->Search, new Entity Controller, Milliseconds elapsed: {0}", timer.ElapsedMilliseconds);

            if (entityController.EntityType != GnosisEntity.EntityTypeEnum.Search)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Can not load search in non search entity: " + entityController.EntityName, "GnosisSystemController.LoadSearch");

            }
            else
            {
                //build content of instance request
                string datasetName = entityController.Entity.SearchFrame.SearchParameters.First().Dataset;
                string elementName = entityController.GetElementName(datasetName);
                XElement xContent = new XElement(elementName);

                List<GnosisSearchParameter> localSearchParameters = entityController.Entity.SearchFrame.SearchParameters;



                for (int i = 0; i < localSearchParameters.Count; i++)
                {
                    string attributeName = entityController.GetTargetAttributeName(localSearchParameters[i].Dataset, localSearchParameters[i].DatasetItem);
                    XAttribute attribute = new XAttribute(attributeName, currentSearchRequest.SearchParams[i].Content);
                    xContent.Add(attribute);
                }

                Debug.WriteLine("->Search, Build instance request xml, Milliseconds elapsed: {0}", timer.ElapsedMilliseconds);
                //((GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation).Dispatcher.Invoke((Action)(() =>
                //{

                GnosisInstance instance = GetInstance(currentSearchRequest.EntityID, currentSearchRequest.SystemID, "Search", xContent);
                GnosisInstanceController instanceController = new GnosisInstanceController(instance, entityController);

                Debug.WriteLine("->Search, get instance and create controller , Milliseconds elapsed: {0}", timer.ElapsedMilliseconds);

                instanceController.Setup();
                if (currentSearchRequest.AutoSearchAction != null && currentSearchRequest.AutoSearchAction.Equals("Search"))
                {
                    ((GnosisSearchFrameController)instanceController.VisibleController).SetAutoSearch(true);
                }

                CurrentInstanceController = instanceController;

                Debug.WriteLine("->Search, set up instance controller, Milliseconds elapsed: {0}", timer.ElapsedMilliseconds);

                GlobalData.Singleton.PrimarySplitController.LoadSearchFrame((GnosisSearchFrameController)currentInstanceController.VisibleController);
                    currentInstanceController.Editable = true;

                //}));

                //Dispatcher.Run();

                timer.Stop();
                Debug.WriteLine("->Search, Load search frame, Milliseconds elapsed: {0}", timer.ElapsedMilliseconds);


            }

        }

        public void LoadSearch(int entityID, int systemID, List<GnosisSearchParameter> searchParameters, string searchAction, string autoSearchAction)
        {
           
            currentSearchRequest = new GnosisSearchRequest(entityID, systemID, searchParameters, searchAction, autoSearchAction);

            //ThreadStart start = new ThreadStart(LoadSearch);
            //Thread t = new Thread(start);
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
            ((GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation).Dispatcher.BeginInvoke((Action)(() =>
            {
                LoadSearch();
            }
            ), System.Windows.Threading.DispatcherPriority.Background);


            ((GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation).Dispatcher.Invoke((Action)(() =>
                {
                    GlobalData.Singleton.PrimarySplitController.DisplayLoadingProgress();

                }
            ), System.Windows.Threading.DispatcherPriority.Send);

          

            //BackgroundWorker backgroundWorker = new BackgroundWorker();
            //backgroundWorker.DoWork += LoadSearchBackground;
            //backgroundWorker.RunWorkerCompleted += LoadSearchResults;
            //backgroundWorker.RunWorkerAsync();

          

            //timer.Stop();
            //Debug.WriteLine("GnosisSystemController, Search, Milliseconds elapsed: {0}", timer.ElapsedMilliseconds);



        }

        //private void LoadSearchResults(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    GlobalData.Singleton.PrimarySplitController.LoadSearchFrame((GnosisSearchFrameController)currentInstanceController.VisibleController);
        //    currentInstanceController.Editable = true;

        //}

        private void LoadStyles(GnosisEntityController entityController)
        {
            string entityTypeString = Enum.GetName(typeof(GnosisEntity.EntityTypeEnum), entityController.EntityType);
            EntityType entityType = system.StyleSet.EntityTypes.Find(et => et.GnosisName.Equals(entityTypeString));
            GnosisStyle normalStyle = system.StyleSet.Styles.Find(s => s.GnosisName.Equals(entityType.NormalStyle));
            GnosisStyle captionStyle = system.StyleSet.Styles.Find(s => s.GnosisName.Equals(entityType.CaptionStyle));
            entityController.SetStyles(normalStyle, captionStyle);

        }

        internal GnosisEnvironmentVariable GetEnvironmentVariable(int variableSystemID, int variableControlID)
        {
            if (variableSystemID != SystemID)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Can not retrieve variable from another system", "GnosisSystemController.GetEnvironmentVariable");
                return null;
            }
            else
            {
                return environmentVariableController.GetEnvironmentVariable(variableControlID);
            }
        }

        internal string GetEnvironmentVariableValue(int variableSystemID, int variableControlID)
        {
            if (variableSystemID != SystemID)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Can not retrieve variable from another system", "GnosisSystemController.GetEnvironmentVariable");
                return null;
            }
            else
            {
                return environmentVariableController.GetEnvironmentVariableValue(variableControlID);
            }
        }

        internal void SetEnvironmentVariable(int variableSystemID, int variableControlID, string value)
        {
            if (variableSystemID != SystemID)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Can not retrieve variable from another system", "GnosisSystemController.GetEnvironmentVariable");
            }
            else
            {
                environmentVariableController.SetEnvironmentVariable(variableControlID, value);
            }
        }

        internal List<GnosisDataItem> GetDataItemList(int variableSystemID, int variableControlID)
        {
            GnosisEnvironmentVariable variable = environmentVariableController.GetEnvironmentVariable(variableControlID);
            GnosisDataType dataType = system.DataDefinition.DataTypes.Find(d => d.GnosisName.Equals(variable.DataType));

            return dataType.DataItems;

        }

        internal GnosisInstance GetInstance(GnosisInstance request)
        {
            GnosisInstance instance = GlobalData.Singleton.DatabaseConnection.GetGnosisInstance(request.Content);

            return instance;
        }

        //public GnosisInstance GetInstance(int entityID, int systemID, string action)
        //{
        //    XElement request = environmentVariableController.GetInstanceRequest(entityID, systemID, action);
        //    GnosisInstance instance = GlobalData.Singleton.DatabaseConnection.GetGnosisInstance(request);

        //    return instance;
        //}

        public GnosisInstance GetInstance(int entityID, int systemID, string action, Dictionary<int, string> keys)
        {
            XElement xRequest = environmentVariableController.GetInstanceRequest(entityID, systemID, action);

            foreach (KeyValuePair<int, string> pair in keys.OrderBy(k => k.Key))
            {
                XAttribute att = new XAttribute("Key" + pair.Key.ToString(), pair.Value);
                xRequest.Add(att);
            }

            GnosisInstance instance = GlobalData.Singleton.DatabaseConnection.GetGnosisInstance(xRequest);

            return instance;
        }


        public GnosisInstance GetInstance(int entityID, int systemID, string action, XElement content)
        {
            XElement xRequest = environmentVariableController.GetInstanceRequest(entityID, systemID, action);
            xRequest.Add(content);

            GnosisInstance instance = GlobalData.Singleton.DatabaseConnection.GetGnosisInstance(xRequest);
            return instance;
        }

        internal GnosisEntityController GetEntityController(int entityID, int systemID)
        {
            GnosisEntityController result = entityControllers.Find(e => e.EntityID == entityID && e.SystemID == systemID);

            if (result == null)
            {
                XElement entityRequest = environmentVariableController.GetEntityRequest(entityID, systemID);
                XElement xEntity = GlobalData.Singleton.DatabaseConnection.GetGnosisEntityXML(entityRequest);
                GnosisEntity entity = GnosisXMLHelper.LoadGnosisEntity(xEntity);

                result = new GnosisEntityController(entity, xEntity);
                LoadStyles(result);

                entityControllers.Add(result);

            }

            return result;

        }

        internal GnosisController.HorizontalAlignmentType GetContentHorizontalAlignment(string sqlDataType)
        {
            GnosisDataType dataType = system.DataDefinition.DataTypes.Find(d => d.SqlDataType.Equals(sqlDataType));

            if (dataType == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("DataType definition not found for sqldatatype " + sqlDataType + " in system definition", "GnosisSystemController.GetContentHorizontalAlignment");
                return 0;
            }

            return dataType._ContentHorizontalAlignment;

        }


        internal int GetMinDisplayChars(string sqlDataType)
        {
            GnosisDataType dataType = system.DataDefinition.DataTypes.Find(d => d.SqlDataType.Equals(sqlDataType));

            if (dataType == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("DataType definition not found for sqldatatype " + sqlDataType + " in system definition", "GnosisSystemController");
                return 0;
            }

            if (dataType.MinDisplayChars == 0)
            {
                if (dataType.MaxChars > 0)
                {
                    return dataType.MaxChars;
                }

                GlobalData.Singleton.ErrorHandler.HandleError("MinDisplayChars and MaxChars not set for sqldatatype " + sqlDataType + " in system definition", "GnosisSystemController");
                return 0;
            }

            return dataType.MinDisplayChars;
        }

        internal int GetMaxChars(string SqlDataType)
        {
            GnosisDataType dataType = system.DataDefinition.DataTypes.Find(d => d.SqlDataType.Equals(SqlDataType));

            if (dataType == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("DataType definition not found for sqldatatype " + SqlDataType + " in system definition", "GnosisSystemController.GetMaxChars");
                return 0;
            }

            if (dataType.MaxChars > 0)
            {
                return dataType.MaxChars;
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("MaxChars not found for sqldatatype " + SqlDataType + " in GnosisSystemController", "GnosisSystemController.GetMaxChars");
                return 0;
            }
        }

        internal int GetMaxDisplayChars(string sqlDataType)
        {
            GnosisDataType dataType = system.DataDefinition.DataTypes.Find(d => d.SqlDataType.Equals(sqlDataType));

            if (dataType == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("DataType definition not found for sqldatatype " + sqlDataType + " in system definition", "GnosisSystemController");
                return 0;
            }

            if (dataType.MaxDisplayChars == 0)
            {
                if (dataType.MaxChars > 0)
                {
                    return dataType.MaxChars;
                }
                else
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("MaxDisplayChars and MaxChars not set for sqldatatype " + sqlDataType + " in system definition", "GnosisSystemController");
                    return 0;
                }
            }

            return dataType.MaxDisplayChars;

        }

        public List<GnosisConnectionFrameController> GetConnectionFrames()
        {
            List<GnosisConnectionFrameController> connectionFrames = new List<GnosisConnectionFrameController>();
            var connectionInstanceControllers = instanceControllers.Where(i => i.EntityController.EntityType == GnosisEntity.EntityTypeEnum.Connection);

            foreach (GnosisInstanceController controller in connectionInstanceControllers)
            {
                connectionFrames.Add((GnosisConnectionFrameController)controller.VisibleController);
            }

            return connectionFrames;
        }

        public GnosisController FindControllerByID(int entityID, int controlID)
        {
            GnosisController result = null;

            foreach (GnosisGenericMenuController menuController in genericMenuControllers)
            {
                if (menuController.ControlImplementation.ID == controlID)
                {
                    result = menuController;
                    break;
                }
            }

            if (result == null)
            {
                foreach (GnosisInstanceController instanceController in instanceControllers)
                {
                    if (instanceController.EntityController.EntityID == entityID)
                    {
                        result = instanceController.FindControllerByID(controlID);

                        if (result != null)
                        {
                            break;
                        }
                    }
                }

            }

            return result;
        }

        public GnosisGenericControlController FindGenericControllerByType(string controlType)
        {
            foreach (GnosisGenericControlController genericControlController in genericControlControllers)
            {
                if (genericControlController.GenericControlType == controlType)
                {
                    return genericControlController;
                }
            }

            return null;
        }




    }
}
