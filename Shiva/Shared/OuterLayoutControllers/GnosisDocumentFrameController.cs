using System;
using System.Collections.Generic;
using System.Text;

using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.Events;
using ShivaShared3.Data;
using ShivaShared3.Utility;
using ShivaShared3.ContentControllers;
using ShivaShared3.ContainerControllers;
using GnosisControls;
using ShivaShared3.InnerLayoutControllers;
using System.Linq;

namespace ShivaShared3.OuterLayoutControllers
{
    public class GnosisDocumentFrameController : GnosisFrameController
    {
        public List<IGnosisMouseVisibleControlImplementation> ChildControls;

        //public bool Created
        //{
        //    get { return ((GnosisDocumentFrame)ControlImplementation).Created; }
        //    set
        //    {
        //        ((GnosisDocumentFrame)ControlImplementation).Created = value;
        //        OnPropertyChanged("Created");
        //    }

        //}
        //public bool Deleted
        //{
        //    get { return ((GnosisDocumentFrame)ControlImplementation).Deleted; }
        //    set
        //    {
        //        ((GnosisDocumentFrame)ControlImplementation).Deleted = value;
        //        OnPropertyChanged("Deleted");
        //    }
        //}

        //public bool Updated
        //{
        //    get { return ((GnosisDocumentFrame)ControlImplementation).Updated; }
        //    set
        //    {
        //        ((GnosisDocumentFrame)ControlImplementation).Updated = value;
        //        OnPropertyChanged("Updated");
        //    }
        //}

        //public bool IsEditing
        //{
        //    get { return ((GnosisDocumentFrame)ControlImplementation).IsEditing; }
        //    set
        //    {
        //        ((GnosisDocumentFrame)ControlImplementation).IsEditing = value;
        //        OnPropertyChanged("IsEditing");
        //    }
        //}

        //public override void Undo(object oldState)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool IsEmpty
        //{
        //    get { return ((GnosisDocumentFrame)ControlImplementation).IsEmpty; }
        //    set
        //    {
        //        ((GnosisDocumentFrame)ControlImplementation).IsEmpty = value;
        //        OnPropertyChanged("IsEmpty");
        //    }
        //}

        //internal void Redo()
        //{
        //    throw new NotImplementedException();
        //}

        public GnosisDocumentFrameController(
            GnosisDocumentFrame documentFrame, 
          //  IGnosisDocFrameImplementation _docFrameImplementation,
            GnosisInstanceController instanceController,
            GnosisContainerController parent)
            : base(documentFrame, instanceController, parent)
        {
           // documentFrame.PropertyChanged += DocFrameController_PropertyChanged;

            ChildControls = new List<IGnosisMouseVisibleControlImplementation>();

        }


        public override void ExecuteAction(GnosisEventHandler.TargetActionType actionType)
        {
            switch (actionType)
            {
                case GnosisEventHandler.TargetActionType.Save:
                    InstanceController.Save();
                    break;
                case GnosisEventHandler.TargetActionType.Get:
                    GlobalData.Singleton.ErrorHandler.HandleError("ExecuteAction not implemented for GET on DocFrame", "GnosisDocFrameController");
                    break;
                case GnosisEventHandler.TargetActionType.New:
                    GnosisDocumentFrameController docFrameController = New();
                    GlobalData.Singleton.ErrorHandler.HandleError("Created new DocFrame but don't know where to put it.", "GnosisDocFrameController");
                    break;
                case GnosisEventHandler.TargetActionType.Delete:
                    Delete();
                    break;
                default:
                    base.ExecuteAction(actionType);
                    break;
            }
        }

        private GnosisDocumentFrameController New()
        {
            GnosisDocumentFrame docFrame = GnosisControlCreator.CreateGnosisDocFrame(
                GlobalData.Singleton.Connection.System,
                GlobalData.Singleton.Connection.VersionNo,
                GlobalData.Singleton.Connection.URL);

           // IGnosisDocFrameImplementation docFrameImp = GlobalData.Singleton.ImplementationCreator.GetGnosisDocFrameImplementation();

            GnosisDocumentFrameController newController = 
                new GnosisDocumentFrameController(docFrame, InstanceController, (GnosisContainerController)Parent);
            newController.Setup();

            return newController;
        }

        private void Delete()
        {

        }

        //private void DocFrameController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    //GlobalData.Instance.ErrorHandler.HandleError("Doc frame" + e.PropertyName, "GnosisDocFrameController");
        //}


        internal void Save()
        {
            foreach(GnosisInnerLayoutController child in ChildControllers)
            {
                if (!(child is GnosisMessageGridController))
                {
                    child.Save();
                }
            }
        }

        internal void SetStrikethrough(bool strikethrough)
        {
            foreach (GnosisInnerLayoutController child in ChildControllers)
            {
                if (!(child is GnosisMessageGridController))
                {
                    child.SetStrikethrough(strikethrough);
                }
            }
        }

    }
}
