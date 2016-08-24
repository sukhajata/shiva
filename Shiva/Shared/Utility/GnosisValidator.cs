using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shiva.Shared.Utility
{
    public class GnosisValidator
    {
        Hashtable completionRegex = new Hashtable();
        Hashtable errorMessage = new Hashtable();
        
        public GnosisValidator()
        {
            //completionRegex["Percent"] = @"(?!^0*$)(?!^0*\.0*$)^\d{1,2}(\.\d{1,2})?$";
            completionRegex["Percent"] = @"^[0-9]?[0-9]$";
            errorMessage["Percent"] = "Please enter a number between 1 and 99";

            completionRegex["Width"] = @"^([0-9][0-9]{1,3}|2000)$";
            errorMessage["Width"] = "Please enter a number between 1 and 2000";
        }

        public bool ValidateSplitUnits(int input)
        {
            if (input > 0 && input < 2000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateSplitPercentage(int input)
        {
            if (input > 0 && input < 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateText(string type, string input)
        {
            Regex regex = new Regex((string)completionRegex[type]);
            if (regex.IsMatch(input))
            {
                return true;
            }

            return false;
        }
    }
}
