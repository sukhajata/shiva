using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisMessageGridImplementation : IGnosisInnerLayoutControlImplementation
    {
        string CaptionRelativePosition { get; set; }
        string CaptionAlignmentHorizontal { get; set; }
        string CaptionAlignmentVertical { get; set; }
        int MinDisplayRows { get; set; }
        int MaxDisplayRows { get; set; }
        int MaxLines { get; set; }
       // int MaxSectionSpan { get; set; }
        int MaxWrapRows { get; set; }

        //void SetMessage(string message);
    }
}
