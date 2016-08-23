


namespace GnosisControls
{
    public class GnosisSearchParameter : GnosisControl
    {
        private string dataset;

        private string datasetItem;

        private string parameter;

        private string content;

        private bool isOutput;

        private bool isInput;

        private string gnosisValue;

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
        public string Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }
        
        public string Content
        {
            get { return content; }
            set { content = value; }
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
        public string Value
        {
            get { return gnosisValue; }
            set { gnosisValue = value; }
        }

    }
}
