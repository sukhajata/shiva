using Shiva.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisSearchResultsAttribute : GnosisAttribute
    {
        private SearchLinkRole linkRole;
        private string parameter;

        public enum SearchLinkRole
        {
            KEY,
            ENTITY,
            SYSTEM,
            NAME
        }

        [GnosisProperty]
        public string Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        [GnosisProperty]
        public string LinkRole
        {
            get
            {
                return Enum.GetName(typeof(SearchLinkRole), linkRole);
            }
            set
            {
                try
                {
                    linkRole = (SearchLinkRole)Enum.Parse(typeof(SearchLinkRole), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }

        }

        public SearchLinkRole _LinkRole
        {
            get { return linkRole; }
            set { linkRole = value; }
        }

        
    }
}
