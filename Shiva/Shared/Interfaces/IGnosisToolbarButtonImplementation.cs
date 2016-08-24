using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisToolbarButtonImplementation : IGnosisMouseVisibleControlImplementation,
        IGnosisPaddingPossessor, IGnosisMarginPossessor//: IGnosisButtonImplementation
    {
        string GnosisIcon { get; set; }
        string Shortcut { get; set; }
        string MenuTag { get; set; }
        bool Disabled { get; set; }
        //void SetEnabled(bool v);
    }
}
