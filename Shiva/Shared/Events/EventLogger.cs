using GnosisControls;
using ShivaShared3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Events
{
    public class EventLogger
    {
        //public static void LogEvent(GnosisEventHandler handler, string targetName, bool result)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("(EventType: " + handler.EventTypeAsString + ")  ");
        //    builder.Append("(TargetControl: " + targetName + ") ");
        //    builder.Append("(TargetAction: " + handler.TargetActionAsString + ") ");
        //    builder.Append("(TargetProperty: " + handler.TargetPropertyAsString + ") ");
        //    builder.Append("(TargetEvent: " + handler.TargetEventAsString + ") ");
        //    builder.Append("(SourceExpression: " + result.ToString() + ") ");

        //    GlobalData.Singleton.IOHelper.LogEvent(builder.ToString());
        //}

        public static void LogSourceExpression(string booleanSequence, bool result)
        {
            string line = booleanSequence + ", result = " + result.ToString();

            GlobalData.Singleton.IOHelper.LogSourceExpression(line);
        }
    }
}
