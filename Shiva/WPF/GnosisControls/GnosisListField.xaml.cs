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
using System.ComponentModel;
using Shiva.Shared.Data;

namespace GnosisControls
{
    /// <summary>
    /// Interaction logic for GnosisListField.xaml
    /// </summary>
    public partial class GnosisListField : Border, IGnosisListFieldImplementation, INotifyPropertyChanged
    {
        private List<GnosisComboOption> listOptions;

        private bool hasFocus;
        private bool hasMouseFocus;
        private bool hasMouseDown;
        private bool optional;

        private string caption;
        private GnosisCaptionLabel captionLabel;
        private GnosisController.VerticalAlignmentType contentVerticalAlignment;
        private GnosisController.HorizontalAlignmentType contentHorizontalAlignment;
        private string controlType;
        private bool datasetCreated;
        private bool datasetUpdated;
        private bool datasetDeleted;
        private string dataset;
        private string datasetItem;
        private string gnosisName;
        private IGnosisVisibleControlImplementation gnosisParent;
        private bool hasScrollBar;
        private bool hidden;
        private string icon;
        private int id;
        private bool locked;
        private int maxChars;
        private int maxDisplayChars;
        private int maxTextDisplayWidthChars;
        private int maxSectionSpan;
        private int minDisplayChars;
        private int minTextDisplayWidthChars;
        private int order;
        private bool readOnly;
        private string tooltip;
        private string valueField;
        private int variableControlID;
        private int variableSystemID;
        private bool variableIsInput;
        private bool variableIsOutput;

