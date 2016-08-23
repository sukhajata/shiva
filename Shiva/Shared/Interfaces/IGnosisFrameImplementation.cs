using System;
using ShivaShared3.BaseControllers;
using GnosisControls;
using System.ComponentModel;
using System.Collections.Generic;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisFrameImplementation : IGnosisOuterLayoutControlImplementation, INotifyPropertyChanged,
        IContainerPaddingPossessor
    {
        //collections
        [GnosisCollection]
        List<GnosisCalendar> Calendars { get; set; }

        [GnosisCollection]
        List<GnosisMessageGrid> MessageGrids { get; set; }

        [GnosisCollection]
        List<GnosisPanel> Panels { get; set; }

        [GnosisCollection]
        List<GnosisGrid> Grids { get;  set; }

        [GnosisCollection]
        List<GnosisTextArea> TextAreas { get; set; }

        //properties
        [GnosisProperty]
        string GnosisIcon { get; set; }

        [GnosisProperty]
        string AllowedSectionList { get; set; }

        List<int> _AllowedSectionList { get; }

        [GnosisProperty]
        int OptimalSectionWidthCharacters { get; set; }

        [GnosisProperty]
        int MinWidthCharacters { get; set; }

        [GnosisProperty]
        int MaxWidthCharacters { get; set; }

        //dynamic properties
        bool IsEditing { get; set; }
        bool IsEmpty { get; set; }
        bool SQLSuccessful { get; set; }

        //methods
        void SetWidthChangedHandler(Action<double> widthChangedHandler);
        void LoadMessageGridImplementation(IGnosisMessageGridImplementation msgGridImp, int col, int row, int colSpan, int rowSpan);
        void LoadGrid(IGnosisGridImplementation gridImp, int col, int row, int colSpan, int rowSpan);
        void LoadPanel(IGnosisPanelImplementation panelImp, int col, int row, int colSpan, int rowSpan);
        //string GetCaption();
        double GetAvailableWidth();
        void SetLoadedHandler(Action<double> action);
        void Clear();
        void AddColumn();
        void AddRowAutoHeight();
        void AddRow();
        void SetMaxWidth(double maxWidth);
        void SetHorizontalAlignment(GnosisController.HorizontalAlignmentType left);
        void SetMinWidth(double minWidth);
        void SetNumColumns(int numCols);
        void SetVerticalScrollbarVisibility(GnosisVisibleController.GnosisVerticalScrollbarVisibility visibility);
        void IncreaseColSpan(IGnosisInnerLayoutControlImplementation ControlImplementation, int numCols);
        void AddRowFixedHeight(int rowHeight);
        void RemoveChild(IGnosisMouseVisibleControlImplementation ControlImplementation);
        double GetAvailableHeight();
        double GetPaddingVertical();
    }
}
