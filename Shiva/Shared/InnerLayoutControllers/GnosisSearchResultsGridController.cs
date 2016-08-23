using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.DataControllers;
using ShivaShared3.OuterLayoutControllers;
using System.Xml.Linq;
using ShivaShared3.GridColumnControllers;
using System.Linq;
using System.Diagnostics;

namespace ShivaShared3.InnerLayoutControllers
{
    public class GnosisSearchResultsGridController : GnosisGridController
    {
        //public int DocumentSystemID
        //{
        //    get { return ((GnosisSearchResultsGrid)ControlImplementation).DocumentSystemID; }
        //}

        //public int DocumentEntityID
        //{
        //    get { return ((GnosisSearchResultsGrid)ControlImplementation).DocumentEntityID; }
        //}

        //public string DocumentAction
        //{
        //    get { return ((GnosisSearchResultsGrid)ControlImplementation).DocumentAction; }
        //}

        
        public GnosisSearchResultsGridController(
            GnosisSearchResultsGrid resultsGrid,
          //  IGnosisGridImplementation gridImp,
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController parent)
            :base (resultsGrid, instanceController, parent)
        {

        }

        public override void Setup()
        {
            columns = new List<GnosisGridColumnController>();

            GnosisSearchResultsGrid grid = ((GnosisSearchResultsGrid)ControlImplementation);

            if (grid.TextResults != null)
            {
                foreach (GnosisTextResults textResults in grid.TextResults.Where(t => !t.Hidden))
                {
                    GnosisResultsColumnController columnController = new GnosisResultsColumnController(textResults, InstanceController, this);
                    columns.Add(columnController);
                }
            }

            if (grid.NumberResults != null)
            {
                foreach (GnosisNumberResults numberResults in grid.NumberResults.Where(n => !n.Hidden))
                {
                    GnosisResultsColumnController numberController = new GnosisResultsColumnController(numberResults, InstanceController, this);
                    columns.Add(numberController);
                }
            }

            if (grid.DateResults != null)
            {
                foreach (GnosisDateResults dateResults in grid.DateResults.Where(d => !d.Hidden))
                {
                    GnosisResultsColumnController columnController = new GnosisResultsColumnController(dateResults, InstanceController, this);
                    columns.Add(columnController);
                }
            }

            if (grid.DateTimeResults != null)
            {
                foreach (GnosisDateTimeResults datetime in grid.DateTimeResults.Where(d => !d.Hidden))
                {
                    GnosisResultsColumnController columnController = new GnosisResultsColumnController(datetime, InstanceController, this);
                    columns.Add(columnController);
                }
            }

            if (grid.CheckResults != null)
            {
                foreach (GnosisCheckResults checkResults in grid.CheckResults.Where(c => !c.Hidden))
                {
                    GnosisResultsColumnController checkController = new GnosisResultsColumnController(checkResults, InstanceController, this);
                    columns.Add(checkController);
                }
            }

            totalMinWidth = columns.Sum(c => c.MinFieldWidth);


            CreateFields();

        }

        public override void CreateFields()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            rowControllers = new Dictionary<int, GnosisGridRowController>();
            string datasetName = ((GnosisGrid)ControlImplementation).Dataset;
            int rowNo = 0;

            if (InstanceController != null)
            {//if1
                IEnumerable<XElement> dataRows = InstanceController.GetDataRows(datasetName);

                if (dataRows == null || dataRows.Count() == 0)
                {//if2
                    return;
                }//if2

                fieldsCreated = true;
                var orderedColumns = columns.OrderBy(c => c.Order);

                foreach (var row in dataRows)
                {//foreach2
                    GnosisResultsRowController rowController = new GnosisResultsRowController(this, rowNo);
                    rowControllers.Add(rowNo, rowController);

                    foreach (GnosisGridColumnController column in orderedColumns)
                    {//foreach3
                        IGnosisResultsFieldImplementation resultsField = (IGnosisResultsFieldImplementation)column.GetFieldClone();

                        string attributeName = InstanceController.GetTargetAttributeName(column.Dataset, column.DatasetItem);
                        string attribute = "";
                        if (row.Attribute(attributeName) != null)
                        {
                            attribute = row.Attribute(attributeName).Value;
                        }

                        if (column.ColumnModel is GnosisTextResults)//(column.ColumnType.Equals(ControlTypeMapping.GetControlTypeName(typeof(GnosisTextColumn))))
                        {//if4
                           
                            if (!attribute.Equals(""))
                            {
                                ((IGnosisResultsTextFieldImplementation)resultsField).SetText(attribute);
                            }

                        }//if4
                        else if (column.ColumnModel is GnosisCheckResults)
                        {
                            if (!attribute.Equals(""))
                            {
                                bool val = false;
                                if (attribute.Equals("1"))
                                {
                                    val = true;
                                }
                                ((IGnosisResultsCheckFieldImplementation)resultsField).GnosisChecked = val;
                            }

                        }

                        rowController.AddCell(resultsField);

                    }//foreach3

                    rowNo++;
                }//foreach2

            }//if1

            stopWatch.Stop();
            Debug.WriteLine("GnosisSearchResultsGridController, CreateFields, Milliseconds elapsed: {0}", stopWatch.ElapsedMilliseconds);         stopWatch.Stop();
            

        }

        //internal override void OnRowSelected(int rowNum)
        //{
        //    base.OnRowSelected(rowNum);


        //    //load document
        //    int searchSystemID = ((GnosisSearchResultsGrid)ControlImplementation).DocumentSystemID;
        //    int searchEntityID = ((GnosisSearchResultsGrid)ControlImplementation).DocumentEntityID;

        //    Dictionary<int, string> keys = new Dictionary<int, string>();

        //    foreach (GnosisSearchResultsAttribute attribute in ((GnosisSearchResultsGrid)ControlImplementation).SearchResultsAttributes)
        //    {
        //        string value = InstanceController.GetDataString(attribute.Dataset, attribute.DatasetItem, rowNum);
        //        switch (attribute._LinkRole)
        //        {
        //            case GnosisSearchResultsAttribute.SearchLinkRole.SYSTEM:
        //                searchSystemID = Convert.ToInt32(value);
        //                break;
        //            case GnosisSearchResultsAttribute.SearchLinkRole.ENTITY:
        //                searchEntityID = Convert.ToInt32(value);
        //                break;
        //            case GnosisSearchResultsAttribute.SearchLinkRole.KEY:
        //                keys.Add(attribute.LinkKeyOrder, value);
        //                break;
        //         }
        //    }

        //    //GnosisEntityController target = GlobalData.Singleton.SystemController.GetEntityController(searchEntityID, searchSystemID);
        //    //GnosisInstance instance = GlobalData.Singleton.SystemController.GetInstance(searchEntityID, searchSystemID, "Open", keys);
        //    //target.Instance = instance;

        //    GlobalData.Singleton.SystemController.LoadDocument(searchEntityID, searchSystemID, "Open", keys);
        //}

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           //stop base class from enabling fields
        }

    }
}
