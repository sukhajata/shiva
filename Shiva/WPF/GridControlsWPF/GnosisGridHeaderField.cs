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
using System.Windows.Markup;
using System.ComponentModel;

namespace GnosisControls
{
    public partial class GnosisGridHeaderField : GnosisGridTextField, IGnosisGridHeaderFieldImplementation
    {
        private GnosisController.CaptionPosition relativePosition;
        private GnosisController.HorizontalAlignmentType captionHorizontalAlignment;
        private GnosisController.VerticalAlignmentType captionVerticalAlignment;
        private int horizontalMargin;
        private int verticalMargin;
        private int controlThickness;
        private int captionSpacing;

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
                //switch (relativePosition)
                //{
                //    case GnosisController.CaptionPosition.ABOVE:
                //        this.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin, horizontalMargin + captionSpacing);
                //        break;
                //    case GnosisController.CaptionPosition.BELOW:
                //        this.Margin = new Thickness(verticalMargin, horizontalMargin + captionSpacing, verticalMargin, horizontalMargin);
                //        break;
                //    case GnosisController.CaptionPosition.LEFT:
                //        this.Margin = new Thickness(verticalMargin, horizontalMargin, verticalMargin + captionSpacing, horizontalMargin);
                //        break;
                //    case GnosisController.CaptionPosition.RIGHT:
                //        this.Margin = new Thickness(verticalMargin + captionSpacing, horizontalMargin, verticalMargin, horizontalMargin);
                //        break;
                //}
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
            }
        }

        public GnosisGridHeaderField()
        {
            this.Focusable = false;
            this.TextWrapping = TextWrapping.Wrap;
            this.VerticalContentAlignment = VerticalAlignment.Bottom;
            this.BorderThickness = new Thickness(0);
            this.Margin = new System.Windows.Thickness(0, 0, 1, 1);
           // string xaml = XamlWriter.Save(this.Style);
            //  this.Padding = new Thickness(0, 1, 1, 0);
        }

        protected override void GnosisGridTextField_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Caption"))
            {
                this.Text = Caption;
            }
            else
            {
                base.GnosisGridTextField_PropertyChanged(sender, e);

            }
        }

        public IGnosisGridHeaderFieldImplementation GetClone()
        {
            return (GnosisGridHeaderField)this.Clone();
        }


        public void RemoveBorder()
        {
            this.BorderThickness = new Thickness(0);
        }
      

        public void SetMargin(int left, int top, int right, int bottom)
        {
            throw new NotImplementedException();
        }

     

        public void SetPaddingHorizontal(int paddingHorizontal)
        {
            this.Padding = new System.Windows.Thickness(paddingHorizontal, this.Padding.Top, paddingHorizontal, this.Padding.Bottom);
        }

    

    }
}
