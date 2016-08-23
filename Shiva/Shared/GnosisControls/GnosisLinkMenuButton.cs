
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    
    public partial class GnosisLinkMenuButton : GnosisVisibleControl
    {
        private bool disabled;

        private List<GnosisLinkButtonItem> buttonItems;

        [GnosisProperty]
        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }

        [GnosisCollection]
        public List<GnosisLinkButtonItem> LinkButtonItems
        {
            get { return buttonItems; }
            set { buttonItems = value; }
        }

    }
}
