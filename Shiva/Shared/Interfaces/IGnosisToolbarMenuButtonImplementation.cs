using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisToolbarMenuButtonImplementation : IGnosisToolbarButtonImplementation
    {
        void AddItem(IGnosisToolbarMenuButtonItemImplementation secondTierItemImplementation);
    }
}
