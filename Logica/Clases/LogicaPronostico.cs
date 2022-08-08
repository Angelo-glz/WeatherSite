using System.Collections.Generic;
using EntidadesCompartidas;
using Logica.Interfaces;

namespace Logica
{
  internal class LogicaPronostico : ILogicaPronostico
  {
    private static LogicaPronostico _instance = null;
    private LogicaPronostico() { }
    public static LogicaPronostico GetInstance()
    {
      if (_instance == null) _instance = new LogicaPronostico();
      return _instance;
    }

    public void Alta(Pronostico P, Meteorologo U)
    {
      if (P.Fecha < System.DateTime.Now)
        throw new System.Exception("No se pueden crear pronosticos para el pasado.");
      Persistencia.Fabrica.GetPP().Agregar(P, U);
    }

    public List<Pronostico> ListarPronosticoHoy()
    {
      return Persistencia.Fabrica.GetPP().ListarHoy();
    }

    public List<Pronostico> ListarUltimoAño(Empleado U)
    {
      return Persistencia.Fabrica.GetPP().ListarUltimoAño(U);
    }
  }
}
