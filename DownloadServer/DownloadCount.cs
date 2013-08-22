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

        public static int AddDownload(HttpContext context, String fileName, long fileSize, long bytesTransfered, long rangeBegin, long rangeEnd)
        {
            StringBuilder sql = new StringBuilder();
            string sessionId = "";

            try
            {
                sessionId = context.Session.SessionID;
            }
            catch(Exception)
            {
                sessionId = CountDownload().ToString();
            }

            sql.Append("INSERT INTO ");
            sql.Append(Configuration.DataBaseTablesPrefix + "downloads  VALUES ( ");
            sql.Append("NULL, ");
            sql.Append("NOW(), ");
            sql.Append("'" + fileName + "', ");
            sql.Append(fileSize.ToString() + ", ");
            sql.Append(bytesTransfered.ToString() + ", ");
            sql.Append(rangeBegin.ToString() + ", ");
            sql.Append(rangeEnd.ToString() + ", ");
            sql.Append(context.Response.StatusCode.ToString() + ", ");
            sql.Append("'" + context.Request.HttpMethod.Equals("HEAD").ToString() + "', ");
            sql.Append("'" + sessionId + "', ");
            sql.Append("'" + context.Request.ServerVariables["REMOTE_ADDR"] + "', ");
            sql.Append("'" + context.Request.ServerVariables["REMOTE_HOST"] + "', ");
            sql.Append("'" + context.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"] + "', ");
            sql.Append("'" + context.Request.ServerVariables["HTTP_HOST"] + "', ");
            sql.Append("'" + context.Request.ServerVariables["HTTP_USER_AGENT"] + "' ");
            sql.Append(")");

            return DataBase.ExecuteNonQuery(sql.ToString());
        }

        public static int CountDownload(String fileName = "")
        {
            string where = "";
            if (fileName != "")
            {
                where = " WHERE file_name = '" + fileName + "'";
            }
            return DataBase.ExecuteScalarInt("SELECT COUNT(id) FROM " + Configuration.DataBaseTablesPrefix + "downloads" + where);
        }
    }
}