using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisTabHeaderButton.xaml
    /// </summary>
    public partial class GnosisTabHeaderButton : Border, IGnosisTabHeaderButtonImplementation, INotifyPropertyChanged
    {
        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasBorder;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool hidden;
        private int id;
        private int menuSystemID;
        private int menuControlID;
        private int order;
        private GnosisController.OrientationType orientation;
        private string tooltip;


        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
            }
        }

        public bool HasMouseFocus
        {
            get { return hasMouseFocus; }
            set
            {
                hasMouseFocus = value;
                OnPropertyChanged("HasMouseFocus");
                // string xaml = XamlWriter.Save(this);
            }
        }

        public bool HasMouseDown
        {
            get { return hasMouseDown; }
            set
            {
                hasMouseDown = value;
                OnPropertyChanged("HasMouseDown");
            }
        }

        [GnosisProperty]
        public bool HasBorder
        {
            get { return hasBorder; }
            set { hasBorder = value; }
        }

      
        [GnosisProperty]
        public string GnosisOrientation
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.OrientationType), orientation);
            }
            set
            {
                try
                {
                    orientation = (GnosisController.OrientationType)Enum.Parse(typeof(GnosisController.OrientationType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.OrientationType _GnosisOrientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisProperty]
        public int MenuSystemID
        {
            get
            {
                return menuSystemID;
            }

            set
            {
                menuSystemID = value;
            }
        }

        [GnosisProperty]
        public int MenuControlID
        {
            get
            {
                return menuControlID;
            }

            set
            {
                menuControlID = value;
            }
        }

        [GnosisProperty]
        public string Caption
        {
            get
            {
                return caption;
            }

            set
            {
                caption = value;
                headerButton.Content = caption;
                OnPropertyChanged("Caption");
            }
        }

        [GnosisProperty]
        public bool Hidden
        {
            get
            {
                return hidden;
            }

            set
            {
                hidden = value;
                OnPropertyChanged("Hidden");
            }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get
            {
                return tooltip;
            }

            set
            {
                tooltip = value;
                OnPropertyChanged("Tooltip");
            }
        }

        [GnosisProperty]
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
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
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

        [GnosisProperty]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public void SetGotFocusHandler(Action action)
        {
            throw new NotImplementedException();
        }

        public void SetLostFocusHandler(Action action)
        {
            throw new NotImplementedException();
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }

        public GnosisTabHeaderButton()
        {
            InitializeComponent();
        }

        private void headerButton_Click(object sender, RoutedEventArgs e)
        {
           // string xaml = XamlWriter.Save(this.Style);
        }
    }
}
