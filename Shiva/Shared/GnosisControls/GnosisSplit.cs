using System;
using System.ComponentModel;
using System.Collections.Generic;
using ShivaShared3.Interfaces;
using ShivaShared3.Data;

namespace GnosisControls
{
    public partial class GnosisSplit //: GnosisContainer
    {

        protected List<GnosisSplitDetail> gnosisSplitDetails;

        protected List<GnosisSplit> gnosisSplits;

        protected List<GnosisTile> gnosisTiles;


        [GnosisCollection]
        public List<GnosisSplit> Splits
        {
            get { return gnosisSplits; }
            set { gnosisSplits = value; }
        }

        [GnosisCollection]
        public List<GnosisTile> Tiles
        {
            get
            {
                return gnosisTiles;
            }
            set
            {
                gnosisTiles = value;
            }
        }


        [GnosisCollection]
        public List<GnosisSplitDetail> SplitDetails
        {
            get
            {
                return this.gnosisSplitDetails;
            }
            set { this.gnosisSplitDetails = value; }
        }

        //public GnosisControl FindControlByID(int controlID)
        //{
        //    GnosisControl control = null;
        //    while (control == null)
        //    {
        //        foreach (GnosisTile tile in GnosisTiles)
        //        {
        //            if (tile.ID == controlID)
        //            {
        //                control = tile;
        //            }
        //        }
        //        foreach(GnosisSplit split in GnosisSplits)
        //        {
        //            if (split.ID == controlID)
        //            {
        //                control = split;
        //            }
        //        }
        //        if (GnosisSplitDetails[0].ID == controlID)
        //        {
        //            control = GnosisSplitDetails[0];
        //        }
        //        break;
        //    }

        //    return control;
        //}

        //public void RemoveChild(GnosisContainer child)
        //{
        //    if (child is GnosisSplit)
        //    {

        //        List<GnosisSplit> splits = new List<GnosisSplit>(this.GnosisSplits);
        //        splits.Remove((GnosisSplit)child);
        //        this.GnosisSplits = splits.ToArray();
        //    }
        //    else if (child is GnosisTile)
        //    {
        //        List<GnosisTile> tiles = new List<GnosisTile>(GnosisTiles);
        //        tiles.Remove((GnosisTile)child);
        //        GnosisTiles = tiles.ToArray();
        //    }

        //}

        public override void GnosisAddChild(IGnosisObject child)
        {
            if (child is GnosisSplit)
            {
                if (gnosisSplits == null)
                {
                    gnosisSplits = new List<GnosisSplit>();
                }

                gnosisSplits.Add((GnosisSplit)child);
                
            }
            else if (child is GnosisTile)
            {
                if (gnosisTiles == null)
                {
                    gnosisTiles = new List<GnosisTile>();
                }

                gnosisTiles.Add((GnosisTile)child);
            }
            else if (child is GnosisSplitDetail)
            {
                if (gnosisSplitDetails == null)
                {
                    gnosisSplitDetails = new List<GnosisSplitDetail>();
                }

                gnosisSplitDetails.Add((GnosisSplitDetail)child);
            }
            else
            {
                GlobalData.Singleton.ErrorHandler.HandleError("Unknown type added to GnosisSplit: " + child.GetType().Name,
                    "GnosisSplit.GnosisAddChild");
            }
        }


        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("GnosisSplitDetail")]
        //public GnosisSplitDetail[] GnosisSplitDetails
        //{
        //    get
        //    {
        //        return this.gnosisSplitDetailField;
        //    }
        //    set
        //    {
        //        this.gnosisSplitDetailField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("GnosisTile", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        //public GnosisTile[] GnosisTiles
        //{
        //    get
        //    {
        //        return this.gnosisTileField;
        //    }
        //    set
        //    {
        //        this.gnosisTileField = value;
        //    }
        //}




        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("GnosisSplit", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        //public GnosisSplit[] GnosisSplits
        //{
        //    get
        //    {
        //        return this.gnosisSplitField;
        //    }
        //    set
        //    {
        //        this.gnosisSplitField = value;
        //    }
        //}



    }
}