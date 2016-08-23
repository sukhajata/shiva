using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using System.ComponentModel;
using ShivaShared3.BaseControllers;
using ShivaShared3.Data;

namespace GnosisControls
{
    public partial class GnosisLinkField : INotifyPropertyChanged //: GnosisPanelField
    {
        private GnosisLinkButton linkButton;
        private GnosisLinkMenuButton linkMenuButton;
        private List<GnosisLinkAttribute> linkAttributes;
        private List<GnosisLinkEntity> linkEntities;


        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private GnosisController.VerticalAlignmentType contentVerticalAlignment;
        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
        private string controlType;
        private bool datasetCreated;
        private bool datasetUpdated;
        private bool datasetDeleted;
        private string dataset;
        private string datasetItem;
        private int documentEntityID;
        private int documentSystemID;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private bool locked;
        private int minDisplayChars;
        private int maxDisplayChars;
        private int order;
        private string perspective;
        private bool previouslySelected;
        private bool readOnly;
        private string tooltip;
        private string valueField;

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
        public string ContentVerticalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), contentVerticalAlignment);
            }
            set
            {
                try
                {
                    contentVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
                    OnPropertyChanged("ContentVerticalAlignment");
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.VerticalAlignmentType _ContentVerticalAlignment
        {
            get { return contentVerticalAlignment; }
            set { contentVerticalAlignment = value; }
        }

        

        [GnosisPropertyAttribute]
        public string ContentHorizontalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), contentHorizontalAlignment);
            }
            set
            {
                try
                {
                    contentHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                    OnPropertyChanged("ContentHorizontalAlignment");
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get { return contentHorizontalAlignment;}
            set { contentHorizontalAlignment = value; }
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
        public bool DatasetCreated
        {
            get
            {
                return datasetCreated;
            }

            set
            {
                datasetCreated = value;
                OnPropertyChanged("DatasetCreated");
            }
        }

        [GnosisPropertyAttribute]
        public bool DatasetUpdated
        {
            get
            {
                return datasetUpdated;
            }

            set
            {
                datasetUpdated = value;
                OnPropertyChanged("DatasetUpdated");
            }
        }

        [GnosisPropertyAttribute]
        public bool DatasetDeleted
        {
            get
            {
                return datasetDeleted;
            }

            set
            {
                datasetDeleted = value;
                OnPropertyChanged("DatasetDeleted");
            }
        }


        [GnosisPropertyAttribute]
        public string Dataset
        {
            get
            {
                return dataset;
            }

            set
            {
                dataset = value;
                //OnPropertyChanged("Dataset");
            }
        }

        [GnosisPropertyAttribute]
        public string DatasetItem
        {
            get
            {
                return datasetItem;
            }

            set
            {
                datasetItem = value;
                // OnPropertyChanged("DatasetItem");
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

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
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
        public int MinDisplayChars
        {
            get
            {
                return minDisplayChars;
            }
            set
            {
                minDisplayChars = value;
                OnPropertyChanged("MinDisplayChars");
            }
        }

        [GnosisPropertyAttribute]
        public int MaxDisplayChars
        {
            get
            {
                return maxDisplayChars;
            }
            set
            {
                maxDisplayChars = value;
                OnPropertyChanged("MaxDisplayChars");
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
        public bool ReadOnly
        {
            get
            {

                return readOnly;
            }

            set
            {
                readOnly = value;
                OnPropertyChanged("ReadOnly");
            }
        }

        [GnosisPropertyAttribute]
        public string Value
        {
            get
            {
                return valueField;
            }

            set
            {
                valueField = value;
            }
        }

        public bool Locked
        {
            get { return locked; }
            set
            {
                locked = value;
               
                OnPropertyChanged("Locked");
            }
        }

        [GnosisPropertyAttribute]
        public int DocumentEntityID
        {
            get
            {
                return documentEntityID;
            }

            set
            {
                documentEntityID = value;
            }
        }

        [GnosisPropertyAttribute]
        public int DocumentSystemID
        {
            get
            {
                return documentSystemID;
            }

            set
            {
                documentSystemID = value;
            }
        }

        [GnosisPropertyAttribute]
        public string Perspective
        {
            get
            {
                return perspective;
            }

            set
            {
                perspective = value;
            }
        }

        public bool PreviouslySelected
        {
            get
            {
                return previouslySelected;
            }

            set
            {
                previouslySelected = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //  private List<GnosisSearchParameter> searchParameters;



        [GnosisChild]
        public GnosisLinkButton LinkButton
        {
            get { return linkButton; }
            set { linkButton = value; }
        }

        [GnosisChild]
        public GnosisLinkMenuButton LinkMenuButton
        {
            get { return linkMenuButton; }
            set { linkMenuButton = value; }
        }

        [GnosisCollection]
        public List<GnosisLinkAttribute> LinkAttributes
        {
            get { return linkAttributes; }
            set { linkAttributes = value; }
        }


        [GnosisCollection]
        public List<GnosisLinkEntity> LinkEntities
        {
            get { return linkEntities; }
            set { linkEntities = value; }
        }

        //[System.Xml.Serialization.XmlElement("GnosisSearchParameter")]
        //public List<GnosisSearchParameter> SearchParameters
        //{
        //    get { return searchParameters; }
        //    set { searchParameters = value; }
        //}

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisLinkAttribute)
            {
                linkAttributes.Add((GnosisLinkAttribute)child);
            }
            else if (child is GnosisLinkEntity)
            {
                linkEntities.Add((GnosisLinkEntity)child);
            }
            else if (child is GnosisLinkMenuButton)
            {
                linkMenuButton = (GnosisLinkMenuButton)child;
            }
            else if (child is GnosisLinkButton)
            {
                linkButton = (GnosisLinkButton)child;
            }
        }


    }
}
