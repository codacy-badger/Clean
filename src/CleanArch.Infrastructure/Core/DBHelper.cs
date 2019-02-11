using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleanArch.Infrastructure.Core
{
   public class DBHelper
    {
        IConfiguration configuration;
        public DBHelper(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IDbConnection GetConnection()
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }
        public DBHelper()
        {
            var dfd=this.GetConnection();
            //var SSPString = this.GetConnection();
            //SSPConnectionString =Convert.ToString(SSPString);
            //FOCISConnectionString = "";// System.Configuration.ConfigurationManager.ConnectionStrings["FOCiS"].ConnectionString;
        }
    }
}
