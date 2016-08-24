using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisErrorHandler
    {
        void HandleError(string message, string stackTrace);
        void HandleError(string message, string stackTrace, int severity);
        void HandleUnknowChildAddedError(string parentType, string childType);
    }
}
