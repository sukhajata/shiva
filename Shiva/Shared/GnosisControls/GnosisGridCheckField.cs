//using Shiva.Shared.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.ComponentModel;
//using Shiva.Shared.BaseControllers;
//using Shiva.Shared.Data;

//namespace GnosisControls
//{
//    public partial class GnosisGridCheckField : IGnosisGridFieldImplementation, INotifyPropertyChanged
//    {
//        private bool hasFocus;
//        private bool hasMouseFocus;
//        private bool hasMouseDown;
//        private bool isEvenRow;
//        private bool rowSelected;

//        private string caption;
//        private bool disabled;
//        private GnosisController.VerticalAlignmentType contentVerticalAlignment;
//        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
//      //  private int checkedFactor;
//        private string controlType;
//        private bool datasetCreated;
//        private bool datasetUpdated;
//        private bool datasetDeleted;
//        private string dataset;
//        private string datasetItem;
//        private bool gnosisChecked;
//        private string gnosisName;
//        private IGnosisVisibleControlImplementation gnosisParent;
//       // private string groupName;
//        private bool hidden;
//        private string icon;
//        private int id;
//        private bool locked;
//        private int minDisplayChars;
//        private int maxDisplayChars;
//        private int order;
//        private bool readOnly;
//      //  private string shortcut;
//        private string tooltip;
//        private string valueField;


//        //Dynamic Properties
//        public bool HasFocus
//        {
//            get { return hasFocus; }
//            set
//            {
//                hasFocus = value;
//                OnPropertyChanged("HasFocus");
//            }
//        }
//        public bool HasMouseFocus
//        {
//            get { return hasMouseFocus; }
//            set
//            {
//                hasMouseFocus = value;
//                OnPropertyChanged("HasMouseFocus");
//                // string xaml = XamlWriter.Save(this);
//            }
//        }

//        public bool HasMouseDown
//        {
//            get { return hasMouseDown; }
//            set
//            {
//                hasMouseDown = value;
//                OnPropertyChanged("HasMouseDown");
//            }
//        }


//        public bool Locked
//        {
//            get
//            {
//                return locked;
//            }

//            set
//            {
//                locked = value;
//                chkBox.IsEnabled = !locked;
//                OnPropertyChanged("Locked");
//            }
//        }

//        public bool RowSelected
//        {
//            get { return rowSelected; }
//            set
//            {
//                rowSelected = value;
//               // OnPropertyChanged("RowSelected");
//            }
//        }

//        //Static Properties
//        [GnosisPropertyAttribute]
//        public string ContentVerticalAlignment
//        {
//            get
//            {
//                return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), contentVerticalAlignment);
//            }
//            set
//            {
//                try
//                {
//                    contentVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
//                    //this.SetVerticalContentAlignmentExt(contentVerticalAlignment);
//                    OnPropertyChanged("ContentVerticalAlignment");
//                }
//                catch (Exception ex)
//                {
//                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
//                }
//            }
//        }

//        [GnosisPropertyAttribute]
//        public string ContentHorizontalAlignment
//        {
//            get
//            {
//                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), contentHorizontalAlignment);
//            }
//            set
//            {
//                try
//                {
//                    contentHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
//                    //this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
//                    OnPropertyChanged("ContentHorizontalAlignment");
//                }
//                catch (Exception ex)
//                {
//                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
//                }
//            }
//        }

//        public GnosisController.VerticalAlignmentType _ContentVerticalAlignment
//        {
//            get { return contentVerticalAlignment; }
//            set { contentVerticalAlignment = value; }
//        }

//        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
//        {
//            get { return contentHorizontalAlignment; }
//            set { contentHorizontalAlignment = value; }
//        }


//        [GnosisPropertyAttribute]
//        public string ControlType
//        {
//            get
//            {
//                return controlType;
//            }

//            set
//            {
//                controlType = value;
//            }
//        }

//        [GnosisPropertyAttribute]
//        public bool DatasetCreated
//        {
//            get
//            {
//                return datasetCreated;
//            }

//            set
//            {
//                datasetCreated = value;
//                OnPropertyChanged("DatasetCreated");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public bool DatasetUpdated
//        {
//            get
//            {
//                return datasetUpdated;
//            }

