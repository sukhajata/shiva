using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisComboFieldImplementation :  IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        int DocumentSystemID { get; set; }
        int DocumentEntityID { get; set; }
        bool Locked { get; set; }
        bool Optional { get; set; }
        bool ReadOnly { get; set; }
        string Value { get; set; }
        int VariableControlID { get; set; }
        int VariableSystemID { get; set; }
        bool VariableIsInput { get; set; }
        bool VariableIsOutput { get; set; }

        void SetOptionChangedHandler(Action<GnosisComboOption> optionChangedHandler);
        void LoadComboOptionImplementations(List<GnosisComboOption> comboOptionImplementations);
        void SetSelectedOption(GnosisComboOption selectedOption);
        GnosisComboOption GetSelectedOption();
    }
}
