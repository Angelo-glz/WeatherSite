using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RefWCF;

public partial class ListadoSinAsignacion : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["Usuario"] is Meteorologo)
      Response.Redirect("~/GenerarPronostico.aspx");
  }

  protected void btnFiltrar_Click(object sender, EventArgs e)
  {
    try
    {
      List<Ciudad> ListaC = new List<Ciudad>();
      List<Meteorologo> ListaM = new List<Meteorologo>();
      Empleado E = (Empleado)Session["Usuario"];


      if (rbTipo.SelectedValue == null)
      {
        lblError.Text = "Debe seleccionar un tipo (Ciudad / Meteorologo)";
        return;
      }
      if (rbTipo.SelectedValue == "Ciudad")
      {
        if (txtAño.Text != "")
          ListaC.AddRange(new ServicioClient().ListarCiudadesSinPAño(Convert.ToInt32(txtAño.Text), E));
        else
          ListaC.AddRange(new ServicioClient().ListarCiudadesSinP(E));
      }
      else
      {
        if (txtAño.Text != "")
          ListaM.AddRange(new ServicioClient().ListarMeteorologoSinPAño(Convert.ToInt32(txtAño.Text), E));
        else
          ListaM.AddRange(new ServicioClient().ListarMeteorologoSinP(E));
      }

      if(ListaC != null && ListaC.Count > 0)
      {
        gvListado.DataSource = ListaC;
        gvListado.DataBind();
      }
      else
      {
        gvListado.DataSource = ListaM;
        gvListado.DataBind();
      }
    }
    catch(Exception ex)
    {
      lblError.Text = ex.Message;
    }
  }


}