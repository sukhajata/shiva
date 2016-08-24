using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Data;

namespace GnosisControls
{
    public class GnosisNewMenuItem : GnosisControl
    {
        private List<GnosisDocumentParameter> documentParameters;

        private string icon;
        private string shortcut;
        private string caption;
        private string tooltip;
        private int documentSystemID;
        private int documentEntityID;

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

        [GnosisProperty]
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        [GnosisProperty]
        public string Tooltip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }

        [GnosisProperty]
        public int DocumentSystemID
        {
            get { return documentSystemID; }
            set { documentSystemID = value; }
        }

        [GnosisProperty]
        public int DocumentEntityID
        {
            get { return documentEntityID; }
            set { documentEntityID = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisDocumentParameter)
            {
                if (documentParameters == null)
                {
                    documentParameters = new List<GnosisDocumentParameter>();
                }
                documentParameters.Add((GnosisDocumentParameter)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisNewMenuItem", child.GetType().Name);
            }
        }


    }
}
