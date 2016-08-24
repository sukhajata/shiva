using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisControls
{
    public partial class GnosisNavigatorFrame : GnosisFrame , IGnosisNavFrameImplementation
    {
        public GnosisNavigatorFrame()
        {
            galleries = new List<GnosisGallery>();
        }

        public void LoadGallery(IGnosisGalleryImplementation galleryImp, int col, int row, int colSpan, int rowSpan)
        {
            LoadControl((GnosisGallery)galleryImp, col, row, colSpan, rowSpan);
        }
    }
}
