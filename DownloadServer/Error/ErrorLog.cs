using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace DownloadServer.Error
{
    public class ErrorLog
    {
        private Exception _exception;
        public Exception exception
        {
            get { return _exception; }
            set { _exception = value; }
        }
        private HttpRequest _request;
        public HttpRequest Request
        {
            get { return _request; }
            set { _request = value; }
        }
        private int _status;
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        public ErrorLog()
        {
            recordLOG();
        }
        public ErrorLog(Exception ex, HttpRequest request)
        {
            this.exception = ex;
            this.Request = request;
            if (!String.IsNullOrEmpty(Request["status"]))
            {
                status = int.Parse(Request["status"]);
            }
            recordLOG();
        }

        private void recordLOG()
        {
            DateTime eventTime = DateTime.UtcNow;
            string date = DateTime.UtcNow.ToString("yyyy_MM_dd");
            string time = DateTime.UtcNow.TimeOfDay.ToString();
            string msg = "";

            if (exception != null)
            {
                msg = exception.Message;

                if (status != 404)
                {
                    recordDetail(eventTime, exception, Request.Url.AbsoluteUri);
                }
            }

            recordSummary(eventTime, msg);
        }

        private void recordSummary(DateTime eventTime, string msg)
        {
            string fileURL = Configuration.LOGPath + "Summary_" + DateTime.UtcNow.ToString("yyyy_MM_dd") + "{0}.config";
            FileInfo fi = new FileInfo(String.Format(fileURL, ""));
            string namecomplement = "";
            
            int count = 1;
            bool nameOK = false;
            while (!nameOK)
            {
                if (fi.Exists)
                {
                    if (File.Exists(String.Format(fileURL, "(" + count + ")")) || fi.Length > 1048576)
                    {
                        namecomplement = "(" + count + ")";
                        fi = new FileInfo(String.Format(fileURL, "(" + count + ")"));
                    }
                    else
                    {
                        nameOK = true;
                    }
                }
                else
                {
                    nameOK = true;
                }
                count++;
            }

            StreamWriter sw = new StreamWriter(String.Format(fileURL, namecomplement), true);
            sw.WriteLine(DateTime.UtcNow.TimeOfDay.ToString() + "; Status= " + status.ToString("###") + "; UserIP= " + Request.UserHostAddress + "; Message= " + msg + "; URL= " + Request.Url.AbsoluteUri);
            sw.Flush();
            sw.Close();
        }

        private void recordDetail(DateTime eventTime, Exception ex, string url)
        {
            StreamWriter swDetail = new StreamWriter(Configuration.LOGPath + eventTime.ToString("yyyy_MM_dd") + "_" + eventTime.ToString("HH_mm_ss_fffffff") + ".config", true);
            swDetail.WriteLine("url:           " + url);
            swDetail.WriteLine("Message:       " + exception.Message);
            swDetail.WriteLine("HashCode:      " + exception.GetHashCode());
            swDetail.WriteLine("TargetSite:    " + exception.TargetSite);
            swDetail.WriteLine("Source:        " + exception.Source);
            swDetail.WriteLine("Type:          " + exception.GetType());
            swDetail.WriteLine("Data:          ");
            swDetail.WriteLine("               " + String.Join(" - ", exception.Data));
            swDetail.WriteLine("BaseException: " + exception.GetBaseException());
            swDetail.WriteLine("StackTrace:");
            swDetail.Write(exception.StackTrace);
            swDetail.Flush();
            swDetail.Close();
        }

        public string GetErrorMessage()
        {
            switch(status)
            {
                case 404:
                    return "We're very sorry we couldn't find the page you are looking for.";
                default:
                    return "We're sorry but this is an error page!";
            }
        }
    }
}