using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.DataControllers;
using Shiva.Shared.OuterLayoutControllers;

namespace Shiva.Shared.InnerLayoutControllers
{
    public class GnosisCalendarController : GnosisInnerLayoutController
    {
        public GnosisCalendarController(
            GnosisCalendar calendar,
            //IGnosisCalendarImplementation calendarImplementation,
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController parent)
            :base (calendar, instanceController, parent)
        {

        }


    }
}
