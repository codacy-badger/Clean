using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities.UserManagement
{
    public class UserEntity
    {
        public string USERID { get; set; }
        public int USERTYPE { get; set; }
        public string EMAILID { get; set; }
        public string PASSWORD { get; set; }
        public int ISTEMPPASSWORD { get; set; }
        public string TOKEN { get; set; }
        public DateTime TOKENEXPIRY { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string COMPANYNAME { get; set; }
        public long ISDID { get; set; }
        public string ISDCODE { get; set; }
        public string MOBILENUMBER { get; set; }
        public int ISTERMSAGREED { get; set; }
        public long COUNTRYID { get; set; }
        public int ACCOUNTSTATUS { get; set; }
        public decimal LOGINCOUNT { get; set; }
        public DateTime LASTLOGONTIME { get; set; }
        public long TIMEZONEID { get; set; }
        public long USERCULTUREID { get; set; }
        public DateTime DATECREATED { get; set; }
        public DateTime DATEMODIFIED { get; set; }
        public int ISVATREGISTERED { get; set; }
        public string VATREGNUMBER { get; set; }
        public string SALUTATION { get; set; }
        public long JOBTITLEID { get; set; }
        public string JOBTITLE { get; set; }
        public string COMPANYLINK { get; set; }
        public string COUNTRYCODE { get; set; }
        public string COUNTRYNAME { get; set; }
        public string UAADDRESSTYPE { get; set; }
        public string UAHOUSENO { get; set; }
        public string UABUILDINGNAME { get; set; }
        public string UAADDRESSLINE1 { get; set; }
        public string UAADDRESSLINE2 { get; set; }
        public long UACOUNTRYID { get; set; }
        public string UACOUNTRYCODE { get; set; }
        public string UACOUNTRYNAME { get; set; }
        public long UASTATEID { get; set; }
        public string UASTATECODE { get; set; }
        public string UASTATENAME { get; set; }
        public long UACITYID { get; set; }
        public string UACITYCODE { get; set; }
        public string UACITYNAME { get; set; }
        public string UAPOSTCODE { get; set; }
        public string UAFULLADDRESS { get; set; }
        public int UAISDEFAULT { get; set; }
        public DateTime UADATECREATED { get; set; }
        public DateTime UADATEMODIFIED { get; set; }
        public string IMGFILENAME { get; set; }
        public byte[] IMGCONTENT { get; set; }
        public long DEPTID { get; set; }
        public string DEPTCODE { get; set; }
        public string DEPTNAME { get; set; }
        public string WORKPHONE { get; set; }
        public long FAILEDPASSWORDATTEMPT { get; set; }
        public IList<UMUserAddressDetailsEntity> UserAddress { get; set; }
        public IList<CreditEntity> CreditItems { get; set; }
        public long NOTIFICATIONID { get; set; }
        public string InstantPASSWORD { get; set; }
        public DateTime InstTime { get; set; }
        public DateTime ServerDate { get; set; }
        public string GuestEmail { get; set; }
        public long ListId { get; set; }
        public long ActiveCampaignContactId { get; set; }
        public bool NOTIFICATIONSUBSCRIPTION { get; set; }
        public int SubscriptionStatus { get; set; }
        public int SubscriberId { get; set; }
        public string CRMEmail { get; set; }
        public string SHIPPINGEXPERIENCE { get; set; }
        public string SHIPMENTPROCESS { get; set; }
    }
}
