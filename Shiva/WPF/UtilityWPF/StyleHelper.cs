using GnosisControls;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.ContentControllers;
using Shiva.Shared.Data;
using Shiva.Shared.DataControllers;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.Interfaces;
using Shiva.Shared.OuterLayoutControllers;
using Shiva.Shared.PanelFieldControllers;
using Shiva.Shared.ToolbarControllers;
using Shiva.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using UtilityWPF.TriggerTracing;

namespace ShivaWPF3.UtilityWPF
{
    public class StyleHelper : IGnosisStyleHelper
    {
        private static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        private static string numbers = "0123456789";
        //private Dictionary<string, string> gnosisWindowsPropertyMap = new Dictionary<string, string>
        //{
        //    {"Selected" }
        //};
        //private string fontFamily;
        // private double fontSize;

        public static SolidColorBrush GetBrushFromHex(string hex)
        {
            return (SolidColorBrush)new BrushConverter().ConvertFrom("#" + hex);
        }

        public void ApplyFontStyle(IGnosisContentControlImplementation control, GnosisStyle gnosisStyle)
        {
            Style windowsStyle = new Style();

            if (gnosisStyle.Font != null && gnosisStyle.Font.Length > 0)
            {
                Setter setter = new Setter(Control.FontFamilyProperty, new FontFamily(gnosisStyle.Font));
                windowsStyle.Setters.Add(setter);
            }

            if (gnosisStyle.FontSize > 0)
            {
                Setter setter = new Setter(Control.FontSizeProperty, (double)gnosisStyle.FontSize);
                windowsStyle.Setters.Add(setter);
            }

            ((FrameworkElement)control).Style = windowsStyle;
        }

        public void ApplyCaptionStyle(IGnosisCaptionLabelImplementation captionLabel, GnosisStyle captionStyle)
        {
            if (captionLabel is GnosisGridHeaderField)
            {
                int i = 1;
            }

            FrameworkElement wpfControl = (FrameworkElement)captionLabel;
            if (wpfControl.Style == null)
            {
                wpfControl.Style = new Style();
            }
            Style windowsStyle = new Style(wpfControl.GetType(), wpfControl.Style);

            if (captionStyle.Font != null && captionStyle.Font.Length > 0)
            {
                //captionLabel.SetFont(captionStyle.Font);
                //fontFamily = captionStyle.Font;
                Setter setter = new Setter(Control.FontFamilyProperty, new FontFamily(captionStyle.Font));
                windowsStyle.Setters.Add(setter);
            }

            if (captionStyle.FontSize > 0)
            {
                //captionLabel.SetFontSize(captionStyle.FontSize);
                Setter setter = new Setter(Control.FontSizeProperty, (double)captionStyle.FontSize);
                windowsStyle.Setters.Add(setter);
            }

            if (captionStyle.BackgroundColour != null && captionStyle.BackgroundColour.Length > 0)
            {
                //captionLabel.SetBackgroundColour(captionStyle.BackgroundColour);
                Setter setter = new Setter(Control.BackgroundProperty, GetBrushFromHex(captionStyle.BackgroundColour));
                windowsStyle.Setters.Add(setter);
            }

            if (captionStyle.ContentColour != null && captionStyle.ContentColour.Length > 0)
            {
                //captionLabel.SetForegroundColour(captionStyle.ContentColour);
                Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(captionStyle.ContentColour));
                windowsStyle.Setters.Add(setter);
            }

            if (captionStyle.ControlColour != null && captionStyle.ControlColour.Length > 0)
            {
                //captionLabel.SetBorderColour(captionStyle.ControlColour);
                Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(captionStyle.ControlColour));
                windowsStyle.Setters.Add(setter);
            }

            if (captionStyle.HorizontalPadding > 0)
            {
                captionLabel.HorizontalPadding = captionStyle.HorizontalPadding;
            }

            if (captionStyle.VerticalPadding > 0)
            {
                captionLabel.VerticalPadding = captionStyle.VerticalPadding;
            }

            if (captionStyle.HorizontalMargin > 0)
            {
                captionLabel.HorizontalMargin = captionStyle.HorizontalMargin;
            }

            if (captionStyle.VerticalMargin > 0)
            {
                captionLabel.VerticalMargin = captionStyle.VerticalMargin;
            }

            if (captionStyle.CaptionSpacing > 0)
            {
                captionLabel.CaptionSpacing = captionStyle.CaptionSpacing;
            }