//            set
//            {
//                datasetUpdated = value;
//                OnPropertyChanged("DatasetUpdated");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public bool DatasetDeleted
//        {
//            get
//            {
//                return datasetDeleted;
//            }

//            set
//            {
//                datasetDeleted = value;
//                OnPropertyChanged("DatasetDeleted");
//            }
//        }


//        [GnosisPropertyAttribute]
//        public string Dataset
//        {
//            get
//            {
//                return dataset;
//            }

//            set
//            {
//                dataset = value;
//                //OnPropertyChanged("Dataset");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public string DatasetItem
//        {
//            get
//            {
//                return datasetItem;
//            }

//            set
//            {
//                datasetItem = value;
//                // OnPropertyChanged("DatasetItem");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public string Caption
//        {
//            get
//            {
//                return caption;
//            }

//            set
//            {
//                caption = value;
//                OnPropertyChanged("Caption");
//            }
//        }

//        [GnosisProperty]
//        public string GnosisName
//        {
//            get { return gnosisName; }
//            set { gnosisName = value; }
//        }

//        public IGnosisVisibleControlImplementation GnosisParent
//        {
//            get { return gnosisParent; }
//            set { gnosisParent = value; }
//        }


//        [GnosisPropertyAttribute]
//        public bool Hidden
//        {
//            get
//            {
//                return hidden;
//            }

//            set
//            {
//                hidden = value;
//                OnPropertyChanged("Hidden");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public int ID
//        {
//            get
//            {
//                return id;
//            }

//            set
//            {
//                id = value;
//                // OnPropertyChanged("ID");
//            }
//        }

//        [GnosisProperty]
//        public bool IsEvenRow
//        {
//            get { return isEvenRow; }
//            set { isEvenRow = value; }
//        }

//        [GnosisPropertyAttribute]
//        public int MinDisplayChars
//        {
//            get
//            {
//                return minDisplayChars;
//            }
//            set
//            {
//                minDisplayChars = value;
//                OnPropertyChanged("MinDisplayChars");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public int MaxDisplayChars
//        {
//            get
//            {
//                return maxDisplayChars;
//            }
//            set
//            {
//                maxDisplayChars = value;
//                OnPropertyChanged("MaxDisplayChars");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public int Order
//        {
//            get
//            {
//                return order;
//            }

//            set
//            {
//                order = value;
//                //OnPropertyChanged("Order");
//            }
//        }


//        [GnosisPropertyAttribute]
//        public string Tooltip
//        {
//            get
//            {
//                return tooltip;
//            }

//            set
//            {
//                tooltip = value;
//                OnPropertyChanged("Tooltip");
//            }
//        }


//        //[GnosisPropertyAttribute]
//        //public int CheckedFactor
//        //{
//        //    get
//        //    {
//        //        return checkedFactor;
//        //    }

//        //    set
//        //    {
//        //        checkedFactor = value;
//        //    }
//        //}

//        //[GnosisPropertyAttribute]
//        //public string GnosisGroupName
//        //{
//        //    get
//        //    {
//        //        return groupName;
//        //    }

//        //    set
//        //    {
//        //        groupName = value;
//        //    }
//        //}

//        [GnosisPropertyAttribute]
//        public bool GnosisChecked
//        {
//            get
//            {
//                return gnosisChecked;
//            }

//            set
//            {
//                gnosisChecked = value;
//                OnPropertyChanged("GnosisChecked");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public bool ReadOnly
//        {
//            get
//            {
//                return readOnly;
//            }

//            set
//            {
//                readOnly = value;
//                OnPropertyChanged("ReadOnly");
//            }
//        }

//        [GnosisPropertyAttribute]
//        public string Value
//        {
//            get
//            {
//                return valueField;
//            }

//            set
//            {
//                valueField = value;
//            }
//        }

       

//        public event PropertyChangedEventHandler PropertyChanged;

//        public void OnPropertyChanged(string name)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//        }

//        public void GnosisAddChild(IGnosisObject child)
//        {
//            throw new NotImplementedException();
//        }

      

       

//    }
//}
