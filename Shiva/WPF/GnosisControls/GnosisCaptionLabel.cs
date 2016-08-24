using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Shiva.Shared.BaseControllers;
using System;
using ShivaWPF3.UtilityWPF;

namespace GnosisControls
{
    public class GnosisCaptionLabel : TextBox, IGnosisCaptionLabelImplementation
    {
       // private TextBox txtContent;
        private GnosisController.CaptionPosition relativePosition;
        private GnosisController.HorizontalAlignmentType captionHorizontalAlignment;
        private GnosisController.VerticalAlignmentType captionVerticalAlignment;
        private int controlThickness;
        private int captionSpacing;
        private int horizontalPadding;
        private int horizontalMargin;
        private int verticalPadding;
        private int verticalMargin;

        //public static readonly DependencyProperty ControlThicknessProperty =
        //    DependencyProperty.RegisterAttached("ControlThickness",
        //    typeof(int), typeof(GnosisCaptionLabel), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
        ////new FrameworkPropertyMetadata(0,
        ////    FrameworkPropertyMetadataOptions.Inherits));

        //public static void SetControlThickness(UIElement element, int value)
        //{
        //    element.SetValue(ControlThicknessProperty, value);
        //}

        //public static int GetControlThickness(UIElement element)
        //{
        //    return (int)element.GetValue(ControlThicknessProperty);
        //}

        //public static void ControlThicknessPropertyChanged(object source, DependencyPropertyChangedEventArgs e)
        //{
        //    GnosisCaptionLabel captionLabel = source as GnosisCaptionLabel;
        //    int newThickness = (int)e.NewValue;
        //    int oldThickness = (int)e.OldValue;
        //    double marginHorizontal;
        //    double marginVertical;

        //    if (newThickness > oldThickness)
        //    {
        //        //increase border thickness, decrease margin
        //        marginHorizontal = captionLabel.Margin.Left - newThickness;
        //        marginVertical = captionLabel.Margin.Top - newThickness;
        //    }
        //    else
        //    {
        //        //decrease border thickness, increase margin
        //        marginHorizontal = captionLabel.Margin.Left + oldThickness;
        //        marginVertical = captionLabel.Margin.Top + oldThickness;
        //    }

        //    if (marginHorizontal >= 0 && marginVertical >= 0)
        //    {
        //        captionLabel.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
        //        captionLabel.BorderThickness = new Thickness(newThickness);
        //    }
        //}

        public string Caption
        {
            get
            {
                return this.Text;
            }

            set
            {
                this.Visibility = System.Windows.Visibility.Visible;
                this.Text = value;
                this.IsReadOnly = true;
            }
        }

        public GnosisController.VerticalAlignmentType CaptionVerticalAlignment
        {
            get
            {
                return captionVerticalAlignment;
            }

            set
            {
                captionVerticalAlignment = value;
                switch (captionVerticalAlignment)
                {
                    case GnosisController.VerticalAlignmentType.TOP:
                        this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        this.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
                        break;
                    case GnosisController.VerticalAlignmentType.CENTRE:
                        this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        break;
                    case GnosisController.VerticalAlignmentType.BOTTOM:
                        this.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                        this.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
                        break;
                }
            }
        }

        public GnosisController.HorizontalAlignmentType CaptionHorizontalAlignment
        {
            get
            {
                return captionHorizontalAlignment;
            }

            set
            {
                captionHorizontalAlignment = value;
                switch (captionHorizontalAlignment)
                {
                    case GnosisController.HorizontalAlignmentType.LEFT:
                        this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                        break;
                    case GnosisController.HorizontalAlignmentType.CENTRE:
                        this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                        break;
                    case GnosisController.HorizontalAlignmentType.RIGHT:
                        this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                        this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                        break;
                }
            }
        }

        public GnosisController.CaptionPosition RelativePosition
        {
            get
            {
                return relativePosition;
            }

            set
            {
                relativePosition = value;
                //update caption spacing 
                CaptionSpacing = captionSpacing;
            }
        }

        public int ControlThickness
        {
            get
            {
                return controlThickness;
            }

            set
            {
                controlThickness = value;
                if (controlThickness > 0)
                {
                    if (horizontalMargin >= controlThickness && verticalMargin >= controlThickness)
                    {
                        this.BorderThickness = new Thickness(controlThickness);
                        this.Margin = new Thickness(verticalMargin - controlThickness, horizontalMargin - controlThickness, verticalMargin - controlThickness, horizontalMargin - controlThickness);
                    }
                }
            }
        }

