using System;
using System.Collections.Generic;
using System.Text;
using ShivaShared3.Data;
using ShivaShared3.Interfaces;
using ShivaShared3.Utility;
using ShivaShared3.BaseControllers;
using GnosisControls;

namespace ShivaShared3.GenericControllers
{
    public class GnosisGenericMenuGroupController : GnosisGenericMenuItemController
    {
        public List<GnosisGenericToggleMenuItemController> ToggleMenuItemControllers;
        private string variableName;

        //public int VariableSystemID
        //{
        //    get { return ((GnosisGenericMenuGroup)ControlImplementation).VariableSystemID; }
        //}

        //public int VariableControlID
        //{
        //    get { return ((GnosisGenericMenuGroup)ControlImplementation).VariableControlID; }
        //}

        public GnosisGenericMenuGroupController(
            GnosisGenericMenuGroup menuGroup,
            GnosisController parent)
            :base(menuGroup, parent)
        {
        }

        protected override void Setup()
        {
            //content is derived from 
            ToggleMenuItemControllers = new List<GnosisGenericToggleMenuItemController>();
            string value = GlobalData.Singleton.SystemController.GetEnvironmentVariableValue(
                ((GnosisGenericMenuGroup)ControlImplementation).VariableSystemID, 
                ((GnosisGenericMenuGroup)ControlImplementation).VariableControlID);
            int code = Convert.ToInt32(value);
            //GnosisEnvironmentVariable variable = GlobalData.Singleton.SystemController.GetEnvironmentVariable(VariableSystemID, VariableControlID);
            //variableName = variable.GnosisName;
            //GlobalData.Singleton.SystemController.PropertyChanged += SystemController_PropertyChanged;

            List<GnosisDataItem> values = GlobalData.Singleton.SystemController.GetDataItemList(
                ((GnosisGenericMenuGroup)ControlImplementation).VariableSystemID,
                ((GnosisGenericMenuGroup)ControlImplementation).VariableControlID);

            foreach (GnosisDataItem dataItem in values)
            {
                GnosisGenericToggleMenuItem toggleItem = GnosisControlCreator.CreateGenericToggleMenuItem(
                    dataItem.GnosisName, 
                    dataItem.Order, 
                    dataItem.GnosisIcon, 
                    dataItem.Tooltip,
                    ((GnosisGenericMenuGroup)ControlImplementation).VariableSystemID,
                    ((GnosisGenericMenuGroup)ControlImplementation).VariableControlID, 
                    dataItem.Code);

                ((GnosisGenericMenuGroup)ControlImplementation).GnosisAddChild(toggleItem);

                GnosisGenericToggleMenuItemController controller = new GnosisGenericToggleMenuItemController(toggleItem, this);
                toggleItem.PropertyChanged += Child_PropertyChanged;
                ToggleMenuItemControllers.Add(controller);
                if (toggleItem.Code == code)
                {
                    toggleItem.Active = true;
                }
                else
                {
                    toggleItem.Active = false;
                }
             }
        }

        private void Child_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Active"))
            {
                GnosisGenericToggleMenuItem child = sender as GnosisGenericToggleMenuItem;
                
                if (child.Active)
                {
                    GlobalData.Singleton.SystemController.SetEnvironmentVariable(
                        ((GnosisGenericMenuGroup)ControlImplementation).VariableSystemID,
                        ((GnosisGenericMenuGroup)ControlImplementation).VariableControlID, 
                        child.Code.ToString());

                    foreach (GnosisGenericToggleMenuItemController toggleItemController in ToggleMenuItemControllers)
                    {
                        if (toggleItemController.ControlImplementation != child)
                        {
                            ((GnosisGenericToggleMenuItem)toggleItemController.ControlImplementation).Active = false;
                        }
                    }

                }
            }
        }


        //private void SystemController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals(variableName))
        //    {

        //    }
        //}
    }
}
