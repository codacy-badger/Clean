using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities.UserManagement
{
    public class TokenDetailsEntity
    {
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
    }
}
