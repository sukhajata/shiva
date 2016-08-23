using ShivaShared3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ShivaWPF3.UtilityWPF;
using ShivaShared3.BaseControllers;

namespace ShivaWPF3.GridControlsWPF
{
    public class GnosisGridHeaderFieldWPF : TextBox, IGnosisGridHeaderFieldImplementation
    {
        private GnosisController.CaptionPosition relativePosition;

        public GnosisGridHeaderFieldWPF()
        {
            this.Focusable = false;
            this.TextWrapping = TextWrapping.Wrap;
            this.VerticalContentAlignment = VerticalAlignment.Bottom;
            this.Margin = new System.Windows.Thickness(0, 0, 1, 1);
            //  this.Padding = new Thickness(0, 1, 1, 0);
        }

        public IGnosisGridHeaderFieldImplementation GetClone()
        {
            return (GnosisGridHeaderFieldWPF)this.Clone();
        }

        public FontFamily GetFontFamily()
        {
            return this.FontFamily;
        }

        public double GetFontSize()
        {
            return this.FontSize;
        }

        public FontStretch GetFontStretch()
        {
            return this.FontStretch;
        }

        public FontStyle GetFontStyle()
        {
            return this.FontStyle;
        }

        public FontWeight GetFontWeight()
        {
            return this.FontWeight;
        }

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public GnosisController.CaptionPosition GetRelativePosition()
        {
            return relativePosition;
        }

        public void RemoveBorder()
        {
            this.BorderThickness = new Thickness(0);
        }

        public void SetBackgroundColour(string backgroundColour)
        {
            this.Background = StyleHelper.GetBrushFromHex(backgroundColour);
        }

        public void SetBorderColour(string controlColour)
        {
            this.BorderBrush = StyleHelper.GetBrushFromHex(controlColour);
            this.BorderThickness = new Thickness(1);
        }

        public void SetCaption(string caption)
        {
            this.Text = caption;
            this.IsReadOnly = true;
        }

        public void SetCaptionAlignmentHorizontal(GnosisController.HorizontalAlignmentType captionAlignmentHorizontal)
        {
            throw new NotImplementedException();
        }

        public void SetCaptionAlignmentVertical(GnosisController.VerticalAlignmentType captionAlignmentVertical)
        {
            throw new NotImplementedException();
        }

        public void SetFont(string font)
        {
            this.FontFamily = new FontFamily(font);
        }

        public void SetFontSize(int fontSize)
        {
            this.FontSize = fontSize;
        }

        public void SetForegroundColour(string contentColour)
        {
            this.Foreground = StyleHelper.GetBrushFromHex(contentColour);
        }

        public void SetHorizontalContentAlignment(GnosisController.HorizontalAlignmentType ha)
        {
            this.SetHorizontalContentAlignmentExt(ha);
        }

        public void SetMargin(int left, int top, int right, int bottom)
        {
            throw new NotImplementedException();
        }

        public void SetMaxLines(int maxLines)
        {
            throw new NotImplementedException();
        }

        public void SetPaddingHorizontal(int paddingHorizontal)
        {
            this.Padding = new System.Windows.Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        }

        public void SetRelativePosition(GnosisController.CaptionPosition _relativePosition)
        {
            relativePosition = _relativePosition;
        }

    }
}
