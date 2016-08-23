﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GnosisControls;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.Events;
using ShivaShared3.Utility;
using ShivaShared3.InnerLayoutControllers;
using ShivaShared3.ContentControllers;
using ShivaShared3.Data;

namespace ShivaShared3.PanelFieldControllers
{
    public class GnosisTextFieldController : GnosisPanelFieldController
    {
        private string currentText;
        private int numLines;
        private double textHeight;
        protected double paddingVertical;


        //public int MaxDisplayWidthChars
        //{
        //    get { return ((GnosisTextField)ControlImplementation).MaxDisplayWidthChars; }
        //}

        public int NumLines
        {
            get { return numLines; }
            set
            {
                numLines = value;

                if (numLines > 1)
                {
                    ((IGnosisTextFieldImplementation)ControlImplementation).SetTextWrapping(true);
                }
                else
                {
                    ((IGnosisTextFieldImplementation)ControlImplementation).SetTextWrapping(false);
                }
            }
        }

        public double TextHeight
        {
            get { return textHeight; }
            set { textHeight = value; }
        }

        public double PaddingVertical
        {
            get { return paddingVertical; }
            set { paddingVertical = value; }
        }


        public GnosisTextFieldController(
            GnosisTextField textField, 
         //   IGnosisTextFieldImplementation textFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(textField, instanceController, parent)
        {
            SetDisplayDimensions();
            textField.SetTextChangedHandler(new Action<string>(TextChanged));
            numLines = 1;

            //textField.SetVerticalAlignment(VerticalAlignmentType.TOP)

            //if (MaxChars > 0)
            //{
            //    textFieldImplementation.SetMaxChars(MaxChars);
            //}

            //if (textField.ReadOnly)
            //{
            //    textFieldImplementation.Locked = true;
            //}

        }

        public void TextChanged(string text)
        {
            InstanceController.SetDataUpdated(Dataset, 0);
        }

        protected override void EnableGenericEvent(GnosisEventHandler.GnosisEventType eventType)
        {
            switch (eventType)
            {
                
                default:
                    base.EnableGenericEvent(eventType);
                    break;
            }
        }

        public override void LoadData(int rowNo)
        {

            string datasetName = ((GnosisTextField)ControlImplementation).Dataset;
            string datasetItemName = ((GnosisTextField)ControlImplementation).DatasetItem;

            if (InstanceController != null)
            {
                currentText = InstanceController.GetDataString(datasetName, datasetItemName, rowNo);
                ((IGnosisTextFieldImplementation)ControlImplementation).SetText(currentText);
            }
        }

        internal override void Save(int rowNo)
        {
            if (InstanceController != null)
            {
                string value = ((IGnosisTextFieldImplementation)ControlImplementation).GetText();
                InstanceController.PutDataString(Dataset, DatasetItem, rowNo, value);
            }
        }

        public override void Undo(object oldState)
        {
            string currentText = ((IGnosisTextFieldImplementation)ControlImplementation).GetText();
            ((IGnosisTextFieldImplementation)ControlImplementation).SetText((String)oldState);

            InstanceController.PushRedo(this, currentText);
        }

        public override void Redo(object oldState)
        {
            string currentText = ((IGnosisTextFieldImplementation)ControlImplementation).GetText();
            ((IGnosisTextFieldImplementation)ControlImplementation).SetText((String)oldState);

            InstanceController.PushUndo(this, currentText);
        }


        public override void OnLostFocus()
        {
            string newText = ((IGnosisTextFieldImplementation)ControlImplementation).GetText();

            if (!newText.Equals(currentText))
            {
                InstanceController.PushUndo(this, currentText); //save old text for undo event
                currentText = newText;
            }
        }

        protected override void SetDisplayDimensions()
        {
            base.SetDisplayDimensions();

            string font = EntityController.GetNormalStyle().Font;
            int fontSize = EntityController.GetNormalStyle().FontSize;

            if (((GnosisTextField)ControlImplementation).MaxTextDisplayWidthChars > 0)
            {
                MaxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxFieldWidth(
                    (IGnosisTextFieldImplementation)ControlImplementation, 
                    font, 
                    fontSize,
                    ((GnosisTextField)ControlImplementation).MaxTextDisplayWidthChars);
            }
            else if (((GnosisTextField)ControlImplementation).MaxChars > 0)
            {
                MaxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxFieldWidth(
                    (IGnosisTextFieldImplementation)ControlImplementation,
                    font,
                    fontSize,
                    ((GnosisTextField)ControlImplementation).MaxChars);
            }

            //textHeight = GlobalData.Singleton.StyleHelper.GetTextHeight((IGnosisTextFieldImplementation)ControlImplementation, font, fontSize);
            //paddingVertical = (FieldHeight - textHeight)/2;

            //((IGnosisTextFieldImplementation)ControlImplementation).SetPaddingVertical(paddingVertical);
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisTextField)ControlImplementation).ReadOnly)
                {
                    ((IGnosisTextFieldImplementation)ControlImplementation).Locked = !Editable;
                }

            }
        }

    }
}
