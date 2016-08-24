using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisResultsCheckFieldImplementation : IGnosisResultsFieldImplementation
    {
        bool GnosisChecked { get; set; }
        //void SetChecked(bool check);
    }
}
