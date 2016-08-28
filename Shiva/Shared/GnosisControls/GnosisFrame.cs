
using GnosisControls;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;

namespace GnosisControls
{
    public partial class GnosisFrame : INotifyPropertyChanged//: GnosisOuterLayoutControl
    {

        //protected string iconField;

        //protected int iconSizeField;

        //protected bool sqlSuccessfulField;

        //protected string urlField;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string allowedSectionList;
        private string caption;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasBorder;
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

        protected List<GnosisDocumentParameter> documentParameters;

        protected List<GnosisGrid> grids;

        protected List<GnosisMessageGrid> messageGrids;

        protected List<GnosisPanel> panels;

        protected List<GnosisTab> tabs;

        protected List<GnosisCalendar> calendars;

        protected List<GnosisTree> trees;

        protected List<GnosisTextArea> textAreas;

        protected List<GnosisSystemLayoutArea> systemLayoutAreas;

        //protected List<GnosisFramePresentation> framePresentations;


        //[System.Xml.Serialization.XmlAttributeAttribute]
        //public string Icon
        //{
        //    get { return iconField; }
        //    set { iconField = value; }
        //}

        //[System.Xml.Serialization.XmlAttributeAttribute]
        //public int IconSize
        //{
        //    get { return iconSizeField; }
        //    set { iconSizeField = value; }
        //}


        //[System.Xml.Serialization.XmlAttributeAttribute()]
        //public string URL
        //{
        //    get { return urlField; }
        //    set { urlField = value; }
        //}

        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
               // string xaml = XamlWriter.Save(this.Style);

            }
        }
        public bool HasMouseFocus
        {
            get { return hasMouseFocus; }
            set
            {
                hasMouseFocus = value;
                OnPropertyChanged("HasMouseFocus");
                // string xaml = XamlWriter.Save(this.Style);
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


        [GnosisProperty]
        public bool HasBorder
        {
            get { return hasBorder; }
            set { hasBorder = value; }
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

        [GnosisProperty]
        public string GnosisIcon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("GnosisIcon");
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

        [GnosisPropertyAttribute]
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

        [GnosisPropertyAttribute]
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

        [GnosisCollection]
        public List<GnosisSystemLayoutArea> SystemLayoutAreas
        {
            get { return systemLayoutAreas; }
            set { systemLayoutAreas = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

       


        
        //[System.Xml.Serialization.XmlElementAttribute("GnosisEventHandler")]
        //public List<GnosisEventHandler> EventHandlers
        //{
        //    get { return eventHandlers; }
        //    set { eventHandlers = value; }
        //}


        //[System.Xml.Serialization.XmlElementAttribute("GnosisGallery")]
        //public List<GnosisGallery> Galleries
        //{
        //    get { return galleries; }
        //    set { galleries = value; }
        //}


       

        [System.Xml.Serialization.XmlElement("GnosisTree")]
        public List<GnosisTree> Trees
        {
            get { return trees; }
            set { trees = value; }
        }

        public virtual void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisDocumentParameter)
            {
                if (documentParameters == null)
                {
                    documentParameters = new List<GnosisDocumentParameter>();
                }
                documentParameters.Add((GnosisDocumentParameter)child);
            }
            else if (child is IGnosisGridImplementation)
            {
                if (grids == null)
                {
                    grids = new List<GnosisGrid>();
                }

                grids.Add((GnosisGrid)child);
            }
            else if (child is IGnosisMessageGridImplementation)
            {
                if (messageGrids == null)
                {
                    messageGrids = new List<GnosisMessageGrid>();
                }

                messageGrids.Add((GnosisMessageGrid)child);
            }
            else if (child is IGnosisPanelImplementation)
            {
                if (panels == null)
                {
                    panels = new List<GnosisPanel>();
                }

                panels.Add((GnosisPanel)child);
            }
            else if (child is IGnosisTextAreaImplementation)
            {
                if (textAreas == null)
                {
                    textAreas = new List<GnosisTextArea>();
                }

                textAreas.Add((GnosisTextArea)child);
            }
            else if (child is IGnosisCalendarImplementation)
            {
                if (calendars == null)
                {
                    calendars = new List<GnosisCalendar>();
                }

                calendars.Add((GnosisCalendar)child);
            }
            else if (child is GnosisTree)
            {
                if (trees == null)
                {
                    trees = new List<GnosisTree>();
                }

                trees.Add((GnosisTree)child);
            }
            else if (child is GnosisSystemLayoutArea)
            {
                if (systemLayoutAreas == null)
                {
                    systemLayoutAreas = new List<GnosisSystemLayoutArea>();
                }
                systemLayoutAreas.Add((GnosisSystemLayoutArea)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisFrame", child.GetType().Name);
            }
        }


    }
}