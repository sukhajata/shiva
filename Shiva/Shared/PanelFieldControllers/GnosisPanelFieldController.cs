using Shiva.Shared.ContentControllers;
using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.Interfaces;
using GnosisControls;
using Shiva.Shared.DataControllers;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.Data;

namespace Shiva.Shared.PanelFieldControllers
{
    public class GnosisPanelFieldController : GnosisContentController
    {
        public GnosisPanelFieldController(
            IGnosisPanelFieldImplementation panelField,
           // IGnosisPanelFieldImplementation panelFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisInnerLayoutController parent)
            :base (panelField, instanceController, parent)
        {
            SetDisplayDimensions();

            ((IGnosisPanelFieldImplementation)ControlImplementation).SetHorizontalAlignment(HorizontalAlignmentType.LEFT);

            instanceController.PropertyChanged += InstanceController_PropertyChanged;

        }



        public GnosisPanelFieldController(
            IGnosisPanelFieldImplementation panelField,
          //  IGnosisPanelFieldImplementation panelFieldImplementation,
            GnosisInstanceController instanceController,
            GnosisPanelFieldController parent)
            : base(panelField, instanceController, parent)
        {
            SetDisplayDimensions();

            ((IGnosisPanelFieldImplementation)ControlImplementation).SetHorizontalAlignment(HorizontalAlignmentType.LEFT);

            instanceController.PropertyChanged += InstanceController_PropertyChanged;

        }

        private void InstanceController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Deleted":
                    ((IGnosisPanelFieldImplementation)ControlImplementation).DatasetDeleted = InstanceController.Deleted;
                    break;
            }
        }

        //protected override void Initialize()
        //{
        //    base.Initialize();


        //    ((IGnosisPanelFieldImplementation)ControlImplementation).SetHorizontalAlignment(HorizontalAlignmentType.LEFT);

        //    string font = EntityController.GetNormalStyle().Font;
        //    int fontSize = EntityController.GetNormalStyle().FontSize;

        //    if (MinDisplayChars > 0)
        //    {
        //        //minFieldWidth = StyleManager.GetMinFieldWidth(MinDisplayChars);
        //        minFieldWidth = GlobalData.Singleton.StyleHelper.GetMinFieldWidth((IGnosisContentControlImplementation)ControlImplementation, font, fontSize, MinDisplayChars);
        //    }

        //    if (MaxDisplayChars > 0)
        //    {
        //        //maxFieldWidth = StyleManager.GetMaxFieldWidth(MaxDisplayChars);
        //        maxFieldWidth = GlobalData.Singleton.StyleHelper.GetMaxFieldWidth((IGnosisContentControlImplementation)ControlImplementation, font, fontSize, MaxDisplayChars);
        //    }

        //    fieldHeight = GlobalData.Singleton.StyleHelper.GetFieldHeight((IGnosisPanelFieldImplementation)ControlImplementation, font, fontSize);


        //}




    }
}
