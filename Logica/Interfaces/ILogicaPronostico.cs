using System.Collections.Generic;
using EntidadesCompartidas;

namespace Logica.Interfaces
{
  public interface ILogicaPronostico
  {
    void Alta(Pronostico C, Meteorologo U);
    List<Pronostico> ListarPronosticoHoy();
    List<Pronostico> ListarUltimoAño(Empleado U);
  }
}
