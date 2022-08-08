using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master : System.Web.UI.MasterPage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["Usuario"] == null)
      Response.Redirect("~/Logueo.aspx");

    Label1.Text = ((RefWCF.Usuario)Session["Usuario"]).NomCompleto;
  }

  protected void btnSalir_Click(object sender, EventArgs e)
  {
    Session["Usuario"] = null;
    Response.Redirect("~/Default.aspx");
  }
}
