using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.DataControllers;
using Shiva.Shared.Events;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.InnerLayoutControllers;

namespace Shiva.Shared.PanelFieldControllers
{
    public class GnosisButtonController : GnosisPanelFieldController
    {
        //public bool Depressed
        //{
        //    get { return ((GnosisButton)ControlImplementation).Depressed; }
        //    set {
        //        //this is an event rather than a property change
        //       // ((GnosisButton)ControlImplementation).Depressed = value;
        //        OnPropertyChanged("Depressed");
        //    }
        //}

        public GnosisButtonController(
            GnosisButton button, 
          //  IGnosisButtonImplementation buttonImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(button, instanceController, parent)
        {
         
        }

        public override void OnMouseUp()
        {
            base.OnMouseUp();

            //foreach (GnosisEventHandler handler in ((GnosisButton)ControlImplementation).EventHandlers)
            //{
            //    handler.HandleEvent(this);
            //}
        }


    }
}
