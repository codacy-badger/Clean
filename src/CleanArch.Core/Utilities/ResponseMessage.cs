using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CleanArch.Core.Utilities
{
    public class ResponseMessage
    {
        public static HttpResponseMessage BaseResponse(dynamic data, HttpStatusCode code)
        {
            return new HttpResponseMessage()
            {
                Content = new JsonContent(new
                {
                    StatusCode = code,
                    Data = data
                })
            };
        }
        
    }
}
