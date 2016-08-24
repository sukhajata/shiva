using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisNavFrameImplementation : IGnosisFrameImplementation
    {
        void LoadGallery(IGnosisGalleryImplementation galleryImp, int col, int row, int colSpan, int rowSpan);
    }
}
