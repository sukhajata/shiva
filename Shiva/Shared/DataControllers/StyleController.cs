
//using ShivaShared3.Data;
//using ShivaShared3.GridFieldControllers;
//using ShivaShared3.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Media;
//using ShivaShared3.BaseControllers;


//namespace ShivaShared3.DataControllers
//{
//    public class StyleController
//    {
//        private Data.GnosisStyle style;
//        private GnosisVisibleController controller;
//        private IGnosisCaptionLabelImplementation captionLabel;
//        private Dictionary<string, GnosisStyleCondition> styleChangeProperties;
//        private static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
//        private static string numbers = "0123456789";
//        private string fontFamily;
//        private double fontSize;

//        /// <summary>
//        /// Constructor for Field Style
//        /// </summary>
//        /// <param name="_fieldStyle"></param>
//        /// <param name="_controller"></param>
//        public StyleController(Data.GnosisStyle _fieldStyle, GnosisVisibleController _controller)
//        {
//            style = _fieldStyle;
//            controller = _controller;
//            styleChangeProperties = new Dictionary<string, GnosisStyleCondition>();

//            //register to listen for property changes
//            controller.PropertyChanged += Controller_PropertyChanged;

//            GlobalData.Singleton.StyleHelper.ApplyStyle(controller.ControlImplementation, controller, _fieldStyle);

//            //ApplyNormalStyle(_fieldStyle);
//        }

//        /// <summary>
//        /// Constructor for Caption Style
//        /// </summary>
//        /// <param name="captionStyle"></param>
//        /// <param name="captionLabel"></param>
//        public StyleController(Data.GnosisStyle captionStyle, GnosisVisibleController _controller, IGnosisCaptionLabelImplementation _captionLabel)
//        {
//            style = captionStyle;
//            captionLabel = _captionLabel;

//            ApplyCaptionStyle(captionStyle, _controller);

//        }


//        private void ApplyCaptionStyle(Data.GnosisStyle captionStyle, GnosisVisibleController _controller)
//        {
//            if (captionStyle.Font != null && captionStyle.Font.Length > 0)
//            {
//                captionLabel.SetFont(captionStyle.Font);
//                fontFamily = captionStyle.Font;
//            }

//            if (captionStyle.FontSize > 0)
//            {
//                captionLabel.SetFontSize(captionStyle.FontSize);
//                fontSize = captionStyle.FontSize;
//            }

//            if (captionStyle.BackgroundColour != null && captionStyle.BackgroundColour.Length > 0)
//            {
//                captionLabel.SetBackgroundColour(captionStyle.BackgroundColour);
//            }

//            if (captionStyle.ContentColour != null && captionStyle.ContentColour.Length > 0)
//            {
//                captionLabel.SetForegroundColour(captionStyle.ContentColour);
//            }

//            if (captionStyle.ControlColour != null && captionStyle.ControlColour.Length > 0)
//            {
//                captionLabel.SetBorderColour(captionStyle.ControlColour);
//            }

//            if (captionStyle.HorizontalPadding > 0)
//            {
//                captionLabel.SetPaddingHorizontal(captionStyle.HorizontalPadding);
//            }

//            captionLabel.SetRelativePosition(captionStyle.RelativePosition);


//            foreach (Data.GnosisStyleCondition condition in captionStyle.Conditions)
//            {
//                Type controlType = _controller.ControlImplementation.GetType();

//                if (condition.Property.Equals("ControlType"))
//                {
//                    //find out if this control falls under the inheritance hierarchy of ControlType
//                    Type conditionControlType = ControlTypeMapping.GetControlType(condition.Value);

//                    if (conditionControlType.IsAssignableFrom(controlType))
//                    {
//                        ApplyCaptionStyle(condition, _controller);
//                    }
//                    else
//                    {
//                        GnosisVisibleController parent = _controller.Parent;
//                        while (parent != null)
//                        {
//                            if (conditionControlType.IsAssignableFrom(parent.ControlImplementation.GetType()))
//                            {
//                                ApplyCaptionStyle(condition, _controller);
//                                break;
//                            }
//                            parent = parent.Parent;
//                        }
//                    }
//                }


