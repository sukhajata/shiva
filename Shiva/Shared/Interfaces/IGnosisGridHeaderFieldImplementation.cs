using ShivaShared3.BaseControllers;



namespace ShivaShared3.Interfaces
{
    public interface IGnosisGridHeaderFieldImplementation : IGnosisCaptionLabelImplementation
    {
        GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment { get; set; }

        IGnosisGridHeaderFieldImplementation GetClone();
        //void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType ha);
        void RemoveBorder();
    }
}
