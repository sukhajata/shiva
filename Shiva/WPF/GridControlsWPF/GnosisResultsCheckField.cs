using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public partial class GnosisResultsCheckField : GnosisGridCheckField, IGnosisResultsCheckFieldImplementation
    {
        protected Action GotMouseFocusHandler;
        protected Action LostMouseFocusHandler;
        protected Action MouseDownHandler;
        protected Action MouseUpHandler;

       

        public void SetMouseDownHandler(Action action)
        {
            MouseDownHandler = action;
            this.MouseDown += GnosisCheckResultsFieldWPF_MouseDown;
        }

        private void GnosisCheckResultsFieldWPF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseDownHandler.Invoke();
        }

        public void SetMouseUpHandler(Action action)
        {
            MouseUpHandler = action;
            this.MouseUp += GnosisCheckResultsFieldWPF_MouseUp;
        }

        private void GnosisCheckResultsFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseUpHandler.Invoke();
        }

        public void SetGotMouseFocusHandler(Action action)
        {
            GotMouseFocusHandler = action;
            this.MouseEnter += GnosisCheckResultsFieldWPF_MouseEnter;
        }

        private void GnosisCheckResultsFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            GotMouseFocusHandler.Invoke();
        }

        public void SetLostMouseFocusHandler(Action action)
        {
            LostMouseFocusHandler = action;
            this.MouseLeave += GnosisCheckResultsFieldWPF_MouseLeave;

        }

        private void GnosisCheckResultsFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LostMouseFocusHandler.Invoke();
        }
    }
}
