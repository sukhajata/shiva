using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisGenericInput : GnosisControl
    {
        private int variableSystemID;
        private int variableControlID;


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

    }
}
