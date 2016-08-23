using System;
using System.Collections.Generic;
using System.Text;

using ShivaShared3.ContentControllers;
using GnosisControls;
using ShivaShared3.Interfaces;
using ShivaShared3.DataControllers;
using ShivaShared3.OuterLayoutControllers;
using ShivaShared3.BaseControllers;

namespace ShivaShared3.ContainerControllers
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
