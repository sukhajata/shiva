using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisControlThicknessPossessor
    {
        // int ControlThickness { get; set; } //must use attached property for styles
        int CurrentThickness { get;  }
    }
}
