using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisNumberFieldImplementation : IGnosisPanelFieldImplementation, IGnosisCaptionLabelPossessor
    {
        bool Locked { get; set; }
        int MaxChars { get; set; }
        string MeasureRelativePosition { get; set; }
        int NoOfDecimals { get; set; }
        bool ReadOnly { get; set; }
        string UnitOfMeasure { get; set; }
        string Value { get; set; }

        void SetNumber(double number);
        double GetNumber();

    }
}
