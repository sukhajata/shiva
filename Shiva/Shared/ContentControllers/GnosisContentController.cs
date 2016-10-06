using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.DataControllers;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.Data;
using Shiva.Shared.PanelFieldControllers;
using Shiva.Shared.BaseControllers;

namespace Shiva.Shared.ContentControllers
{
    public class GnosisContentController  : GnosisVisibleController
    {
        protected int colSpan;
        protected double minFieldWidth;
        protected double maxFieldWidth;
        protected double fieldHeight;
        protected double characterWidth;


        public int MinDisplayChars
        {
            get
            {
                if (ControlImplementation is IGnosisDisplayCharsPossessor)
                {
                    return ((IGnosisDisplayCharsPossessor)ControlImplementation).MinDisplayChars;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (ControlImplementation is IGnosisDisplayCharsPossessor)
                {
                    ((IGnosisDisplayCharsPossessor)ControlImplementation).MinDisplayChars = value;
                    OnPropertyChanged("MinDisplayCharacters");
                }
            }
        }


        public int MaxDisplayChars
        {
            get
            {
                if (ControlImplementation is IGnosisDisplayCharsPossessor)
                {
                    return ((IGnosisDisplayCharsPossessor)ControlImplementation).MaxDisplayChars;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (ControlImplementation is IGnosisDisplayCharsPossessor)
                {
                    ((IGnosisDisplayCharsPossessor)ControlImplementation).MaxDisplayChars = value;
                    OnPropertyChanged("MaxDisplayCharacters");
                }
            }
        }

        //public int MaxChars
        //{
        //    get { return ((IGnosisContentControlImplementation)ControlImplementation).MaxChars; }
        //    set
        //    {
        //        ((IGnosisContentControlImplementation)ControlImplementation).MaxChars = value;
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

        public double CharacterWidth
        {
            get { return characterWidth; }
            set { characterWidth = value; }
        }

        internal virtual void Save(int rowNo)
        {
            throw new NotImplementedException();
        }

        public int ColSpan
        {
            get { return colSpan; }
            set
            {
                colSpan = value;
            }
        }

        public int ColNo
        {
            get; set;
        }

        public string Dataset
        {
            get { return ((IGnosisContentControlImplementation)ControlImplementation).Dataset; }
        }

        public string DatasetItem
        {
            get { return ((IGnosisContentControlImplementation)ControlImplementation).DatasetItem; }
        }

        public GnosisContentController(
            IGnosisContentControlImplementation contentControl,
          //  IGnosisContentControlImplementation contentControlImplementation,
            GnosisInstanceController instanceController,
            GnosisContentController parent)
            : base(contentControl, instanceController, parent)
        {
            //Initialize();
        }

        public GnosisContentController(
            IGnosisContentControlImplementation contentControl, 
           // IGnosisContentControlImplementation contentControlImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(contentControl, instanceController, parent)
        {
           // Initialize();
        }


        protected override void Initialize()
        {
            base.Initialize();

            if (ControlImplementation is IGnosisCaptionLabelPossessor)
            {
                string caption = ((IGnosisContentControlImplementation)ControlImplementation).Caption;
                GnosisCaptionLabel captionLabel = new GnosisCaptionLabel();
                //  IGnosisCaptionLabelImplementation captionLabelImp = GlobalData.Singleton.ImplementationCreator.GetGnosisCaptionLabelImplementation();
                GlobalData.Singleton.StyleHelper.ApplyCaptionStyle(captionLabel, EntityController.GetCaptionStyle());
                captionLabel.Caption = caption;
                ((IGnosisCaptionLabelPossessor)ControlImplementation).CaptionLabel = captionLabel;
            }
        }


        internal void SetEditMode()
        {
            throw new NotImplementedException();
        }

        public virtual void LoadData(int rowNo)
        {
            throw new NotImplementedException();
        }

        //internal void SetFont(string font)
        //{
        //    ((IGnosisContentControlImplementation)ControlImplementation).SetFont(font);
        //}

        //internal void SetFontSize(int fontSize)
        //{
        //    ((IGnosisContentControlImplementation)ControlImplementation).SetFontSize(fontSize);
        //}

        internal void SetStrikethrough(bool strikethrough)
        {
            ((IGnosisContentControlImplementation)ControlImplementation).SetStrikethrough(strikethrough);
        }

        ////internal void SetForegroundColour(string contentColour)
        ////{
        ////    ((IGnosisContentControlImplementation)ControlImplementation).SetForegroundColour(contentColour);
        ////}

        protected virtual void SetDisplayDimensions()
        {
            string font = EntityController.GetNormalStyle().Font;
            int fontSize = EntityController.GetNormalStyle().FontSize;

            if (ControlImplementation is IGnosisDisplayCharsPossessor)
            {
                characterWidth = GlobalData.Singleton.StyleHelper.GetCharacterWidth((IGnosisContentControlImplementation)ControlImplementation, EntityController.GetNormalStyle().Font, EntityController.GetNormalStyle().FontSize);

                //display chars
                if (MinDisplayChars == 0)
                {
                    if (this.Dataset == null || this.DatasetItem == null)
                    {
                        GlobalData.Singleton.ErrorHandler.HandleError("Dataset or DatasetItem not defined for " + this.ControlImplementation.GnosisName + " " + ControlTypeMapping.GetControlTypeName(this.GetType()), "GnosisContentController");
                    }
                    else
                    {
                        MinDisplayChars = EntityController.GetMinDisplayChars(Dataset, DatasetItem);
                    }
                }

                if (MaxDisplayChars == 0)
                {
                    MaxDisplayChars = EntityController.GetMaxDisplayChars(Dataset, DatasetItem);
                }


                //field width
               
                if (MaxDisplayChars > 0)
                {
                    //MaxFieldWidth = StyleManager.GetMaxFieldWidth(MaxDisplayChars);
                    MaxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxFieldWidth((IGnosisContentControlImplementation)ControlImplementation, font, fontSize, MaxDisplayChars);
                }

                if (MinDisplayChars > 0)
                {
                    //MinFieldWidth = StyleManager.GetMinFieldWidth(MinDisplayChars);
                    MinFieldWidth = GlobalData.Singleton.StyleHelper.GetMinFieldWidth((IGnosisContentControlImplementation)ControlImplementation, font, fontSize, MinDisplayChars);
                }


                if (MinFieldWidth > MaxFieldWidth)
                {
                    MinFieldWidth = MaxFieldWidth;
                }
                else if (MaxFieldWidth < MinFieldWidth)
                {
                    MaxFieldWidth = MinFieldWidth;
                }

                //if (ControlImplementation is IGnosisCaptionLabelPossessor)
                //{
                //    GnosisCaptionLabel captionLabel = ((IGnosisCaptionLabelPossessor)ControlImplementation).CaptionLabel;
                //    if (captionLabel.RelativePosition == CaptionPosition.LEFT || captionLabel.RelativePosition == CaptionPosition.RIGHT)
                //    {
                //        double captionWidth = captionLabel.GetWidth() + (2 * captionLabel.HorizontalMargin);
                //        minFieldWidth += captionWidth;
                //        maxFieldWidth += captionWidth;
                //    }
                //}
            }

            //field height
            fieldHeight = GlobalData.Singleton.StyleHelper.GetFieldHeight((IGnosisContentControlImplementation)ControlImplementation, font, fontSize);

            ((IGnosisContentControlImplementation)ControlImplementation).SetHeight(fieldHeight);

            ((IGnosisContentControlImplementation)ControlImplementation).SetVerticalAlignment(VerticalAlignmentType.TOP);


            if (((IGnosisContentControlImplementation)ControlImplementation)._ContentHorizontalAlignment == HorizontalAlignmentType.NONE)
            {
                if (Dataset != null && DatasetItem != null)
                {
                    HorizontalAlignmentType ha = EntityController.GetContentHorizontalAlignment(Dataset, DatasetItem);
                    ((IGnosisContentControlImplementation)ControlImplementation)._ContentHorizontalAlignment = ha;
                }
               
            }

        }


       

    }
}
