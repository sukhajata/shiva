using ShivaShared3.BaseControllers;
using ShivaShared3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisNumberResults : GnosisResultsColumn
    {
        private string unitofMeasure;
        private GnosisController.CaptionPosition measureRelativePosition;

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

        public string UnitOfMeasure
        {
            get { return unitofMeasure; }
            set { unitofMeasure = value; }
        }

    }
}
