using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.DataControllers;
using ShivaShared3.InnerLayoutControllers;
using ShivaShared3.Data;
using System.Linq;

namespace ShivaShared3.PanelFieldControllers
{
    public class GnosisLinkFieldController : GnosisPanelFieldController
    {
        //public bool PreviouslySelected
        //{
        //    get { return ((GnosisLinkField)ControlImplementation).PreviouslySelected; }
        //    set
        //    {
        //        ((GnosisLinkField)ControlImplementation).PreviouslySelected = value;
        //        OnPropertyChanged("PreviouslySelected");
        //    }
        //}

        public GnosisLinkFieldController(
            GnosisLinkField linkField,
          //  IGnosisLinkFieldImplementation linkFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base(linkField, instanceController, parent)
        {
            //if (linkField.ReadOnly)
            //{
            //    linkFieldImplementation.Locked = true;
            //}

            if (linkField.LinkButton != null)
            {
                IGnosisButtonImplementation linkButtonImplementation = linkField.GetLinkButtonImplementation();
                GlobalData.Singleton.StyleHelper.ApplyStyle(linkButtonImplementation, EntityController.GetNormalStyle());
                ((GnosisLinkField)ControlImplementation).SetClickHandler(new Action(OnClick));

            }
            else if (linkField.LinkMenuButton != null)
            {
                ((GnosisLinkField)ControlImplementation).SetMenuButtonEnabled(true);
                IGnosisMenuButtonImplementation menuButtonImplementation = linkField.GetLinkMenuButtonImplementation();
                GlobalData.Singleton.StyleHelper.ApplyStyle(menuButtonImplementation, EntityController.GetNormalStyle());

                //foreach (GnosisLinkButtonItem buttonItem in linkField.LinkMenuButton.LinkButtonItems)
                //{
                //  //  IGnosisMenuItemImplementation itemImplementation = GlobalData.Singleton.ImplementationCreator.GetGnosisMenuItemImplementation();
                //    GlobalData.Singleton.StyleHelper.ApplyStyle(buttonItem, this, EntityController.GetNormalStyle());
                //    buttonItem.SetClickHandler(new Action(OnClick));
                //    menuButtonImplementation.AddMenuItem(buttonItem);
                //}
            }
        }

        

        public void OnClick()
        {
            ((GnosisLinkField)ControlImplementation).PreviouslySelected = true;

            //get the values of the parameters
            GnosisLinkButton linkButton = ((GnosisLinkField)ControlImplementation).LinkButton;
            var searchParameters = linkButton.SearchParameters;
            foreach (GnosisSearchParameter searchParameter in searchParameters)
            {
                searchParameter.Content = InstanceController.GetDataString(searchParameter.Dataset, searchParameter.DatasetItem, 0);
            }

            //get the target entity
            int searchEntityID = ((GnosisGallerySearchItem)ControlImplementation).SearchEntityID;
            int searchSystemID = ((GnosisGallerySearchItem)ControlImplementation).SearchSystemID;
           // string searchAction =linkButton.SearchAction;
           // string autoSearchAction = Enum.GetName(typeof(GnosisLinkButton.LinkSearchActionType), linkButton.AutoSearchAction);
            GlobalData.Singleton.SystemController.LoadSearch(searchEntityID, searchSystemID, searchParameters, linkButton.SearchAction, linkButton.AutoSearchAction);
        }

        public void SetUnderline(bool underline)
        {
            ((IGnosisLinkFieldImplementation)ControlImplementation).SetUnderline(underline);
        }

        protected override void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Editable"))
            {
                if (!((GnosisLinkField)ControlImplementation).ReadOnly)
                {
                    ((IGnosisLinkFieldImplementation)ControlImplementation).Locked = !Editable;
                }

            }
        }

    }
}
