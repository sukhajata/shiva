using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisControl : IGnosisObject
    {
        string ControlType { get; set; }
        int ID { get; set; }
        string GnosisName { get; set; }
        int Order { get; set; }
    }
}
