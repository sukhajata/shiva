using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public partial class GnosisResultsCheckField : GnosisGridCheckField, IGnosisResultsCheckFieldImplementation
    {
        private bool hasRowMouseFocus;
        private bool hasRowMouseDown;

        public bool HasRowMouseFocus
        {
            get { return hasRowMouseFocus; }
            set
            {
                hasRowMouseFocus = value;
                OnPropertyChanged("HasRowMouseFocus");
            }
        }
        public bool HasRowMouseDown
        {
            get { return hasRowMouseDown; }
            set
            {
                hasRowMouseDown = value;
                OnPropertyChanged("HasRowMouseDown");
            }
        }
    }
}
