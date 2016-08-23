using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisListFieldImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        bool HasScrollBar { get; set; }
        bool Locked { get; set; }
        bool ReadOnly { get; set; }
        string Value { get; set; }


        void LoadListOptionImplementations(List<IGnosisComboOptionImplementation> optionImplementations);
    }
}
