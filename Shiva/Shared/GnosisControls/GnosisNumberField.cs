using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System.Windows.Markup;

namespace GnosisControls
{
    public partial class GnosisNumberField : INotifyPropertyChanged//: GnosisTextField
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool optional;

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
        private bool hidden;
        private int id;
        private bool locked;
        private int maxChars;
        private int maxDisplayChars;
        private GnosisController.CaptionPosition measureRelativePosition;
        private int minDisplayChars;
        private int noOfDecimals;
        private int order;
        private bool readOnly;
        private string tooltip;
        private string unitOfMeasure;
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
               // string xaml = XamlWriter.Save(this.Style);
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
            get { return contentHorizontalAlignment; }
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
        public bool Optional
        {
            get { return optional; }
            set
            {
                optional = value;
                OnPropertyChanged("Optional");
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


        [GnosisPropertyAttribute]
        public string MeasureRelativePosition
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.CaptionPosition), measureRelativePosition);
            }

            set
            {
                try
                {
                    measureRelativePosition = (GnosisController.CaptionPosition)Enum.Parse(typeof(GnosisController.CaptionPosition), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.CaptionPosition _MeasureRelativePosition
        {
            get { return measureRelativePosition; }
            set { measureRelativePosition = value; }
        }

        [GnosisPropertyAttribute]
        public int NoOfDecimals
        {
            get
            {
                return noOfDecimals;
            }

            set
            {
                noOfDecimals = value;
            }
        }

        [GnosisPropertyAttribute]
        public string UnitOfMeasure
        {
            get
            {
                return unitOfMeasure;
            }

            set
            {
                unitOfMeasure = value;
            }
        }


        [GnosisPropertyAttribute]
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


     


    }
}
