using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShivaShared3.Utility
{

    public class PropertyNameConverter
    {
        //Some property names used by Gnosis XML clash with windows property names and so are converted to different names.

        private static Dictionary<string, string> PropertyNameConversions = new Dictionary<string, string>
        {
            {"BorderThickness", "GnosisBorderThickness" },
            {"Checked", "GnosisChecked" },
            {"Expanded", "GnosisExpanded" },
            {"GroupName", "GnosisGroupName" },
            {"Icon", "GnosisIcon" },
            {"Name", "GnosisName" },
            {"Orientation", "GnosisOrientation" },
            {"Selected", "Active" },
            {"Object", "GnosisObject" }
        };

        public static string GetShivaPropertyName(string xmlPropertyName)
        {

            if (PropertyNameConversions.ContainsKey(xmlPropertyName))
            {
                return PropertyNameConversions[xmlPropertyName];
            }
            else
            {
                return xmlPropertyName;
            }
        }

        public static string GetXMLPropertyName(string shivaPropertyName)
        {
            if (PropertyNameConversions.ContainsValue(shivaPropertyName))
            {
                return PropertyNameConversions.FirstOrDefault(p => p.Value.Equals(shivaPropertyName)).Key;
            }
            else
            {
                return shivaPropertyName;
            }
        }

    }
}
