using System;
using Shiva.Shared.Interfaces;


namespace GnosisControls
{
    public partial class GnosisConnectionFrame : GnosisFrame, IGnosisConnectionFrameImplementation
    {
        private bool autoCreate;
        private bool _created;
        private bool _deleted;
        private bool _updated;

        [GnosisPropertyAttribute]
        public bool AutoCreate
        {
            get
            {
                return autoCreate;
            }

            set
            {
                autoCreate = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool _Created
        {
            get
            {
                return _created;
            }

            set
            {
                _created = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool _Deleted
        {
            get
            {
                return _deleted;
            }

            set
            {
                _deleted = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool _Updated
        {
            get
            {
                return _updated;
            }

            set
            {
                _updated = value;
            }
        }
    }
}
