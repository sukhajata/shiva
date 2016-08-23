using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisTextResults : GnosisResultsColumn
    {
        private int maxTextDisplayWidthChars;

        private int minTextDisplayWidthChars;

        [GnosisProperty]
        public int MaxTextDisplayWidthChars
        {
            get { return maxTextDisplayWidthChars; }
            set { maxTextDisplayWidthChars = value; }
        }

        [GnosisProperty]
        public int MinTextDisplayWidthChars
        {
            get { return minTextDisplayWidthChars; }
            set { minTextDisplayWidthChars = value; }
        }
    }
}
