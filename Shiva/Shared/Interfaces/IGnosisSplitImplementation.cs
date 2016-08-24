using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.ContainerControllers;
using GnosisControls;

namespace Shiva.Shared.Interfaces
{
    public interface IGnosisSplitImplementation : IGnosisContainerImplementation
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadedHandler">To be called when the Split is loaded, with parameter width</param>
        void SetLoadedHandler(Action<double> loadedHandler);


        /// <summary>
        /// Draw the UI with the given containers separated by a divider in direction splitDirection
        /// at position splitPercentage.
        /// </summary>
        /// <param name="containers"></param>
        /// <param name="splitPercentage"></param>
        /// <param name="splitDirection"></param>
        void BuildContent(List<GnosisContainer> containers, double splitPercentage, 
            GnosisSplitController.DirectionType splitDirection);

        /// <summary>
        /// This handler must be invoked when the splitter is moved.
        /// </summary>
        /// <param name="splitterMovedHandler"></param>
        void SetSplitterMovedHandler(Action<double> splitterMovedHandler);

        /// <summary>
        /// This method must be called when the splitter is moved. It must invoke the splitterMovedHandler.
        /// </summary>
        /// <param name="splitDirection"></param>
        void SplitterMoved(GnosisSplitController.DirectionType splitDirection);

        /// <summary>
        /// Remove a child container from the UI.
        /// </summary>
        /// <param name="child"></param>
        void RemoveChild(GnosisContainer child);

        /// <summary>
        /// Move the splitter to the newPercent.
        /// </summary>
        /// <param name="newPercent"></param>
        /// <param name="splitDirection"></param>
        void SetSplitterPercent(double newPercent, GnosisSplitController.DirectionType splitDirection);
    }
}
