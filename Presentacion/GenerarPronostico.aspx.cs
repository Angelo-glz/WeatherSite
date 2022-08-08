using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RefWCF;

public partial class GenerarPronostico : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["Usuario"] is Empleado)
      Response.Redirect("~/ABMEmpleado.aspx");

    if (!IsPostBack)
      Limpiar();
  }

  public void Limpiar()
  {
    txtCiudad.Text = "";
    txtHora.Text = "";
    txtProbLluvia.Text = "";
    txtProbTormenta.Text = "";
    txtTempMax.Text = "";
    txtTempMinima.Text = "";
    txtVelViento.Text = "";
    Session["listaCadaHora"] = new List<CadaHora>();
  }

  protected void btnAlta_Click(object sender, EventArgs e)
  {
    try
    {
      if (clFecha.SelectedDate < DateTime.Now)
      {
        lblErrorFYC.Text = "La fecha del pronostico es invalida.";
        return;
      }

      List<CadaHora> lista = (List<CadaHora>)Session["listaCadaHora"];

      Pronostico P = new Pronostico()
      {
        CadaHora = lista.ToArray(),
        Ciudad = new ServicioClient().BuscarCiudad(txtCiudad.Text, (Usuario)Session["Usuario"]),
        Fecha = clFecha.SelectedDate,
        Meteorologo = (Meteorologo)Session["Usuario"]
      };

      new ServicioClient().AltaPronostico(P, (Meteorologo)Session["Usuario"]);
      Limpiar();
      lblErrorPreview.Text = "Se dio de alta el pronostico con exito.";
    }
    catch (Exception ex)
    {
      lblErrorPreview.Text = ex.Message;
    }
  }

  protected void btnAgregarHora_Click(object sender, EventArgs e)
  {
    try
    {
      List<CadaHora> list = (List<CadaHora>)Session["listaCadaHora"];

      CadaHora rep = null;
      rep = list.Find(r => txtHora.Text == r.Hora.ToString());

      if(rep != null)
      {
        lblErrorCH.Text = "Ya hay un pronostico asignado para esa hora.";
        return;
      }

      CadaHora H = new CadaHora()
      {
        Hora = Convert.ToInt16(txtHora.Text),
        TempMax = Convert.ToInt16(txtTempMax.Text),
        TempMin = Convert.ToInt16(txtTempMinima.Text),
        ProbLluvia = Convert.ToInt16(txtProbLluvia.Text),
        ProbTormenta = Convert.ToInt16(txtProbTormenta.Text),
        VelViento = Convert.ToInt16(txtVelViento.Text),
        TipoCielo = ddlTipoCielo.SelectedValue
      };

      if(!Validar(H))
      {
        lblErrorCH.Text = "Los datos para el pronostico son invalidos.";
        return;
      }

      ((List<CadaHora>)Session["listaCadaHora"]).Add(H);

      gvCadaHora.DataSource = (List<CadaHora>)Session["listaCadaHora"];
      gvCadaHora.DataBind();

    }
    catch(Exception ex)
    {
      lblErrorCH.Text = ex.Message;
    }
  }

  public bool Validar(CadaHora H)
  {
    if (H.Hora < 0)
      return false;
    if (H.TempMax < H.TempMin)
      return false;
    if (H.ProbLluvia < 0)
      return false;
    if (H.ProbTormenta < 0)
      return false;


    return true;
  }

  protected void btnLimpiar_Click(object sender, EventArgs e)
  {
    Limpiar();
  }
}