using System;
using System.Collections.Generic;
using ShivaShared3.BaseControllers;

using ShivaShared3.Events;
using ShivaShared3.Data;

namespace GnosisControls
{
    public class GnosisInnerLayoutControl : GnosisVisibleControl
    {
       
        protected double maximumPrintWidthField;

        protected GnosisController.CaptionPosition captionRelativePosition;

        protected GnosisController.HorizontalAlignmentType captionHorizontalAlignment;

        protected int sectionSpan;

        protected List<GnosisEventHandler> eventHandlers;


        [GnosisProperty]
        public double MaximumPrintWidth
        {
            get
            {
                return this.maximumPrintWidthField;
            }
            set
            {
                this.maximumPrintWidthField = value;
            }
        }

        [GnosisProperty]
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

        
        [GnosisProperty]
        public string CaptionHorizontalAlignment
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

        
        [GnosisCollection]
        public List<GnosisEventHandler> EventHandlers
        {
            get { return eventHandlers; }
            set { eventHandlers = value; }
        }

        [GnosisProperty]
        public int SectionSpan
        {
            get { return sectionSpan; }
            set { sectionSpan = value; }
        }

     

     



    }
}
