using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisPermissionVariable : GnosisVariable
    {
        private string variableCode;

        [GnosisProperty]
        public string VariableCode
        {
            get { return variableCode; }
            set { variableCode = value; }
        }

    }
}
