using EntidadesCompartidas;

namespace Persistencia.Class
{
  public interface IPersistenciaEmpleado
  {
    void Agregar(Empleado E, Empleado U);
    Empleado BuscarEmpleado(string nomUsu, Empleado U);
    void Eliminar(Empleado E, Empleado U);
    Empleado Logueo(string usu, string pass);
    void Modificar(Empleado E, Empleado U);
  }
}