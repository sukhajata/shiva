using GnosisControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisPanelImplementation : IGnosisInnerLayoutControlImplementation, IGnosisSpacingPossessor
    {
        string CaptionRelativePosition { get; set; }
        string CaptionAlignmentHorizontal { get; set; }
        string CaptionAlignmentVertical { get; set; }
        int NumColumns { get; set; }

        //void SetNumColumns(int numColumns);
        void Clear();
        void AddGnosisCaptionLabel(GnosisCaptionLabel captionLabel, int column, int row, int colSpan, int rowSpan);
        void AddGnosisContentControlImplementation(IGnosisContentControlImplementation contentControlImplementation, int column, int row, int colSpan, int rowSpan);
        void AddGnosisLayoutControlImplementation(IGnosisInnerLayoutControlImplementation layoutControlImplementation, int column, int row, int colSpan, int rowSpan);
        void AddRowAutoHeight();
        void AddRowFixedHeight(double height);
        void AddGnosisDateTimeFieldImplementation(IGnosisDateTimeFieldImplementation ControlImplementation, int column, int row, int colSpan, int rowSpan);
        double GetColumnWidth(int currentCol);
        void SetLoadedHandler(Action<double> action);
        void SetIsVisibleChangedHandler(Action<bool> action);
        void SetWidthChangedHandler(Action<double> action);
        void SetRowAutoHeight(int currentRow);
        void SetRowFixedHeight(int row, double newFieldHeight);
        void SetHeightChangedHandler(Action<double> action);
        void AddHorizontalSpacing(int column, int row, int colSpan, int rowSpan);
    }
}
