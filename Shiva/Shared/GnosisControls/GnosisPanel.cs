using ShivaShared3.BaseControllers;
using ShivaShared3.Data;
using GnosisControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public partial class GnosisPanel : INotifyPropertyChanged //: GnosisInnerLayoutControl
    {
        //Collections
        private List<GnosisButton> buttons;
        private List<GnosisCheckField> checkFields;
        //private List<GnosisCheckGroup> checkGroups;
        private List<GnosisComboField> comboFields;
        private List<GnosisDateField> dateFields;
        private List<GnosisDateTimeField> dateTimeFields;
        //private List<GnosisLabel> labels;
        private List<GnosisLinkField> linkFields;
        private List<GnosisListField> listFields;
        private List<GnosisNumberField> numberFields;
        private List<GnosisRadioField> radioFields;
        private List<GnosisRadioGroup> radioGroups;
        private List<GnosisTextField> textFields;


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
        private int maxSectionSpan;
        private int numColumns;
        private int order;
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

        public int NumColumns
        {
            get
            {
                return numColumns;
            }

            set
            {
                numColumns = value;
                OnPropertyChanged("NumColumns");
            }
        }


        
        //private List<GnosisToggleButtonGroup> toggleButtonGroups;



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private GnosisController.CaptionPosition captionRelativePositionField;

        
        [GnosisCollection]
        public List<GnosisButton> Buttons
        {
            get { return buttons; }
            set { buttons = value; }
        }


        [GnosisCollection]
        public List<GnosisCheckField> CheckFields
        {
            get { return checkFields; }
            set { checkFields = value; }
        }

        [GnosisCollection]
        public List<GnosisComboField> ComboFields
        {
            get { return comboFields; }
            set { comboFields = value; }
        }

        [GnosisCollection]
        public List<GnosisDateField> DateFields
        {
            get { return dateFields; }
            set { dateFields = value; }
        }

        [GnosisCollection]
        public List<GnosisDateTimeField> DateTimeFields
        {
            get { return dateTimeFields; }
            set { dateTimeFields = value; }
        }


        //[System.Xml.Serialization.XmlElementAttribute("GnosisLabelField")]
        //public List<GnosisLabelField> LabelFields
        //{
        //    get { return labelFields; }
        //    set { labelFields = value; }
        //}

        [GnosisCollection]
        public List<GnosisLinkField> LinkFields
        {
            get { return linkFields; }
            set { linkFields = value; }
        }

        [GnosisCollection]
        public List<GnosisListField> ListFields
        {
            get { return listFields; }
            set { listFields = value; }
        }

        
        [GnosisCollection]
        public List<GnosisRadioField> RadioFields
        {
            get { return radioFields; }
            set { radioFields = value; }
        }


        [GnosisCollection]
        public List<GnosisTextField> TextFields
        {
            get { return textFields; }
            set { textFields = value; }
        }

        [GnosisCollection]
        public List<GnosisNumberField> NumberFields
        {
            get { return numberFields; }
            set { numberFields = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisComboField)
            {
                if (comboFields == null)
                {
                    comboFields = new List<GnosisComboField>();
                }
                comboFields.Add((GnosisComboField)child);
            }
            else if (child is GnosisCheckField)
            {
                if (checkFields == null )
                {
                    checkFields = new List<GnosisCheckField>();
                }
                checkFields.Add((GnosisCheckField)child);
            }
            else if (child is GnosisButton)
            {
                if (buttons == null)
                {
                    buttons = new List<GnosisButton>();
                }
                buttons.Add((GnosisButton)child);
            }
            else if (child is GnosisDateField)
            {
                if (dateFields == null)
                {
                    dateFields = new List<GnosisDateField>();
                }
                dateFields.Add((GnosisDateField)child);
            }
            else if (child is GnosisDateTimeField)
            {
                if (dateTimeFields == null)
                {
                    dateTimeFields = new List<GnosisDateTimeField>();
                }
                dateTimeFields.Add((GnosisDateTimeField)child);
            }
            else if (child is GnosisLinkField)
            {
                if (linkFields == null)
                {
                    linkFields = new List<GnosisLinkField>();
                }
                linkFields.Add((GnosisLinkField)child);
            }
            else if (child is GnosisListField)
            {
                if (listFields == null)
                {
                    listFields = new List<GnosisListField>();
                }
                listFields.Add((GnosisListField)child);
            }
            else if (child is GnosisNumberField)
            {
                if (numberFields == null)
                {
                    numberFields = new List<GnosisNumberField>();
                }
                numberFields.Add((GnosisNumberField)child);
            }
            else if (child is GnosisRadioField)
            {
                if (radioFields == null)
                {
                    radioFields = new List<GnosisRadioField>();
                }
                radioFields.Add((GnosisRadioField)child);
            }
            else if (child is GnosisTextField)
            {
                if (textFields == null)
                {
                    textFields = new List<GnosisTextField>();
                }
                textFields.Add((GnosisTextField)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisPanel", child.GetType().Name);
            }
        }

    }
}
