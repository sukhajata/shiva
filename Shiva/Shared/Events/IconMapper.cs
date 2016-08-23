using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Events
{
    public class IconMapper
    {
        private static Dictionary<string, string> IconNames = new Dictionary<string, string>
        {
          //  { "NewMenu", "new" },
            {"SearchMenu",  "search"},
          //  {"ProtectionStatusMenu", "pen" },
            {"PropertiesMenu", "properties" },
            {"BackwardMenu", "back" },
            {"ForwardMenu", "forward" },
            {"CutMenu", "cut" },
            {"CopyMenu", "copy" },
            {"UndoMenu", "undo" },
            {"RedoMenu", "redo" },
         //   {"EditMenu", "edit" },
            {"DeleteMenu", "delete" },
            {"SaveMenu", "save" },
            {"RefreshMenu" , "refresh"},
            {"CancelMenu", "cancel" },
            {"PrintMenu", "print" },
            {"PasteMenu", "paste" },
            {"ShowNavigatorMenu", "navigator" }
            
        };

        private static Dictionary<int, string> Icons = new Dictionary<int, string>
        {
            { 10, "forward" },
            {12, "back" },
            //{ 20, "up" },
            //{ 22, "down" },
            //{ 30, "home" },
            //{ 32, "favourites" },
            { 34, "navigator" },
            //{ 36, "left-gallery" },
            //{ 38, "right-gallery" },
            {40, "cut" },
            { 42, "copy" },
            {44, "paste" },
            {50, "undo" },
            {52, "redo" },
            // {60, "new" },
            // {64, "copy-new" },
            {64, "edit" },
            // {66, "open" },
            {68, "search" },
            //{69, "reset" },
            {70, "delete" },
            {72, "save" },
            //{73, "save-delete" },
            {74, "cancel" },
            {76, "refresh" },
            //{80, "check" },
            //{82, "help" },
            {85, "print" },
            //{90,  "create-split"},
            // {92, "split-horizontal" },
            // {93, "split-vertical" },
            // {96, "window" },
            //{100, "connect" },
            //{102, "disconnect" },
            {104, "properties" }
            //{110, "hide" },
            //{112, "view" },
            //{114, "close" },
            //{120, "protected" }
            //{122, "unprotected" },
            //{124, "locked" },
            //{204, "person" },
            //{206, "company" },
            //{208, "trust" },
            //{210, "employee" },
            //{212, "customer" },
            //{214, "supplier" },
            //{220, "address" },
            //{222, "mail" },
            //{230, "phone" },
            //{242, "meeting" },
            //{244, "task" },
            //{246, "calendar" },
            //{250, "email" },
            //{252, "reply" },
            //{254, "reply-all" },
            //{256, "forward-email"}
        };

        public static string GetIconName(string menuItemName)
        {

            if (IconNames.ContainsKey(menuItemName))
            {
                return IconNames[menuItemName];
            }
            else
            {
                return null;
            }

        }

        public static string GetIconFileName(int iconId)
        {
            if (Icons.ContainsKey(iconId))
            {
                return Icons[iconId];
            }

            return null;
        }

    }
}
