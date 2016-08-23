using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.OuterLayoutControllers;
using ShivaShared3.DataControllers;
using ShivaShared3.Data;

namespace ShivaShared3.InnerLayoutControllers
{
    public class GnosisTextAreaController : GnosisInnerLayoutController
    {
        private double minFieldWidth;
        private double maxFieldWidth;
        private double fieldHeight;
        private double characterWidth;
        private double textHeight;

        //public bool Locked
        //{
        //    get { return ((GnosisTextArea)ControlImplementation).Locked; }
        //    set
        //    {
        //        ((GnosisTextArea)ControlImplementation).Locked = value;
        //        OnPropertyChanged("Locked");
        //    }
        //}

        //public bool ReadOnly
        //{
        //    get { return ((GnosisTextArea)ControlImplementation).ReadOnly; }
        //}

        //public int MinDisplayChars
        //{
        //    get { return ((GnosisTextArea)ControlImplementation).MinDisplayChars; }
        //    set
        //    {
        //        ((GnosisTextArea)ControlImplementation).MinDisplayChars = value;
        //        OnPropertyChanged("MinDisplayCharacters");
        //    }
        //}


        //public int MaxDisplayChars
        //{
        //    get
        //    {
        //        return ((GnosisTextArea)ControlImplementation).MaxDisplayChars;
        //    }
        //    set
        //    {
        //        ((GnosisTextArea)ControlImplementation).MaxDisplayChars = value;
        //        OnPropertyChanged("MaxDisplayCharacters");
        //    }
        //}

        //public int MaxChars
        //{
        //    get { return ((GnosisTextArea)ControlImplementation).MaxChars; }
        //    set
        //    {
        //        ((GnosisTextArea)ControlImplementation).MaxChars = value;
        //        OnPropertyChanged("MaxCharacters");
        //    }
        //}

        public double MinFieldWidth
        {
            get { return minFieldWidth; }
            set { minFieldWidth = value; }
        }

        public double MaxFieldWidth
        {
            get { return maxFieldWidth; }
            set { maxFieldWidth = value; }
        }

        public double FieldHeight
        {
            get { return fieldHeight; }
            set { fieldHeight = value; }
        }

        public double TextHeight
        {
            get { return textHeight; }
            set { textHeight = value; }
        }

        public double CharacterWidth
        {
            get { return characterWidth; }
            set { characterWidth = value; }
        }

       

        public string Dataset
        {
            get { return ((GnosisTextArea)ControlImplementation).Dataset; }
        }

        public string DatasetItem
        {
            get { return ((GnosisTextArea)ControlImplementation).DatasetItem; }
        }


        
        public GnosisTextAreaController(
            GnosisTextArea textArea,
         //   IGnosisTextAreaImplementation textAreaImplementation,
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController parent)
            :base(textArea, instanceController, parent)
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            // ((IGnosisTextAreaImplementation)ControlImplementation).SetHorizontalAlignment(HorizontalAlignmentType.Left);
           // ((IGnosisTextAreaImplementation)ControlImplementation).SetVerticalContentAlignment(((GnosisTextArea)ControlImplementation).ContentVerticalAlignment);

            string font = InstanceController.EntityController.GetNormalStyle().Font;
            int fontSize = InstanceController.EntityController.GetNormalStyle().FontSize;

            //if (((GnosisTextArea)ControlImplementation).ContentHorizontalAlignment != HorizontalAlignmentType.NONE)
            //{
            //    ((IGnosisTextAreaImplementation)ControlImplementation).SetHorizontalContentAlignment(((GnosisTextArea)ControlImplementation).ContentHorizontalAlignment);
            //}

            //if (((GnosisTextArea)ControlImplementation).ReadOnly)
            //{
            //    ((IGnosisTextAreaImplementation)ControlImplementation).Locked = true;
            //}

            //if (((GnosisTextArea)ControlImplementation).Disabled)
            //{
            //    ((IGnosisTextAreaImplementation)ControlImplementation).SetIsEnabled(false);
            //}

            if (((GnosisTextArea)ControlImplementation).MinDisplayChars > 0)
            {
                //minFieldWidth = StyleManager.GetMinFieldWidth(MinDisplayChars);
                minFieldWidth = GlobalData.Singleton.StyleHelper.GetMinTextAreaWidth((GnosisTextArea)ControlImplementation, font, fontSize, ((GnosisTextArea)ControlImplementation).MinDisplayChars);
            }

            if (((GnosisTextArea)ControlImplementation).MaxDisplayChars > 0)
            {
                //maxFieldWidth = StyleManager.GetMaxFieldWidth(MaxDisplayChars);
                maxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxTextAreaWidth((GnosisTextArea)ControlImplementation, font, fontSize, ((GnosisTextArea)ControlImplementation).MaxDisplayChars);
            }

            textHeight = GlobalData.Singleton.StyleHelper.GetTextHeight((GnosisTextArea)ControlImplementation, font, fontSize);

            characterWidth = GlobalData.Singleton.StyleHelper.GetCharacterWidth((GnosisTextArea)ControlImplementation, font, fontSize);



            //if (!contentControl.HideCaption)
            //{
            //    contentControlImplementation.SetCaptionSpan(contentControl.CaptionCellSpan, contentControl.CellSpan);
            //}
        }

    }
}
