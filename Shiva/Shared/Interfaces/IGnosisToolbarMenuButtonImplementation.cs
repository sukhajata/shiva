using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisToolbarMenuButtonImplementation : IGnosisToolbarButtonImplementation
    {
        void AddItem(IGnosisToolbarMenuButtonItemImplementation secondTierItemImplementation);
    }
}
