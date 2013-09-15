using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace DownloadServer
{
    public partial class Default : System.Web.UI.Page
    {
        DownloadCount dc;


        protected void Page_Load(object sender, EventArgs e)
        {
            dc = new DownloadCount();
            loadGrid();
            lblDownloadsCount.Text = dc.Count().ToString();
        }

        private void loadGrid()
        {
            List<Item> itemsList = new List<Item>();

            try
            {
                DirectoryInfo di = new DirectoryInfo(Configuration.FilesPath);
                FileInfo[] files = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);

                foreach (FileInfo d in files)
                {
                    itemsList.Add(new Item(d.Name, dc.Count(d.Name)));
                }

            }
            catch (Exception)
            {

            }

            GridView1.DataSource = itemsList;
            GridView1.DataBind();
        }
    }
}