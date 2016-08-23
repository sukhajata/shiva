using System;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.BaseControllers;
using GnosisControls;
using ShivaShared3.OuterLayoutControllers;

namespace ShivaShared3.InnerLayoutControllers
{
    public class GnosisInnerLayoutController : GnosisVisibleController
    {
        protected bool editMode;
      //  protected int verticalSpacing;
        //protected int horizontalSpacing;
        
        //public string GnosisControlType
        //{
        //    get { return ControlImplementation.ControlType; }
        //}

        //public int SectionSpan
        //{
        //    get { return ((GnosisInnerLayoutControl)ControlImplementation).SectionSpan; }
        //    set
        //    {
        //        ((GnosisInnerLayoutControl)ControlImplementation).SectionSpan = value;
        //        OnPropertyChanged("SectionSpan");
        //    }
        //}



        public GnosisInnerLayoutController (
            IGnosisInnerLayoutControlImplementation gnosisLayoutControl, 
           // IGnosisInnerLayoutControlImplementation _controlImplementation,
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController parent)
            :base(gnosisLayoutControl, instanceController, parent)
        {

            //if (gnosisLayoutControl.Hidden)
            //{
            //    _controlImplementation.SetVisible(false);
            //}

            //if (gnosisLayoutControl.Caption != null)
            //{
            //    _controlImplementation.SetCaption(gnosisLayoutControl.Caption);
            //}

            //if (gnosisLayoutControl.MaximumPrintWidth != 0)
            //{
            //    _controlImplementation.SetMaxPrintWidth(gnosisLayoutControl.MaximumPrintWidth);
            //}

            //if (((GnosisInnerLayoutControl)ControlImplementation).ToolTip != null)
            //{
            //    _controlImplementation.SetTooltip(gnosisLayoutControl.ToolTip);
            //}


        }



        public virtual void SetEditMode()
        {
            editMode = true;
        }

        internal virtual void Save()
        {
            throw new NotImplementedException();
        }

        //internal void SetMargin(int margin)
        //{
        //    ((IGnosisLayoutControlImplementation)ControlImplementation).SetMargin(margin);
        //}

        //internal void SetMarginBottom(int marginBottom)
        //{
        //    ((IGnosisLayoutControlImplementation)ControlImplementation).SetMarginBottom(marginBottom);
        //}







        //public virtual void WidthChanged(double newWidth)
        //{

        //}



        public double GetWidth()
        {
            return ((IGnosisInnerLayoutControlImplementation)ControlImplementation).GetAvailableWidth();
        }


        //internal void SetVerticalSpacing(int _verticalSpacing)
        //{
        //    verticalSpacing = _verticalSpacing;
        //}

        internal virtual GnosisController FindControllerByID(int controlID)
        {
            throw new NotImplementedException();
        }

        //internal void SetHorizontalSpacing(int _horizontalSpacing)
        //{
        //    horizontalSpacing = _horizontalSpacing;
        //}

        internal virtual void SetStrikethrough(bool v)
        {
            throw new NotImplementedException();
        }

        internal virtual void SizeChanged()
        {
            throw new NotImplementedException();
        }




        //public virtual void OnClick()
        //{

        //}

        //public virtual void OnMouseOver()
        //{

        //}

        //public virtual void OnRightClick()
        //{

        //}


    }
}
