﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisGridDateFieldImplementation : IGnosisGridFieldImplementation
    {
        void SetDate(DateTime date);
    }
}
