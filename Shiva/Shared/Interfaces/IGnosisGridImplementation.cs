using System;
using Shiva.Shared.BaseControllers;


namespace Shiva.Shared.Interfaces
{
    public interface IGnosisGridImplementation : IGnosisInnerLayoutControlImplementation, IGnosisSpacingPossessor,
        IGnosisMarginPossessor//, IGnosisBorderThicknessPossessor
    {
        //properties
        bool GridIsLoaded { get;  }
        bool GridIsVisible { get;  }
        string CaptionRelativePosition { get; set; }
        string CaptionAlignmentHorizontal { get; set; }
        string CaptionAlignmentVertical { get; set; }
        string Dataset { get; set; }
        int LineThickness { get; set; }
        int MinDisplayRows { get; set; }
        int MaxDisplayRows { get; set; }
        int MaxLines { get; set; }
        bool MultipleRowSelection { get; set; }
        int NumColumns { get; set; }
        bool ReadOnly { get; set; }


        //methods
        //void SetNumColumns(int numColumns);
        //void SetLocked(bool locked);
        //void SetMultipleRowSelectionEnabled(bool multipleRowSelect);
        //void SetCaptionRelativePosition(GnosisController.CaptionPosition captionRelativePosition);
       // void SetNumRows(int numRows);
        void LoadCell(IGnosisGridFieldImplementation txt, int column, int row, int colSpan, int rowSpan, bool alternateRowColour);
        void LoadCell(IGnosisGridHeaderFieldImplementation header, int col, int row, int colSpan, int rowSpan);
        void LoadCell(IGnosisCaptionLabelImplementation header, int col, int row, int colSpan, int rowSpan);
        void Clear();
        void CheckColumnCount(int count);

        //void AddButtonColumnImplementation(IGnosisButtonColumnImplementation buttonColumnImplementation);
        //void AddCheckColumnImplementation(IGnosisCheckColumnImplementation checkColumnImplementation);
        //void AddComboColumnImplementation(IGnosisComboColumnImplementation comboColumnImplementation);
        //void AddDateTimeColumnImplementation(IGnosisDateTimeColumnImplementation dateTimeColumnImplementation);
        //void AddJoinColumnImplementation(IGnosisJoinColumnImplementation joinColumnImplementation);
        void AddGridHeader(IGnosisGridHeaderFieldImplementation gridHeaderFieldImplementation, int column, int row, int colSpan, int rowSpan, bool columnarFormat);
        void SetWidthChangedHandler(Action<double> action);
        //void AddColumn(int textLength);
        //double GetColumnWidth(int colNo);
        void AddColumn();
        void SetLoadedHandler(Action action);
        void SetIsVisibleChangedHandler(Action<bool> action);
        void SetColumnarFormat(bool v);
        void SetHeight(double v);
        void AddRow(double rowHeight);
        void AddHeaderRow(double rowHeight);
        void AddHeaderRow();
        void AddRowAutoHeight();
        double GetAvailableHeight();
    }
}
