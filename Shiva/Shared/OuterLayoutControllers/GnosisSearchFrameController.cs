using ShivaShared3.ContainerControllers;
using ShivaShared3.Data;
using ShivaShared3.DataControllers;
using ShivaShared3.InnerLayoutControllers;
using ShivaShared3.BaseControllers;
using ShivaShared3.Interfaces;
using GnosisControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShivaShared3.OuterLayoutControllers
{
    public class GnosisSearchFrameController : GnosisFrameController
    {
        private bool autoSearch;
        private List<GnosisInstance> searches;

        public GnosisSearchFrameController(
            GnosisSearchFrame searchFrame,
          //  IGnosisSearchFrameImplementation searchFrameImplementation,
            GnosisInstanceController instanceController,
            GnosisContainerController parent)
            :base(searchFrame, instanceController, parent)
        {
            searchFrame.SetVerticalScrollbarVisibility(GnosisVisibleController.GnosisVerticalScrollbarVisibility.Hidden);
            searches = new List<GnosisInstance>();
        }


        public override void Setup()
        {
            base.Setup();

           // IGnosisGridImplementation searchResultsGridImp = GlobalData.Singleton.ImplementationCreator.GetGnosisGridImplementation();
            GnosisSearchResultsGridController searchResultsGridController = new GnosisSearchResultsGridController(((GnosisSearchFrame)ControlImplementation).SearchResultsGrid, InstanceController, this);
            searchResultsGridController.Setup();
            searchResultsGridController.PropertyChanged += ResultsGridController_PropertyChanged;
            childControllers.Add(searchResultsGridController);

            GnosisPanelController panelController = (GnosisPanelController)childControllers.Find(c => c is GnosisPanelController);
            panelController.PropertyChanged += PanelController_PropertyChanged;
        }

        private void PanelController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Height"))
            {
                SetGridHeight();
            }
        }

        internal void SetAutoSearch(bool _autoSearch)
        {
            autoSearch = _autoSearch;
        }

        public override void FrameLoaded(double width)
        {
            if (autoSearch)
            {
                GnosisInstance results = GlobalData.Singleton.SystemController.GetInstance(InstanceController.Instance);//   .DatabaseConnection.GetGnosisInstance(.InstanceRetriever.GetInstance(EntityController.Instance);
                InstanceController.Instance = results;
            }

            base.FrameLoaded(width);

        }

        public void Search()
        {
            foreach (GnosisInnerLayoutController child in childControllers.Where(c => c is GnosisPanelController))
            {
                child.Save();
            }
            searches.Add(InstanceController.Instance);
            
            GnosisInstance results = GlobalData.Singleton.SystemController.GetInstance(InstanceController.Instance);
            InstanceController.Instance = results;

            double width = ((IGnosisFrameImplementation)ControlImplementation).GetAvailableWidth();
            ShowResults();//LayoutChildren(width);
        }

        private void ShowResults()
        {
            GnosisSearchResultsGridController grid = (GnosisSearchResultsGridController)childControllers.Find(c => c is GnosisSearchResultsGridController);

            if (grid != null)
            {
                grid.CreateFields();
                grid.LayoutRows();
                
            }


        }

        private void ResultsGridController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("LayoutCompleted"))
            {

                //SetGridHeight();
            }
        }


        private void SetGridHeight()
        {
            //set the height of the results grid to the space not occupied by other children
            double frameHeight = ((IGnosisFrameImplementation)ControlImplementation).GetAvailableHeight() - (((IGnosisFrameImplementation)ControlImplementation).GetPaddingVertical() * 2);

            double gridHeight = frameHeight;

            foreach (GnosisInnerLayoutController child in childControllers.Where(c => !(c is GnosisSearchResultsGridController)))
            {
                double height = ((IGnosisInnerLayoutControlImplementation)child.ControlImplementation).GetHeight() + verticalSpacing;
                gridHeight = gridHeight - height;
            }

            GnosisSearchResultsGridController grid = (GnosisSearchResultsGridController)childControllers.Find(c => c is GnosisSearchResultsGridController);
            //((IGnosisGridImplementation)grid.ControlImplementation).SetHeight(gridHeight);

        }


    }
}
