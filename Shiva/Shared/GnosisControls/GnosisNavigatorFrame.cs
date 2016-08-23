using ShivaShared3.Events;
using GnosisControls;
using System.Collections.Generic;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public partial class GnosisNavigatorFrame  //: GnosisFrame
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


    }
}
