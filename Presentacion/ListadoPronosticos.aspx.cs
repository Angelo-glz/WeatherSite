using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RefWCF;

public partial class ListadoPronosticos : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["Usuario"] is RefWCF.Meteorologo)
      Response.Redirect("~/Default.aspx");

    CargarGrilla();
  }

  private void CargarGrilla()
  {
    try
    {
      if (!IsPostBack)
      {
        List<Pronostico> listaP = new List<Pronostico>();
        listaP.AddRange(new ServicioClient().ListarPronosticoUltimoAño((Empleado)Session["Usuario"]));
        List<Ciudad> listaC = new List<Ciudad>();
        listaC.AddRange(new ServicioClient().ListarCiudades((Empleado)Session["Usuario"]));

        Session["Pronosticos"] = listaP;
        Session["Ciudades"] = listaC;

        GridView1.DataSource = listaP;
        GridView1.DataBind();

        foreach (Ciudad C in listaC)
        {
          DropDownList1.Items.Add(C.NombreCiudad);
        }
      }
    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
    }
  }

  protected void btnFiltrarFecha_Click(object sender, EventArgs e)
  {
    try
    {
      List<Pronostico> list = (List<Pronostico>)Session["Pronosticos"];
      List<Pronostico> result = (from uP in list
                                 where uP.Fecha.ToShortDateString() == Calendar1.SelectedDate.ToShortDateString()
                             select uP).ToList<Pronostico>();

      GridView1.DataSource = result;
      GridView1.DataBind();

    }catch(Exception ex) { lblError.Text = ex.Message; }
  }

  protected void btnFiltroCiudad_Click(object sender, EventArgs e)
  {
    try
    {
      List<Pronostico> list = (List<Pronostico>)Session["Pronosticos"];
      List<Pronostico> result = (from uP in list
                                 where uP.Ciudad.NombreCiudad == DropDownList1.SelectedValue
                                 select uP).ToList<Pronostico>();

      GridView1.DataSource = result;
      GridView1.DataBind();
    }
    catch (Exception ex) { lblError.Text = ex.Message; }
  }

  protected void btnResumen_Click(object sender, EventArgs e)
  {
    try
    {
      List<Pronostico> list = (List<Pronostico>)Session["Pronosticos"];
      List<object> result = (from uP in list
                             group uP by new { uP.Meteorologo.NomUsu }
                             into Grupo
                             select new {
                               NombreUsuario = Grupo.First().Meteorologo.NomUsu,
                               Cantidad = Grupo.Count()
                             }).ToList<object>();

      GridView2.DataSource = result;
      GridView2.DataBind();
    }
    catch (Exception ex) { lblError.Text = ex.Message; }
  }

  protected void btnLimpiar_Click(object sender, EventArgs e)
  {
    CargarGrilla();
  }
}