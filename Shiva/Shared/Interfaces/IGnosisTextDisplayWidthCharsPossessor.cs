﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisTextDisplayWidthCharsPossessor
    {
        int MinTextDisplayWidthChars { get; set; }
        int MaxTextDisplayWidthChars { get; set; }
    }
}
