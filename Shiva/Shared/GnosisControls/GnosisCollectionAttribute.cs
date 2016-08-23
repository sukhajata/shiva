using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisControls
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public class GnosisCollectionAttribute : System.Attribute
    {
        public GnosisCollectionAttribute() { }
    }
}
