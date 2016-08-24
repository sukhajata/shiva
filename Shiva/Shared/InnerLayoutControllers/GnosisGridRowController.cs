using Shiva.Shared.ContentControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.InnerLayoutControllers
{

    public class GnosisGridRowController
    {
        protected GnosisGridController parent;
        protected int rowNum;
        protected double maxHeight;
        protected bool rowSelected;
        protected bool isEvenRow;

        public List<IGnosisGridFieldImplementation> Fields;

        //public bool RowSelected
        //{
        //    get { return rowSelected; }
        //    set
        //    {
        //        rowSelected = value;
        //        foreach (GnosisGridFieldController field in Fields)
        //        {
        //            field.RowSelected = rowSelected;
        //        }
        //    }
        //}

        public GnosisGridRowController(GnosisGridController _parent, int _rowNum)
        {
            parent = _parent;
            rowNum = _rowNum;
            Fields = new List<IGnosisGridFieldImplementation>();

            if (rowNum % 2 != 0) //rowNum starts from 0
            {
                isEvenRow = true;
            }
        }

        public virtual void AddCell(IGnosisGridFieldImplementation gridFieldImp)
        {
            gridFieldImp.IsEvenRow = isEvenRow;
            gridFieldImp.SetGotFocusHandler(new Action(OnGotFocus));
            gridFieldImp.SetLostFocusHandler(new Action(OnLostFocus));


            Fields.Add(gridFieldImp);

        }

        public virtual void OnGotFocus()
        {
            foreach (IGnosisGridFieldImplementation gridField in Fields)
            {
                gridField.RowSelected = true;
            }
        }

        public virtual void OnLostFocus()
        {
            foreach (IGnosisGridFieldImplementation gridField in Fields)
            {
                gridField.RowSelected = false;
            }
        }



        //protected virtual void Cell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("HasFocus"))
        //    {
        //        if (((GnosisGridFieldController)sender).HasFocus)
        //        {
        //            RowSelected = true;
        //            parent.OnRowSelected(rowNum);
        //        }
        //    }
        //}


        //public void HeightChanged(double newHeight, GnosisGridFieldController changedCell)
        //{
        //    if (newHeight > maxHeight)
        //    {
        //        maxHeight = newHeight;
        //        foreach (GnosisGridFieldController cell in Fields)
        //        {
        //            if (cell != changedCell)
        //            {
        //                //cell.SetHeightBinding(cell);
        //            }
        //        }
        //    }
        //}

    }
}
