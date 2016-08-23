using ShivaShared3.Data;
using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GnosisControls
{
    public partial class GnosisTileTab : INotifyPropertyChanged//GnosisContainer
    {
        private List<GnosisTileTabItem> tabItems;
        private List<IGnosisFrameImplementation> frames;

        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool hidden;
        private int id;
        private GnosisTileTabItem currentTabItem;
        private int order;
        private string tooltip;

        public GnosisTileTabItem CurrentTileTabItem
        {
            get
            {
                return currentTabItem;
            }

            set
            {
                currentTabItem = value;
                OnPropertyChanged("CurrentTileTabItem");
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

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
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
                //string xaml = XamlWriter.Save(this);
                //GnosisIOHelperWPF.WriteXamlToFile(xaml);
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
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        [GnosisCollection]
        public List<GnosisTileTabItem> TabItems
        {
            get { return tabItems;  }
            set { tabItems = value; }
        }

        [GnosisCollection]
        public List<IGnosisFrameImplementation> Frames
        {
            get { return frames; }
            set { frames = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisTileTabItem)
            {
                tabItems.Add((GnosisTileTabItem)child);
            }
            else if (child is IGnosisFrameImplementation)
            {
                frames.Add((IGnosisFrameImplementation)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown type added to GnosisTileTab: " + child.GetType().ToString(), "GnosisTileTab.AddChild()");
            }
        }



    }
}
