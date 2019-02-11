using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities.SalesForce
{
    public class SalesForceEntity
    {
        public HeaderSection HeaderSection { get; set; }
        public Body Body { get; set; }

    }
    public class HeaderSection
    {
        public string TransactionID { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedDateTimeZone { get; set; }
        public string VersionNumber { get; set; }
        public string TransactionType { get; set; }
        public string PartnerID { get; set; }
        public int Criteria { get; set; }
    }
    public class Body
    {
        public Details Details { get; set; }
    }
    public class Details
    {
        public UserDetails UserDetails { get; set; }
        public QuotationDetails QuotationDetails { get; set; }
        public BookingDetails BookingDetails { get; set; }
    }
    public class QuotationDetails
    {
        public string QuotationId { get; set; }
        public string QuotationNumber { get; set; }
        public string QuotationUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public string State { get; set; }
        public string QuotationDescription { get; set; }
        public string CreatedBy { get; set; }
        public string MovementType { get; set; }
        public string Product { get; set; }
        public string ProductType { get; set; }
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }
        public string UserCountry { get; set; }
        public string OriginPortName { get; set; }
        public string DestinationPortName { get; set; }
        public string PreferredCurrency { get; set; }
        public DateTime QuoteExpiryDate { get; set; }
        public decimal QuoteAmount { get; set; }
    }

    public class BookingDetails
    {
        public string BookingId { get; set; }
        public string Shipper { get; set; }
        public string Consignee { get; set; }
        public string MovementType { get; set; }
        public string Product { get; set; }
        public string ProductType { get; set; }
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }
        public string UserCountry { get; set; }
        public string OriginPortName { get; set; }
        public string DestinationPortName { get; set; }
        public string UnitofMeasure { get; set; }
        public string PaymentType { get; set; }
        public string PreferredCurrency { get; set; }
        public Decimal AdditionalAmount { get; set; }
        public Decimal GrandTotal { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime BookingExpiryDate { get; set; }
        public string UserId { get; set; }
        public string QuotationId { get; set; }
        public string BookingUrl { get; set; }
        public string State { get; set; }
        public string DiscountCode { get; set; }
        public Decimal DiscountAmount { get; set; }
        public DateTime DiscountExpiryDate { get; set; }
    }

    public class FocisShipaData
    {
        public string ID { get; set; }
        public string TYPE { get; set; }

    }
    public class UserDetails
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string ISDCode { get; set; }
        public string WorkPhone { get; set; }
        public string UserFlag { get; set; }
        public int LoginCount { get; set; }
        public string AccountStatus { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string JobTitle { get; set; }
        public string CountryName { get; set; }
        public string Type { get; set; }
        public string CityName { get; set; }
        public string FullAddress { get; set; }
        public string ControlClientId { get; set; }
        public string PreferredCurrencyCode { get; set; }
        public bool Subscribed { get; set; }
        public string ShippingExperience { get; set; }
        public string ShipmentProcess { get; set; }
        public string PersonalAssistance { get; set; }
        public string ExistingFreightForward { get; set; }
        public string UseofQuote { get; set; }
        public string CreatedBy { get; set; }
        public string CurrencyName { get; set; }
        public string DiscountCode { get; set; }
        public Decimal DiscountAmount { get; set; }
        public DateTime DiscountExpiryDate { get; set; }
        public string Industry { get; set; }

    }

}
