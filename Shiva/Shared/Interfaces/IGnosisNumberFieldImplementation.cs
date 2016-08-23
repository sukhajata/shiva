using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisNumberFieldImplementation : IGnosisPanelFieldImplementation
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
