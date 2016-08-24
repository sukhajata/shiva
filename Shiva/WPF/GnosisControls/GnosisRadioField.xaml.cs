using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shiva.Shared.BaseControllers;
using Shiva.Shared.Interfaces;
using ShivaWPF3.UtilityWPF;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisRadioField.xaml
    /// </summary>
    public partial class GnosisRadioField : RadioButton, IGnosisRadioFieldImplementation
    {
        protected Action GotFocusHandler;
        protected Action LostFocusHandler;

        protected int horizontalPadding;
        protected int verticalPadding;
        protected int horizontalMargin;
        protected int verticalMargin;

        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set
            {
                horizontalPadding = value;
                this.SetHorizontalPaddingExt(horizontalPadding);
            }
        }

        public int VerticalPadding
        {
            get { return verticalPadding; }
            set
            {
                verticalPadding = value;
                this.SetVerticalPaddingExt(verticalPadding);
            }
        }

        public int HorizontalMargin
        {
            get { return horizontalMargin; }
            set
            {
                horizontalMargin = value;
                this.SetHorizontalMarginExt(horizontalMargin);
            }
        }

        public int VerticalMargin
        {
            get { return verticalMargin; }
            set
            {
                verticalMargin = value;
                this.SetVerticalMarginExt(verticalMargin);
            }
        }

        public static readonly DependencyProperty ControlThicknessProperty =
           DependencyProperty.RegisterAttached("ControlThickness",
           typeof(int), typeof(GnosisRadioField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
        //new FrameworkPropertyMetadata(0,
        //    FrameworkPropertyMetadataOptions.Inherits));

        public static void SetControlThickness(UIElement element, int value)
        {
            element.SetValue(ControlThicknessProperty, value);
        }

        public static int GetControlThickness(UIElement element)
        {
            return (int)element.GetValue(ControlThicknessProperty);
        }

        public static void ControlThicknessPropertyChanged(object source, DependencyPropertyChangedEventArgs e)
        {
            GnosisRadioField panelField = source as GnosisRadioField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double paddingHorizontal;
            double paddingVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease padding
                paddingHorizontal = panelField.Padding.Left - newThickness;
                paddingVertical = panelField.Padding.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase padding
                paddingHorizontal = panelField.Padding.Left + oldThickness;
                paddingVertical = panelField.Padding.Top + oldThickness;
            }

            panelField.Padding = new Thickness(paddingHorizontal, paddingVertical, paddingHorizontal, paddingVertical);
            panelField.BorderThickness = new Thickness(newThickness);

        }


        public GnosisRadioField()
        {
            InitializeComponent();

            this.MouseEnter += GnosisRadioField_MouseEnter;
            this.MouseLeave += GnosisRadioField_MouseLeave;
            this.MouseDown += GnosisRadioField_MouseDown;
            this.MouseUp += GnosisRadioField_MouseUp;

            this.PropertyChanged += GnosisRadioField_PropertyChanged;
        }

        private void GnosisRadioField_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    this.Content = caption;
                    break;
                case "ContentHorizontalAlignment":
                    this.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Locked":
                    if (!readOnly)
                    {
                        this.IsEnabled = !locked;
                    }
                    break;
                case "ReadOnly":
                    this.IsEnabled = !readOnly;
                    break;
                case "Tooltip":
                    this.ToolTip = tooltip;
                    break;
            }
            
        }

        private void GnosisRadioField_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HasMouseDown = false;
        }

        private void GnosisRadioField_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HasMouseDown = true;
        }

        private void GnosisRadioField_MouseLeave(object sender, MouseEventArgs e)
        {
            this.HasMouseFocus = false;
        }

        private void GnosisRadioField_MouseEnter(object sender, MouseEventArgs e)
        {
            HasMouseFocus = true;
        }

        public double GetPaddingHorizontal()
        {
            return this.Padding.Left;
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisRadioField_GotFocus;
        }

        private void GnosisRadioField_GotFocus(object sender, RoutedEventArgs e)
        {
            HasFocus = true;
            GotFocusHandler.Invoke();
        }

        public void SetHeight(double fieldHeight)
        {
            this.Height = fieldHeight;
        }

        public void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType horizontalAlignment)
        {
            this.SetHorizontalAlignmentExt(horizontalAlignment);
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisRadioField_LostFocus;
        }

        private void GnosisRadioField_LostFocus(object sender, RoutedEventArgs e)
        {
            HasFocus = false;
            LostFocusHandler.Invoke();

        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            this.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            this.SetVerticalPaddingExt(paddingVertical);
        }

        public void SetStrikethrough(bool strikethrough)
        {
            throw new NotImplementedException();
        }

        public void SetTooltipVisible(bool visible)
        {
            ToolTipService.SetIsEnabled(this, visible);
        }

        public void SetVerticalAlignment(GnosisController.VerticalAlignmentType verticalAlignment)
        {
            this.SetVerticalAlignmentExt(verticalAlignment);
        }

        public void SetWidth(double width)
        {
            this.Width = width;
        }
    }
}
