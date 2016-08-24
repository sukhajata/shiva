using GnosisControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public partial class GnosisSearchFrame : INotifyPropertyChanged //: GnosisFrame
    {
        private List<GnosisSearchParameter> searchParameters;
        private GnosisNewMenuItem newMenuItem;

        private GnosisSearchResultsGrid searchResultsGrid;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string allowedSectionList;
        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private string icon;
        private int id;
        private bool isEditing;
        private bool isEmpty;
        private int minWidthCharacters;
        private int maxWidthCharacters;
        private int optimalSectionWidthCharacters;
        private int order;
        private bool sqlSuccessful;
        private string tooltip;

        private List<GnosisCalendar> calendars;
        private List<GnosisGrid> grids;
        private List<GnosisMessageGrid> messageGrids;
        private List<GnosisPanel> panels;
        private List<GnosisTextArea> textAreas;

        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                if (hasFocus)
                {
                    // Highlight();
                }
                else
                {
                    // UnHighlight();
                }
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
                //string xaml = XamlWriter.Save(this.Style);
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
            get { return gnosisParent; }
            set { gnosisParent = value; }
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

        public bool IsEditing
        {
            get
            {
                return isEditing;
            }

            set
            {
                isEditing = value;
                OnPropertyChanged("IsEditing");
            }
        }

        public bool IsEmpty
        {
            get
            {
                return isEmpty;
            }

            set
            {
                isEmpty = value;
                OnPropertyChanged("IsEmpty");
            }
        }

        public bool SQLSuccessful
        {
            get
            {
                return sqlSuccessful;
            }

            set
            {
                sqlSuccessful = value;
                OnPropertyChanged("SQLSuccessful");
            }
        }

        [GnosisPropertyAttribute]
        public string AllowedSectionList
        {
            get
            {
                return allowedSectionList;
            }

            set
            {
                allowedSectionList = value;
            }
        }

        public List<int> _AllowedSectionList
        {
            get
            {

                if (allowedSectionList == null)
                {
                    return new List<int> { 1, 2, 4 };
                }

                string[] ss = allowedSectionList.Split(',');
                List<int> ints = new List<int>();
                foreach (string s in ss)
                {
                    ints.Add(Int16.Parse(s.Trim()));
                }

                return ints;
            }


        }

        [GnosisPropertyAttribute]
        public int OptimalSectionWidthCharacters
        {
            get
            {
                return optimalSectionWidthCharacters;
            }

            set
            {
                optimalSectionWidthCharacters = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MinWidthCharacters
        {
            get
            {
                return minWidthCharacters;
            }

            set
            {
                minWidthCharacters = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxWidthCharacters
        {
            get
            {
                return maxWidthCharacters;
            }

            set
            {
                maxWidthCharacters = value;
            }
        }

        //Collections
        [GnosisCollectionAttribute]
        public List<GnosisCalendar> Calendars
        {
            get { return calendars; }
            set { calendars = value; }
        }

        [GnosisCollectionAttribute]
        public List<GnosisGrid> Grids
        {
            get { return grids; }
            set { grids = value; }
        }

        [GnosisCollectionAttribute]
        public List<GnosisPanel> Panels
        {
            get { return panels; }
            set { panels = value; }
        }

        [GnosisCollectionAttribute]
        public List<GnosisMessageGrid> MessageGrids
        {
            get { return messageGrids; }
            set { messageGrids = value; }
        }

        [GnosisCollectionAttribute]
        public List<GnosisTextArea> TextAreas
        {
            get { return textAreas; }
            set { textAreas = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        [GnosisCollection]
        public List<GnosisSearchParameter> SearchParameters
        {
            get { return searchParameters; }
            set { searchParameters = value; }
        }

        [GnosisChild]
        public GnosisSearchResultsGrid SearchResultsGrid
        {
            get { return searchResultsGrid; }
            set { searchResultsGrid = value; }
        }

        [GnosisChild]
        public GnosisNewMenuItem NewMenuItem
        {
            get { return newMenuItem; }
            set { newMenuItem = value; }
        }

        public virtual void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisSearchResultsGrid)
            {
                searchResultsGrid = (GnosisSearchResultsGrid)child;
            }
            else if (child is GnosisMessageGrid)
            {
                if (messageGrids == null)
                {
                    messageGrids = new List<GnosisMessageGrid>();
                }

                messageGrids.Add((GnosisMessageGrid)child);
            }
            else if (child is GnosisGrid)
            {
                if (grids == null)
                {
                    grids = new List<GnosisGrid>();
                }

                grids.Add((GnosisGrid)child);
            }
            else if (child is GnosisPanel)
            {
                if (panels == null)
                {
                    panels = new List<GnosisPanel>();
                }

                panels.Add((GnosisPanel)child);
            }
            else if (child is GnosisTextArea)
            {
                if (textAreas == null)
                {
                    textAreas = new List<GnosisTextArea>();
                }

                textAreas.Add((GnosisTextArea)child);
            }
            else if (child is GnosisCalendar)
            {
                if (calendars == null)
                {
                    calendars = new List<GnosisCalendar>();
                }

                calendars.Add((GnosisCalendar)child);
            }
           
            else if (child is GnosisSearchParameter)
            {
                if (searchParameters == null)
                {
                    searchParameters = new List<GnosisSearchParameter>();
                }

                searchParameters.Add((GnosisSearchParameter)child);
            }
           
            else if (child is GnosisNewMenuItem)
            {
                newMenuItem = (GnosisNewMenuItem)child;
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("AddChild unhandled in Frame - type " + child.GetType().Name, 
                    "GnosisFrame.AddChild");
            }
        }

    }
}
