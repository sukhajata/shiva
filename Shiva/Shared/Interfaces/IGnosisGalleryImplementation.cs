using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisGalleryImplementation : IGnosisInnerLayoutControlImplementation, IGnosisSpacingPossessor,
        IContainerPaddingPossessor
    {
        //properties
        int ExpandToLevel { get; set; }
        string Direction { get; set; }
      //  int MaxSectionSpan { get; set; }


      //  void AddGalleryItem(IGnosisGalleryItemImplementation galleryItemImplementation);
    }
}
