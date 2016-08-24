using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisResultsTextFieldImplementation : IGnosisResultsFieldImplementation
    {
        int NumLines { get; set; }

        void SetText(string text);
        void SetTextWrapping(bool v);
    }
}
