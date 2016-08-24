using Shiva.Shared.BaseControllers;



namespace Shiva.Shared.Interfaces
{
    public interface IGnosisContentControlImplementation : IGnosisMouseVisibleControlImplementation
    {
        //properties
       // int ColSpan { get; set; }
        string ContentVerticalAlignment { get; set; }
        GnosisController.VerticalAlignmentType _ContentVerticalAlignment { get; set; }
        string ContentHorizontalAlignment { get; set; }
        GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment { get; set; }
        bool DatasetCreated { get; set; }
        bool DatasetUpdated { get; set; }
        bool DatasetDeleted { get; set; }
        string Dataset { get; set; }
        string DatasetItem { get; set; }
        //List<GnosisEventHandler> eventHandlers;
       
        //int MaxChars { get; set; }



        //methods
        //void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType verticalAlignment);
        //void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment);
        void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment);
        //void SetMinWidth(double minWidth);
        //void SetMaxWidth(double maxWidth);
        void SetWidth(double width);
        void SetHeight(double fieldHeight);
        void SetVerticalAlignment(GnosisController.VerticalAlignmentType top);
        void SetStrikethrough(bool strikethrough);
        //void SetMaxWidth(int maxWidth);
        //void SetWidth(int width);
        //  void SetCaptionRelativePosition(GnosisController.CaptionPosition captionRelativePosition);
        // void SetCaptionAlignmentHorizontal(GnosisController.HorizontalAlignmentType captionAlignmentHorizontal);
        // void SetCaptionAlignmentVertical(GnosisController.VerticalAlignmentType captionAlignmentVertical);
        //   string GetCaption();
        //void SetCaptionSpan(double captionCellSpan, int totalCellSpan);
    }
}
