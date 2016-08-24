using System;
using System.Collections.Generic;
using System.Text;

using Shiva.Shared.ContentControllers;
using GnosisControls;
using Shiva.Shared.Interfaces;
using Shiva.Shared.DataControllers;
using Shiva.Shared.OuterLayoutControllers;
using Shiva.Shared.BaseControllers;

namespace Shiva.Shared.ContainerControllers
{
    //provides umbrella class for GnosisSplitController and GnosisTileController
    public class GnosisContainerController  : GnosisVisibleController
    {


        public GnosisContainerController(
            IGnosisContainerImplementation _container, 
           // IGnosisContainerImplementation _containerImplementation, 
            GnosisEntityController entityController,
            GnosisVisibleController parent)
            : base(_container, entityController, parent)
        {
           // _containerImplementation.SetOrder(_container.Order);

        }


        //overridden in implementing classes
        public virtual GnosisController FindControllerByID(int controlID)
        {
            throw new NotImplementedException();
        }

        public virtual void Highlight()
        {
            ((IGnosisContainerImplementation)ControlImplementation).Highlight();
        }

        public virtual void UnHighlight()
        {
            ((IGnosisContainerImplementation)ControlImplementation).UnHighlight();
        }

        internal bool LoadDocumentFrame(GnosisDocumentFrameController docFrameController)
        {
           
            bool loaded = false;

            if (this is GnosisSplitController)
            {
                foreach (GnosisContainerController child in ((GnosisSplitController)this).ChildControllers)
                {
                    loaded = child.LoadDocumentFrame(docFrameController);

                    if (loaded)
                    {
                        break;
                    }
                }
            }
            else
            {
                if (((GnosisTile)this.ControlImplementation).AcceptsDocumentFrames)
                {
                    ((GnosisTileController)this).LoadFrame(docFrameController);
                    loaded = true;
                }
            }

            return loaded;
        }

        internal virtual void RemoveFrame(GnosisFrameController frameController)
        {
            throw new NotImplementedException();
        }

        internal virtual void SizeChanged()
        {
            throw new NotImplementedException();
        }
    }
}
