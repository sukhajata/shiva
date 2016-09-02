using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Shiva.Shared.Interfaces;
using Shiva.Shared.BaseControllers;


namespace GnosisControls
{
    public partial class GnosisContainerTreeViewItem : TreeViewItem, IGnosisContainerTreeViewItemImplementation
    {
        private string caption;
        private string gnosisIcon;
        private GnosisVisibleController tag;

        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                lblCaption.Content = caption;
            }
        }

        public string GnosisIcon
        {
            get { return gnosisIcon; }
            set
            {
                gnosisIcon = value;
                img.Margin = new Thickness(2);
                img.Source = new BitmapImage(new Uri(@"pack://application:,,/Icons/" + gnosisIcon + "-25.png"));
            }
        }

        public GnosisVisibleController GnosisTag
        {
            get { return tag; }
            set
            {
                tag = value;
                this.Tag = tag;
            }
        }

        private Grid gdBorder;
        private Label lblCaption;
        private StackPanel pnlContent;
        private Image img;

        public GnosisContainerTreeViewItem() : base()
        {
            this.IsExpanded = true;
            this.Margin = new Thickness(0, 4, 0, 4);
            

            //TreeViewItem consists of a StackPanel containing an icon and a label. The icon is wrapped
            //in a grid to allow for changing of the icon 'border'.
            pnlContent = new StackPanel();
            pnlContent.Orientation = Orientation.Horizontal;
            gdBorder = new Grid();
           
            lblCaption = new Label();
            img = new Image();
            Grid.SetColumn(img, 0);
            Grid.SetRow(img, 0);

            gdBorder.Children.Add(img);
            pnlContent.Children.Add(gdBorder);
            pnlContent.Children.Add(lblCaption);

            this.Header = pnlContent;

           // this.PropertyChanged += GnosisContainerTreeViewItem_PropertyChanged;

        }

        //private void GnosisContainerTreeViewItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    switch(e.PropertyName)
        //    {
        //        case "Caption":
        //            lblCaption.Content = caption;
        //            break;
        //        case "GnosisIcon":
        //            img.Margin = new Thickness(2);
        //            img.Source = new BitmapImage(new Uri(@"pack://application:,,/Icons/" + gnosisIcon + "-25.png"));
        //            break;
        //        case "GnosisTag":
        //            this.Tag = GnosisTag;
        //            break;

        //    }
        //}

        //public void SetTag(GnosisVisibleController controller)
        //{
        //    this.Tag = controller;
        //}

        //public void SetIconName(string iconName)
        //{
            
        //    img.Margin = new Thickness(2);
        //    img.Source = new BitmapImage(new Uri(@"pack://application:,,/Icons/" + iconName + "-25.png"));

        //}

        //public void SetCaption(string caption)
        //{
        //    lblCaption.Content = caption;
        //}

        public void UnHighlight()
        {
            gdBorder.Background = Brushes.Transparent;
        }

        public void Highlight()
        {
            gdBorder.Background = (Brush)Application.Current.FindResource("BorderHighlightColor");
        }

        public void AddItem(IGnosisContainerTreeViewItemImplementation item)
        {
            this.Items.Add((GnosisContainerTreeViewItem)item);
        }
    }
}
