using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Utilities
{
    public class ShipaConfiguration
    {
        #region Accessing Keys from config file 
        internal static string GetActiveCampaignApiKey()
        {
            return GetValueforKey("ActiveCampaignApiKey");
        }
        internal static string GetActiveCampaignApiUrl()
        {
            return GetValueforKey("ActiveCampaignApiUrl");
        }
        internal static string ListGuestUser()
        {
            return GetValueforKey("ListGuestUser");
        }
        internal static string ListRegisteredUser()
        {
            return GetValueforKey("ListRegisteredUser");
        }
        internal static string SFSubScriberUser()
        {
            return GetValueforKey("SFSubScriberUser");
        }
        internal static string SFUnSubScriberUser()
        {
            return GetValueforKey("SFUnSubScriberUser");
        }
        internal static string ListUserAddress()
        {
            return GetValueforKey("sender_addr1");
        }
        internal static string UserCountry()
        {
            return GetValueforKey("sender_country");
        }
        internal static string SenderZipCode()
        {
            return GetValueforKey("sender_zip");
        }
        internal static string SenderUrl()
        {
            return GetValueforKey("sender_url");
        }
        internal static string SenderName()
        {
            return GetValueforKey("sender_name");
        }
        internal static string SenderCity()
        {
            return GetValueforKey("sender_city");
        }
        internal static string SenderReminder()
        {
            return GetValueforKey("sender_reminder");
        }
        internal static string ShipaApiUrl()
        {
            return GetValueforKey("APPURL");
        }

        internal static string MQInputQueueName()
        {
            return GetValueforKey("INPUTQUEUE_NAME");
        }
        internal static string MQOutputQueueName()
        {
            return GetValueforKey("OUTPUTQUEUE_NAME");
        }
        internal static string MQQueueManagerName()
        {
            return GetValueforKey("QUEUE_MANAGER_NAME");
        }
        internal static string HostName()
        {
            return GetValueforKey("HOST_NAME");
        }
        internal static string ChannelProperty()
        {
            return GetValueforKey("CHANNEL_PROPERTY");
        }
        internal static string SenderID()
        {
            return GetValueforKey("SenderID");
        }
        internal static string PartnerID()
        {
            return GetValueforKey("PartnerID");
        }
        internal static string ReceiverID()
        {
            return GetValueforKey("ReceiverID");
        }
        internal static string ShipaClientID()
        {
            return GetValueforKey("ShipaClientID");
        }
        internal static string CreatedDateTimeZone()
        {
            return GetValueforKey("CreatedDateTimeZone");
        }
        internal static string VersionNumber()
        {
            return GetValueforKey("VersionNumber");
        }
        internal static string Internal()
        {
            return GetValueforKey("Internal");
        }
        internal static string External()
        {
            return GetValueforKey("External");
        }
        internal static string Active()
        {
            return GetValueforKey("Active");
        }
        internal static string InActive()
        {
            return GetValueforKey("InActive");
        }
        internal static string Registered()
        {
            return GetValueforKey("Registered");
        }
        internal static string Guest()
        {
            return GetValueforKey("Guest");
        }
        internal static string ShipaNewFrontEndUrl()
        {
            return GetValueforKey("SHIPAAPPNEWFRONENDURL");
        }

        internal static string PaymentInProgress()
        {
            return GetValueforKey("PaymentInProgress");
        }
        internal static string Jobintiated()
        {
            return GetValueforKey("Jobintiated");
        }

        internal static string ReceivedFromSalesForce()
        {
            return GetValueforKey("ReceivedFromSalesForce");
        }
        public static string IBMMQCheck()
        {
            return GetValueforKey("IBMMQCheck");
        }
        #endregion
        private static string GetValueforKey(string Key)
        {
            return "Off";//pavanConfigurationManager.AppSettings[Key].ToString();

        }

    }

    public class ShipaApiException : Exception
    {
        public ShipaApiException(string message)
            : base(message)
        {
        }
    }
}
