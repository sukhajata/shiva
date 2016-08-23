﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using GnosisControls;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.Utility;
using ShivaShared3.InnerLayoutControllers;

namespace ShivaShared3.PanelFieldControllers
{
    public class GnosisCheckFieldController : GnosisPanelFieldController
    {
        bool isChecked;
        //public bool Checked
        //{
        //    get { return ((GnosisCheckField)ControlImplementation).Checked; }
        //    set
        //    {
        //        ((GnosisCheckField)ControlImplementation).Checked = value;
        //        OnPropertyChanged("Checked");
        //    }
        //}

        public GnosisCheckFieldController (
            GnosisCheckField checkField, 
        //    IGnosisCheckFieldImplementation checkFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(checkField, instanceController, parent)
        {
            //checkFieldImplementation.SetCaption(checkField.Caption);

            //if (checkField.ReadOnly)
            //{
            //    checkFieldImplementation.Locked = true;
            //}
        }

        public override void LoadData(int rowNo)
        {

            string datasetName = ((GnosisCheckField)ControlImplementation).Dataset;
            string datasetItemName = ((GnosisCheckField)ControlImplementation).DatasetItem;
            ((GnosisCheckField)ControlImplementation).GnosisChecked = InstanceController.GetDataBool(datasetName, datasetItemName, rowNo);
            isChecked = ((GnosisCheckField)ControlImplementation).GnosisChecked;
        }

        public override void OnLostFocus()
        {
            base.OnLostFocus();

            if (isChecked != ((GnosisCheckField)ControlImplementation).GnosisChecked)
            {
                InstanceController.PushUndo(this, isChecked);
                isChecked = ((GnosisCheckField)ControlImplementation).GnosisChecked;
            }
        }

        public override void Undo(object oldState)
        {
            if (((GnosisCheckField)ControlImplementation).GnosisChecked != (bool)oldState)
            {
                InstanceController.PushRedo(this, ((GnosisCheckField)ControlImplementation).GnosisChecked);

                ((GnosisCheckField)ControlImplementation).GnosisChecked = (bool)oldState;
                isChecked = ((GnosisCheckField)ControlImplementation).GnosisChecked;

            }
        }

        public override void Redo(object newState)
        {
            if (((GnosisCheckField)ControlImplementation).GnosisChecked != (bool)newState)
            {
                InstanceController.PushUndo(this, ((GnosisCheckField)ControlImplementation).GnosisChecked);

                ((GnosisCheckField)ControlImplementation).GnosisChecked = (bool)newState;
                isChecked = ((GnosisCheckField)ControlImplementation).GnosisChecked;
            }
        }

        internal override void Save(int rowNo)
        {
            //bool isChecked = ((IGnosisCheckFieldImplementation)ControlImplementation).GetIsChecked();
            InstanceController.PutDataBool(Dataset, DatasetItem, rowNo, ((GnosisCheckField)ControlImplementation).GnosisChecked);
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisCheckField)ControlImplementation).ReadOnly)
                {
                    ((GnosisCheckField)ControlImplementation).Locked = !Editable;
                }

            }
        }
    }
}
