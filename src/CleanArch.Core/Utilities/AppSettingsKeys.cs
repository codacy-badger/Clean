using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Utilities
{
    public class AppSettingsKeys
    {
        public string GetConnectionString(IConfiguration configuration, string Key)
        {
            string FOCISConnectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            string SSPConnectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            // var connectionString = dbConnectionMode == DBConnectionMode.FOCiS ? FOCISConnectionString : SSPConnectionString;
            //configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            var key = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            return key;
        }
    }
}
