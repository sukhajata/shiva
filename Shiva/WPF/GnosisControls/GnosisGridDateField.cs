using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisGridDateField : GnosisDateField, IGnosisGridFieldImplementation
    {
        private bool isEvenRow;
        private bool rowSelected;
        private string valueField;

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
