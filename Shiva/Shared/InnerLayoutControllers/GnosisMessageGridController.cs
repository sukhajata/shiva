using Shiva.Shared.DataControllers;
using GnosisControls;
using Shiva.Shared.Interfaces;
using Shiva.Shared.OuterLayoutControllers;
using System;
using System.Collections.Generic;
using System.Text;



namespace Shiva.Shared.InnerLayoutControllers
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
