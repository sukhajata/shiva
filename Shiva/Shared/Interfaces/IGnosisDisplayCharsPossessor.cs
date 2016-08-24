using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisDisplayCharsPossessor
    {
        int MaxDisplayChars { get; set; }
        int MinDisplayChars { get; set; }
    }
}
