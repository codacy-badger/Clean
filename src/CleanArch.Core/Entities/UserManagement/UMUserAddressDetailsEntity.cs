using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities.UserManagement
{
    public class UMUserAddressDetailsEntity
    {
        public long USERADTLSID { get; set; }
        public string USERID { get; set; }
        public string ADDRESSTYPE { get; set; }
        public string HOUSENO { get; set; }
        public string BUILDINGNAME { get; set; }
        public string ADDRESSLINE1 { get; set; }
        public string ADDRESSLINE2 { get; set; }
        public long COUNTRYID { get; set; }
        public string COUNTRYCODE { get; set; }
        public string COUNTRYNAME { get; set; }
        public long STATEID { get; set; }
        public string STATECODE { get; set; }
        public string STATENAME { get; set; }
        public long CITYID { get; set; }
        public string CITYCODE { get; set; }
        public string CITYNAME { get; set; }
        public string POSTCODE { get; set; }
        public string FULLADDRESS { get; set; }
        public int ISDEFAULT { get; set; }
        public DateTime DATECREATED { get; set; }
        public DateTime DATEMODIFIED { get; set; }
    }
}
