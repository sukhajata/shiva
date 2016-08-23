

using System;
using ShivaShared3.Interfaces;
using ShivaShared3.OuterLayoutControllers;

namespace GnosisControls
{
    public partial class GnosisPrimarySplit 
    {

        private GnosisNavigatorTile gnosisNavTileField;

        
        [GnosisChild]
        public GnosisNavigatorTile GnosisNavTile
        {
            get
            {
                return this.gnosisNavTileField;
            }
            set
            {
                this.gnosisNavTileField = value;
            }
        }


      
    }
}