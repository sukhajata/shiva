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
        private List<GnosisGallery> galleries;


        [GnosisCollection]
        public List<GnosisGallery> Galleries
        {
            get
            {
                return this.galleries;
            }
            set
            {
                this.galleries = value;
            }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisGallery)
            {
                if (galleries == null)
                {
                    galleries = new List<GnosisGallery>();
                }

                galleries.Add((GnosisGallery)child);
            }
            else
            {
                base.GnosisAddChild(child);
            }
        }

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
