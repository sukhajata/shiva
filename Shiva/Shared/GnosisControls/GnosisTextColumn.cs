using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisTextColumn : GnosisGridColumn
    {
        private int maxDisplayWidthChars;

        private int minDisplayWidthChars;

        [GnosisProperty]
        public int MaxTextDisplayWidthChars
        {
            get { return maxDisplayWidthChars; }
            set { maxDisplayWidthChars = value; }
        }

        [GnosisProperty]
        public int MinTextDisplayWidthChars
        {
            get { return minDisplayWidthChars; }
            set { minDisplayWidthChars = value; }
        }


    }
}
