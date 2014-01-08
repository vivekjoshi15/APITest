<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendMessage.aspx.cs" Inherits="VNTAPI.sendMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 700px; margin: auto;">
            select device:
        <asp:DropDownList ID="DropDownList1" runat="server" Height="17px" Width="122px">
        </asp:DropDownList>
            <br />
            Send message:<br />
            <asp:TextBox ID="TextBox1" runat="server" Height="99px" TextMode="MultiLine" Width="261px"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Send Message To Device" />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>

        </div>
    </form>
</body>
</html>
