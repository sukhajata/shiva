using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisGridTextFieldImplementation : IGnosisGridFieldImplementation
    {
        //properties
        int NumLines { get; set; }

        void SetText(string attribute);
        string GetText();
        void SetTextWrapping(bool wrap);
        void SetMaxLines(int maxLines);
    }
}