//            }


//        }
  

//        public void ApplyNormalStyle(Data.GnosisStyle normalStyle)
//        {
            
//            //if (normalStyle.Font != null && normalStyle.Font.Length > 0)
//            //{
//            //    controller.SetFont(normalStyle.Font);
//            //    fontFamily = normalStyle.Font;
//            //}

//            //if (normalStyle.FontSize > 0)
//            //{
//            //    controller.SetFontSize(normalStyle.FontSize);
//            //    fontSize = normalStyle.FontSize;
//            //}



//           // if (normalStyle.BackgroundColour != null && normalStyle.BackgroundColour.Length > 0)
//           // {
//           //     controller.SetBackgroundColour(normalStyle.BackgroundColour);
//           // }

//           // if (normalStyle.ContentColour != null && normalStyle.ContentColour.Length > 0)
//           // {
//           //     controller.SetForegroundColour(normalStyle.ContentColour);
//           // }

//           // if (normalStyle.ControlColour != null && normalStyle.ControlColour.Length > 0)
//           // {
//           //     if (controller is GnosisContentController)
//           //     {
//           //         controller.SetBorderColour(normalStyle.ControlColour);
//           //     }
//           // }

//           // if (normalStyle.IsUnderline)
//           // {
//           //     ((GnosisLinkFieldController)controller).SetUnderline(true);
//           // }

//           // if (normalStyle.HorizontalPadding > 0)
//           // {
//           //     if (controller is GnosisFrameController)
//           //     {
//           //         controller.SetHorizontalPadding(normalStyle.HorizontalPadding);
//           //     }
//           // }

//           // if (normalStyle.VerticalPadding > 0)
//           // {
//           //     if (controller is GnosisFrameController)
//           //     {
//           //         controller.SetVerticalPadding(normalStyle.VerticalPadding);
//           //     }
//           // }

//           // if (normalStyle.VerticalSpacing > 0)
//           // {
//           //     if (controller is GnosisInnerLayoutController)
//           //     {
//           //         ((GnosisInnerLayoutController)controller).SetVerticalSpacing(normalStyle.VerticalSpacing);
//           //     }
//           //     else if (controller is GnosisOuterLayoutController)
//           //     {
//           //         ((GnosisOuterLayoutController)controller).SetVerticalSpacing(normalStyle.VerticalSpacing);
//           //     }
//           // }

//           // if (normalStyle.HorizontalSpacing > 0)
//           // {
//           //     if (controller is GnosisInnerLayoutController)
//           //     {
//           //         ((GnosisInnerLayoutController)controller).SetHorizontalSpacing(normalStyle.HorizontalSpacing);
//           //     }
//           //     else if (controller is GnosisOuterLayoutController)
//           //     {
//           //         ((GnosisOuterLayoutController)controller).SetHorizontalSpacing(normalStyle.HorizontalSpacing);
//           //     }
//           // }

//           //foreach (Data.GnosisStyleCondition condition in normalStyle.Conditions.OrderBy(c => c.Order))
//           // {
//           //     Type controlType = controller.ControlImplementation.GetType();

//           //     if (condition.Property.Equals("ControlType"))
//           //     {
//           //         //find out if this control falls under the inheritance hierarchy of ControlType
//           //         Type conditionControlType = ControlTypeMapping.GetControlType(condition.Value);
//           //         if (conditionControlType.IsAssignableFrom(controlType))
//           //         {
//           //             ApplyNormalStyle(condition);
//           //         }
//           //         else
//           //         {
//           //             GnosisVisibleController parent = controller.Parent;
//           //             while (parent != null)
//           //             {
//           //                 if (conditionControlType.IsAssignableFrom(parent.ControlImplementation.GetType()))
//           //                 {
//           //                     ApplyNormalStyle(condition);
//           //                     break;
//           //                 }
//           //                 parent = parent.Parent;
//           //             }
//           //         }
//           //     }
//           //     else
//           //     {
//           //         //check if the property condition is met
//           //         if (controlType.GetProperty(condition.Property) != null)
//           //         {
//           //             bool propValue = (bool)controlType.GetProperty(condition.Property).GetValue(controller.ControlImplementation);

