using CleanArch.Core.Entities.SalesForce;
using CleanArch.Core.Entities.UserManagement;
using CleanArch.Core.Repository.SalesForce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Core.Utilities
{
    public class SalesForceDataIntegration 
    {
        ISalesForceRepository _salesRepository;
        public SalesForceDataIntegration(ISalesForceRepository salesRepository)
        {
            _salesRepository = salesRepository;

        }
        public SalesForceDataIntegration()
        {
            
        }
        public async Task<HeaderSection> HeaderDetailsMapping(string transcationType)
        {
            HeaderSection HeaderSectionObject = new HeaderSection();
            try
            {
                HeaderSectionObject.TransactionID = GenerateTranscationNo().ToString();
                HeaderSectionObject.SenderID = ShipaConfiguration.SenderID();
                HeaderSectionObject.ReceiverID = ShipaConfiguration.ReceiverID();
                HeaderSectionObject.UserID = ShipaConfiguration.ShipaClientID();
                HeaderSectionObject.CreatedDateTime = System.DateTime.Now;
                HeaderSectionObject.CreatedDateTimeZone = ShipaConfiguration.CreatedDateTimeZone();
                HeaderSectionObject.VersionNumber = ShipaConfiguration.VersionNumber();
                HeaderSectionObject.TransactionType = transcationType;
                HeaderSectionObject.PartnerID = ShipaConfiguration.PartnerID();
                HeaderSectionObject.Criteria = 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => HeaderSectionObject).ConfigureAwait(false);
        }
        public int GenerateTranscationNo()
        {
            int transcationnumber = 0;
            try
            {
                // SalesForceDBFactory SalesForceDBObj = new SalesForceDBFactory();
                transcationnumber = _salesRepository.GetTranscationNumber();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return transcationnumber;
        }

        public static async Task<UserDetails> UserSignUpDetailsMapping(UserEntity mDBEntity)
        {
            UserDetails UserMappingObject = new UserDetails();
            try
            {
                UserMappingObject.UserId = mDBEntity.USERID;
                if (mDBEntity.ACCOUNTSTATUS != 0)
                {
                    UserMappingObject.AccountStatus = ShipaConfiguration.Active();
                }
                else
                {
                    UserMappingObject.AccountStatus = ShipaConfiguration.InActive();
                }
                if (mDBEntity.USERID.ToLower().Contains("@agility.com"))
                {
                    UserMappingObject.UserFlag = ShipaConfiguration.Internal();
                }
                else
                {
                    UserMappingObject.UserFlag = ShipaConfiguration.External();
                }
                UserMappingObject.FirstName = mDBEntity.FIRSTNAME;
                UserMappingObject.LastName = mDBEntity.LASTNAME;
                UserMappingObject.CompanyName = mDBEntity.COMPANYNAME;
                UserMappingObject.WorkPhone = mDBEntity.ISDCODE + "-" + mDBEntity.MOBILENUMBER;
                UserMappingObject.CountryName = mDBEntity.COUNTRYNAME;
                UserMappingObject.Subscribed = mDBEntity.NOTIFICATIONSUBSCRIPTION;
                UserMappingObject.ShipmentProcess = mDBEntity.SHIPMENTPROCESS;
                UserMappingObject.ShippingExperience = mDBEntity.SHIPPINGEXPERIENCE;
                UserMappingObject.LoginCount = Convert.ToInt32(mDBEntity.LOGINCOUNT);
                UserMappingObject.LastLoginDate = mDBEntity.LASTLOGONTIME;
                UserMappingObject.CreatedBy = ShipaConfiguration.Registered();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.Run(() => UserMappingObject).ConfigureAwait(false);
        }
    }
}