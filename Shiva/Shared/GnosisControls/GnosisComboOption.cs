using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{

    public partial class GnosisComboOption
    {
        private string code;
        private string controlType;
        private string gnosisName;
        private int id;
        private int order;

        [GnosisPropertyAttribute]
        public string ControlType
        {
            get
            {
                return controlType;
            }

            set
            {
                controlType = value;
            }
        }

        [GnosisProperty]
        public string GnosisName
        {
            get
            {
                return gnosisName;
            }

            set
            {
                gnosisName = value;
            }
        }


        [GnosisPropertyAttribute]
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                // OnPropertyChanged("ID");
            }
        }

        [GnosisPropertyAttribute]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
                //OnPropertyChanged("Order");
            }
        }

        [GnosisProperty]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }

}
