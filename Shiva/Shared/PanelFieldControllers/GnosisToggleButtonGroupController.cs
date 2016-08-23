using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.DataControllers;
using ShivaShared3.InnerLayoutControllers;

namespace ShivaShared3.PanelFieldControllers
{
    public class GnosisToggleButtonGroupController : GnosisPanelFieldController
    {

        public GnosisToggleButtonGroupController(
            GnosisToggleButtonGroup toggleButtonGroup,
          //  IGnosisToggleButtonGroupImplementation toggleButtonGroupImplementation,
            GnosisInstanceController instanceController,
            GnosisPanelController parent)
            :base (toggleButtonGroup, instanceController, parent)
        {
            //if (toggleButtonGroup.ReadOnly)
            //{
            //    toggleButtonGroupImplementation.Locked = true;
            //}
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisToggleButtonGroup)ControlImplementation).ReadOnly)
                {
                    ((IGnosisToggleButtonGroupImplementation)ControlImplementation).Locked = !Editable;
                }

            }
        }

    }
}
