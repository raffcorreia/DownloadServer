using System;
using System.Web;
using System.IO;
using System.Web.SessionState;

namespace DownloadServer
{
    public class DownloadHandler : IHttpHandler, IRequiresSessionState 
    {                                             
        public void ProcessRequest(HttpContext context)
        {
            const long packSize = 65536;
            string fileName = context.Request.Url.Segments[context.Request.Url.Segments.Length - 1];
            FileStream file;
            long offset = 0;
            long rangeBegin = 0;
            long rangeEnd = 0;
            long dataTransfered = 0;
            long fileSize = 0;

            if (context.Session != null)
            {
                try
                {
                    context.Session.Add("ID", context.Session.SessionID);
                }
                catch (Exception)
                {
                    context.Session.Add("ID", DownloadCount.CountDownload().ToString());
                }
            }
            
            if (File.Exists(Configuration.FilesPath + fileName))
            {
                context.Response.Clear();

                file = new FileStream(Configuration.FilesPath + fileName, FileMode.Open, FileAccess.Read);

                fileSize = file.Length;
                rangeBegin = 0;
                rangeEnd = file.Length;

                CalculateRange(context.Request, file.Length, ref rangeBegin, ref rangeEnd);

                //If isn't range
                if (rangeBegin == 0 && (rangeEnd == file.Length))
                {
                    context.Response.StatusCode = 200;
                }
                else
                {
                    context.Response.AppendHeader("Content-Range", "bytes " + rangeBegin + "-" + rangeEnd + "/" + file.Length.ToString());
                    context.Response.StatusCode = 206;
                }

                context.Response.AppendHeader("Content-Length", file.Length.ToString());
                context.Response.AppendHeader("Last-Modified", File.GetLastAccessTimeUtc(Configuration.FilesPath + fileName).ToString());
                context.Response.AppendHeader("Accept-Ranges", "bytes");
                context.Response.AppendHeader("ETag", "id_test");
                context.Response.ContentType = "application/octet-stream";

                if (!context.Request.HttpMethod.Equals("HEAD"))
                {
                    context.Response.Flush();

                    offset = rangeBegin;
                    int readCount;
                    byte[] buffer = new Byte[packSize];
                    file.Seek(offset, SeekOrigin.Begin);
                    while (context.Response.IsClientConnected && offset < rangeEnd)
                    {
                        readCount = file.Read(buffer, 0, (int)Math.Min(rangeEnd - rangeBegin, buffer.Length));

                        context.Response.OutputStream.Write(buffer, 0, readCount);
                        context.Response.Flush();

                        offset += readCount;
                        dataTransfered += readCount;
                    }
                }
                file.Dispose();
                file.Close();
            }
            else
            {
                context.Response.StatusCode = 404;
            }

            DownloadCount.AddDownload(context, fileName, fileSize, dataTransfered, rangeBegin, rangeEnd);
            context.Response.End();
        }

        private void CalculateRange(HttpRequest request, long fileSize, ref long byteIni, ref long byteEnd)
        {
            try
            {
                string[] range = request.Headers["Range"].Split('=');
                if (range[0].ToLower() == "bytes" && range.Length > 1)
                {
                    string[] values = range[1].Split('-');
                    byteIni = long.Parse(values[0]);

                    if (values.Length > 1)
                    {
                        if (values[1] != "")
                        {
                            byteEnd = Math.Min(long.Parse(values[1]), fileSize);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}