using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using GnosisControls;
using Shiva.Shared.Events;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;

namespace Shiva.Shared.BaseControllers
{
    public class GnosisController: INotifyPropertyChanged
    {
        public IGnosisControl ControlImplementation;

        public event PropertyChangedEventHandler PropertyChanged;

        public enum OrientationType
        {
            PORTRAIT,
            LANDSCAPE
        }

        public enum DirectionType
        {
            HORIZONTAL,
            VERTICAL
        }

        public enum VerticalAlignmentType
        {
            NONE,
            CENTRE,
            TOP,
            BOTTOM,
            STRETCH
        }

        public enum HorizontalAlignmentType
        {
            NONE,
            LEFT,
            RIGHT,
            CENTRE,
            STRETCH
        }

        public enum TilePosition
        {
            LEFT,
            RIGHT,
            TOP,
            BOTTOM
        }

        public enum CaptionPosition
        {
            ABOVE, 
            BELOW,
            LEFT, 
            RIGHT
        }

        public enum FieldPlacementPriority
        {
            RIGHT,
            DOWN,
            NEWROW
        }

        public enum MenuTagEnum
        {
            BACKWARD,
            FORWARD,
            CUT,
            COPY,
            PASTE,
            UNDO,
            REDO,
            EDIT,
            DELETE,
            INSERT,
            SAVE,
            REMOVE,
            REFRESH,
            CANCEL,
            SEARCH,
            RESET,
            PRINT,
            NAVIGATOR,
            NEW,
            TOOLTIP,
            PROTECTIONSTATUS,
            PROPERTIES
        };

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public GnosisController(IGnosisControl control)
        {
            ControlImplementation = control;
        }

        public virtual void ExecuteAction(GnosisEventHandler.TargetActionType type)
        {
            GlobalData.Singleton.ErrorHandler.HandleError("ExecuteAction not implemented for " +
                Enum.GetName(typeof(GnosisEventHandler.TargetActionType), type), "GnosisController.ExecuteAction");
        }

        //public string Name
        //{
        //    get { return ControlImplementation.GnosisName; }
        //    set { ControlImplementation.GnosisName = value; }
        //}
        //public int ID
        //{
        //    get { return ControlImplementation.ID; }
        //    set { ControlImplementation.ID = value; }
        //}
        //public int Order
        //{
        //    get { return ControlImplementation.Order; }
        //    set { ControlImplementation.Order = value; }
        //}

    }
}
