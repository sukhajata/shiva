using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Utility
{
    public class IconNameMapper
    {
        private static Dictionary<string, string> IconNames = new Dictionary<string, string>
        {
          //  { "NewMenu", "new" },

          //  {"ProtectionStatusMenu", "pen" },
            {"Backward", "back" },
            {"Cancel", "cancel" },
            {"Cut", "cut" },
            {"Copy", "copy" },
            {"Delete", "delete" },
            {"Edit", "edit" },
            {"Forward", "forward" },
            {"Help", "help" },
            {"Insert", "insert" },
            {"Locked", "locked" },
            {"Navigator", "navigator" },
            {"New", "new" },
            //   {"EditMenu", "edit" },
            {"Paste", "paste" },
            {"Print", "print" },
            {"PropertiesMenu", "properties" },
            {"Protected", "protected" },
            {"Properties", "properties" },
            {"Redo", "redo" },
            {"Refresh" , "refresh"},
            {"Remove", "remove" },
            {"Reset", "reset" },
            {"Save", "save" },
            {"Search",  "search"},
            {"Tooltip", "tooltip" },
            {"Undo", "undo" },
            {"Unprotected", "unprotected" }

        };

        public static string GetIconFileName(string iconName)
        {

            if (IconNames.ContainsKey(iconName))
            {
                return IconNames[iconName];
            }
            else
            {
                return null;
            }

        }

    }
}
