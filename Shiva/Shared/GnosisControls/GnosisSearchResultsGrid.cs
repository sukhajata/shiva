using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using ShivaShared3.Data;

namespace GnosisControls
{
    public class GnosisSearchResultsGrid : GnosisGrid
    {
        private int documentSystemID;

        private int documentEntityID;

        private string documentAction;

        private List<GnosisSearchResultsAttribute> searchResultsAttributes;

        private List<GnosisTextResults> textResults;

        private List<GnosisNumberResults> numberResults;

        private List<GnosisDateResults> dateResults;

        private List<GnosisDateTimeResults> dateTimeResults;

        private List<GnosisCheckResults> checkResults;

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

        [GnosisProperty]
        public string DocumentAction
        {
            get { return documentAction; }
            set { documentAction = value; }
        }

        [GnosisCollection]
        public List<GnosisSearchResultsAttribute> SearchResultsAttributes
        {
            get { return searchResultsAttributes; }
            set { searchResultsAttributes = value; }
        }

        [GnosisCollection]
        public List<GnosisTextResults> TextResults
        {
            get { return textResults; }
            set { textResults = value; }
        }

        [GnosisCollection]
        public List<GnosisNumberResults> NumberResults
        {
            get { return numberResults; }
            set { numberResults = value; }
        }

        [GnosisCollection]
        public List<GnosisDateResults> DateResults
        {
            get { return dateResults; }
            set { dateResults = value; }
        }

        [GnosisCollection]
        public List<GnosisDateTimeResults> DateTimeResults
        {
            get { return dateTimeResults; }
            set { dateTimeResults = value; }
        }

        [GnosisCollection]
        public List<GnosisCheckResults> CheckResults
        {
            get { return checkResults; }
            set { checkResults = value; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisNumberResults)
            {
                if (numberResults == null)
                {
                    numberResults = new List<GnosisNumberResults>();
                }
                numberResults.Add((GnosisNumberResults)child);
            }
            else if (child is GnosisDateResults)
            {
                if (dateResults == null)
                {
                    dateResults = new List<GnosisDateResults>();
                }
                dateResults.Add((GnosisDateResults)child);
            }
            else if (child is GnosisDateTimeResults)
            {
                if (dateTimeResults == null)
                {
                    dateTimeResults = new List<GnosisDateTimeResults>();
                }
                dateTimeResults.Add((GnosisDateTimeResults)child);
            }
            else if (child is GnosisTextResults)
            {
                if (textResults == null)
                {
                    textResults = new List<GnosisTextResults>();
                }
                textResults.Add((GnosisTextResults)child);
            }
            else if (child is GnosisCheckResults)
            {
                if (checkResults == null)
                {
                    checkResults = new List<GnosisCheckResults>();
                }
                checkResults.Add((GnosisCheckResults)child);
            }
            else if (child is GnosisSearchResultsAttribute)
            {
                if (searchResultsAttributes == null)
                {
                    searchResultsAttributes = new List<GnosisSearchResultsAttribute>();
                }
                searchResultsAttributes.Add((GnosisSearchResultsAttribute)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleUnknowChildAddedError("GnosisSearchResultsGrid", child.GetType().Name);
            }
           
        }
    }
}
