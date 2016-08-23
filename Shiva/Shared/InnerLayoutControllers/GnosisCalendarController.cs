using System;
using System.Collections.Generic;
using System.Text;

using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.BaseControllers;
using ShivaShared3.DataControllers;
using ShivaShared3.OuterLayoutControllers;

namespace ShivaShared3.InnerLayoutControllers
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
