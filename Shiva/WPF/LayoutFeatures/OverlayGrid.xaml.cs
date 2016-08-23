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

namespace ShivaWPF3.LayoutFeatures
{
    /// <summary>
    /// Interaction logic for OverlayGrid.xaml
    /// </summary>
    public partial class OverlayGrid : Grid
    {
        public OverlayGrid()
        {
            InitializeComponent();

            //make this overlay fill all available space
            Grid.SetColumn(this, 0);
            Grid.SetRow(this, 0);
            Grid.SetColumnSpan(this, 3);
            Grid.SetRowSpan(this, 3);

        }

        public void SetTileOrder(int visibleOrder)
        {
            //show the VisibleSplitOrder in the middle of the grid
            lblVisibleTileOrder.Content = visibleOrder.ToString();
        }
    }
}
