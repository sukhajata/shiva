using System;
using System.Collections.Generic;
using System.Text;

using GnosisControls;
using Shiva.Shared.Interfaces;
using Shiva.Shared.DataControllers;
using System.IO;
using Shiva.Shared.Data;
using Shiva.Shared.InnerLayoutControllers;
using System.Linq;
using System.Xml.Linq;
using Shiva.Shared.Utility;

namespace Shiva.Shared.PanelFieldControllers
{
    public class GnosisComboFieldController : GnosisPanelFieldController
    {
        private Dictionary<GnosisComboOption, List<GnosisKey>> comboOptionImplementations;
        private GnosisComboOption currentComboOptionImplementation;

        public GnosisComboFieldController (
            GnosisComboField comboField, 
        //    IGnosisComboFieldImplementation comboFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(comboField, instanceController, parent)
        {
            comboField.SetOptionChangedHandler(new Action<GnosisComboOption>(OptionChanged));

            //if (comboField.ReadOnly)
            //{
            //    comboFieldImplementation.Locked = true;
            //}

            comboField.Optional = EntityController.GetDatasetItemOptional(Dataset, DatasetItem);

            SetDisplayDimensions();
        }

        public void Setup()
        {
           
        }

        public override void OnLostFocus()
        {
            base.OnLostFocus();

            GnosisComboOption selectedOption = ((GnosisComboField)ControlImplementation).GetSelectedOption();

            if (selectedOption != currentComboOptionImplementation)
            {
                InstanceController.PushUndo(this, currentComboOptionImplementation);
                currentComboOptionImplementation = selectedOption;
            }
        }

        public void OptionChanged(IGnosisComboOptionImplementation newOption)
        {
            //if (newOption != currentComboOptionImplementation)
            //{
            //    //InstanceController.PushUndo(this, currentComboOptionImplementation);
            //    currentComboOptionImplementation = newOption;
            //}
        }

        public override void LoadData(int rowNo)
        {
            if (InstanceController != null)
            {
                string datasetName = ((GnosisComboField)ControlImplementation).Dataset;
                string datasetItemName = ((GnosisComboField)ControlImplementation).DatasetItem;

                XElement optionsXML = InstanceController.GetOptionsXML(datasetName, datasetItemName);
                string valueAttribute = InstanceController.GetSourceAttributeName(datasetName, datasetItemName);
                string selected = InstanceController.GetDataString(datasetName, datasetItemName, rowNo);

                comboOptionImplementations = new Dictionary<GnosisComboOption, List<GnosisKey>>();
                //IGnosisComboOptionImplementation selectedOption = null;
                int i = 1;
                foreach (var row in optionsXML.Descendants())
                {
                    GnosisComboOption comboOptionImp = GnosisControlCreator.CreateGnosisComboOption(i);//GlobalData.Singleton.ImplementationCreator.GetGnosisComboOptionImplementation();
                    string display = row.Attribute(valueAttribute).Value;
                    comboOptionImp.SetText(display);
                    if (display.Equals(selected))
                    {
                        currentComboOptionImplementation = comboOptionImp;
                    }

                    List<GnosisKey> keys = new List<GnosisKey>();
                    foreach (GnosisComboAttribute comboAttribute in ((GnosisComboField)ControlImplementation).ComboAttributes.Where(a => a._ComboRole == GnosisComboAttribute.ComboRoleType.KEY))
                    {
                        string targetAttribute = InstanceController.GetTargetAttributeName(comboAttribute.Dataset, comboAttribute.DatasetItem);
                        string sourceAttribute = InstanceController.GetKeyOptionSourceAttributeName(comboAttribute.Dataset, comboAttribute.DatasetItem);
                        string keyString = row.Attribute(sourceAttribute).Value;
                        GnosisKey key = new GnosisKey(comboAttribute.Dataset, comboAttribute.DatasetItem, sourceAttribute, targetAttribute, keyString);
                        keys.Add(key);
                    }

                    comboOptionImplementations.Add(comboOptionImp, keys);
                    i++;

                }

                //List<string> values = InstanceController.GetOptionsList(datasetName, datasetItemName);
                //string selected = InstanceController.GetDataString(datasetName, datasetItemName, rowNo);

                //comboOptionImplementations = new List<IGnosisComboOptionImplementation>();
                //IGnosisComboOptionImplementation selectedOption = null;

                //foreach (string option in values)
                //{
                //    IGnosisComboOptionImplementation comboOptionImp = GlobalData.Singleton.ImplementationCreator.GetGnosisComboOptionImplementation();
                //    comboOptionImplementations.Add(comboOptionImp);
                //    //comboOptionImp.LoadItem(option);
                //    comboOptionImp.SetText(option);
                //    if (option.Equals(selected))
                //    {
                //        selectedOption = comboOptionImp;
                //    }
                //}

                List<GnosisComboOption> comboOptions = new List<GnosisComboOption>();

                foreach (GnosisComboOption option in comboOptionImplementations.Keys)
                {
                    comboOptions.Add(option);
                }

                ((GnosisComboField)ControlImplementation).LoadComboOptionImplementations(comboOptions);

                if (currentComboOptionImplementation != null)
                {
                    ((GnosisComboField)ControlImplementation).SetSelectedOption(currentComboOptionImplementation);
                }

            }
        }

        public override void Undo(object oldState)
        {
            ((GnosisComboField)ControlImplementation).SetSelectedOption((GnosisComboOption)oldState);

            InstanceController.PushRedo(this, currentComboOptionImplementation);
            currentComboOptionImplementation = (GnosisComboOption)oldState;
        }

        public override void Redo(object oldState)
        {
            ((GnosisComboField)ControlImplementation).SetSelectedOption((GnosisComboOption)oldState);
            InstanceController.PushUndo(this, currentComboOptionImplementation);
            currentComboOptionImplementation = (GnosisComboOption)oldState;
        }


        internal override void Save(int rowNo)
        {
            string datasetName = ((GnosisComboField)ControlImplementation).Dataset;
            string datasetItemName = ((GnosisComboField)ControlImplementation).DatasetItem;

            GnosisComboOption selectedOption = ((GnosisComboField)ControlImplementation).GetSelectedOption();
            string value = selectedOption.GetText();

            InstanceController.PutDataString(datasetName, datasetItemName, rowNo, value);

            List<GnosisKey> keys = comboOptionImplementations[selectedOption];
            foreach (GnosisKey key in keys)
            {
                InstanceController.PutDataString(key.Dataset, key.DatasetItem, rowNo, key.KeyValue);
            }
        }


        protected override void SetDisplayDimensions()
        {
            base.SetDisplayDimensions();

            minFieldWidth = minFieldWidth + 15; //allow for drop down button

            if (minFieldWidth > maxFieldWidth)
            {
                maxFieldWidth = minFieldWidth;
            }
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisComboField)ControlImplementation).ReadOnly)
                {
                    ((GnosisComboField)ControlImplementation).Locked = !Editable;
                }

            }
        }

    }
}
