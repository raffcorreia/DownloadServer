using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DownloadServer
{
    public class Item
    {
        private string _File;
        public string File
        {
            get { return _File; }
            set { _File = value; }
        }

        private int _Count;
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        public Item(string link, int c2)
        {
            File = link;
            Count = c2;
        }
    }
}