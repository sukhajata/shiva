using GnosisControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Shiva.Shared.Interfaces;
using System.Windows;

namespace GnosisControls
{
    public class GnosisGridColumn : GnosisContentControl, INotifyPropertyChanged, IGnosisPaddingPossessor, 
        IGnosisControlThicknessPossessor
    {
        private bool rowSelected;
        private bool isEvenRow;
        private bool locked;
        private bool readOnly;
        private string valueField;

        private int horizontalPadding;
        private int verticalPadding;
        private int currentThickness;


        [GnosisProperty]
        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }


        [GnosisProperty]
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
            }
        }


        public bool RowSelected
        {
            get { return rowSelected; }
            set { rowSelected = value; }
        }

        [GnosisProperty]
        public bool IsEvenRow
        {
            get { return isEvenRow; }
            set { isEvenRow = value; }
        }


        [GnosisProperty]
        public string Value
        {
            get { return valueField; }
            set { valueField = value; }
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
            }
        }

        public int CurrentThickness
        {
            get
            {
                return currentThickness;
            }
        }

        public static readonly DependencyProperty ControlThicknessProperty =
            DependencyProperty.RegisterAttached("ControlThickness",
            typeof(int), typeof(GnosisGridColumn), new FrameworkPropertyMetadata(ControlThicknessPropertyChanged));
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
            GnosisGridColumn gridColumn = source as GnosisGridColumn;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double newHorizontalPadding;
            double newVerticalPadding;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease padding
                newHorizontalPadding = gridColumn.HorizontalPadding - newThickness;
                newVerticalPadding = gridColumn.VerticalPadding - newThickness;
            }
            else
            {
                //decrease border thickness, increase padding
                newHorizontalPadding = gridColumn.HorizontalPadding + oldThickness;
                newVerticalPadding = gridColumn.VerticalPadding + oldThickness;
            }

            if (newHorizontalPadding >= 0 && newVerticalPadding >= 0)
            {
                gridColumn.Margin = new Thickness(newHorizontalPadding, newVerticalPadding, newHorizontalPadding, newVerticalPadding);
                gridColumn.BorderThickness = new Thickness(newThickness);

                double fieldHeight = GlobalData.Singleton.StyleHelper.GetFieldHeight(gridColumn, gridColumn.FontFamily.ToString(),
                    (int)gridColumn.FontSize);
                gridColumn.SetHeight(fieldHeight);
            }

        }

    }
}
