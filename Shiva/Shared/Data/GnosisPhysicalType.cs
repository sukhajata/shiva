using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisPhysicalType : IGnosisObject
    {
        private string physicalTypeName;

        [GnosisProperty]
        public string physical_type_name
        {
            get { return physicalTypeName; }
            set { physicalTypeName = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
