using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisGridCheckFieldImplementation : IGnosisGridFieldImplementation
    {
        void SetChecked(bool selected);
        bool GetIsChecked();
    }
}
