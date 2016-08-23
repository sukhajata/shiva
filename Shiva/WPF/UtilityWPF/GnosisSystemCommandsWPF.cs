using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ShivaShared3.Data;
using ShivaShared3.Interfaces;

namespace ShivaWPF3.UtilityWPF
{
    public class GnosisSystemCommandsWPF : IGnosisSystemCommandsImplementation
    {
        public void Copy()
        {
            GlobalData.Singleton.ErrorHandler.HandleError("We're getting to that...", "GnosisSystemCommandsWPF");
        }

        public void Cut()
        {
            GlobalData.Singleton.ErrorHandler.HandleError("We're getting to that...", "GnosisSystemCommandsWPF");
        }

        public void New()
        {
            GlobalData.Singleton.ErrorHandler.HandleError("We're getting to that...", "GnosisSystemCommandsWPF");
        }

        public void Paste()
        {
            GlobalData.Singleton.ErrorHandler.HandleError("We're getting to that...", "GnosisSystemCommandsWPF");
        }

        public void Print()
        {
            GlobalData.Singleton.ErrorHandler.HandleError("We're getting to that...", "GnosisSystemCommandsWPF");
        }

        public void ShowSystemProperties()
        {
            GnosisSystemPropertiesDialog dialog = new GnosisSystemPropertiesDialog();
            dialog.Show();
            
        }

       
    }
}
