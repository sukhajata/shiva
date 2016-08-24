using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisObject
    {
        void GnosisAddChild(IGnosisObject child);
    }
}
