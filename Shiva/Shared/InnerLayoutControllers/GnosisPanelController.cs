using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.Data;
using Shiva.Shared.ContentControllers;
using System.Linq;
using System.Windows;
using Shiva.Shared.OuterLayoutControllers;
using Shiva.Shared.PanelFieldControllers;
using Shiva.Shared.BaseControllers;

namespace Shiva.Shared.InnerLayoutControllers
{
    public class GnosisPanelController : GnosisInnerLayoutController
    {
        private List<GnosisContentController> childControllers;
        private int[,] usedCells;
        private int lastRowUsed;
       // private double fieldRowHeight;
        private int numCols;
        private CaptionPosition captionPosition;
        private double oldWidth;
        private int numSections;
        private double initialWidth;
        private bool loaded;
        private bool laidOut;
        private double height;

        public List<GnosisContentController> ChildControllers
        {
            get { return childControllers; }
        }

        public bool Loaded
        {
            get { return loaded; }
            set
            {
                loaded = value;
                OnPropertyChanged("Loaded");
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }


        public GnosisPanelController (
            GnosisPanel panel, 
            //IGnosisPanelImplementation panelImplementation,
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController parent)
            : base (panel, instanceController, parent)
        {
           // fieldRowHeight = StyleManager.GetFieldHeight();
            panel.SetLoadedHandler(new Action<double>(PanelLoaded));
            panel.SetIsVisibleChangedHandler(new Action<bool>(IsVisibleChanged));
           // panelImplementation.SetWidthChangedHandler(new Action<double>(WidthChanged));
            panel.SetHeightChangedHandler(new Action<double>(HeightChanged));

            
            captionPosition = EntityController.GetCaptionStyle()._CaptionRelativePosition;

            //IGnosisCaptionLabelImplementation captionLabel = GlobalData.Singleton.ImplementationCreator.GetGnosisCaptionLabelImplementation();
            //StyleController captionStyle = new StyleController(instanceController.EntityController.GetCaptionStyle(), this, captionLabel);
            //captionPosition =  captionLabel.GetRelativePosition();

        }

        internal override void Save()
        {
            foreach (GnosisContentController child in childControllers
                .Where(c => !((IGnosisContentControlImplementation)c.ControlImplementation).Hidden))
            {
                child.Save(0);
            }
        }

        internal override void SetStrikethrough(bool strikethrough)
        {
            foreach (GnosisContentController child in childControllers
                .Where(c => !((IGnosisContentControlImplementation)c.ControlImplementation).Hidden))
            {
                child.SetStrikethrough(strikethrough);
            }
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                foreach (GnosisContentController child in childControllers)
                {
                    child.Editable = this.Editable;
                }
            }
        }

        public void HeightChanged(double newHeight)
        {
            Height = newHeight;
        }

        public  void WidthChanged(double newWidth)
        {
            if (laidOut)
            {
                //don't run on every micro pixel change
                if (Math.Abs(newWidth - oldWidth) < 2)
                {
                    return;
                }

                double colWidth = newWidth / numCols;
                bool widthIncreased = false;
                if (newWidth > oldWidth)
                {
                    widthIncreased = true;
                }

                foreach (GnosisContentController child in childControllers)
                {
                    if (child.MinFieldWidth > 0)
                    {
                        bool layoutChanged = ResizeChild(child, colWidth, widthIncreased);

                        if (layoutChanged)
                        {
                            LayoutChildren();
                            break;
                        }
                    }

                }

                oldWidth = newWidth;
            }

        }


