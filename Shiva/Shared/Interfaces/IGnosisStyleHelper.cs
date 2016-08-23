using GnosisControls;
using ShivaShared3.BaseControllers;
using ShivaShared3.ContainerControllers;
using ShivaShared3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisStyleHelper
    {
        void ApplyStyle(IGnosisVisibleControlImplementation control, GnosisStyle gnosisStyle);
        // void ApplyStyle(IGnosisVisibleControlImplementation control, IGnosisVisibleControlImplementation bindingSource, GnosisVisibleController controller, GnosisStyle gnosisStyle);
        double GetFieldHeight(IGnosisCaptionLabelImplementation caption, string font, int fontSize);
        double GetFieldHeight(IGnosisContentControlImplementation gnosisControl, string font, int fontSize);
        double GetFieldHeight(IGnosisGridFieldImplementation gridField, string font, int fontSize);
        double GetCharacterWidth(IGnosisContentControlImplementation gnosisControl, string font, int fontSize);
        double GetCharacterWidth(IGnosisTextAreaImplementation textArea, string font, int fontSize);
        double GetCharacterWidthNumeric(IGnosisContentControlImplementation gnosisControl);
        double GetMinFieldWidth(IGnosisContentControlImplementation control, string font, int fontSize, int minChars);
        double GetMaxFieldWidth(IGnosisContentControlImplementation control, string font, int fontSize, int maxCharacters);
        double GetMinTextAreaWidth(IGnosisTextAreaImplementation textArea, string font, int fontSize, int minChars);
        double GetMaxTextAreaWidth(IGnosisTextAreaImplementation textArea, string font, int fontSize, int maxChars);
       // void CloneStyle(IGnosisVisibleControlImplementation receiver, IGnosisVisibleControlImplementation source);
        //void CloneCaptionStyle(IGnosisCaptionLabelImplementation receiver, IGnosisCaptionLabelImplementation source);
        double GetCharacterWidth(string font, int fontSize);
        void ApplyFontStyle(IGnosisContentControlImplementation txt, GnosisStyle gnosisStyle);
        void ApplyCaptionStyle(IGnosisCaptionLabelImplementation header, GnosisStyle captionStyle);
        double GetTextHeight(IGnosisVisibleControlImplementation ControlImplementation, string font, int fontSize);
        double GetTextHeight(IGnosisTextAreaImplementation textArea, string font, int fontSize);
        double GetTextHeight(IGnosisGridTextFieldImplementation gridTextField, string font, int fontSize);
        double GetTextHeight(IGnosisResultsTextFieldImplementation textResultsField, string font, int fontSize);
    }
}
