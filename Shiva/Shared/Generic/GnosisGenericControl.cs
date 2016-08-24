using System;
using System.Collections.Generic;
using Shiva.Shared.Events;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Data;

namespace GnosisControls
{
    public class GnosisGenericControl : GnosisControl
    {

        private List<GnosisEventHandler> eventHandlers;
        private List<GnosisGenericInput> genericInputs;
        private List<GnosisGenericOutput> genericOutputs;

        private bool visibleField;

        private bool isEditingField;

        private string genericControlType;

       // private GnosisControl currentInstance;


        [GnosisCollection]
        public List<GnosisEventHandler> EventHandlers
        {
            get
            {
                return this.eventHandlers;
            }
            set
            {
                this.eventHandlers = value;
            }
        }

        [GnosisCollection]
        public List<GnosisGenericInput> GenericInputs
        {
            get { return genericInputs; }
            set { genericInputs = value; }
        }

        [GnosisProperty]
        public List<GnosisGenericOutput> GenericOutputs
        {
            get { return genericOutputs; }
            set { genericOutputs = value; }
        }
        /// <remarks/>
        //[System.Xml.Serialization.XmlAttributeAttribute("ControlType")]
        //public string GenericControlTypeString
        //{
        //    get
        //    {
        //        return this.genericControlTypeString;
        //    }
        //    set
        //    {
        //        this.genericControlTypeString = value;

        //        try
        //        {
        //            this.genericControlType = (GnosisTypeEnum)Enum.Parse(typeof(GnosisTypeEnum), value);
        //        }
        //        catch (Exception ex)
        //        {
        //            GlobalData.Instance.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //        }

        //    }
        //}

        [GnosisProperty]
        public string GenericControlType
        {
            get { return this.genericControlType; }
            set { genericControlType = value; }
        }

        [GnosisProperty]
        public bool Visible
        {
            get
            {
                return this.visibleField;
            }
            set
            {
                this.visibleField = value;
            }
        }

        [GnosisProperty]
        public bool IsEditing
        {
            get
            {
                return isEditingField;
            }
            set
            {
                isEditingField = value;
            }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisGenericInput)
            {
                if (genericInputs == null)
                {
                    genericInputs = new List<GnosisGenericInput>();
                }
                genericInputs.Add((GnosisGenericInput)child);
            }
            else if (child is GnosisGenericOutput)
            {
                if (genericOutputs == null)
                {
                    genericOutputs = new List<GnosisGenericOutput>();
                }
                genericOutputs.Add((GnosisGenericOutput)child);
            }
            else if (child is GnosisEventHandler)
            {
                if (eventHandlers == null)
                {
                    eventHandlers.Add((GnosisEventHandler)child);
                }
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisGenericControl", child.GetType().Name);
            }
        }

        //public GnosisControl FindControlByID(int controlID)
        //{
        //    if (this.ID == controlID)
        //    {
        //        return this;
        //    }
        //    else
        //    {
        //        GnosisControl control = null;

        //        if (GnosisEventHandlers != null)
        //        {
        //            foreach (GnosisEventHandler handler in GnosisEventHandlers)
        //            {
        //                control = handler.FindControlByID(controlID);
        //                if (control != null)
        //                {
        //                    break;
        //                }
        //            }
        //        }

        //        return control;
        //    }
        //}

        //[System.Xml.Serialization.XmlIgnore]
        //public GnosisControl CurrentInstance
        //{
        //    get
        //    {
        //        return currentInstance;
        //    }
        //    set
        //    {
        //        if(value == null)
        //        {
        //            currentInstance = null;
        //        }
        //        else if (value.GnosisControlType == GenericControlType)
        //        {
        //            currentInstance = value;
        //        }
        //        else
        //        {
        //            GlobalData.Instance.ErrorHandler.HandleError("CurrentInstance of GnosisGenericControl with ControlType " + GenericControlType +
        //                " can not be assigned type " + value.GnosisControlType, "GnosisGenericControl.CurrentInstance.Set");
        //        }

        //    }
        //}

    }
}