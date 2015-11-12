<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 55%;
        }
        .style3
        {
            width: 267px;
        }
        .style4
        {
            height: 23px;
            width: 267px;
            text-align: right;
        }
        .style5
        {
            width: 267px;
            text-align: right;
        }
        .style6
        {
            width: 267px;
            text-align: right;
            height: 26px;
        }
        .style8
        {
            height: 26px;
            width: 181px;
        }
        .style9
        {
            height: 23px;
            width: 181px;
        }
        .style10
        {
            width: 181px;
        }
        .style11
        {
            height: 17px;
            width: 267px;
            text-align: right;
        }
        .style12
        {
            height: 17px;
            width: 181px;
        }
        .style13
        {
            height: 26px;
            width: 552px;
            text-align: left;
        }
        .style14
        {
            height: 23px;
            width: 552px;
            text-align: left;
        }
        .style15
        {
            width: 552px;
        }
        .style16
        {
            height: 17px;
            width: 552px;
            text-align: left;
        }
        .style17
        {
            width: 552px;
            text-align: left;
        }
        #Reset1
        {
            width: 62px;
            height: 27px;
        }
        .style18
        {
            width: 267px;
            height: 31px;
            text-align: right;
        }
        .style19
        {
            width: 181px;
            height: 31px;
        }
        .style20
        {
            width: 552px;
            height: 31px;
        }
        .auto-style1 {
            height: 18px;
            width: 267px;
            text-align: right;
        }
        .auto-style2 {
            height: 18px;
            width: 181px;
        }
        .auto-style3 {
            height: 18px;
            width: 552px;
            text-align: left;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table class="style1">
        <tr>
            <td class="style6">
                User Name:</td>
            <td class="style8">
                <asp:TextBox ID="TextBoxUName" runat="server" Width="180px"></asp:TextBox>
            </td>
            <td class="style13">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TextBoxUName" ErrorMessage="User Name is Required." 
                    ForeColor="#FF5050"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style4">
                E-mail:</td>
            <td class="style9">
                <asp:TextBox ID="TextBoxEmail" runat="server" Width="180px"></asp:TextBox>
            </td>
            <td class="style14">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="TextBoxEmail" ErrorMessage="E-mail is required." 
                    ForeColor="#FF5050"></asp:RequiredFieldValidator>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="TextBoxEmail" ErrorMessage="Invalid E-mail address." 
                    ForeColor="#FF5050" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
Invalid Email address</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="style5">
                Password:</td>
            <td class="style10">
                <asp:TextBox ID="TextBoxPS" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style17">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="TextBoxPS" ErrorMessage="Password is required." 
                    ForeColor="#FF5050"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style6">
                Confirm Password:</td>
            <td class="style8">
                <asp:TextBox ID="TextBoxCPS" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
            </td>
            <td class="style13">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="TextBoxCPS" ErrorMessage="Confirm Password is required." 
                    ForeColor="#FF5050"></asp:RequiredFieldValidator>
                <br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="TextBoxPS" ControlToValidate="TextBoxCPS" 
                    ErrorMessage="Both password must be the same." ForeColor="#FF5050"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                Team Leader Code:</td>
            <td class="auto-style2">
                <asp:TextBox ID="TextBoxTLCode" runat="server" Width="180px"></asp:TextBox>
            </td>
            <td class="auto-style3">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="TextBoxTLCode" ErrorMessage="Invalid Team Leader Code." 
                    ForeColor="#FF5050"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style18">
                Team Number:&nbsp; </td>
            <td class="style19">
                <asp:TextBox ID="TextBoxTeamNo" runat="server" Width="180px"></asp:TextBox>
            </td>
            <td class="style20">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="TextBoxTeamNo" ErrorMessage="Invalid Team Number." 
                    ForeColor="#FF5050"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style10">
                <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" style="height: 26px" />
                <input id="Reset1" type="reset" value="reset" onclick="return Reset1_onclick()" /></td>
            <td class="style15">
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>
