using CleanArch.Core.Entities.UserManagement;
using CleanArch.Core.Repository.Communication;
using CleanArch.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace CleanArch.Infrastructure.CommunicationRepository
{
    public class MailService : APIDynamicClass , ICommunicationService
    {

        public bool Sendmail(UserEntity mDBEntity, string Token)
        {
            //pavan  var oneSignalService = new OneSignal();
            var notData = new { NOTIFICATIONTYPEID = 5111, NOTIFICATIONCODE = "Profile Incomplete" };
            //pavan  oneSignalService.SendPushMessageByTag("Email", mDBEntity.USERID, "Profile Incomplete", notData);
            string EnvironmentName = APIDynamicClass.GetEnvironmentName();
            string subject = "Account Activation for user " + mDBEntity.USERID + EnvironmentName;
            string appurl = "https://www.shipafreight.com/";//pavanSystem.Configuration.ConfigurationManager.AppSettings["APPURL"];
            var url = "https://www.google.com/";//pavan this.Url.Link("DefaultApi", new { Controller = "UserManagement", Action = "ActivateUser", rt = Token, UserId = mDBEntity.EMAILID, ReturnUrl = EncryptStringAES("/") });
            string urlparams = url.Split('?')[1];
            string resetLink1 = appurl + "UserManagement/ActivateUser?" + urlparams.Split('&')[1] + "&" + urlparams.Split('&')[2] + "&" + urlparams.Split('&')[3];
            string userName = mDBEntity.FIRSTNAME;
            string body = createEmailBody(userName, resetLink1);
            MailMessage message = new MailMessage();
            message.To.Add(mDBEntity.USERID);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            bool IsMailSent = SendMailNotification(message);
            // return SalesForce(mDBEntity);
            return IsMailSent;
        }
        private string createEmailBody(string userName, string url)
        {
            string body = string.Empty;
            //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/account-activation-temp.html")))
            //{ body = reader.ReadToEnd(); }

            //body = body.Replace("{user}", ((string.IsNullOrEmpty(userName) != true) ? userName : string.Empty));
            //body = body.Replace("{url}", url);
            //body = body.Replace("{webappurl}", System.Configuration.ConfigurationManager.AppSettings["APPURL"]);
            body = "Test Mail";
            return body;
        }
        public bool SendMailNotification(MailMessage message)
        {
            bool IsMailSent = false;
            SmtpClient mySmtpClient = new SmtpClient();
            if (mySmtpClient.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory && mySmtpClient.PickupDirectoryLocation.StartsWith("~"))
            {
                string root = AppDomain.CurrentDomain.BaseDirectory;
                string pickupRoot = mySmtpClient.PickupDirectoryLocation.Replace("~/", root);
                pickupRoot = pickupRoot.Replace("/", @"\");
                mySmtpClient.PickupDirectoryLocation = pickupRoot;
            }
            try
            {
                mySmtpClient.Send(message);
                IsMailSent = true;
            }
            catch (Exception ex)
            {
              ///pavan  LogError("api/UserManagement", "SendMailNotification", ex.Message, ex.StackTrace);
               //pavan ModelState.AddModelError("", "Issue sending email: " + ex.Message);
            }
            return IsMailSent;
        }
    }
}
