using System;
using System.Collections.Generic;
using System.Text;

using ShivaShared3.ContainerControllers;
using GnosisControls;

namespace ShivaShared3.Interfaces
{
    public interface IGnosisPrimarySplitImplementation : IGnosisSplitImplementation
    {
        //properties
        /// <summary>
        /// Ignore the Split and display the Tile over the window, use for Phone where both Navigator and Next window overlay their parent window, 
        /// when set then Split Detail is not required
        /// </summary>
        bool TilesOverlayParent { get; set; }

            
        //methods
        void SetSplitterMovedHandler(Action<double, int> splitterMovedHandler);
        
        /// <summary>
        /// Move the splitter to new percent, returning the new navigator width.
        /// </summary>
        /// <param name="newPercent"></param>
        /// <param name="splitDirection"></param>
        /// <returns></returns>
        int SetSplitterPercent(double newPercent);

        /// <summary>
        /// Move the splitter to new units, returning the new percentage.
        /// </summary>
        /// <param name="newUnits"></param>
        /// <param name="splitDirection"></param>
        /// <returns></returns>
        double SetSplitterUnits(int newUnits);

        /// <summary>
        /// Build UI with a fixed width for the navigator tile, with remaining content filling available space.
        /// </summary>
        /// <param name="containerImplementations"></param>
        /// <param name="splitUnits"></param>
        /// <param name="splitDirection"></param>
        void BuildContentFixedNavWidth(List<GnosisContainer> containerImplementations, int splitUnits, 
            GnosisSplitController.DirectionType splitDirection);
    }
}