//           //             bool conditionValue = false;
//           //             if (condition.Value.Equals("1"))
//           //             {
//           //                 conditionValue = true;
//           //             }
//           //             if (propValue == conditionValue)
//           //             {
//           //                 ApplyNormalStyle(condition);
//           //             }

//           //             //add this property to the list of properties to watch out for, if it is not already there
//           //             if (!styleChangeProperties.Keys.Contains(condition.Property))
//           //             {
//           //                 styleChangeProperties.Add(condition.Property, condition);
//           //             }
//           //         }

//           //     }
//           // }
            
//        }


//        private void ApplyDynamicStyle(GnosisStyle normalStyle, string propertyName, string value)
//        {


//        }


//        //private void ApplyCondition(Data.Condition condition, GnosisLayoutController layoutController)
//        //{
//        //    if (condition.BackgroundColour != null && condition.BackgroundColour.Length > 0)
//        //    {
//        //        layoutController.SetBackgroundColour(condition.BackgroundColour);
//        //    }

//        //    if (condition.ContentColour != null && condition.ContentColour.Length > 0)
//        //    {
//        //        layoutController.SetForegroundColour(condition.ContentColour);
//        //    }

//        //    if (condition.ControlColour != null && condition.ControlColour.Length > 0)
//        //    {
//        //        layoutController.SetBorderColour(condition.ControlColour);
//        //    }

//        //    if (condition.IsUnderline)
//        //    {
//        //        ((GnosisLinkFieldController)layoutController).SetUnderline(true);
//        //    }

//        //    if (condition.IsOutline)
//        //    {
//        //        layoutController.SetOutlineColour(condition.OutlineColour);
//        //    }

//        //    if (condition.IsStrikethrough)
//        //    {
//        //        layoutController.SetStrikethrough();
//        //    }

//        //    if (style.PaddingHorizontal > 0)
//        //    {
//        //        layoutController.SetPaddingHorizontal(style.PaddingHorizontal);
//        //    }

//        //    //if (condition.Margin > 0)
//        //    //{
//        //    //    layoutController.SetMargin(condition.Margin);
//        //    //}

//        //    //if (condition.MarginBottom > 0)
//        //    //{
//        //    //    layoutController.SetMarginBottom(condition.MarginBottom);
//        //    //}

//        //    if (condition.Conditions != null && condition.Conditions.Count > 0)
//        //    {
//        //        foreach (Data.Condition condition2 in condition.Conditions)
//        //        {
//        //            if (condition2.Property.Equals("ControlType"))
//        //            {
//        //                //find out if this control falls under the inheritance hierarchy of ControlType
//        //                Type type1 = ControlTypeMapping.GetControlType(condition2.Value);
//        //                Type type2 = layoutController.GetType();
//        //                if (type2.IsAssignableFrom(type1))
//        //                {
//        //                    ApplyCondition(condition2, layoutController);
//        //                }
//        //            }
//        //            else
//        //            {
//        //                //check if the property condition is met
//        //                var propValue = layoutController.GetType().GetProperty(condition2.Property).GetValue(layoutController);

//        //                if (propValue.Equals(condition2.Value))
//        //                {
//        //                    ApplyCondition(condition2, layoutController);
//        //                }

//        //                //add this property to the list of properties to watch out for, if it is not already there
//        //                if (!styleChangeProperties.Contains(condition.Property))
//        //                {
//        //                    styleChangeProperties.Add(condition.Property);
//        //                }

//        //            }
//        //        }
//        //    }

//        //}

