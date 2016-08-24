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


        public GnosisDocumentFrame() : base()
        {

        }

        protected override void GnosisFrame_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Deleted":
                    break;
                case "Updated":
                    break;
                case "Created":
                    break;
                default:
                    base.GnosisFrame_PropertyChanged(sender, e);
                    break;
            }

        }

    }
}
