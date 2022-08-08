using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using RefWCF;

public partial class _Default : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
      CargarDocumento();
      Session["Usuario"] = null;
    }
  }

  public void CargarDocumento()
  {
    try
    {
      XElement doc = XElement.Parse(new ServicioClient().ListarPronosticoHoy());
      Session["Documento"] = doc;

      List<object> result = (from uP in (doc.Elements("Pronostico"))
                             select new
                             {
                               Pais = uP.Element("Pais").Value,
                               Ciudad = uP.Element("Ciudad").Value
                             }).ToList<object>();

      gvPronosticos.DataSource = result;
      gvPronosticos.DataBind();
    }
    catch(Exception ex)
    { lblError.Text = ex.Message; }
  }

  protected void btnBuscar_Click(object sender, EventArgs e)
  {
    XElement doc = (XElement)Session["Documento"];

    List<object> result = (from uP in (doc.Elements("Pronostico"))
                           where uP.Element("Ciudad").Value == txtCiudad.Text
                           select new
                           {
                             Pais = uP.Element("Pais").Value,
                             Ciudad = uP.Element("Ciudad").Value
                           }).ToList<object>();

    gvPronosticos.DataSource = result;
    gvPronosticos.DataBind();
  }

  protected void gvPronosticos_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      XElement xE = (XElement)Session["Documento"];

      var result = (from uN in xE.Elements("Pronostico")
                    from uC in uN.Elements("cadaHora")
                    where (string)uN.Element("Ciudad").Value == gvPronosticos.SelectedRow.Cells[0].Text && (string)uN.Element("Pais").Value == gvPronosticos.SelectedRow.Cells[1].Text
                    select uC);

      string _result = "<Pronostico>";
      foreach(var uN in result)
      {
        _result += uN.ToString();
      }
      _result += "</Pronostico>";

      xmlCadaHora.DocumentContent = _result;
    }
    catch(Exception ex)
    { lblError.Text = ex.Message; }
  }

  protected void btnLog_Click(object sender, EventArgs e)
  {
    Session["Usuario"] = null;
    Response.Redirect("~/Logueo.aspx");
  }

  protected void lbAtras_Click(object sender, EventArgs e)
  {
    CargarDocumento();
  }
}