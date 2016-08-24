using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisGridColumnImplementation : IGnosisContentControlImplementation
    {
        bool IsEvenRow { get; set; }
        bool Locked { get; set; }
        bool ReadOnly { get; set; }
        bool RowSelected { get; set; }
    }
}
