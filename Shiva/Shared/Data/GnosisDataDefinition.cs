using ShivaShared3.Data;
using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisDataDefinition : IGnosisObject
    {
        private List<GnosisDataType> types;
        private List<GnosisDataCache> caches;
        private List<GnosisDataSystemReference> dataSystemReferences;
        private List<GnosisDataKey> dataKeys;

        [GnosisCollection]
        public List<GnosisDataType> DataTypes
        {
            get { return types; }
            set { types = value; }
        }

        [GnosisCollection]
        public List<GnosisDataCache> DataCaches
        {
            get { return caches; }
            set { caches = value; }
        }

        public void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisDataType)
            {
                if (types == null)
                {
                    types = new List<GnosisDataType>();
                }
                types.Add((GnosisDataType)child);
            }
            else if (child is GnosisDataCache)
            {
                if (caches == null)
                {
                    caches = new List<GnosisDataCache>();
                }
                caches.Add((GnosisDataCache)child);
            }
            else if (child is GnosisDataSystemReference)
            {
                if (dataSystemReferences == null)
                {
                    dataSystemReferences = new List<GnosisDataSystemReference>();
                }
            }
            else if (child is GnosisDataKey)
            {
                if (dataKeys == null)
                {
                    dataKeys = new List<GnosisDataKey>();
                }
                dataKeys.Add((GnosisDataKey)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown type added to GnosisDataDefinition: " + child.GetType().ToString(), "GnosisDataDefinition.AddChild()");
            }
        }
    }
}
