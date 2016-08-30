using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using GnosisControls;

namespace Shiva.Shared.InnerLayoutControllers
{
    public class GnosisResultsRowController : GnosisGridRowController
    {

        public GnosisResultsRowController(GnosisSearchResultsGridController _parent, int _rowNum)
            :base(_parent, _rowNum)
        {

        }

        //protected override void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{

        //    if (e.PropertyName.Equals("HasMouseFocus"))
        //    {
        //        bool hasMouseFocus = ((GnosisResultsGridFieldController)sender).HasMouseFocus;
        //        foreach (GnosisResultsGridFieldController child in Fields)
        //        {
        //            child.HasRowMouseFocus = hasMouseFocus;
        //        }
        //        GlobalData.Singleton.ParentWindowImplementation.WriteToWindow("HasRowMouseFocus - " + rowNum.ToString());
        //    }

        //    base.Cell_PropertyChanged(sender, e);
        //}

        //public override void AddCell(IGnosisGridFieldImplementation resultsFieldImp)
        //{
        //    base.AddCell(resultsFieldImp);

        //    //((IGnosisResultsFieldImplementation)gridFieldImp).SetGotMouseFocusHandler(new Action(OnGotMouseFocus));
        //    //((IGnosisResultsFieldImplementation)gridFieldImp).SetLostMouseFocusHandler(new Action(OnLostMouseFocus));
        //    //((IGnosisResultsFieldImplementation)gridFieldImp).SetMouseDownHandler(new Action(OnMouseDown));
        //    //((IGnosisResultsFieldImplementation)gridFieldImp).SetMouseUpHandler(new Action(OnMouseUp));
        //    resultsFieldImp.PropertyChanged += ResultsFieldImp_PropertyChanged;
        //}

        protected override void GridFieldImp_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.GridFieldImp_PropertyChanged(sender, e);

            IGnosisResultsFieldImplementation resultsField = sender as IGnosisResultsFieldImplementation;
            switch (e.PropertyName)
            {
                case "HasMouseFocus":
                    if (resultsField.HasMouseFocus)
                        OnGotMouseFocus();
                    else
                        OnLostMouseFocus();
                    break;
                case "HasMouseDown":
                    if (resultsField.HasMouseDown)
                        OnMouseDown();
                    else
                        OnMouseUp();
                    break;

            }
        }

        public void OnGotMouseFocus()
        {
            foreach (IGnosisGridFieldImplementation gridField in Fields)
            {
                ((IGnosisResultsFieldImplementation)gridField).HasRowMouseFocus = true;
            }
        }

        public void OnLostMouseFocus()
        {
            foreach (IGnosisGridFieldImplementation gridField in Fields)
            {
                ((IGnosisResultsFieldImplementation)gridField).HasRowMouseFocus = false;
            }
        }

        public void OnMouseDown()
        {
            foreach (IGnosisGridFieldImplementation gridField in Fields)
            {
                ((IGnosisResultsFieldImplementation)gridField).HasRowMouseDown = true;
            }

            //load document
            int searchSystemID = ((GnosisSearchResultsGrid)parent.ControlImplementation).DocumentSystemID;
            int searchEntityID = ((GnosisSearchResultsGrid)parent.ControlImplementation).DocumentEntityID;

            Dictionary<int, string> keys = new Dictionary<int, string>();

            foreach (GnosisSearchResultsAttribute attribute in ((GnosisSearchResultsGrid)parent.ControlImplementation).SearchResultsAttributes)
            {
                string value = parent.InstanceController.GetDataString(attribute.Dataset, attribute.DatasetItem, rowNum);
                switch (attribute._LinkRole)
                {
                    case GnosisSearchResultsAttribute.SearchLinkRole.SYSTEM:
                        searchSystemID = Convert.ToInt32(value);
                        break;
                    case GnosisSearchResultsAttribute.SearchLinkRole.ENTITY:
                        searchEntityID = Convert.ToInt32(value);
                        break;
                    case GnosisSearchResultsAttribute.SearchLinkRole.KEY:
                        keys.Add(attribute.LinkKeyOrder, value);
                        break;
                }
            }

            //GnosisEntityController target = GlobalData.Singleton.SystemController.GetEntityController(searchEntityID, searchSystemID);
            //GnosisInstance instance = GlobalData.Singleton.SystemController.GetInstance(searchEntityID, searchSystemID, "Open", keys);
            //target.Instance = instance;

            GlobalData.Singleton.SystemController.LoadDocument(searchEntityID, searchSystemID, "Open", keys);
        }

        public void OnMouseUp()
        {

            foreach (IGnosisGridFieldImplementation gridField in Fields)
            {
                ((IGnosisResultsFieldImplementation)gridField).HasRowMouseDown = false;
            }
        }

       

    }
}
