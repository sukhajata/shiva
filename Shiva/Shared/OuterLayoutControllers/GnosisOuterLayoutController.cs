using ShivaShared3.ContainerControllers;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using GnosisControls;
using ShivaShared3.BaseControllers;


namespace ShivaShared3.OuterLayoutControllers
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
