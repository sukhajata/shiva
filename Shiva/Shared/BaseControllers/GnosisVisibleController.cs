
using GnosisControls;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.Data;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Events;
using Shiva.Shared.GenericControllers;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shiva.Shared.BaseControllers
{
    public class GnosisVisibleController : GnosisController
    {

      //  protected StyleController styleManager;
        private GnosisInstanceController instanceController;
        private GnosisEntityController entityController;
        //private IGnosisMouseVisibleControlImplementation ControlImplementation;
        private GnosisVisibleController parent;
        private bool editable;

        //public StyleController StyleManager
        //{
        //    get { return styleManager; }
        //}

        //public string Caption
        //{
        //    get { return ((GnosisVisibleControl)ControlImplementation).Caption; }
        //    set { ((GnosisVisibleControl)ControlImplementation).Caption = value; }
        //}

        //public bool HasFocus
        //{
        //    get { return ((GnosisVisibleControl)ControlImplementation).HasFocus; }
        //    set
        //    {
        //        ((GnosisVisibleControl)ControlImplementation).HasFocus = value;
        //        OnPropertyChanged("HasFocus");
        //    }
        //}

        //public bool HasMouseFocus
        //{
        //    get { return ((GnosisVisibleControl)ControlImplementation).HasMouseFocus; }
        //    set
        //    {
        //        ((GnosisVisibleControl)ControlImplementation).HasMouseFocus = value;
        //        OnPropertyChanged("HasMouseFocus");
        //    }
        //}

        //public bool HasMouseDown
        //{
        //    get { return ((GnosisVisibleControl)ControlImplementation).HasMouseDown; }
        //    set
        //    {
        //        ((GnosisVisibleControl)ControlImplementation).HasMouseDown = value;
        //        OnPropertyChanged("HasMouseDown");
        //    }
        //}

        public GnosisInstanceController InstanceController
        {
            get { return instanceController; }
        }

        public GnosisEntityController EntityController
        {
            get { return entityController; }
        }

        //public IGnosisMouseVisibleControlImplementation ControlImplementation
        //{
        //    get { return ControlImplementation; }
        //}



        public GnosisVisibleController Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public bool Editable
        {
            get { return editable; }
            set
            {
                editable = value;
                OnPropertyChanged("Editable");
            }
        }

        public enum GnosisVerticalScrollbarVisibility
        {
            Visible,
            Hidden,
            Auto
        }

        public GnosisVisibleController(
            IGnosisVisibleControlImplementation control,
            // IGnosisMouseVisibleControlImplementation controlImplementation,
            GnosisEntityController _entityController,
            GnosisVisibleController _parent)
            : base(control)
        {
            // ControlImplementation = controlImplementation;
            entityController = _entityController;
            parent = _parent;
            if (parent != null)
            {
                control.GnosisParent = (IGnosisVisibleControlImplementation)_parent.ControlImplementation;
            }
            Initialize();
        }


        public GnosisVisibleController(
            IGnosisVisibleControlImplementation control,
          //  IGnosisMouseVisibleControlImplementation controlImplementation,
            GnosisInstanceController _instanceController,
            GnosisVisibleController _parent)
            :base(control)
        {
            instanceController = _instanceController;
            entityController = instanceController.EntityController;
            // ControlImplementation = controlImplementation;
            parent = _parent;
            if (parent != null)
            {
                control.GnosisParent = (IGnosisVisibleControlImplementation)_parent.ControlImplementation;
            }
            Initialize();
        }

        protected virtual void Initialize()
        {
           // ControlImplementation.SetController(this);

            //set properties
            //if (((GnosisVisibleControl)ControlImplementation).ToolTip != null && ((GnosisVisibleControl)ControlImplementation).ToolTip.Length > 0)
            //{
            //    ControlImplementation.SetTooltip(((GnosisVisibleControl)ControlImplementation).ToolTip);
            //}

            SetupGenericEvents();

            //pass event handlers to implementation
            //ControlImplementation.SetMouseDownHandler(new Action(OnMouseDown));
            //ControlImplementation.SetMouseUpHandler(new Action(OnMouseUp));
            //ControlImplementation.SetGotMouseFocusHandler(new Action(OnGotMouseFocus));
            //ControlImplementation.SetLostMouseFocusHandler(new Action(OnLostMouseFocus));
            ((IGnosisVisibleControlImplementation)ControlImplementation).SetGotFocusHandler(new Action(OnGotFocus));
            ((IGnosisVisibleControlImplementation)ControlImplementation).SetLostFocusHandler(new Action(OnLostFocus));

            //apply styles
            GlobalData.Singleton.StyleHelper.ApplyStyle((IGnosisVisibleControlImplementation)ControlImplementation, EntityController.GetNormalStyle());

            //if (InstanceController != null)
            //{
            //    //styleManager = new StyleController(instanceController.EntityController.GetNormalStyle(), this);
            //    GlobalData.Singleton.StyleHelper.ApplyStyle(ControlImplementation, this, EntityController.GetNormalStyle());
            //}

            this.PropertyChanged += GnosisVisibleController_PropertyChanged;
        }


        protected virtual void GnosisVisibleController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
          
        }

        /// <summary>
        /// Find the Generic Control for this ControlType. For each type of Event defined there, enable that event
        /// by passing a handler to the instantiating class.
        /// </summary>
        private void SetupGenericEvents()
        {
            //if there is a generic control for this type, get the events defined in the generic control and activate them in the implementation
            GnosisGenericControlController genericControlController = GlobalData.Singleton.FindGenericControllerByType(ControlImplementation.ControlType);
            if (genericControlController != null)
            {
                var eventTypes = genericControlController.EventHandlers.Select(x => x._EventType).Distinct().ToList();

                foreach (GnosisEventHandler.GnosisEventType type in eventTypes)
                {
                    EnableGenericEvent(type);
                }
            }
        }

        protected virtual void EnableGenericEvent(GnosisEventHandler.GnosisEventType eventType)
        {
            //switch (eventType)
            //{
                //case GnosisEventHandler.GnosisEventType.Click:
                //    ControlImplementation.EnableClickEvent(new Action(OnClick));
                //    break;
                //case GnosisEventHandler.GnosisEventType.MouseOver:
                //    ControlImplementation.EnableMouseOverEvent(new Action(OnMouseOver));
                //    break;
                //case GnosisEventHandler.GnosisEventType.RightClick:
                //    ControlImplementation.EnableRightClickEvent(new Action(OnRightClick));
                //    break;

           // }
        }

        //protected virtual int GetIconHeight()
        //{
        //    GnosisStyle style = EntityController.GetNormalStyle();
        //    double textHeight = GlobalData.Singleton.StyleHelper.GetTextHeight((IGnosisVisibleControlImplementation)ControlImplementation,
        //        style.Font, style.FontSize);
        //    double iconHeight = 2 * ((IGnosisPaddingPossessor)ControlImplementation).VerticalPadding + textHeight;

        //    return (int)iconHeight;

        //}


        //internal void SetBackgroundColour(string backgroundColour)
        //{
        //    ControlImplementation.SetBackgroundColour(backgroundColour);
        //}



        //internal void SetBorderColour(string controlColour)
        //{
        //    ControlImplementation.SetBorderColour(controlColour);
        //}

        //internal void SetHorizontalPadding(double paddingHorizontal)
        //{
        //    ((IGnosisVisibleControlImplementation)ControlImplementation).SetPaddingHorizontal(paddingHorizontal);
        //}

        //internal void SetVerticalPadding(double paddingVertical)
        //{
        //    ((IGnosisVisibleControlImplementation)ControlImplementation).SetPaddingVertical(paddingVertical);
        //}

        public virtual void OnMouseDown()
        {
           // this.HasMouseDown = true;
        }

        public virtual void OnMouseUp()
        {
            //this.HasMouseDown = false;
        }

        public virtual void OnGotMouseFocus()
        {
           // this.HasMouseFocus = true;
            //if (editMode && this is GnosisContentController)
            //{
            //    SetOutlineColour("FFC107");
            //}
        }

        public virtual void OnLostMouseFocus()
        {
           // this.HasMouseFocus = false;
            //if (editMode && this is GnosisContentController)
            //{
            //    RemoveOutlineColour();
            //}
        }

        //private void RemoveOutlineColour()
        //{
        //    ((IGnosisInnerLayoutControlImplementation)ControlImplementation).RemoveOutlineColour();
        //}

        public virtual void OnGotFocus()
        {
           // this.HasFocus = true;
        }

        public virtual void OnLostFocus()
        {
           // this.HasFocus = false;
        }

        public virtual void Undo(object oldState)
        {
            throw new NotImplementedException();
        }

        public virtual void Redo(object oldState)
        {
            throw new NotImplementedException();
        }

        public virtual void LoadData()
        {
            throw new NotImplementedException();
        }

        internal virtual void ShowTooltips()
        {
            ((IGnosisVisibleControlImplementation)ControlImplementation).SetTooltipVisible(true);
        }

        internal virtual void HideTooltips()
        {
            ((IGnosisVisibleControlImplementation)ControlImplementation).SetTooltipVisible(false);
        }


    }
}
