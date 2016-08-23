using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public class GnosisResultsNumberField : GnosisGridNumberField, IGnosisResultsFieldImplementation
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
                OnPropertyChanged("HasRowMouseDown");
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
                OnPropertyChanged("HasRowMouseFocus");
            }
        }
    }
}
