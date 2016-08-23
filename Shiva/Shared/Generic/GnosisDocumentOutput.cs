


namespace GnosisControls
{
    public class GnosisDocumentOutput : GnosisControl
    {
        private int systemVariableControlID;

        [GnosisProperty]
        public int SystemVariableControlID
        {
            get { return systemVariableControlID; }
            set { systemVariableControlID = value; }
        }
    }
}
