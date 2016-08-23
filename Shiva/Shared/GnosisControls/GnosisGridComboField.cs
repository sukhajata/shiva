using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public class GnosisGridComboField : GnosisComboField, IGnosisGridFieldImplementation
    {
        private bool isEvenRow;
        private bool rowSelected;

        [GnosisProperty]
        public bool IsEvenRow
        {
            get { return isEvenRow; }
            set { isEvenRow = value; }
        }

        public bool RowSelected
        {
            get { return rowSelected; }
            set
            {
                rowSelected = value;
                OnPropertyChanged("RowSelected");
            }
        }

       
    }
}
