using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisRadioFieldImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        bool Locked { get; set; }
    }
}
