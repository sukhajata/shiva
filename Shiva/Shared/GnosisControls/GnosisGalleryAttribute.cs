using ShivaShared3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisGalleryAttribute : GnosisAttribute
    {
        private GalleryAttributeRole linkRole;

        public enum GalleryAttributeRole
        {
            KEY,
            ENTITY,
            SYSTEM
        };

        [GnosisProperty]
        public string LinkRole
        {
            get
            {
                return Enum.GetName(typeof(GalleryAttributeRole), linkRole);
            }
            set
            {
                try
                {
                    linkRole = (GalleryAttributeRole)Enum.Parse(typeof(GalleryAttributeRole), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GalleryAttributeRole _LinkRole
        {
            get { return linkRole; }
            set { linkRole = value; }
        }

    }
}
