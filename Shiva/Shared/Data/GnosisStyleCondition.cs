using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisStyleCondition : GnosisStyle
    {
        private string property;

        private string valueField;

        private int highlightThickness;

        private int order;

        
        public GnosisStyleCondition Parent { get; set; }

        [GnosisProperty]
        public string Property
        {
            get { return property; }
            set { property = value; }
        }

        [GnosisProperty]
        public string Value
        {
            get { return valueField; }
            set { valueField = value; }
        }

        [GnosisProperty]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        [GnosisProperty]
        public int HighlightThickness
        {
            get { return highlightThickness; }
            set { highlightThickness = value; }
        }

    }
}
