using Shiva.Shared.ContainerControllers;
using Shiva.Shared.Interfaces;
using Shiva.Shared.DataControllers;
using GnosisControls;
using Shiva.Shared.BaseControllers;


namespace Shiva.Shared.OuterLayoutControllers
{
    public class GnosisOuterLayoutController : GnosisVisibleController
    {
        protected int verticalSpacing;
        protected int horizontalSpacing;

        public GnosisOuterLayoutController(
            IGnosisOuterLayoutControlImplementation control,
          //  IGnosisOuterLayoutControlImplementation controlImplementation,
            GnosisInstanceController instanceController,
            GnosisContainerController parent)
            :base (control, instanceController, parent)
        {

        }

        internal void SetVerticalSpacing(int _verticalSpacing)
        {
            verticalSpacing = _verticalSpacing;
        }

        internal void SetHorizontalSpacing(int _horizontalSpacing)
        {
            horizontalSpacing = _horizontalSpacing;
        }
    }
}
