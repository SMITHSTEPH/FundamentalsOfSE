<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
	<meta charset="utf-8" />
</head>
<body id="Body" runat="server">
    <form id="form1" runat="server">
    <div>
        
    <h1>&nbsp;Process Model Home Page</h1>
        <p><asp:Label ID="lbUser" runat="server"></asp:Label></p>

    <p>
        Username&nbsp;
        <asp:TextBox ID="txtUsername" runat="server" Width="120px"></asp:TextBox>
        </p>
    <p>
        Password&nbsp;&nbsp; <asp:TextBox id="txtPsBox" TextMode="password" runat="server" Width="120px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:Button id="btnLogin" Text="Login" runat="server" OnClick ="btnLogin_Click"/></p>

    <asp:DropDownList ID="Select1" runat="server" >
        <asp:ListItem>Team Member</asp:ListItem>
        <asp:ListItem>Team Leader</asp:ListItem>
        <asp:ListItem>Admin</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button id="btnSignUp" Text="Sign Up" runat="server" OnClick ="btnSignUp_Click"/>
    </div>
    </form>
</body>
</html>
