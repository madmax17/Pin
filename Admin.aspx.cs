using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if ((txtUser.Text == "admin") && (txtPass.Text == "admin"))
        {
            lblUspjeh.Visible = true;
            panelAd.Visible = true;
        }
        else
            Response.Redirect("Default.aspx");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridView1.Rows[index];
        int id = Convert.ToInt32(row.Cells[0].Text);

        Response.ContentType = "Application/pdf";
        //Get the physical path to the file.
        string FilePath = MapPath("~/docs/Web_narudzba#" + id + ".pdf");
        //Write the file directly to the HTTP content output stream.
        Response.WriteFile(FilePath);
        Response.End();
    }
}