using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisInstanceVariable : GnosisVariable
    {
        private bool isForCRUD;

        [GnosisProperty]
        public bool IsForCRUD
        {
            get { return isForCRUD; }
            set { isForCRUD = value; }
        }
    }
}
