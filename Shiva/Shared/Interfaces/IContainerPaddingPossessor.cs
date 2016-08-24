using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IContainerPaddingPossessor
    {
        int ContainerVerticalPadding { get; set; }
        int ContainerHorizontalPadding { get; set; }
    }
}
