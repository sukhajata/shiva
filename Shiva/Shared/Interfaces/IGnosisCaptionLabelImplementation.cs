using System.Windows;
using System.Windows.Media;
using Shiva.Shared.BaseControllers;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisCaptionLabelImplementation : IGnosisControlThicknessPossessor
    {
        string Caption { get; set; }
        int MaxLines { get; set; }
        GnosisController.VerticalAlignmentType CaptionVerticalAlignment { get; set; }
        GnosisController.HorizontalAlignmentType CaptionHorizontalAlignment { get; set; }
        GnosisController.CaptionPosition RelativePosition { get; set; }
        int ControlThickness { get; set; }
        int CaptionSpacing { get; set; }
        int HorizontalPadding { get;  set; }
        int HorizontalMargin { get; set; }
        int VerticalMargin { get; set; }
        int VerticalPadding { get; set; }

        double GetWidth();
        
    }
}
