using Shiva.Shared.ContentControllers;
using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.Interfaces;

namespace GnosisControls
{
    public class GnosisStyle : IGnosisObject
    {
        private int gnosisBorderThickness;

        private GnosisController.CaptionPosition captionRelativePosition;

        private int containerVerticalPadding;

        private int containerHorizontalPadding;

        private string name;

        private string font;

        private int fontSize;

        private int captionSpacing;

        private string controlColour;

        private int controlThickess;

        private string contentColour;

        private string backgroundColour;

        private bool isUnderline;

        private bool isOutlined;

		private int lineWidth;

        private string outlineColour;

        private bool isStrikethrough;

        private int horizontalPadding;

        private int verticalPadding;

        private int verticalMargin;

        private int horizontalMargin;

        private int horizontalSpacing;

        private int verticalSpacing;

        private GnosisController.CaptionPosition relativePosition;

        private List<GnosisStyleCondition> conditions;

        [GnosisProperty]
        public int GnosisBorderThickness
        {
            get { return gnosisBorderThickness; }
            set { gnosisBorderThickness = value; }
        }

        [GnosisProperty]
        public string CaptionRelativePosition
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.CaptionPosition), captionRelativePosition);
            }
            set
            {
                try
                {
                    captionRelativePosition = (GnosisController.CaptionPosition)Enum.Parse(typeof(GnosisController.CaptionPosition), value.ToUpper());
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.CaptionPosition _CaptionRelativePosition
        {
            get { return captionRelativePosition; }
            set { captionRelativePosition = value; }
        }

        [GnosisProperty]
        public int ContainerHorizontalPadding
        {
            get { return containerHorizontalPadding; }
            set { containerHorizontalPadding = value; }
        }

        [GnosisProperty]
        public int ContainerVerticalPadding
        {
            get { return containerVerticalPadding; }
            set { containerVerticalPadding = value; }
        }

        [GnosisProperty]
        public int ControlThickness
        {
            get { return controlThickess; }
            set { controlThickess = value; }
        }

        [GnosisProperty]
        public string GnosisName
        {
            get { return name; }
            set { name = value; }
        }

        [GnosisProperty]
        public string Font
        {
            get { return font; }
            set { font = value; }
        }

        [GnosisProperty]
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        [GnosisProperty]
        public int CaptionSpacing
        {
            get { return captionSpacing; }
            set { captionSpacing = value; }
        }

        [GnosisProperty]
        public string ControlColour
        {
            get { return controlColour; }
            set { controlColour = value; }
        }

        [GnosisProperty]
        public string ContentColour
        {
            get { return contentColour; }
            set { contentColour = value; }
        }

        [GnosisProperty]
        public string BackgroundColour
        {
            get { return backgroundColour; }
            set { backgroundColour = value; }
        }

        [GnosisCollection]
        public List<GnosisStyleCondition> Conditions
        {
            get { return conditions; }
            set { conditions = value; }
        }

        [GnosisProperty]
        public bool IsUnderline
        {
            get { return isUnderline; }
            set { isUnderline = value; }
        }

        [GnosisProperty]
        public int HorizontalMargin
        {
            get { return horizontalMargin; }
            set { horizontalMargin = value; }
        }

        [GnosisProperty]
        public int VerticalMargin
        {
            get { return verticalMargin; }
            set { verticalMargin = value; }
        }

        [GnosisProperty]
        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set { horizontalPadding = value; }
        }

        [GnosisProperty]
        public int VerticalPadding
        {
            get { return verticalPadding; }
            set { verticalPadding = value; }
        }

        [GnosisProperty]
        public int HorizontalSpacing
        {
            get { return horizontalSpacing; }
            set { horizontalSpacing = value; }
        }

        [GnosisProperty]
        public int VerticalSpacing
        {
            get { return verticalSpacing; }
            set { verticalSpacing = value; }
        }

        //[GnosisProperty]
        //public string RelativePosition
        //{
        //    get
        //    {
        //        return Enum.GetName(typeof(GnosisController.CaptionPosition), relativePosition);
        //    }
        //    set
        //    {
        //        try
        //        {
        //            relativePosition = (GnosisController.CaptionPosition)Enum.Parse(typeof(GnosisController.CaptionPosition), value.ToUpper());
        //        }
        //        catch (Exception ex)
        //        {
        //            GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //        }
        //    }
        //}

        //public GnosisController.CaptionPosition _RelativePosition
        //{
        //    get { return relativePosition; }
        //    set { relativePosition = value; }
        //}

        [GnosisProperty]
        public bool IsOutlined
        {
            get { return isOutlined; }
            set { isOutlined = value; }
        }

        [GnosisProperty]
        public string OutlineColour
        {
            get { return outlineColour; }
            set { outlineColour = value; }
        }

        [GnosisProperty]
        public bool IsStrikethrough
        {
            get { return isStrikethrough; }
            set { isStrikethrough = value; }
        }

		[GnosisProperty]
		public int LineWidth
		{
			get { return lineWidth;}
			set { lineWidth = value;}

		}

        public virtual void GnosisAddChild(IGnosisObject child)
        {
            if (conditions == null)
            {
                conditions = new List<GnosisStyleCondition>();
            }
            conditions.Add((GnosisStyleCondition)child);
        }
    }
}
