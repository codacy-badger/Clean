using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Model.Response
{
    [Serializable]
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
