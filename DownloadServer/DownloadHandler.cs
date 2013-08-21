using System;
using System.Web;
using System.IO;

namespace DownloadServer
{
    public class DownloadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request.Url.Segments[context.Request.Url.Segments.Length - 1];
            context.ApplicationInstance.CompleteRequest();
            string filePath = Configuration.FilesPath;
            FileStream file = null;

            if (File.Exists(filePath + fileName))
            {
                file = new FileStream(filePath + fileName, FileMode.Open, FileAccess.Read);

                context.Response.Clear();

                long byteIni = 0;
                long byteEnd = file.Length - 1;

                CalculateRange(context.Request, file.Length, ref byteIni, ref byteEnd);

                //If isn't range
                if (byteIni == 0 && byteEnd == file.Length)
                {
                    context.Response.StatusCode = 200;
                }
                else
                {
                    context.Response.AppendHeader("Content-Range", "bytes " + byteIni + "-" + byteEnd + "/" + file.Length.ToString());
                    context.Response.StatusCode = 206;
                }

                context.Response.AppendHeader("Content-Length", file.Length.ToString());
                FileInfo fi = new FileInfo(filePath + fileName);
                context.Response.AppendHeader("Last-Modified", fi.LastAccessTimeUtc.ToString());
                context.Response.AppendHeader("Accept-Ranges", "bytes");
                context.Response.AppendHeader("ETag", "id_test");
                context.Response.ContentType = "application/octet-stream";

                if (context.Request.HttpMethod.Equals("HEAD"))
                {
                    //Only asking for the head so, exit! But need a LOG
                }
                else
                {
                    context.Response.Flush();

                    long offset = byteIni;
                    int readCount;
                    byte[] buffer = new Byte[65536];
                    while (context.Response.IsClientConnected && offset < byteEnd)
                    {
                        file.Seek(offset, SeekOrigin.Begin);

                        readCount = file.Read(buffer, 0, (int)Math.Min(byteEnd - byteIni, buffer.Length));

                        context.Response.OutputStream.Write(buffer, 0, readCount);
                        context.Response.Flush();

                        offset += readCount;
                        buffer = new Byte[25000];
                    }
                }
                if (context.Response.IsClientConnected)
                {
                    //DownloadCount.AddDownload(context.Request.ServerVariables, fileName);
                }
                file.Dispose();
                file.Close();
                context.Response.End();
            }
            else
            {
                context.Response.StatusCode = 404;
            }
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
                            byteEnd = Math.Min(long.Parse(values[1]), fileSize - 1);
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