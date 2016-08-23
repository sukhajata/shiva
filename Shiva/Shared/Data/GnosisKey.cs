using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisKey
    {
        private string dataset;

        private string datasetItem;

        private string sourceAttribute;

        private string targetAttribute;

        private string keyValue;

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
        public string SourceAttribute
        {
            get { return sourceAttribute; }
            set
            {
                sourceAttribute = value;
            }
        }

        [GnosisProperty]
        public string TargetAttribute
        {
            get { return targetAttribute; }
            set { targetAttribute = value; }
        }

        [GnosisProperty]
        public string KeyValue
        {
            get { return keyValue; }
            set { keyValue = value; }

        }

        public GnosisKey(string _dataset, string _datasetItem, string _sourceAttribute, string _targetAttribute, string _keyValue)
        {
            dataset = _dataset;
            datasetItem = _datasetItem;
            sourceAttribute = _sourceAttribute;
            targetAttribute = _targetAttribute;
            keyValue = _keyValue;
        }


    }
}
