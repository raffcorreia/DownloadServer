<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DownloadServer.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Number of downloads:<asp:Label ID="lblDownloadsCount" runat="server" Text="0"></asp:Label>
            <br />
        </div>
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
    </form>
</body>
</html>
