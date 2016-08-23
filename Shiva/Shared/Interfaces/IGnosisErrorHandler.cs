using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisErrorHandler
    {
        void HandleError(string message, string stackTrace);
        void HandleError(string message, string stackTrace, int severity);
        void HandleUnknowChildAddedError(string parentType, string childType);
    }
}
