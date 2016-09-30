using Shiva.Shared.ContainerControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Events;
using Shiva.Shared.Interfaces;
using Shiva.Shared.OuterLayoutControllers;
using Shiva.Shared.WindowControllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Shiva.Shared.ContentControllers;
using System.ComponentModel;
using Shiva.Shared.PanelFieldControllers;
using Shiva.Shared.BaseControllers;
using GnosisControls;

namespace Shiva.Shared.DataControllers
{
    public class GnosisInstanceController 
    {
        private GnosisInstance instance;
        private GnosisEntityController entityController;
        private bool editable;

        protected Stack<GnosisDocChange> undoStack;
        protected Stack<GnosisDocChange> redoStack;

        protected GnosisVisibleController visibleController;

        //public static GnosisInstanceController CurrentInstanceController;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Editable
        {
            get { return editable; }
            set
            {
                editable = value;
                OnPropertyChanged("Editable");
            }
        }

        public bool IsEditing
        {
            get { return instance.IsEditing; }
            set
            {
                instance.IsEditing = value;
                OnPropertyChanged("IsEditing");
            }
        }

        public bool IsEmpty
        {
            get { return instance.IsEmpty; }
            set { instance.IsEmpty = value; }
        }


        public bool Created
        {
            get { return instance.Created; }
            set { instance.Created = value; }
        }

        public bool Updated
        {
            get { return instance.Updated; }
            set
            {
                instance.Updated = value;
                OnPropertyChanged("Updated");
            }
        }

        public int RedoCount
        {
            get { return redoStack.Count; }
        }

        public int UndoCount
        {
            get { return undoStack.Count; }
        }

        public bool Deleted
        {
            get { return instance.Deleted; }
            set
            {
                instance.Deleted = value;
                OnPropertyChanged("Deleted");
                //visibleController.DatasetDeleted = value;
            }
        }

        public bool SQLSuccessful
        {
            get { return instance.SQLSuccessful; }
            set { instance.SQLSuccessful = value; }
        }

        public GnosisEntityController EntityController
        {
            get { return entityController; }
        }

        public GnosisVisibleController VisibleController
        {
            get { return visibleController; }
        }

        public GnosisInstance Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        public GnosisInstanceController(
            GnosisInstance _instance,
            GnosisEntityController _entityController)
        {
            instance = _instance;
            entityController = _entityController;

            undoStack = new Stack<GnosisDocChange>();
            redoStack = new Stack<GnosisDocChange>();
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
    }

        public virtual void Setup()
        {

            if (entityController.Entity.ParentWindow != null)
            {
               // IGnosisParentWindowImplementation parentWindowImplementation = GlobalData.Singleton.ParentWindowImplementation;
                visibleController = new GnosisParentWindowController(entityController.Entity.ParentWindow, entityController);
                ((GnosisParentWindowController)visibleController).Setup();
            }
            if (entityController.Entity.DocumentFrame != null)
            {
                GnosisTileController firstTile = GlobalData.Singleton.PrimarySplitController.GetFirstTile();

              //  IGnosisDocFrameImplementation docFrameImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisDocFrameImplementation();
                visibleController = new GnosisDocumentFrameController(entityController.Entity.DocumentFrame, this, firstTile);
                ((GnosisDocumentFrameController)visibleController).Setup();

                //  firstTile.LoadFrame(docFrameController);
            }
            else if (entityController.Entity.ConnectionFrame != null)
            {
               // IGnosisConnectionFrameImplementation connectionFrameImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisConnectionFrameImplementation();
                visibleController = new GnosisConnectionFrameController(entityController.Entity.ConnectionFrame,  this, null);
                ((GnosisConnectionFrameController)visibleController).Setup();

            }
            else if (entityController.Entity.NavigatorFrame != null)
            {
              //  IGnosisNavFrameImplementation navFrameImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisNavFrameImplementation();
                visibleController = new GnosisNavigatorFrameController(entityController.Entity.NavigatorFrame, this, GlobalData.Singleton.PrimarySplitController.NavTileController);
                ((GnosisNavigatorFrameController)visibleController).Setup();

               // GlobalData.Singleton.PrimarySplitController.AddNavFrame((GnosisNavFrameController)visibleController);
            }
            else if (entityController.Entity.SearchFrame != null)
            {
                GnosisTileController firstTile = GlobalData.Singleton.PrimarySplitController.GetFirstTile();

               // IGnosisSearchFrameImplementation searchFrameImp = GlobalData.Singleton.ImplementationCreator.GetGnosisSearchFrameImplementation();
                visibleController = new GnosisSearchFrameController(entityController.Entity.SearchFrame, this, firstTile);
                ((GnosisSearchFrameController)visibleController).Setup();

                // firstTile.LoadFrame(searchFrameController);
            }

            this.PropertyChanged += GnosisInstanceController_PropertyChanged;
            visibleController.PropertyChanged += VisibleController_PropertyChanged;

        }

