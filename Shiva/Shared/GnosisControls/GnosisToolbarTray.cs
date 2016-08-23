using ShivaShared3.BaseControllers;

using ShivaShared3.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public partial class GnosisToolbarTray : INotifyPropertyChanged//: GnosisVisibleControl
    {
        private List<GnosisToolbar> toolbars;

        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasBorder;
        private bool hasFocus;
        private bool hidden;
        private int id;
        private int menuSystemID;
        private int menuControlID;
        private int order;
        private GnosisController.OrientationType orientation;
        private string tooltip;
        private GnosisController.HorizontalAlignmentType trayHorizontalAlignment;


        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
            }
        }

        [GnosisProperty]
        public bool HasBorder
        {
            get { return hasBorder;}
            set { hasBorder = value; }
        }
       
        [GnosisProperty]
        public string TrayHorizontalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), trayHorizontalAlignment);
            }

            set
            {
                try
                {
                    trayHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.HorizontalAlignmentType _TrayHorizontalAlignment
        {
            get { return trayHorizontalAlignment; }
            set { trayHorizontalAlignment = value; }
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
       


        [GnosisCollection]
        public List<GnosisToolbar> Toolbars
        {
            get { return toolbars; }
            set { toolbars = value; }
        }

       

    }
}
