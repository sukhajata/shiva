using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.DataControllers;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.Data;

namespace Shiva.Shared.PanelFieldControllers
{
    public class GnosisNumberFieldController : GnosisPanelFieldController
    {
        private double currentNumber;
        private double textHeight;
        protected double paddingVertical;

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

        public GnosisNumberFieldController(
            GnosisNumberField numberField,
          //  IGnosisTextFieldImplementation textFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(numberField, instanceController, parent)
        {
            SetDisplayDimensions();
            numberField.SetTextChangedHandler(new Action<string>(TextChanged));
        }

        public void TextChanged(string text)
        {
            InstanceController.SetDataUpdated(Dataset, 0);
        }

        public override void LoadData(int rowNo)
        {

            string datasetName = ((GnosisNumberField)ControlImplementation).Dataset;
            string datasetItemName = ((GnosisNumberField)ControlImplementation).DatasetItem;

            if (InstanceController != null)
            {
                currentNumber = InstanceController.GetDataDouble(datasetName, datasetItemName, rowNo);
                ((GnosisNumberField)ControlImplementation).SetNumber(currentNumber);
            }
        }

        protected override void SetDisplayDimensions()
        {
            base.SetDisplayDimensions();

            string font = EntityController.GetNormalStyle().Font;
            int fontSize = EntityController.GetNormalStyle().FontSize;

            if (MaxFieldWidth == 0)
            {
                if (((GnosisNumberField)ControlImplementation).MaxChars == 0)
                {
                    ((GnosisNumberField)ControlImplementation).MaxChars = EntityController.GetMaxChars(Dataset, DatasetItem);
                }

                if (((GnosisNumberField)ControlImplementation).MaxChars > 0)
                {
                    MaxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxFieldWidth(
                      (GnosisNumberField)ControlImplementation,
                      font,
                      fontSize,
                      ((GnosisNumberField)ControlImplementation).MaxChars);
                }

            }

            //textHeight = GlobalData.Singleton.StyleHelper.GetTextHeight((IGnosisVisibleControlImplementation)ControlImplementation, font, fontSize);
            //paddingVertical = (FieldHeight - textHeight) / 2;

            //((IGnosisVisibleControlImplementation)ControlImplementation).SetPaddingVertical(paddingVertical);
        }

        internal override void Save(int rowNo)
        {
            if (InstanceController != null)
            {
                double value = ((GnosisNumberField)ControlImplementation).GetNumber();
                InstanceController.PutDataString(Dataset, DatasetItem, rowNo, value.ToString());
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
            double newNumber = ((GnosisNumberField)ControlImplementation).GetNumber();

            if (newNumber != currentNumber)
            {
                InstanceController.PushUndo(this, currentNumber); //save old text for undo event
                currentNumber = newNumber;
            }
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisNumberField)ControlImplementation).ReadOnly)
                {
                    ((GnosisNumberField)ControlImplementation).Locked = !Editable;
                }

            }
        }

    }
}
