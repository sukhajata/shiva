
using GnosisControls;
using Shiva.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public  class GnosisComboAttribute : GnosisControl
    {
        private ComboRoleType comboRole;

        private string dataset;

        private string datasetItem;


        public enum ComboRoleType
        {
            KEY,
            FILTER
        };

        [GnosisProperty]
        public string Dataset
        {
            get { return dataset; }
            set { dataset = value; }
        }

        [GnosisProperty]
        public string DatasetItem
        {
            get { return datasetItem; }
            set { datasetItem = value; }
        }

        [GnosisProperty]
        public string ComboRole
        {
            get
            {
                return Enum.GetName(typeof(ComboRoleType), comboRole);
            }
            set
            {
                try
                {
                    comboRole = (ComboRoleType)Enum.Parse(typeof(ComboRoleType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public ComboRoleType _ComboRole
        {
            get { return comboRole; }
            set { comboRole = value; }
        }


    }
}
