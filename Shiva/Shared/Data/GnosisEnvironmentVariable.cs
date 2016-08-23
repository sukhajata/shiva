using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisEnvironmentVariable : GnosisVariable
    {
        private string sourceType;

        [GnosisProperty]
        public string SourceType
        {
            get { return sourceType; }
            set { sourceType = value; }
        }


    }
}
