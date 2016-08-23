
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisLinkEntity : GnosisControl
    {
        private int documentSystemID;

        private int documentEntityID;

        [GnosisProperty]
        public int DocumentSystemID
        {
            get { return documentSystemID; }
            set { documentSystemID = value; }
        }

        [GnosisProperty]
        public int DocumentEntityID
        {
            get { return documentEntityID; }
            set { documentEntityID = value; }
        }

    }
}
