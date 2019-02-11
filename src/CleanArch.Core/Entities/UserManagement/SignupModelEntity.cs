using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities.UserManagement
{
    public class SignupModelEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public long UACountryId { get; set; }
        public string COUNTRYCODE { get; set; }
        public string Password { get; set; }
        public long ISDID { get; set; }
        public string ISDCode { get; set; }
        public string ContactNo { get; set; }
        public bool NotificationSubscription { get; set; }
        public string ShipmentProcess { get; set; }
        public string ShippingExperience { get; set; }
    }
}
