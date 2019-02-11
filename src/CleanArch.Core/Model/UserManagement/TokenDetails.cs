using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Model.UserManagement
{
    public class TokenDetails
    {
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
    }
}
