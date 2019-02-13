using CleanArch.Infrastructure.CommunicationRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Net.Mail;
using Xunit;

namespace CleanArch.Infrastructure.Test
{
    public class MailServiceTests
    {
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IHostingEnvironment> _host;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private IConfigurationRoot config; 
        MailService _mailService;
        public MailServiceTests()
        {
            _configuration = new Mock<IConfiguration>(MockBehavior.Loose);
            _host = new Mock<IHostingEnvironment>(MockBehavior.Loose);
            _httpContextAccessor = new Mock<IHttpContextAccessor>(MockBehavior.Loose);
            _mailService = new MailService(_configuration.Object,_host.Object,_httpContextAccessor.Object);

            config = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .Build();            
        }
        [Fact]
        public void Check_NotifictionMail_Sent()
        {
            SmtpClient smtpClient = new SmtpClient();
               smtpClient.Host= config["Email:Smtp:Host"].ToString();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtpClient.PickupDirectoryLocation = @"~/";

            //
            //Arrange
            string userId = "pkumaras@hcl.com";
            MailMessage message = new MailMessage();
            message.To.Add(userId);
            message.From =new MailAddress("vijaypavankumar.a@hcl.com");
            message.Subject = "Account Activation for user "+userId;
            message.Body = "Account Activation Test Mail";

            //
            // Act
            bool response = _mailService.SendMailNotification(message,smtpClient);

            //
            // Assert            
            Assert.True(response,"Notification Mail Sent.");

        }
    }
}
