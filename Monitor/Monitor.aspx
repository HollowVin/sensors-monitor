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
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:ListBox ID="ListBoxSensors" runat="server" Height="146px" Width="244px"></asp:ListBox>
                    <asp:TextBox ID="TextLog" runat="server" Height="136px" Width="188px"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <div id="DivSensorValues">
        </div>
    </form>
</body>
</html>
