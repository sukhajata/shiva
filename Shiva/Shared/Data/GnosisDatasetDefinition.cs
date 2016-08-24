using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisDatasetDefinition : IGnosisObject
    {
        private string name;

        private string element;

        private string path;

        private bool allowRead;

        private bool allowCreate;

        private bool allowUpdate;

        private bool allowDelete;

        private List<GnosisDatasetDefinition> datasets;

        private List<GnosisDatasetItem> datasetItems;

        [GnosisProperty]
        public string GnosisName
        {
            get { return name; }
            set { name = value; }
        }

        [GnosisProperty]
        public string Element
        {
            get { return element; }
            set { element = value; }
        }

        [GnosisProperty]
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        [GnosisProperty]
        public bool AllowRead
        {
            get { return allowRead; }
            set { allowRead = value; }
        }

        [GnosisProperty]
        public bool AllowCreate
        {
            get { return allowCreate; }
            set { allowCreate = value; }
        }

        [GnosisProperty]
        public bool AllowUpdate
        {
            get { return allowUpdate; }
            set { allowUpdate = value; }
        }

        [GnosisProperty]
        public bool AllowDelete
        {
            get { return allowDelete; }
            set { allowDelete = value; }
        }

        [GnosisCollection]
        public List<GnosisDatasetDefinition> DatasetDefinitions
        {
            get { return datasets; }
            set { datasets = value; }
        }

        [GnosisCollection]
        public List<GnosisDatasetItem> DatasetItems
        {
            get { return datasetItems; }
            set { datasetItems = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisDatasetDefinition)
            {
                if (datasets == null)
                {
                    datasets = new List<GnosisDatasetDefinition>();
                }
                datasets.Add((GnosisDatasetDefinition)child);
            }
            else if (child is GnosisDatasetItem)
            {
                if (datasetItems == null)
                {
                    datasetItems = new List<GnosisDatasetItem>();
                }
                datasetItems.Add((GnosisDatasetItem)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisDatasetDefinition", child.GetType().Name);
            }
        }
    }
}
