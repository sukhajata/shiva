using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Shiva.Shared.Interfaces;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Data;
using System.Linq;
using Shiva.Shared.ContainerControllers;
using GnosisControls;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.BaseControllers;


namespace Shiva.Shared.OuterLayoutControllers
{
    public class GnosisFrameController : GnosisOuterLayoutController
    {
        protected List<GnosisInnerLayoutController> childControllers;
        protected IGnosisFrameContainer parentController;
        protected GnosisMessageGridController msgGridController;
        protected int numSections;
        protected int numRows;
        protected double optimalSectionWidth;
        protected IGnosisToggleButtonImplementation tabHeaderButton;
       // protected IGnosisButtonImplementation closeButton;

        public List<GnosisInnerLayoutController> ChildControllers
        {
            get { return childControllers; }
        }

        public IGnosisToggleButtonImplementation TabHeaderButton
        {
            get { return tabHeaderButton; }
            set { tabHeaderButton = value; }
        }
        //public double MaxWidth
        //{
        //    get { return ((IGnosisFrameImplementation)ControlImplementation).MaxWidth; }
        //}

        public double OptimalSectionCharacters
        {
            get { return ((IGnosisFrameImplementation)ControlImplementation).OptimalSectionWidthCharacters; }
        }

        public GnosisFrameController(
            IGnosisFrameImplementation frame, 
          //  IGnosisFrameImplementation _frameImplementation,
            GnosisInstanceController instanceController,
            GnosisContainerController parent)
            :base(frame, instanceController, parent)
        {

            frame.SetWidthChangedHandler(new Action<double>(WidthChanged));
            string font = instanceController.EntityController.GetNormalStyle().Font;
            int fontSize = instanceController.EntityController.GetNormalStyle().FontSize;
            optimalSectionWidth = OptimalSectionCharacters * GlobalData.Singleton.StyleHelper.GetCharacterWidth(font, fontSize);
            frame.SetVerticalScrollbarVisibility(BaseControllers.GnosisVisibleController.GnosisVerticalScrollbarVisibility.Auto);

            //layout will be carried out after loading since we need to know available width
            ((IGnosisFrameImplementation)ControlImplementation).SetLoadedHandler(new Action<double>(FrameLoaded));

            frame.PropertyChanged += GnosisFrame_PropertyChanged;
        }

