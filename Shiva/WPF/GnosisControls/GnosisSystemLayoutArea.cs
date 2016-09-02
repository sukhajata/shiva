using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisSystemLayoutArea : GnosisVisibleControl
    {
        private string datasetName;
        private string datasetItemName;
        private int variableSystemID;
        private int variableControlID;
        private bool variableIsInput;
        private bool variableIsOutput;

        [GnosisProperty]
        public string Dataset
        {
            get { return datasetName; }
            set { datasetName = value; }
        }

        [GnosisProperty]
        public string DatasetItem
        {
            get { return datasetItemName; }
            set { datasetItemName = value; }
        }

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
        public bool VariableIsInput
        {
            get { return variableIsInput; }
            set { variableIsInput = value; }
        }

        [GnosisProperty]
        public bool VariableIsOutput
        {
            get { return variableIsOutput; }
            set { variableIsOutput = value; }
        }


    }
}
