using EntidadesCompartidas;

namespace Logica.Interfaces
{
  public interface ILogicaEmpleado
  {
    void Alta(Empleado E, Empleado U);
    void Eliminar(Empleado E, Empleado U);
    void Modificar(Empleado E, Empleado U);
    Empleado Buscar(string nomUsu, Empleado U);
    Empleado Logueo(string nomUsu, string pass);
  }
}
