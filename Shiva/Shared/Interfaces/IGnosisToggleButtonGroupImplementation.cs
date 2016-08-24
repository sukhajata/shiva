using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisToggleButtonGroupImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        bool Locked { get; set; }
    }
}
