using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Shiva.Shared.Interfaces;
using Shiva.Shared.DataControllers;
using GnosisControls;
using Shiva.Shared.Data;
using System.Collections;
using System.Xml.Linq;
using Shiva.Shared.Utility;
using Shiva.Shared.ContentControllers;
using System.Windows;
using Shiva.Shared.OuterLayoutControllers;
using System.Diagnostics;
using Shiva.Shared.GridColumnControllers;
using System.ComponentModel;

namespace Shiva.Shared.InnerLayoutControllers
{
    public class GnosisGridController : GnosisInnerLayoutController
    {
        protected List<GnosisGridColumnController> columns;
        protected Dictionary<int, GnosisGridRowController> rowControllers;
        private Dictionary<int, List<GnosisGridColumnController>> headerRows;
        private GnosisOuterLayoutController parentController;
        private double rowHeight;
        private bool alternateRowColour;
        protected double minWidthForSingleRow; //the sum of MinWidth of all columns
        private double newWidth;
        private double oldWidth;
        private BackgroundWorker widthChangedBackgroundWorker;
        private double minWidth;
        private double maxWidth;
        private bool layoutNeeded;
        private int numFieldCols;
        private int numGridCols;
        private bool isColumnar;
        private double characterWidth;
        //private GnosisGridTextFieldController indentController; //dummy controller to get styles of textbox for indenting rows
        protected bool fieldsCreated;

        internal void RowSelected(GnosisGridRowController selectedRow)
        {
            foreach (GnosisGridRowController rowController in rowControllers.Values)
            {
                if (rowController != selectedRow)
                {
                    rowController.UnSelectRow();
                }
            }
        }

        //public int MaxWrapRows
        //{
        //    get { return ((GnosisGrid)ControlImplementation).MaxWrapRows; }
        //    set
        //    {
        //        ((GnosisGrid)ControlImplementation).MaxWrapRows = value;
        //        OnPropertyChanged("MaxWrapLines");
        //    }
        //}

        //public int MinDisplayRows
        //{
        //    get { return ((GnosisGrid)ControlImplementation).MinDisplayRows; }
        //    set
        //    {
        //        ((GnosisGrid)ControlImplementation).MinDisplayRows = value;
        //        OnPropertyChanged("MinDisplayRows");
        //    }
        //}

        //public int MaxDisplayRows
        //{
        //    get { return ((GnosisGrid)ControlImplementation).MaxDisplayRows; }
        //}

        //public int MaxLines
        //{
        //    get { return ((GnosisGrid)ControlImplementation).MaxLines; }
        //}

        public bool Loaded
        {
            set { OnPropertyChanged("Loaded"); }
        }

        public bool LayoutCompleted
        {
            set { OnPropertyChanged("LayoutCompleted"); }
        }

        public GnosisGridController(
            GnosisGrid grid, 
           // IGnosisGridImplementation gridImplementation,
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController _parentController)
            : base(grid, instanceController, _parentController)
        {
            parentController = _parentController;
            numGridCols = 512;

            //indent 
            //GnosisTextColumn txtField = ModelCreator.CreateGnosisGridTextField(1);
            //GnosisGridColumnController columnController = new GnosisGridColumnController(txtField, InstanceController, this);
            // IGnosisGridTextFieldImplementation txtImp = GlobalData.Singleton.ImplementationCreator.GetGnosisGridTextFieldImplementation();


            //determine layout parameters. Use dummy text field to apply styles
            GnosisGridTextField txtField = new GnosisGridTextField();
            GlobalData.Singleton.StyleHelper.ApplyFontStyle(txtField, EntityController.GetNormalStyle());

            string font = EntityController.GetNormalStyle().Font;
            int fontSize = EntityController.GetNormalStyle().FontSize;
            rowHeight = GlobalData.Singleton.StyleHelper.GetFieldHeight(txtField, font, fontSize);
            characterWidth = GlobalData.Singleton.StyleHelper.GetCharacterWidth(font, fontSize);

            //background thread for calculating if layout change is necessary
            //widthChangedBackgroundWorker = new BackgroundWorker();
            //widthChangedBackgroundWorker.DoWork += WidthChanged_DoWorkBackground;
            //widthChangedBackgroundWorker.RunWorkerCompleted += WidthChanged_WorkCompleted;

            //gridImplementation.SetWidthChangedHandler(new Action<double>(WidthChanged));

          //  gridImplementation.SetLocked((bool)((GnosisGrid)ControlImplementation).Locked);

            //gridImplementation.SetMultipleRowSelectionEnabled((bool)((GnosisGrid)ControlImplementation).MultipleRowSelection);

            //gridImplementation.SetCaptionRelativePosition(((GnosisGrid)ControlImplementation).CaptionRelativePosition);

            grid.SetLoadedHandler(new Action(GridLoaded));

            grid.SetIsVisibleChangedHandler(new Action<bool>(IsVisibleChanged));

        }


