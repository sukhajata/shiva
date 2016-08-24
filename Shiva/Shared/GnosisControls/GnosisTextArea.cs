using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GnosisControls
{
    public partial class GnosisTextArea : INotifyPropertyChanged//GnosisInnerLayoutControl
    {
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
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasScrollBar;
        private bool hidden;
        private int id;
        private bool locked;
        private int maxChars;
        private int maxDisplayChars;
        private int maxTextDisplayWidthChars;
        private int maxSectionSpan;
        private int minDisplayChars;
        private int minTextDisplayWidthChars;
        private int order;
        private bool readOnly;
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

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisPropertyAttribute]
        public int MaxChars
        {
            get
            {
                return maxChars;
            }

            set
            {
                maxChars = value;
                OnPropertyChanged("MaxChars");
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

        [GnosisProperty]
        public string GnosisName
        {
            get { return gnosisName; }
            set { gnosisName = value; }
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
        public bool HasScrollBar
        {
            get
            {
                return hasScrollBar;
            }

            set
            {
                hasScrollBar = value;
                
                OnPropertyChanged("HasScrollBar");
            }
        }

        [GnosisPropertyAttribute]
        public int MaxSectionSpan
        {
            get
            {
                return maxSectionSpan;
            }

            set
            {
                maxSectionSpan = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxTextDisplayWidthChars
        {
            get
            {
                return maxTextDisplayWidthChars;
            }

            set
            {
                maxTextDisplayWidthChars = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MinTextDisplayWidthChars
        {
            get
            {
                return minTextDisplayWidthChars;
            }

            set
            {
                minTextDisplayWidthChars = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }



        //[System.Xml.Serialization.XmlAttributeAttribute("ContentVerticalAlignment")]
        //public string VerticalAlignmentString
        //{
        //    get
        //    {
        //        return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), contentVerticalAlignment);
        //    }
        //    set
        //    {
        //        try
        //        {
        //            contentVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
        //        }
        //        catch (Exception ex)
        //        {
        //            GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //        }
        //    }
        //}

        //[System.Xml.Serialization.XmlIgnore]
        //public GnosisController.VerticalAlignmentType ContentVerticalAlignment
        //{
        //    get { return contentVerticalAlignment; }
        //}


        //[System.Xml.Serialization.XmlAttribute("HorizontalAlignment")]
        //public string HorizontalAlignmentString
        //{
        //    get
        //    {
        //        return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), contentHorizontalAlignment);
        //    }
        //    set
        //    {
        //        try
        //        {
        //            contentHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
        //        }
        //        catch (Exception ex)
        //        {
        //            GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //        }
        //    }
        //}

        //[System.Xml.Serialization.XmlIgnore]
        //public GnosisController.HorizontalAlignmentType ContentHorizontalAlignment
        //{
        //    get { return contentHorizontalAlignment; }
        //    set { contentHorizontalAlignment = value; }
        //}


    }
}
