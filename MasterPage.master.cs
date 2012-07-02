using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Web.Configuration;

public partial class MasterPage : System.Web.UI.MasterPage
{
    string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;           

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["isLogged"] == null) || (Session["isLogged"] == "Ne"))
        {
            login.Visible = true;
            logout.Visible = false;
            lblLog.Visible = false;
            lblGreska.Visible = true;
            ContentPlaceHolder1.Visible = false;
            btnShop.Visible = false;
        }
        else
        {
            login.Visible = false;
            logout.Visible = true;
            txtUsr.Visible = false;
            txtPass.Visible = false;
            lblIme.Visible = false;
            lblPass.Visible = false;
            lblGreska.Visible = false;
            LinkButton1.Visible = false;
            ContentPlaceHolder1.Visible = true;
            btnShop.Visible = false;
            loggedUser.Text = Session["ime"].ToString();
        }
       

    }
    protected void login_Click(object sender, EventArgs e)
    {
        SqlCeConnection conn = new SqlCeConnection(connectionString);

        try
        {

            string lozinka = txtPass.Text;
            string hashiranaLoznika = Util.SHA256(lozinka);
            conn.Open();

            SqlCeCommand cmd1 = new SqlCeCommand();
            cmd1.Connection = conn;
            cmd1.CommandType = System.Data.CommandType.Text;
            cmd1.CommandText = "SELECT salt FROM Kori WHERE username ='" + txtUsr.Text + "'";
            string salt2 = "null";
            SqlCeDataReader reader1 = cmd1.ExecuteReader();

            if (reader1.Read())
            {
                salt2 = reader1[0].ToString();
            }
            string hashiranaSlanaLoznika = Util.SHA256(salt2 + hashiranaLoznika);
            reader1.Close();
            conn.Close();

            conn.Open();
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT id,address,email FROM Kori WHERE username ='" + txtUsr.Text + "' AND password = '" + hashiranaSlanaLoznika + "'";

            SqlCeDataReader reader = cmd.ExecuteReader();

            if (reader.Read()) //stani na prvi zapis – false ako ga nema
            {
                Session["ime"] = txtUsr.Text;
                Session["id"] = reader[0].ToString();
                Session["address"] = reader[1].ToString();
                Session["email"] = reader[2].ToString();
                lblGreska.Text = "Dobrodošao!";
                Session["isLogged"] = "Da";
                loggedUser.Text = Session["ime"].ToString();
                lblLog.Visible = true;
                logout.Visible = true;
                btnShop.Visible = true;
               // Response.Redirect("About.aspx");
            }
            else
            {
                lblGreska.Text = "Korisnik ne postoji u bazi!";
            }

            reader.Close();

        }
        catch (Exception ex)
        {
            lblGreska.Text = "Greška pri prijavi: " + ex.Message;
        }
        finally
        {
            conn.Close();
        }

    }

    protected void logout_Click(object sender, EventArgs e)
    {
        Session["isLogged"] = "Ne";
        Response.Redirect("Default.aspx");
        lblGreska.Visible = false;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registracija2.aspx");
    }
    protected void btnShop_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
