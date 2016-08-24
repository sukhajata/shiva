using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.Events;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Utility;
using Shiva.Shared.ContainerControllers;
using GnosisControls;
using Shiva.Shared.GenericControllers;
using Shiva.Shared.DataControllers;
using Shiva.Shared.WindowControllers;
using Shiva.Shared.OuterLayoutControllers;
using System.ComponentModel;
using Shiva.Shared.BaseControllers;

namespace Shiva.Shared.Data
{
    public class GlobalData : INotifyPropertyChanged
    {
        private static GlobalData instance;

        private int lastID = -1;
        private GnosisFrameController currentFrameController;

        public event PropertyChangedEventHandler PropertyChanged;
        public IGnosisLoginDialog Login { get; set; }
        public GnosisApplicationManager ApplicationManager { get; set; }
        public GnosisSystemController SystemController { get; set; }
        public LayoutManager LayoutController { get; set; }
        public GnosisController.OrientationType AppOrientation { get; set; }
        public GnosisParentWindowController ParentWindowController { get; set; }
        public GnosisPrimarySplitController PrimarySplitController { get; set; }
        public GnosisConnection Connection { get; set; }
        public ServerCommunication.ServerConnection DatabaseConnection { get; set; }
        public GnosisNavigatorFrameController NavFrameController { get; set; }
        public GnosisApplicationCommands SystemCommands { get; set; }
        public IGnosisErrorHandler ErrorHandler { get; set; }
        public IGnosisIOHelper IOHelper { get; set; }
       // public IGnosisImplementationCreator ImplementationCreator { get; set; }
        public IGnosisParentWindowImplementation ParentWindowImplementation { get;
            set; }
        public GnosisTileController CurrentTileController { get; set; }
        public IGnosisStyleHelper StyleHelper { get; set; }
        public GnosisFrameController CurrentFrameController
        {
            get { return currentFrameController; }
            set
            {
                currentFrameController = value;
                OnPropertyChanged("CurrentFrameController");
            }
        }

        public enum GnosisDateFormat
        {
            SHORT,
            LONG
        };

        public enum GnosisTimeFormat
        {
            HOUR,
            MINUTE,
            SECOND,
            MILLISECOND
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private GlobalData() { }

        public static GlobalData Singleton
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalData();
                    //instance.LineHeight = Math.Ceiling(instance.FontSize * instance.ContentFontFamily.LineSpacing) + 0.4;
                }
                return instance;
            }
        }

        public GnosisGenericControlController FindGenericControllerByType(string controlType)
        {
            return SystemController.FindGenericControllerByType(controlType);

        }

        //public GnosisControl FindControl(int entityID, int controlID)
        //{
        //    GnosisControl control = null;
        //    if (ParentWindow.EntityID == entityID)
        //    {
        //        control = ParentWindow.FindControlByID(controlID);
        //    }
        //    else if (Connection.EntityID == entityID)
        //    {
        //        control =  Connection;
        //    }
        //    else if (Generic.EntityID == entityID)
        //    {
        //        control = Generic.FindControlByID(controlID);
        //    }
        //    else if (Global.EntityID == entityID)
        //    {
        //        control = Global.FindControlByID(controlID);
        //    }
        //    else if (NavFrame.EntityID == entityID)
        //    {
        //        control = NavFrame;
        //    }

        //    return control;
        //}

        public GnosisController FindController(int entityID, int controlID)
        {
            GnosisController controller = null;
            if (SystemController != null)
            {
                controller = SystemController.FindControllerByID(entityID, controlID);
            }
            
            return controller;
        }


        public int GetNewControlID()
        {
            return lastID--;
        }

        public int GetNewEntityID()
        {
            return lastID--;
        }


    }
}