//        private void Controller_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//        {
//            if (styleChangeProperties.Keys.Contains(e.PropertyName))
//            {
//                //If condition is met, apply just that condition. Otherwise apply all the styles
//                //This is the only way to undo a dynamic style
//                GnosisVisibleController visibleController = (GnosisVisibleController)sender;
//                Type controlType = visibleController.GetType();
//                GlobalData.Singleton.ParentWindowImplementation.WriteToWindow(e.PropertyName + " - " + controlType.GnosisName);
//                GnosisStyleCondition condition = styleChangeProperties[e.PropertyName];
//                bool propValue = (bool)controlType.GetProperty(condition.Property).GetValue(controller);

//                bool conditionValue = false;
//                if (condition.Value.Equals("1"))
//                {
//                    conditionValue = true;
//                }
//                if (propValue == conditionValue)
//                {
//                    ApplyNormalStyle(condition);
//                }
//                else
//                {
//                    ApplyNormalStyle(style);
//                }

//            }
//        }

//        public double GetFieldHeight()
//        {

//            var formattedText = new FormattedText(
//                   alphabet,
//                   CultureInfo.CurrentUICulture,
//                   FlowDirection.LeftToRight,
//                   new Typeface(fontFamily),
//                   fontSize,
//                   Brushes.Black);

//            double height =  formattedText.Height;

//            if (height < 25)
//            {
//                height = 25;
//            }

//            return height;
//        }


//        /// <summary>
//        /// Get the average width of a numeric character.
//        /// </summary>
//        /// <returns></returns>
//        public double GetCharacterWidthNumeric()
//        {
//            var formattedText = new FormattedText(
//                  numbers,
//                  CultureInfo.CurrentUICulture,
//                  FlowDirection.LeftToRight,
//                  new Typeface(fontFamily),
//                  fontSize,
//                  Brushes.Black);

//            double numberWidth = formattedText.Width / numbers.Length;

//            return numberWidth;
//        }

//        /// <summary>
//        /// Get the average width of a single alphanumeric character.
//        /// </summary>
//        /// <returns></returns>
//        public static double GetCharacterWidth(FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch )
//        {

//            var formattedText = new FormattedText(
//                   alphabet,
//                   CultureInfo.CurrentUICulture,
//                   FlowDirection.LeftToRight, 
//                   new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
//                   fontSize,
//                   Brushes.Black);

//            double characterWidth = formattedText.Width / alphabet.Length;

//            return characterWidth;

//        }

//        public double GetCharacterWidth()
//        {

//            var formattedText = new FormattedText(
//                    alphabet,
//                    CultureInfo.CurrentUICulture,
//                    FlowDirection.LeftToRight,
//                    new Typeface(fontFamily),
//                    fontSize,
//                    Brushes.Black);

//            double characterWidth = formattedText.Width / alphabet.Length;

//            return characterWidth + (characterWidth * 0.2);
//        }

//        public double GetMinFieldWidth(int minChars)
//        {
//            double characterWidth = GetCharacterWidth();
//            double padding = 2 * GetPaddingHorizontal();
//            if (padding == 0)
//            {
//                padding = 5;
//            }
//            double minWidth = minChars * characterWidth + padding;

//            return minWidth + characterWidth;//buffer
            
//        }

//        internal double GetMaxFieldWidth(int maxCharacters)
//        {
//            double characterWidth = GetCharacterWidth();
//            double padding = 2 * GetPaddingHorizontal();
//            if (padding == 0)
//            {
//                padding = 5;
//            }
//            double maxWidth = maxCharacters * characterWidth + padding;

//            return maxWidth + characterWidth;
//        }

//        public double GetPaddingHorizontal()
//        {
//            double padding;

//            if (controller != null)
//            {
//                padding = controller.ControlImplementation.GetPaddingHorizontal();
//            }
//            else
//            {
//                padding = captionLabel.GetPaddingHorizontal();
//            }

//            return padding;

//        }


//    }
//}
