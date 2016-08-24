using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shiva.Shared.Interfaces;


namespace GnosisControls
{
    public class GnosisGridIndentField : GnosisGridTextField, IGnosisGridIndentFieldImplementation
    {
        public GnosisGridIndentField()
            :base()
        {
            this.IsReadOnly = true;
            this.BorderThickness = new System.Windows.Thickness(0);
        }


    }
}
