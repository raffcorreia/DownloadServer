<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DownloadServer.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download Server Usage Example</title>
    <style type="text/css">
        h1
        {
            font-size: xx-large;
            text-align: center
        }
        h2
        {
            font-size: large;            
            color: Gray;
            margin-bottom: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            USAGE EXAMPLE
            <br />
        </h1>
        <h2>
            DOWNLOAD COUNT
        </h2>
        <div>
            Number of downloads:<asp:Label ID="lblDownloadsCount" runat="server" Text="0"></asp:Label>
        </div>

        <h2>
            DIRECT FILE LIST
        </h2>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:HyperLinkField
                        DataNavigateUrlFields="File"
                        DataNavigateUrlFormatString="~/Download/{0}"
                        DataTextField="File"
                        HeaderText="Files founded" /> 
                <asp:BoundField DataField="Count" HeaderText="Download count"/>
            </Columns>
        </asp:GridView>
        <h2>
            SUBFOLDER iFrame ORDERING BY DESC
        </h2>

        <iframe src="FileList.aspx?subfolder=ex1&orderby=desc&bgcolor=LightGrey&showdownloadcount=false" width="400" height="250" frameborder="0">
        </iframe>

        <h2>
            SUBFOLDER iFrame FILTERING FILE EXTENSION
        </h2>
        <iframe src="FileList.aspx?subfolder=ex2&orderby=asc&bgcolor=LightGrey&fileextension=bmp&showdownloadcount=true" width="400" height="250" frameborder="0">
        </iframe>

    </form>
</body>
</html>
