using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisDataSystemReference : IGnosisObject
    {
        private int systemID;
        private string systemName;
        private string systemURL;
        private int versionNo;

        [GnosisProperty]
        public int SystemID
        {
            get { return systemID; }
            set { systemID = value; }
        }

        [GnosisProperty]
        public string SystemName
        {
            get { return systemName; }
            set { systemName = value; }
        }

        [GnosisProperty]
        public string SystemURL
        {
            get { return systemURL; }
            set { systemURL = value; }
        }

        [GnosisProperty]
        public int VersionNo
        {
            get { return versionNo; }
            set { versionNo = value; }
        }
            

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
