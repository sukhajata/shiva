using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisListFieldImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor,
        IGnosisCaptionLabelPossessor
    {
        //properties
        bool HasScrollBar { get; set; }
        bool Locked { get; set; }
        bool ReadOnly { get; set; }
        string Value { get; set; }


        void LoadListOptionImplementations(List<IGnosisComboOptionImplementation> optionImplementations);
    }
}
