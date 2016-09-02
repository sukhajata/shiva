﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisTabItemImplementation : IGnosisInnerLayoutControlImplementation,
        IContainerPaddingPossessor, IGnosisBorderThicknessPossessor, IGnosisSpacingPossessor
    {
    }
}
