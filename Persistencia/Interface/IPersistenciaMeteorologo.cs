using EntidadesCompartidas;
using System.Collections.Generic;

namespace Persistencia.Class
{
  public interface IPersistenciaMeteorologo
  {
    void Agregar(Meteorologo M, Empleado U);
    void Eliminar(Meteorologo M, Empleado U);
    List<Meteorologo> ListarMeteorologoesSinP(Empleado U);
    List<Meteorologo> ListarMeteorologoesSinPAño(int año, Empleado U);
    Meteorologo Logueo(string usu, string pass);
    Meteorologo BuscarMeteorologo(string usu, Empleado U);
    void Modificar(Meteorologo M, Empleado U);
  }
}