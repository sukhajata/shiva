﻿using System;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisMenuItemImplementation : IGnosisMouseVisibleControlImplementation
    {
        bool Disabled { get; set; }
        string GnosisIcon { get; set; }
        string Shortcut { get; set; }
        string MenuTag { get; set; }

        void SetClickHandler(Action action);

    }
}