using Shiva.Shared.BaseControllers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ShivaWPF3.UtilityWPF
{
    public static class ExtensionMethodsWPF
    {
        public static UIElement Clone(this UIElement toClone)
        {
            UIElement clone = (UIElement)XamlReader.Parse(XamlWriter.Save(toClone));

            return clone;
        }

        public static void SetHorizontalPaddingExt(this Control control, double horizontalPadding)
        {
            control.Padding = new Thickness(horizontalPadding, control.Padding.Top, horizontalPadding, control.Padding.Bottom);
        }

        public static void SetHorizontalPaddingExt(this Border border, int horizontalPadding)
        {
            border.Padding = new Thickness(horizontalPadding, border.Padding.Top, horizontalPadding, border.Padding.Bottom);
        }

        public static void SetVerticalPaddingExt(this Control control, double verticalPadding)
        {
            control.Padding = new Thickness(control.Padding.Left, verticalPadding, control.Padding.Right, verticalPadding);
        }

        public static void SetVerticalPaddingExt(this Border border, int verticalPadding)
        {
            border.Padding = new Thickness(border.Padding.Left, verticalPadding, border.Padding.Right, verticalPadding);
        }

        public static void SetHorizontalMarginExt(this FrameworkElement control, int horizontalMargin)
        {
            control.Margin = new Thickness(horizontalMargin, control.Margin.Top, horizontalMargin, control.Margin.Bottom);
        }

        public static void SetHorizontalMarginExt(this Border border, int horizontalMargin)
        {
            border.Margin = new Thickness(horizontalMargin, border.Margin.Top, horizontalMargin, border.Margin.Bottom);
        }

        public static void SetVerticalMarginExt(this FrameworkElement control, int verticalMargin)
        {
            control.Margin = new Thickness(control.Margin.Left, verticalMargin, control.Margin.Right, verticalMargin);
        }

        public static void SetVerticalMarginExt(this Border border, int verticalMargin)
        {
            border.Margin = new Thickness(border.Margin.Left, verticalMargin, border.Margin.Right, verticalMargin);
        }

        public static void SetBackgroundColourExt(this Control control, string backgroundHex)
        {
            control.Background = StyleHelper.GetBrushFromHex(backgroundHex);
        }

        public static void SetHorizontalAlignmentExt(this FrameworkElement control, GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case GnosisController.HorizontalAlignmentType.LEFT:
                    control.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    break;
                case GnosisController.HorizontalAlignmentType.RIGHT:
                    control.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    break;
                case GnosisController.HorizontalAlignmentType.CENTRE:
                    control.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    break;
                case GnosisController.HorizontalAlignmentType.STRETCH:
                    control.HorizontalAlignment = HorizontalAlignment.Stretch;
                    break;
            }
        }

        public static void SetHorizontalAlignmentExt(this Border border, GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case GnosisController.HorizontalAlignmentType.LEFT:
                    border.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    break;
                case GnosisController.HorizontalAlignmentType.RIGHT:
                    border.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    break;
                case GnosisController.HorizontalAlignmentType.CENTRE:
                    border.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    break;
                case GnosisController.HorizontalAlignmentType.STRETCH:
                    border.HorizontalAlignment = HorizontalAlignment.Stretch;
                    break;
            }
        }


        public static void SetHorizontalContentAlignmentExt(this Control control, GnosisController.HorizontalAlignmentType horizontalContentAlignment)
        {
            switch (horizontalContentAlignment)
            {
                case GnosisController.HorizontalAlignmentType.LEFT:
                    control.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    break;
                case GnosisController.HorizontalAlignmentType.CENTRE:
                    control.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    break;
                case GnosisController.HorizontalAlignmentType.RIGHT:
                    control.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                    break;
                case GnosisController.HorizontalAlignmentType.STRETCH:
                    control.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    break;
            }
        }


        public static void SetVerticalAlignmentExt(this FrameworkElement element, GnosisController.VerticalAlignmentType verticalAlignment)
        {
            switch (verticalAlignment)
            {
                case GnosisController.VerticalAlignmentType.TOP:
                    element.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case GnosisController.VerticalAlignmentType.CENTRE:
                    element.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case GnosisController.VerticalAlignmentType.BOTTOM:
                    element.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case GnosisController.VerticalAlignmentType.STRETCH:
                    element.VerticalAlignment = VerticalAlignment.Stretch;
                    break;
            }
        }

        public static void SetVerticalContentAlignmentExt(this Control control, GnosisController.VerticalAlignmentType verticalContentAlignment)
        {
            switch (verticalContentAlignment)
            {
                case GnosisController.VerticalAlignmentType.TOP:
                    control.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                    break;
                case GnosisController.VerticalAlignmentType.CENTRE:
                    control.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    break;
                case GnosisController.VerticalAlignmentType.BOTTOM:
                    control.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
                    break;
                case GnosisController.VerticalAlignmentType.STRETCH:
                    control.VerticalContentAlignment = VerticalAlignment.Stretch;
                    break;
            }
        }

        public static void SetVisibleExt(this UIElement control, bool visible)
        {
            if (visible)
            {
                control.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                control.Visibility = System.Windows.Visibility.Collapsed;
            }

        }


        public static void OnControlThicknessChangedExt(this Border control, int newThickness, int oldThickness, bool useMargin)
        {
            if (useMargin)
            {
                double marginHorizontal;
                double marginVertical;

                if (newThickness > oldThickness)
                {
                    //increase border thickness, decrease padding
                    marginHorizontal = control.Margin.Left - newThickness;
                    marginVertical = control.Margin.Top - newThickness;

                    if (marginHorizontal >= 0 && marginVertical >= 0)
                    {
                        control.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                        control.BorderThickness = new Thickness(newThickness);
                        control.Height = control.Height + (newThickness - oldThickness);
                    }
                }
                else
                {
                    //decrease border thickness, increase padding
                    marginHorizontal = control.Margin.Left + oldThickness;
                    marginVertical = control.Margin.Top + oldThickness;

                    if (marginHorizontal >= 0 && marginVertical >= 0)
                    {
                        control.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                        control.BorderThickness = new Thickness(newThickness);
                        control.Height = control.Height - (oldThickness - newThickness);
                    }
                }
            }
        }
    }
}
