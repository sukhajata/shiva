using ShivaShared3.DataControllers;
using GnosisControls;
using ShivaShared3.InnerLayoutControllers;
using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;

namespace ShivaShared3.GridColumnControllers
{
    public class GnosisResultsColumnController : GnosisGridColumnController
    {
        public bool RowSelected
        {
            get { return ((GnosisResultsColumn)columnModel).RowSelected; }
            set { ((GnosisResultsColumn)columnModel).RowSelected = value; }
        }

        public GnosisResultsColumnController(
         GnosisResultsColumn column,
         GnosisInstanceController _instanceController,
         GnosisSearchResultsGridController _parent)
            :base (column, _instanceController, _parent)
        {
            
        }

        public override IGnosisGridFieldImplementation GetFieldClone()
        {
            IGnosisGridFieldImplementation gridField = base.GetFieldClone();
            gridField.Locked = true;

            return gridField;
        }

       


    }
}
