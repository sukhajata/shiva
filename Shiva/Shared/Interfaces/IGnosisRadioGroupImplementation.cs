using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisRadioGroupImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        bool Locked { get; set; }
    }
}
