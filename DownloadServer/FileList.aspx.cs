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
                FileExtension = FileExtension.Replace('.', '\0');
            }
            catch (Exception)
            {
                FileExtension = "";
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
            FileInfo[] files;
            
            try
            {
                DirectoryInfo di = new DirectoryInfo(Configuration.FilesPath + "\\" + SubFolder + "\\");
                files = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);

                if (OrderBy == "ASC")
                {
                    Array.Sort(files, (a, b) => a.Name.CompareTo(b.Name));
                    //files = files.OrderBy(x => x.Name);
                }
                else
                {
                    Array.Reverse(files);
                    //files.OrderByDescending(x => x.Name);
                }
                foreach (FileInfo d in files)
                {
                    itemsList.Add(new Item(d.Name, GetDownloadCount(d.Name)));
                }

            }
            catch (Exception)
            {

            }

            GridView1.DataSource = itemsList;
            GridView1.DataBind();
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