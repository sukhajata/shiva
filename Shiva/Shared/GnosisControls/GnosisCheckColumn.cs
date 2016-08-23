using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisCheckColumn : GnosisGridColumn
    {
        private string groupName;
        private bool gnosisChecked;
        private int checkedFactor;
        private string valueField;

        [GnosisPropertyAttribute]
        public string GnosisGroupName
        {
            get
            {
                return groupName;
            }

            set
            {
                groupName = value;
            }
        }

        public bool GnosisChecked
        {
            get { return gnosisChecked; }

            set { gnosisChecked = value; }
        }

        [GnosisPropertyAttribute]
        public int CheckedFactor
        {
            get
            {
                return checkedFactor;
            }

            set
            {
                checkedFactor = value;
            }
        }

      

    }
}
