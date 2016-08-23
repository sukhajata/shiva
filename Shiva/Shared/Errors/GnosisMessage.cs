using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisMessage : IGnosisObject
    {
        private string errorLevel;

        private string errorMessage;

        private int errorOrder;

        [GnosisProperty]
        public string ErrorLevel
        {
            get { return errorLevel; }
            set { errorLevel = value; }
        }

        [GnosisProperty]
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        [GnosisProperty]
        public int ErrorOrder
        {
            get { return errorOrder; }
            set { errorOrder = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
