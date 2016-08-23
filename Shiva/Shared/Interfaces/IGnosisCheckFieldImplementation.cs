using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisCheckFieldImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        int CheckedFactor { get; set; }
        string GnosisGroupName { get; set; }
        bool Locked { get; set; }
        bool GnosisChecked { get; set; }
        bool ReadOnly { get; set; }
        string Value { get; set; }
        int VariableControlID { get; set; }
        int VariableSystemID { get; set; }
        bool VariableIsInput { get; set; }
        bool VariableIsOutput { get; set; }


        //void SetIsChecked(bool isChecked);
        //void SetCaption(string caption);
        //bool GetIsChecked();
    }
}
