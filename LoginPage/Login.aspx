<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style4 {
            font-weight: normal;
        }
        .auto-style7 {
            width: 178px;
            text-align: right;
        }
        .auto-style8 {
            width: 178px;
        }
        .auto-style13 {
            width: 32px;
            text-align: right;
        }
        .auto-style14 {
            width: 32px;
        }
        .auto-style15 {
            width: 184px;
            text-align: center;
        }
        .auto-style16 {
            width: 184px;
        }
        .auto-style17 {
            width: 32px;
            font-weight: normal;
            text-align: left;
        }
        .auto-style18 {
            width: 184px;
            text-align: center;
            font-weight: normal;
        }
    </style>
</head>
<body style="font-weight: 700; font-size: x-large; text-align: center">
    <form id="form1" runat="server">
    <div>
    
        Login Page</div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style7"><span class="auto-style4">Username</td>
                <td class="auto-style13"><span class="auto-style4">
                    <asp:TextBox ID="TBusername" runat="server" style="text-align: left" Width="180px"></asp:TextBox>
                    </span></td>
                <td class="auto-style15">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBusername" ErrorMessage="Please enter Username" Width="180px"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style7">Password</span></td>
                <td class="auto-style17">
                    <asp:TextBox ID="TBpassword" runat="server" style="text-align: left" Width="180px"></asp:TextBox>
                </td>
                <td class="auto-style18">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TBpassword" ErrorMessage="Please enter Password" Width="180px"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style14">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style14">
                    <asp:Button ID="LoginButton" runat="server" OnClick="Button1_Click" Text="Log in " />
                </td>
                <td class="auto-style16">&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
