using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisLinkFieldImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor
    {
        //properties
        int DocumentEntityID { get; set; }
        int DocumentSystemID { get; set; }
        bool Locked { get; set; }
        string Perspective { get; set; }
        bool PreviouslySelected { get; set; }
        bool ReadOnly { get; set; }
        string Value { get; set; }

        //methods
        void SetClickHandler(Action action);
        void SetUnderline(bool isUnderline);
        void SetMenuButtonEnabled(bool enabled);
        //void AddMenuItem(IGnosisLinkMenuButtonItemImplementation itemImplementation);
        IGnosisButtonImplementation GetLinkButtonImplementation();
        IGnosisMenuButtonImplementation GetLinkMenuButtonImplementation();
    }
}
