using System;
using System.Collections.Generic;
using System.Text;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisGalleryItemImplementation : IGnosisMouseVisibleControlImplementation, IGnosisControlThicknessPossessor,
        IGnosisPaddingPossessor, IGnosisSpacingPossessor
    {
        //properties
        bool Active { get; set; }
        bool Disabled { get; set; }
        bool GnosisExpanded { get; set; }
        string GnosisIcon { get; set; }

        //methods
        void AddGalleryItem(IGnosisGalleryItemImplementation childImplementation);
        //void SetExpanded(bool expanded);
        //void SetCaption(string caption);
        void SetItemSelectedHandler(Action selectedHandler);
        void SetItemUnselectedHandler(Action action);
    }
}
