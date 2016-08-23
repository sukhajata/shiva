using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public partial class GnosisGallerySearchItem : GnosisGalleryItem, IGnosisGallerySearchItemImplementation
    {
        public GnosisGallerySearchItem()
            :base()
        {
            searchParameters = new List<GnosisSearchParameter>();
        }

       
    }
}
