using ShivaShared3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Events
{
    public class GnosisSystemCommands
    {
        //private IGnosisSystemCommandsImplementation commandsImplementation;



        public void RunSystemCommand(string name)
        {
            switch (name)
            {
                case "BackwardMenu":
                    GlobalData.Singleton.CurrentTileController.NavigateBack();
                    break;
                case "ForwardMenu":
                    GlobalData.Singleton.CurrentTileController.NavigateForward();
                    break;
                case "CutMenu":
                   // commandsImplementation.Cut();
                    break;
                case "CopyMenu":
                   // commandsImplementation.Copy();
                    break;
                case "PasteMenu":
                 //   commandsImplementation.Paste();
                    break;
                case "UndoMenu":
                 //   commandsImplementation.Undo();
                    break;
                case "RedoMenu":
                 //   commandsImplementation.Redo();
                    break;
                case "PrintMenu":
                 //   commandsImplementation.Print();
                    break;
                case "NewMenu":
                 //   commandsImplementation.New();
                    break;
                case "ToolTipStatusMenu":
                //    commandsImplementation.TooltipStatusToggle();
                    break;
                case "ProtectionStatusMenu":
                //    commandsImplementation.ProtectionStatusToggle();
                    break;
                case "PropertiesMenu":
                //    commandsImplementation.ShowSystemProperties();
                    break;
            }
        }
    }
}
