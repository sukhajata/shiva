using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisDataCache : IGnosisObject
    {
        private string element;
        private string attribute;
        private string gnosis_object;
        private string schema;
        private string database;
        private XElement content;

        [GnosisProperty]
        public string Element
        {
            get { return element; }
            set { element = value; }
        }

        [GnosisProperty]
        public string Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }

        [GnosisProperty]
        public string GnosisObject
        {
            get { return gnosis_object; }
            set { gnosis_object = value; }
        }

        [GnosisProperty]
        public string Schema
        {
            get { return schema; }
            set { schema = value; }
        }

        [GnosisProperty]
        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        
        public XElement Content
        {
            get { return content; }
            set { content = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