            if (captionStyle.ControlThickness > 0)
            {
                captionLabel.ControlThickness = captionStyle.ControlThickness;
                //Setter setter = GetControlThicknessSetter(captionLabel, captionStyle);
                //windowsStyle.Setters.Add(setter);
            }

            
            captionLabel.RelativePosition = captionStyle._CaptionRelativePosition;
            wpfControl.Style = windowsStyle;
          //  string xaml = XamlWriter.Save(windowsStyle);
        }

        

        public void ApplyStyle(IGnosisVisibleControlImplementation control, GnosisStyle gnosisStyle)
        {
            FrameworkElement wpfControl = (FrameworkElement)control;
            if (wpfControl.Style == null)
            {
                wpfControl.Style = new Style();
            }
            //Style windowsStyle = new Style();
            Style windowsStyle = new Style(wpfControl.GetType(), wpfControl.Style);

            //Setter overrideStyleSetter = new Setter(Control.OverridesDefaultStyleProperty, true);
            //windowsStyle.Setters.Add(overrideStyleSetter);

            if (gnosisStyle.Font != null && gnosisStyle.Font.Length > 0)
            {
                Setter setter = new Setter(Control.FontFamilyProperty, new FontFamily(gnosisStyle.Font));
                windowsStyle.Setters.Add(setter);
                //wpfControl.Style.Setters.Add(setter);
            }
            if (gnosisStyle.FontSize > 0)
            {
                Setter setter = new Setter(Control.FontSizeProperty, (double)gnosisStyle.FontSize);
                windowsStyle.Setters.Add(setter);
                //wpfControl.Style.Setters.Add(setter);
            }

            if (gnosisStyle.BackgroundColour != null && gnosisStyle.BackgroundColour.Length > 0)
            {
                Setter setter = new Setter(Control.BackgroundProperty, GetBrushFromHex(gnosisStyle.BackgroundColour));
                windowsStyle.Setters.Add(setter);
                //wpfControl.Style.Setters.Add(setter);
            }

            if (gnosisStyle.ContentColour != null && gnosisStyle.ContentColour.Length > 0)
            {
                Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(gnosisStyle.ContentColour));
                windowsStyle.Setters.Add(setter);
                //wpfControl.Style.Setters.Add(setter);
            }

            if (gnosisStyle.ContainerHorizontalPadding > 0 && control is IContainerPaddingPossessor)
            {
                ((IContainerPaddingPossessor)control).ContainerHorizontalPadding = gnosisStyle.ContainerHorizontalPadding;
            }

            if (gnosisStyle.ContainerVerticalPadding > 0 && control is IContainerPaddingPossessor)
            {
                ((IContainerPaddingPossessor)control).ContainerVerticalPadding = gnosisStyle.ContainerVerticalPadding;
            }


            if (gnosisStyle.GnosisBorderThickness > 0 && control is IGnosisBorderThicknessPossessor)
            {
                if (control is GnosisSearchFrame)
                {
                    Setter setter = new Setter(GnosisSearchFrame.GnosisBorderThicknessProperty, gnosisStyle.GnosisBorderThickness);
                    windowsStyle.Setters.Add(setter);
                }
                else if (control is GnosisFrame)
                {
                    Setter setter = new Setter(GnosisFrame.GnosisBorderThicknessProperty, gnosisStyle.GnosisBorderThickness);
                    windowsStyle.Setters.Add(setter);
                }
                else if (control is GnosisGallery)
                {
                    Setter setter = new Setter(GnosisGallery.GnosisBorderThicknessProperty, gnosisStyle.GnosisBorderThickness);
                    windowsStyle.Setters.Add(setter);
                }
                else if (control is GnosisToolbarTray)
                {
                    Setter setter = new Setter(GnosisToolbarTray.GnosisBorderThicknessProperty, gnosisStyle.GnosisBorderThickness);
                    windowsStyle.Setters.Add(setter);
                }

            }

            if (gnosisStyle.ControlColour != null && gnosisStyle.ControlColour.Length > 0)
            {
                if (control is IGnosisControlThicknessPossessor || control is IGnosisBorderThicknessPossessor)
                {
                    Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisStyle.ControlColour));
                    windowsStyle.Setters.Add(setter);
                    //wpfControl.Style.Setters.Add(setter);

                    //Setter setter2 = new Setter(Control.BorderThicknessProperty, 1);
                    //windowsStyle.Setters.Add(setter2);
                }
            }


            if (control is IGnosisControlThicknessPossessor)
            {
                Setter setter = GetControlThicknessSetter((IGnosisControlThicknessPossessor)control, gnosisStyle);
                windowsStyle.Setters.Add(setter);
            }


            if (gnosisStyle.OutlineColour != null && gnosisStyle.OutlineColour.Length > 0)
            {
                Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisStyle.OutlineColour));
                windowsStyle.Setters.Add(setter);
                //wpfControl.Style.Setters.Add(setter);
            }

            if (gnosisStyle.IsUnderline)
            {
                //if (controller is GnosisLinkFieldController)
                //{

                //    Setter setter = new Setter(TextBox.TextDecorationsProperty, TextDecorations.Underline);
                //    windowsStyle.Setters.Add(setter);

                //    //wpfControl.Style.Setters.Add(setter);
                //}

                if (control is GnosisLinkField)
                {
                    Setter setter = new Setter(TextBox.TextDecorationsProperty, TextDecorations.Underline);
                    windowsStyle.Setters.Add(setter);

                }
                //((GnosisLinkFieldController)controller).SetUnderline(true);
            }

            if (gnosisStyle.HorizontalPadding > 0)
            {
                if (control is IGnosisPaddingPossessor)
                {
                    ((IGnosisPaddingPossessor)control).HorizontalPadding = gnosisStyle.HorizontalPadding;
                }
               // if (controller is GnosisFrameController)
               // {
                    //controller.SetHorizontalPadding(gnosisStyle.HorizontalPadding);
                    //Border b = (Border)control;
                    //Setter setter = new Setter(Border.PaddingProperty, new Thickness(b.Padding.Left, gnosisStyle.HorizontalPadding, b.Padding.Right, gnosisStyle.HorizontalPadding));
                    //windowsStyle.Setters.Add(setter);
               // }
            }

            if (gnosisStyle.VerticalPadding > 0)
            {
                if (control is IGnosisPaddingPossessor)
                {
                    ((IGnosisPaddingPossessor)control).VerticalPadding = gnosisStyle.VerticalPadding;
                }
              //  if (controller is GnosisFrameController)
                //{
                    //controller.SetVerticalPadding(gnosisStyle.VerticalPadding);
                    //Border b = (Border)control;
                    //Setter setter = new Setter(Border.PaddingProperty, new Thickness(gnosisStyle.VerticalPadding, b.Padding.Top, gnosisStyle.VerticalPadding, b.Padding.Bottom));
                    //windowsStyle.Setters.Add(setter);
                //}
            }

            if (gnosisStyle.HorizontalMargin > 0 )
            {
                if (control is IGnosisMarginPossessor)
                {
                    ((IGnosisMarginPossessor)control).HorizontalMargin = gnosisStyle.HorizontalMargin;
                }
            }

            if (gnosisStyle.VerticalMargin > 0)
            {
                if (control is IGnosisMarginPossessor)
                {
                    ((IGnosisMarginPossessor)control).VerticalMargin = gnosisStyle.VerticalMargin;
                }
            }

            if (gnosisStyle.VerticalSpacing > 0)
            {
                if (control is IGnosisSpacingPossessor)
                {
                    ((IGnosisSpacingPossessor)control).VerticalSpacing = gnosisStyle.VerticalSpacing;
                }

                //if (controller is GnosisInnerLayoutController)
                //{
                //    ((GnosisInnerLayoutController)controller).SetVerticalSpacing(gnosisStyle.VerticalSpacing);
                //}
                //else if (controller is GnosisOuterLayoutController)
                //{
                //    ((GnosisOuterLayoutController)controller).SetVerticalSpacing(gnosisStyle.VerticalSpacing);
                //}
            }

            if (gnosisStyle.HorizontalSpacing > 0)
            {
                if (control is IGnosisSpacingPossessor)
                {
                    ((IGnosisSpacingPossessor)control).HorizontalSpacing = gnosisStyle.HorizontalSpacing;
                }

                //if (controller is GnosisInnerLayoutController)
                //{
                //    ((GnosisInnerLayoutController)controller).SetHorizontalSpacing(gnosisStyle.HorizontalSpacing);
                //}
                //else if (controller is GnosisOuterLayoutController)
                //{
                //    ((GnosisOuterLayoutController)controller).SetHorizontalSpacing(gnosisStyle.HorizontalSpacing);
                //}
            }

           // Type controlModelType = controller.ControlImplementation.GetType();
            Type controlType = control.GetType();

            foreach (GnosisStyleCondition condition in gnosisStyle.Conditions.OrderBy(c => c.Order))
            {

                if (condition.Property.Equals("ControlType"))
                {
                    //find out if this control falls under the inheritance hierarchy of ControlType
                    Type conditionControlType = ControlTypeMapping.GetControlType(condition.Value);
                    if (conditionControlType.IsAssignableFrom(controlType))
                    {
                        ApplyCondition(control, condition, windowsStyle);
                    }
                    else
                    {
                        IGnosisVisibleControlImplementation parent = control.GnosisParent;
                        while (parent != null)
                        {
                            if (conditionControlType.IsAssignableFrom(parent.GetType()))
                            {
                                ApplyCondition(control, condition, windowsStyle);
                                break;
                            }
                            parent = parent.GnosisParent;
                        }
                    }
                }
                else
                {

                    //dynamic properties
                    if (controlType.GetProperty(PropertyNameConverter.GetShivaPropertyName( condition.Property)) != null)
                    {
                        BuildStyleConditions(control, condition, windowsStyle);

                    }

                }
            }

            ((FrameworkElement)control).Style = windowsStyle;

            //if (controller is GnosisSearchFrameController)
            //{
            //    string xaml = XamlWriter.Save(windowsStyle);
            //    int i = 1;
            //}
            //if (controller is GnosisToolbarButtonController)
            //{
            //    string xaml = XamlWriter.Save(windowsStyle);
            //    int i = 1;
            //}
            //else if (controller is GnosisToolbarToggleButtonController)
            //{
            //    string xaml = XamlWriter.Save(windowsStyle);
            //    int i = 1;
            //}
        }

        public void ApplyCondition(IGnosisVisibleControlImplementation control, GnosisStyleCondition gnosisStyleCondition, Style windowsStyle)
        {
            //Setter overrideStyleSetter = new Setter(Control.OverridesDefaultStyleProperty, true);
            //windowsStyle.Setters.Add(overrideStyleSetter);

            if (gnosisStyleCondition.Font != null && gnosisStyleCondition.Font.Length > 0)
            {
                Setter setter = new Setter(Control.FontFamilyProperty, new FontFamily(gnosisStyleCondition.Font));
                windowsStyle.Setters.Add(setter);
            }
            if (gnosisStyleCondition.FontSize > 0)
            {
                Setter setter = new Setter(Control.FontSizeProperty, (double)gnosisStyleCondition.FontSize);
                windowsStyle.Setters.Add(setter);
            }
            if (gnosisStyleCondition.ContentColour != null && gnosisStyleCondition.ContentColour.Length > 0)
            {
                Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(gnosisStyleCondition.ContentColour));
                windowsStyle.Setters.Add(setter);
            }


            if (gnosisStyleCondition.BackgroundColour != null && gnosisStyleCondition.BackgroundColour.Length > 0)
            {
                Setter setter = new Setter(Control.BackgroundProperty, GetBrushFromHex(gnosisStyleCondition.BackgroundColour));
                windowsStyle.Setters.Add(setter);
            }

            if (gnosisStyleCondition.ControlColour != null && gnosisStyleCondition.ControlColour.Length > 0)
            {
                if (control is IGnosisContentControlImplementation)
                {
                    Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisStyleCondition.ControlColour));
                    windowsStyle.Setters.Add(setter);

                    //Setter setter2 = new Setter(Control.BorderThicknessProperty, 1);
                    //windowsStyle.Setters.Add(setter2);
                }
            }

            if (gnosisStyleCondition.ControlThickness > 0)
            {
                if (control is IGnosisControlThicknessPossessor)
                {
                    Setter setter = GetControlThicknessSetter((IGnosisControlThicknessPossessor)control, gnosisStyleCondition);
                    windowsStyle.Setters.Add(setter);
                }
            }


            if (gnosisStyleCondition.OutlineColour != null && gnosisStyleCondition.OutlineColour.Length > 0)
            {
                Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisStyleCondition.OutlineColour));
                windowsStyle.Setters.Add(setter);
            }

            if (gnosisStyleCondition.IsUnderline)
            {
                if (control is IGnosisLinkFieldImplementation)
                {
                    Setter setter = new Setter(TextBox.TextDecorationsProperty, TextDecorations.Underline);
                    windowsStyle.Setters.Add(setter);
                }
                //((GnosisLinkFieldController)controller).SetUnderline(true);
            }

           

           // Type controlModelType = controller.ControlImplementation.GetType();
            Type controlType = control.GetType();

            foreach (GnosisStyleCondition condition in gnosisStyleCondition.Conditions.OrderBy(c => c.Order))
            {

                if (condition.Property.Equals("ControlType"))
                {
                    //find out if this control falls under the inheritance hierarchy of ControlType
                    Type conditionControlType = ControlTypeMapping.GetControlType(condition.Value);
                    if (conditionControlType.IsAssignableFrom(controlType))
                    {
                        ApplyCondition(control, condition, windowsStyle);
                    }
                    else
                    {
                        IGnosisVisibleControlImplementation parent = control.GnosisParent;
                        while (parent != null)
                        {
                            if (conditionControlType.IsAssignableFrom(parent.GetType()))
                            {
                                ApplyCondition(control, condition, windowsStyle);
                                break;
                            }
                            parent = parent.GnosisParent;
                        }
                    }
                }
                else
                {

                    //dynamic properties
                    if (controlType.GetProperty(PropertyNameConverter.GetShivaPropertyName(condition.Property)) != null)
                    {
                        BuildStyleConditions(control, condition, windowsStyle);

                    }

                }
            }

          
        }


        //public void ApplyStyle(IGnosisVisibleControlImplementation control, IGnosisVisibleControlImplementation bindingSource, GnosisVisibleController controller, GnosisStyle gnosisStyle)
        //{
        //    Style windowsStyle = new Style();

        //    //Setter overrideStyleSetter = new Setter(Control.OverridesDefaultStyleProperty, true);
        //    //windowsStyle.Setters.Add(overrideStyleSetter);

        //    if (gnosisStyle.Font != null && gnosisStyle.Font.Length > 0)
        //    {
        //        Setter setter = new Setter(Control.FontFamilyProperty, new FontFamily(gnosisStyle.Font));
        //        windowsStyle.Setters.Add(setter);
        //    }
        //    if (gnosisStyle.FontSize > 0)
        //    {
        //        Setter setter = new Setter(Control.FontSizeProperty, (double)gnosisStyle.FontSize);
        //        windowsStyle.Setters.Add(setter);
        //    }
        //    if (gnosisStyle.ContentColour != null && gnosisStyle.ContentColour.Length > 0)
        //    {
        //        Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(gnosisStyle.ContentColour));
        //        windowsStyle.Setters.Add(setter);
        //    }

        //    if (gnosisStyle.ControlThickness > 0)
        //    {
        //        if (control is IGnosisControlThicknessPossessor)
        //        {
        //            Setter setter = GetControlThicknessSetter((IGnosisControlThicknessPossessor)control, gnosisStyle);
        //            windowsStyle.Setters.Add(setter);
        //        }
        //    }

        //    //if (controller is GnosisContentController)
        //    //{
        //    //    if (gnosisStyle.Font != null && gnosisStyle.Font.Length > 0)
        //    //    {
        //    //        ((GnosisContentController)controller).SetFont(gnosisStyle.Font);
        //    //        // fontFamily = gnosisStyle.Font;
        //    //        //Setter setter = new Setter(Control.FontFamilyProperty, new FontFamily(gnosisStyle.Font));
        //    //        //windowsStyle.Setters.Add(setter);
        //    //    }

        //    //    if (gnosisStyle.FontSize > 0)
        //    //    {
        //    //        ((GnosisContentController)controller).SetFontSize(gnosisStyle.FontSize);
        //    //        //fontSize = gnosisStyle.FontSize;
        //    //        //Setter setter = new Setter(Control.FontSizeProperty, (double)gnosisStyle.FontSize);
        //    //        // windowsStyle.Setters.Add(setter);
        //    //    }

        //    //    if (gnosisStyle.ContentColour != null && gnosisStyle.ContentColour.Length > 0)
        //    //    {
        //    //        //controller.SetForegroundColour(gnosisStyle.ContentColour);
        //    //        Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(gnosisStyle.ContentColour));
        //    //        windowsStyle.Setters.Add(setter);
        //    //    }

        //    //}
        //    //else if (controller is GnosisGalleryItemController)
        //    //{
        //    //    if (gnosisStyle.Font != null && gnosisStyle.Font.Length > 0)
        //    //    {
        //    //        ((IGnosisGalleryItemImplementation)control).SetFont(gnosisStyle.Font);
        //    //        // fontFamily = gnosisStyle.Font;
        //    //        //Setter setter = new Setter(Control.FontFamilyProperty, new FontFamily(gnosisStyle.Font));
        //    //        //windowsStyle.Setters.Add(setter);
        //    //    }

        //    //    if (gnosisStyle.FontSize > 0)
        //    //    {
        //    //        ((IGnosisGalleryItemImplementation)control).SetFontSize(gnosisStyle.FontSize);
        //    //        //fontSize = gnosisStyle.FontSize;
        //    //        //Setter setter = new Setter(Control.FontSizeProperty, (double)gnosisStyle.FontSize);
        //    //        // windowsStyle.Setters.Add(setter);
        //    //    }

        //    //    if (gnosisStyle.ContentColour != null && gnosisStyle.ContentColour.Length > 0)
        //    //    {
        //    //        //controller.SetForegroundColour(gnosisStyle.ContentColour);
        //    //        Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(gnosisStyle.ContentColour));
        //    //        windowsStyle.Setters.Add(setter);
        //    //    }

        //    //}

        //    if (gnosisStyle.BackgroundColour != null && gnosisStyle.BackgroundColour.Length > 0)
        //    {
        //        //controller.SetBackgroundColour(gnosisStyle.BackgroundColour);
        //        Setter setter = new Setter(Control.BackgroundProperty, GetBrushFromHex(gnosisStyle.BackgroundColour));
        //        windowsStyle.Setters.Add(setter);
        //    }

        //    if (gnosisStyle.ControlColour != null && gnosisStyle.ControlColour.Length > 0)
        //    {
        //        if (controller is GnosisContentController)
        //        {
        //            Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisStyle.ControlColour));
        //            windowsStyle.Setters.Add(setter);

        //            //Setter setter2 = new Setter(Control.BorderThicknessProperty, 1);
        //            //windowsStyle.Setters.Add(setter2);
        //        }
        //    }


        //    if (gnosisStyle.OutlineColour != null && gnosisStyle.OutlineColour.Length > 0)
        //    {
        //        Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisStyle.OutlineColour));
        //        windowsStyle.Setters.Add(setter);
        //    }

        //    if (gnosisStyle.IsUnderline)
        //    {
        //        if (controller is GnosisLinkFieldController)
        //        {
        //            Setter setter = new Setter(TextBox.TextDecorationsProperty, TextDecorations.Underline);
        //            windowsStyle.Setters.Add(setter);
        //        }
        //        //((GnosisLinkFieldController)controller).SetUnderline(true);
        //    }

        //    if (gnosisStyle.HorizontalPadding > 0)
        //    {
        //        if (controller is GnosisFrameController)
        //        {
        //            controller.SetHorizontalPadding(gnosisStyle.HorizontalPadding);
        //            //Border b = (Border)control;
        //            //Setter setter = new Setter(Border.PaddingProperty, new Thickness(b.Padding.Left, gnosisStyle.HorizontalPadding, b.Padding.Right, gnosisStyle.HorizontalPadding));
        //            //windowsStyle.Setters.Add(setter);
        //        }
        //    }

        //    if (gnosisStyle.VerticalPadding > 0)
        //    {
        //        if (controller is GnosisFrameController)
        //        {
        //            controller.SetVerticalPadding(gnosisStyle.VerticalPadding);
        //            //Border b = (Border)control;
        //            //Setter setter = new Setter(Border.PaddingProperty, new Thickness(gnosisStyle.VerticalPadding, b.Padding.Top, gnosisStyle.VerticalPadding, b.Padding.Bottom));
        //            //windowsStyle.Setters.Add(setter);
        //        }
        //    }

        //    if (gnosisStyle.HorizontalMargin > 0)
        //    {

        //    }

        //    if (gnosisStyle.VerticalSpacing > 0)
        //    {
        //        if (controller is GnosisInnerLayoutController)
        //        {
        //            ((GnosisInnerLayoutController)controller).SetVerticalSpacing(gnosisStyle.VerticalSpacing);
        //        }
        //        else if (controller is GnosisOuterLayoutController)
        //        {
        //            ((GnosisOuterLayoutController)controller).SetVerticalSpacing(gnosisStyle.VerticalSpacing);
        //        }
        //    }

        //    if (gnosisStyle.HorizontalSpacing > 0)
        //    {
        //        if (controller is GnosisInnerLayoutController)
        //        {
        //            ((GnosisInnerLayoutController)controller).SetHorizontalSpacing(gnosisStyle.HorizontalSpacing);
        //        }
        //        else if (controller is GnosisOuterLayoutController)
        //        {
        //            ((GnosisOuterLayoutController)controller).SetHorizontalSpacing(gnosisStyle.HorizontalSpacing);
        //        }
        //    }

        //    Type controlModelType = controller.ControlImplementation.GetType();
        //    Type controlImpType = control.GetType();

        //    foreach (GnosisStyleCondition condition in gnosisStyle.Conditions.OrderBy(c => c.Order))
        //    {

        //        if (condition.Property.Equals("ControlType"))
        //        {
        //            //find out if this control falls under the inheritance hierarchy of ControlType
        //            Type conditionControlType = ControlTypeMapping.GetControlType(condition.Value);
        //            if (conditionControlType.IsAssignableFrom(controlModelType))
        //            {
        //                ApplyCondition(control, controller, bindingSource, condition, windowsStyle);
        //            }
        //            else
        //            {
        //                GnosisVisibleController parent = controller.Parent;
        //                while (parent != null)
        //                {
        //                    if (conditionControlType.IsAssignableFrom(parent.ControlImplementation.GetType()))
        //                    {
        //                        ApplyCondition(control, controller, bindingSource, condition, windowsStyle);
        //                        break;
        //                    }
        //                    parent = parent.Parent;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //dynamic properties
        //            if (controlImpType.GetProperty(PropertyNameConverter.GetShivaPropertyName(condition.Property)) != null)
        //            {
        //                BuildStyleConditions(controller, bindingSource, condition, controlModelType, windowsStyle);

        //            }

        //        }
        //    }

        //   ((FrameworkElement)control).Style = windowsStyle;

        //    //if (control is IGnosisToggleButtonImplementation)
        //    //{
        //    //    string xaml = XamlWriter.Save(windowsStyle);
        //    //    int i = 1;
        //    //}
        //    //if (controller is GnosisToolbarButtonController)
        //    //{
        //    //    string xaml = XamlWriter.Save(windowsStyle);
        //    //    int i = 1;
        //    //}
        //    //else if (controller is GnosisToolbarToggleButtonController)
        //    //{
        //    //    string xaml = XamlWriter.Save(windowsStyle);
        //    //    int i = 1;
        //    //}
        //}

        public void ApplyCondition(IGnosisVisibleControlImplementation control, IGnosisVisibleControlImplementation bindingSource, GnosisStyleCondition gnosisStyleCondition, Style windowsStyle)
        {

            //Setter overrideStyleSetter = new Setter(Control.OverridesDefaultStyleProperty, true);
            //windowsStyle.Setters.Add(overrideStyleSetter);

            if (gnosisStyleCondition.Font != null && gnosisStyleCondition.Font.Length > 0)
            {
                Setter setter = new Setter(Control.FontFamilyProperty, new FontFamily(gnosisStyleCondition.Font));
                windowsStyle.Setters.Add(setter);
            }
            if (gnosisStyleCondition.FontSize > 0)
            {
                Setter setter = new Setter(Control.FontSizeProperty, (double)gnosisStyleCondition.FontSize);
                windowsStyle.Setters.Add(setter);
            }
            if (gnosisStyleCondition.ContentColour != null && gnosisStyleCondition.ContentColour.Length > 0)
            {
                Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(gnosisStyleCondition.ContentColour));
                windowsStyle.Setters.Add(setter);
            }


            if (gnosisStyleCondition.BackgroundColour != null && gnosisStyleCondition.BackgroundColour.Length > 0)
            {
                Setter setter = new Setter(Control.BackgroundProperty, GetBrushFromHex(gnosisStyleCondition.BackgroundColour));
                windowsStyle.Setters.Add(setter);
            }

            if (gnosisStyleCondition.ControlColour != null && gnosisStyleCondition.ControlColour.Length > 0)
            {
                if (control is IGnosisContentControlImplementation)
                {
                    Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisStyleCondition.ControlColour));
                    windowsStyle.Setters.Add(setter);

                    //Setter setter2 = new Setter(Control.BorderThicknessProperty, 1);
                    //windowsStyle.Setters.Add(setter2);
                }
            }

            if (gnosisStyleCondition.ControlThickness > 0)
            {
                if (control is IGnosisControlThicknessPossessor)
                {
                    Setter setter = GetControlThicknessSetter((IGnosisControlThicknessPossessor)control, gnosisStyleCondition);
                    windowsStyle.Setters.Add(setter);
                }
            }


            if (gnosisStyleCondition.OutlineColour != null && gnosisStyleCondition.OutlineColour.Length > 0)
            {
                Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisStyleCondition.OutlineColour));
                windowsStyle.Setters.Add(setter);
            }

            if (gnosisStyleCondition.IsUnderline)
            {
                if (control is IGnosisLinkFieldImplementation)
                {
                    Setter setter = new Setter(TextBox.TextDecorationsProperty, TextDecorations.Underline);
                    windowsStyle.Setters.Add(setter);
                }
                //((GnosisLinkFieldController)controller).SetUnderline(true);
            }



            //Type controlModelType = controller.ControlImplementation.GetType();
            Type controlType = control.GetType();

            foreach (GnosisStyleCondition condition in gnosisStyleCondition.Conditions.OrderBy(c => c.Order))
            {

                if (condition.Property.Equals("ControlType"))
                {
                    //find out if this control falls under the inheritance hierarchy of ControlType
                    Type conditionControlType = ControlTypeMapping.GetControlType(condition.Value);
                    if (conditionControlType.IsAssignableFrom(controlType))
                    {
                        ApplyCondition(control, bindingSource, condition, windowsStyle);
                    }
                    else
                    {
                        IGnosisVisibleControlImplementation parent = control.GnosisParent;
                        while (parent != null)
                        {
                            if (conditionControlType.IsAssignableFrom(parent.GnosisParent.GetType()))
                            {
                                ApplyCondition(control, bindingSource, condition, windowsStyle);
                                break;
                            }
                            parent = parent.GnosisParent;
                        }
                    }
                }
                else
                {

                    //dynamic properties
                    if (controlType.GetProperty(PropertyNameConverter.GetShivaPropertyName(condition.Property)) != null)
                    {
                        BuildStyleConditions(control, bindingSource, condition, windowsStyle);

                    }

                }
            }


        }

        private void BuildStyleConditions(IGnosisVisibleControlImplementation control, GnosisStyleCondition gnosisCondition, Style windowsStyle)
        {
            //build list of this condition and all ancestors
            //if (gnosisCondition.BackgroundColour != null ||
            //    gnosisCondition.ContentColour != null ||
            //    gnosisCondition.ControlColour != null ||
            //    gnosisCondition.IsOutlined ||
            //    gnosisCondition.IsStrikethrough ||
            //    gnosisCondition.IsUnderline ||
            //    gnosisCondition.OutlineColour != null )
            //{
                List<GnosisStyleCondition> ancestors = new List<GnosisStyleCondition>();
                ancestors.Add(gnosisCondition);
                GnosisStyleCondition currentCondition = gnosisCondition;

                while (currentCondition.Parent != null)
                {
                    ancestors.Add(currentCondition.Parent);
                    currentCondition = currentCondition.Parent;
                }

                MultiDataTrigger dataTrigger = GetMultiDataTrigger(control, ancestors);
                windowsStyle.Triggers.Add(dataTrigger);
           // }

            if (gnosisCondition.Conditions != null && gnosisCondition.Conditions.Count > 0)
            {
                foreach (GnosisStyleCondition styleCondition in gnosisCondition.Conditions)
                {
                    if (control.GetType().GetProperty(PropertyNameConverter.GetShivaPropertyName(styleCondition.Property)) != null)
                    {
                        styleCondition.Parent = gnosisCondition;
                        BuildStyleConditions(control, styleCondition, windowsStyle);
                    }
                }

            }

        }

        private void BuildStyleConditions(IGnosisVisibleControlImplementation control, IGnosisVisibleControlImplementation bindingSource, GnosisStyleCondition gnosisCondition, Style windowsStyle)
        {
            //build list of this condition and all ancestors
            List<GnosisStyleCondition> ancestors = new List<GnosisStyleCondition>();
            ancestors.Add(gnosisCondition);
            GnosisStyleCondition currentCondition = gnosisCondition;

            while (currentCondition.Parent != null)
            {
                ancestors.Add(currentCondition.Parent);
                currentCondition = currentCondition.Parent;
            }

            MultiDataTrigger dataTrigger = GetMultiDataTrigger(control, bindingSource, ancestors);
            windowsStyle.Triggers.Add(dataTrigger);

            if (gnosisCondition.Conditions != null &&  gnosisCondition.Conditions.Count > 0)
            {
                foreach (GnosisStyleCondition styleCondition in gnosisCondition.Conditions)
                {
                    if (control.GetType().GetProperty(PropertyNameConverter.GetShivaPropertyName(styleCondition.Property)) != null)
                    {
                        styleCondition.Parent = gnosisCondition;
                        BuildStyleConditions(control, bindingSource, styleCondition, windowsStyle);
                    }
                }

            }

        }

        private MultiDataTrigger GetMultiDataTrigger(IGnosisVisibleControlImplementation control, List<GnosisStyleCondition> gnosisConditions)
        {
            //only the styles of the first condition are applied
            MultiDataTrigger multiDataTrigger = new MultiDataTrigger();

            string triggerName = "";

            foreach (GnosisStyleCondition gnosisCondition in gnosisConditions.OrderBy(c => c.Order))
            {
                Binding binding;
                string propertyName = PropertyNameConverter.GetShivaPropertyName(gnosisCondition.Property);

               
                binding = new Binding(propertyName);
                //if (gnosisCondition.Property.Equals("HasMouseFocus"))
                //{
                //    binding = new Binding("IsMouseOver");
                //}
                //else if (gnosisCondition.Property.Equals("Disabled"))
                //{
                //    binding = new Binding("IsEnabled");
                //}
                //else
                //{
                //if (propertyName.Equals("Active"))
                //{
                //    int i = 1;
                //}
                //}
                binding.RelativeSource = RelativeSource.Self;

                if (gnosisCondition.Value.Equals("0|1"))
                {
                    //Condition condition = new Condition(binding, );
                    //multiDataTrigger.Conditions.Add(condition);

                    //Condition condition2 = new Condition(binding, false);
                    //multiDataTrigger.Conditions.Add(condition2);
                }
                else if (gnosisCondition.Value.Equals("0|null"))
                {
                    Condition condition = new Condition(binding, false);
                    multiDataTrigger.Conditions.Add(condition);
                }
                else
                {
                    bool conditionValue = false;

                    if (gnosisCondition.Value.Equals("1"))
                    {
                        conditionValue = true;
                    }
                    //if (gnosisCondition.Property.Equals("Disabled"))
                    //{
                    //    conditionValue = !conditionValue;
                    //}
                    Condition condition = new Condition(binding, conditionValue);
                    multiDataTrigger.Conditions.Add(condition);
                }
                triggerName = triggerName + propertyName + ", ";

            }

            //triggerName = triggerName.TrimEnd(' ').TrimEnd(',');
            //TriggerTracing.SetTriggerName(multiDataTrigger, triggerName);
            //TriggerTracing.SetTraceEnabled(multiDataTrigger, true);

            GnosisStyleCondition gnosisFirstCondition = gnosisConditions.First();
            if (gnosisFirstCondition.BackgroundColour != null && gnosisFirstCondition.BackgroundColour.Length > 0)
            {
                Brush brush = GetBrushFromHex(gnosisFirstCondition.BackgroundColour);

                //if (!gnosisFirstCondition.Property.Equals("Selected"))
                //{
                    Setter setter = new Setter(Control.BackgroundProperty, brush);
                    multiDataTrigger.Setters.Add(setter);
               // }
                
            }

            if (gnosisFirstCondition.IsStrikethrough)
            {
                Setter setter = new Setter(TextBlock.TextDecorationsProperty, TextDecorations.Strikethrough);
                multiDataTrigger.Setters.Add(setter);

            }

            if (gnosisFirstCondition.ContentColour != null && gnosisFirstCondition.ContentColour.Length > 0)
            {
                Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(gnosisFirstCondition.ContentColour));
                multiDataTrigger.Setters.Add(setter);
            }

            if (gnosisFirstCondition.ControlThickness > 0 && control is IGnosisControlThicknessPossessor)
            {
                Setter setter = GetControlThicknessSetter((IGnosisControlThicknessPossessor)control, gnosisFirstCondition);
                multiDataTrigger.Setters.Add(setter);
            }

            if (gnosisFirstCondition.GnosisBorderThickness > 0 && control is IGnosisBorderThicknessPossessor)
            {
                Setter setter = GetBorderThicknessSetter((IGnosisBorderThicknessPossessor)control, gnosisFirstCondition);
                multiDataTrigger.Setters.Add(setter); 
            }

            //if (gnosisFirstCondition.ControlColour != null && gnosisFirstCondition.ControlColour.Length > 0)
            //{
            //    Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisFirstCondition.ControlColour));
            //    multiDataTrigger.Setters.Add(setter);
            //}
                //if (gnosisFirstCondition.IsOutlined)
                //{
                //    if (controller is GnosisFrameController || controller is GnosisContentController)
                //    {
                //        Setter setter = new Setter(Control.BorderThicknessProperty, new Thickness(1));
                //        multiDataTrigger.Setters.Add(setter);
                //    }
                //}

                if (gnosisFirstCondition.OutlineColour != null && gnosisFirstCondition.OutlineColour.Length > 0)
            {
                if (control is GnosisFrame || control is IGnosisContentControlImplementation || control is GnosisToggleButton)
                {
                    Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisFirstCondition.OutlineColour));
                    multiDataTrigger.Setters.Add(setter);

                }
            }

            if (gnosisFirstCondition.GnosisBorderThickness > 0 && control is IGnosisBorderThicknessPossessor)
            {
                if (control is GnosisSearchFrame)
                {
                    Setter setter = new Setter(GnosisSearchFrame.GnosisBorderThicknessProperty, gnosisFirstCondition.GnosisBorderThickness);
                    multiDataTrigger.Setters.Add(setter);
                }
                else if (control is GnosisFrame)
                {
                    Setter setter = new Setter(GnosisFrame.GnosisBorderThicknessProperty, gnosisFirstCondition.GnosisBorderThickness);
                    multiDataTrigger.Setters.Add(setter);
                }
                else if (control is GnosisGallery)
                {
                    Setter setter = new Setter(GnosisGallery.GnosisBorderThicknessProperty, gnosisFirstCondition.GnosisBorderThickness);
                    multiDataTrigger.Setters.Add(setter);
                }
                else if (control is GnosisToolbarTray)
                {
                    Setter setter = new Setter(GnosisToolbarTray.GnosisBorderThicknessProperty, gnosisFirstCondition.GnosisBorderThickness);
                    multiDataTrigger.Setters.Add(setter);
                }


            }

            if (gnosisFirstCondition.ControlColour != null && gnosisFirstCondition.ControlColour.Length > 0)
            {
                if (control is IGnosisContentControlImplementation)
                {
                    //controller.SetBorderColour(gnosisStyle.ControlColour);
                    Brush brush = GetBrushFromHex(gnosisFirstCondition.ControlColour);
                    Setter setter = new Setter(Control.BorderBrushProperty, brush);
                    multiDataTrigger.Setters.Add(setter);
                }
            }

            //if (gnosisStyle.IsUnderline)
            //{
            //    ((GnosisLinkFieldController)controller).SetUnderline(true);
            //}
            //string xaml = XamlWriter.Save(multiDataTrigger);

            return multiDataTrigger;

        }

      

        private MultiDataTrigger GetMultiDataTrigger(IGnosisVisibleControlImplementation control, IGnosisVisibleControlImplementation bindingSource, List<GnosisStyleCondition> gnosisConditions)
        {
            //only the styles of the first condition are applied
            MultiDataTrigger multiDataTrigger = new MultiDataTrigger();

            string triggerName = "";

            foreach (GnosisStyleCondition gnosisCondition in gnosisConditions)
            {
                Binding binding = new Binding(PropertyNameConverter.GetShivaPropertyName(gnosisCondition.Property));
                binding.Source = bindingSource;
                bool conditionValue = false;
                if (gnosisCondition.Value.Equals("1"))
                {
                    conditionValue = true;
                }
                Condition condition = new Condition(binding, conditionValue);
                multiDataTrigger.Conditions.Add(condition);
                triggerName = triggerName + gnosisCondition.Property + ", ";
            }

            //triggerName = triggerName.TrimEnd(' ').TrimEnd(',');
            //TriggerTracing.SetTriggerName(multiDataTrigger, triggerName);
            //TriggerTracing.SetTraceEnabled(multiDataTrigger, true);

            GnosisStyleCondition gnosisFirstCondition = gnosisConditions.First();
            if (gnosisFirstCondition.BackgroundColour != null && gnosisFirstCondition.BackgroundColour.Length > 0)
            {
                Brush brush = GetBrushFromHex(gnosisFirstCondition.BackgroundColour);
                Setter setter = new Setter(Control.BackgroundProperty, brush);
                multiDataTrigger.Setters.Add(setter);

            }

            if (gnosisFirstCondition.ContentColour != null && gnosisFirstCondition.ContentColour.Length > 0)
            {
                Setter setter = new Setter(Control.ForegroundProperty, GetBrushFromHex(gnosisFirstCondition.ContentColour));
                multiDataTrigger.Setters.Add(setter);
            }

            if (gnosisFirstCondition.ControlThickness > 0)
            {
                if (control is IGnosisControlThicknessPossessor)
                {
                    Setter setter = GetControlThicknessSetter((IGnosisControlThicknessPossessor)control, gnosisFirstCondition);
                    multiDataTrigger.Setters.Add(setter);
                }
            }
            //if (gnosisFirstCondition.IsOutlined)
            //{
            //    if (controller is GnosisFrameController || controller is GnosisContentController)
            //    {
            //        Setter setter = new Setter(Control.BorderThicknessProperty, new Thickness(1));
            //        multiDataTrigger.Setters.Add(setter);
            //    }
            //}

            if (gnosisFirstCondition.OutlineColour != null && gnosisFirstCondition.OutlineColour.Length > 0)
            {
                if (control is GnosisFrame || control is IGnosisContentControlImplementation)
                {
                    Setter setter = new Setter(Control.BorderBrushProperty, GetBrushFromHex(gnosisFirstCondition.OutlineColour));
                    multiDataTrigger.Setters.Add(setter);
                }
            }

          

            if (gnosisFirstCondition.ControlColour != null && gnosisFirstCondition.ControlColour.Length > 0)
            {
                //if (control is IGnosisContentControlImplementation)
                //{
                    //controller.SetBorderColour(gnosisStyle.ControlColour);
                    Brush brush = GetBrushFromHex(gnosisFirstCondition.ControlColour);
                    Setter setter = new Setter(Control.BorderBrushProperty, brush);
                    multiDataTrigger.Setters.Add(setter);
                //}
            }

            if (gnosisFirstCondition.GnosisBorderThickness > 0)
            {
                if (control is GnosisSearchFrame)
                {
                    Setter setter = new Setter(GnosisSearchFrame.GnosisBorderThicknessProperty, gnosisFirstCondition.GnosisBorderThickness);
                    multiDataTrigger.Setters.Add(setter);
                }
                else if (control is GnosisFrame)
                {
                    Setter setter = new Setter(GnosisFrame.GnosisBorderThicknessProperty, gnosisFirstCondition.GnosisBorderThickness);
                    multiDataTrigger.Setters.Add(setter);
                }
                else if (control is GnosisGallery)
                {
                    Setter setter = new Setter(GnosisGallery.GnosisBorderThicknessProperty, gnosisFirstCondition.GnosisBorderThickness);
                    multiDataTrigger.Setters.Add(setter);
                }
                else if (control is GnosisToolbarTray)
                {
                    Setter setter = new Setter(GnosisToolbarTray.GnosisBorderThicknessProperty, gnosisFirstCondition.GnosisBorderThickness);
                    multiDataTrigger.Setters.Add(setter);
                }

            }
            //if (gnosisStyle.IsUnderline)
            //{
            //    ((GnosisLinkFieldController)controller).SetUnderline(true);
            //}
            //if (bindingSource is IGnosisTileTabItemImplementation)
            //{
            //    string xaml = XamlWriter.Save(multiDataTrigger);
            //}
            return multiDataTrigger;

        }

        private Setter GetBorderThicknessSetter(IGnosisBorderThicknessPossessor control, GnosisStyle gnosisStyle)
        {
            Setter setter = null;

            if (control is GnosisFrame)
            {
                setter = new Setter(GnosisFrame.GnosisBorderThicknessProperty, gnosisStyle.GnosisBorderThickness);
            }
            else if (control is GnosisSearchFrame)
            {
                setter = new Setter(GnosisSearchFrame.GnosisBorderThicknessProperty, gnosisStyle.GnosisBorderThickness);
            }
            else if (control is GnosisGallery)
            {
                setter = new Setter(GnosisGallery.GnosisBorderThicknessProperty, gnosisStyle.GnosisBorderThickness);
            }
            else if (control is GnosisToolbarTray)
            {
                setter = new Setter(GnosisToolbarTray.GnosisBorderThicknessProperty, gnosisStyle.GnosisBorderThickness);
            }
            return setter;
        }

        private Setter GetControlThicknessSetter(IGnosisControlThicknessPossessor control, GnosisStyle gnosisStyle)
        {
            Setter setter = null;
            if (control is GnosisButton)
            {
                setter = new Setter(GnosisButton.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            //else if (control is GnosisCaptionLabel)
            //{
            //    setter = new Setter(GnosisCaptionLabel.ControlThicknessProperty, gnosisStyle.ControlThickness);
            //}
            else if (control is GnosisCheckField)
            {
                setter = new Setter(GnosisCheckField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisComboField)
            {
                setter = new Setter(GnosisComboField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisDateField)
            {
                setter = new Setter(GnosisDateField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisDateTimeField)
            {
                setter = new Setter(GnosisDateTimeField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisGalleryItem)
            {
                setter = new Setter(GnosisGalleryItem.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisLinkField)
            {
                setter = new Setter(GnosisLinkField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisListField)
            {
                setter = new Setter(GnosisListField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisNumberField)
            {
                setter = new Setter(GnosisNumberField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisRadioField)
            {
                setter = new Setter(GnosisRadioField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisTextField)
            {
                setter = new Setter(GnosisTextField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisToggleButton)
            {
                setter = new Setter(GnosisToggleButton.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisGridCheckField)
            {
                setter = new Setter(GnosisGridCheckField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }
            else if (control is GnosisGridTextField)
            {
                setter = new Setter(GnosisGridTextField.ControlThicknessProperty, gnosisStyle.ControlThickness);
            }

            return setter;
        }



        public double GetTextHeight(IGnosisTextAreaImplementation textArea, string font, int fontSize)
        {
            
            TextBox txt = (TextBox)textArea;

            //var formattedText = new FormattedText(
            //        alphabet,
            //        CultureInfo.CurrentUICulture,
            //        FlowDirection.LeftToRight,
            //        new Typeface(txt.FontFamily, txt.FontStyle, txt.FontWeight, txt.FontStretch),
            //        txt.FontSize,
            //        Brushes.Black);

            var formattedText = new FormattedText(
                 alphabet,
                 CultureInfo.CurrentUICulture,
                 FlowDirection.LeftToRight,
                 new Typeface(font),
                 fontSize,
                 Brushes.Black);


            return formattedText.Height + 2;
            
        }

        public double GetTextHeight(IGnosisVisibleControlImplementation gnosisControl, string font, int fontSize)
        {
            double height;

            //try
            //{
            //    Control control = (Control)gnosisControl;
            //    var formattedText = new FormattedText(
            //           alphabet,
            //           CultureInfo.CurrentUICulture,
            //           FlowDirection.LeftToRight,
            //           new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
            //           control.FontSize,
            //           Brushes.Black);

            //    height = formattedText.Height; 
            //}
            //catch (InvalidCastException)
            //{
                var formattedText = new FormattedText(
                     alphabet,
                     CultureInfo.CurrentUICulture,
                     FlowDirection.LeftToRight,
                     new Typeface(font),
                     fontSize,
                     Brushes.Black);

                height = formattedText.Height;
          //  }

            return height + 2;
        }

        public double GetTextHeight(IGnosisGridTextFieldImplementation gnosisTextField, string font, int fontSize)
        {
            double height;

            //try
            //{
            //    Control control = (Control)gnosisTextField;
            //    var formattedText = new FormattedText(
            //           alphabet,
            //           CultureInfo.CurrentUICulture,
            //           FlowDirection.LeftToRight,
            //           new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
            //           control.FontSize,
            //           Brushes.Black);

            //    height = formattedText.Height;
            //}
            //catch (InvalidCastException)
            //{
                var formattedText = new FormattedText(
                     alphabet,
                     CultureInfo.CurrentUICulture,
                     FlowDirection.LeftToRight,
                     new Typeface(font),
                     fontSize,
                     Brushes.Black);

                height = formattedText.Height;
          //  }

            return height + 2;
        }

        public double GetTextHeight(IGnosisResultsTextFieldImplementation textResultsField, string font, int fontSize)
        {
            double height;

            //try
            //{
            //    Control control = (Control)textResultsField;
            //    var formattedText = new FormattedText(
            //           alphabet,
            //           CultureInfo.CurrentUICulture,
            //           FlowDirection.LeftToRight,
            //           new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
            //           control.FontSize,
            //           Brushes.Black);

            //    height = formattedText.Height;
            //}
            //catch (InvalidCastException)
            //{
                var formattedText = new FormattedText(
                     alphabet,
                     CultureInfo.CurrentUICulture,
                     FlowDirection.LeftToRight,
                     new Typeface(font),
                     fontSize,
                     Brushes.Black);

                height = formattedText.Height;
            //}

            return height + 2;
        }

        public double GetFieldHeight(IGnosisCaptionLabelImplementation caption, string font, int fontSize)
        {
            double height = caption.HorizontalPadding * 2;

            var formattedText = new FormattedText(
                     alphabet,
                     CultureInfo.CurrentUICulture,
                     FlowDirection.LeftToRight,
                     new Typeface(font),
                     fontSize,
                     Brushes.Black);

            height += formattedText.Height + 1;

            return height;
        }

        public double GetFieldHeight(IGnosisContentControlImplementation gnosisControl, string font, int fontSize)
        {
            double height = 0;

            if (gnosisControl is IGnosisPaddingPossessor)
            {
                height = ((IGnosisPaddingPossessor)gnosisControl).HorizontalPadding * 2;
            }

            //if (gnosisControl is GnosisDateField || gnosisControl is GnosisDateTimeField)
            //{
            //    height += 20;
            //}
            //else
            //{
                //try
                //{
                //    Control control = (Control)gnosisControl;


                //    var formattedText = new FormattedText(
                //           alphabet,
                //           CultureInfo.CurrentUICulture,
                //           FlowDirection.LeftToRight,
                //           new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
                //           control.FontSize,
                //           Brushes.Black);

                //    // if (formattedText.Height > height)
                //    // {
                //    height += formattedText.Height;
                //    //}
                //}
                //catch (InvalidCastException)
                //{
                    var formattedText = new FormattedText(
                      alphabet,
                      CultureInfo.CurrentUICulture,
                      FlowDirection.LeftToRight,
                      new Typeface(font),
                      fontSize,
                      Brushes.Black);

            // if (formattedText.Height > height)
            //{
            height += formattedText.Height + 1;
                    //}
              //  }
            //}
            return height;

        }

        public double GetFieldHeight(IGnosisGridFieldImplementation gridField, string font, int fontSize)
        {
            double height = gridField.HorizontalPadding * 2;

            //try
            //{
            //    Control control = (Control)gridField;
            //    var formattedText = new FormattedText(
            //           alphabet,
            //           CultureInfo.CurrentUICulture,
            //           FlowDirection.LeftToRight,
            //           new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
            //           control.FontSize,
            //           Brushes.Black);

            //    // if (formattedText.Height > height)
            //    // {
            //    height += formattedText.Height;
            //    //}
            //}
            //catch (InvalidCastException)
            //{
                var formattedText = new FormattedText(
                  alphabet,
                  CultureInfo.CurrentUICulture,
                  FlowDirection.LeftToRight,
                  new Typeface(font),
                  fontSize,
                  Brushes.Black);

                // if (formattedText.Height > height)
                //{
                height += formattedText.Height;
                //}
        //    }

            return height;

        }


        ///// <summary>
        ///// Get the average width of a numeric character.
        ///// </summary>
        ///// <returns></returns>
        //public double GetCharacterWidthNumeric()
        //{
        //    var formattedText = new FormattedText(
        //          numbers,
        //          CultureInfo.CurrentUICulture,
        //          FlowDirection.LeftToRight,
        //          new Typeface(fontFamily),
        //          fontSize,
        //          Brushes.Black);

        //    double numberWidth = formattedText.Width / numbers.Length;

        //    return numberWidth;
        //}

        public double GetCharacterWidthNumeric(IGnosisContentControlImplementation gnosisControl)
        {
            Control control = (Control)gnosisControl;
            var formattedText = new FormattedText(
                   numbers,
                   CultureInfo.CurrentUICulture,
                   FlowDirection.LeftToRight,
                   new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
                   control.FontSize,
                   Brushes.Black);

            double characterWidth = formattedText.Width / numbers.Length;

            return characterWidth;

        }

        public double GetCharacterWidth(IGnosisTextAreaImplementation textArea, string font, int fontSize)
        {
            TextBox control = (TextBox)textArea;

            var formattedText = new FormattedText(
                     alphabet,
                     CultureInfo.CurrentUICulture,
                     FlowDirection.LeftToRight,
                     new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
                     control.FontSize,
                     Brushes.Black);

            double characterWidth = formattedText.Width / alphabet.Length;

            return characterWidth;// + (characterWidth * 0.2);
        }

        /// <summary>
        /// Get the average width of a single alphanumeric character.
        /// </summary>
        /// <returns></returns>
        public double GetCharacterWidth(IGnosisContentControlImplementation gnosisControl, string font, int fontSize)
        {
            try
            {
                Control control = (Control)gnosisControl;
                var formattedText = new FormattedText(
                       alphabet,
                       CultureInfo.CurrentUICulture,
                       FlowDirection.LeftToRight,
                       new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
                       control.FontSize,
                       Brushes.Black);

                double characterWidth = formattedText.Width / alphabet.Length;

                return characterWidth;// + (characterWidth * 0.2);
            }
            catch (InvalidCastException)
            {
                return GetCharacterWidth(font, fontSize);
            }
            
        }

        public double GetCharacterWidth(string font, int fontSize)
        {
            var formattedText = new FormattedText(
                    alphabet,
                    CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface(font),
                    fontSize,
                    Brushes.Black);

            double characterWidth = formattedText.Width / alphabet.Length;

            return characterWidth;// + (characterWidth * 0.2);
        }

        //public double GetCharacterWidth()
        //{

        //    var formattedText = new FormattedText(
        //            alphabet,
        //            CultureInfo.CurrentUICulture,
        //            FlowDirection.LeftToRight,
        //            new Typeface(fontFamily),
        //            fontSize,
        //            Brushes.Black);

        //    double characterWidth = formattedText.Width / alphabet.Length;

        //    return characterWidth + (characterWidth * 0.2);
        //}

        public double GetMinTextAreaWidth(IGnosisTextAreaImplementation textArea, string font, int fontSize, int minChars)
        {
            TextBox control = (TextBox)textArea;
            double characterWidth = GetCharacterWidth(textArea, font, fontSize);
            double padding = 2 * ((GnosisTextArea)textArea).HorizontalPadding;
            if (padding == 0)
            {
                padding = 5;
            }
            double minWidth = minChars * characterWidth + padding;

            return minWidth + characterWidth;//buffer
        }

        public double GetMinFieldWidth(IGnosisContentControlImplementation control, string font, int fontSize, int minChars)
        {
            double characterWidth = GetCharacterWidth(control, font, fontSize);
            double padding = 2 * ((IGnosisPaddingPossessor)control).HorizontalPadding;
            if (padding == 0)
            {
                padding = 5;
            }
            double minWidth = minChars * characterWidth + padding;

            return minWidth + characterWidth;//buffer

        }

        public double GetMaxFieldWidth(IGnosisContentControlImplementation control, string font, int fontSize, int maxCharacters)
        {
            double characterWidth = GetCharacterWidth(control, font, fontSize);
            double padding = 2 * ((IGnosisPaddingPossessor)control).HorizontalPadding;
            if (padding == 0)
            {
                padding = 5;
            }
            double maxWidth = maxCharacters * characterWidth + padding;

            return maxWidth + characterWidth;
        }
        

        public double GetMaxTextAreaWidth(IGnosisTextAreaImplementation textArea, string font, int fontSize, int maxChars)
        {
            double characterWidth = GetCharacterWidth(textArea, font, fontSize);
            double padding = 2 * ((GnosisTextArea)textArea).HorizontalPadding;
            if (padding == 0)
            {
                padding = 5;
            }
            double maxWidth = maxChars * characterWidth + padding;

            return maxWidth + characterWidth;
        }

        //public void CloneStyle(IGnosisVisibleControlImplementation receiver, IGnosisVisibleControlImplementation source)
        //{
        //    ((FrameworkElement)receiver).Style = ((FrameworkElement)source).Style;
        //}

        //public void CloneCaptionStyle(IGnosisCaptionLabelImplementation receiver, IGnosisCaptionLabelImplementation source)
        //{
        //    ((FrameworkElement)receiver).Style = ((FrameworkElement)source).Style;
        //}

       

        //private double GetPaddingHorizontal(IGnosisVisibleControlImplementation control)
        //{
        //    double padding;


        //        padding = control.GetPaddingHorizontal();

        //    //else
        //    //{
        //    //    padding = captionLabel.GetPaddingHorizontal();
        //    //}

        //    return padding;

        //}


    }

    public static class StyleExtensions
    {
        public static void CopyStyle(this FrameworkElement control, Style style)
        {

            if (style == null)
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Can not copy null style", "StyleExtensions.CopyStyle");
                return;
            }

            if (control.Style == null)
            {
                control.Style = new Style(control.GetType());
            }

            if (control.Style.TargetType.IsAssignableFrom(style.TargetType))
            {
                control.Style = new Style(control.GetType(), style);
            }
            else
            {
                Style newStyle = new Style(control.GetType(), control.Style);

                foreach (SetterBase currentSetter in style.Setters)
                    newStyle.Setters.Add(currentSetter);
                foreach (TriggerBase currentTrigger in style.Triggers)
                    newStyle.Triggers.Add(currentTrigger);
                // This code is only needed when using DynamicResources.
                foreach (object key in style.Resources.Keys)
                    newStyle.Resources[key] = style.Resources[key];

                control.Style = newStyle;
            }
           
        }
    }
}