        public int CaptionSpacing
        {
            get
            {
                return captionSpacing;
            }

            set
            {
                captionSpacing = value;
                switch (relativePosition)
                {
                    case GnosisController.CaptionPosition.ABOVE:
                        this.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin, horizontalMargin + captionSpacing);
                        break;
                    case GnosisController.CaptionPosition.BELOW:
                        this.Margin = new Thickness(verticalMargin, horizontalMargin + captionSpacing, verticalMargin, horizontalMargin);
                        break;
                    case GnosisController.CaptionPosition.LEFT:
                        this.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin + captionSpacing, horizontalMargin);
                        break;
                    case GnosisController.CaptionPosition.RIGHT:
                        this.Margin = new Thickness(verticalMargin + captionSpacing, horizontalMargin, verticalMargin, horizontalMargin);
                        break;
                }
            }
        }

        public int HorizontalPadding
        {
            get
            {
                return horizontalPadding;
            }

            set
            {
                horizontalPadding = value;
                this.SetHorizontalPaddingExt(horizontalPadding);
            }
        }

        public int HorizontalMargin
        {
            get
            {
                return horizontalMargin;
            }

            set
            {
                horizontalMargin = value;
                this.SetHorizontalMarginExt(horizontalMargin);
            }
        }

        public int VerticalMargin
        {
            get
            {
                return verticalMargin;
            }

            set
            {
                verticalMargin = value;
                this.SetVerticalMarginExt(verticalMargin);
            }
        }

        public int VerticalPadding
        {
            get
            {
                return verticalPadding;
            }

            set
            {
                verticalPadding = value;
                this.SetVerticalPaddingExt(verticalPadding);
            }
        }

        public GnosisCaptionLabel()
        {
            this.TextWrapping = System.Windows.TextWrapping.Wrap;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.Focusable = false;
            this.BorderThickness = new System.Windows.Thickness(0);
            this.Visibility = System.Windows.Visibility.Collapsed;

            this.Padding = new System.Windows.Thickness(0);
           // Padding = new System.Windows.Thickness(1, 0, 1, 0);
        }

        public double GetWidth()
        {
            double characterWidth = GlobalData.Singleton.StyleHelper.GetCharacterWidth(this.FontFamily.ToString(), (int)this.FontSize);
            double width = (characterWidth * Caption.Length) + (verticalPadding * 2);
            return width;
        }


        //public string GetText()
        //{
        //    return txtContent.Text;
        //}

        //public void SetCaption(string caption)
        //{
        //    txtContent.Visibility = System.Windows.Visibility.Visible;
        //    txtContent.Text = caption;
        //    txtContent.IsReadOnly = true;
        //}


        //public void SetMaxLines(int maxLines)
        //{
        //    txtContent.MaxLines = maxLines;
        //}

        //public void SetCaptionVerticalAlignment(GnosisController.VerticalAlignmentType captionAlignmentVertical)
        //{
        //    switch(captionAlignmentVertical)
        //    {
        //        case GnosisController.VerticalAlignmentType.TOP:
        //            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        //            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
        //            break;
        //        case GnosisController.VerticalAlignmentType.CENTRE:
        //            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        //            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        //            break;
        //        case GnosisController.VerticalAlignmentType.BOTTOM:
        //            this.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
        //            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;
        //            break;
        //    }
        //}

        //public void SetCaptionAlignmentHorizontal(GnosisController.HorizontalAlignmentType captionAlignmentHorizontal)
        //{
        //    switch(captionAlignmentHorizontal)
        //    {
        //        case GnosisController.HorizontalAlignmentType.LEFT:
        //            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        //            this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
        //            break;
        //        case GnosisController.HorizontalAlignmentType.CENTRE:
        //            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        //            this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
        //            break;
        //        case GnosisController.HorizontalAlignmentType.RIGHT:
        //            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
        //            this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
        //            break;
        //    }
        //}

        //public void SetMargin(int left, int top, int right, int bottom)
        //{
        //    this.Margin = new System.Windows.Thickness(left, top, right, bottom);
        //}

        //public void SetFont(string font)
        //{
        //    txtContent.FontFamily = new FontFamily(font);
        //}

        //public void SetFontSize(int fontSize)
        //{
        //    txtContent.FontSize = fontSize;
        //}

        //public void SetBackgroundColour(string backgroundColour)
        //{
        //    string hex = "#" + backgroundColour;
        //    txtContent.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        //}

        //public void SetForegroundColour(string contentColour)
        //{
        //    string hex = "#" + contentColour;
        //    txtContent.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        //}

        //public void SetBorderColour(string controlColour)
        //{
        //    string hex = "#" + controlColour;
        //    this.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        //}

        //public void SetPaddingHorizontal(int paddingHorizontal)
        //{
        //    this.Padding = new System.Windows.Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        //}

        //public double GetPaddingHorizontal()
        //{
        //    return this.Padding.Left;
        //}

        //public FontFamily GetFontFamily()
        //{
        //    return txtContent.FontFamily;
        //}

        //public double GetFontSize()
        //{
        //    return txtContent.FontSize;
        //}

        //public FontStyle GetFontStyle()
        //{
        //    return txtContent.FontStyle;
        //}

        //public FontWeight GetFontWeight()
        //{
        //    return txtContent.FontWeight;
        //}

        //public FontStretch GetFontStretch()
        //{
        //    return txtContent.FontStretch;
        //}

        //public void SetRelativePosition(GnosisController.CaptionPosition _relativePosition)
        //{
        //    relativePosition = _relativePosition;
        //}

        //public GnosisController.CaptionPosition GetRelativePosition()
        //{
        //    return relativePosition;
        //}

    }
}
