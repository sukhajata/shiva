

using System;
using Shiva.Shared.Interfaces;
using Shiva.Shared.OuterLayoutControllers;

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