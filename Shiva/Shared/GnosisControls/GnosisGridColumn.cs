using GnosisControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GnosisControls
{
    public class GnosisGridColumn : GnosisContentControl, INotifyPropertyChanged
    {
        private bool rowSelected;
        private bool isEvenRow;
        private bool locked;
        private bool readOnly;
        private string valueField;

        [GnosisProperty]
        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }


        [GnosisProperty]
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
            }
        }


        public bool RowSelected
        {
            get { return rowSelected; }
            set { rowSelected = value; }
        }

        [GnosisProperty]
        public bool IsEvenRow
        {
            get { return isEvenRow; }
            set { isEvenRow = value; }
        }


        [GnosisProperty]
        public string Value
        {
            get { return valueField; }
            set { valueField = value; }
        }

       

    }
}
