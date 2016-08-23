using ShivaShared3.ContentControllers;
using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Interfaces;
using GnosisControls;
using ShivaShared3.DataControllers;
using ShivaShared3.InnerLayoutControllers;
using ShivaShared3.Data;

namespace ShivaShared3.PanelFieldControllers
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
