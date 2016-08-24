using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.DataControllers;
using Shiva.Shared.InnerLayoutControllers;

namespace Shiva.Shared.PanelFieldControllers
{
    public class GnosisToggleButtonController : GnosisPanelFieldController
    {

        public GnosisToggleButtonController(
            GnosisToggleButton toggleButton,
           // IGnosisToggleButtonImplementation toggleButtonImplementation,
            GnosisInstanceController instanceController,
            GnosisPanelController parent)
            :base (toggleButton, instanceController, parent)
        {

        }

        public GnosisToggleButtonController(
            GnosisToggleButton toggleButton,
          //  IGnosisToggleButtonImplementation toggleButtonImplementation,
            GnosisInstanceController instanceController,
            GnosisToggleButtonGroupController parent)
            : base(toggleButton, instanceController, parent)
        {

        }

        //public void SetToggleBinding(object source, string propertyName)
        //{
        //    ((IGnosisToggleButtonImplementation)ControlImplementation).SetToggleBinding(source, propertyName);
        //}

    }
}
