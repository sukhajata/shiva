using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisDataItem : IGnosisObject
    {
        private int code;

        private string name;

        private int order;

        private string tooltip;

        private string icon;

        [GnosisProperty]
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        [GnosisProperty]
        public string GnosisName
        {
            get { return name; }
            set { name = value; }
        }

        [GnosisProperty]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }

        [GnosisProperty]
        public string GnosisIcon
        {
            get { return icon; }
            set { icon = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
