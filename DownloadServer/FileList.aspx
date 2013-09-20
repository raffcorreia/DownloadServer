<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileList.aspx.cs" Inherits="DownloadServer.FileList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body id="body" runat="server">
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BorderWidth="0px" GridLines="None" ShowHeader="False">
            <Columns>
                <asp:HyperLinkField
                        DataNavigateUrlFields="File"
                        DataNavigateUrlFormatString="~/Download/{0}"
                        DataTextField="File" /> 
                <asp:BoundField DataField="Count" Visible="False"/>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
