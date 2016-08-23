using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ShivaShared3.Interfaces;
using ShivaShared3.Data;
using ShivaShared3.Events;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.Utility
{
    public class GnosisApplicationManager
    {
        public GnosisApplicationManager (
           // IGnosisParentWindowImplementation parentWindowImplementation,
            GnosisController.OrientationType orientation,
          //  IGnosisImplementationCreator creator, 
          //  IGnosisIOHelper ioHelper, 
            IGnosisSystemCommandsImplementation systemCommandsImplementation,
            IGnosisStyleHelper styleHelper)
        {
          //  GlobalData.Singleton.ParentWindowImplementation = parentWindowImplementation;
            GlobalData.Singleton.AppOrientation = orientation;
          //  GlobalData.Singleton.ImplementationCreator = creator;
          //  GlobalData.Singleton.IOHelper = ioHelper;
            GlobalData.Singleton.SystemCommands = new GnosisApplicationCommands(systemCommandsImplementation);
            GlobalData.Singleton.StyleHelper = styleHelper;
            GlobalData.Singleton.LayoutController = new LayoutManager();
        } 

        //public void Setup()
        //{
        //    GlobalData.Singleton.SystemController.SetupSystem();

        //    //GnosisSystem system = GnosisXMLHelper.LoadGnosisSystem("System.xml");
        //    //GnosisSystemController systemController = new GnosisSystemController(system);
        //    //GlobalData.Singleton.SystemController = systemController;
        //    //systemController.Setup();

        //    //GnosisGlobalGeneric global = GnosisXMLHelper.LoadGnosisGlobalGeneric(GlobalData.Instance.IOHelper.GetStreamReader("xml1_Global.xml"));
        //    //GnosisGenericEntityController globalController = new GnosisGenericEntityController(global);
        //    //GlobalData.Instance.GlobalGenericController = globalController;

        //    //GnosisConnection connection = GnosisXMLHelper.LoadGnosisConnection(GlobalData.Instance.IOHelper.GetStreamReader("xml2_Connection.xml"));
        //    //GlobalData.Instance.Connection = connection;

        //    //GnosisSystemGeneric generic = GnosisXMLHelper.LoadGnosisSystemGeneric(GlobalData.Instance.IOHelper.GetStreamReader("xml3_Generic.xml"));
        //    //GnosisGenericEntityController genericController = new GnosisGenericEntityController(generic);
        //    //GlobalData.Instance.SystemGenericController = genericController;

        //    //GnosisEntity parentWindowEntity = GnosisXMLHelper.LoadGnosisEntity("xml4_ParentWindow.xml");
        //    //GnosisEntityController parentWindowEntityController = new GnosisEntityController(parentWindowEntity, "xml4_ParentWindow.xml", null);
        //    //parentWindowEntityController.Setup();



        //}

        //public void LoadEntity()
        //{


        //}

    }
}
