using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisToggleButtonGroupImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        bool Locked { get; set; }
    }
}