        public GnosisCaptionLabel CaptionLabel
        {
            get { return captionLabel; }
            set { captionLabel = value; }
        }

        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;
                OnPropertyChanged("HasFocus");
            }
        }
        public bool HasMouseFocus
        {
            get { return hasMouseFocus; }
            set
            {
                hasMouseFocus = value;
                OnPropertyChanged("HasMouseFocus");
                // string xaml = XamlWriter.Save(this);
            }
        }

        public bool HasMouseDown
        {
            get { return hasMouseDown; }
            set
            {
                hasMouseDown = value;
                OnPropertyChanged("HasMouseDown");
            }
        }

        [GnosisProperty]
        public bool HasScrollBar
        {
            get
            {
                return hasScrollBar;
            }

            set
            {
                hasScrollBar = value;
            }
        }

        [GnosisPropertyAttribute]
        public string ContentVerticalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.VerticalAlignmentType), contentVerticalAlignment);
            }
            set
            {
                try
                {
                    _ContentVerticalAlignment = (GnosisController.VerticalAlignmentType)Enum.Parse(typeof(GnosisController.VerticalAlignmentType), value.ToUpper());
                   // OnPropertyChanged("ContentVerticalAlignment");
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.VerticalAlignmentType _ContentVerticalAlignment
        {
            get { return contentVerticalAlignment; }
            set
            {
                contentVerticalAlignment = value;
                lstBox.SetVerticalContentAlignmentExt(contentVerticalAlignment);
            }
        }

        [GnosisPropertyAttribute]
        public string ContentHorizontalAlignment
        {
            get
            {
                return Enum.GetName(typeof(GnosisController.HorizontalAlignmentType), contentHorizontalAlignment);
            }
            set
            {
                try
                {
                    _ContentHorizontalAlignment = (GnosisController.HorizontalAlignmentType)Enum.Parse(typeof(GnosisController.HorizontalAlignmentType), value.ToUpper());
                    //OnPropertyChanged("ContentHorizontalAlignment");
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
            }
        }

        public GnosisController.HorizontalAlignmentType _ContentHorizontalAlignment
        {
            get { return contentHorizontalAlignment; }
            set
            {
                contentHorizontalAlignment = value;
                lstBox.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
            }
        }

        [GnosisPropertyAttribute]
        public string ControlType
        {
            get
            {
                return controlType;
            }

            set
            {
                controlType = value;
            }
        }

        [GnosisPropertyAttribute]
        public bool DatasetCreated
        {
            get
            {
                return datasetCreated;
            }

            set
            {
                datasetCreated = value;
                OnPropertyChanged("DatasetCreated");
            }
        }

        [GnosisPropertyAttribute]
        public bool DatasetUpdated
        {
            get
            {
                return datasetUpdated;
            }

            set
            {
                datasetUpdated = value;
                OnPropertyChanged("DatasetUpdated");
            }
        }

        [GnosisPropertyAttribute]
        public bool DatasetDeleted
        {
            get
            {
                return datasetDeleted;
            }

            set
            {
                datasetDeleted = value;
                OnPropertyChanged("DatasetDeleted");
            }
        }



        [GnosisPropertyAttribute]
        public string Dataset
        {
            get
            {
                return dataset;
            }

            set
            {
                dataset = value;
                //OnPropertyChanged("Dataset");
            }
        }

        [GnosisPropertyAttribute]
        public string DatasetItem
        {
            get
            {
                return datasetItem;
            }

            set
            {
                datasetItem = value;
                // OnPropertyChanged("DatasetItem");
            }
        }

        [GnosisPropertyAttribute]
        public string Caption
        {
            get
            {
                return caption;
            }

            set
            {
                caption = value;
                OnPropertyChanged("Caption");
            }
        }

        public IGnosisVisibleControlImplementation GnosisParent
        {
            get { return gnosisParent; }
            set { gnosisParent = value; }
        }


        [GnosisPropertyAttribute]
        public bool Hidden
        {
            get
            {
                return hidden;
            }

            set
            {
                hidden = value;
                this.SetVisibleExt(!hidden);
                OnPropertyChanged("Hidden");
            }
        }

        [GnosisPropertyAttribute]
        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                // OnPropertyChanged("ID");
            }
        }




        [GnosisPropertyAttribute]
        public int MinDisplayChars
        {
            get
            {
                return minDisplayChars;
            }
            set
            {
                minDisplayChars = value;
                OnPropertyChanged("MinDisplayChars");
            }
        }

        [GnosisPropertyAttribute]
        public int MaxDisplayChars
        {
            get
            {
                return maxDisplayChars;
            }
            set
            {
                maxDisplayChars = value;
                OnPropertyChanged("MaxDisplayChars");
            }
        }

        [GnosisPropertyAttribute]
        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
                //OnPropertyChanged("Order");
            }
        }

        [GnosisPropertyAttribute]
        public string Tooltip
        {
            get
            {
                return tooltip;
            }

            set
            {
                tooltip = value;
                this.Tooltip = tooltip;
            }
        }

        [GnosisPropertyAttribute]
        public int MaxSectionSpan
        {
            get
            {
                return maxSectionSpan;
            }

            set
            {
                maxSectionSpan = value;
            }
        }


        [GnosisPropertyAttribute]
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }

            set
            {
                readOnly = value;
                if (readOnly)
                {
                    lstBox.IsEnabled = false;
                    locked = true;
                    OnPropertyChanged("Locked");
                }
            }
        }

        [GnosisPropertyAttribute]
        public bool Locked
        {
            get { return locked; }
            set
            {
                if (!readOnly)
                {
                    locked = value;
                    lstBox.IsEnabled = !locked;
                    OnPropertyChanged("Locked");
                }
                
            }
        }

        [GnosisPropertyAttribute]
        public bool Optional
        {
            get { return optional; }
            set
            {
                optional = value;
                OnPropertyChanged("Optional");
            }
        }

        [GnosisPropertyAttribute]
        public string Value
        {
            get
            {
                return valueField;
            }

            set
            {
                valueField = value;
            }
        }
        [GnosisProperty]
        public string GnosisName
        {
            get
            {
                return gnosisName;
            }

            set
            {
                gnosisName = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public void GnosisAddChild(IGnosisObject child)
        {
            listOptions.Add((GnosisComboOption)child);
        }

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

        public int CurrentThickness
        {
            get
            {
                return (int)this.BorderThickness.Top;
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
            GnosisListField listField = source as GnosisListField;
            int newThickness = (int)e.NewValue;
            int oldThickness = (int)e.OldValue;
            double marginHorizontal;
            double marginVertical;

            if (newThickness > oldThickness)
            {
                //increase border thickness, decrease margin
                marginHorizontal = listField.Margin.Left - newThickness;
                marginVertical = listField.Margin.Top - newThickness;
            }
            else
            {
                //decrease border thickness, increase margin
                marginHorizontal = listField.Margin.Left + oldThickness;
                marginVertical = listField.Margin.Top + oldThickness;
            }

            if (marginHorizontal >= 0 && marginVertical >= 0)
            {
                listField.Margin = new Thickness(marginHorizontal, marginVertical, marginHorizontal, marginVertical);
                listField.BorderThickness = new Thickness(newThickness);

                double fieldHeight = GlobalData.Singleton.StyleHelper.GetFieldHeight(listField, listField.lstBox.FontFamily.ToString(),
                    (int)listField.lstBox.FontSize);
                listField.SetHeight(fieldHeight);
            }

        }

        public GnosisListField()
        {
            InitializeComponent();

            this.MouseEnter += GnosisListField_MouseEnter;
            this.MouseLeave += GnosisListField_MouseLeave;
            this.MouseDown += GnosisListField_MouseDown;
            this.MouseUp += GnosisListField_MouseUp;

           // this.PropertyChanged += GnosisListField_PropertyChanged;
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

        //private void GnosisListField_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "Caption":
        //            break;
        //        case "ContentVerticalAlignment":
        //            lstBox.SetVerticalContentAlignmentExt(contentVerticalAlignment);
        //            break;
        //        case "ContentHorizontalAlignment":
        //            lstBox.SetHorizontalContentAlignmentExt(contentHorizontalAlignment);
        //            break;
        //        case "Hidden":
        //            this.SetVisibleExt(!hidden);
        //            break;
        //        case "Locked":
        //            if (!readOnly)
        //            {
        //                lstBox.IsEnabled = !locked;
        //            }
        //            break;
        //        case "ReadOnly":
        //            lstBox.IsEnabled = !readOnly;
        //            break;
        //        case "Tooltip":
        //            this.Tooltip = tooltip;
        //            break;

        //    }
        //}

        //public double GetPaddingHorizontal()
        //{
        //    return lstBox.Padding.Left;
        //}


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

        //public void SetPaddingHorizontal(double paddingHorizontal)
        //{
        //    lstBox.SetHorizontalPaddingExt(paddingHorizontal);
        //}

        //public void SetPaddingVertical(double paddingVertical)
        //{
        //    lstBox.SetVerticalPaddingExt(paddingVertical);
        //}

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
