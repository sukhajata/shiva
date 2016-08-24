using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shiva.Shared.Interfaces;
using System.Windows;
using System.IO;
using Shiva.Shared.Data;

namespace ShivaWPF3.UtilityWPF
{
    public class GnosisErrorHandlerWPF : IGnosisErrorHandler
    {
        private int errNo = 1;

        public void HandleError(string message, string stackTrace)
        {
            string error = "err no." + errNo.ToString() + "\n" + message + "\n" + stackTrace;
            
            Application.Current.Dispatcher.Invoke(new Action(() => MessageBox.Show(error)  ));

            if (GlobalData.Singleton.IOHelper != null)
            {
                GlobalData.Singleton.IOHelper.LogError(error);
            }

            errNo++;
        }

        public void HandleError(string message, string stackTrace, int severity)
        {
            string error = "err no." + errNo.ToString() + "\n" + message + "\n" + stackTrace;
            GlobalData.Singleton.IOHelper.LogError(error);

            errNo++;

            if (severity > 1)
            {
                MessageBox.Show(error);
            }
        }

        public void HandleUnknowChildAddedError(string parentType, string childType)
        {
            string error = "Unknown object added to " + parentType + ": " + childType;

            Application.Current.Dispatcher.Invoke(new Action(() => MessageBox.Show(error)));

        }
    }
}
