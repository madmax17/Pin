<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="username: "></asp:Label>
        <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="password: "></asp:Label>
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" 
            Text="Logiranje" />
        <br />
        <br />
        <asp:Label ID="lblUspjeh" runat="server" BackColor="White" ForeColor="Red" 
            Text="Dobrodošli admin!" Visible="False"></asp:Label>
    
    </div>
    <asp:Panel ID="panelAd" runat="server" Height="168px" Visible="False">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource2" onrowcommand="GridView1_RowCommand" >
            <Columns>
                <asp:BoundField DataField="id_narudzbe" HeaderText="id_narudzbe" 
                    SortExpression="id_narudzbe" />
                <asp:BoundField DataField="id_klijenta" HeaderText="id_klijenta" 
                    SortExpression="id_klijenta" />
                <asp:ButtonField CommandName="Select" HeaderText="Pdf" ShowHeader="True" 
                    Text="Pdf" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" 
            SelectCommand="SELECT * FROM [narudzbe2]"></asp:SqlDataSource>

        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">&lt;-back</asp:HyperLink>

    </asp:Panel>
    </form>
</body>
</html>
