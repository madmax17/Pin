using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using System.Web.Configuration;
using System.Data.SqlServerCe;

public partial class cart : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    private Kosarica kosarica;
    string naziv = "";
    string opis = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["kosarica"] == null)
        {
            kosarica = new Kosarica();
            Session["kosarica"] = kosarica;
        }
        else
        {
            kosarica = (Kosarica)Session["kosarica"];
        }
        if (!Page.IsPostBack)
            Povezi();

    }

    private void Povezi()
    {
        gvKosarica.DataSource = kosarica.DajKosaricu;
        gvKosarica.DataBind();

    }

    protected void gvKosarica_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvKosarica.EditIndex = e.NewEditIndex;
        Povezi();
    }

    protected void gvKosarica_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        gvKosarica.EditIndex = -1;
        Povezi();
    }

    protected void gvKosarica_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataControlFieldCell celija = (DataControlFieldCell)gvKosarica.Rows[e.RowIndex].Controls[3];
        TextBox t = (TextBox)celija.Controls[0];
        try
        {
            int kol = int.Parse(t.Text);
            if (kol > 0)
                kosarica.Promijeni(e.RowIndex, kol);
            else
                e.Cancel = true;
        }
        catch
        {
            e.Cancel = true;
        }
        gvKosarica.EditIndex = -1;

        Povezi();
    }

    protected void gvKosarica_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        kosarica.Brisi(e.RowIndex);
        Session["kosarica"] = kosarica;
        Povezi();
    }

    public string displayMembers()
    {
        var text = string.Empty;
        foreach (Stavak s in kosarica.DajKosaricu)
        {
            text += s.ToString() + Environment.NewLine;
        }
        return text;
    }


    protected void btnKupi_Click(object sender, EventArgs e)
    {
        lblIme.Text = Session["ime"].ToString();
        lblEmail.Text = Session["email"].ToString();
        lblAdresa.Text = Session["address"].ToString();
        decimal uk = kosarica.UkupnoUKosarici;
        lblUkupno.Text = uk.ToString() + " kn";
        textList.Visible = true;
        textList.Text = displayMembers();
        btnFinal.Visible = true;
        lblNaziv.Visible = true;
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        int idi=0;
        SqlCeConnection conn = new SqlCeConnection(connectionString);
        SqlCeCommand cmd = new SqlCeCommand();
        try
        {

            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT broj FROM narudzbe";

            SqlCeDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                idi = (int)reader[0];
            }
            reader.Close();
            conn.Close();

            idi += 1;

            string mail_send = Session["email"].ToString();

            MailMessage mail = new MailMessage("madmax171@gmail.com", mail_send, "Webshop narudžba#" + idi, "Vidi attachment"+naziv+" " + opis);

            mail.IsBodyHtml = false;

            Font titlefont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            Font boldTablefont = FontFactory.GetFont("Arial", 12, Font.BOLD);

            Document document = new Document(PageSize.A4, 20, 20, 20, 20);
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/docs/Web_narudzba#" + idi + ".pdf"), FileMode.Create));

            PdfPTable table = new PdfPTable(1);
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 10;
            table.SpacingAfter = 10;
            table.DefaultCell.Border = 0;
            table.SetWidths(new int[] { 4 });
            table.AddCell(new Phrase("ID Naziv Opis cijena kol kol*cijena"));
             
            document.Open();

            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/js/shop.jpg"));
            logo.ScaleAbsolute(185, 49);
            logo.SetAbsolutePosition(320, 790);
            document.Add(logo);

            document.Add(new Paragraph("Webshop Narudzba#"+idi, titlefont));
            document.Add(new Paragraph("Ime: " + Session["ime"].ToString()));
            document.Add(new Paragraph("Adresa: " + Session["address"].ToString()));
            document.Add(new Paragraph("email: " + Session["email"].ToString()));

            document.Add(table);

            foreach (Stavak s in kosarica.DajKosaricu)
            {
               document.Add(new Paragraph(s.ToString()));
            }

            document.Add(new Paragraph(" "));
            document.Add(new Paragraph("Ukupno: " + kosarica.UkupnoUKosarici.ToString() + " kn",boldTablefont));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph("Zahvaljujemo na narudžbi!", titlefont));
            document.Close();

            Attachment data = new Attachment(Server.MapPath("~/docs/Web_narudzba#" + idi + ".pdf"));
            mail.Attachments.Add(data);

            System.Net.NetworkCredential networkCredentials = new
                System.Net.NetworkCredential("username", "password");       /*tu idu username i šifra od email servisa koje sam maknuo da se ne vide*/

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = networkCredentials;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.Send(mail);

            lblUspjeh.Text = "Email uspješno poslan! Zavaljujemo na kupnji, dođite opet naivčine.";

            conn.Open();
            SqlCeCommand command = new SqlCeCommand("UPDATE narudzbe SET[broj]=@broj", conn);
            command.Parameters.AddWithValue("broj", idi);
            command.ExecuteScalar();
            conn.Close();

            conn.Open();
            SqlCeCommand command2 = new SqlCeCommand("INSERT INTO narudzbe2(id_narudzbe, id_klijenta) VALUES (@id_narudzbe, @id_klijenta)", conn);
            command2.Parameters.AddWithValue("id_narudzbe", idi);
            command2.Parameters.AddWithValue("id_klijenta", Session["id"].ToString());
            command2.ExecuteNonQuery();
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
    }
}