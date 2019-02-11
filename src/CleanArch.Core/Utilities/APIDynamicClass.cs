using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CleanArch.Core.Utilities
{
    public class APIDynamicClass
    {
        
        public static string GetEnvironmentName()
        {

            string environment = "AGILE";//ConfigurationManager.AppSettings["EnvironmentName"];
            if (environment.ToUpper().Contains("AGILE"))
            {
                environment = "AGL";
            }
            else if (environment.ToUpper().Contains("SIT"))
            {
                environment = "SIT";
            }
            else if (environment.ToUpper().Contains("DEMO"))
            {
                environment = "DEM";
            }
            else if (environment.ToUpper().Contains("DEV"))
            {
                environment = "DEV";
            }
            else if (environment.ToUpper() == string.Empty)
            {
                environment = "PRD";
            }
            return environment;
        }

    }
}
