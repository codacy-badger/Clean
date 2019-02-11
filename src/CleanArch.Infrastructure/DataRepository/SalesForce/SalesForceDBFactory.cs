using CleanArch.Infrastructure.Core;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Infrastructure.DataRepository.SalesForce
{
    public class SalesForceDBFactory : DBFactoryCore
    {
        IConfiguration configuration;
        public SalesForceDBFactory(IConfiguration _configuration)
        {

            configuration = _configuration;
        }

        public int GetTranscationNumber()
        {
            int salesforceidcount = 0;
            try
            {
                salesforceidcount = this.ExecuteScalar<int>(GetConnectionString(configuration, DBConnectionMode.SSP), "SELECT COUNT(TRANSCATIONID)+1 FROM  SALESFORCETRANSCATION",
                           new OracleParameter[]{

                    });
            }
            catch (Exception ex)
            {
                // throw new ShipaDbException(ex.ToString());
            }
            return salesforceidcount;
        }
    }
}
