using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities
{
    public class QuotationShipmentEntity
    {
        public long CONTAINERID { get; set; }
        public string CONTAINERNAME { get; set; }
        public string ITEMDESCRIPTION { get; set; }
        public decimal ITEMHEIGHT { get; set; }
        public decimal ITEMLENGTH { get; set; }
        public decimal ITEMWIDTH { get; set; }
        public long LENGTHUOM { get; set; }
        public string LENGTHUOMCODE { get; set; }
        public string LENGTHUOMNAME { get; set; }
        public string PACKAGETYPECODE { get; set; }
        public long PACKAGETYPEID { get; set; }
        public string PACKAGETYPENAME { get; set; }
        public long QUANTITY { get; set; }
        public long QUOTATIONID { get; set; }
        public long SHIPMENTITEMID { get; set; }
        public decimal WEIGHTPERPIECE { get; set; }
        public decimal WEIGHTTOTAL { get; set; }
        public long WEIGHTUOM { get; set; }
        public string WEIGHTUOMCODE { get; set; }
        public string WEIGHTUOMNAME { get; set; }
        public decimal TOTALCBM { get; set; }
        public string CONTAINERCODE { get; set; }
        public double TotalWgtKG { get; set; }
        public double TotalWgtCBM { get; set; }
        public double TotalWgtTON { get; set; }
        public string PALLETIZINGBY { get; set; }
        public string LABELLINGBY { get; set; }
    }
}
