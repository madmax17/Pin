<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<body>
    <div>
    
    </div>
    <asp:Label ID="Label1" runat="server" Text="Odaberi kategoriju:"></asp:Label>

    <asp:DropDownList ID="DropDownList1" runat="server" 
        DataSourceID="SqlDataSource1" DataTextField="naziv" 
        DataValueField="kategorijaID" Height="16px" AutoPostBack="True" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [kategorije]">
    </asp:SqlDataSource>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="artiklID" DataSourceID="SqlDataSource2" 
        onrowcommand="GridView1_RowCommand" >
        <Columns>
           
            <asp:BoundField DataField="artiklID" HeaderText="artiklID" 
                SortExpression="artiklID" ReadOnly="True" />
            <asp:BoundField DataField="naziv" HeaderText="naziv" SortExpression="naziv" />

            <asp:BoundField DataField="opis" HeaderText="opis" SortExpression="opis" />
            <asp:BoundField DataField="katID" HeaderText="katID" SortExpression="katID" />

            <asp:BoundField DataField="cijena" HeaderText="cijena" 
                SortExpression="cijena" />
            <asp:ButtonField ButtonType="Image" CommandName="Select" 
                 ShowHeader="True" ImageUrl="~/cart_add.ico" />

        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [artikli] WHERE katID = @id">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="1" Name="id" QueryStringField="id" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/cart.aspx">Košarica</asp:HyperLink>
</body>
</html>
</asp:Content>