using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisRadioGroupImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        bool Locked { get; set; }
    }
}
