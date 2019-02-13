using log4net;
using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CleanArch.Core.Utilities
{
    public class OneSignal 
    {
        IConfiguration _Configuration;
        protected readonly string OKey = string.Empty;
        protected readonly string OId = string.Empty;
        private static readonly ILog Log =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        
        public OneSignal()
        {
           
            //IConfiguration configuration;
            //OKey = obj.appley(Configuration1, "OneSignalKey");
            // OId = obj.appley(Configuration, "OneSignalId");
            //OKey = ShipaConfiguration.InternalUrls.OneSignalKey;
            //OId = ShipaConfiguration.InternalUrls.OneSignalId;
            OKey = _Configuration.GetSection("KeyList").GetSection("OneSignalKey").Value;
            OId= _Configuration.GetSection("KeyList").GetSection("OneSignalId").Value;
            //OKey = "Y2M1MmViYTUtOWU3Mi00ZmRlLWJjMmEtZjFiZWRmNzVkNGUz";
           // OId = "ce7098f9-e5ed-429a-bbfc-888e8b5018e2";
        }
       

        public bool SendPushMessageByTag(string tagName, string tagValue, string message, dynamic data)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic " + OKey);

            // var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var obj = new
            {
                app_id = OId,
                contents = new { en = message },
                data = new { Data = data },
                filters = new object[] {
                     new {
                        field = "tag",
                        key = tagName,
                        relation = "=",
                        value = tagValue
                    }
                }
            };
            var param = JsonConvert.SerializeObject(obj);
            //var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                    if (response.StatusCode == HttpStatusCode.OK) return true;
                    return false;
                }
            }
            catch (WebException ex)
            {
                Log.ErrorFormat(String.Format("Error Message: {0},  {1}", ex.Message.ToString(), DateTime.Now.ToString()));
                Log.ErrorFormat(String.Format("Error StackTrace: {0},  {1}", ex.StackTrace.ToString(), DateTime.Now.ToString()));
                return false;
            }
        }

    }
}
