using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.DataControllers;
using ShivaShared3.InnerLayoutControllers;

namespace ShivaShared3.PanelFieldControllers
{
    public class GnosisDateFieldController : GnosisPanelFieldController
    {

        public GnosisDateFieldController(
            GnosisDateField dateField,
          //  IGnosisDateFieldImplementation dateFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(dateField, instanceController, parent)
        {
           // dateFieldImplementation.SetDateFormat(GlobalData.GnosisDateFormat.LONG);

            //if (dateField.ReadOnly)
            //{
            //    dateFieldImplementation.Locked = true;
            //}
        }

        public override void LoadData(int rowNo)
        {
            string datasetName = ((GnosisDateField)ControlImplementation).Dataset;
            string datasetItemName = ((GnosisDateField)ControlImplementation).DatasetItem;
            DateTime value = InstanceController.GetDataDateTime(datasetName, datasetItemName, rowNo);

            ((IGnosisDateFieldImplementation)ControlImplementation).SetDate(value);
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisDateField)ControlImplementation).ReadOnly)
                {
                    ((IGnosisDateFieldImplementation)ControlImplementation).Locked = !Editable;
                }

            }
        }
    }
}
