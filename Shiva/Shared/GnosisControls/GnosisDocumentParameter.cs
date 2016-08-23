using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisDocumentParameter : GnosisControl
    {
        private string parameter;
        private bool isInput;
        private bool isOutput;
        private string valueField;
        private string dataset;
        private string datasetItem;


        [GnosisProperty]
        public string Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        [GnosisProperty]
        public bool IsInput
        {
            get { return isInput; }
            set { isInput = value; }
        }

        [GnosisProperty]
        public bool IsOutput
        {
            get { return isOutput; }
            set { isOutput = value; }
        }

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


    }
}
