using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities.UserManagement
{
    public class CreditEntity
    {
        public long CREDITAPPID { get; set; }

        public string INDUSTRYID { get; set; }

        public string TRADINGNAME { get; set; }

        public long COUNTRYID { get; set; }

        public string COUNTRYCODE { get; set; }

        public string CITYNAME { get; set; }

        public string COUNTRYNAME { get; set; }

        public long CITYID { get; set; }

        public long NOOFEMP { get; set; }

        public string SUBVERTICAL { get; set; }

        public string PARENTCOMPANY { get; set; }

        public decimal ANNUALTURNOVER { get; set; }

        public string ANNUALTURNOVERCURRENCY { get; set; }

        public string DBNUMBER { get; set; }

        public string HFMENTRYCODE { get; set; }

        public decimal REQUESTEDCREDITLIMIT { get; set; }

        public string REQUESTEDCREDITLIMITCURRENCY { get; set; }

        public long CURRENTPAYTERMSID { get; set; }

        public string CURRENTPAYTERMSNAME { get; set; }

        public decimal CURRENTOSBALANCE { get; set; }

        public decimal CURRENTOVERDUEBALANCE { get; set; }

        public DateTime CREDITLIMITEXPDATE { get; set; }

        public decimal NEXTYEARREVENUE { get; set; }

        public decimal NEXTYEARNR { get; set; }

        public decimal NEXTYEARNRMARGIN { get; set; }

        public decimal NEXTYEARBILLING { get; set; }

        public decimal CURRENTYEARREVENUE { get; set; }

        public decimal CURRENTYEARNR { get; set; }

        public decimal CURRENTYEARNRMARGIN { get; set; }

        public decimal CURRENTYEARBILLING { get; set; }

        public decimal PREVIOUSYEARREVENUE { get; set; }

        public decimal PREVIOUSYEARNR { get; set; }

        public decimal PREVIOUSYEARNRMARGIN { get; set; }

        public decimal PREVIOUSYEARBILLING { get; set; }

        public string PRODUCTPROFILE { get; set; }

        public string SALESCHANNEL { get; set; }

        public decimal APPROVEDCREDITLIMIT { get; set; }

        public string APPREJCOMMENT { get; set; }

        public string COMMENTS { get; set; }

        public string USERID { get; set; }

        public string CURRENCYID { get; set; }

        public string DESCRIPTIONOFKS { get; set; }

        public string CREATEDBY { get; set; }

        public DateTime DATECREATED { get; set; }

        public DateTime DATEMODIFIED { get; set; }

        public string MODIFIEDBY { get; set; }

        public string OWNERORGID { get; set; }

        public string OWNERLOCID { get; set; }

        public string STATEID { get; set; }

        public long EXISTINGCUSTOMER { get; set; }
        public long USEMYCREIDT { get; set; }
    }
}
