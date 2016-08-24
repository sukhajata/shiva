using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.ContentControllers;
using GnosisControls;
using Shiva.Shared.Data;

using Shiva.Shared.BaseControllers;

namespace GnosisControls
{
    public class GnosisTileDetail : GnosisControl
    {

        private GnosisController.DirectionType splitDirectionField;

        private GnosisController.DirectionType tabDirectionField;

        private GnosisController.DirectionType textDirectionField;

        private string tooltipField;



        [GnosisProperty]
        public string SplitDirection
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.DirectionType), splitDirectionField);
            }
            set
            {
                try
                {
                    splitDirectionField = (GnosisController.DirectionType)Enum.Parse(typeof(GnosisController.DirectionType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }

        }

        public GnosisController.DirectionType _SplitDirection
        {
            get { return splitDirectionField; }
            set { splitDirectionField = value; }
        }

        [GnosisProperty]
        public string TabDirection
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.DirectionType), tabDirectionField);
            }
            set
            {
                try
                {
                    tabDirectionField = (GnosisController.DirectionType)Enum.Parse(typeof(GnosisController.DirectionType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }

        }

        public GnosisController.DirectionType _TabDirection
        {
            get { return tabDirectionField; }
            set { tabDirectionField = value; }
        }



        [GnosisProperty]
        public string TextDirection
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.DirectionType), textDirectionField);
            }
            set
            {
                try
                {
                    textDirectionField = (GnosisController.DirectionType)Enum.Parse(typeof(GnosisController.DirectionType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }

        }

        public GnosisController.DirectionType _TextDirection
        {
            get { return textDirectionField; }
            set { textDirectionField = value; }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get { return tooltipField; }
            set { tooltipField = value; }
        }

    }
}
