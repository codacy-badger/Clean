using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities
{
    public class SSPJobDetailsEntity
    {

        public virtual long JOBNUMBER { get; set; }
        public virtual string OPERATIONALJOBNUMBER { get; set; }
        public virtual long QUOTATIONID { get; set; }
        public virtual string QUOTATIONNUMBER { get; set; }
        public virtual string INCOTERMCODE { get; set; }
        public virtual string INCOTERMDESCRIPTION { get; set; }
        public virtual DateTime CARGOAVAILABLEFROM { get; set; }
        public virtual DateTime CARGODELIVERYBY { get; set; }
        //public virtual string ORIGINCUSTOMSCLEARANCEBY { get; set; }
        //public virtual string DESTINATIONCUSTOMSCLEARANCEBY { get; set; }
        public virtual int DOCUMENTSREADY { get; set; }
        public virtual string SLINUMBER { get; set; }
        public virtual string CONSIGNMENTID { get; set; }
        public virtual DateTime DATECREATED { get; set; }
        public virtual DateTime DATEMODIFIED { get; set; }
        public virtual string MODIFIEDBY { get; set; }
        public virtual string STATEID { get; set; }
        public virtual string SPECIALINSTRUCTIONS { get; set; }
        public virtual decimal JOBGRANDTOTAL { get; set; }
        public virtual string COLLABRATEDCONSIGNEE { get; set; }
        public virtual string COLLABRATEDSHIPPER { get; set; }
        public virtual string SHIPPERPRIVILEGE { get; set; }
        public virtual string CONSIGNEEPRIVILEGE { get; set; }
        public virtual decimal ADDITIONALCHARGEAMOUNT { get; set; }
        public virtual string ADDITIONALCHARGESTATUS { get; set; }
        public long LIVEUPLOADS { get; set; }
        public long LOADINGDOCKAVAILABLE { get; set; }
        public long CARGOPALLETIZED { get; set; }
        public long ORIGINALDOCUMENTSREQUIRED { get; set; }
        public long TEMPERATURECONTROLREQUIRED { get; set; }
        public long COMMERCIALPICKUPLOCATION { get; set; }
        public virtual string AMAZONREFERENCEID { get; set; }
        public virtual string AMAZONSHIPMENTID { get; set; }

    }
}
