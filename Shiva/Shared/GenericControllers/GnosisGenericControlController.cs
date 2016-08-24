using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

using Shiva.Shared.BaseControllers;


using Shiva.Shared.Events;
using Shiva.Shared.Data;
using GnosisControls;

namespace Shiva.Shared.GenericControllers
{
    public class GnosisGenericControlController : GnosisController
    {
        private GnosisVisibleController currentInstance;

        public List<GnosisEventHandler> EventHandlers;


        //The type of control which this GenericControl targets. Found in the 'Name' property
        public string GenericControlType
        {
            get
            {
                return ((GnosisGenericControl)ControlImplementation).GnosisName;
            }
        }
        public bool Visible
        {
            get
            {
                return ((GnosisGenericControl)ControlImplementation).Visible;
            }
            set
            {
                ((GnosisGenericControl)ControlImplementation).Visible = value;
                OnPropertyChanged("Visible");
            }
        }

        public bool IsEditing
        {
            get
            {
                return ((GnosisGenericControl)ControlImplementation).IsEditing;
            }
            set
            {
                ((GnosisGenericControl)ControlImplementation).IsEditing = value;
                OnPropertyChanged("IsEditing");
            }
        }

        public GnosisVisibleController CurrentInstance
        {
            get
            {
                return currentInstance;
            }
            set
            {
                if (value == null)
                {
                    currentInstance = null;
                }
                else if (value.ControlImplementation.ControlType.Equals(GenericControlType))
                {
                    currentInstance = value;
                }
                else
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("CurrentInstance of GnosisGenericControl with ControlType " + GenericControlType +
                        " can not be assigned type " + value.ControlImplementation.ControlType, "GnosisGenericControl.CurrentInstance.Set");
                }

            }
        }


        public GnosisGenericControlController(GnosisControl _control)
            :base (_control)
        {
            EventHandlers = new List<GnosisEventHandler>();
            if (((GnosisGenericControl)ControlImplementation).EventHandlers != null)
            {
                foreach (GnosisEventHandler handler in ((GnosisGenericControl)ControlImplementation).EventHandlers)
                {
                    EventHandlers.Add(handler);
                }
            }
           
        }


        public void HandleGenericEvent(GnosisEventHandler.GnosisEventType eventType, GnosisVisibleController controller)
        {
            var handlers = EventHandlers.Where(x => x._EventType == eventType).OrderBy(x => x.Order);
            foreach(GnosisEventHandler handler in handlers)
            {
                handler.HandleEvent(controller);
            }
        }



    }
}
