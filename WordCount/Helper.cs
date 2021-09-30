using System;
using System.Configuration;
using System.Data.Common;
using System.Web;

namespace WordCount
{
    public class Helper
    {
        public static string CnnVal(string name)
        {
            // lav til rigtig klasse og brug dependency injection
            string connectionStringSettings = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            
            return connectionStringSettings;
        }
    }
}
