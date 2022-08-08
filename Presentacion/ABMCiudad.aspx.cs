using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RefWCF;

public partial class ABMCiudad : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["Usuario"] is Meteorologo)
      Response.Redirect("~/GenerarPronostico.aspx");

    if (!IsPostBack)
    {
      Session["Ciudad"] = null;
      DeshabilitarABM();
    }
  }

  private void DeshabilitarABM()
  {
    btnBuscar.Enabled = true;
    txtCodigo.Enabled = true;
    txtCodigo.Text = "";

    btnAgregar.Enabled = false;
    btnEliminar.Enabled = false;
    btnModificar.Enabled = false;

    txtNomCiudad.Text = "";
    txtNomCiudad.Enabled = false;
    txtNomPais.Text = "";
    txtNomPais.Enabled = false;

    Session["Ciudad"] = null;
  }

  private void Agregar()
  {
    btnBuscar.Enabled = false;
    txtCodigo.Enabled = false;

    btnAgregar.Enabled = true;

    txtNomCiudad.Enabled = true;
    txtNomPais.Enabled = true;
  }

  private void BM()
  {
    btnBuscar.Enabled = false;
    txtCodigo.Enabled = false;

    txtNomCiudad.Enabled = true;
    txtNomPais.Enabled = true;

    btnModificar.Enabled = true;
    btnEliminar.Enabled = true;
  }


  protected void btnBuscar_Click(object sender, EventArgs e)
  {
    try
    {
      Ciudad C = new ServicioClient().BuscarCiudad(txtCodigo.Text, (Empleado)Session["Usuario"]);

      if (C != null)
      {
        txtCodigo.Text = C.Codigo;
        txtNomCiudad.Text = C.NombreCiudad;
        txtNomPais.Text = C.NombrePais;

        Session["Ciudad"] = C;

        BM();
      }
      else
        Agregar();
    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
    }

  }

  protected void btnAgregar_Click(object sender, EventArgs e)
  {
    Ciudad C = null;

    try
    {
      C = new Ciudad()
      {
        Codigo = txtCodigo.Text,
        NombrePais = txtNomPais.Text,
        NombreCiudad = txtNomCiudad.Text
      };

    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
      return;
    }

    try
    {
      new ServicioClient().AltaCiudad(C, (Empleado)Session["Usuario"]);
      lblError.Text = "Se agrego la ciudad.";
      DeshabilitarABM();
    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
    }
  }

  protected void btnEliminar_Click(object sender, EventArgs e)
  {
    try
    {
      Ciudad C = (Ciudad)Session["Ciudad"];
      new ServicioClient().EliminarCiudad(C, (Empleado)Session["Usuario"]);
      lblError.Text = "Se elimino la ciudad.";
      DeshabilitarABM();

    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
    }

  }

  protected void btnModificar_Click(object sender, EventArgs e)
  {
    try
    {
      Ciudad C = (Ciudad)Session["Ciudad"];
      C.Codigo = txtCodigo.Text;
      C.NombreCiudad = txtNomCiudad.Text;
      C.NombrePais = txtNomPais.Text;

      new ServicioClient().ModificarCiudad(C, (Empleado)Session["Usuario"]);
      lblError.Text = "Se modifico la ciudad.";
      DeshabilitarABM();
    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
    }

  }

  protected void btnLimpiar_Click(object sender, EventArgs e)
  {
    DeshabilitarABM();
  }
}