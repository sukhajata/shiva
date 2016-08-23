using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisLinkColumn : GnosisGridColumn
    {
        private int documentSystemID;
        private int documentEntityID;
        private string perspective;

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

        [GnosisProperty]
        public string Perspective
        {
            get { return perspective; }
            set { perspective = value; }
        }
    }
}
