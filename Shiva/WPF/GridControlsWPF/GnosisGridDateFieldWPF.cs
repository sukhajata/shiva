using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShivaShared3.Interfaces;
using ShivaWPF3.ContentControlsWPF;

namespace ShivaWPF3.GridControlsWPF
{
    public class GnosisGridDateFieldWPF : GnosisDateFieldWPF, IGnosisGridDateFieldImplementation
    {
        private bool isEvenRow;
        private bool rowSelected;

        public int Order { get; set; }
        public bool IsEvenRow
        {
            get { return isEvenRow; }
            set
            {
                isEvenRow = value;
                OnPropertyChanged("IsEvenRow");
            }
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

        public double GetWidth()
        {
            return this.ActualWidth;
        }



        public double GetHeight()
        {
            return this.ActualHeight;
        }

    }
}
