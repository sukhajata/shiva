using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisGridFieldImplementation : IGnosisContentControlImplementation, 
        INotifyPropertyChanged, IGnosisDisplayCharsPossessor, IGnosisPaddingPossessor, IGnosisControlThicknessPossessor
    {
        //properties
        bool IsEvenRow { get; set; }
        bool RowSelected { get; set; }  
        bool Locked { get; set; }
        bool ReadOnly { get; set; }
        string Value { get;  set; }

     //   int HorizontalPadding { get; set; }
       // int VerticalPadding { get; set; }
       

        //methods
        double GetHeight();
        double GetWidth();
    }
}
