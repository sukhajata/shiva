using Shiva.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public partial class GnosisDocumentFrame 
    {

        private bool isEditingField;

        private bool isEmptyField;

        private bool createdField;

        private bool deletedField;

        private bool updatedField;

        //private List<GnosisDragEvent> dragEvents;



        [GnosisProperty]
        public bool _Created
        {
            get { return createdField; }
            set
            {
                createdField = value;
                OnPropertyChanged("Created");
            }
        }

        [GnosisProperty]
        public bool _Deleted
        {
            get { return deletedField; }
            set
            {
                deletedField = value;
                OnPropertyChanged("Deleted");
            }
        }

        [GnosisProperty]
        public bool _Updated
        {
            get { return updatedField; }
            set
            {
                updatedField = value;
                OnPropertyChanged("Updated");
            }
        }

        //public List<GnosisDragEvent> DragEvents
        //{
        //    get { return dragEvents; }
        //    set { dragEvents = value; }
        //}
    }
}
