



namespace GnosisControls
{
    public class GnosisItem : GnosisControl
    {
        private string tooltip;

        [GnosisProperty]
        public string Tooltip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }
    }
}
