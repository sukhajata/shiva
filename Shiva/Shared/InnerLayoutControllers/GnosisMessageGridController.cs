using ShivaShared3.DataControllers;
using GnosisControls;
using ShivaShared3.Interfaces;
using ShivaShared3.OuterLayoutControllers;
using System;
using System.Collections.Generic;
using System.Text;



namespace ShivaShared3.InnerLayoutControllers
{
    public class GnosisMessageGridController : GnosisInnerLayoutController
    {
        public GnosisMessageGridController (
            GnosisMessageGrid messgeGrid,
          //  IGnosisMessageGridImplementation messageGridImplementation,
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController parent)
            :base(messgeGrid, instanceController, parent)
        {

        }

        internal override void SizeChanged()
        {

        }

    }

  
}
