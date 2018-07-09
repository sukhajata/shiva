using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisTextResults : GnosisTextColumn, IGnosisResultsFieldImplementation, IGnosisTextDisplayWidthCharsPossessor
    {
        private bool hasRowMouseDown;

        private bool hasRowMouseFocus;

        public bool HasRowMouseDown
        {
            get
            {
                return hasRowMouseDown;
            }

            set
            {
                hasRowMouseDown = value;
            }
        }

        public bool HasRowMouseFocus
        {
            get
            {
                return hasRowMouseFocus;
            }

            set
            {
                hasRowMouseFocus = value;
            }
        }


    }
}
