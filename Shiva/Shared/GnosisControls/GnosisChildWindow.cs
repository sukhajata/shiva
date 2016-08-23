using System;
using System.Collections.Generic;
using GnosisControls;



namespace GnosisControls
{
    public class GnosisChildWindow : GnosisVisibleControl
    {
        private List<GnosisSplit> splits;
        private List<GnosisTile> tiles;

        [GnosisCollection]
        public List<GnosisSplit> Splits
        {
            get { return splits; }set { splits = value; }
        }

        [GnosisCollection]
        public List<GnosisTile> Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }
    }
}
