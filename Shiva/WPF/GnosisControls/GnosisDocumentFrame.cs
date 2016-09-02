using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public partial class GnosisDocumentFrame : GnosisFrame, IGnosisDocFrameImplementation
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


        public GnosisDocumentFrame() : base()
        {

        }

       

    }
}
