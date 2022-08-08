using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class ABMEmpleado : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["Usuario"] is Meteorologo)
      Response.Redirect("~/Logueo.aspx");

    if (!IsPostBack)
    {
      Session["Empleado"] = null;
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
    txtCargaH.Text = "";
    txtCargaH.Enabled = false;
    txtPass.Text = "";
    txtPass.Enabled = false;

    Session["Empleado"] = null;
  }

  private void Agregar()
  {
    btnBuscar.Enabled = false;
    txtNomUsu.Enabled = false;

    btnAgregar.Enabled = true;

    txtCargaH.Enabled = true;
    txtNomComp.Enabled = true;
    txtPass.Enabled = true;
  }

  private void BM()
  {
    btnBuscar.Enabled = false;
    txtNomUsu.Enabled = false;

    txtCargaH.Enabled = true;
    txtNomComp.Enabled = true;
    txtPass.Enabled = true;

    btnModificar.Enabled = true;
    btnEliminar.Enabled = true;
  }

  protected void btnBuscar_Click(object sender, EventArgs e)
  {
    try
    {
      Empleado E = new ServicioClient().BuscarEmpleado(txtNomUsu.Text, (Empleado)Session["Usuario"]);

      if (E != null)
      {
        txtCargaH.Text = E.CargaHoraria.ToString();
        txtNomComp.Text = E.NomCompleto;
        txtPass.Text = E.Pass;

        Session["Empleado"] = E;

        BM();
      }
      else
        Agregar();
    }
    catch(Exception ex)
    {
      lblError.Text = ex.Message;
    }
  }

  protected void btnAgregar_Click(object sender, EventArgs e)
  {
    Empleado E = null;

    try
    {
      E = new Empleado()
      {
        CargaHoraria = Convert.ToInt32(txtCargaH.Text),
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
      new ServicioClient().AltaEmpleado(E, (Empleado)Session["Usuario"]);
      lblError.Text = "Se agrego el Empleado";
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
      Empleado E = (Empleado)Session["Empleado"];
      E.CargaHoraria = Convert.ToInt32(txtCargaH.Text);
      E.NomCompleto = txtNomComp.Text;
      E.Pass = txtPass.Text;

      new ServicioClient().ModificarEmpleado(E, (Empleado)Session["Usuario"]);
      lblError.Text = "Se modifico el empleado";
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
      Empleado E = (Empleado)Session["Empleado"];
      new ServicioClient().EliminarEmpleado(E, (Empleado)Session["Usuario"]);
      lblError.Text = "Se elimino el empleado.";
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