<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Monitor.aspx.vb" Inherits="Monitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            IP:
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            Port:
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <div>
            </div>
            <br />
            Temperature:
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
