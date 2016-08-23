using System;
using System.Collections.Generic;
using System.Text;
using GnosisControls;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.InnerLayoutControllers;
using ShivaShared3.Data;

namespace ShivaShared3.PanelFieldControllers
{
    public class GnosisDateTimeFieldController : GnosisPanelFieldController
    {
        public GnosisDateTimeFieldController(
            GnosisDateTimeField dateTime,
          //  IGnosisDateTimeFieldImplementation dateTimeFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(dateTime, instanceController, parent)
        {
            //dateTimeFieldImplementation.SetTimeFormat(GlobalData.GnosisTimeFormat.MINUTE);
            //dateTimeFieldImplementation.SetDateFormat(GlobalData.GnosisDateFormat.SHORT);

            //if (dateTime.ReadOnly)
            //{
            //    dateTimeFieldImplementation.Locked = true;
            //}

            SetDisplayDimensions();

        }

        public override void LoadData(int rowNo)
        {
            string datasetName = ((GnosisDateTimeField)ControlImplementation).Dataset;
            string datasetItemName = ((GnosisDateTimeField)ControlImplementation).DatasetItem;
            DateTime value = InstanceController.GetDataDateTime(datasetName, datasetItemName, rowNo);

            ((IGnosisDateTimeFieldImplementation)ControlImplementation).SetDateTime(value);
        }


        internal override void Save(int rowNo)
        {
            string datasetName = ((GnosisDateTimeField)ControlImplementation).Dataset;
            string datasetItemName = ((GnosisDateTimeField)ControlImplementation).DatasetItem;
            DateTime value = ((IGnosisDateTimeFieldImplementation)ControlImplementation).GetDateTime();

            InstanceController.PutDataDateTime(datasetName, datasetItemName, rowNo, value);

        }

        protected override void SetDisplayDimensions()
        {
            base.SetDisplayDimensions();

            double widthNeeded = ((GnosisDateTimeField)ControlImplementation).GetSetWidth();
            MinFieldWidth = widthNeeded;
            MaxFieldWidth = widthNeeded;
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisDateTimeField)ControlImplementation).ReadOnly)
                {
                    ((IGnosisDateTimeFieldImplementation)ControlImplementation).Locked = !Editable;
                }

            }
        }

    }
}
