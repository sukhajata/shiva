﻿using Shiva.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisDateFieldImplementation : IGnosisPanelFieldImplementation, IGnosisCaptionLabelPossessor
    {
        //properties
        bool Locked { get; set; }
        bool LongDateFormat { get; set; }
        bool ReadOnly { get; set; }
        string Value { get; set; }
        int VariableControlID { get; set; }
        int VariableSystemID { get; set; }
        bool VariableIsInput { get; set; }
        bool VariableIsOutput { get; set; }

        void SetDate(DateTime value);
        //void SetDateFormat(GlobalData.GnosisDateFormat dateFormat);
    }
}
