using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShivaShared3.Interfaces;


namespace ShivaWPF3.GridControlsWPF
{
    public class GnosisGridIndentFieldWPF : GnosisGridTextFieldWPF, IGnosisGridIndentFieldImplementation
    {
        public GnosisGridIndentFieldWPF()
            :base()
        {
            this.IsReadOnly = true;
            this.BorderThickness = new System.Windows.Thickness(0);
        }


    }
}
