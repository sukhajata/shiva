using Shiva.Shared.BaseControllers;
using Shiva.Shared.ContainerControllers;
using Shiva.Shared.ContentControllers;
using GnosisControls;
using Shiva.Shared.Data;
using Shiva.Shared.DataControllers;
using Shiva.Shared.InnerLayoutControllers;
using Shiva.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shiva.Shared.ContentControllers
{
    public class GnosisGalleryItemController : GnosisVisibleController
    {
        private List<GnosisGalleryItemController> childControllers;

        //public bool Active
        //{
        //    get { return ((GnosisGalleryItem)ControlImplementation).Active; }
        //    set
        //    {
        //        ((GnosisGalleryItem)ControlImplementation).Active = value;
        //        OnPropertyChanged("Active");
        //    }
        //}

        //public bool Disabled
        //{
        //    get { return ((GnosisGalleryItem)ControlImplementation).Disabled; }
        //    set
        //    {
        //        ((GnosisGalleryItem)ControlImplementation).Disabled = value;
        //        OnPropertyChanged("Disabled");
        //    }
        //}

        public GnosisGalleryItemController(
            GnosisGalleryItem galleryItem,
           // IGnosisGalleryItemImplementation galleryItemImplementation,
            GnosisInstanceController instanceController,
            GnosisGalleryController parent)
            :base(galleryItem, instanceController, parent)
        {
            childControllers = new List<GnosisGalleryItemController>();


        }

        public GnosisGalleryItemController(
            GnosisGalleryItem galleryItem,
          //  IGnosisGalleryItemImplementation galleryItemImplementation,
            GnosisInstanceController instanceController,
            GnosisGalleryItemController parent)
            : base(galleryItem, instanceController, parent)
        {
            childControllers = new List<GnosisGalleryItemController>();



        }



        public virtual void Setup()
        {
            ((IGnosisGalleryItemImplementation)ControlImplementation).SetItemSelectedHandler(new Action(ItemSelected));
            ((IGnosisGalleryItemImplementation)ControlImplementation).SetItemUnselectedHandler(new Action(ItemUnselected));

            //if (((GnosisGalleryItem)ControlImplementation).Caption != null)
            //{
            //    ((IGnosisGalleryItemImplementation)ControlImplementation).SetCaption(((GnosisGalleryItem)ControlImplementation).Caption);
            //}

            bool expanded = ((GnosisGalleryItem)ControlImplementation).GnosisExpanded;


            if (((GnosisGalleryItem)ControlImplementation).GalleryDatasetItems != null)
            {
                foreach (GnosisGalleryDatasetItem datasetItem in ((GnosisGalleryItem)ControlImplementation).GalleryDatasetItems)
                {
                    GnosisGalleryDatasetItemController datasetItemController = new GnosisGalleryDatasetItemController(datasetItem, InstanceController, this);
                    datasetItemController.LoadData(InstanceController.Instance, expanded);
                }
            }
            ((IGnosisGalleryItemImplementation)ControlImplementation).GnosisExpanded = expanded;

        }

        internal void AddGalleryItemController(GnosisGalleryItemController galleryItemController)
        {
            childControllers.Add(galleryItemController);
            ControlImplementation.GnosisAddChild(galleryItemController.ControlImplementation);

        }

        internal override void ShowTooltips()
        {
            base.ShowTooltips();

            foreach (GnosisGalleryItemController child in childControllers)
            {
                child.ShowTooltips();
            }
        }

        internal override void HideTooltips()
        {
            base.HideTooltips();

            foreach (GnosisGalleryItemController child in childControllers)
            {
                child.HideTooltips();
            }
        }

        public virtual void ItemSelected()
        {
            
        }

        public virtual void ItemUnselected()
        {
        }

        //internal void OnItemSelected(GnosisGalleryItemController selectedItem)
        //{
        //    if (selectedItem != this)
        //    {
        //        Selected = false;
        //    }

        //    foreach (GnosisGalleryItemController child in childControllers)
        //    {
        //        child.OnItemSelected(selectedItem);
        //    }
        //}

        internal void LoadGalleryItems()
        {
            foreach (GnosisGalleryItemController child in childControllers.OrderBy(c => c.ControlImplementation.Order))
            {
                ((GnosisGalleryItem)ControlImplementation).AddGalleryItem((GnosisGalleryItem)child.ControlImplementation);

            }
        }


    }
}
