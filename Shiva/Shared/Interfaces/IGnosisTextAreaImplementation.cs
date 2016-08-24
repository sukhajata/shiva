using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.BaseControllers;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisTextAreaImplementation : IGnosisInnerLayoutControlImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        string ContentVerticalAlignment { get; set; }
        string ContentHorizontalAlignment { get; set; }
        bool DatasetCreated { get; set; }
        bool DatasetUpdated { get; set; }
        bool DatasetDeleted { get; set; }
        string Dataset { get; set; }
        string DatasetItem { get; set; }
        bool HasScrollBar { get; set; }
        bool Locked { get; set; }
        int MaxChars { get; set; }
       // int MaxDisplayChars { get; set; }
      //  int MaxSectionSpan { get; set; }
        int MaxTextDisplayWidthChars { get; set; }
       // int MinDisplayChars { get; set; }
        int MinTextDisplayWidthChars { get; set; }
        bool ReadOnly { get; set; }

        //methods
        //void SetVerticalContentAlignment(GnosisController.VerticalAlignmentType contentVerticalAlignment);
        //void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType contentHorizontalAlignment);
        //void SetLocked(bool locked);
        string GetText();
        void SetTextWrapping(bool wrap);
    }
}
