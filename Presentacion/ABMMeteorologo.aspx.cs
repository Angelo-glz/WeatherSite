using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RefWCF;

public partial class ABMMeteorologo : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["Usuario"] is Meteorologo)
      Response.Redirect("~/GenerarPronostico.aspx");

    if (!IsPostBack)
    {
      Session["Meteorologo"] = null;
      DeshabilitarABM();
    }

  }

  private void DeshabilitarABM()
  {

    btnBuscar.Enabled = true;
    txtNomUsu.Enabled = true;
    txtNomUsu.Text = "";

    btnAgregar.Enabled = false;
    btnEliminar.Enabled = false;
    btnModificar.Enabled = false;

    txtNomComp.Text = "";
    txtNomComp.Enabled = false;
    txtEmail.Text = "";
    txtEmail.Enabled = false;
    txtTel.Text = "";
    txtTel.Enabled = false;
    txtPass.Text = "";
    txtPass.Enabled = false;

    Session["Meteorologo"] = null;
  }

  private void Agregar()
  {
    btnBuscar.Enabled = false;
    txtNomUsu.Enabled = false;

    btnAgregar.Enabled = true;

    txtTel.Enabled = true;
    txtEmail.Enabled = true;
    txtNomComp.Enabled = true;
    txtPass.Enabled = true;
  }

  private void BM()
  {
    btnBuscar.Enabled = false;
    txtNomUsu.Enabled = false;

    txtEmail.Enabled = true;
    txtTel.Enabled = true;
    txtNomComp.Enabled = true;
    txtPass.Enabled = true;

    btnModificar.Enabled = true;
    btnEliminar.Enabled = true;
  }

  protected void btnBuscar_Click(object sender, EventArgs e)
  {
    try
    {
      Meteorologo M = new ServicioClient().BuscarMeteorologo(txtNomUsu.Text, (Empleado)Session["Usuario"]);

      if (M != null)
      {
        txtTel.Text = M.Tel.ToString();
        txtNomComp.Text = M.NomCompleto;
        txtPass.Text = M.Pass;
        txtEmail.Text = M.Email;

        Session["Meteorologo"] = M;

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
    Meteorologo M = null;

    try
    {
      M = new Meteorologo()
      {
        Email = txtEmail.Text,
        Tel = Convert.ToInt32(txtTel.Text),
        NomCompleto = txtNomComp.Text,
        Pass = txtPass.Text,
        NomUsu = txtNomUsu.Text
      };

    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
      return;
    }

    try
    {
      new ServicioClient().AltaMeteorologo(M, (Empleado)Session["Usuario"]);
      lblError.Text = "Se agrego el Meteorologo";
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
      Meteorologo M = (Meteorologo)Session["Meteorologo"];
      new ServicioClient().EliminarMeteorologo(M, (Empleado)Session["Usuario"]);
      lblError.Text = "Se elimino el meteorologo.";
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
      Meteorologo M = (Meteorologo)Session["Meteorologo"];
      M.Tel = Convert.ToInt32(txtTel.Text);
      M.NomCompleto = txtNomComp.Text;
      M.Pass = txtPass.Text;
      M.Email = txtEmail.Text;

      new ServicioClient().ModificarMeteorologo(M, (Empleado)Session["Usuario"]);
      lblError.Text = "Se modifico el meteorologo";
      DeshabilitarABM();
    }
    catch (Exception ex)
    {
      lblError.Text = ex.Message;
    }
  }

  protected void btnLimpiarForm_Click(object sender, EventArgs e)
  {
    DeshabilitarABM();
  }
}