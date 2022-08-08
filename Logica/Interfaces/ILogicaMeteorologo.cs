using EntidadesCompartidas;
using System.Collections.Generic;

namespace Logica.Interfaces
{
  public interface ILogicaMeteorologo
  {
    void Alta(Meteorologo M, Empleado U);
    void Eliminar(Meteorologo M, Empleado U);
    void Modificar(Meteorologo M, Empleado U);
    Meteorologo Buscar(string nomUsu, Empleado E);
    Meteorologo Logueo(string nomUsu, string pass);
    List<Meteorologo> ListarMeteorologosSinP(Empleado U);
    List<Meteorologo> ListarMeterologosSinPAño(int año, Empleado U);
  }
}
