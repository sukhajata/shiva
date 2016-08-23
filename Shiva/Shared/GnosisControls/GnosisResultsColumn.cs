using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    
    public class GnosisResultsColumn : GnosisGridColumn
    {

        private bool hasRowMouseFocus;

        private bool hasRowMouseDown;



        [GnosisProperty]
        public bool HasRowMouseFocus
        {
            get { return hasRowMouseFocus; }
            set { hasRowMouseFocus = value; }
        }

        [GnosisProperty]
        public bool HasRowMouseDown
        {
            get { return hasRowMouseDown; }
            set { hasRowMouseDown = value; }
        }

    }
}
