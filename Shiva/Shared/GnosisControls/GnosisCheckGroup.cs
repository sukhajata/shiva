using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisCheckGroup 
    {
        private bool lockedField;

        [System.Xml.Serialization.XmlAttributeAttribute]
        public bool Locked
        {
            get { return lockedField; }
            set { lockedField = value; }
        }

        
    }
}
