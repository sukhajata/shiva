using System;
using System.Linq;

using ShivaShared3.BaseControllers;


using ShivaShared3.Data;
using ShivaShared3.GenericControllers;
using System.Collections.Generic;
using ShivaShared3.Events;
using ShivaShared3.Interfaces;

namespace GnosisControls
{
    public class GnosisEventHandler : GnosisControl
    {
        private List<GnosisEventHandlerSource> gnosisEventHandlerSources;

        private GnosisEventType eventTypeField = GnosisEventType.None;

        private string eventTypeString;

        private string sourceExpressionField;

        private int targetControlEntityIDField;

        private int targetControlControlIDField;

        private string targetActionString;

        private TargetActionType targetActionField = TargetActionType.None;

        private string targetEventString;

        private TargetEventType targetEventField = TargetEventType.None;

        private string targetPropertyString;

        private TargetPropertyType targetPropertyField = TargetPropertyType.None;

        public static string EVENT_TYPE_ATTRIBUTE_NAME = "EventType";
        public static string SOURCE_EXPRESSION_ATTRIBUTE_NAME = "SourceExpression";
        public static string TARGET_CONTROL_ENTITY_ID_ATTRIBUTE_NAME = "TargetControlEntityID";
        public static string TARGET_CONTROL_CONTROL_ID_ATTRIBUTE_NAME = "TargetControlControlID";
        public static string TARGET_ACTION_ATTRIBUTE_NAME = "TargetAction";
        public static string TARGET_PROPERTY_ATTRIBUTE_NAME = "TargetProperty";

        //public static string SELECT_EVENT_TYPE = "Select";
        //public static string CLICK_EVENT_TYPE = "Click";
        //public static string DBL_CLICK_EVENT_TYPE = "DblClick";
        //public static string RIGHT_CLICK_EVENT_TYPE = "RightClick";
        //public static string MOUSE_OVER_EVENT_TYPE = "MouseOver";
        //public static string LOAD_EVENT_TYPE = "Load";
        //public static string VALIDATE_EVENT_TYPE = "Validate";
        //public static string ROW_SELECT_EVENT_TYPE = "RowSelect";
        //public static string GOT_FRAME_FOCUS_EVENT_TYPE = "GotFrameFocus";
        //public static string LOST_FRAME_FOCUS_EVENT_TYPE = "LostFrameFocus";
        //public static string GOT_TOOLBAR_FOCUS_EVENT_TYPE = "GotToolbarFocus";
        //public static string LOST_TOOLBAR_FOCUS_EVENT_TYPE = "LostToolbarFocus";
        //public static string CHANGE_VISIBILITY_EVENT_TYPE = "ChangeVisibility";
        //public static string CHANGE_ORIENTATION_EVENT_TYPE = "ChangeOrientation";
        //public static string SELECT_MENU_ITEM_EVENT_TYPE = "SelectMenuItem";

        public enum GnosisEventType
        {
            None,
            Select,
            Click,
            DblClick,
            RightClick,
            MouseOver,
            Load,
            Validate,
            RowSelect,
            GotFrameFocus,
            LostFrameFocus,
            GotToolbarFocus,
            LostToolbarFocus,
            ChangeVisibilty,
            ChangeOrientation,
            SelectMenuItem
        }


        public enum TargetActionType
        {
            None,
            Hide,
            Show,
            Enable,
            Disable,
            Redraw, //docFrame, toolbar
            SetEmpty, //remove any data
            Get, //docFrame
            New, //docFrame
            Delete, //delete the instance in the docframe
            Save, //docFrame
            TabClose,
            ModalClose,
            Execute, //frame. run a process
            Reset, //searchFrame
            Search, //searchFrame
            NewRow, //GnosisGrid
            DeleteRow, //GnosisGrid
            NewNode, //GnosisTree
            DeleteNode, //GnosisTree
            NewEvent,
            DeleteEvent
        }

        public enum TargetEventType
        {
            None,
            GotFrameFocus
        }

