using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisGenericToggleMenuItem  : GnosisGenericMenuItem
    {

        private int variableSystemID;

        private int variableControlID;

        private int code;

        private bool active;

        private int selectedFactor;

        [GnosisProperty]
        public int VariableSystemID
        {
            get { return variableSystemID; }
            set { variableSystemID = value; }
        }

        [GnosisProperty]
        public int VariableControlID
        {
            get { return variableControlID; }
            set { variableControlID = value; }
        }

        [GnosisProperty]
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        [GnosisProperty]
        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                OnPropertyChanged("Active");
            }
        }

        [GnosisProperty]
        public int SelectedFactor
        {
            get { return selectedFactor; }
            set { selectedFactor = value; }
        }

    }
}
