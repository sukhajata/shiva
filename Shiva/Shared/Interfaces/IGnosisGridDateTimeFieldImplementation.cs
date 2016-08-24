using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisGridDateTimeFieldImplementation : IGnosisGridFieldImplementation
    {
        DateTime GetDateTime();
        void SetDateTime(DateTime dateTime);
    }
}
