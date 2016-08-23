



namespace GnosisControls
{
    public class GnosisEventHandlerSource : GnosisControl
    {

        private string sourceControlTypeField;

        private string sourcePropertyField;


        [GnosisProperty]
        public string SourceControlType
        {
            get
            {
                return this.sourceControlTypeField;
            }
            set
            {
                this.sourceControlTypeField = value;
            }
        }

        [GnosisProperty]
        public string SourceProperty
        {
            get
            {
                return this.sourcePropertyField;
            }
            set
            {
                this.sourcePropertyField = value;
            }
        }

        
    }
}