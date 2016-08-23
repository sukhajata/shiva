using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using ShivaShared3.Interfaces;
using ShivaShared3.ContentControllers;
using System.Windows.Media;
using System.Windows.Input;

namespace GnosisControls
{
    public partial class GnosisComboOption : TextBox, IGnosisComboOptionImplementation
    {
        

        public GnosisComboOption()
            :base()
        {
            this.Background = Brushes.Transparent;
            this.BorderThickness = new System.Windows.Thickness(0);
            this.IsReadOnly = true;
            this.Cursor = Cursors.Arrow;
            this.Focusable = false;
        }

        public void SetText(string text)
        {
            this.Text = text;
        }

        public string GetText()
        {
            return this.Text.ToString();
        }

        
    }
}
