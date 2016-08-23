using System;
using System.Collections.Generic;
using System.Text;

using ShivaShared3.Data;
using ShivaShared3.GenericControllers;
using GnosisControls;
using ShivaShared3.Interfaces;
using ShivaShared3.OuterLayoutControllers;

namespace ShivaShared3.Events
{
    public class GnosisApplicationCommands
    {
        private IGnosisSystemCommandsImplementation commandsImplementation;


        public GnosisApplicationCommands(IGnosisSystemCommandsImplementation implementation)
        {
            commandsImplementation = implementation;
        }

        public void RunSystemCommand(GnosisGenericMenuItem.MenuTagEnum menuTag)
        {
            switch (menuTag)
            {
                case GnosisGenericMenuItem.MenuTagEnum.BACKWARD:
                    GlobalData.Singleton.CurrentTileController.NavigateBack();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.FORWARD:
                    GlobalData.Singleton.CurrentTileController.NavigateForward();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.CUT:
                    commandsImplementation.Cut();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.COPY:
                   commandsImplementation.Copy();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.PASTE:
                    commandsImplementation.Paste();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.UNDO:
                    if (GlobalData.Singleton.SystemController.CurrentInstanceController != null)
                    {
                        GlobalData.Singleton.SystemController.Undo();
                    }
                        //GnosisGenericControlController generic = GlobalData.Singleton.FindGenericControllerByType("Document Frame");
                        //((GnosisDocFrameController)CurrentInstance).Undo();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.REDO:
                    if (GlobalData.Singleton.SystemController.CurrentInstanceController != null)
                    {
                        GlobalData.Singleton.SystemController.Redo();
                    }
                        //GnosisGenericControlController generic1 = GlobalData.Singleton.FindGenericControllerByType("Document Frame");
                        //((GnosisDocFrameController)generic1.CurrentInstance).Redo();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.PRINT:
                    commandsImplementation.Print();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.NEW:
                    GlobalData.Singleton.ErrorHandler.HandleError("New command not implemented", "GnosisApplicationManager");
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.EDIT:
                    if (GlobalData.Singleton.SystemController.CurrentInstanceController != null)
                    {
                        GlobalData.Singleton.SystemController.ToggleEdit();
                    }
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.CANCEL:
                    if (GlobalData.Singleton.SystemController.CurrentInstanceController != null)
                    {
                        GlobalData.Singleton.SystemController.Cancel();
                    }
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.SAVE:
                    if (GlobalData.Singleton.SystemController.CurrentInstanceController != null)
                    {
                        GlobalData.Singleton.SystemController.Save();
                    }
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.DELETE:
                    if (GlobalData.Singleton.SystemController.CurrentInstanceController != null)
                    {
                        GlobalData.Singleton.SystemController.Delete();
                    }
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.SEARCH:
                    if (GlobalData.Singleton.CurrentFrameController is GnosisSearchFrameController)
                    {
                        ((GnosisSearchFrameController)GlobalData.Singleton.CurrentFrameController).Search();
                    }
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.TOOLTIP:
                    GlobalData.Singleton.SystemController.ShowTooltips = !GlobalData.Singleton.SystemController.ShowTooltips;
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.PROTECTIONSTATUS:
                    //code in GnosisGenericMenuGroupController Child_PropertyChanged
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.PROPERTIES:
                   commandsImplementation.ShowSystemProperties();
                    break;
                case GnosisGenericMenuItem.MenuTagEnum.NAVIGATOR:
                    GlobalData.Singleton.PrimarySplitController.ToggleNavigatorVisible();
                    break;
            }
        }
    }
}
