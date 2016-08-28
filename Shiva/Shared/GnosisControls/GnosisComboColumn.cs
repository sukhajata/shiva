using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisComboColumn : GnosisGridColumn
    {
        private int documentSystemID;
        private int documentEntityID;
        private List<GnosisComboAttribute> comboAttributes;

        [GnosisProperty]
        public int DocumentSystemID
        {
            get { return documentSystemID; }
            set { documentSystemID = value; }
        }

        [GnosisProperty]
        public int DocumentEntityID
        {
            get { return documentEntityID; }
            set { documentEntityID = value; }
        }

        [GnosisCollection]
        public List<GnosisComboAttribute> ComboAttributes
        {
            get { return comboAttributes; }
            set { comboAttributes = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisComboAttribute)
            {
                if (comboAttributes == null)
                {
                    comboAttributes = new List<GnosisComboAttribute>();
                }
                comboAttributes.Add((GnosisComboAttribute)child);
            }
        }
    }
}
