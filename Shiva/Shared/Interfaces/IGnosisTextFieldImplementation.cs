using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisTextFieldImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        bool Locked { get; set; }
        int MaxChars { get; set; }
        int MaxSectionSpan { get; set; }
        int MaxTextDisplayWidthChars { get; set; }
        int MinTextDisplayWidthChars { get; set; }
        bool Optional { get; set; }
        bool ReadOnly { get; set; }
        string Value { get; set; }
        int VariableControlID { get; set; }
        int VariableSystemID { get; set; }
        bool VariableIsInput { get; set; }
        bool VariableIsOutput { get; set; }

        //methods
        void SetText(string text);
        string GetText();
        void SetMaxLines(int maxLines);
        int GetRowNumber();
        //void SetMaxChars(int maxCharacters);
        int GetLineCount();
        double GetHeight();
        void SetTextChangedHandler(Action<string> action);
        void SetTextWrapping(bool v);

    }
}
