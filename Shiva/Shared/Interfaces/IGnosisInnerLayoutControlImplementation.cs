using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisInnerLayoutControlImplementation : IGnosisMouseVisibleControlImplementation
    {
        int MaxSectionSpan { get; set; }

        //void SetMaxPrintWidth(double maxPrintWidth);
        double GetAvailableWidth();
       // void SetMarginLeft(int horizontalSpacing);
        double GetHeight();
        // void SetMarginLeft(int horizontalSpacing);
        //FontFamily GetFontFamily();
        //double GetFontSize();
        //FontStyle GetFontStyle();
        //FontWeight GetFontWeight();
        //FontStretch GetFontStretch();
        //void SetMargin(int left, int top, int right, int bottom);
        //void SetMargin(int margin);
        //void SetMarginBottom(int marginBottom);
    }
}
