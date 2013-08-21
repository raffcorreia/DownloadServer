using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections.Specialized;

namespace DownloadServer
{
    public static class DownloadCount
    {

        public static int AddDownload(NameValueCollection ServerVariables, String fileName)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO tb_FWM_downloads  VALUES ( ");
            sql.Append("NULL, ");
            sql.Append("NOW(), ");
            sql.Append("'" + fileName + "', ");
            sql.Append("'" + ServerVariables["REMOTE_ADDR"] + "', ");
            sql.Append("'" + ServerVariables["REMOTE_HOST"] + "', ");
            sql.Append("'" + ServerVariables["HTTP_ACCEPT_LANGUAGE"] + "', ");
            sql.Append("'" + ServerVariables["HTTP_HOST"] + "', ");
            sql.Append("'" + ServerVariables["HTTP_USER_AGENT"] + "' ");
            sql.Append(")");

            return DataBase.ExecuteNonQuery(sql.ToString());
        }

        public static int CountDownload(String fileName)
        {
            string where = "";
            if (fileName != "")
            {
                where = " WHERE file_name = '" + fileName + "'";
            }
            return DataBase.ExecuteScalarInt("SELECT COUNT(id) FROM tb_FWM_downloads" + where);
        }
    }
}