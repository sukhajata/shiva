using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisConnectionFrameImplementation : IGnosisFrameImplementation
    {
        bool AutoCreate { get; set; }
        bool _Created { get; set; }
        bool _Deleted { get; set; }
        bool _Updated { get; set; }
    }
}