        private bool ResizeChild(GnosisContentController child, double colWidth, bool widthIncreased)
        {
            double minFieldWidth = child.MinFieldWidth;
            double maxFieldWidth = child.MaxFieldWidth;

            double displayWidth = colWidth * child.ColSpan - 8;

            //if (child.MaxDisplayChars == 0)
            //{
            //    //((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(displayWidth);
            //}
            if (displayWidth > maxFieldWidth)
            {
                if (displayWidth - maxFieldWidth > colWidth)
                {
                    //need to decrease col span
                    return true;
                }
                ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(maxFieldWidth);

                if (child is GnosisTextFieldController)
                {
                    ((IGnosisTextFieldImplementation)child.ControlImplementation).SetTextWrapping(false);
                }

            }
            else if (displayWidth > minFieldWidth)
            {
                if (displayWidth - minFieldWidth > colWidth)
                {
                    //need to decrease col span
                    return true;
                }
                ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(displayWidth);

                //set text fields to single line
                if (child is GnosisTextFieldController)
                {
                    ((IGnosisTextFieldImplementation)child.ControlImplementation).SetTextWrapping(false);
                    ((IGnosisTextFieldImplementation)child.ControlImplementation).SetHeight(child.FieldHeight);
                    ((GnosisTextFieldController)child).NumLines = 1;
                    //int row = ((IGnosisTextFieldImplementation)child.ControlImplementation).GetRowNumber();
                    //((IGnosisPanelImplementation)ControlImplementation).SetRowFixedHeight(row, fieldRowHeight);
                }
                

            }
            else if (displayWidth < minFieldWidth)
            {
                if (child.ColSpan < numCols)
                {
                    //colspan must increase, which requires a layout change
                    return true;
                }
                else
                {
                    ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(displayWidth);

                    if (child is GnosisTextFieldController)
                    {
                        int charsPerLine = (int)Math.Floor(displayWidth / child.CharacterWidth);
                        int numChars = charsPerLine * ((GnosisTextFieldController)child).NumLines;

                        if (numChars < child.MinDisplayChars)
                        {
                            int difference = child.MinDisplayChars - numChars;
                            int numLinesNeeded = (int)Math.Ceiling((double)difference / charsPerLine);
                            int totalLines = ((GnosisTextFieldController)child).NumLines + numLinesNeeded;
                            ((GnosisTextFieldController)child).NumLines = totalLines;

                            double newHeight = ((GnosisTextFieldController)child).TextHeight * totalLines + (((GnosisTextFieldController)child).PaddingVertical * 2);
                            ((IGnosisTextFieldImplementation)child.ControlImplementation).SetHeight(newHeight);

                            if (totalLines > 1)
                            {
                                ((IGnosisTextFieldImplementation)child.ControlImplementation).SetTextWrapping(true);
                            }
                            else
                            {
                                ((IGnosisTextFieldImplementation)child.ControlImplementation).SetTextWrapping(false);

                            }
                        }

                        //    double minArea = minFieldWidth * child.FieldHeight;
                        //double oneLineArea = displayWidth * child.FieldHeight;
                        //int numLines = (int)Math.Ceiling(minArea / oneLineArea);

                        //((IGnosisTextFieldImplementation)child.ControlImplementation).SetHeight(numLines * child.FieldHeight);
                        ////int row = ((IGnosisTextFieldImplementation)child.ControlImplementation).GetRowNumber();
                        ////((IGnosisPanelImplementation)ControlImplementation).SetRowFixedHeight(row, numLines * child.FieldHeight);

                        //if (numLines > 1)
                        //{
                        //    ((IGnosisTextFieldImplementation)child.ControlImplementation).SetTextWrapping(true);
                        //}
                        //else
                        //{
                        //    ((IGnosisTextFieldImplementation)child.ControlImplementation).SetTextWrapping(false);

                        //}
                    }

                }
            }
    
           

            return false; //layout has not changed
        }

        public void IsVisibleChanged(bool visible)
        {
            if (visible && loaded && numCols > 0)
            {
                LayoutChildren();
            }
        }

        public void PanelLoaded(double width)
        {
            Loaded = true;

            if (numCols > 0)
            {
                LayoutChildren();
            }
        }

        public override void SetEditMode()
        {
            base.SetEditMode();

            foreach (GnosisContentController child in childControllers)
            {
                child.SetEditMode();
            }
        }

