using EntidadesCompartidas;
using System.Collections.Generic;

namespace Persistencia.Class
{
  public interface IPersistenciaCiudad
  {
    void Agregar(Ciudad C, Empleado U);
    Ciudad BuscarCiudad(string pCode, Usuario U);
    void Eliminar(Ciudad C, Empleado U);
    List<Ciudad> ListarCiudades(Empleado U);
    List<Ciudad> ListarCiudadesSinP(Empleado U);
    List<Ciudad> ListarCiudadesSinPAño(int año, Empleado U);
    void Modificar(Ciudad C, Empleado U);
  }
}