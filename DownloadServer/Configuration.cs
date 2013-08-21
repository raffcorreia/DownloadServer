using System;
using System.Configuration;

namespace DownloadServer
{
    public static class Configuration
    {
        public static string FilesPath 
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["FilesRelativePath"];
            }
        }
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
            }
        }
        public static string DataBaseTablesPrefix
        {
            get
            {
                return ConfigurationManager.AppSettings["DataBaseTablesPrefix"];
            }
        }
    }
}