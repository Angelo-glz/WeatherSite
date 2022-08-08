using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logueo : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    Session["Usuario"] = null;
  }

  protected void btnLogueo_Click(object sender, EventArgs e)
  {
    try
    {
      RefWCF.Usuario Usu = null;

      Usu = new RefWCF.ServicioClient().LogueoEmpleado(txtNomUsu.Text, txtPass.Text);
      if (Usu == null)
        Usu = new RefWCF.ServicioClient().LogueoMeteorologo(txtNomUsu.Text, txtPass.Text);
      else
      {
        Response.Redirect("~/ListadoSinAsignacion.aspx", false);
      }

      if (Usu == null)
        lblError.Text = "Nombre de usuario o contraseña incorrectos.";
      if(Usu is RefWCF.Meteorologo)
        Response.Redirect("~/GenerarPronostico.aspx", false);

      Session["Usuario"] = Usu;
    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
      txtNomUsu.Text = txtPass.Text = "";
    }
  }
}