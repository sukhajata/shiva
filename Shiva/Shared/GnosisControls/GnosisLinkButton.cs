using ShivaShared3.Data;
using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public partial class GnosisLinkButton : GnosisButton
    {
        private int searchSystemID;

        private int searchEntityID;

        private LinkSearchActionType searchAction;

        private LinkSearchActionType autoSearchAction;

        private List<GnosisSearchParameter> searchParameters;

        

        public enum LinkSearchActionType
        {
            Open,
            New, 
            Reset,
            Search,
            SaveNew,
            SaveDelete
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
            get { return Enum.GetName(typeof(LinkSearchActionType), searchAction); }
            set
            {
                try
                {
                    searchAction = (LinkSearchActionType)Enum.Parse(typeof(LinkSearchActionType), value);
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
            get { return Enum.GetName(typeof(LinkSearchActionType), autoSearchAction); }
            set
            {
                try
                {
                    autoSearchAction = (LinkSearchActionType)Enum.Parse(typeof(LinkSearchActionType), value);
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        
        [GnosisCollection]
        public List<GnosisSearchParameter> SearchParameters
        {
            get { return searchParameters; }
            set { searchParameters = value; }
        }


    }
}
