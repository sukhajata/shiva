using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisGridDateTimeFieldImplementation : IGnosisGridFieldImplementation
    {
        DateTime GetDateTime();
        void SetDateTime(DateTime dateTime);
    }
}