        private void GnosisFrame_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasFocus"))
            {
                if (((IGnosisFrameImplementation)ControlImplementation).HasFocus)
                {
                    GlobalData.Singleton.SystemController.CurrentInstanceController = InstanceController;
                }
                else
                {
                   // GlobalData.Singleton.SystemController.CurrentInstanceController = null;
                }
            }
        }

        
        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                foreach (GnosisInnerLayoutController child in childControllers)
                {
                    child.Editable = this.Editable;
                }
            }
        }


        public void SetEditMode()
        {
            foreach(GnosisInnerLayoutController child in childControllers)
            {
                child.SetEditMode();
            }
        }

        public virtual void Setup()
        {
            childControllers = new List<GnosisInnerLayoutController>();

           // ((IGnosisFrameImplementation)ControlImplementation).SetHorizontalAlignment(HorizontalAlignmentType.Left);
           // ((IGnosisFrameImplementation)ControlImplementation).SetController(this);

            if (((IGnosisFrameImplementation)ControlImplementation).MessageGrids != null && ((IGnosisFrameImplementation)ControlImplementation).MessageGrids[0] != null)
            {
               // IGnosisMessageGridImplementation msgGridImp = GlobalData.Singleton.ImplementationCreator.GetGnosisMessageGridImplementation();
                msgGridController = new GnosisMessageGridController(((IGnosisFrameImplementation)ControlImplementation).MessageGrids[0], InstanceController, this);
                childControllers.Add(msgGridController);
            }

            if (((IGnosisFrameImplementation)ControlImplementation).Grids != null)
            {
                foreach (GnosisGrid grid in ((IGnosisFrameImplementation)ControlImplementation).Grids)
                {
                   // IGnosisGridImplementation gridImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisGridImplementation();
                    GnosisGridController gridController = new GnosisGridController(grid, InstanceController, this);
                    gridController.Setup();

                    childControllers.Add(gridController);
                }
            }

            if (((IGnosisFrameImplementation)ControlImplementation).Panels != null)
            {
                foreach (GnosisPanel panel in ((IGnosisFrameImplementation)ControlImplementation).Panels)
                {
                  //  IGnosisPanelImplementation panelImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisPanelImplementation();
                    GnosisPanelController panelController = new GnosisPanelController(panel, InstanceController, this);
                    panelController.Setup();

                    childControllers.Add(panelController);
                }
            }

            if (((IGnosisFrameImplementation)ControlImplementation).Calendars != null)
            {
                foreach (GnosisCalendar calendar in ((IGnosisFrameImplementation)ControlImplementation).Calendars)
                {
                   // IGnosisCalendarImplementation calendarImp = GlobalData.Singleton.ImplementationCreator.GetGnosisCalendarImplementation();
                    GnosisCalendarController calendarController = new GnosisCalendarController(calendar, InstanceController, this);
                    //calendarController.Setup();

                    childControllers.Add(calendarController);
                }
            }

            if (((IGnosisFrameImplementation)ControlImplementation).TextAreas != null)
            {
                foreach (GnosisTextArea textArea in ((IGnosisFrameImplementation)ControlImplementation).TextAreas)
                {
                  //  IGnosisTextAreaImplementation textAreaImp = GlobalData.Singleton.ImplementationCreator.GetGnosisTextAreaImplementation();
                    GnosisTextAreaController textAreaController = new GnosisTextAreaController(textArea, InstanceController, this);
                }
            }

        }

        internal void SizeChanged()
        {
            foreach (var child in ChildControllers)
            {
                child.SizeChanged();
            }
        }

        public virtual GnosisController FindControllerByID(int controlID)
        {
            GnosisController controller = null;

            foreach (GnosisController child in childControllers)
            {
                if (child.ControlImplementation.ID == controlID)
                {
                    return child;
                }
            }

            foreach (GnosisInnerLayoutController child in childControllers)
            {
                controller = child.FindControllerByID(controlID);

                if (controller != null)
                {
                    break;
                }
            }

            return controller;
        }


        public virtual void FrameLoaded(double width)
        {
            LayoutChildren(width);
            LoadData();
        }

        protected virtual void LayoutChildren(double totalWidthAvailable)
        {
            ((IGnosisFrameImplementation)ControlImplementation).Clear();

            //workout how many sections we can fit horizontally
            int sectionsFit = (int)Math.Round(totalWidthAvailable / optimalSectionWidth);

            // find closest allowable number of sections
            numSections = ((IGnosisFrameImplementation)ControlImplementation)._AllowedSectionList.OrderBy(item => Math.Abs(sectionsFit - item)).First();
            ((IGnosisFrameImplementation)ControlImplementation).SetNumColumns(numSections);
            double sectionWidth = totalWidthAvailable / numSections;

            //layout the content in order
            var orderedContent = childControllers.OrderBy(x => x.ControlImplementation.Order);
            int currentCol = 0;
            int currentRow = 0;
            numRows = 0;

            GnosisInnerLayoutController previousChild = null;
            if (orderedContent.First() is GnosisGridController || orderedContent.First() is GnosisGalleryController)
            {
                ((IGnosisFrameImplementation)ControlImplementation).AddRow();
            }
            else
            {
                ((IGnosisFrameImplementation)ControlImplementation).AddRowAutoHeight();
            }
            numRows++;

            foreach (GnosisInnerLayoutController child in orderedContent)
            {

                bool placed = false;
                int sectionSpan = ((IGnosisInnerLayoutControlImplementation)child.ControlImplementation).MaxSectionSpan;
                //grids default to take up all horizontal space
                if (child is GnosisGridController || child is GnosisMessageGridController || child is GnosisPanelController)
                {
                    if (sectionSpan == 0)
                    {
                        sectionSpan = numSections;
                    }
                }
                else if (sectionSpan == 0)
                {
                    sectionSpan = 1;
                }

                while (!placed)
                {
                   
                    if (currentCol + sectionSpan  <= numSections)
                    {
                        if (currentCol != 0)
                        {
                            //if (verticalSpacing > 0)
                            //{
                            //    ((IGnosisInnerLayoutControlImplementation)child.ControlImplementation).SetMarginLeft(horizontalSpacing);
                            //}
                        }

                        LayoutChild(child, currentCol, currentRow, sectionSpan, 1, sectionSpan * sectionWidth);

                        currentCol = currentCol + sectionSpan;
                        //if (currentCol > numSections - 1)
                        //{
                        //    ((IGnosisFrameImplementation)ControlImplementation).AddRow();
                        //    currentRow++;
                        //    currentCol = 0;
                        //}

                        placed = true;
                    }
                    else if (sectionSpan > numSections)
                    {
                        currentCol = 0;
                        //int rowSpan = (int)Math.Ceiling((double)sectionSpan / (double)numSections);

                        //for (int i = 0; i < rowSpan; i++)
                        //{
                        //    ((IGnosisFrameImplementation)ControlImplementation).AddRow();
                        //}
                        LayoutChild(child, currentCol, currentRow, numSections, 1, numSections * sectionWidth);
                        currentRow = currentRow + 1;

                        placed = true;
                    }
                    else
                    {
                        //We are moving to the next row. Check if there are spare columns we can give to the previous child
                        if (previousChild != null)
                        {
                            int spareCols = numSections - currentCol;
                            ((IGnosisFrameImplementation)ControlImplementation).IncreaseColSpan((IGnosisInnerLayoutControlImplementation)previousChild.ControlImplementation, spareCols);
                        }

                        if (verticalSpacing > 0)
                        {
                            ((IGnosisFrameImplementation)ControlImplementation).AddRowFixedHeight(verticalSpacing);
                            currentRow++;
                        }

                        if (child is GnosisGridController || child is GnosisGalleryController)
                        {
                            ((IGnosisFrameImplementation)ControlImplementation).AddRow(); //star sized, use left over space
                        }
                        else
                        {
                            ((IGnosisFrameImplementation)ControlImplementation).AddRowAutoHeight();
                        }
                        currentRow++;
                        currentCol = 0;
                    }
                }

                previousChild = child;
            }
        }


        public void LayoutChild(GnosisInnerLayoutController child, int col, int row, int colSpan, int rowSpan, double width)
        {
            if (child is GnosisMessageGridController)
            {
                
                IGnosisMessageGridImplementation msgGridImp = (IGnosisMessageGridImplementation)child.ControlImplementation;
                ((IGnosisFrameImplementation)ControlImplementation).LoadMessageGridImplementation(msgGridImp, col, row, colSpan, rowSpan);
            }
            else if (child is GnosisGridController)
            {
                IGnosisGridImplementation gridImp = (IGnosisGridImplementation)child.ControlImplementation;
                ((IGnosisFrameImplementation)ControlImplementation).LoadGrid(gridImp, col, row, colSpan, rowSpan);
              // ((GnosisGridController)child).LayoutRows();
            }
            else if (child is GnosisPanelController)
            {
                IGnosisPanelImplementation panelImp = (IGnosisPanelImplementation)child.ControlImplementation;
                ((GnosisPanelController)child).SetLayoutParameters(colSpan, numSections, width);
                ((IGnosisFrameImplementation)ControlImplementation).LoadPanel(panelImp, col, row, colSpan, rowSpan);

            }
            else if (child is GnosisGalleryController)
            {
                IGnosisGalleryImplementation galleryImp = (IGnosisGalleryImplementation)child.ControlImplementation;
                ((IGnosisNavFrameImplementation)ControlImplementation).LoadGallery(galleryImp, col, row, colSpan, rowSpan );
            }
        }
                 

        protected override void EnableGenericEvent(GnosisEventHandler.GnosisEventType eventType)
        {
            //switch (eventType)
            //{
            //    case GnosisEventHandler.GnosisEventType.GotFrameFocus:
            //        ((IGnosisFrameImplementation)ControlImplementation).SetGotFrameFocusHandler(new Action(GotFrameFocus));
            //        break;
            //    case GnosisEventHandler.GnosisEventType.LostFrameFocus:
            //        ((IGnosisFrameImplementation)ControlImplementation).SetLostFrameFocusHandler(new Action(LostFrameFocus));
            //        break;
            //    default:
            //        base.EnableGenericEvent(eventType);
            //        break;
            //}
            
        }

        public void WidthChanged(double newWidth)
        {
            //workout how many sections we can fit horizontally
            int sectionsFit = (int)Math.Round(newWidth / optimalSectionWidth);

            // find closest allowable number of sections
            int  sectionsAllowed = ((IGnosisFrameImplementation)ControlImplementation)._AllowedSectionList.OrderBy(item => Math.Abs(sectionsFit - item)).First();

            if (sectionsAllowed != numSections)
            {
                LayoutChildren(newWidth);
            }

        }

        internal override void ShowTooltips()
        {
            base.ShowTooltips();

            foreach (GnosisInnerLayoutController child in childControllers)
            {
                child.ShowTooltips();
            }
        }

        internal override void HideTooltips()
        {
            base.HideTooltips();

            foreach (GnosisInnerLayoutController child in childControllers)
            {
                child.HideTooltips();
            }
        }

        public override void LoadData()
        {
            foreach (GnosisInnerLayoutController child in childControllers)
            {
                if (child is GnosisPanelController)
                {
                    child.LoadData();
                }
                //else if (child is GnosisGridController)
                //{
                //    double width = ((IGnosisFrameImplementation)ControlImplementation).GetAvailableWidth();
                //    ((GnosisGridController)child).LayoutRows(width);
                //}
            }
        }

        public void GotFrameFocus()
        {
            //GotFrameFocus EventHandlers are defined in the GenericControl of this type.
            //We find the Generic Control, set this as the current instance and then ask it to handle the event
            //GnosisGenericControlController generic = GlobalData.Instance.FindGenericControllerByType(this.ControlImplementation.ControlType);
            //generic.CurrentInstance = this;
            //generic.HandleGenericEvent(GnosisEventHandler.GnosisEventType.GotFrameFocus, this);


        }

        public void LostFrameFocus()
        {
            //GnosisGenericControlController generic = GlobalData.Instance.FindGenericControllerByType(this.ControlImplementation.ControlType);
            //generic.CurrentInstance = null;
            //generic.HandleGenericEvent(GnosisEventHandler.GnosisEventType.LostFrameFocus, this);

            GlobalData.Singleton.CurrentFrameController = null;

        }

    }
}
