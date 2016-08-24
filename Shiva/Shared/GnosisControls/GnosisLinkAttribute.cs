using Shiva.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisLinkAttribute : GnosisAttribute
    {
        private LinkRoleType linkRole;

        public enum LinkRoleType
        {
            KEY,
            ENTITY,
            SYSTEM,
            NAME
        };

        [GnosisProperty]
        public string LinkRole
        {
            get { return Enum.GetName(typeof(LinkRoleType), linkRole); }
            set
            {
                try
                {
                    linkRole = (LinkRoleType)Enum.Parse(typeof(LinkRoleType), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }


        
    }
}
