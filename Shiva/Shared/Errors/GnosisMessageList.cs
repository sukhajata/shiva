using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using ShivaShared3.Data;

namespace GnosisControls
{
    public class GnosisMessageList : IGnosisObject
    {
        private List<GnosisMessage> messages;


        [GnosisCollection]
        public List<GnosisMessage> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisMessage)
            {
                if (messages == null)
                {
                    messages = new List<GnosisMessage>();
                }
                messages.Add((GnosisMessage)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisMessageList", child.GetType().Name);
            }
        }
    }
}
