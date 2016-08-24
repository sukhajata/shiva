using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisStyleSet : IGnosisObject
    {
        private List<EntityType> entityTypes;
        private List<GnosisStyle> styles;

        [GnosisCollection]
        public List<EntityType> EntityTypes
        {
            get { return entityTypes; }
            set { entityTypes = value; }
        }

        [GnosisCollection]
        public List<GnosisStyle> Styles
        {
            get { return styles; }
            set { styles = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {

            if (child is EntityType)
            {
                if (entityTypes == null)
                {
                    entityTypes = new List<EntityType>();
                }
                entityTypes.Add((EntityType)child);
            }
            else if (child is GnosisStyle)
            {
                if (styles == null)
                {
                    styles = new List<GnosisStyle>();
                }
                styles.Add((GnosisStyle)child);
            }
        }

    }
}
