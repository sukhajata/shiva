using Shiva.Shared.BaseControllers;



namespace Shiva.Shared.Interfaces
{
    public interface IGnosisGridHeaderFieldImplementation : IGnosisCaptionLabelImplementation
    {
        GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment { get; set; }

        IGnosisGridHeaderFieldImplementation GetClone();
        //void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType ha);
        void RemoveBorder();
    }
}
