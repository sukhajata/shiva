using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.DataControllers;
using ShivaShared3.InnerLayoutControllers;

namespace ShivaShared3.PanelFieldControllers
{
    public class GnosisRadioGroupController : GnosisPanelFieldController
    {

        public GnosisRadioGroupController(
            GnosisRadioGroup radioGroup,
           // IGnosisRadioGroupImplementation radioGroupImplementation,
            GnosisInstanceController instanceController,
            GnosisPanelController parent)
            :base (radioGroup, instanceController, parent)
        {
            //if (radioGroup.ReadOnly)
            //{
            //    radioGroupImplementation.Locked = true;
            //}
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisRadioGroup)ControlImplementation).ReadOnly)
                {
                    ((GnosisRadioGroup)ControlImplementation).Locked = !Editable;
                }

            }
        }

    }
}
