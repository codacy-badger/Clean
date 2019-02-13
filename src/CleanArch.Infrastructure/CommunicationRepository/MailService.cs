using CleanArch.Core.Entities.UserManagement;
using CleanArch.Core.Repository.Communication;
using CleanArch.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.CommunicationRepository
{
    public class MailService : APIDynamicClass, ICommunicationService
    {

        IConfiguration _Configuration;
        IHostingEnvironment _host;
        IHttpContextAccessor _httpContextAccessor;


        public MailService(IConfiguration configuration, IHostingEnvironment host, IHttpContextAccessor httpContextAccessor)
        {
            _Configuration = configuration;
            _host = host;
            _httpContextAccessor = httpContextAccessor;

        }
        public bool Sendmail(UserEntity mDBEntity, string Token)
        {
            var oneSignalService = new OneSignal();
            var notData = new { NOTIFICATIONTYPEID = 5111, NOTIFICATIONCODE = "Profile Incomplete" };
            oneSignalService.SendPushMessageByTag("Email", mDBEntity.USERID, "Profile Incomplete", notData);
            string EnvironmentName = APIDynamicClass.GetEnvironmentName();
            string subject = "Account Activation for user" + mDBEntity.USERID + EnvironmentName;
            var context = _httpContextAccessor.HttpContext;
            string appurl = _Configuration.GetSection("APPURLList").GetSection("APPURL").Value;

            //Multiple Parameters
            var queryParams = new Dictionary<string, string>()
                {
                    {"rt", Token },
                    {"UserId", mDBEntity.EMAILID },
                    {"ReturnUrl",EncryptStringAES("/") }
                };
            var url = QueryHelpers.AddQueryString("/api/UserManagement/ActivateUser", queryParams);

            //pavan this.Url.Link("DefaultApi", new { Controller = "UserManagement", Action = "ActivateUser", rt = Token, UserId = mDBEntity.EMAILID, ReturnUrl = EncryptStringAES("/") });
            string urlparams = url.Split('?')[1];
            string resetLink1 = appurl + "UserManagement/ActivateUser?" + urlparams.Split('&')[0] + "&" + urlparams.Split('&')[1] + "&" + urlparams.Split('&')[2];
            string userName = mDBEntity.FIRSTNAME;
            string body = createEmailBody(userName, resetLink1);
            MailMessage message = new MailMessage();
            message.To.Add(mDBEntity.USERID);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            bool IsMailSent = SendMailNotification(message);
            return IsMailSent;
        }
        private string createEmailBody(string userName, string url)
        {
            string body = string.Empty;
            var contentroot = _host.ContentRootPath;
            using (StreamReader reader = new StreamReader(contentroot + "\\App_Data\\account-activation-temp.html"))
            { body = reader.ReadToEnd(); }
            body = body.Replace("{user}", ((string.IsNullOrEmpty(userName) != true) ? userName : string.Empty));
            body = body.Replace("{url}", url);
            body = body.Replace("{webappurl}", _Configuration.GetSection("APPURLList").GetSection("APPURL").Value);


            return body;
        }
        public bool SendMailNotification(MailMessage message, SmtpClient mySmtpClient)
        {
            bool IsMailSent = false;
           
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
        public static string EncryptStringAES(string plainText)
        {

            var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            var EncriptedFromJavascript = EncryptStringToBytes(plainText, keybytes, iv);
            return Convert.ToBase64String(EncriptedFromJavascript);
        }
        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
    }
}
