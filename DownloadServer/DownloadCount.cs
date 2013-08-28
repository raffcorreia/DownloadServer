using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections.Specialized;

namespace DownloadServer
{
    public class DownloadCount
    {
        DataBase db;

        public DownloadCount()
        {
            db = new DataBase();
        }

        public int AddDownload(HttpContext context, String fileName, long fileSize, long bytesTransfered, long rangeBegin, long rangeEnd)
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
            sql.Append("tb_" + Configuration.DataBaseTablesPrefix + "downloads  VALUES ( ");
            sql.Append("NULL, ");
            sql.Append("NOW(), ");
            sql.Append("'" + fileName + "', ");
            sql.Append(fileSize.ToString() + ", ");
            sql.Append(bytesTransfered.ToString() + ", ");
            sql.Append(rangeBegin.ToString() + ", ");
            sql.Append(rangeEnd.ToString() + ", ");
            sql.Append(context.Response.StatusCode.ToString() + ", ");
            sql.Append(Convert.ToUInt16(context.Request.HttpMethod.Equals("HEAD")) + ", ");
            sql.Append("'" + sessionId + "', ");
            sql.Append("'" + context.Request.ServerVariables["REMOTE_ADDR"] + "', ");
            sql.Append("'" + context.Request.ServerVariables["REMOTE_HOST"] + "', ");
            sql.Append("'" + context.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"] + "', ");
            sql.Append("'" + context.Request.ServerVariables["HTTP_HOST"] + "', ");
            sql.Append("'" + context.Request.ServerVariables["HTTP_USER_AGENT"] + "' ");
            sql.Append(")");

            return db.ExecuteNonQuery(sql.ToString());
        }

        public int CountDownload(String fileName = "")
        {
            string where = "";
            if (fileName != "")
            {
                where = "WHERE file_name = '" + fileName + "'";
            }
            return db.ExecuteScalarInt("SELECT SUM(total) FROM vw_" + Configuration.DataBaseTablesPrefix + "downloads_per_file " + where);
        }
    }
}