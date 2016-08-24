using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public partial class GnosisGallerySearchItem : GnosisGalleryItem
    {
        private string autoSearchAction;

        private int searchSystemID;

        private int searchEntityID;

        private string searchAction;

        private List<GnosisSearchParameter> searchParameters;

        [GnosisProperty]
        public string AutoSearchAction
        {
            get { return autoSearchAction; }
            set { autoSearchAction = value; }
        }

        [GnosisProperty]
        public int SearchSystemID
        {
            get { return searchSystemID; }
            set { searchSystemID = value; }
        }

        [GnosisProperty]
        public int SearchEntityID
        {
            get { return searchEntityID; }
            set { searchEntityID = value; }
        }

        [GnosisProperty]
        public string SearchAction
        {
            get { return searchAction; }
            set { searchAction = value; }
        }

        [GnosisCollection]
        public List<GnosisSearchParameter> SearchParameters
        {
            get { return searchParameters; }
            set { searchParameters = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisSearchParameter)
            {
                if (searchParameters == null)
                {
                    searchParameters = new List<GnosisSearchParameter>();
                }

                searchParameters.Add((GnosisSearchParameter)child);
            }
            else
            {
                base.GnosisAddChild(child);
            }
        }

    }
}
