using System;
using System.Collections.Generic;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.Data;
using GnosisControls;
using System.ComponentModel;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public partial class GnosisGrid : INotifyPropertyChanged //: GnosisInnerLayoutControl
    {
        private List<GnosisCheckColumn> checkColumns;
        private List<GnosisComboColumn> comboColumns;
        private List<GnosisDateColumn> dateColumns;
        private List<GnosisDateTimeColumn> dateTimeColumns;
        private List<GnosisLinkColumn> linkColumns;
        private List<GnosisNumberColumn> numberColumns;
        private List<GnosisTextColumn> textColumns;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private GnosisController.HorizontalAlignmentType captionHorizontalAlignment;
        private GnosisController.VerticalAlignmentType captionVerticalAlignment;
        private GnosisController.CaptionPosition captionRelativePosition;
        private string controlType;
        private string dataset;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int maxDisplayRows;
        private int maxLines;
        private int maxSectionSpan;
        private int maxWrapRows;
        private int minDisplayRows;
        private bool multipleRowSelection;
        private int numColumns;
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

        public int NumColumns
        {
            get { return numColumns; }
            set
            {
                numColumns = value;
                OnPropertyChanged("NumColumns");
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



        public bool GridIsLoaded
        {
            get
            {
                return this.IsLoaded;
            }
        }

        public bool GridIsVisible
        {
            get
            {
                return this.IsVisible;
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionRelativePosition
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.CaptionPosition), captionRelativePosition);
            }

            set
            {
                try
                {
                    captionRelativePosition = (GnosisController.CaptionPosition)Enum.Parse(typeof(GnosisController.CaptionPosition), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionAlignmentHorizontal
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), captionHorizontalAlignment);
            }

            set
            {
                try
                {
                    captionHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        [GnosisPropertyAttribute]
        public string CaptionAlignmentVertical
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), captionVerticalAlignment);
            }

            set
            {
                try
                {
                    captionVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
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
            }
        }

        [GnosisPropertyAttribute]
        public int MinDisplayRows
        {
            get
            {
                return minDisplayRows;
            }

            set
            {
                minDisplayRows = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxDisplayRows
        {
            get
            {
                return maxDisplayRows;
            }

            set
            {
                maxDisplayRows = value;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxLines
        {
            get
            {
                return maxLines;
            }

            set
            {
                maxLines = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool MultipleRowSelection
        {
            get
            {
                return multipleRowSelection;
            }

            set
            {
                multipleRowSelection = value;
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

        [GnosisProperty]
        public int MaxWrapRows
        {
            get { return maxWrapRows; }
            set { maxWrapRows = value; }
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

      

        [GnosisCollection]
        public List<GnosisComboColumn> ComboColumns
        {
            get { return comboColumns; }
            set { comboColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisCheckColumn> CheckColumns
        {
            get { return checkColumns; }
            set { checkColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisDateTimeColumn> DateTimeColumns
        {
            get { return dateTimeColumns; }
            set { dateTimeColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisDateColumn> DateColumns
        {
            get { return dateColumns; }
            set { dateColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisLinkColumn> LinkColumns
        {
            get { return linkColumns; }
            set { linkColumns = value; }

        }

        [GnosisCollection]
        public List<GnosisTextColumn> TextColumns
        {
            get { return textColumns; }
            set { textColumns = value; }
        }

        [GnosisCollection]
        public List<GnosisNumberColumn> NumberColumns
        {
            get { return numberColumns; }
            set { numberColumns = value; }
        }

        public virtual void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisCheckColumn)
            {
                checkColumns.Add((GnosisCheckColumn)child);
            }
            else if (child is GnosisComboColumn)
            {
                comboColumns.Add((GnosisComboColumn)child);
            }
            else if (child is GnosisDateColumn)
            {
                dateColumns.Add((GnosisDateColumn)child);
            }
            else if (child is GnosisDateTimeColumn)
            {
                dateTimeColumns.Add((GnosisDateTimeColumn)child);
            }
            else if (child is GnosisLinkColumn)
            {
                linkColumns.Add((GnosisLinkColumn)child);
            }
            else if (child is GnosisNumberColumn)
            {
                numberColumns.Add((GnosisNumberColumn)child);
            }
            else if (child is GnosisTextColumn)
            {
                textColumns.Add((GnosisTextColumn)child);
            }
        }


    }
}
