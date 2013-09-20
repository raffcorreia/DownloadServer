using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace DownloadServer
{
    public partial class FileList : System.Web.UI.Page
    {
        Color color;
        string OrderBy;
        string SubFolder;
        string FileExtension;
        bool ShowDownloadCount;

        DownloadCount dc;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetParameters();
            loadGrid();
        }
        
        private void GetParameters(){
            try
            {
                SubFolder = Request["subfolder"];
                SubFolder = SubFolder.Replace('\\', '\0');
                SubFolder = SubFolder.Replace('/', '\0');
            }
            catch (Exception)
            {
                SubFolder = "";
            }
            try
            {
                OrderBy =  Request["orderby"].ToUpper();
                if (OrderBy != "ASC" && OrderBy != "DESC")
                {
                    OrderBy = "ASC";
                }
            }
            catch (Exception)
            {
                OrderBy = "ASC";
            }
            try
            {
                color = ColorTranslator.FromHtml(Request["bgcolor"].ToString());
            }
            catch (Exception)
            {
                color = Color.White;
            }
            body.Attributes["bgcolor"] = ColorTranslator.ToHtml(color);
            try
            {
                FileExtension = Request["fileextension"].ToUpper();
                if (FileExtension == "")
                {
                    FileExtension = "*";
                }
            }
            catch (Exception)
            {
                FileExtension = "*";
            }
            try
            {
                ShowDownloadCount = (Request["showdownloadcount"].ToUpper() == "TRUE");
                if (ShowDownloadCount)
                {
                    dc = new DownloadCount();
                    GridView1.Columns[1].Visible = true;
                }
            }
            catch (Exception)
            {
                ShowDownloadCount = false;
            }
        }

        private void loadGrid()
        {
            List<Item> itemsList = new List<Item>();
            string[] files;

            string name;

            try
            {
                files = Directory.GetFiles(Configuration.FilesPath + "\\" + SubFolder + "\\", FileExtension, SearchOption.TopDirectoryOnly);

                if (OrderBy == "ASC")
                {
                    Array.Sort(files);
                }
                else
                {
                    Array.Reverse(files);
                }

                for (int x = 0; x < files.Length; x++)
                {
                    name = files[x].Substring(files[x].LastIndexOf('\\')+1);
                    itemsList.Add(new Item(name, GetDownloadCount(name)));
                }
                GridView1.DataSource = itemsList;
                GridView1.DataBind();           
            }
            catch (Exception)
            {

            }
        }

        private int GetDownloadCount(string name)
        {
            if (dc != null)
            {
                return dc.Count(name);
            }
            return 0;
        }
    }
}