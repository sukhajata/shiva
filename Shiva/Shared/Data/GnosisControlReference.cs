using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisControlReference : IGnosisObject
    {
        private string name;

        private int id;

        private string elementItem;

        private string path;

        [GnosisProperty]
        public string GnosisName
        {
            get { return name; }
            set { name = value; }
        }

        [GnosisProperty]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [GnosisProperty]
        public string ElementItem
        {
            get { return elementItem; }
            set { elementItem = value; }
        }

        [GnosisProperty]
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
