using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Shiva.Shared.Interfaces;
using Shiva.Shared.ContentControllers;
using System.Windows.Media;
using System.Windows.Input;

namespace GnosisControls
{
    public partial class GnosisComboOption : TextBox, IGnosisComboOptionImplementation
    {
        private string code;
        private string controlType;
        private string gnosisName;
        private int id;
        private int order;

        [GnosisPropertyAttribute]
        public string ControlType
        {
            get
            {
                return controlType;
            }

            set
            {
                controlType = value;
            }
        }

        [GnosisProperty]
        public string GnosisName
        {
            get
            {
                return gnosisName;
            }

            set
            {
                gnosisName = value;
            }
        }


        [GnosisPropertyAttribute]
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                // OnPropertyChanged("ID");
            }
        }

        [GnosisPropertyAttribute]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
                //OnPropertyChanged("Order");
            }
        }

        [GnosisProperty]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

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