        public void Setup()
        {
            childControllers = new List<GnosisContentController>();

            if (((GnosisPanel)ControlImplementation).TextFields != null)
            {
                foreach (GnosisTextField textField in ((GnosisPanel)ControlImplementation).TextFields.Where(t => !t.Hidden))
                {
                   // IGnosisTextFieldImplementation textFieldImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisTextFieldImplementation();
                    GnosisTextFieldController textFieldController = new GnosisTextFieldController(textField, InstanceController, this);
                    childControllers.Add(textFieldController);
                }
            }

            if (((GnosisPanel)ControlImplementation).NumberFields != null)
            {
                foreach (GnosisNumberField numberField in ((GnosisPanel)ControlImplementation).NumberFields.Where(n => !n.Hidden))
                {
                   // IGnosisTextFieldImplementation numberFieldImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisTextFieldImplementation();
                    GnosisNumberFieldController numberFieldController = new GnosisNumberFieldController(numberField, InstanceController, this);
                    childControllers.Add(numberFieldController);
                }
            }

            if (((GnosisPanel)ControlImplementation).DateFields != null)
            {
                foreach (GnosisDateField dateField in ((GnosisPanel)ControlImplementation).DateFields.Where(d => !d.Hidden))
                {
                   // IGnosisDateFieldImplementation dateFieldImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisDateFieldImplementation();
                    GnosisDateFieldController dateFieldController = new GnosisDateFieldController(dateField, InstanceController, this);
                    //dateFieldController.Setup();
                    childControllers.Add(dateFieldController);
                }
            }

            if (((GnosisPanel)ControlImplementation).DateTimeFields != null)
            {
                foreach (GnosisDateTimeField dateTimeField in ((GnosisPanel)ControlImplementation).DateTimeFields.Where(d => !d.Hidden))
                {
                   // IGnosisDateTimeFieldImplementation dateTimeFieldImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisDateTimeFieldImplementation();
                    GnosisDateTimeFieldController dateTimeFieldController = new GnosisDateTimeFieldController(dateTimeField, InstanceController, this);
                    //dateTimeFieldController.Setup();
                    childControllers.Add(dateTimeFieldController);
                }
            }

            if (((GnosisPanel)ControlImplementation).Buttons != null)
            {
                foreach (GnosisButton button in ((GnosisPanel)ControlImplementation).Buttons.Where(b => !b.Hidden))
                {
                   // IGnosisButtonImplementation buttonImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisButtonImplementation();
                    GnosisButtonController buttonController = new GnosisButtonController(button, InstanceController, this);
                    childControllers.Add(buttonController);
                }
            }

            if (((GnosisPanel)ControlImplementation).CheckFields != null)
            {
                foreach (GnosisCheckField checkField in ((GnosisPanel)ControlImplementation).CheckFields.Where(c => !c.Hidden))
                {
                   // IGnosisCheckFieldImplementation chkFieldImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisCheckFieldImplementation();
                    GnosisCheckFieldController checkFieldController = new GnosisCheckFieldController(checkField, InstanceController, this);
                    childControllers.Add(checkFieldController);
                }
            }

            if (((GnosisPanel)ControlImplementation).ComboFields != null)
            {
                foreach (GnosisComboField comboField in ((GnosisPanel)ControlImplementation).ComboFields.Where(c => !c.Hidden))
                {
                   // IGnosisComboFieldImplementation comboFieldImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisComboFieldImplementation();
                    GnosisComboFieldController comboFieldController = new GnosisComboFieldController(comboField, InstanceController, this);
                    childControllers.Add(comboFieldController);
                }
            }


            if (editMode)
            {
                foreach (GnosisContentController child in childControllers)
                {
                    child.SetEditMode();
                }
            }

            //register for property changes
            foreach (GnosisContentController child in childControllers)
            {
                child.PropertyChanged += Child_PropertyChanged;
            }

        }


        private void LayoutChildren()
        {
            laidOut = false;

            ((IGnosisPanelImplementation)ControlImplementation).Clear();
            ((IGnosisPanelImplementation)ControlImplementation).NumColumns = numCols;

            double totalWidthAvailable = ((IGnosisPanelImplementation)ControlImplementation).GetAvailableWidth();

            if (totalWidthAvailable < 10)
            {
                totalWidthAvailable = initialWidth;
            }

            if (totalWidthAvailable == 0)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("panel width 0", "GnosisPanelController.LayoutChildren");
                return;
            }
            //initial row
            if (captionPosition == CaptionPosition.ABOVE || captionPosition == CaptionPosition.BELOW)
            {
                ((IGnosisPanelImplementation)ControlImplementation).AddRowAutoHeight(); //caption row
             //   ((IGnosisPanelImplementation)ControlImplementation).AddRowFixedHeight(fieldRowHeight); //field row
                ((IGnosisPanelImplementation)ControlImplementation).AddRowAutoHeight(); //field row
            }
            else
            {
                //   ((IGnosisPanelImplementation)ControlImplementation).AddRowFixedHeight(fieldRowHeight);
                ((IGnosisPanelImplementation)ControlImplementation).AddRowAutoHeight(); //field row
            }

