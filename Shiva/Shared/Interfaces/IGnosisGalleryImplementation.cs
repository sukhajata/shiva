using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisGalleryImplementation : IGnosisInnerLayoutControlImplementation, IGnosisSpacingPossessor,
        IContainerPaddingPossessor, IGnosisBorderThicknessPossessor
    {
        //properties
        int ExpandToLevel { get; set; }
        string Direction { get; set; }

       // void ApplySpacing();
        //  int MaxSectionSpan { get; set; }


        //  void AddGalleryItem(IGnosisGalleryItemImplementation galleryItemImplementation);
    }
}
