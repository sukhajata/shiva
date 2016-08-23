using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ShivaShared3.BaseControllers;

namespace GnosisControls
{
    public partial class GnosisContainerTreeViewItem : INotifyPropertyChanged
    {
        private string caption;
        private string gnosisIcon;
        private GnosisVisibleController tag;

        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                OnPropertyChanged("Caption");
            }
        }

        public string GnosisIcon
        {
            get { return gnosisIcon; }
            set
            {
                gnosisIcon = value;
                OnPropertyChanged("GnosisIcon");
            }
        }

        public GnosisVisibleController GnosisTag
        {
            get { return tag; }
            set
            {
                tag = value;
                OnPropertyChanged("GnosisTag");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
