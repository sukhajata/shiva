


namespace GnosisControls
{
    public class GnosisVariable : GnosisControl
    {
        private string dataType;
        private string sqlDataType;
        private string sqlParameterName;
        private string defaultValue;
        private bool isInstanceInput;
        private bool isEntityInput;
        private bool isInstanceOutput;
        

        [GnosisProperty]
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        [GnosisProperty]
        public string SqlDataType
        {
            get { return sqlDataType; }
            set { sqlDataType = value; }
        }

        [GnosisProperty]
        public string SqlParameterName
        {
            get { return sqlParameterName; }
            set { sqlParameterName = value; }
        }

        [GnosisProperty]
        public string Default
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        [GnosisProperty]
        public bool IsInstanceInput
        {
            get { return isInstanceInput; }
            set { isInstanceInput = value; }
        }

        [GnosisProperty]
        public bool IsInstanceOutput
        {
            get { return isInstanceOutput; }
            set { isInstanceOutput = value; }
        }

        [GnosisProperty]
        public bool IsEntityInput
        {
            get { return isEntityInput; }
            set { isEntityInput = value; }
        }

    }
}
