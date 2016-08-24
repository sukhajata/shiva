using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public partial class GnosisMessageGrid : INotifyPropertyChanged // : GnosisInnerLayoutControl
    {
        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;

        private string caption;
        private GnosisController.HorizontalAlignmentType captionHorizontalAlignment;
        private GnosisController.VerticalAlignmentType captionVerticalAlignment;
        private GnosisController.CaptionPosition captionRelativePosition;
        private string controlType;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hidden;
        private int id;
        private int maxDisplayRows;
        private int maxLines;
        private int maxSectionSpan;
        private int minDisplayRows;
        private int order;
        private int maxWrapRows;
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
        public int MaxWrapRows
        {
            get
            {
                return maxWrapRows;
            }

            set
            {
                maxWrapRows = value;
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
    }
}
