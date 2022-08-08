using System.Collections.Generic;
using EntidadesCompartidas;
using Logica.Interfaces;

namespace Logica
{
  internal class LogicaMeteorologo : ILogicaMeteorologo
  {
    private static LogicaMeteorologo _instance = null;
    private LogicaMeteorologo() { }
    public static LogicaMeteorologo GetInstance()
    {
      if (_instance == null) _instance = new LogicaMeteorologo();
      return _instance;
    }

    public void Alta(Meteorologo M, Empleado U)
    {
      Persistencia.Fabrica.GetPM().Agregar(M, U);
    }

    public Meteorologo Buscar(string nomUsu, Empleado U)
    {
      return Persistencia.Fabrica.GetPM().BuscarMeteorologo(nomUsu, U);
    }

    public void Eliminar(Meteorologo M, Empleado U)
    {
      Persistencia.Fabrica.GetPM().Eliminar(M, U);

    }

    public List<Meteorologo> ListarMeteorologosSinP(Empleado U)
    {
      return Persistencia.Fabrica.GetPM().ListarMeteorologoesSinP(U);
    }

    public List<Meteorologo> ListarMeterologosSinPAño(int año, Empleado U)
    {
      return Persistencia.Fabrica.GetPM().ListarMeteorologoesSinPAño(año, U);
    }

    public Meteorologo Logueo(string nomUsu, string pass)
    {
      return Persistencia.Fabrica.GetPM().Logueo(nomUsu, pass);
    }

    public void Modificar(Meteorologo M, Empleado U)
    {
      Persistencia.Fabrica.GetPM().Modificar(M, U);
    }
  }
}
