using GnosisControls;
using ShivaShared3.ContentControllers;
using ShivaShared3.Data;
using ShivaShared3.InnerLayoutControllers;
using ShivaShared3.Interfaces;
using ShivaShared3.OuterLayoutControllers;
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

namespace ShivaWPF3.UtilityWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShivaDesigner : Window
    {
        private GnosisTile mainTile;
        private GnosisFrameController currentFrameController;

        public ShivaDesigner()
        {
            InitializeComponent();

            List<GnosisConnectionFrameController> connectionFrames = GlobalData.Singleton.SystemController.GetConnectionFrames();
            foreach (GnosisConnectionFrameController frameController in connectionFrames)
            {
                LoadConnectionFrame(frameController);
            }

            mainTile = new GnosisTile();
            pnlFrame.Children.Add(mainTile);
        }

        private void LoadConnectionFrame(GnosisConnectionFrameController connectionFrame)
        {
            ListBoxItem item = new ListBoxItem();
            item.Content = ((GnosisConnectionFrame)connectionFrame.ControlImplementation).Caption;
            item.Tag = connectionFrame;
            lstFrames.Items.Add(item);

        }

        private void txtColumns_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPresentationRowFactor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCellSpan_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtMinRows_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtMaxRows_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtMinColumns_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtMaxColumns_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void chkPlacementPriorityDown_Checked(object sender, RoutedEventArgs e)
        {

        }


        private void lstFrames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)lstFrames.SelectedItem;
            currentFrameController = (GnosisFrameController)item.Tag;
            currentFrameController.SetEditMode();

            //register for property changes, include child panels. Content of panels can change so 
            //we must register for their child property changes on the panel loaded event
            currentFrameController.PropertyChanged += Controller_PropertyChanged;
            foreach (GnosisInnerLayoutController child in currentFrameController.ChildControllers)
            {
                child.PropertyChanged += Controller_PropertyChanged;
            }

            //List<GnosisFramePresentation> presentations = currentFrameController.FramePresentations;
            //pnlButtonFormats.Children.Clear();
            //lstLabels = new List<Label>();
            //foreach (GnosisFramePresentation framePresentation in presentations)
            //{
            //    Label lblPresentation = new Label();
            //    lblPresentation.Tag = framePresentation;
            //   // btnPresentation.Click += BtnPresentation_Click;
            //    lblPresentation.Content = framePresentation.Format.ToString();
            //    if (framePresentation.Format == currentFrameController.CurrentPresentation.Format)
            //    {
            //        //highlight current format
            //        lblPresentation.Background = Brushes.Lime;

            //        //update fields
            //        UpdateFramePresentationFields(framePresentation);
            //    }
            //    lstLabels.Add(lblPresentation);
            //    pnlButtonFormats.Children.Add(lblPresentation);
            //}

            mainTile.Clear();
            mainTile.LoadFrameImplementation((IGnosisFrameImplementation)currentFrameController.ControlImplementation);
        }

        private void Controller_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (e.PropertyName.Equals("CurrentPresentation"))
            //{
            //    currentFrameController.SetEditMode();
            //    foreach (Label lbl in lstLabels)
            //    {
            //        GnosisFramePresentation framePresentation = (GnosisFramePresentation)lbl.Tag;
            //        if (framePresentation.Format == currentFrameController.CurrentPresentation.Format)
            //        {
            //            lbl.Background = Brushes.Lime;
            //            UpdateFramePresentationFields(framePresentation);
            //        }
            //        else
            //        {
            //            lbl.Background = Brushes.Transparent;
            //        }
            //    }
            //}
            if (e.PropertyName.Equals("Loaded"))
            {
                if (sender is GnosisPanelController)
                {
                    foreach (GnosisContentController panelChild in ((GnosisPanelController)sender).ChildControllers)
                    {
                        panelChild.PropertyChanged += Controller_PropertyChanged;
                    }
                }
            }
            else if (e.PropertyName.Equals("HasMouseDown"))
            {
                if (!(sender is GnosisFrameController || sender is GnosisPanelController))
                {
                    UpdateControlFields(((GnosisContentController)sender));
                }
            }

        }

        private void UpdateControlFields(GnosisContentController controller)
        {
            lblField.Content = controller.ControlImplementation.GnosisName;

            Binding bdCellSpan = new Binding();
            bdCellSpan.Mode = BindingMode.TwoWay;
            bdCellSpan.Path = new PropertyPath("TextLength");
            bdCellSpan.Source = controller;
            txtTextLength.SetBinding(TextBox.TextProperty, bdCellSpan);

            Binding bdOrder = new Binding();
            bdOrder.Mode = BindingMode.TwoWay;
            bdOrder.Path = new PropertyPath("Order");
            bdOrder.Source = controller;
            txtFieldOrder.SetBinding(TextBox.TextProperty, bdOrder);

            //Binding bdMinRows = new Binding();
            //bdMinRows.Mode = BindingMode.TwoWay;
            //bdMinRows.Path = new PropertyPath("MinRows");
            //bdMinRows.Source = controller;
            //txtMinRows.SetBinding(TextBox.TextProperty, bdMinRows);

            //Binding bdMaxRows = new Binding();
            //bdMaxRows.Mode = BindingMode.TwoWay;
            //bdMaxRows.Path = new PropertyPath("MaxRows");
            //bdMaxRows.Source = controller;
            //txtMaxRows.SetBinding(TextBox.TextProperty, bdMaxRows);

            //Binding bdMinColumns = new Binding();
            //bdMinColumns.Mode = BindingMode.TwoWay;
            //bdMinColumns.Path = new PropertyPath("MinColumns");
            //bdMinColumns.Source = controller;
            //txtMinColumns.SetBinding(TextBox.TextProperty, bdMinColumns);

            //Binding bdMaxColumns = new Binding();
            //bdMaxColumns.Mode = BindingMode.TwoWay;
            //bdMaxColumns.Path = new PropertyPath("MaxColumns");
            //bdMaxColumns.Source = controller;
            //txtMaxColumns.SetBinding(TextBox.TextProperty, bdMaxColumns);

            //Binding bdPlacementPriority = new Binding();
            //bdPlacementPriority.Mode = BindingMode.TwoWay;
            //bdPlacementPriority.Path = new PropertyPath("PlacementPriorityDown");
            //bdPlacementPriority.Source = controller;
            //chkPlacementPriorityDown.SetBinding(CheckBox.IsCheckedProperty, bdPlacementPriority);

        }

        //private void UpdateFramePresentationFields(GnosisFramePresentation framePresentation)
        //{


        //    //minWidth
        //    Binding binding = new Binding();
        //    binding.Mode = BindingMode.TwoWay;
        //    binding.Path = new PropertyPath("MinWidth");
        //    binding.Source = framePresentation;
        //    txtMinWidth.SetBinding(TextBox.TextProperty, binding);

        //    //maxWidth
        //    Binding binding2 = new Binding();
        //    binding2.Mode = BindingMode.TwoWay;
        //    binding2.Path = new PropertyPath("MaxWidth");
        //    binding2.Source = framePresentation;
        //    txtMaxWidth.SetBinding(TextBox.TextProperty, binding2);
        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //remove controls
            mainTile.Clear();
        }
    }
}
