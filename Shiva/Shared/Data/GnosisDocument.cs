



namespace GnosisControls
{
    public class GnosisDocument : GnosisControl
    {
        private int documentEntityID;
        private int documentControlID;

        [GnosisProperty]
        public int DocumentEntityID
        {
            get { return documentEntityID; }
            set { documentEntityID = value; }
        }

        [GnosisProperty]
        public int DocumentControlID
        {
            get { return documentControlID; }
            set { documentControlID = value; }
        }

    }
}
