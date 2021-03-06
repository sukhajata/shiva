﻿using Shiva.Shared.PanelFieldControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisToggleButtonImplementation : IGnosisPanelFieldImplementation, IGnosisDisplayCharsPossessor,
        IGnosisIconPossessor
    {
        //properties
        bool Active { get; set; }
        bool Disabled { get; set; }
        string GnosisGroupName { get; set; }
      //  string GnosisIcon { get; set; }
        int SelectedFactor { get; set; }
        string Shortcut { get; set; }
        bool TooltipSelected { get; set; }


        //methods
        void SetClickHandler(Action handler);
        //void SetCaption(string caption);
        //void SetIcon(string icon, bool enabled);
        //void SetSelected(bool selected);
        //bool GetSelected();
       // void SetSelectedChangedHandler(Action<bool> handler);
      //  void SetToggleBinding(object source, string property);
    }
}
