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
using ShivaShared3.BaseControllers;
using ShivaShared3.Interfaces;
using ShivaWPF3.UtilityWPF;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisListField.xaml
    /// </summary>
    public partial class GnosisListField : Border, IGnosisListFieldImplementation
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
           typeof(int), typeof(GnosisListField), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisListField panelField = source as GnosisListField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin
                marginHorizontal = panelField.Margin.Left - newThickness;
                marginVertical = panelField.Margin.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase margin
                marginHorizontal = panelField.Margin.Left + oldThickness;
                marginVertical = panelField.Margin.Top + oldThickness;
            }

            panelField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
            panelField.BorderThickness = new Thickness(newThickness);

        }

        public GnosisListField()
        {
            InitializeComponent();

            this.MouseEnter += GnosisListField_MouseEnter;
            this.MouseLeave += GnosisListField_MouseLeave;
            this.MouseDown += GnosisListField_MouseDown;
            this.MouseUp += GnosisListField_MouseUp;

            this.PropertyChanged += GnosisListField_PropertyChanged;
        }

        private void GnosisListField_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HasMouseDown = false;
        }

        private void GnosisListField_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HasMouseDown = true;
        }

        private void GnosisListField_MouseLeave(object sender, MouseEventArgs e)
        {
            HasMouseFocus = false;
        }

        private void GnosisListField_MouseEnter(object sender, MouseEventArgs e)
        {
            HasMouseFocus = true;
        }

        private void GnosisListField_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Caption":
                    break;
                case "ContentVerticalAlignment":
                    lstBox.SetVerticalContentAlignmentExt(contentVerticalAlignment);
                    break;
                case "ContentHorizontalAlignment":
                    lstBox.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
                    break;
                case "Hidden":
                    this.SetVisibleExt(!hidden);
                    break;
                case "Locked":
                    if (!readOnly)
                    {
                        lstBox.IsEnabled = !locked;
                    }
                    break;
                case "ReadOnly":
                    lstBox.IsEnabled = !readOnly;
                    break;
                case "Tooltip":
                    this.Tooltip = tooltip;
                    break;

            }
        }

        public double GetPaddingHorizontal()
        {
            return lstBox.Padding.Left;
        }


        public void LoadListOptionImplementations(List<IGnosisComboOptionImplementation> optionImplementations)
        {
            throw new NotImplementedException();
        }

        public void SetGotFocusHandler(Action action)
        {
            GotFocusHandler = action;
            this.GotFocus += GnosisListField_GotFocus;
        }

        private void GnosisListField_GotFocus(object sender, RoutedEventArgs e)
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
            lstBox.SetHorizontalAlignmentExt(horizontalAlignment);
        }

        public void SetLostFocusHandler(Action action)
        {
            LostFocusHandler = action;
            this.LostFocus += GnosisListField_LostFocus;
        }

        private void GnosisListField_LostFocus(object sender, RoutedEventArgs e)
        {
            HasFocus = false;
            LostFocusHandler.Invoke();
        }

        public void SetPaddingHorizontal(double paddingHorizontal)
        {
            lstBox.SetHorizontalPaddingExt(paddingHorizontal);
        }

        public void SetPaddingVertical(double paddingVertical)
        {
            lstBox.SetVerticalPaddingExt(paddingVertical);
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
            lstBox.SetVerticalAlignmentExt(verticalAlignment);
        }

        public void SetWidth(double width)
        {
            this.Width = width;
        }
    }
}
