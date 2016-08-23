using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisDataKey : IGnosisObject
    {
        private string gnosisName;
        private int keyOrder;
        private string dataset;
        private string datasetItem;

        [GnosisProperty]
        public string GnosisName
        {
            get { return gnosisName; }
            set { gnosisName = value; }
        }

        [GnosisProperty]
        public int KeyOrder
        {
            get { return keyOrder; }
            set { keyOrder = value; }
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

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