        public enum TargetPropertyType
        {
            None,
            Name, ID, SystemName, SystemVersion, URL, EntityName, EntityID, EntityType, Icon, IconSize, HasFocus, IsEditing, IsEmpty,
            Created, Updated, Deleted, SQLSuccessful, UserName, UserID, IsProtected, ShowTooltips, HostName, DefaultLayout, Device,
            Font, ReadOnlyBGFieldColour, EditBGFieldColour, WindowBGFieldColour, FieldFontSize, FieldBold, CreatedColour, UpdatedColour,
            DeletedColour, TitleFontSize, TitleBold, SubtitleFontSize, SubtitleBold, PromptFontSize, PromptBold, TabCaptionFontSize, TabCaptionBold,
            GridCaptionFontSize, GridCaptionBold, DepressedBackgroundColour, ToolbarNaviIconSize, TileTabIconSize, DefaultIconSize, Order, Caption,
            Tooltip, CaptionRelativePosition, LabelType, MinWidth, MaxWidth, MaxPrintWidth, WidthProportion, Rows, Columns, Row, Column, RowSpan,
            ColumnSpan, MaxLines, Alignment, Type, SQLType, Prec, Scale, Default, Depressed, MultipleRowSelection, ShowOnChildWindow, Orientation,
            Menu, TilesOverlayParent, HasTabs, ExpandToLevel, Expanded, Direction, TreeRole, CalendarRole, ControlType, ControlCondition, Visible,
            Locked, LockIgnoreProtection, Enabled, AllVisible, Disabled, Hidden
        }

  


        public void HandleEvent(GnosisController source)
        {
            try
            {
                bool result = false;
                //if no source expression is defined then the event should be fired
                if (this.SourceExpression == null)
                {
                    result = true;
                }
                else if (this.SourceExpression.Equals("True"))
                {
                    result = true;
                }
                else if (this.SourceExpression.Equals("False"))
                {
                    result = false;
                }
                else //boolean sequence
                {
                    GnosisBooleanHelper booleanHelper = new GnosisBooleanHelper(this, (GnosisControl)source.ControlImplementation);
                    result = booleanHelper.Evaluate(this.SourceExpression);
                }

                //Either TargetProperty, TargetAction or TargetEvent is defined
                //Find the target control
                //if the target is a GnosisGenericControl, the real target will be the type of the control that has current focus
                GnosisController target = GlobalData.Singleton.FindController(this.TargetControlEntityID, this.TargetControlControlID);
                GnosisController realTarget;
                if (target is GnosisGenericControlController)
                {
                    realTarget = ((GnosisGenericControlController)target).CurrentInstance;
                }
                else
                {
                    realTarget = target;
                }

                if (realTarget != null)
                {
                   // EventLogger.LogEvent(this, realTarget.ControlImplementation.GnosisName, result);

                    if (this._TargetAction != GnosisEventHandler.TargetActionType.None)
                    {
                        if (result)
                        {
                            realTarget.ExecuteAction(this._TargetAction);
                        }
                    }
                    else if (this._TargetProperty != GnosisEventHandler.TargetPropertyType.None)
                    {
                        //set property to SourceExpression result
                        string propName = Enum.GetName(typeof(GnosisEventHandler.TargetPropertyType), this.TargetProperty);
                        realTarget.GetType().GetProperty(propName).SetValue(realTarget, result, null);
                    }
                    else if (this._TargetEvent != TargetEventType.None)
                    {
                        if (result)
                        {
                            //If TargetEvent is defined, we must find the matching EventHandler(s) in the GenericControl (target)
                            //and invoke them on the CurrentInstance of the GenericControl (realTarget)
                            var targetEventHandlers = ((GnosisGenericControlController)target).EventHandlers
                                                   .Where(x => Enum.GetName(typeof(GnosisEventType), x.EventType).Equals(Enum.GetName(typeof(TargetEventType), this.TargetEvent)));

                            foreach (GnosisEventHandler eh in targetEventHandlers)
                            {
                                eh.HandleEvent(realTarget);
                            }
                        }
                    }
                    else
                    //target must be a variable
                    {

                        GlobalData.Singleton.ErrorHandler.HandleError("TargetProperty, TargetAction and TargetEvent not found", "GnosisEventHandler.HandleEvent()");
                    }
                    
                }
                else
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("TargetControl not found for GnosisEventHandler " + this.GnosisName,
                        "GnosisEventHandler.HandleEvent");
                }

            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }
        }






        [GnosisCollection]
        public List<GnosisEventHandlerSource> GnosisEventHandlerSources
        {
            get
            {
                return this.gnosisEventHandlerSources;
            }
            set
            {
                this.gnosisEventHandlerSources = value;
            }
        }

