using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.BaseControllers;

namespace GnosisControls
{
    public class GnosisRadioGroup : IGnosisRadioGroupImplementation
    {
        private bool locked;
        private bool readOnly;
        private IGnosisVisibleControlImplementation gnosisParent;

        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;

        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set
            {
                horizontalPadding = value;
              //  this.SetHorizontalPaddingExt(horizontalPadding);
            }
        }

        public int VerticalPadding
        {
            get { return verticalPadding; }
            set
            {
                verticalPadding = value;
               // this.SetVerticalPaddingExt(verticalPadding);
            }
        }

        public int HorizontalMargin
        {
            get { return horizontalMargin; }
            set
            {
                horizontalMargin = value;
             //   this.SetHorizontalMarginExt(horizontalMargin);
            }
        }

        public int VerticalMargin
        {
            get { return verticalMargin; }
            set
            {
                verticalMargin = value;
             //   this.SetVerticalMarginExt(verticalMargin);
            }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisProperty]
        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }


        [GnosisProperty]
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
            }
        }

        public string ContentVerticalAlignment
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public GnosisController.VerticalAlignmentType _ContentVerticalAlignment
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ContentHorizontalAlignment
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool DatasetCreated
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool DatasetUpdated
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool DatasetDeleted
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Dataset
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DatasetItem
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int MaxDisplayChars
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int MinDisplayChars
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool HasMouseFocus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool HasMouseDown
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Caption
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool HasFocus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Hidden
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Tooltip
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ControlType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ID
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string GnosisName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Order
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void SetHeight(double fieldHeight)
        {
            throw new NotImplementedException();
        }

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            throw new NotImplementedException();
        }

        public void SetStrikethrough(bool strikethrough)
        {
            throw new NotImplementedException();
        }

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType top)
        {
            throw new NotImplementedException();
        }

        public void SetWidth(double width)
        {
            throw new NotImplementedException();
        }

        public double GetPaddingHorizontal()
        {
            throw new NotImplementedException();
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            throw new NotImplementedException();
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            throw new NotImplementedException();
        }

        public void SetTooltipVisible(bool visible)
        {
            throw new NotImplementedException();
        }

        public void SetGotFocusHandler(Action action)
        {
            throw new NotImplementedException();
        }

        public void SetLostFocusHandler(Action action)
        {
            throw new NotImplementedException();
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            throw new NotImplementedException();
        }
    }
}
