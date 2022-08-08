using EntidadesCompartidas;
using System.Collections.Generic;

namespace Logica.Interfaces
{
  public interface ILogicaCiudad
  {
    void Alta(Ciudad C, Empleado U);
    void Eliminar(Ciudad C, Empleado U);
    void Modificar(Ciudad C, Empleado U);
    Ciudad Buscar(string Code, Usuario U);
    List<Ciudad> ListarCiudades(Empleado U);
    List<Ciudad> ListarCiudadesSinP(Empleado U);
    List<Ciudad> ListarCiudadesSinPAño(int año, Empleado U);

  }
}
