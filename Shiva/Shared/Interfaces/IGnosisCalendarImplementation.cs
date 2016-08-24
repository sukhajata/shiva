using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisCalendarImplementation : IGnosisInnerLayoutControlImplementation
    {
        string Dataset { get; set; }
        string DatasetItem { get; set; }
        bool ReadOnly { get; set; }
       // int MaxSectionSpan { get; set; }
    }
}
