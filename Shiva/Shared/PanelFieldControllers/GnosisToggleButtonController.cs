using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.DataControllers;
using ShivaShared3.InnerLayoutControllers;

namespace ShivaShared3.PanelFieldControllers
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