        private void GnosisInstanceController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (editable)
                {
                    visibleController.Editable = true;
                }
                else
                {
                    visibleController.Editable = false;
                }
            }
        }

        private void VisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasFocus"))
            {
                if (((IGnosisVisibleControlImplementation)visibleController.ControlImplementation).HasFocus)
                {
                    if (instance != null)
                    {
                        GlobalData.Singleton.SystemController.CurrentInstanceController = this;
                    }
                }
                else
                {
                    //GlobalData.Singleton.SystemController.SetCurrentInstanceController(null);
                }
            }
        }

        internal void SetDataUpdated(string datasetName, int rowNo)
        {
            Updated = true;
            entityController.SetDataUpdated(datasetName, rowNo, instance);
        }

        internal void PutDataString(string datasetName, string datasetItemName, int rowNo, string value)
        {
            entityController.PutDataString(datasetName, datasetItemName, instance, rowNo, value);

        }

        internal void PutDataBool(string datasetName, string datasetItemName, int rowNo, bool value)
        {
            string boolString = "0";
            if (value)
            {
                boolString = "1";
            }

            entityController.PutDataString(datasetName, datasetItemName, instance, rowNo, boolString);
        }

        internal void PutDataDateTime(string datasetName, string datasetItemName, int rowNo, DateTime value)
        {
            entityController.PutDataDateTime(datasetName, datasetItemName, instance, rowNo, value);
        }

        internal string GetDataString(string datasetName, string datasetItemName, int rowNo)
        {
            return entityController.GetDataString(datasetName, datasetItemName, instance, rowNo);
        }

        internal double GetDataDouble(string datasetName, string datasetItemName, int rowNo)
        {
            return entityController.GetDataDouble(datasetName, datasetItemName, instance, rowNo);
        }

        public IEnumerable<XElement> GetDataRows(string datasetName)
        {
            return entityController.GetDataRows(datasetName, instance);
        }

        public List<string> GetOptionsList(string datasetName, string datasetItemName)
        {
            return entityController.GetOptionsList(datasetName, datasetItemName);
        }

        public XElement GetOptionsXML(string datasetName, string datasetItemName)
        {
            return entityController.GetOptionsXML(datasetName, datasetItemName);
        }

        public string GetKeyOptionSourceAttributeName(string datasetName, string datasetItemName)
        {
            return entityController.GetKeyOptionSourceAttributeName(datasetName, datasetItemName);
        }

        public DateTime GetDataDateTime(string datasetName, string datasetItemName, int rowNo)
        {
            return entityController.GetDataDateTime(datasetName, datasetItemName, instance, rowNo);
        }

        public string GetTargetAttributeName(string datasetName, string datasetItemName)
        {
            return entityController.GetTargetAttributeName(datasetName, datasetItemName);
        }

        public string GetSourceAttributeName(string datasetName, string datasetItemName)
        {
            return entityController.GetSourceAttributeName(datasetName, datasetItemName);
        }

        public bool GetDataBool(string datasetName, string datasetItemName, int rowNo)
        {
            return entityController.GetDataBool(datasetName, datasetItemName, instance, rowNo);
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

        public void Save()
        {
            undoStack = new Stack<GnosisDocChange>();
            redoStack = new Stack<GnosisDocChange>();

            ((GnosisDocumentFrameController)visibleController).Save();
        }

        public virtual GnosisController FindControllerByID(int controlID)
        {
            GnosisController controller = null;

            if (EntityController.EntityType == GnosisEntity.EntityTypeEnum.Connection)
            {
                controller = ((GnosisConnectionFrameController)visibleController).FindControllerByID(controlID);
            }
            else if (EntityController.EntityType == GnosisEntity.EntityTypeEnum.Document)
            {
                controller = ((GnosisDocumentFrameController)visibleController).FindControllerByID(controlID);
            }
            else if (EntityController.EntityType == GnosisEntity.EntityTypeEnum.Navigator)
            {
                controller = ((GnosisNavigatorFrameController)visibleController).FindControllerByID(controlID);
            }
            else if (EntityController.EntityType == GnosisEntity.EntityTypeEnum.Search)
            {
                controller = ((GnosisSearchFrameController)visibleController).FindControllerByID(controlID);
            }

            return controller;
        }

        internal void SaveDelete()
        {
            undoStack = new Stack<GnosisDocChange>();
            redoStack = new Stack<GnosisDocChange>();

            var descendants = instance.Content.Descendants();

            foreach (var descendant in descendants)
            {
                if (descendant.Attribute("_Deleted") == null)
                {
                    XAttribute att = new XAttribute("_Deleted", "1");
                    descendant.Add(att);
                }
                else
                {
                    descendant.Attribute("_Deleted").Value = "1";
                }
            }

        }

        internal void ShowTooltips()
        {
            visibleController.ShowTooltips();
        }

        internal void HideTooltips()
        {
            visibleController.HideTooltips();
        }

        internal void Cancel()
        {
            //while (undoStack.Count != 0)
            //{
            //    GnosisDocChange docChange = undoStack.Pop();
            //    docChange.Controller.Undo(docChange.OldState);
            //}

            undoStack = new Stack<GnosisDocChange>();
            redoStack = new Stack<GnosisDocChange>();

            //((GnosisFrameController)VisibleController).LoadData();
        }
    }
}
