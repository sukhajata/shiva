using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisResultsTextFieldImplementation : IGnosisResultsFieldImplementation
    {
        int NumLines { get; set; }

        void SetText(string text);
        void SetTextWrapping(bool v);
    }
}
