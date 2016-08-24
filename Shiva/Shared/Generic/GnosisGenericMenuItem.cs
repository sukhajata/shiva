

using Shiva.Shared.Data;
using Shiva.Shared.Events;
using System;
using System.Collections.Generic;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisGenericMenuItem : GnosisVisibleControl
    {
        private List<GnosisEventHandler> eventHandlers;
        private List<GnosisGenericMenuItem> genericMenuItems;
        private List<GnosisGenericToggleMenuItem> genericToggleMenuItems;
        private List<GnosisGenericMenuGroup> genericMenuGroups;



        private bool allHidden;

        private string shortcut;

        private MenuTagEnum menuTag;

        private bool disabled;

        private string icon;


        public enum MenuTagEnum
        {
            BACKWARD,
            FORWARD,
            CUT,
            COPY,
            PASTE,
            UNDO,
            REDO,
            EDIT,
            DELETE,
            INSERT,
            SAVE,
            REMOVE,
            REFRESH,
            CANCEL,
            SEARCH,
            RESET,
            PRINT,
            NAVIGATOR,
            NEW,
            TOOLTIP,
            PROTECTIONSTATUS,
            PROPERTIES
        };


        [GnosisCollection]
        public List<GnosisEventHandler> EventHandlers
        {
            get
            {
                return this.eventHandlers;
            }
            set
            {
                this.eventHandlers = value;
            }
        }

        [GnosisCollection]
        public List<GnosisGenericMenuItem> GnosisGenericMenuItems
        {
            get
            {
                return this.genericMenuItems;
            }
            set
            {
                this.genericMenuItems = value;
            }
        }


       
        [GnosisProperty]
        public bool AllHidden
        {
            get { return allHidden; }
            set { allHidden = value; }
        }


        [GnosisProperty]
        public string MenuTag
        {
            get
            {
                return Enum.GetName(typeof(MenuTagEnum), menuTag);
            }
            set
            {
                try
                {
                    menuTag = (MenuTagEnum)Enum.Parse(typeof(MenuTagEnum), value.ToUpper());
                }
                catch(Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public MenuTagEnum _MenuTag
        {
            get { return menuTag; }
            set { menuTag = value; }
        }

        [GnosisProperty]
        public bool Disabled
        {
            get
            {
                return this.disabled;
            }
            set
            {
                this.disabled = value;
                OnPropertyChanged("Disabled");
            }
        }

        [GnosisProperty]
        public string GnosisIcon
        {
            get { return icon; }
            set { icon = value; }
        }

        [GnosisProperty]
        public string Shortcut
        {
            get { return shortcut; }
            set { shortcut = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisEventHandler)
            {
                if (eventHandlers == null)
                {
                    eventHandlers = new List<GnosisEventHandler>();
                }
                eventHandlers.Add((GnosisEventHandler)child);
            }
            else if (child is GnosisGenericMenuGroup)
            {
                if (genericMenuGroups == null)
                {
                    genericMenuGroups = new List<GnosisGenericMenuGroup>();
                }
                genericMenuGroups.Add((GnosisGenericMenuGroup)child);
            }
            else if (child is GnosisGenericMenuItem)
            {
                if (genericMenuItems == null)
                {
                    genericMenuItems = new List<GnosisGenericMenuItem>();
                }
                genericMenuItems.Add((GnosisGenericMenuItem)child);

            }
            else if (child is GnosisGenericToggleMenuItem)
            {
                if (genericToggleMenuItems == null)
                {
                    genericToggleMenuItems = new List<GnosisGenericToggleMenuItem>();
                }
                genericToggleMenuItems.Add((GnosisGenericToggleMenuItem)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown type added to GnosisGenericMenuItem: " + child.GetType().ToString(),
                    "GnosisGenericMenutItem.AddChild()");
            }
        }

    }
}