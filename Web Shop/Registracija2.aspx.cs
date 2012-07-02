using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlServerCe;
using System.Timers;
using System.Threading;

public partial class Registracija2 : System.Web.UI.Page
{
    string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool registriraj(string korisnickoIme, string lozinka, string address, string email)
    {
        SqlCeConnection conn = new SqlCeConnection(connString);
        try
        {
            Random r = new Random(System.DateTime.Now.Millisecond);

            string salt = r.Next().ToString();
            string hashiranaLoznika = Util.SHA256(lozinka);
            string hashiranaSlanaLoznika = Util.SHA256(salt + hashiranaLoznika);

            conn.Open();

            SqlCeCommand command = new SqlCeCommand
                ("INSERT INTO Kori(username,password,salt,address,email) VALUES (@username,@password,@salt,@address,@email)",conn);
            command.Parameters.AddWithValue("username", korisnickoIme);
            command.Parameters.AddWithValue("password", hashiranaSlanaLoznika);
            command.Parameters.AddWithValue("salt", salt);
            command.Parameters.AddWithValue("address", address);
            command.Parameters.AddWithValue("email", email);

            command.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        catch (Exception ex)
        {       
            return false;
        }
    }
    protected void btnReg_Click(object sender, EventArgs e)
    {
        if (registriraj(txtUser.Text, txtPass.Text, txtAddress.Text, txtEmail.Text))
        {
            lblStanje.Text = "Uspješno ste registrirani!";

            Thread.Sleep(5000);
            Response.Redirect("Default.aspx");
        }
        else
            lblStanje.Text = "Registracija nije uspjela!";
    }

}