
using Shiva.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisLinkButtonItem : GnosisVisibleControl
    {
        private bool disabled;

        private int searchSystemID;

        private int searchEntityID;

        private LinkSearchAction searchAction;

        private LinkSearchAction autoSearchAction;

        public enum LinkSearchAction
        {
            OPEN,
            NEW,
            RESET,
            SEARCH,
            SAVENEW,
            SAVEDELETE
        }

        [GnosisProperty]
        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }


        [GnosisProperty]
        public int SearchSystemID
        {
            get { return searchSystemID; }
            set { searchSystemID = value; }
        }

        [GnosisProperty]
        public int SearchEntityID
        {
            get { return searchEntityID; }
            set { searchEntityID = value; }
        }

        [GnosisProperty]
        public string SearchAction
        {
            get { return Enum.GetName(typeof(LinkSearchAction), searchAction); }
            set
            {
                try
                {
                    searchAction = (LinkSearchAction)Enum.Parse(typeof(LinkSearchAction), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }


        
        [GnosisProperty]
        public string AutoSearchAction
        {
            get { return Enum.GetName(typeof(LinkSearchAction), autoSearchAction); }
            set
            {
                try
                {
                    autoSearchAction = (LinkSearchAction)Enum.Parse(typeof(LinkSearchAction), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        

    }
}
