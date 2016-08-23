using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisIcon
    {
        private int icon_id;
        private string icon_name;

        [GnosisProperty]
        public int IconID
        {
            get { return icon_id; }
            set { icon_id = value; }
        }

        [GnosisProperty]
        public string IconName
        {
            get { return icon_name; }
            set { icon_name = value; }
        }
    }
}
