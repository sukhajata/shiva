using GnosisControls;



namespace GnosisControls
{
    public class GnosisDocumentInput : GnosisControl
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
