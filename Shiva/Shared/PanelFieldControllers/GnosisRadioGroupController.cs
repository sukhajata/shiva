using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.DataControllers;
using Shiva.Shared.InnerLayoutControllers;

namespace Shiva.Shared.PanelFieldControllers
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
