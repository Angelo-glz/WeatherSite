using EntidadesCompartidas;
using System.Collections.Generic;

namespace Persistencia.Class
{
  public interface IPersistenciaPronostico
  {
    void Agregar(Pronostico P, Meteorologo U);
    List<Pronostico> ListarHoy();
    List<Pronostico> ListarUltimoAño(Empleado U);
  }
}