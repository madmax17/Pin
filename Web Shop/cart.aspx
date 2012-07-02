<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="cart.aspx.cs" Inherits="cart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>

    <asp:GridView ID="gvKosarica" runat="server" AutoGenerateColumns="False" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowDeleting="gvKosarica_RowDeleting" OnRowCancelingEdit="gvKosarica_RowCancelingEdit" OnRowEditing="gvKosarica_RowEditing" OnRowUpdating="gvKosarica_RowUpdating">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Naziv" HeaderText="Naziv" ReadOnly="True" />
                <asp:BoundField DataField="Opis" HeaderText="Opis" ReadOnly="True" />
                <asp:BoundField DataField="Cijena" HeaderText="Cijena" ReadOnly="True" />
                <asp:BoundField DataField="Kolicina" HeaderText="Količina" />
                <asp:BoundField DataField="Ukupno" HeaderText="Ukupno" ReadOnly="True" />
                <asp:CommandField ButtonType="Link" DeleteText="Briši" ShowDeleteButton="True"  />
                <asp:CommandField ButtonType="Link" CancelText="Otkaži" EditText="Količina" ShowEditButton="True"
                    UpdateText="Spremi" />
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>

    <asp:Button ID="btnKupi" runat="server" onclick="btnKupi_Click" 
        Text="Izračunaj" />
    <br />
    <asp:Label ID="lblNaziv" runat="server" 
        Text="id   naziv      opis     cijena     cijena*kol" Visible="False"></asp:Label>
&nbsp;
    <br />
    <asp:TextBox ID="textList" runat="server" Height="76px" 
         Width="306px" BorderColor="White" TextMode="MultiLine" Visible="False"></asp:TextBox>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Narudžba za korisnika: "></asp:Label>
    <asp:Label ID="lblIme" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Email: "></asp:Label>
    <asp:Label ID="lblEmail" runat="server"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <asp:Label ID="Label3" runat="server" Text="Adresa:"></asp:Label>
    <asp:Label ID="lblAdresa" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Ukupno: " Font-Bold="True"></asp:Label>
    <asp:Label ID="lblUkupno" runat="server" Font-Bold="True"></asp:Label>
    <p>
        <asp:Button ID="btnFinal" runat="server" onclick="btnFinal_Click" 
            Text="Finaliziraj kupnju" Visible="False" />
    </p>
    <asp:Label ID="lblUspjeh" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">&lt;-back</asp:HyperLink>
    </form>
</body>
</html>
