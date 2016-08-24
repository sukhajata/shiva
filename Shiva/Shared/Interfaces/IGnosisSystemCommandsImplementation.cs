using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisSystemCommandsImplementation
    {
        void Cut();
        void Copy();
        void Paste();
        void Print();
        void ShowSystemProperties();
    }
}
