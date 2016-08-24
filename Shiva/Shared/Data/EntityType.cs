using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class EntityType : IGnosisObject
    {
        private string name;

        private string controlType;

        private string controlTypeTag;

        private string normalStyle;

        private string captionStyle;

        [GnosisProperty]
        public string GnosisName
        {
            get { return name; }
            set { name = value; }
        }

        [GnosisProperty]
        public string ControlType
        {
            get { return controlType; }
            set { controlType = value; }
        }

        [GnosisProperty]
        public string ControlTypeTag
        {
            get { return controlTypeTag; }
            set { controlTypeTag = value; }
        }

        [GnosisProperty]
        public string NormalStyle
        {
            get { return normalStyle; }
            set { normalStyle = value; }
        }

        [GnosisProperty]
        public string CaptionStyle
        {
            get { return captionStyle; }
            set { captionStyle = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
