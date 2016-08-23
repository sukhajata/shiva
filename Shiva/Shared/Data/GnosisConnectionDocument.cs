using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisConnectionDocument : GnosisControl
    {
        private int connectionSystemID;
        private int connectionEntityID;

        [GnosisProperty]
        public int ConnectionSystemID
        {
            get { return connectionSystemID; }
            set { connectionSystemID = value; }
        }

        [GnosisProperty]
        public int ConnectionEntityID
        {
            get { return connectionEntityID; }
            set { connectionEntityID = value; }
        }
    }
}
