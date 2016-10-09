using System;
using Shiva.Shared.Interfaces;
using Shiva.Shared.DataControllers;
using Shiva.Shared.BaseControllers;
using GnosisControls;
using Shiva.Shared.OuterLayoutControllers;

namespace Shiva.Shared.InnerLayoutControllers
{
    public class GnosisInnerLayoutController : GnosisVisibleController
    {
        protected bool editMode;


        public GnosisInnerLayoutController (
            IGnosisInnerLayoutControlImplementation gnosisLayoutControl, 
            GnosisInstanceController instanceController,
            GnosisOuterLayoutController parent)
            :base(gnosisLayoutControl, instanceController, parent)
        {


        }



        public virtual void SetEditMode()
        {
            editMode = true;
        }

        internal virtual void Save()
        {
            throw new NotImplementedException();
        }

      

        public double GetWidth()
        {
            return ((IGnosisInnerLayoutControlImplementation)ControlImplementation).GetAvailableWidth();
        }

      

        internal virtual GnosisController FindControllerByID(int controlID)
        {
            throw new NotImplementedException();
        }

      

        internal virtual void SetStrikethrough(bool v)
        {
            throw new NotImplementedException();
        }

        internal virtual void SizeChanged()
        {
            throw new NotImplementedException();
        }




    }
}
