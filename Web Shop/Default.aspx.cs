using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && Request.QueryString["id"] != null)
            DropDownList1.SelectedValue = Request.QueryString["id"];
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?id=" + DropDownList1.SelectedValue.ToString());
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Kosarica kosarica;

        // Dohvati  row index iz CommandArgument svojstva.
        int index = Convert.ToInt32(e.CommandArgument);

        // Dohvati  row koji je okinuo događaj 

        GridViewRow row = GridView1.Rows[index];

        // Provjeri imamo li košaricu u session-u ako ima uzmi je, ako ne kreiraj // novu

        if (Session["kosarica"] == null)
        {
            kosarica = new Kosarica();
            Session["kosarica"] = kosarica;
        }
        else
            kosarica = (Kosarica)Session["kosarica"];

        // Iz GridView-a pročitaj vrijednosti ćelija (paziti na redoslijed kojim //su navedene u GV-u) i prebaci u pravi tip podataka
        int id = Convert.ToInt32(row.Cells[0].Text); //prvi stupac u  GV
        string naziv = row.Cells[1].Text;
        string opis = row.Cells[2].Text;
        decimal cijena = Convert.ToDecimal(row.Cells[4].Text);

        // Dodaj naš artikl u košaricu i spremi u Session
        kosarica.Dodaj(id, naziv, opis, cijena);
        Session["kosarica"] = kosarica;

    }
}