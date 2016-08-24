using Shiva.Shared.BaseControllers;
using GnosisControls;

namespace Shiva.Shared.GenericControllers
{
    public class GnosisGenericMenuItemController_Search : GnosisGenericMenuItemController
    {

        public GnosisGenericMenuItemController_Search(GnosisGenericMenuItem _menuItem, GnosisController _parentController)
            :base(_menuItem, _parentController)
        {

        }

        protected override void Setup()
        {
            base.Setup();

         //   GlobalData.Singleton.PropertyChanged += GlobalData_PropertyChanged;
        }

        //private void GlobalData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals("CurrentFrameController"))
        //    {
        //        if (GlobalData.Singleton.CurrentFrameController is GnosisSearchFrameController)
        //        {
        //            this.Disabled = false;
        //        }
        //        else
        //        {
        //            this.Disabled = true;
        //        }
        //    }
        //}


    }
}
