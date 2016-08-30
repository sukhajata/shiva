using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Shiva.Shared.Interfaces;
using System.Windows.Markup;

namespace GnosisControls
{
    public partial class GnosisResultsCheckField : GnosisGridCheckField, IGnosisResultsCheckFieldImplementation
    {
        private bool hasRowMouseFocus;
        private bool hasRowMouseDown;

        public bool HasRowMouseFocus
        {
            get { return hasRowMouseFocus; }
            set
            {
                hasRowMouseFocus = value;
                OnPropertyChanged("HasRowMouseFocus");
               // string xaml = XamlWriter.Save(this.Style);
            }
        }
        public bool HasRowMouseDown
        {
            get { return hasRowMouseDown; }
            set
            {
                hasRowMouseDown = value;
                OnPropertyChanged("HasRowMouseDown");
            }
        }
    }
}
