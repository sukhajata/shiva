using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using ShivaShared3.ContainerControllers;
using GnosisControls;

namespace ShivaWPF3.LayoutFeatures
{
    public class HorizontalGridSplitter : GridSplitter
    {
        private GnosisSplit parent;
        public HorizontalGridSplitter(GnosisSplit _parent) : base()
        {
            parent = _parent;
            this.Height = 3;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Center;
            this.Background = Brushes.LightGray;
            this.DragCompleted += HorizontalGridSplitter_DragCompleted;
        }

        /// <summary>
        /// Update the SplitPercentage in parent Split after the GridSplitter has been moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorizontalGridSplitter_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            parent.SplitterMoved(GnosisSplitController.DirectionType.HORIZONTAL);

        }
    }
}
