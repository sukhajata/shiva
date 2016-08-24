using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using Shiva.Shared.ContainerControllers;
using GnosisControls;

namespace ShivaWPF3.LayoutFeatures
{
    public class VerticalGridSplitter : GridSplitter
    {
        private GnosisSplit parentSplit;

        public VerticalGridSplitter(GnosisSplit _parentSplit)
        {
            parentSplit = _parentSplit;
            this.Width = 3;
            this.Background = Brushes.LightGray;
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.DragCompleted += VerticalGridSplitter_DragCompleted;
        }

        private void VerticalGridSplitter_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (parentSplit is GnosisPrimarySplit)
            {
                ((GnosisPrimarySplit)parentSplit).SplitterMoved(GnosisSplitController.DirectionType.VERTICAL);
            }
            else
            {
                parentSplit.SplitterMoved(GnosisSplitController.DirectionType.VERTICAL);
            }
        }
    }
}
