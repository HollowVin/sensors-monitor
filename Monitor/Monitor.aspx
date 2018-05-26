<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Monitor.aspx.vb" Inherits="Monitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 140px;
            width: 231px;
        }
        #TextAreaLog {
            height: 140px;
            width: 197px;
        }
    </style>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="newStyle1" style="height: 101px">
                Monitor de sensores</div>
    <div class="content">
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    IP:
                    <asp:Label ID="LabelIP" runat="server"></asp:Label>
                    <br />
                    Port:
                    <asp:Label ID="LabelPort" runat="server"></asp:Label>
                    <br />
                    <br />
                    Connected Sensors:
                    <asp:Label ID="LabelNSensors" runat="server"></asp:Label>
                    <br />
                    Status:
                    <asp:Label ID="LabelStatus" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Table ID="Table1" runat="server" GridLines="Both" Width="329px">
                        <asp:TableRow runat="server" Font-Bold="True">
                            <asp:TableCell runat="server">Sensor</asp:TableCell>
                            <asp:TableCell runat="server">Value</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">Temperature</asp:TableCell>
                            <asp:TableCell runat="server">(N/A)</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br />
                    <asp:Timer ID="Timer1" runat="server" Interval="1000">
                    </asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
    </form>
    </div>
</body>
</html>