        public virtual void Setup()
        {
            columns = new List<GnosisGridColumnController>();

            GnosisGrid grid = ((GnosisGrid)ControlImplementation);

            foreach (GnosisTextColumn textColumn in grid.TextColumns.Where(t => !t.Hidden))
            {
                GnosisGridColumnController columnController = new GnosisGridColumnController(textColumn, InstanceController, this);
                columns.Add(columnController);
            }

            foreach (GnosisNumberColumn numberColumn in grid.NumberColumns.Where(n => !n.Hidden))
            {
                GnosisGridColumnController numberController = new GnosisGridColumnController(numberColumn, InstanceController, this);
            }

            foreach (GnosisComboColumn combo in grid.ComboColumns.Where(c => !c.Hidden))
            {
                GnosisGridColumnController columnController = new GnosisGridColumnController(combo, InstanceController, this);
                columns.Add(columnController);
            }

            foreach (GnosisDateColumn date in grid.DateColumns.Where(d => !d.Hidden))
            {
                GnosisGridColumnController columnController = new GnosisGridColumnController(date, InstanceController, this);
                columns.Add(columnController);
            }

            foreach (GnosisDateTimeColumn datetime in grid.DateTimeColumns.Where(d => !d.Hidden))
            {
                GnosisGridColumnController columnController = new GnosisGridColumnController(datetime, InstanceController, this);
                columns.Add(columnController);
            }

            foreach (GnosisCheckColumn check in grid.CheckColumns.Where(c => !c.Hidden))
            {
                GnosisGridColumnController columnController = new GnosisGridColumnController(check, InstanceController, this);
                columns.Add(columnController);
            }

            minWidthForSingleRow = columns.Sum(c => c.CharacterWidth * c.MinDisplayChars + (2 * c.HorizontalPadding));
           

            CreateFields();

        }


        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                foreach (GnosisGridRowController row in rowControllers.Values)
                {
                    foreach (IGnosisGridFieldImplementation gridField in row.Fields)
                    {
                        gridField.Locked = !this.Editable;
                    }
                }
            }
        }

        public virtual void CreateFields()
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
                    GnosisGridRowController rowController = new GnosisGridRowController(this, rowNo);
                    rowControllers.Add(rowNo, rowController);
                    
                    foreach (GnosisGridColumnController columnController in orderedColumns)
                    {//foreach3
                        IGnosisGridFieldImplementation gridField = columnController.GetFieldClone();

                        string attributeName = InstanceController.GetTargetAttributeName(columnController.Dataset, columnController.DatasetItem);
                        string attribute = "";
                        if (row.Attribute(attributeName) != null)
                        {
                            attribute = row.Attribute(attributeName).Value;
                        }

                        if (columnController.ColumnModel is GnosisTextColumn)//(column.ColumnType.Equals(ControlTypeMapping.GetControlTypeName(typeof(GnosisTextColumn))))
                        {//if4

                            if (!attribute.Equals(""))
                            {
                                ((GnosisGridTextField)gridField).SetText(attribute);
                            }

                        }//if4
                        else if (columnController.ColumnModel is GnosisCheckColumn)
                        {

                            if (!attribute.Equals(""))
                            {
                                bool val = false;
                                if (attribute.Equals("1"))
                                {
                                    val = true;
                                }
                                
                                ((GnosisGridCheckField)gridField).GnosisChecked = val;

                            }
                        }
                        else if (columnController.ColumnModel is GnosisDateColumn)
                        {
                            if (!attribute.Equals(""))
                            {
                                DateTime date = Convert.ToDateTime(attribute);
                                ((IGnosisGridDateFieldImplementation)gridField).SetDate(date);
                            } 
                        }
                        else if (columnController.ColumnModel is GnosisDateTimeColumn)
                        {
                            if (!attribute.Equals(""))
                            {
                                DateTime dateTime = Convert.ToDateTime(attribute);
                                ((IGnosisGridDateTimeFieldImplementation)gridField).SetDateTime(dateTime);
                            }
                        }

                        rowController.AddCell(gridField);


                    }//foreach3

                    rowNo++;
                }//foreach2

            }//if1

            stopWatch.Stop();
            Debug.WriteLine("GnosisGridController, CreateFields, Milliseconds elapsed: {0}", stopWatch.ElapsedMilliseconds);
        }

        public void IsVisibleChanged(bool visible)
        {
            if (visible && ((IGnosisGridImplementation)ControlImplementation).GridIsLoaded)
            {
                LayoutRows();
            }
        }

        //internal virtual void OnRowSelected(int rowNum)
        //{
        //    //styles
        //    foreach (KeyValuePair<int, GnosisGridRowController> rowController in rowControllers.Where(r => r.Key != rowNum))
        //    {
        //        (rowController.Value).RowSelected = false;
        //    }
        //}

        public void GridLoaded()
        {
            Loaded = true;

            LayoutRows();
        }

        public void LayoutRows()
        {
            double totalWidthAvailable = ((IGnosisGridImplementation)ControlImplementation).GetAvailableWidth();
            AssignFieldDimensions(totalWidthAvailable);

        }

        public void AssignFieldDimensions(double totalWidthAvailable)
        {
            if (totalWidthAvailable <= 0)
            {
                return;
            }

            if (columns.Count() == 0)
            {
                return;
            }

            if (!fieldsCreated)
            {
                CreateFields();
            }

            if (!fieldsCreated)
            {
                return;
            }



            double widthGridCol = totalWidthAvailable / numGridCols;
            AssignColSpans(totalWidthAvailable, widthGridCol, false);

            //LayoutRows(totalWidthAvailable);

            // headerRows = new Dictionary<int, List<GnosisGridColumnController>>();

            //Calculate ColSpans
            //ColSpans are assigned to columns but are applicable to headers and fields in that column
            //First see if all fields can fit in space available
            //int rowCount;

            //if (minWidthForSingleRow <= totalWidthAvailable)
            //{
            //    //all fields will fit on one row with a single line
            //    AssignColSpans(totalWidthAvailable, widthGridCol, false);
            //    rowCount = 1;
            //    headerRows.Add(0, columns);
            //}
            //else
            //{//1
            //    //all fields will not fit on one row with a single line
            //    //they may fit on a single row with multiple lines
            //    if (((GnosisGrid)ControlImplementation).MaxLines > 1)
            //    {//2



            //       // int numLines = 1;
            //        foreach (IGnosisGridFieldImplementation fieldImp in rowControllers[0].Fields)
            //        {//5
            //            GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
            //            double fieldWidth = fieldImp.GetWidth();
            //            int charsPerLine = (int)Math.Floor((fieldWidth - 2 * fieldImp.HorizontalPadding) / columnController.CharacterWidth);
            //            int numChars = 0;

            //            if (fieldImp is IGnosisResultsTextFieldImplementation)
            //            {//6
            //                numChars = charsPerLine * ((IGnosisResultsTextFieldImplementation)fieldImp).NumLines;
            //            }//6
            //            else if (fieldImp is IGnosisGridTextFieldImplementation)
            //            {//6
            //                numChars = charsPerLine * ((IGnosisGridTextFieldImplementation)fieldImp).NumLines;
            //            }//6

            //            if (numChars < columnController.MinDisplayChars)
            //            {//6
            //                int numLinesNeeded = (int)Math.Ceiling((double)columnController.MinDisplayChars / charsPerLine);

            //                if (numLinesNeeded > ((GnosisGrid)ControlImplementation).MaxLines)
            //                {//7
            //                    layoutNeeded = true;
            //                    break;
            //                }//7
            //                else
            //                {//7
            //                    if (numLinesNeeded > numLines)
            //                    {//8
            //                        numLines = numLinesNeeded;
            //                    }//8

            //                }//7
            //            }//6

            //        }//5

            //        //numLines has been calculated. Now apply it to all fields
            //        foreach (IGnosisGridFieldImplementation fieldImp in firstRow.Fields)
            //        {//6
            //            if (fieldImp is IGnosisResultsTextFieldImplementation)
            //            {//7
            //                GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
            //                if (numLines != ((IGnosisResultsTextFieldImplementation)fieldImp).NumLines)
            //                {//8
            //                    columnController.NumLines = numLines;

            //                    //Use Dispatcher to touch UI since this is run in a background thread
            //                    Application.Current.Dispatcher.Invoke((Action)(() =>
            //                    {//9
            //                        foreach (IGnosisGridFieldImplementation gridFieldImp in columnController.Fields)
            //                        {//10
            //                            if (numLines > 1)
            //                            {//11
            //                                ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetTextWrapping(true);
            //                            }//11
            //                            else
            //                            {//11
            //                                ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetTextWrapping(false);
            //                            }//11

            //                            double newHeight = columnController.TextHeight * numLines + columnController.VerticalPadding * 2;
            //                            ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetHeight(newHeight);
            //                            ((IGnosisResultsTextFieldImplementation)gridFieldImp).NumLines = numLines;
            //                        }//10
            //                    }));//9
            //                }//8
            //            }//7
            //            else if (fieldImp is IGnosisGridTextFieldImplementation)
            //            {//7
            //                GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
            //                if (numLines != ((IGnosisGridTextFieldImplementation)fieldImp).NumLines)
            //                {//8
            //                    columnController.NumLines = numLines;

            //                    //Use Dispatcher to touch UI since this is run in a background thread
            //                    Application.Current.Dispatcher.Invoke((Action)(() =>
            //                    {//9
            //                        foreach (IGnosisGridTextFieldImplementation gridFieldImp in columnController.Fields)
            //                        {//10
            //                            if (numLines > 1)
            //                            {//11
            //                                ((IGnosisGridTextFieldImplementation)gridFieldImp).SetTextWrapping(true);
            //                            }//11
            //                            else
            //                            {//11
            //                                ((IGnosisGridTextFieldImplementation)gridFieldImp).SetTextWrapping(false);
            //                            }//11

            //                            double newHeight = columnController.TextHeight * numLines + columnController.VerticalPadding * 2;
            //                            ((IGnosisGridTextFieldImplementation)gridFieldImp).SetHeight(newHeight);
            //                            ((IGnosisGridTextFieldImplementation)gridFieldImp).NumLines = numLines;
            //                        }//10
            //                    }));//9
            //                }//8
            //            }//7
            //        }//6
            //    }//2

            //    List<GnosisGridColumnController> assigned = new List<GnosisGridColumnController>();
            //    int currentOrder = 0;
            //    rowCount = 0;

            //    while (assigned.Count() < columns.Count())
            //    {//2
            //        double usedWidth = 0;
            //        List<GnosisGridColumnController> row = new List<GnosisGridColumnController>();
            //        var unassignedColumns = columns.Where(c => c.Order > currentOrder);
            //        foreach (GnosisGridColumnController column in unassignedColumns.OrderBy(c => c.Order))
            //        {//3
            //            if (column.MinFieldWidth > totalWidthAvailable)
            //            {//4
            //                GlobalData.Singleton.ErrorHandler.HandleError("MinFieldWidth greater than width available: " + column.DatasetItem + " - " + column.MinFieldWidth.ToString(), "GnosisGridController.LayoutRows");
            //               // column.MinFieldWidth = totalWidthAvailable;
            //            }//4

            //            if (usedWidth + column.MinFieldWidth <= totalWidthAvailable)
            //            {//4
            //                usedWidth = usedWidth + column.MinFieldWidth;
            //                row.Add(column);
            //                assigned.Add(column);
            //                currentOrder = column.Order;
            //            }//4
            //            else
            //            {//4
            //                break;
            //            }//4
            //        }//3

            //        if (rowCount == 0)
            //        {//3
            //            AssignColSpans(row, widthGridCol, false);
            //        }//3
            //        else
            //        {//3
            //            AssignColSpans(row, widthGridCol, true);
            //        }//3
            //        headerRows.Add(rowCount, row);

            //        rowCount++;

            //        if (rowCount > ((GnosisGrid)ControlImplementation).MaxWrapRows)
            //        {//3
            //            LoadColumnar(totalWidthAvailable);
            //            return;
            //        }//3
            //    }//2

            //}//1

            //All text fields will be assigned the same NumLines based on the minimum
            //required to meet the MinDisplayChars of all fields
            //int colNo = 0;
            //int numLines = 1;
            //foreach(var columnController in columns)
            //{//1
            //    int colSpan = columnController.ColSpan;
            //    bool placed = false;
            //    while (!placed)
            //    {//2
            //        if (colNo + colSpan <= numGridCols)
            //        {//3

            //            colNo = colNo + colSpan;
            //            placed = true;

            //            //check MinDisplayChars is met
            //            if (columnController.ColumnModel is GnosisTextColumn || columnController.ColumnModel is GnosisTextResults)
            //            {//4
            //                int charsPerLine = (int)Math.Floor((colSpan * widthGridCol) / columnController.CharacterWidth);
            //                int numLinesNeeded = (int)Math.Ceiling((double)columnController.MinDisplayChars / charsPerLine);
            //                //((GnosisGridTextFieldController)columnController.FieldController).NumLines = numLinesNeeded;
            //                if (numLinesNeeded > numLines)
            //                {
            //                    numLines = numLinesNeeded;
            //                }

            //            }//4
            //            //else if (columnController.ColumnModel is GnosisTextResults)
            //            //{//4
            //            //    int charsPerLine = (int)Math.Floor((colSpan * widthGridCol) / columnController.CharacterWidth);
            //            //    int numLinesNeeded = (int)Math.Ceiling((double)columnController.MinDisplayChars / charsPerLine);
            //            //    //((GnosisTextResultsFieldController)columnController.FieldController).NumLines = numLinesNeeded;
            //            //    if (numLinesNeeded > numLines)
            //            //    {
            //            //        numLines = numLinesNeeded;
            //            //    }

            //            //}//4

            //        }//3
            //        else
            //        {//3
            //            colNo = 0;
            //        }//3
            //    }//2
            //}//1

            //foreach (var columnController in columns)
            //{
            //    if (columnController.ColumnModel is GnosisTextColumn || columnController.ColumnModel is GnosisTextResults)
            //    {
            //        columnController.NumLines = numLines;
            //    }

            //}

            //  LayoutRows(totalWidthAvailable);
        }

        public void LayoutRows(double totalWidthAvailable)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //can not get to this point unless layout is not columnar
            isColumnar = false;
            ((IGnosisGridImplementation)ControlImplementation).SetColumnarFormat(isColumnar);

            ((IGnosisGridImplementation)ControlImplementation).Clear();
            ((IGnosisGridImplementation)ControlImplementation).NumColumns = numGridCols;

            double widthGridCol = totalWidthAvailable / numGridCols;

            //These values are calculated and used in WidthChanged() to determine whether a new layout should be applied
            minWidth = 0; //reset
            maxWidth = 0;
            oldWidth = totalWidthAvailable;

            //((IGnosisGridImplementation)ControlImplementation).SetHeight(StyleManager.GetFieldHeight() * rowCount * MinDisplayRows);

            //Apply column headers. 
            int colNo = 0;
            int rowNo = 0;
            numFieldCols = 0;

            //first row
            ((IGnosisGridImplementation)ControlImplementation).AddHeaderRow();

            foreach (GnosisGridColumnController columnController in columns)
            {
                bool placed = false;
                while (!placed)
                {
                    if (colNo + columnController.ColSpan <= numGridCols)
                    {
                        if (colNo == 0 && rowNo > 0)
                        {
                            //apply indent
                            IGnosisGridHeaderFieldImplementation indent = GnosisControlCreator.CreateGnosisGridHeaderField();//GlobalData.Singleton.ImplementationCreator.GetGnosisGridHeaderImplementation();
                            //StyleController indentStyle = new StyleController(InstanceController.EntityController.GetCaptionStyle(), this, indent);
                            //indent.SetCaption(">>");
                            indent.RemoveBorder();
                            int indentSpan = GetColSpan(1, characterWidth, widthGridCol); //use textbox controller to get colspan so it aligns with data
                            ((IGnosisGridImplementation)ControlImplementation).AddGridHeader(indent, colNo, rowNo, indentSpan, 1, isColumnar);
                            colNo = colNo + indentSpan;
                        }

                        if (columnController.ID == columns.Last().ID)
                        {
                            //fill remaining space
                            int remainingColumns = numGridCols - colNo;
                            ((IGnosisGridImplementation)ControlImplementation).AddGridHeader(columnController.Header, colNo, rowNo, remainingColumns, 1, isColumnar);
                        }
                        else
                        {
                            ((IGnosisGridImplementation)ControlImplementation).AddGridHeader(columnController.Header, colNo, rowNo, columnController.ColSpan, 1, isColumnar);
                            colNo = colNo + columnController.ColSpan;
                        }
                        
                        placed = true;

                        if (rowNo == 0)
                        {
                            numFieldCols++;
                        }
                    }
                    else
                    {
                        //start a new row
                        rowNo++;
                        ((IGnosisGridImplementation)ControlImplementation).AddHeaderRow();
                        colNo = 0;
                    }
                }
            }

            //load data rows
            rowNo = 0;
            colNo = 0;

            //first data row
            ((IGnosisGridImplementation)ControlImplementation).AddRowAutoHeight();//AddRow(rowHeight);

            foreach (KeyValuePair<int, GnosisGridRowController> row in rowControllers)
            {
                foreach (IGnosisGridFieldImplementation field in row.Value.Fields)
                {
                    GnosisGridColumnController columnController = columns.Where(c => c.Order == field.Order).First();
                    int colSpan = columnController.ColSpan;
                    bool placed = false;
                    while (!placed)
                    {//while5
                        if (colNo + colSpan <= numGridCols)
                        {//if6
                            if (colNo == 0 && columnController.ID != columns.First().ID)
                            {//if7
                                //apply indent
                                IGnosisGridIndentFieldImplementation indentImp = GnosisControlCreator.CreateGnosisGridIndentField();
                                //IGnosisGridTextFieldImplementation indentImp = GlobalData.Singleton.ImplementationCreator.GetGnosisGridTextFieldImplementation();
                                //indentImp.RemoveOutlineColour();
                                //indentImp.SetVerticalContentAlignment(VerticalAlignmentType.Centre);
                                int indentSpan = GetColSpan(1, characterWidth, widthGridCol);

                                ((IGnosisGridImplementation)ControlImplementation).LoadCell(indentImp, colNo, rowNo, indentSpan, 1, alternateRowColour);
                                colNo = colNo + indentSpan;
                            }//if7
                            
                            //undo fixed width from columnar layout
                            field.SetWidth(double.NaN);
                            field.SetHorizontalAlignment(HorizontalAlignmentType.STRETCH);

                            ((IGnosisGridImplementation)ControlImplementation).LoadCell(field, colNo, rowNo, colSpan, 1, alternateRowColour);
                            colNo = colNo + colSpan;
                            placed = true;

                            //check NumLines is met
                            if (field is IGnosisResultsTextFieldImplementation)
                            {//7
                                int numLinesNeeded = columnController.NumLines;
                                if (numLinesNeeded != ((IGnosisResultsTextFieldImplementation)field).NumLines)
                                {//8
                                    if (numLinesNeeded > 1)
                                    {//9
                                        ((IGnosisResultsTextFieldImplementation)field).SetTextWrapping(true);
                                    }//9
                                    else
                                    {//9
                                        ((IGnosisResultsTextFieldImplementation)field).SetTextWrapping(false);
                                    }//9

                                    double newHeight = columnController.TextHeight * numLinesNeeded + columnController.VerticalPadding * 2;
                                    ((IGnosisResultsTextFieldImplementation)field).SetHeight(newHeight);
                                    ((IGnosisResultsTextFieldImplementation)field).NumLines = numLinesNeeded;
                                }//8
                             
                            }//7
                            else if (field is IGnosisGridTextFieldImplementation)
                            {//7
                                int numLinesNeeded = columnController.NumLines;

                                if (numLinesNeeded != ((IGnosisGridTextFieldImplementation)field).NumLines)
                                {//8
                                    if (numLinesNeeded > 1)
                                    {//9
                                        ((IGnosisGridTextFieldImplementation)field).SetTextWrapping(true);
                                    }//9
                                    else
                                    {//9
                                        ((IGnosisGridTextFieldImplementation)field).SetTextWrapping(false);
                                    }//9

                                    double newHeight = columnController.TextHeight * numLinesNeeded + columnController.VerticalPadding * 2;
                                    ((IGnosisGridTextFieldImplementation)field).SetHeight(newHeight);
                                    ((IGnosisGridTextFieldImplementation)field).NumLines = numLinesNeeded;
                                }//8

                            }//7

                        }//if6
                        else
                        {//else6
                            rowNo++;
                            colNo = 0;
                            ((IGnosisGridImplementation)ControlImplementation).AddRowAutoHeight();//AddRow(rowHeight);
                        }//else6
                    }//while5

                }
                LayoutCompleted = true;
            }



            stopWatch.Stop();
            Debug.WriteLine("GnosisGridController, LayoutRows, Milliseconds elapsed: {0}", stopWatch.ElapsedMilliseconds);
        }

        private int GetColSpan(int numCharacters, double characterWidth, double widthGridCol)
        {
            double width = numCharacters * characterWidth;// + (2 * styleManager.GetPaddingHorizontal());
            int colSpan = (int)Math.Ceiling(width / widthGridCol);

            return colSpan;
        }

        private void AssignColSpans(double totalWidthAvailable, double widthGridCol, bool isSecondaryRow)
        {


            //double totalMinWidth = columns.Sum(c => c.MinFieldWidth);

            //if (totalMinWidth <= totalWidthAvailable)
            //{
            //    //all columns will fit on one row
            //    foreach (GnosisGridColumnController columnController in columns)
            //    {
            //        row.Add(columnController);
            //    }
            //}
            //else
            //{
            //    //we need more than one row
            //    double remainingWidth = totalWidthAvailable;

            //    foreach (GnosisGridColumnController columnController in columns)
            //    {
            //        if (remainingWidth - columnController.MinFieldWidth <= 0)
            //        {
            //            row = new List<GnosisGridColumnController>();
            //            headerRows.Add(rowNo, row);

            //            remainingWidth = totalWidthAvailable;
            //            rowNo++;    
            //        }

            //        row.Add(columnController);
            //        remainingWidth = remainingWidth - columnController.MinFieldWidth;

            //    }

            //}

            //assign columns to rows 

            headerRows = new Dictionary<int, List<GnosisGridColumnController>>();

            //will all columns fit on one row?
            double minWidth = columns.Sum(c => c.MinFieldWidth);
            if(minWidth <= totalWidthAvailable)
            {
                List<GnosisGridColumnController> row = new List<GnosisGridColumnController>();

                foreach (GnosisGridColumnController columnController in columns)
                {
                    row.Add(columnController);
                }

                headerRows.Add(0, row);
            }
            else
            {
                double remainingWidth = totalWidthAvailable;
                List<GnosisGridColumnController> row = new List<GnosisGridColumnController>();
                headerRows.Add(0, row);
                int idx = 0;

                foreach (GnosisGridColumnController columnController in columns)
                {
                    if (columnController.MinFieldWidth > totalWidthAvailable)
                    {
                        GlobalData.Singleton.ErrorHandler.HandleError("Minimum field width greater than width available", "GnosisGridController");
                        break;
                    }

                    if (columnController.MinFieldWidth <= remainingWidth)
                    {
                        headerRows[idx].Add(columnController);
                        remainingWidth = remainingWidth - columnController.MinFieldWidth;
                    }
                    else
                    {
                        if (headerRows.Count() + 1 > ((GnosisGrid)ControlImplementation).MaxWrapRows)
                        {
                            LoadColumnar(totalWidthAvailable);
                            return;
                        }
                        idx++;
                        List<GnosisGridColumnController> newRow = new List<GnosisGridColumnController>();
                        headerRows.Add(idx, newRow);

                        headerRows[idx].Add(columnController);
                    }
                }
               
            }

            

            bool finished = false;
            bool impossible = false;
            bool needColumnarFormat = false;

            while (!finished)
            {//1
                int rowNum = 0;
                int order = 1;

                for (int i = 0; i < headerRows.Count(); i++)
                {//2
                    if (needColumnarFormat)
                    {
                        finished = true;
                        break;
                    }

                    rowNum = i;
                    double totalMinFieldWidth = headerRows[i].Sum(c => c.MinFieldWidth);
                    int remainingGridCols = numGridCols;
                    int numLines = 1;

                    foreach (GnosisGridColumnController columnController in headerRows[i])
                    {//3
                        double proportion = columnController.MinFieldWidth / totalMinFieldWidth;
                        int colSpan = (int)Math.Round(proportion * numGridCols);

                        double width = colSpan * widthGridCol;
                        if (width < columnController.MinFieldWidth)
                        {//4
                            colSpan = (int)Math.Ceiling(columnController.MinFieldWidth / widthGridCol);
                        }//4

                        //if (remainingGridCols - colSpan < 0)
                        //{//4
                        //    impossible = true;

                        //    if (headerRows.Count() + 1 > ((GnosisGrid)ControlImplementation).MaxWrapRows)
                        //    {//5
                        //        needColumnarFormat = true;
                        //    }//5
                        //    else
                        //    {//5 
                        //        order = columnController.Order;
                        //    }//5

                        //    break;
                        //}//4
                        while (remainingGridCols - colSpan < 0)
                        {
                            numGridCols++;
                            remainingGridCols++;
                            ((GnosisGrid)ControlImplementation).AddColumn();
                        }

                        columnController.ColSpan = colSpan;
                        remainingGridCols = remainingGridCols - columnController.ColSpan;

                        width = colSpan * widthGridCol;
                        int displayChars = (int)Math.Floor(width - (2 * columnController.HorizontalPadding) / characterWidth) * numLines;
                        while (displayChars < columnController.MinDisplayChars)
                        {//4
                            numLines++;
                            if (numLines > ((GnosisGrid)ControlImplementation).MaxLines)
                            {//5
                                //we cannot continue with this layout

                                impossible = true;

                                //check if we are allowed to add another row
                                if (headerRows.Count() + 1 > ((GnosisGrid)ControlImplementation).MaxWrapRows)
                                {//6
                                    needColumnarFormat = true;
                                }//6
                                else
                                { //6
                                    //store a reference to this column then break out of the for loop in order to modify the header rows
                                    order = columnController.Order;

                                  
                                    
                                }//6

                                //start over
                                break;
                            }//5
                            displayChars = (int)Math.Floor(width - (2 * columnController.HorizontalPadding) / characterWidth) * numLines;

                        }//4

                        if (impossible)
                        {//4
                            //start over
                            break;
                        }//4

                    }//3

                    if (impossible)
                    {//3
                        break;
                    }//3
                    
                    if (!impossible && numLines > 1)
                    {//3
                        foreach (GnosisGridColumnController columnController in headerRows[i])
                        {//4
                            columnController.NumLines = numLines;
                        }//4
                    }//3

                }//2

                if (impossible && !needColumnarFormat)
                {//2
                    //rebuild the row with all previous columns
                    List<GnosisGridColumnController> newRow = new List<GnosisGridColumnController>();
                    foreach (GnosisGridColumnController colController in headerRows[rowNum].Where(c => c.Order < order))
                    {//3
                        newRow.Add(colController);
                    }//3

                    //create a new row with the remaning columns
                    List<GnosisGridColumnController> nextRow = new List<GnosisGridColumnController>();
                    foreach (GnosisGridColumnController colController in headerRows[rowNum].Where(c => c.Order >= order))
                    {//3
                        nextRow.Add(colController);
                    }//3
                    headerRows[rowNum] = newRow;
                    if (headerRows.ContainsKey(rowNum + 1))
                    {//3
                        headerRows[rowNum + 1] = nextRow;
                    }//3
                    else
                    {//3
                        headerRows.Add(rowNum + 1, nextRow);
                    }//3
                }//2
                else
                {//2
                    finished = true;
                }//2

            }//1

            if (needColumnarFormat)
            {
                LoadColumnar(totalWidthAvailable);
            }
            else
            {
                LayoutRows(totalWidthAvailable);
            }
          

            //foreach (List<GnosisGridColumnController> headerRow in headerRows.Values)
            //{
            //    //Share out grid cols according to MinDisplayChars
            //    //Check that MinFieldWidth is met
            //    int remainingGridCols = numGridCols;
            //    double totalMinDisplayChars = headerRow.Sum(c => c.MinDisplayChars);
            //    int numLines = 1;

            //    foreach (GnosisGridColumnController columnController in headerRow)
            //    {
            //        double proportion = columnController.MinDisplayChars / totalMinDisplayChars;
            //        int colSpan = (int)Math.Round(proportion * numGridCols);

            //        double width = colSpan * widthGridCol;
            //        if (width < columnController.MinFieldWidth)
            //        {
            //            colSpan = (int)Math.Ceiling(columnController.MinFieldWidth / widthGridCol);
            //        }

            //        while (remainingGridCols - colSpan <= 0)
            //        {
            //            colSpan--;
            //        }

            //        columnController.ColSpan = colSpan;
            //        remainingGridCols = remainingGridCols - columnController.ColSpan;

            //        width = colSpan * widthGridCol;
            //        int displayChars = (int)Math.Floor(width - (2 * columnController.HorizontalPadding) / characterWidth) * numLines;
            //        while (displayChars < columnController.MinDisplayChars)
            //        {
            //            numLines++;
            //            if (numLines > ((GnosisGrid)ControlImplementation).MaxLines)
            //            {

            //            }
            //            displayChars = (int)Math.Floor(width - (2 * columnController.HorizontalPadding) / characterWidth) * numLines;

            //        }
            //    }

            //    //all fields are given the same num lines for alignment
            //    if (numLines > 1)
            //    {
            //        foreach (GnosisGridColumnController columnController in headerRow)
            //        {
            //            columnController.NumLines = numLines;
            //        }
            //    }
            //}

            //foreach (GnosisGridColumnController columnController in columns)
            //{
            //    double proportion = columnController.MinDisplayChars / totalMinDisplayChars;
            //    int colSpan = (int)Math.Round(proportion * numGridCols);

            //    double width = colSpan * widthGridCol;
            //    if (width < columnController.MinFieldWidth)
            //    {
            //        colSpan = (int)Math.Ceiling(columnController.MinFieldWidth / widthGridCol);
            //    }

            //    if (remainingGridCols - colSpan <= 0)
            //    {
            //        row = new List<GnosisGridColumnController>();
            //        rowNo += 1;
            //        headerRows.Add(rowNo, row);
            //        remainingGridCols = numGridCols;
            //    }

            //    columnController.ColSpan = colSpan;
            //    remainingGridCols = remainingGridCols - columnController.ColSpan;
            //    row.Add(columnController);

            //}


            //if (failed)
            //{

            //}


            //First find all fixed width fields (MinFieldWidth == MaxFieldWidth)
            //int totalColSpan = 0;
            //List<GnosisGridColumnController> nonFixedWidthColumns = new List<GnosisGridColumnController>();
            //foreach (GnosisGridColumnController columnController in cols)
            //{
            //    if (columnController.MinFieldWidth == columnController.MaxFieldWidth)
            //    {
            //        double width = columnController.MinFieldWidth;
            //        columnController.ColSpan = (int)Math.Round(width / widthGridCol);

            //        totalColSpan = totalColSpan + columnController.ColSpan;
            //    }
            //    else
            //    {
            //        nonFixedWidthColumns.Add(columnController);
            //    }
            //}

            ////now share out the remaining grid columns according to MinDisplayChars
            //double totalMinDisplayChars = nonFixedWidthColumns.Sum(c => c.MinDisplayChars);

            //if (isSecondaryRow)
            //{
            //    //leave space for indent
            //    int indentCols = GetColSpan(1, characterWidth, widthGridCol);
            //    totalColSpan = totalColSpan + indentCols;
            //}
            //int remainingNumGridCols = numGridCols - totalColSpan;

            //foreach (GnosisGridColumnController nonFixedWidthColumn in nonFixedWidthColumns)
            //{
            //    double proportion = nonFixedWidthColumn.MinDisplayChars / totalMinDisplayChars;
            //    nonFixedWidthColumn.ColSpan = (int)Math.Round(proportion * remainingNumGridCols);
            //    // int compare = GetColSpan(nonFixedWidthColumn.MinDisplayChars, nonFixedWidthColumn.StyleManager, widthGridCol);

            //    double width = nonFixedWidthColumn.ColSpan * widthGridCol;
            //    //check that MinFieldWidth is met
            //    if (width < nonFixedWidthColumn.MinFieldWidth)
            //    {
            //        nonFixedWidthColumn.ColSpan = (int)Math.Ceiling(nonFixedWidthColumn.MinFieldWidth / widthGridCol);
            //    }

            //    //check that MaxFieldWidth has not been exceeded
            //    if (width > nonFixedWidthColumn.MaxFieldWidth)
            //    {
            //        nonFixedWidthColumn.ColSpan = (int)Math.Floor(nonFixedWidthColumn.MaxFieldWidth / widthGridCol);
            //    }

            //    totalColSpan = totalColSpan + nonFixedWidthColumn.ColSpan;

            //    while (totalColSpan > numGridCols)
            //    {
            //        nonFixedWidthColumn.ColSpan--;
            //        totalColSpan--;
            //    }
            //}

            //while (totalColSpan < numGridCols)
            //{
            //    cols.Last().ColSpan++;
            //    totalColSpan++;
            //}

        }

        private void WidthChanged_DoWorkBackground(object sender, DoWorkEventArgs e)
        {
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            //newWidth is compared to the stored value oldWidth to determine if the width has increased or decreased.
            //for text fields, we may be able to change the height without changing the layout
            if (rowControllers.Count > 0)
            {
                GnosisGridRowController firstRow = rowControllers[0];

                if (!isColumnar)
                {//1
                    if (newWidth < oldWidth)
                    {//2
                        //width has decreased. Check if MinDisplayChars are being displayed for all columns
                        int maxLines = ((GnosisGrid)ControlImplementation).MaxLines;
                        int lastColumnOrder = firstRow.Fields.OrderBy(f => f.Order).Last().Order;
                        var firstRowColumns = columns.Where(c => c.Order <= lastColumnOrder);

                        //if (newWidth < totalMinWidth)
                        //{//3
                        //    layoutNeeded = true;
                        //}//3
                        if (minWidth == 0)
                        {//3
                            minWidth = firstRowColumns.Sum(c => c.MinFieldWidth);

                        }//3

                        if (newWidth < minWidth)
                        {//3
                            //we need to something
                            //we may be able to increase field height
                            if (((GnosisGrid)ControlImplementation).MaxLines <= 1)
                            {//4
                                //field height can not be increased. We need to move some columns to a new row
                                layoutNeeded = true;
                            }//4
                            else
                            {//4
                                //try increasing field height
                                //take the first row and find the minimum number of lines which meets the requirements of all fields
                                int numLines = 1;
                                foreach (IGnosisGridFieldImplementation fieldImp in firstRow.Fields)
                                {//5
                                    GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
                                    double fieldWidth = fieldImp.GetWidth();
                                    int charsPerLine = (int)Math.Floor((fieldWidth - 2 * fieldImp.HorizontalPadding) / columnController.CharacterWidth);
                                    int numChars = 0;

                                    if (fieldImp is IGnosisResultsTextFieldImplementation)
                                    {//6
                                        numChars = charsPerLine * ((IGnosisResultsTextFieldImplementation)fieldImp).NumLines;
                                    }//6
                                    else if (fieldImp is IGnosisGridTextFieldImplementation)
                                    {//6
                                        numChars = charsPerLine * ((IGnosisGridTextFieldImplementation)fieldImp).NumLines;
                                    }//6

                                    if (numChars < columnController.MinDisplayChars)
                                    {//6
                                        int numLinesNeeded = (int)Math.Ceiling((double)columnController.MinDisplayChars / charsPerLine);

                                        if (numLinesNeeded > ((GnosisGrid)ControlImplementation).MaxLines)
                                        {//7
                                            layoutNeeded = true;
                                            break;
                                        }//7
                                        else
                                        {//7
                                            if (numLinesNeeded > numLines)
                                            {//8
                                                numLines = numLinesNeeded;
                                            }//8

                                        }//7
                                    }//6

                                }//5

                                //numLines has been calculated. Now apply it to all fields
                                foreach (IGnosisGridFieldImplementation fieldImp in firstRow.Fields)
                                {//6
                                    if (fieldImp is IGnosisResultsTextFieldImplementation)
                                    {//7
                                        GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
                                        if (numLines != ((IGnosisResultsTextFieldImplementation)fieldImp).NumLines)
                                        {//8
                                            columnController.NumLines = numLines;

                                            //Use Dispatcher to touch UI since this is run in a background thread
                                            Application.Current.Dispatcher.Invoke((Action)(() =>
                                            {//9
                                                foreach (IGnosisGridFieldImplementation gridFieldImp in columnController.Fields)
                                                {//10
                                                    if (numLines > 1)
                                                    {//11
                                                        ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetTextWrapping(true);
                                                    }//11
                                                    else
                                                    {//11
                                                        ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetTextWrapping(false);
                                                    }//11

                                                    double newHeight = columnController.TextHeight * numLines + columnController.VerticalPadding * 2;
                                                    ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetHeight(newHeight);
                                                    ((IGnosisResultsTextFieldImplementation)gridFieldImp).NumLines = numLines;
                                                }//10
                                            }));//9
                                        }//8
                                    }//7
                                    else if (fieldImp is IGnosisGridTextFieldImplementation)
                                    {//7
                                        GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
                                        if (numLines != ((IGnosisGridTextFieldImplementation)fieldImp).NumLines)
                                        {//8
                                            columnController.NumLines = numLines;

                                            //Use Dispatcher to touch UI since this is run in a background thread
                                            Application.Current.Dispatcher.Invoke((Action)(() =>
                                            {//9
                                                foreach (IGnosisGridTextFieldImplementation gridFieldImp in columnController.Fields)
                                                {//10
                                                    if (numLines > 1)
                                                    {//11
                                                        ((IGnosisGridTextFieldImplementation)gridFieldImp).SetTextWrapping(true);
                                                    }//11
                                                    else
                                                    {//11
                                                        ((IGnosisGridTextFieldImplementation)gridFieldImp).SetTextWrapping(false);
                                                    }//11

                                                    double newHeight = columnController.TextHeight * numLines + columnController.VerticalPadding * 2;
                                                    ((IGnosisGridTextFieldImplementation)gridFieldImp).SetHeight(newHeight);
                                                    ((IGnosisGridTextFieldImplementation)gridFieldImp).NumLines = numLines;
                                                }//10
                                            }));//9
                                        }//8
                                    }//7
                                }//6

                                //reset minWidth since NumLines has increased
                                minWidth = 0;

                            }//4
                        }//3
                        else //minWidth is met, no need to do anything
                        {//3 
                        //    //width is sufficient for this layout but more lines may be needed
                        //    if (((GnosisGrid)ControlImplementation).MaxLines > 1)
                        //    {//4 
                        //        //check if MinDisplayChars are still met
                        //        int numLines = 1;

                        //        foreach (IGnosisGridFieldImplementation fieldImp in firstRow.Fields)
                        //        {//5
                        //            GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
                        //            double fieldWidth = fieldImp.GetWidth();
                        //            int charsPerLine = (int)Math.Floor(fieldWidth / columnController.CharacterWidth);
                        //            int numChars = 0;

                        //            if (fieldImp is IGnosisResultsTextFieldImplementation)
                        //            {//6
                        //                numChars = charsPerLine * ((IGnosisResultsTextFieldImplementation)fieldImp).NumLines;
                        //            }//6
                        //            else if (fieldImp is IGnosisGridTextFieldImplementation)
                        //            {//6
                        //                numChars = charsPerLine * ((IGnosisGridTextFieldImplementation)fieldImp).NumLines;
                        //            }//6

                        //            if (numChars < columnController.MinDisplayChars)
                        //            {//6
                        //                int numLinesNeeded = (int)Math.Ceiling((double)columnController.MinDisplayChars / charsPerLine);

                        //                if (numLinesNeeded > ((GnosisGrid)ControlImplementation).MaxLines)
                        //                {//7
                        //                    layoutNeeded = true;
                        //                    break;
                        //                }//7
                        //                else
                        //                {//7
                        //                    if (numLinesNeeded > numLines)
                        //                    {//8
                        //                        numLines = numLinesNeeded;
                        //                    }//8

                        //                }//7
                        //            }//6

                        //        }//5


                        //        if (!layoutNeeded)
                        //        {//5
                        //            foreach (IGnosisGridFieldImplementation fieldImp in firstRow.Fields)
                        //            {//6
                        //                if (fieldImp is IGnosisResultsTextFieldImplementation)
                        //                {//7
                        //                    GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
                        //                    if (numLines != ((IGnosisResultsTextFieldImplementation)fieldImp).NumLines)
                        //                    {//8
                        //                        columnController.NumLines = numLines;

                        //                        //Use Dispatcher to touch UI since this is run in a background thread
                        //                        Application.Current.Dispatcher.Invoke((Action)(() =>
                        //                        {//9
                        //                            foreach (IGnosisGridFieldImplementation gridFieldImp in columnController.Fields)
                        //                            {//10
                        //                                if (numLines > 1)
                        //                                {//11
                        //                                    ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetTextWrapping(true);
                        //                                }//11
                        //                                else
                        //                                {//11
                        //                                    ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetTextWrapping(false);
                        //                                }//11

                        //                                double newHeight = columnController.TextHeight * numLines + columnController.PaddingVertical * 2;
                        //                                ((IGnosisResultsTextFieldImplementation)gridFieldImp).SetHeight(newHeight);
                        //                                ((IGnosisResultsTextFieldImplementation)gridFieldImp).NumLines = numLines;
                        //                            }//10
                        //                        }));//9
                        //                    }//8
                        //                }//7
                        //                else if (fieldImp is IGnosisGridTextFieldImplementation)
                        //                {//7
                        //                    GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
                        //                    if (numLines != ((IGnosisGridTextFieldImplementation)fieldImp).NumLines)
                        //                    {//8
                        //                        columnController.NumLines = numLines;

                        //                        //Use Dispatcher to touch UI since this is run in a background thread
                        //                        Application.Current.Dispatcher.Invoke((Action)(() =>
                        //                        {//9
                        //                            foreach (IGnosisGridTextFieldImplementation gridFieldImp in columnController.Fields)
                        //                            {//10
                        //                                if (numLines > 1)
                        //                                {//11
                        //                                    ((IGnosisGridTextFieldImplementation)gridFieldImp).SetTextWrapping(true);
                        //                                }//11
                        //                                else
                        //                                {//11
                        //                                    ((IGnosisGridTextFieldImplementation)gridFieldImp).SetTextWrapping(false);
                        //                                }//11

                        //                                double newHeight = columnController.TextHeight * numLines + columnController.PaddingVertical * 2;
                        //                                ((IGnosisGridTextFieldImplementation)gridFieldImp).SetHeight(newHeight);
                        //                                ((IGnosisGridTextFieldImplementation)gridFieldImp).NumLines = numLines;
                        //                            }//10
                        //                        }));//9
                        //                    }//8
                        //                }//7
                        //            }//6

                        //        }//5
                        //    }//4
                        }//3
                    }//2
                    else //width increased
                    {//2
                     //if rows are currently wrapping, we might now be able to fit all the fields on one row
                        if (headerRows != null && headerRows.Count > 1 && newWidth > minWidthForSingleRow)
                        {//3
                            layoutNeeded = true;
                        }//3
                        else
                        { //3
                          //we can check if text fields can have their height reduced
                            int numLines = 1;
                            foreach (IGnosisGridFieldImplementation fieldImp in firstRow.Fields)
                            {//4
                                if (fieldImp is IGnosisResultsTextFieldImplementation)
                                {//5
                                    GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
                                    double fieldWidth = fieldImp.GetWidth();

                                    if (((IGnosisResultsTextFieldImplementation)fieldImp).NumLines > 1)
                                    {//6
                                        int charsPerLine = (int)Math.Floor(fieldWidth / columnController.CharacterWidth);
                                        int numLinesNeeded = (int)Math.Ceiling((double)columnController.MinDisplayChars / charsPerLine);

                                        if (numLinesNeeded > numLines)
                                        {//7
                                            numLines = numLinesNeeded;
                                        }//7

                                    }//6

                                }//5
                                else if (fieldImp is IGnosisGridTextFieldImplementation)
                                {//5
                                    GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);
                                    double fieldWidth = fieldImp.GetWidth();

                                    if (((IGnosisGridTextFieldImplementation)fieldImp).NumLines > 1)
                                    {//6
                                        int charsPerLine = (int)Math.Floor(fieldWidth / columnController.CharacterWidth);
                                        int numLinesNeeded = (int)Math.Ceiling((double)columnController.MinDisplayChars / charsPerLine);

                                        if (numLinesNeeded > numLines)
                                        {
                                            numLines = numLinesNeeded;
                                        }

                                    }//6
                                }//5

                            }// 4

                            foreach (IGnosisGridFieldImplementation fieldImp in firstRow.Fields)
                            {//4
                                if (fieldImp is IGnosisResultsTextFieldImplementation)
                                {//5 
                                    GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);

                                    if (numLines != ((IGnosisResultsTextFieldImplementation)fieldImp).NumLines)
                                    {//6
                                     //Use Dispatcher to touch UI since this is run in a background thread
                                        Application.Current.Dispatcher.Invoke((Action)(() =>
                                        {//7
                                        foreach (IGnosisResultsTextFieldImplementation resultsFieldImp in columnController.Fields)
                                            { //8
                                            resultsFieldImp.NumLines = numLines;
                                                if (numLines > 1)
                                                {//9
                                                resultsFieldImp.SetTextWrapping(true);
                                                }//9
                                            else
                                                {//9
                                                resultsFieldImp.SetTextWrapping(false);
                                                }//9

                                            double newHeight = columnController.TextHeight * numLines + columnController.VerticalPadding * 2;
                                                resultsFieldImp.SetHeight(newHeight);
                                            }//8
                                    }));//7
                                    }//6
                                }//5
                                else if (fieldImp is IGnosisGridTextFieldImplementation)
                                {//5
                                    GnosisGridColumnController columnController = columns.Find(c => c.Order == fieldImp.Order);

                                    if (numLines != ((IGnosisGridTextFieldImplementation)fieldImp).NumLines)
                                    {//6
                                     //Use Dispatcher to touch UI since this is run in a background thread
                                        Application.Current.Dispatcher.Invoke((Action)(() =>
                                        {//7
                                        foreach (IGnosisGridTextFieldImplementation gridTextField in columnController.Fields)
                                            { //8
                                            gridTextField.NumLines = numLines;
                                                if (numLines > 1)
                                                {//9
                                                gridTextField.SetTextWrapping(true);
                                                }//9
                                            else
                                                {//9
                                                gridTextField.SetTextWrapping(false);
                                                }//9

                                            double newHeight = columnController.TextHeight * numLines + columnController.VerticalPadding * 2;
                                                gridTextField.SetHeight(newHeight);
                                            }//8
                                    }));//7
                                    }//6
                                }//5
                            }//4

                        }//3
                    }//2
                }//1

            }
            oldWidth = newWidth;
            stopWatch.Stop();

            Debug.WriteLine("GridController WidthChanged_DoWorkBackground, Milliseconds elapsed: {0}", stopWatch.ElapsedMilliseconds);
        }

        private void WidthChanged_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (layoutNeeded)
            {
                LayoutRows();
            }
        }

        public void WidthChanged(double _newWidth)
        {

            if (rowControllers == null)
            {
                return; //no data
            }

            //if (!widthChangedBackgroundWorker.IsBusy)
            //{
            //    newWidth = _newWidth;
            //    layoutNeeded = false;
            //    widthChangedBackgroundWorker.RunWorkerAsync();
            //}

            LayoutRows();

            //don't run the algorithm on every tiny change
            //if (Math.Abs(newWidth - oldWidth) < 2)
            //{
            //    return;
            //}

        
        }

        public void LoadColumnar(double totalWidthAvailable)
        {
            isColumnar = true;
            ((IGnosisGridImplementation)ControlImplementation).Clear();
            ((IGnosisGridImplementation)ControlImplementation).SetColumnarFormat(isColumnar);
          //  ((IGnosisGridImplementation)ControlImplementation).SetHeight(rowHeight * MinDisplayRows * columns.Count());

            alternateRowColour = false;

            CaptionPosition captionPosition = headerRows.First().Value.First().Header.RelativePosition;

            switch (captionPosition)
            {
                case CaptionPosition.LEFT:
                    ColumnarLeft();
                    break;
                case CaptionPosition.ABOVE:
                    ColumnarAbove(totalWidthAvailable);
                    break;
            }


        }


        private void ColumnarLeft()
        {
            int rowNo = 0;
            ((IGnosisGridImplementation)ControlImplementation).NumColumns = 2;

            foreach (GnosisGridRowController row in rowControllers.Values)
            {
                foreach (IGnosisGridFieldImplementation field in row.Fields)
                {
                    ((IGnosisGridImplementation)ControlImplementation).AddRow(rowHeight);

                    //header
                    GnosisGridColumnController columnController = columns.Where(c => c.Order == field.Order).First();
                    ((IGnosisGridImplementation)ControlImplementation).LoadCell(columnController.Header, 0, rowNo, 1, 1);

                    //field
                    ((IGnosisGridImplementation)ControlImplementation).LoadCell(field, 1, rowNo, 1, 1, alternateRowColour);

                    rowNo++;
                }

                alternateRowColour = !alternateRowColour;
            }
        }


        private void ColumnarAbove(double totalWidthAvailable)
        {
            int rowNo = 0;
            ((IGnosisGridImplementation)ControlImplementation).NumColumns = 1;

            //double widthGridCol = totalWidthAvailable / numGridCols;
            totalWidthAvailable = totalWidthAvailable - 20;

            foreach (GnosisGridRowController row in rowControllers.Values)
            {
                foreach (IGnosisGridFieldImplementation field in row.Fields)
                {
                    GnosisGridColumnController columnController = columns.Find(c => c.Order == field.Order);

                    double minFieldWidth = columnController.MinFieldWidth;
                    double maxFieldWidth = columnController.MaxFieldWidth;

                    if (columnController.MaxDisplayChars == 0)
                    {
                        // ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(displayWidth);
                        field.SetHorizontalAlignment(HorizontalAlignmentType.STRETCH);
                    }
                    else if (totalWidthAvailable > maxFieldWidth)
                    {//if2
                        field.SetWidth(maxFieldWidth);
                    }//if2
                    else if (totalWidthAvailable > minFieldWidth)
                    {//elseif2
                        field.SetWidth(totalWidthAvailable);
                    }//elseif2
                    else //(displayWidth < minFieldWidth)
                    {//else2

                        field.SetWidth(totalWidthAvailable);


                    }//else2


                    //header
                    ((IGnosisGridImplementation)ControlImplementation).AddRowAutoHeight();
                    IGnosisCaptionLabelImplementation caption = columnController.GetColumnarHeaderClone();
                    ((IGnosisGridImplementation)ControlImplementation).LoadCell(caption, 0, rowNo, 1, 1);
                    rowNo++;

                    //field
                    ((IGnosisGridImplementation)ControlImplementation).AddRowAutoHeight();
                    field.SetHorizontalAlignment(HorizontalAlignmentType.LEFT);
                    ((IGnosisGridImplementation)ControlImplementation).LoadCell(field, 0, rowNo, 1, 1, alternateRowColour);

                    if (((GnosisGrid)ControlImplementation).HorizontalSpacing > 0)
                    {
                        ((GnosisGrid)ControlImplementation).AddRow(((GnosisGrid)ControlImplementation).HorizontalSpacing);
                        rowNo++;
                    }

                    rowNo++;
                }
            }

            

        }


        internal override void Save()
        {
            foreach (KeyValuePair<int, GnosisGridRowController> rowController in rowControllers)
            {
                //foreach (GnosisGridFieldController fieldController in rowController.Value.Fields.Where(f => !f.Hidden && !f.ReadOnly))
                //{
                //    fieldController.Save(rowController.Key);
                //}
            }
        }

        internal override void SetStrikethrough(bool strikethrough)
        {

            foreach (KeyValuePair<int, GnosisGridRowController> rowController in rowControllers)
            {
                foreach (IGnosisGridFieldImplementation gridField in rowController.Value.Fields)
                {
                    gridField.SetStrikethrough(strikethrough);
                }
            }
        }

        internal override void SizeChanged()
        {
            double width = GetWidth();
            if (Math.Abs(oldWidth - width) > 10)
            {
                WidthChanged(width);
            }
        }

    }
}
