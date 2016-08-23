using ShivaShared3.BaseControllers;


namespace ShivaShared3.Events
{
    public class GnosisDocChange
    {
        private GnosisVisibleController controller;
        private object oldState;

        public GnosisVisibleController Controller
        {
            get { return controller; }
        }
        public object OldState
        {
            get { return oldState; }
        }
             
        public GnosisDocChange(GnosisVisibleController _controller, object _oldState)
        {
            controller = _controller;
            oldState = _oldState;
        }
    }
}
