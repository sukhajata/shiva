using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisContainerImplementation : IGnosisMouseVisibleControlImplementation
    {
        //int GetOrder();
        //void SetOrder(int order);
        void Highlight();
        void UnHighlight();
        void Clear();
    }
}