        [GnosisProperty]
        public string EventType
        {
            get
            {
                return this.eventTypeString;
            }
            set
            {
                try
                {
                    eventTypeField = (GnosisEventType)Enum.Parse(typeof(GnosisEventType), value);
                    eventTypeString = value;
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        
        public GnosisEventType _EventType
        {
            get { return eventTypeField; }
        }

        [GnosisProperty]
        public string TargetEvent
        {
            get
            {
                return this.targetEventString;
            }
            set
            {
                if (this.targetPropertyField != TargetPropertyType.None || this.targetActionField != TargetActionType.None)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("Can not set TargetEvent since TargetProperty or TargetAction already set. Should be only one..", "GnosisEventHandler.TargetEvent.set");
                }
                try
                {
                    targetEventField = (TargetEventType)Enum.Parse(typeof(TargetEventType), value);
                    this.targetEventString = value;
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        
        public TargetEventType _TargetEvent
        {
            get { return targetEventField; }
        }

        [GnosisProperty]
        public string SourceExpression
        {
            get
            {
                return this.sourceExpressionField;
            }
            set
            {
                this.sourceExpressionField = value;
            }
        }

        [GnosisProperty]
        public int TargetControlEntityID
        {
            get
            {
                return this.targetControlEntityIDField;
            }
            set
            {
                this.targetControlEntityIDField = value;
            }
        }

        [GnosisProperty]
        public int TargetControlControlID
        {
            get
            {
                return this.targetControlControlIDField;
            }
            set
            {
                this.targetControlControlIDField = value;
            }
        }

        [GnosisProperty]
        public string TargetAction
        {
            get
            {
                return targetActionString;
            }
            set
            {
                
                if (this.targetPropertyField != TargetPropertyType.None || this.targetEventField != TargetEventType.None)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("Can not set TargetAction since TargetProperty or TargetEvent already set. Should be only one.", "GnosisEventHandler.TargetAction.set");
                }
                try
                {
                    targetActionField = (TargetActionType)Enum.Parse(typeof(TargetActionType), value);
                    targetActionString = value;
                }
                catch(Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public TargetActionType _TargetAction
        {
            get { return targetActionField; }
        }


        [GnosisProperty]
        public string TargetProperty
        {
            get
            {
                return targetPropertyString;
            }
            set
            {
                targetPropertyString = value;

                if (this.targetActionField != TargetActionType.None || this.targetEventField != TargetEventType.None)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError("Can not set TargetProperty since TargetAction or TargetEvent already set. Should be only one.", "GnosisEventHandler.TargetProperty.set");
                }
                try
                {
                    targetPropertyField = (TargetPropertyType)Enum.Parse(typeof(TargetPropertyType), value);
                }
                catch(Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public TargetPropertyType _TargetProperty
        {
            get { return targetPropertyField; }
        }

        public override void GnosisAddChild(IGnosisObject child)
        {
            gnosisEventHandlerSources.Add((GnosisEventHandlerSource)child);
        }

        //public void HandleEvent(GnosisControl control)
        //{
        //    try
        //    {
        //        bool result = false;
        //        if (this.SourceExpression == null)
        //        {
        //            result = true;
        //        }
        //        else if (this.SourceExpression.Equals("True"))
        //        {
        //            result = true;
        //        }
        //        else if (this.SourceExpression.Equals("False"))
        //        {
        //            result = false;
        //        }
        //        else //boolean sequence
        //        {
        //            GnosisBooleanHelper booleanHelper = new GnosisBooleanHelper(this, control);
        //            result = booleanHelper.Evaluate(this.SourceExpression);
        //        }

        //        //Either TargetProperty or TargetAction is defined
        //        //if the target is a GnosisGenericControl, the real target will be the GnosisGenericControl.ControlType that has current focus
        //        GnosisControl target = GlobalData.Instance.FindControl(this.TargetControlEntityID, this.TargetControlControlID);
        //        if (target is GnosisGenericControl)
        //        {
        //            target = ((GnosisGenericControl)target).CurrentInstance;
        //        }
        //        if (target != null)
        //        {
        //            if (this.TargetAction != GnosisEventHandler.TargetActionType.None)
        //            {
        //                target.ExecuteAction(this.TargetAction);
        //            }
        //            else if (this.TargetProperty != GnosisEventHandler.TargetPropertyType.None)
        //            {
        //                string propName = Enum.GetName(typeof(GnosisEventHandler.TargetPropertyType), this.TargetProperty);
        //                target.GetType().GetProperty(propName).SetValue(target, result, null);
        //            }
        //            else
        //            {
        //                GlobalData.Instance.ErrorHandler.HandleError("TargetProperty and TargetAction both empty", "GnosisEventHandler.HandleEvent()");
        //            }
        //        }
        //        else
        //        {
        //            GlobalData.Instance.ErrorHandler.HandleError("TargetControl not found for GnosisEventHandler " + this.GnosisName,
        //                "GnosisEventHandler.HandleEvent");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        GlobalData.Instance.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //    }
        //}

        //public GnosisControl FindControlByID(int controlID)
        //{
        //    if (this.ID == controlID)
        //    {
        //        return this;
        //    }
        //    else
        //    {
        //        if (GnosisEventHandlerSources != null)
        //        {
        //            foreach (GnosisEventHandlerSource source in GnosisEventHandlerSources)
        //            {
        //                if (source.ID == controlID)
        //                {
        //                    return source;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //}

    }
}