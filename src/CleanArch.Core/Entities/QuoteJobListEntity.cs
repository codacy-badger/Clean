using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities
{
    public class QuoteJobListEntity
    {
        public decimal CARGOVALUE { get; set; }
        public string CARGOVALUECURRENCYID { get; set; }
        public decimal CHARGEABLEVOLUME { get; set; }
        public decimal CHARGEABLEWEIGHT { get; set; }
        public long CUSTOMERID { get; set; }
        public DateTime DATEMODIFIED { get; set; }
        public DateTime DATEOFENQUIRY { get; set; }
        public DateTime DATEOFSHIPMENT { get; set; }
        public DateTime DATEOFVALIDITY { get; set; }
        public double DENSITYRATIO { get; set; }
        public string DESTINATIONPLACECODE { get; set; }
        public long DESTINATIONPLACEID { get; set; }
        public string DESTINATIONPLACENAME { get; set; }
        public string DESTINATIONPORTCODE { get; set; }
        public long DESTINATIONPORTID { get; set; }
        public string DESTINATIONPORTNAME { get; set; }
        public long INSUREDVALUE { get; set; }
        public string INSUREDVALUECURRENCYID { get; set; }
        public long ISHAZARDOUSCARGO { get; set; }
        public long ISEVENTCARGO { get; set; }
        public long ISINSURANCEREQUIRED { get; set; }
        public long ISCHARGEINCLUDED { get; set; }
        public string MODIFIEDBY { get; set; }
        public long MOVEMENTTYPEID { get; set; }
        public string LANGUAGEMOVEMENTTYPENAME { get; set; }
        public string MOVEMENTTYPENAME { get; set; }
        public string ORIGINPLACECODE { get; set; }
        public long ORIGINPLACEID { get; set; }
        public string ORIGINPLACENAME { get; set; }
        public string ORIGINPORTCODE { get; set; }
        public long ORIGINPORTID { get; set; }
        public string ORIGINPORTNAME { get; set; }
        public string PREFERREDCURRENCYID { get; set; }
        public long PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public long PRODUCTTYPEID { get; set; }
        public string PRODUCTTYPENAME { get; set; }
        public long QUOTATIONID { get; set; }
        public string QUOTATIONNUMBER { get; set; }
        public string SHIPMENTDESCRIPTION { get; set; }
        public string STATEID { get; set; }
        public decimal TOTALCBM { get; set; }
        public decimal TOTALGROSSWEIGHT { get; set; }
        public decimal VOLUMETRICWEIGHT { get; set; }
        public long TOTALQUANTITY { get; set; }
        public long JOBNUMBER { get; set; }
        public string OPERATIONALJOBNUMBER { get; set; }
        public decimal GRANDTOTAL { get; set; }
        public string JSTATEID { get; set; }
        public long HASMISSINGCHARGES { get; set; }
        public string GUESTCOMPANY { get; set; }
        public string GUESTEMAIL { get; set; }
        public string GUESTNAME { get; set; }
        public string GUESTLASTNAME { get; set; }
        public string ORIGINSUBDIVISIONCODE { get; set; }
        public string DESTINATIONSUBDIVISIONCODE { get; set; }
        public long ThresholdQty { get; set; }
        public decimal OCOUNTRYID { get; set; }
        public decimal DCOUNTRYID { get; set; }
        public long OPORTCOUNTRYID { get; set; }
        public long DPORTCOUNTRYID { get; set; }
        public string OCOUNTRYNAME { get; set; }
        public string DCOUNTRYNAME { get; set; }
        public string ORIGINZIPCODE { get; set; }
        public string DESTINATIONZIPCODE { get; set; }
        public string OCOUNTRYCODE { get; set; }
        public string DCOUNTRYCODE { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime JDDATEMODIFIED { get; set; }
        public decimal JOBGRANDTOTAL { get; set; }
        public string TEMPLATENAME { get; set; }
        public IList<QuotationShipmentEntity> ShipmentItems { get; set; }
        public IList<SSPJobDetailsEntity> JobItems { get; set; }
        public IList<ReceiptHeaderEntity> ReceiptHeaderDetails { get; set; }
        public List<QuotationTermsConditionEntity> TermAndConditions { get; set; }
        public decimal ADDITIONALCHARGEAMOUNT { get; set; }
        public string ADDITIONALCHARGESTATUS { get; set; }
        public Int64 CHARGESETID { get; set; }
        public string TRANSITTIME { get; set; }
        public long ISOFFERAPPLICABLE { get; set; }
        public string OFFERCODE { get; set; }
        public decimal DECIMALCOUNT { get; set; }
    }
    public class ListSavedQuotesEntity
    {
        public QuoteStatusCountEntity QuotesTabCount { get; set; }
        public IEnumerable<QuoteJobListEntity> SavedQuotes { get; set; }


    }

    public class ListSavedJobsEntity
    {
        public QuoteStatusCountEntity JobsTabCount { get; set; }
        public IEnumerable<QuoteJobListEntity> SavedJobs { get; set; }


    }
}
