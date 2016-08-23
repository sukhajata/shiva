
using System;
using System.Collections.Generic;

using ShivaShared3.ContentControllers;
using GnosisControls;
using ShivaShared3.Data;
using ShivaShared3.ContainerControllers;

using ShivaShared3.BaseControllers;

namespace GnosisControls
{
    public class GnosisSplitDetail : GnosisControl
    {

        private GnosisController.OrientationType orientationField;

        private GnosisController.DirectionType splitDirectionField;

        private double splitPercentageField;

        private int splitUnitsField;

        private bool usingUnitsField;

        //public static string ORIENTATION_PORTRAIT = "Portrait";
        //public static string ORIENTATION_LANDSCAPE = "Landscape";

        //public static string DIRECTION_VERTICAL = "Vertical";
        //public static string DIRECTION_HORIZONTAL = "Horizontal";

        //public static string ORIENTATION_ATTRIBUTE_NAME = "Orientation";
        //public static string SPLIT_DIRECTION_ATTRIBUTE_NAME = "SplitDirection";
        //public static string SPLIT_PERCENTAGE_ATTRIBUTE_NAME = "SplitPercentage";
        //public static string SPLIT_UNITS_ATTRIBUTE_NAME = "SplitUnits";


        [GnosisProperty]
        public string GnosisOrientation
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.OrientationType), this.orientationField);
            }
            set
            {
               try
                {
                    this.orientationField = (GnosisController.OrientationType)Enum.Parse(typeof(GnosisController.OrientationType), value.ToUpper());
                   // this.orientationStringField = value;
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.OrientationType _GnosisOrientation
        {
            get { return orientationField; }
            set
            {
                orientationField = value;
            }
        }

        [GnosisProperty]
        public string SplitDirection
        {
            get
            {
                return Enum.GetName(typeof(GnosisSplitController.DirectionType), splitDirectionField);
            }
            set
            {
                try
                {
                    this.splitDirectionField = (GnosisSplitController.DirectionType)Enum.Parse(typeof(GnosisSplitController.DirectionType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisSplitController.DirectionType _SplitDirection
        {
            get { return splitDirectionField; }
            set
            {
                splitDirectionField = value;
            }
        }

        [GnosisProperty]
        public double SplitPercentage
        {
            get
            {
                return this.splitPercentageField;
            }
            set
            {
                this.splitPercentageField = value;
              //  OnPropertyChanged("SplitPercentage");
            }
        }

        //Update splitPercentage without firing an event
        public void UpdateSplitPercentage(double newPercent)
        {
            this.splitPercentageField = newPercent;
        }

        [GnosisProperty]
        public int SplitUnits
        {
            get
            {
                return this.splitUnitsField;
            }
            set
            {
                this.splitUnitsField = value;
              //  OnPropertyChanged("SplitUnits");
            }
        }

        [GnosisProperty]
        public bool UsingUnits
        {
            get { return usingUnitsField; }
            set { usingUnitsField = value; }
        }


    }
}