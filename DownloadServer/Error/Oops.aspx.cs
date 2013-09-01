using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DownloadServer.Error
{
    public partial class Oops : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorLog err = new ErrorLog(Server.GetLastError(), Request);
            lblMessage.Text = err.GetErrorMessage();
        }
    }
}