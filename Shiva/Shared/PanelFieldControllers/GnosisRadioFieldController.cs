using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.DataControllers;
using Shiva.Shared.InnerLayoutControllers;

namespace Shiva.Shared.PanelFieldControllers
{
    public class GnosisRadioFieldController : GnosisPanelFieldController
    {
        //public bool Checked
        //{
        //    get { return ((GnosisRadioField)ControlImplementation).Checked; }
        //    set
        //    {
        //        ((GnosisRadioField)ControlImplementation).Checked = value;
        //        OnPropertyChanged("Checked");
        //    }
        //}


        public GnosisRadioFieldController(
            GnosisRadioField radioField,
           // IGnosisRadioFieldImplementation radioFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisPanelController parent)
            :base (radioField, instanceController, parent)
        {
            //if (radioField.ReadOnly)
            //{
            //    radioFieldImplementation.Locked = true;
            //}

        }

        public GnosisRadioFieldController(
            GnosisRadioField radioField,
           // IGnosisRadioFieldImplementation radioFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisRadioGroupController parent)
            : base(radioField, instanceController, parent)
        {
            //if (radioField.ReadOnly)
            //{
            //    radioFieldImplementation.Locked = true;
            //}

        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisRadioField)ControlImplementation).ReadOnly)
                {
                    ((GnosisRadioField)ControlImplementation).Locked = !Editable;
                }

            }
        }

    }
}
