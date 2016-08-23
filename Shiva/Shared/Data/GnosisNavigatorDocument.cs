using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisNavigatorDocument : GnosisControl
    {
        private int navigatorSystemID;
        private int navigatorEntityID;

        [GnosisProperty]
        public int NavigatorSystemID
        {
            get { return navigatorSystemID; }
            set { navigatorSystemID = value; }
        }

        [GnosisProperty]
        public int NavigatorEntityID
        {
            get { return navigatorEntityID; }
            set { navigatorEntityID = value; }
        }

    }
}
