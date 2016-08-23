using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShivaShared3.Interfaces;

namespace ShivaWPF3.GridControlsWPF
{
    public class GnosisDateResultsFieldWPF : GnosisGridDateFieldWPF, IGnosisDateResultsFieldImplementation
    {
        protected Action GotMouseFocusHandler;
        protected Action LostMouseFocusHandler;
        protected Action MouseDownHandler;
        protected Action MouseUpHandler;

        private bool hasRowMouseFocus;
        private bool hasRowMouseDown;

        public bool HasRowMouseFocus
        {
            get { return hasRowMouseFocus; }
            set
            {
                hasRowMouseFocus = value;
                OnPropertyChanged("HasRowMouseFocus");
            }
        }
        public bool HasRowMouseDown
        {
            get { return hasRowMouseDown; }
            set
            {
                hasRowMouseDown = value;
                OnPropertyChanged("HasRowMouseDown");
            }
        }

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
            this.MouseUp += GnosisDateResultsFieldWPF_MouseUp;
        }

        private void GnosisDateResultsFieldWPF_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseUpHandler.Invoke();
        }

        public void SetGotMouseFocusHandler(Action action)
        {
            GotMouseFocusHandler = action;
            this.MouseEnter += GnosisDateResultsFieldWPF_MouseEnter;
        }

        private void GnosisDateResultsFieldWPF_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            GotMouseFocusHandler.Invoke();
        }

        public void SetLostMouseFocusHandler(Action action)
        {
            LostMouseFocusHandler = action;
            this.MouseLeave += GnosisDateResultsFieldWPF_MouseLeave;

        }

        private void GnosisDateResultsFieldWPF_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LostMouseFocusHandler.Invoke();
        }
    }
}
