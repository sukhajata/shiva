using System.Collections.Generic;
using GnosisControls;
using Shiva.Shared.Events;
using Shiva.Shared.OuterLayoutControllers;
using System;
using System.ComponentModel;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public partial class GnosisParentWindow : INotifyPropertyChanged//: GnosisVisibleControl
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private string controlType;
        private string gnosisName;
        private bool hidden;
        private string icon;
        private int id;
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

        [GnosisPropertyAttribute]
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

        [GnosisPropertyAttribute]
        public string GnosisName
        {
            get { return gnosisName; }
            set { gnosisName = value; }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return null; }
            set { }
        }


        [GnosisPropertyAttribute]
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

        [GnosisPropertyAttribute]
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

        [GnosisPropertyAttribute]
        public string GnosisIcon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
                OnPropertyChanged("GnosisIcon");
            }
        }

        [GnosisPropertyAttribute]
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private List<GnosisToolbarTray> toolbarTrays;

        private GnosisPrimarySplit primarySplit;

       
      //  private bool visibleField;

        private List<GnosisChildWindow> childWindows;

        private List<GnosisEventHandler> eventHandlers;



        [GnosisCollection]
        public List<GnosisToolbarTray> ToolbarTrays
        {
            get
            {
                return this.toolbarTrays;
            }
            set
            {
                this.toolbarTrays = value;
            }
        }

        [GnosisChild]
        public GnosisPrimarySplit PrimarySplit
        {
            get
            {
                return this.primarySplit;
            }
            set
            {
                this.primarySplit = value;
            }
        }
        
        [GnosisCollection]
        public List<GnosisChildWindow> ChildWindows
        {
            get { return childWindows; }
            set { childWindows = value; }
        }

        //[System.Xml.Serialization.XmlElementAttribute("GnosisEventHandler")]
        //public List<GnosisEventHandler> EventHandlers
        //{
        //    get { return eventHandlers; }
        //    set { eventHandlers = value; }
        //}



        //public GnosisControl FindControlByID(int controlID)
        //{
        //    if (this.ID == controlID)
        //    {
        //        return this;
        //    }

        //    GnosisControl control = null;
        //    while (control == null)
        //    {
        //        foreach (GnosisToolbarTray toolbarTray in GnosisToolbarTrays)
        //        {
        //            if (toolbarTray.ID == controlID)
        //            {
        //                control =  toolbarTray;
        //            }
        //            else
        //            {
        //                control = toolbarTray.FindControlByID(controlID);
        //            }
        //        }
        //        control = GnosisPrimarySplit[0].FindControlByID(controlID);
        //        break;
        //    }

        //    return control;
        //}



    }
}