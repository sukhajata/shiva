using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisParentWindowImplementation : IGnosisContainerImplementation
    {
        //properties
        string GnosisIcon { get; set; }
        string GnosisOrientation { get; set; }

        void SetLoadedHandler(Action<double> loadedHandler);
      //  void LoadPrimarySplit(IGnosisPrimarySplitImplementation primarySplit);
       // void LoadToolbarTray(IGnosisToolbarTrayImplementation toolBarTrayImplementation);
        //    void LoadToolbarTray(IGnosisToolbarTrayImplementation toolbarTray);
        void WriteToWindow(string status);
        void ShowXML(string msg);
    }
}
