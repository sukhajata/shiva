



namespace GnosisControls
{
    public class GnosisAttribute : GnosisControl
    {
        private int linkKeyOrder;

        private string datasetItem;

        private string dataset;

        [GnosisProperty]
        public int LinkKeyOrder
        {
            get { return linkKeyOrder; }
            set { linkKeyOrder = value; }
        }

        [GnosisProperty]
        public string DatasetItem
        {
            get { return datasetItem; }
            set { datasetItem = value; }
        }

        [GnosisProperty]
        public string Dataset
        {
            get { return dataset; }
            set { dataset = value; }
        }
    }
}
