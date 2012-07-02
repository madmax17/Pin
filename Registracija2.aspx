<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registracija2.aspx.cs" Inherits="Registracija2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ForeColor="#CC0000" />
&nbsp;<asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
    <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ErrorMessage="Molimo unesite korisničko ime." ControlToValidate="txtUser" 
        ForeColor="#CC0000"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
    <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ErrorMessage="Molimo unesite lozinku." ControlToValidate="txtPass" 
        ForeColor="#CC0000"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Confirm Pass:"></asp:Label>
    <asp:TextBox ID="txtPass2" runat="server" TextMode="Password"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
        ErrorMessage="Lozinke nisu iste." ControlToCompare="txtPass" 
        ControlToValidate="txtPass2" ForeColor="#CC0000"></asp:CompareValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ErrorMessage="Molim unesite ponovno lozinku." ControlToValidate="txtPass2" 
        ForeColor="#CC0000"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="Address:"></asp:Label>
    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ErrorMessage="Molimo unesite adresu." ControlToValidate="txtAddress" 
        ForeColor="#CC0000"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Text="Email:"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ErrorMessage="Molimo unesite email adresu." ControlToValidate="txtEmail" 
        ForeColor="#CC0000"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Button ID="btnReg" runat="server" onclick="btnReg_Click" 
        Text="Registracija" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblStanje" runat="server" ForeColor="#CC0000"></asp:Label>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [Kori]"></asp:SqlDataSource>
    </form>
</body>
</html>