            //layout children
            //a control is made up of a field and a caption. Some fields such as buttons do not have a separate caption
            //If the caption is above or below the field, a control row is comprised of 2 layout rows - an autosized caption row and a (fixed) size field row
            //In this case a field spanning more than one row must span the caption row and field row of subsequent control rows. 
            int currentCol = 0;
            int currentRow = 0;
            lastRowUsed = 0;

            //2 dimensional array representing columns and control rows. A value of 1 means cell is filled.
            usedCells = new int[numCols, 50];
            double colWidth = totalWidthAvailable / numCols;

            foreach (GnosisContentController child in childControllers.OrderBy(x => x.ControlImplementation.Order))
            {//foreach1
                int rowSpan = 1;
                child.ColSpan = 1;

                GnosisCaptionLabel captionLabel = null;
                if (!(child is GnosisButtonController || child is GnosisCheckFieldController))
                {
                    //create label
                    string caption = ((IGnosisContentControlImplementation)child.ControlImplementation).Caption;
                    captionLabel = new GnosisCaptionLabel();
                    //  IGnosisCaptionLabelImplementation captionLabelImp = GlobalData.Singleton.ImplementationCreator.GetGnosisCaptionLabelImplementation();
                    GlobalData.Singleton.StyleHelper.ApplyCaptionStyle(captionLabel, EntityController.GetCaptionStyle());
                    captionLabel.Caption = caption;

                   // ((IGnosisPanelImplementation)ControlImplementation).AddGnosisCaptionLabel(captionLabel, currentCol, captionRowNo, child.ColSpan, 1);
                }

                double minFieldWidth = child.MinFieldWidth;
                double maxFieldWidth = child.MaxFieldWidth;

                double displayWidth = colWidth - 8;

                if (captionPosition == CaptionPosition.LEFT || captionPosition == CaptionPosition.RIGHT)
                {
                    displayWidth = displayWidth - captionLabel.GetWidth() - captionLabel.CaptionSpacing;
                }
                //if (child.MaxDisplayChars == 0)
                //{
                //    // ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(displayWidth);
                //    ((IGnosisPanelFieldImplementation)child.ControlImplementation).SetHorizontalAlignment(HorizontalAlignmentType.STRETCH);
                //}
                if (displayWidth > maxFieldWidth)
                {//if2
                    ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(maxFieldWidth);
                }//if2
                else if (displayWidth > minFieldWidth)
                {//elseif2
                    ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(displayWidth);
                }//elseif2
                else //(displayWidth < minFieldWidth)
                {//else2
                    while (displayWidth < minFieldWidth)
                    {//while3
                        if (child.ColSpan < numCols)
                        {//if4
                            //colspan can increase
                            child.ColSpan++;
                            displayWidth = displayWidth + colWidth;
                        }//if4
                        else
                        {//else4
                            //colspan can not increase
                            //can increase the height of text fields
                            if (child is GnosisTextFieldController)
                            {
                                int charsPerLine = (int)Math.Floor(displayWidth / child.CharacterWidth);
                                int numChars = charsPerLine * ((GnosisTextFieldController)child).NumLines;

                                if (numChars < child.MinDisplayChars)
                                {
                                    int difference = child.MinDisplayChars - numChars;
                                    int numLinesNeeded = (int)Math.Ceiling((double)difference / charsPerLine);
                                    int totalLines = ((GnosisTextFieldController)child).NumLines + numLinesNeeded;
                                    ((GnosisTextFieldController)child).NumLines = totalLines;

                                    double newHeight = ((GnosisTextFieldController)child).TextHeight * totalLines + (((GnosisTextFieldController)child).PaddingVertical * 2);
                                    ((IGnosisTextFieldImplementation)child.ControlImplementation).SetHeight(newHeight);

                                }
                                //double minArea = minFieldWidth * child.FieldHeight;
                                //double oneLineArea = displayWidth * child.FieldHeight;
                                //int numLines = (int)Math.Ceiling(minArea / oneLineArea);

                                //((IGnosisTextFieldImplementation)child.ControlImplementation).SetHeight(child.FieldHeight * numLines);

                                //if (numLines > 1)
                                //{
                                //    ((IGnosisTextFieldImplementation)child.ControlImplementation).SetTextWrapping(true);
                                //}
                            }

                            break;

                        }//else4
                    }//while3

                    if (displayWidth > maxFieldWidth)
                    {//if3
                        ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(maxFieldWidth);
                    }//if3
                    else 
                    {//elseif3
                        ((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(displayWidth);
                    }//elseif3

                    //((IGnosisContentControlImplementation)child.ControlImplementation).SetWidth(displayWidth);


                }//else2



                if (currentCol + child.ColSpan > numCols)
                {
                    currentRow++;
                    currentCol = 0;
                    int horizontalSpacing = ((GnosisPanel)ControlImplementation).HorizontalSpacing;

                    if (captionPosition == CaptionPosition.ABOVE || captionPosition == CaptionPosition.BELOW)
                    {
                        if (horizontalSpacing > 0)
                        {
                            ((IGnosisPanelImplementation)ControlImplementation).AddRowAutoHeight();
                            ((IGnosisPanelImplementation)ControlImplementation).AddRowFixedHeight(horizontalSpacing);

                            currentRow++;
                        }
                        
                        ((IGnosisPanelImplementation)ControlImplementation).AddRowAutoHeight(); //caption row
                        //((IGnosisPanelImplementation)ControlImplementation).AddRowFixedHeight(fieldRowHeight); //field row
                        ((IGnosisPanelImplementation)ControlImplementation).AddRowAutoHeight(); //field row
                    }
                    else
                    {
                        if (horizontalSpacing > 0)
                        {
                            ((IGnosisPanelImplementation)ControlImplementation).AddRowFixedHeight(horizontalSpacing);
                            currentRow++;
                        }

                      //  ((IGnosisPanelImplementation)ControlImplementation).AddRowFixedHeight(fieldRowHeight);
                      ((IGnosisPanelImplementation)ControlImplementation).AddRowAutoHeight(); //field row
                    }

                  

                }

                //bool placed = false;
                //while (!placed)
                //{
                //    if (currentCol > numCols - 1)
                //    {
                //        currentRow++;
                //        //rowSpan++;
                //        currentCol = 0;
                //    }
                //    else
                //    {
                //        placed = true;
                //    }
                //}



                //update used cells and lastRowUsed. Check if implementation needs more rows
                //int oldLastRowUsed = lastRowUsed;
                //UpdateUsedCells(currentCol, currentRow, child.ColSpan, rowSpan);
                //if (lastRowUsed > oldLastRowUsed)
                //{
                //    int difference = lastRowUsed - oldLastRowUsed; //the number of rows we need to add
                //    for (int i = 0; i < difference; i++)
                //    {
                //        if (captionPosition == CaptionPosition.Above || captionPosition == CaptionPosition.Below)
                //        {
                //            ((IGnosisPanelImplementation)ControlImplementation).AddRowAutoHeight(); //caption row
                //            ((IGnosisPanelImplementation)ControlImplementation).AddRowFixedHeight(fieldRowHeight); //field row
                //        }
                //        else
                //        {
                //            ((IGnosisPanelImplementation)ControlImplementation).AddRowFixedHeight(fieldRowHeight);
                //        }
                //    }
                //}

                //determine layout row numbers and spans
                int fieldRowNo;
                int fieldRowSpan;
                int captionRowNo;

                if (captionPosition == CaptionPosition.ABOVE || captionPosition == CaptionPosition.BELOW)
                {
                    //account for caption row
                    captionRowNo = currentRow * 2;
                    fieldRowNo = captionRowNo + 1;
                    fieldRowSpan = rowSpan * 2 - 1; //1 row for caption
                }
                else
                {
                    captionRowNo = currentRow;
                    fieldRowNo = currentRow;
                    fieldRowSpan = rowSpan;
                }

                //caption label
                if (!(child is GnosisButtonController || child is GnosisCheckFieldController))
                {
                    //create label
                  //  string caption = ((IGnosisContentControlImplementation)child.ControlImplementation).Caption;
                  //  GnosisCaptionLabel captionLabel = new GnosisCaptionLabel();
                  ////  IGnosisCaptionLabelImplementation captionLabelImp = GlobalData.Singleton.ImplementationCreator.GetGnosisCaptionLabelImplementation();
                  //  GlobalData.Singleton.StyleHelper.ApplyCaptionStyle(captionLabel, EntityController.GetCaptionStyle());
                  //  captionLabel.Caption = caption;

                    ((IGnosisPanelImplementation)ControlImplementation).AddGnosisCaptionLabel(captionLabel, currentCol, captionRowNo, child.ColSpan, 1);
                }

                ((IGnosisPanelImplementation)ControlImplementation).AddGnosisContentControlImplementation((IGnosisContentControlImplementation)child.ControlImplementation, currentCol, fieldRowNo, child.ColSpan, fieldRowSpan);

                //if (child is GnosisTextFieldController)
                //{
                //    ((IGnosisPanelImplementation)ControlImplementation).AddGnosisContentControlImplementation((IGnosisTextFieldImplementation)child.ControlImplementation, currentCol, fieldRowNo, child.ColSpan, fieldRowSpan);
                //}
                //else if (child is GnosisComboFieldController)
                //{
                //    ((IGnosisPanelImplementation)ControlImplementation).AddGnosisContentControlImplementation((IGnosisComboFieldImplementation)child.ControlImplementation, currentCol, fieldRowNo, child.ColSpan, fieldRowSpan);
                //}
                //else if (child is GnosisCheckFieldController)
                //{
                //    ((IGnosisPanelImplementation)ControlImplementation).AddGnosisContentControlImplementation((IGnosisCheckFieldImplementation)child.ControlImplementation, currentCol, fieldRowNo, child.ColSpan, fieldRowSpan);
                //}
                //else if (child is GnosisDateFieldController)
                //{
                //    ((IGnosisPanelImplementation)ControlImplementation).AddGnosisContentControlImplementation((IGnosisDateFieldImplementation)child.ControlImplementation, currentCol, fieldRowNo, child.ColSpan, fieldRowSpan);
                //}
                //else if (child is GnosisDateTimeFieldController)
                //{
                //    ((IGnosisPanelImplementation)ControlImplementation).AddGnosisDateTimeFieldImplementation((IGnosisDateTimeFieldImplementation)child.ControlImplementation, currentCol, fieldRowNo, child.ColSpan, fieldRowSpan);
                //}
                //else if (child is GnosisButtonController)
                //{
                //    ((IGnosisPanelImplementation)ControlImplementation).AddGnosisContentControlImplementation((IGnosisButtonImplementation)child.ControlImplementation, currentCol, fieldRowNo, child.ColSpan, fieldRowSpan);
                //}


                //previousStartCol = colNo;
                //previousStartRow = controlRowNo;
                //previousColSpan = colSpan;
                //previousRowSpan = controlRowSpan;

                currentCol = currentCol + child.ColSpan;

                //if (numCols == 1)
                //{
                //    currentRow = currentRow + rowSpan;
                //}

            }//foreach

            laidOut = true;
        }



        private void UpdateUsedCells(int col, int row, int colSpan, int rowSpan)
        {

            //First fill in spaces occupied by this control
            for (int c = col; c  < col + colSpan; c++)
            {
                for (int r = row; r < row + rowSpan; r++)
                {
                    usedCells[c, r] = 1;
                }
            }

            //update lastRowUsed
            bool found = false;
            int currentRow = row + rowSpan - 1;
            //at the end of while loop current row is the first empty row
            while (!found)
            {
                for (int c = 0; c < usedCells.GetLength(0); c++)
                {
                    if (usedCells[c,currentRow] == 1)
                    {
                        currentRow = currentRow + 1;
                        if (currentRow > usedCells.GetLength(1))
                        {
                            GlobalData.Singleton.ErrorHandler.HandleError("All rows used in panel", "GnosisPanelController", 2);
                            found = true;
                        }
                        break;
                    }
                    else if (c == usedCells.GetLength(0) - 1) 
                    {
                        //we have scanned the whole column and it is empty
                        found = true;
                    }
                }
            }
            lastRowUsed = currentRow - 1;
        
        }

        internal override GnosisController FindControllerByID(int controlID)
        {
            GnosisController controller = null;

            foreach (GnosisPanelFieldController child in childControllers)
            {
                if (child.ControlImplementation.ID == controlID)
                {
                    controller = child;
                    break;
                }
            }

            return controller;
        }

        internal override void ShowTooltips()
        {
            base.ShowTooltips();

            foreach (GnosisContentController child in childControllers)
            {
                child.ShowTooltips();
            }
        }

        internal override void HideTooltips()
        {
            base.HideTooltips();

            foreach (GnosisContentController child in childControllers)
            {
                child.HideTooltips();
            }
        }

        //private Point FindNextRightPosition(int startCol, int startRow, int colSpan, int rowSpan, int previousRepeatDepth)
        //{
        //    bool accomplished = false;
        //    bool impossible = false;
        //    int currentCol = startCol;
        //    int currentRow = startRow;

        //    if (rowSpan > repeatDepth)
        //    {
        //        previousRepeatDepth = repeatDepth;
        //        repeatDepth = rowSpan;
        //    }

        //    while (!accomplished && !impossible)
        //    {
        //        if (usedCells[currentCol, currentRow] == 0)
        //        {//if1
        //            //this space is available, but check above
        //            //if (currentCol != 0 && currentRow - 1 >= repeatDepthStart && usedCells[currentCol, currentRow - 1] == 0)
        //            //{
        //            //    currentRow = currentRow - 1;
        //            //}
        //            //no space above
        //            //else
        //            //{//else1
        //                //check available columns
        //                if (currentCol + colSpan <= currentPanelPresentation.Columns)
        //                {//if2
        //                    //columns are available. check rows
        //                    bool rowsAvailable = false;
        //                    if (currentRow + rowSpan <= repeatDepthStart + repeatDepth)
        //                    {
        //                        //rows are available
        //                        rowsAvailable = true;
        //                    }
        //                    else
        //                    {//else2
        //                        //rows are not available. 
        //                        if (currentCol != 0)
        //                        {
        //                            currentCol = 0;
        //                            currentRow = lastRowUsed + 1;
        //                        }

        //                        //restart repeatDepthStart
        //                        repeatDepthStart = currentRow;
        //                        previousRepeatDepth = repeatDepth;
        //                        if (currentPanelPresentation.RepeatCellDepth == 0)
        //                        {
        //                            repeatDepth = currentPanelPresentation.RowFactor;
        //                        }
        //                        else
        //                        {
        //                            repeatDepth = currentPanelPresentation.RepeatCellDepth;
        //                        }
        //                   }//else2


        //                    if (rowsAvailable)
        //                    {
        //                        accomplished = true;
        //                    }
        //                    else
        //                    {
        //                        currentCol++;
        //                    }
        //                }//if2
        //                else
        //                {
        //                    //not enough columns
        //                    currentCol++;
        //                }
        //          //  }//else1

        //        }//if1
        //        else
        //        {
        //            currentCol++;
        //        }

        //        if (currentCol > usedCells.GetLength(0) - 1)
        //        {
        //            currentCol = 0;
        //            currentRow = currentRow + previousRepeatDepth;
        //            //restart repeatDepthStart
        //            repeatDepthStart = currentRow;
        //            previousRepeatDepth = repeatDepth;
        //            if (currentPanelPresentation.RepeatCellDepth == 0)
        //            {
        //                repeatDepth = currentPanelPresentation.RowFactor;
        //            }
        //            else
        //            {
        //                repeatDepth = currentPanelPresentation.RepeatCellDepth;
        //            }
        //        }
        //        if (currentRow > usedCells.GetLength(1) - 1)
        //        {
        //            impossible = true;
        //            GlobalData.Instance.ErrorHandler.HandleError("Exceeded row limit", "GnosisPanelController", 2);
        //        }

        //    }//end while



        //    if (accomplished)
        //    {
        //    else
        //    {
        //        return new Point(currentCol, currentRow);
        //    }
        //        return new Point(-1, -1);
        //    }
        //}


        //private Point FindNextDownPosition(int startCol, int startRow, int colSpan, int rowSpan)
        //{
        //    bool accomplished = false;
        //    bool impossible = false;
        //    int currentCol = startCol;
        //    int currentRow = startRow;

        //    if (rowSpan > repeatDepth)
        //    {
        //        repeatDepth = rowSpan;
        //    }


        //    while (!accomplished && !impossible)
        //    {
        //        if (usedCells[currentCol, currentRow] == 0)
        //        {
        //            //check if columns are available 
        //            if (currentCol + colSpan <= currentPanelPresentation.Columns)
        //            {
        //                //columns are available. check rows
        //                bool rowsAvailable = false;
        //                if (currentRow + rowSpan <= repeatDepthStart + repeatDepth)
        //                {
        //                    //rows are available
        //                    rowsAvailable = true;
        //                }
        //                else
        //                {
        //                    //rows are not available. 
        //                    if (currentCol != 0)
        //                    {
        //                        currentCol = 0;
        //                        currentRow = lastRowUsed + 1;
        //                    }

        //                    //restart repeatDepthStart
        //                    repeatDepthStart = currentRow;
        //                    previousRepeatDepth = repeatDepth;
        //                    if (currentPanelPresentation.RepeatCellDepth == 0)
        //                    {
        //                        repeatDepth = currentPanelPresentation.RowFactor;
        //                    }
        //                    else
        //                    {
        //                        repeatDepth = currentPanelPresentation.RepeatCellDepth;
        //                    }
        //                }

        //                if (rowsAvailable)
        //                {
        //                    accomplished = true;
        //                }
        //                else
        //                {
        //                    currentCol++;
        //                }
        //            }
        //            else
        //            {
        //                currentCol++;
        //            }
        //        }

        //        if (currentCol > usedCells.GetLength(0) - 1)
        //        {
        //            currentCol = 0;
        //            currentRow = currentRow + 1;
        //        }
        //        if (currentRow > usedCells.GetLength(1) - 1)
        //        {
        //            impossible = true;
        //            GlobalData.Instance.ErrorHandler.HandleError("Exceeded row limit", "GnosisPanelController", 2);
        //        }

        //    }//end while

        //    if (accomplished)
        //    {
        //        return new Point(currentCol, currentRow);
        //    }
        //    else
        //    {
        //        return new Point(-1, -1);
        //    }

        //}


        public override void LoadData()
        {
            foreach (GnosisContentController child in childControllers.Where(c => !(c is GnosisButtonController)))
            {
                child.LoadData(0);
            }
        }


        internal void SetLayoutParameters(int _numCols, int _numSections, double _totalWidth)
        {
            numCols = _numCols;
            numSections = _numSections;
            initialWidth = _totalWidth;
        }

        private void Child_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
             
                if(e.PropertyName.Equals("Width") )
                {
                  // Refresh();
                }
        }

        internal override void SizeChanged()
        {
            double width = GetWidth();
            WidthChanged(width);
        }

        //public override void ChangePresentationFormat(GnosisFramePresentation framePresentation)
        //{
        //    currentFramePresentation = framePresentation;

        //    foreach (GnosisPanelPresentation panelPresentation in ((GnosisPanel)ControlImplementation).PanelPresentations)
        //    {
        //        if (panelPresentation.Format == framePresentation.Format)
        //        {
        //            currentPanelPresentation = panelPresentation;
        //            ((IGnosisPanelImplementation)ControlImplementation).Clear();
        //            ((IGnosisPanelImplementation)ControlImplementation).SetNumColumns(currentPanelPresentation.Columns);
        //            Setup();
        //            LoadData();
        //        }
        //    }
        //}


    }
}
