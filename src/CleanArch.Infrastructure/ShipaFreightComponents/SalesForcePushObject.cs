using CleanArch.Core.Entities.SalesForce;
using CleanArch.Core.Entities.UserManagement;
using CleanArch.Core.Repository.SalesForce;
using CleanArch.Core.Repository.UserManagement;
using CleanArch.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.ShipaFreightComponents
{
    public class SalesForcePushObject : ISalesForcePushObject
    {
        Details objDetails = new Details();
        Body objBody = new Body();
        SalesForceEntity salesForceEntity = new SalesForceEntity();
        Task<HeaderSection> objHeaderDetails;
        Task<UserDetails> objUserDetails;
        string TransactionType, UserHostAddress, UniqueReferenceId;
        public void UserSignUpDetailsMapping(UserEntity mDBEntity)
        {
            try
            {
                SalesForceDataIntegration obj = new SalesForceDataIntegration();
                TransactionType = "Sign-Up-New-User";
                objHeaderDetails = Task.Factory.StartNew(() => obj.HeaderDetailsMapping(TransactionType)).Result;
                objUserDetails = Task.Factory.StartNew(() => SalesForceDataIntegration.UserSignUpDetailsMapping(mDBEntity)).Result;
                objUserDetails.Result.RegisteredDate = System.DateTime.Now;
               //pavan salesForceEntity.HeaderSection = objHeaderDetails.Result;
                objDetails.UserDetails = objUserDetails.Result;
                objBody.Details = objDetails;
                salesForceEntity.Body = objBody;
                UniqueReferenceId = objDetails.UserDetails.UserId;
                PushDataToMessageQueue(salesForceEntity, UniqueReferenceId);
            }
            catch (Exception ex)
            {
               // throw new ShipaApiException(ex.Message.ToString());
            }
        }
        private void PushDataToMessageQueue(SalesForceEntity salesForceEntity, string uniqueReferenceId)
        {
            try
            {
                string IBMMQCheck = ShipaConfiguration.IBMMQCheck();
                if (IBMMQCheck == "On")
                {
                   //pavan MQIntegrationReader PushDataToMessageQueue = new MQIntegrationReader();
                  //pavan  var obj = Task.Factory.StartNew(() => PushDataToMessageQueue.MQIntegrationUserCall(salesForceEntity, uniqueReferenceId));
                }
            }
            catch (Exception ex)
            {
                //throw new ShipaApiException(ex.Message.ToString());
            }
        }
    }
}
