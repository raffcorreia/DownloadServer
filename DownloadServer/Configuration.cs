using System;
using System.Configuration;

namespace DownloadServer
{
    public static class Configuration
    {
        public static string BaseDir
        {
            get
            {
                string ret = AppDomain.CurrentDomain.BaseDirectory;
                if (!ret.EndsWith("\\"))
                {
                    ret += "\\";
                }
                return ret;
            }
        }
        public static bool ArePathsRelative
        {
            get
            {
                return ConfigurationManager.AppSettings["ArePathsRelative"].ToLower() == "true";
            }
        }        
        public static string LOGPath
        {
            get
            {
                string path = ConfigurationManager.AppSettings["LOGPath"];
                if (path[0] == '\\')
                {
                    path = path.Substring(1);
                }
                if(!path.EndsWith("\\"))
                {
                    path += "\\";
                }
                
                if (ArePathsRelative)
                {
                    return BaseDir + path;
                }
                else
                {
                    return path;
                }
            }
        }        
        public static string FilesPath 
        {
            get
            {
                string path = ConfigurationManager.AppSettings["FilesPath"];
                if (path[0] == '\\')
                {
                    path = path.Substring(1);
                }
                if (!path.EndsWith("\\"))
                {
                    path += "\\";
                }

                if (ArePathsRelative)
                {
                    return BaseDir + path;
                }
                else
                {
                    return path;
                }
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